using Bb.Oracle.Models;
using Bb.Oracle.Validators;
using Bb.Oracle.Visitors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Solutions
{

    public class SolutionFolder : ScriptParsers
    {

        public SolutionFolder(ScriptParserContext ctx) : base(ctx)
        {

        }

        /// <summary>
        /// Evaluate all script file in the visitor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="visitor"></param>
        public override void Visit<T>(Antlr4.Runtime.Tree.IParseTreeVisitor<T> visitor)
        {

            var scripts = GetFiles(this._Current_context.SearhPattern, this._Current_context.FilePropertyResolver);

            // Process(KindModelEnum.Table, scripts, visitor);
            // Process(KindModelEnum.MaterializedView, scripts, visitor);
            // Process(KindModelEnum.MaterializedViewLog, scripts, visitor);
            // Process(KindModelEnum.View, scripts, visitor);
            // Process(KindModelEnum.Index, scripts, visitor);
            Process(KindModelEnum.Sequence, scripts, visitor);
            // Process(KindModelEnum.Trigger, scripts, visitor);
            // Process(KindModelEnum.Procedure, scripts, visitor);
            Process(KindModelEnum.Package, scripts, visitor);
            // Process(KindModelEnum.PackageBodies, scripts, visitor);
            // Process(KindModelEnum.Function, scripts, visitor);
            // Process(KindModelEnum.Type, scripts, visitor);
            // Process(KindModelEnum.Jobs, scripts, visitor);
            //Process(KindModelEnum.Synonym, scripts, visitor);

            Process(KindModelEnum.UserObjectPrivilege, scripts, visitor);


            foreach (ScriptFileInfo script in scripts)
                script.Finally();

        }

        private void Process<T>(KindModelEnum kind, List<ScriptFileInfo> scripts, IParseTreeVisitor<T> visitor)
        {
            Trace.WriteLine(string.Format("Start processing {0}", kind), "Debug");
            int count = Process(script => script.ExpectedKind == kind, scripts, visitor);
            Trace.WriteLine(string.Format("end processing {0}. {1} script(s).", kind, count), "Debug");
        }

        public string FolderPath { get => this._Current_context.FolderPath; }

        private List<ScriptFileInfo> GetFiles(string searchPattern, IFilePropertyResolver resolver)
        {
            this._Current_context.Directory.Refresh();
            var files = this._Current_context.Directory.GetFiles(searchPattern, System.IO.SearchOption.AllDirectories).Select(c => new ScriptFileInfo(resolver, c)).ToList();
            return files;
        }


    }


}
