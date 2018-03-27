using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Reader.Rules
{

    public abstract class RuleBase
    {

        public void Process(DbContextOracle dbContext)
        {

            Process_Impl(dbContext.Database);

        }

        protected abstract void Process_Impl(OracleDatabase db);

        protected virtual string Log { get => GetType().Name; }

    }

    public class RebuildMethodKeys : RuleBase
    {

        protected override void Process_Impl(OracleDatabase db)
        {

            var procs = db.Procedures.ToList();
            foreach (ProcedureModel procedure in procs)
            {
                var k = procedure.BuildKey();
                if (k != procedure.Key)
                {
                    db.Procedures.Remove(procedure);
                    procedure.Key = k;
                    db.Procedures.Add(procedure);
                }
            }

        }

        protected override string Log => Resources.RebuildMethodKeysLog;

    }

}
