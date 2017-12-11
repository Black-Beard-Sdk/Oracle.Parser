using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
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

            ICharStream stream = CharStreams.fromstring(source);

            var parser = new ScriptParser() { File = sourceFile ?? string.Empty };
            parser.ParseCharStream(stream);
            return parser;

        }

        public static ScriptParser ParsePath(string source)
        {

            ICharStream stream = CharStreams.fromPath(source);

            var parser = new ScriptParser() { File = source };
            parser.ParseCharStream(stream);
            return parser;

        }

        public static ScriptParser ParseStream(Stream source, string sourceFile = "")
        {

            ICharStream stream = CharStreams.fromStream(source);

            var parser = new ScriptParser() { File = sourceFile ?? string.Empty };
            parser.ParseCharStream(stream);
            return parser;

        }

        public static ScriptParser ParseTextReader(TextReader source, string sourceFile = "")
        {

            ICharStream stream = CharStreams.fromTextReader(source);

            var parser = new ScriptParser() { File = sourceFile ?? string.Empty };
            parser.ParseCharStream(stream);
            return parser;

        }

        
        public static bool Trace { get; set; }

        public PlSqlParser.Sql_scriptContext Tree { get { return this.context; } }

        public string File { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Visit<Result>(IParseTreeVisitor<Result> visitor)
        {

            if (visitor is IFile f)
                f.File = this.File;

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
