using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;
using Antlr4.Runtime.Tree;
using System.Text;
using System.Diagnostics;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        private void AppendException(RecognitionException exception)
        {

            StringBuilder txt = GetText(exception.Context.Parent);

            var t = exception.OffendingToken;
            string message = $"'{exception.Message}' at line '{t.Line} {txt.ToString()}' with token '{t.Text}' column {t.Column}, offset {t.StartIndex}";

            var error = new Error() { Exception = exception, Message = message, File = GetFileElement(exception.OffendingToken) };
            _anomalies.Add(error);

            AppendEventParser(GetEventParser(error, string.Empty, SqlKind.Undefined));

            Debug.WriteLine(message + " in " + this.Filename);

        }

        private StringBuilder GetText(RuleContext context)
        {
            if (context is ParserRuleContext s)
            {
                char[] ar = new char[s.Stop.StopIndex - s.Start.StartIndex];
                _initialSource.CopyTo(s.Start.StartIndex, ar, 0, ar.Length);
                StringBuilder sb2 = new StringBuilder(ar.Length);
                sb2.Append(ar);

                return sb2;
            }

            Stop();
            return new StringBuilder();

        }

        private void AppendException(Exception exception, ParserRuleContext context)
        {
            var t = context;
            string message = $"{exception.Message}. {t.GetText()} at line {t.Start.Line} column {t.Start.Column}, offset {t.Start.StartIndex}";
            _anomalies.Add(new Error() { Exception = exception, Message = message, File = GetFileElement(context.Start) });
            System.Diagnostics.Debug.WriteLine(message + " in " + this.Filename);
        }

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
            //System.Diagnostics.Debug.WriteLine(message + " in " + this.File);
            return null;
        }

        private void AppendEventParser(EventParser eventParser)
        {
            this._events.Add(eventParser);
        }

        private EventParser GetEventParser(Error error, string objectName, SqlKind kind)
        {
            return GetEventParser(error.Message, objectName, kind, error.File);
        }

        private EventParser GetEventParser(string message, string objectName, SqlKind kind, params FileElement[] fileElements)
        {

            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("message is empty", nameof(message));

            if (string.IsNullOrEmpty(objectName))
                throw new ArgumentException("objectName is empty", nameof(objectName));

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

    }

}
