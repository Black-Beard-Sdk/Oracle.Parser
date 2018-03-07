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
using Bb.Oracle.Helpers;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        #region Resolvers

        private ItemBase ResolveTable(ObjectReference ob)
        {

            if (ob.Path.Length == 1)
            {
                var items = ResolveTables(ob.Path[0]);
                if (items.Count == 1)
                    return items[0];
                else
                {
                    Stop();

                    items = ResolveTypes(ob.Path[0]);
                    if (items.Count == 1)
                        return items[0];
                    else
                    {
                        Stop();

                    }
                }
            }
            else if (ob.Path.Length == 2)
                return ResolveTable(ob.Path[0], ob.Path[1]);

            Stop();
            return null;

        }

        private List<ItemBase> ResolveTypes(string typeName)
        {
            var _tableName = typeName.CleanName();
            var items = this.db.Types.Where(c => c.GetName() == _tableName).Cast<ItemBase>().ToList();
            Debug.Assert(items.Count > 0);
            return items;
        }

        private List<ItemBase> ResolveTables(string tableName)
        {
            var _tableName = tableName.CleanName();
            var items = this.db.Tables.Where(c => c.GetName() == _tableName).Cast<ItemBase>().ToList();
            Debug.Assert(items.Count > 0);
            return items;
        }

        private ItemBase ResolveTable(string schema, string tableName)
        {
            var _schema = schema.CleanName();
            var _tableName = tableName.CleanName();

            var t = db.Tables[$"{_schema}.{_tableName}"];

            if (t == null)
            {
                Stop();
                // Search in type

            }

            Debug.Assert(t != null);

            return t;

        }

        private ColumnModel ResolveColumn(ObjectReference ob)
        {

            var tt = this;
            if (ob.Path.Length == 2)
            {
                // CRM_CORE.SITE
                // MASTER.SITE
                // ROUTING_CORE.SITE
                var items = ResolveColumns(ob.Path[0], ob.Path[1]);
                if (items.Count == 1)
                    return items[0];

                else
                {
                    Stop();

                    if (ob.SchemaCaller != null)
                    {
                        var schemaCaller = ob.SchemaCaller;
                        var syn = this.db.Synonymes.Where(c => c.IsPublic || c.SchemaName.ToUpper() == schemaCaller || c.SynonymOwner.ToUpper() == schemaCaller).ToList();

                    }

                }
            }
            else if (ob.Path.Length == 3)
                return ResolveColumn(ob.Path[0], ob.Path[1], ob.Path[2]);

            Stop();
            return null;

        }


        private List<ColumnModel> ResolveColumns(string tableName, string columnName)
        {
            var _columnName = columnName.CleanName();
            var _tableName = tableName.CleanName();
            var items = this.db.Tables.Where(c => c.Name == _tableName).ToList();
            var cols = items.SelectMany(c => c.Columns).Where(c => c.ColumnName == _columnName).ToList();

            Debug.Assert(cols.Count > 0);

            return cols;

        }

        private ColumnModel ResolveColumn(string schema, string tableName, string columnName)
        {
            var _schema = schema.CleanName();
            var _tableName = tableName.CleanName();
            var _columnName = columnName.CleanName();

            var t = db.Tables[$"{_schema}.{_tableName}"];
            ColumnModel col = t.Columns.FirstOrDefault(c => c.ColumnName == _columnName);

            Debug.Assert(col != null);

            return col;

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

            var t = exception.OffendingToken;
            StringBuilder txt;

            if (exception.Context.Parent != null)
                txt = GetText(exception.Context.Parent);
            else
                txt = GetText(t.StartIndex, t.StopIndex + 1);

            string message = $"'{exception.Message}' at line '{t.Line} {txt.ToString()}' with token '{t.Text}' column {t.Column}, offset {t.StartIndex}";

            var error = new Error() { Exception = exception, Message = message, File = GetFileElement(exception.OffendingToken) };
            _anomalies.Add(error);

            AppendEventParser(GetEventParser(error, string.Empty, SqlKind.Undefined));

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
