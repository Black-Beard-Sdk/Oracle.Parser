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

        private ScriptParser()
        {

        }

        public static ScriptParser ParseString(string source, string sourceFile = "")
        {

            var txt = ContentHelper.ToUpper(new StringBuilder(source)).ToString();
            ICharStream stream = CharStreams.fromstring(txt);

            var parser = new ScriptParser() { File = sourceFile ?? string.Empty };
            parser.ParseCharStream(stream);
            return parser;

        }

        public static ScriptParser ParsePath(string source)
        {

            var payload = LoadContent(source);
            ICharStream stream = CharStreams.fromstring(payload.ToString());

            var parser = new ScriptParser() { File = source };
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

        public PlSqlParser.Sql_scriptContext Tree { get { return this.context; } }

        public string File { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Visit<Result>(IParseTreeVisitor<Result> visitor)
        {

            if (visitor is IFile f)
                f.Filename = this.File;

            visitor.Visit(this.context);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ParseCharStream(ICharStream stream)
        {

            //try
            //{

            var lexer = new PlSqlLexer(stream);
            var token = new CommonTokenStream(lexer);
            var parser = new PlSqlParser(token)
            {
                BuildParseTree = true,
                Trace = ScriptParser.Trace,
            };

            context = parser.sql_script();

            //}
            //catch (Exception e)
            //{
            //    if (System.Diagnostics.Debugger.IsAttached)
            //        System.Diagnostics.Debugger.Break();
            //    throw;
            //}

        }

        private PlSqlParser.Sql_scriptContext context;

    }

}
