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

        #region Resolvers

        private void ResolveTable(string tableName)
        {
            var _tableName = CleanName(tableName.ToUpper());
            var items = this.db.Tables.Where(c => c.Name == _tableName).ToList();

            throw new NotImplementedException();

        }

        private void ResolveTable(string schema, string tableName)
        {
            var _schema = CleanName(schema.ToUpper());
            var _tableName = CleanName(tableName.ToUpper());

            var t = db.Tables[$"{_schema}.{_tableName}"];

            throw new NotImplementedException();

        }

        private void ResolveColumn(string tableName, string columnName)
        {
            var _columnName = CleanName(columnName.ToUpper());
            var _tableName = CleanName(tableName.ToUpper());
            var items = this.db.Tables.Where(c => c.Name == _tableName).ToList();

            throw new NotImplementedException();

        }

        private void ResolveColumn(string schema, string tableName, string columnName)
        {
            var _schema = CleanName(schema.ToUpper());
            var _tableName = CleanName(tableName.ToUpper());
            var _columnName = CleanName(columnName.ToUpper());

            var t = db.Tables[$"{_schema}.{_tableName}"];

            throw new NotImplementedException();

        }


        #endregion Resolvers



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
                result = (T)this._statck.Peek()._item;

            if (object.Equals(result, default(T)))
                Stop();

            return result;
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }

        public OracleDatabase db { get; }

        public List<EventParser> Events { get { return this._events; } }

        public List<Error> Errors { get { return this._anomalies; } }

        public string Filename { get; set; }


        private Stack<IRuleNode> _models = new Stack<IRuleNode>();
        private List<Error> _anomalies = new List<Error>();
        private List<EventParser> _events = new List<EventParser>();
        private StringBuilder _initialSource;
        private readonly Stack<Trash> _statck = new Stack<Trash>();

        private void AppendEventParser(EventParser eventParser)
        {
            this._events.Add(eventParser);
        }

        private void AppendException(RecognitionException exception)
        {

            StringBuilder txt = GetText(exception.Context.Parent);

            var t = exception.OffendingToken;
            string message = $"'{exception.Message}' at line '{t.Line} {txt.ToString()}' with token '{t.Text}' column {t.Column}, offset {t.StartIndex}";

            var error = new Error() { Exception = exception, Message = message, File = GetFileElement(exception.OffendingToken) };
            _anomalies.Add(error);

            AppendEventParser(GetEventParser(error, string.Empty, SqlKind.Undefined));

            Trace.WriteLine(message + " in " + this.Filename);

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
            Trace.WriteLine(message + " in " + this.Filename);
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
