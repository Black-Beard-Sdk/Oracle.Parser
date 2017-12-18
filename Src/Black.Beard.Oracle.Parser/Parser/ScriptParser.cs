using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Oracle.Helpers;
using Bb.Oracle.Visitors;
using Black.Beard.Oracle.Helpers;
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

            ICharStream stream = CharStreams.fromstring(ToUpper(new StringBuilder(source)).ToString());

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
            StringBuilder result = new StringBuilder(ContentLoader.LoadContent(rootSource));
            StringBuilder _result = ToUpper(result);
            return _result;
        }

        private static StringBuilder ToUpper(StringBuilder result)
        {

            int length = result.Length;

            StringBuilder sb = new StringBuilder(length + 200);
            bool inChar = false;

            char lastChar = ' ';
            for (int i = 0; i < length; i++)
            {

                var c = result[i];

                if (c == '\'' && lastChar != '\\')
                    inChar = !inChar;

                else if (char.IsLower(c) && !inChar)
                    c = char.ToUpper(c);

                lastChar = c;

                sb.Append(c);

            }

            return sb;

        }

        //public static ScriptParser ParseStream(Stream source, string sourceFile = "")
        //{

        //    ICharStream stream = CharStreams.fromStream(source);

        //    var parser = new ScriptParser() { File = sourceFile ?? string.Empty };
        //    parser.ParseCharStream(stream);
        //    return parser;

        //}

        //public static ScriptParser ParseTextReader(TextReader source, string sourceFile = "")
        //{

        //    ICharStream stream = CharStreams.fromTextReader(source);

        //    var parser = new ScriptParser() { File = sourceFile ?? string.Empty };
        //    parser.ParseCharStream(stream);
        //    return parser;

        //}


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
