using Bb.Oracle;
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

    [Cmdlet(VerbsCommunications.Write, "OracleModelFromServer")]
    public class WriteOracleStructureFromServer : Cmdlet
    {


        [Parameter(Mandatory = true, HelpMessage = "specify the source name of database structure")]
        public string Source { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "specify the username for access to database structure")]
        public string Username { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "specify the Password for access to database structure")]
        public string Password { get; set; }



        [Parameter(Mandatory = false, HelpMessage = "Owner filter")]
        public string OwnerFilter { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Excluded schemas filter")]
        public string ExcludedSchemas { get; set; }
        [Parameter(Mandatory = false, HelpMessage = "specify if the structure must to contains code like procedure, triger, ...")]
        public bool ExcludeCode { get; set; }



        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true, HelpMessage = "specify the name of the database structure")]
        public string OutputFilename { get; set; }
        
        [Parameter(Mandatory = false, HelpMessage = "specify a custom name of the structure")]
        public string Name { get; set; }


        protected override void ProcessRecord()
        {

            LogInitializer.Initialize();

            var ctx = new Bb.Oracle.Reader.ArgumentContext()
            {
                Login = Username,
                Pwd = Password,
                Source = Source,
                Filename = this.OutputFilename,
                ExcludeCode = ExcludeCode,
                Name = string.IsNullOrEmpty(Name) ? Source : Name,
                ExcludedSchemas = ExcludedSchemas,
                OwnerFilter = this.OwnerFilter
            };

            FileInfo f = new FileInfo(this.OutputFilename);
            if (!f.Directory.Exists)
                f.Directory.Create();

            OracleDatabase db = Database.GenerateFile(ctx);

            base.WriteObject(db);

            base.ProcessRecord();

        }

    }

}
