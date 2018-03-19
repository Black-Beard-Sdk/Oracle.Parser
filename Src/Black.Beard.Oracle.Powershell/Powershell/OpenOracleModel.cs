using Bb.Oracle.Models;
using Bb.Oracle.Reader;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Black.Beard.Oracle.Powershell
{

    [Cmdlet(VerbsCommon.Open, "OracleModel")]
    public class OpenOracleModel : Cmdlet
    {

        [Parameter(Mandatory = true, HelpMessage = "specify the source filename of database structure file")]
        public string InputFilename { get; set; }

        protected override void ProcessRecord()
        {

            FileInfo f = new FileInfo(this.InputFilename);
            if (!f.Exists)
                throw new FileNotFoundException(f.FullName);

            var db = OracleDatabase.ReadFile(f.FullName);

            base.WriteObject(db);

            base.ProcessRecord();

        }

    }

}
