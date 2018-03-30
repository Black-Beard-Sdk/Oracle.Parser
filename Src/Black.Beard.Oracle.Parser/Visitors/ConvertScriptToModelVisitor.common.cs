﻿using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;
using Antlr4.Runtime.Tree;
using System.Text;
using System.Diagnostics;
using Bb.Oracle.Helpers;
using Bb.Oracle.Validators;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor : IDbModelVisitor
    {


        public PolicyBehavior Policy { get; set; }

        public override object VisitErrorNode(IErrorNode node)
        {
            if (node.Parent is ParserRuleContext n)
            {
                if (n.exception != null)
                    AppendException(n.exception);

                return null;
            }

            //string message = $"{node.Symbol.Text} at line {node.Symbol.Line} column {node.Symbol.Column}, offset {node.Symbol.StartIndex}";
            //_anomalies.Add(new Error() { Exception = null, Message = message, File = this.File });
            //Trace.WriteLine(message + " in " + this.File);
            return null;
        }

        public IDisposable Enqueue(object item)
        {
            var s = new Trash(this._statck, item);
            _statck.Push(s);
            return s;
        }

        public T Current<T>()
        {

            T result = default(T);

            if (this._statck.Count > 0)
            {
                var _result = this._statck.Peek()._item;
                if (_result is T s)
                    result = s;
            }

            return result;

        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }

        public OracleDatabase Db { get; }

        public EventParsers Events { get { return this._events; } }

        public Errors Errors { get { return this._anomalies; } }

        public List<FileElement> GetFileElements(FileCollection files, ParserRuleContext context)
        {

            List<FileElement> _files = new List<FileElement>();
            _files.AddRange(files);
            _files.Add(new FileElement()
            {
                Path = this.Filename,
                Location = new Location()
                {
                    Line = context.Start.Line,
                    Column = context.Start.Column,
                    Offset = context.Start.StartIndex,
                    Length = context.Stop.StopIndex - context.Start.StartIndex
                }
            });

            return _files;

        }

        public List<ParserValidator> Validators { get { return this._validators; } }

        public string Filename { get; set; }


        private Stack<IRuleNode> _models = new Stack<IRuleNode>();
        private Errors _anomalies = new Errors();
        private EventParsers _events = new EventParsers();
        private StringBuilder _initialSource;
        private readonly Stack<Trash> _statck = new Stack<Trash>();
        private List<ParserValidator> _validators = new List<ParserValidator>();

        private void AppendEventParser(EventParser eventParser)
        {
            this._events.Add(eventParser);
        }

        private void AppendEventParser(string message, string name, Models.KindModelEnum kind, ParserRuleContext context, FileCollection files)
        {
            AppendEventParser(new EventParser()
            {
                Files = GetFileElements(files, context),
                Kind = kind,
                OjectName = name,
                ValidatorName = this.GetType().Name,
                Message = message,
            });

        }


        private void AppendException(RecognitionException exception)
        {

            var t = exception.OffendingToken;
            StringBuilder txt;

            if (exception.Context.Parent != null)
                txt = GetText(exception.Context.Parent);
            else
                txt = GetText(t.StartIndex, t.StopIndex + 1);

            string message = $"'{exception.Message}' at line '{t.Line} {txt.ToString()}' with token '{t.Text}' column {t.Column}, offset {t.StartIndex}";

            var error = new Error() { Exception = exception, Message = message, File = GetFileElement(exception.OffendingToken) };
            _anomalies.Add(error);

            AppendEventParser(GetEventParser(error, string.Empty, KindModelEnum.Undefined));

            Trace.WriteLine(message + " in " + this.Filename);

        }

        private StringBuilder GetText(RuleContext context)
        {

            if (context is ParserRuleContext s)
                return GetText(s.Start.StartIndex, s.Stop.StopIndex + 1);
            return new StringBuilder();
        }

        private StringBuilder GetText(int startIndex, int stopIndex)
        {



            int length = stopIndex - startIndex;

            StringBuilder sb2 = new StringBuilder(length > 0 ? length : 0);

            if (length > 0)
            {
                char[] ar = new char[length];
                _initialSource.CopyTo(startIndex, ar, 0, length);
                sb2.Append(ar);
            }

            return sb2;

        }

        private void AppendException(Exception exception, ParserRuleContext context)
        {
            var t = context;
            string message = $"{exception.Message}. {t.GetText()} at line {t.Start.Line} column {t.Start.Column}, offset {t.Start.StartIndex}";
            _anomalies.Add(new Error() { Exception = exception, Message = message, File = GetFileElement(context.Start) });
            Trace.WriteLine(message + " in " + this.Filename);
        }

        private EventParser GetEventParser(Error error, string objectName, KindModelEnum kind)
        {
            return GetEventParser(error.Message, objectName, kind, error.File);
        }

        private EventParser GetEventParser(string message, string objectName, KindModelEnum kind, params FileElement[] fileElements)
        {

            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("message is empty", nameof(message));

            if (string.IsNullOrEmpty(objectName))
                objectName = string.Empty;
            //throw new ArgumentException("objectName is empty", nameof(objectName));

            var eventParser = new EventParser()
            {
                Message = message,
                Type = kind.ToString(),
                OjectName = objectName,
                ValidatorName = this.GetType().FullName,
            };

            eventParser.Files.AddRange(fileElements);

            return eventParser;

        }

        private void AppendFile(ItemBase item, IToken token)
        {
            item.Files.Add(this.GetFileElement(token));
        }

        private FileElement GetFileElement(IToken token)
        {
            return new FileElement()
            {
                Path = this.Filename,
                Location = new Location()
                {
                    Offset = token.StartIndex,
                    Line = token.Line,
                    Column = token.Column,
                    Length = token.StopIndex - token.StartIndex,
                }
            };
        }

        private class Trash : IDisposable
        {

            public Trash(Stack<Trash> statck, object item)
            {
                this._statck = statck;
                this._item = item;
            }

            private readonly Stack<Trash> _statck;
            public readonly object _item;

            public void Dispose()
            {
                this._statck.Pop();
            }

        }

    }

}

/*
 
     if (context.exception != null)
     {
        // this._initialSource = new StringBuilder(context.Start.InputStream.ToString());
        AppendException(context.exception);
        return null;
    }

     */
