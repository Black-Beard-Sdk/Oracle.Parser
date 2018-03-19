using Bb.Oracle.Models;
using Bb.Oracle.Reader.Queries;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Reader
{

    public static class ContentHelper
    {


        #region Content From Proc

        public static void BuildContentFromProc(this DbContextOracle connection, StringBuilder sb, List<string> schemas, List<string> names)
        {

            var sources = connection.GetCodeFromProc(schemas, names);

            if (sources.Count == 0)
            {

            }
            else if (sources.Count == 1)
            {
                var item = sources.FirstOrDefault();
                sb.Append("CREATE OR REPLACE ");
                sb.Append(Utils.Unserialize(item.Code, true));
            }
            else
            {

                for (int i = 0; i < sources.Count; i++)
                {
                    var item = sources[i];
                    sb.Append("CREATE OR REPLACE ");
                    var t = Utils.Unserialize(item.Code, true);
                    t = t.Trim()
                         .Trim('/')
                         .Trim();
                    sb.Append(t);
                    sb.AppendLine(String.Empty);
                    sb.AppendLine("/");
                }

                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();

            }

        }

        public static void BuildContentFromPackage(this DbContextOracle connection, StringBuilder sb, List<string> schemas, List<string> names, bool isBody)
        {

            var sources = connection.GetCodeFromProc(schemas, names);

            if (isBody)
            {
                foreach (var item in sources)
                {

                    var t = Utils.Unserialize(item.Code, true);
                    var t2 = Bb.Oracle.Helpers.ContentHelper.FormatSource(t);

                    sb.Append("CREATE OR REPLACE ");


                }
            }
            else
            {

            }

        }

        private static List<ContentCodeQuery.CodeSource> GetCodeFromProc(this DbContextOracle connection, List<string> schemas, List<string> names)
        {

            var db = new OracleDatabase();
            connection.database = db;
            ContentCodeQuery q1 = new ContentCodeQuery()
            {
                OwnerNames = schemas,
                ProcedureNames = names,
            };
            q1.Resolve(connection, null);

            return q1.Sources;

        }

        public static void BuildContentFromTypes(this DbContextOracle connection, StringBuilder sb, List<string> schemas, List<string> names)
        {
            BuildContentFromProc(connection, sb, schemas, names);
        }
        public static void BuildContentFromTrigger(this DbContextOracle connection, StringBuilder sb, List<string> schemas, List<string> names)
        {
            BuildContentFromProc(connection, sb, schemas, names);
        }

        #endregion


    }

}

