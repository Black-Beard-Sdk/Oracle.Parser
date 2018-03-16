using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Oracle.Helpers;
using Bb.Oracle.Visitors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Parser
{

    public class ScriptParser
    {

        private ScriptParser(TextWriter output, TextWriter outputError)
        {

            this.Output = output ?? Console.Out;
            this.OutputError = outputError ?? Console.Error;

        }

        public static ScriptParser ParseString(string source, string sourceFile = "", TextWriter output = null, TextWriter outputError = null)
        {

            var txt = ContentHelper.ToUpper(new StringBuilder(source));
            ICharStream stream = CharStreams.fromstring(txt.ToString());

            var parser = new ScriptParser(output, outputError) { File = sourceFile ?? string.Empty, Content = txt };
            parser.ParseCharStream(stream);
            return parser;

        }

        public static ScriptParser ParsePath(string source, TextWriter output = null, TextWriter outputError = null)
        {

            var payload = LoadContent(source);
            ICharStream stream = CharStreams.fromstring(payload.ToString());

            var parser = new ScriptParser(output, outputError) { File = source, Content = payload };
            parser.ParseCharStream(stream);
            return parser;

        }

        /// <summary>
        /// Loads the content of the file.
        /// </summary>
        /// <param name="rootSource">The root source.</param>
        /// <returns></returns>
        public static StringBuilder LoadContent(string rootSource)
        {
            StringBuilder result = new StringBuilder(ContentHelper.LoadContentFromFile(rootSource));
            StringBuilder _result = ContentHelper.ToUpper(result);
            return _result;
        }

        public static bool Trace { get; set; }

        public PlSqlParser.Sql_scriptContext Tree { get { return this._context; } }

        public string File { get; set; }

        public StringBuilder Content { get; private set; }
        public TextWriter Output { get; private set; }
        public TextWriter OutputError { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Visit<Result>(IParseTreeVisitor<Result> visitor)
        {

            if (visitor is IFile f)
                f.Filename = this.File;

            if (System.Diagnostics.Debugger.IsAttached)
                Console.WriteLine(this.File);

            var context = this._context;
            if (this._parser.ErrorListeners.Count > 0)
            {

            }
            visitor.Visit(context);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ParseCharStream(ICharStream stream)
        {

            //try
            //{

            var lexer = new PlSqlLexer(stream, this.Output, this.OutputError);
            var token = new CommonTokenStream(lexer);
            this._parser = new PlSqlParser(token)
            {
                BuildParseTree = true,
                Trace = ScriptParser.Trace,
            };

            _context = _parser.sql_script();

            //}
            //catch (Exception e)
            //{
            //    if (System.Diagnostics.Debugger.IsAttached)
            //        System.Diagnostics.Debugger.Break();
            //    throw;
            //}

        }

        private PlSqlParser _parser;
        private PlSqlParser.Sql_scriptContext _context;

    }

}
