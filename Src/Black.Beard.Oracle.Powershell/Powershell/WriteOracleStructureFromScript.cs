using Bb.Oracle;
using Bb.Oracle.Models;
using Bb.Oracle.Solutions;
using Bb.Oracle.Structures.Models;
using Bb.Oracle.Visitors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Black.Beard.Oracle.Powershell
{

    [Cmdlet(VerbsCommunications.Write, "OracleModelFromScript")]
    public class WriteOracleStructureFromScript : Cmdlet
    {

        [Parameter(Mandatory = true, HelpMessage = "specify the script path folder")]
        public string SourcePath { get; set; }

        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true, HelpMessage = "specify the name of the database structure")]
        public string OutputFilename { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "specify a custom name of the structure")]
        public string Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "specify a custom name of the structure")]
        public string searchPattern { get; set; }

        protected override void ProcessRecord()
        {

            LogInitializer.Initialize();

            string path = SourcePath;

            SolutionFolder sln = new SolutionFolder(new ScriptParserContext(path, searchPattern));

            OracleDatabase db = new OracleDatabase()
            {
                SourceScript = true,
                Name = Name,
            };

            var visitor = new ConvertScriptToModelVisitor(db);

            sln.Visit(visitor);

            FileInfo file = new FileInfo(OutputFilename);
            if (!file.Directory.Exists)
                file.Directory.Create();

            db.WriteFile(file.FullName);

            base.WriteObject(db);

            base.ProcessRecord();

        }

    }

}
