using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Solutions
{

    public class SolutionFolder : ISolutionEvaluator
    {

        public SolutionFolder(ScriptParserContext ctx)
        {

            this._ctx = ctx;

        }

        /// <summary>
        /// Evaluate all script file in the visitor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="visitor"></param>
        public void Visit<T>(Antlr4.Runtime.Tree.IParseTreeVisitor<T> visitor)
        {

            List<EventParser> events = new List<EventParser>();

            var scripts = GetFiles(this._ctx.SearhPattern, this._ctx.FilePropertyResolver);
      
            int scriptInError = 0;
            //scriptInError += Process(SqlKind.Table, scripts, visitor);
            //scriptInError += Process(SqlKind.MaterializedView, scripts, visitor);
            //scriptInError += Process(SqlKind.MaterializedViewLog, scripts, visitor);
            //scriptInError += Process(SqlKind.View, scripts, visitor);
            //scriptInError += Process(SqlKind.Index, scripts, visitor);
            //scriptInError += Process(SqlKind.Sequence, scripts, visitor);
            //scriptInError += Process(SqlKind.Trigger, scripts, visitor);
            //scriptInError += Process(SqlKind.Procedure, scripts, visitor);
            //scriptInError += Process(SqlKind.Package, scripts, visitor);
            //scriptInError += Process(SqlKind.PackageBodies, scripts, visitor);
            //scriptInError += Process(SqlKind.Function, scripts, visitor);
            //scriptInError += Process(SqlKind.Type, scripts, visitor);
            //scriptInError += Process(SqlKind.Jobs, scripts, visitor);
            //scriptInError += Process(SqlKind.Synonym, scripts, visitor);
            scriptInError += Process(SqlKind.UserObjectPrivilege, scripts, visitor);

        }

        private int Process<T>(SqlKind kind, List<ScriptFileInfo> scripts, Antlr4.Runtime.Tree.IParseTreeVisitor<T> visitor)
        {

            int count = 0;
            //log.WriteLine(string.Empty);
            //log.WriteLine("Start processing {0}", kind);
            int scriptInError = 0;
            int cut = this._ctx._cut;

            foreach (ScriptFileInfo script in scripts)
                if (script.ExpectedKind == kind)
                {
                    count++;
                    script.Visit<T>(cut, visitor);
                    //var o = RunOne(kind, outputTarget, script, cut, events);
                    //scriptInError += o;
                }

            foreach (ScriptFileInfo script in scripts)
                script.Finally();

            //outputTarget.Initialize();

            //log.WriteLine("{0} processed for {1}", count, kind);
            //log.WriteLine(string.Empty);

            return scriptInError;

        }

        public string FolderPath { get => this._ctx.FolderPath; }

        private List<ScriptFileInfo> GetFiles(string searchPattern, IFilePropertyResolver resolver)
        {
            this._ctx.Directory.Refresh();
            var files = this._ctx.Directory.GetFiles(searchPattern, System.IO.SearchOption.AllDirectories).Select(c => new ScriptFileInfo(resolver, c)).ToList();
            return files;
        }

        private readonly ScriptParserContext _ctx;

    }


}
