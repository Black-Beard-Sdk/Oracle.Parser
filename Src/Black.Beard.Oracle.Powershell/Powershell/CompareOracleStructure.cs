using Bb.Oracle.Models;
using Bb.Oracle.Models.Comparer;
using Bb.Oracle.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Black.Beard.Oracle.Powershell
{

    [Cmdlet(VerbsData.Compare, "OracleStructure ")]
    public class CompareOracleStructure : Cmdlet
    {

        [Parameter(Mandatory = true, HelpMessage = "specify the source database structure")]
        public OracleDatabase Source { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "specify the target database structure")]
        public OracleDatabase Target { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "specify output target path")]
        public string Output { get; set; }

        protected override void ProcessRecord()
        {

            var sf = new FileInfo(Output);
            string folderForSource = Path.Combine(sf.Directory.FullName, Path.GetFileNameWithoutExtension(sf.Name), Path.GetFileNameWithoutExtension(Source.Name));
            string folderForTarget = Path.Combine(sf.Directory.FullName, Path.GetFileNameWithoutExtension(sf.Name), Path.GetFileNameWithoutExtension(Target.Name));

            ModelComparer comparer = new ModelComparer();
            DifferenceModels diff = new DifferenceModels(folderForSource, folderForTarget, c => Console.WriteLine(c));
            comparer.CompareModels(Source, Target, diff, new CompareContext() { });

            base.WriteObject(diff);

            base.ProcessRecord();

        }

    }

}
