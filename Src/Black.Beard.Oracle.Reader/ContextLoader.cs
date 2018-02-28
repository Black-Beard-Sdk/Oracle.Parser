using Bb.Oracle.Reader.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Reader
{

    public static class ContextLoader
    {


        private static string Sql1 = @"SELECT u.USERNAME from DBA_USERS u {userFilter}";

        public static string GetOwners(string connectionString, ArgumentContext ctx)
        {

            if (!string.IsNullOrEmpty(ctx.ExcludedSchemas))
                foreach (var item in ctx.ExcludedSchemas.Split(';').Where(c => !string.IsNullOrEmpty(c)))
                    ContextLoader.excluded.Add(item.ToUpper());

            StringBuilder sb = new StringBuilder();
            LoadSchemas(sb, connectionString, Sql1.Replace("{userFilter}", System.Configuration.ConfigurationManager.AppSettings["userFilter"]));

            string result = sb.ToString();
            result = result.Trim(';');
            return result;

        }

        private static void LoadSchemas(StringBuilder sb, string connectionString, string sql)
        {

            sb.Append("PUBLIC;");
            var manager = new OracleManager(connectionString);
            using (var reader = manager.ExecuteReader(System.Data.CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    string schemaName = reader.GetString(0);
                    if (!excluded.Contains(schemaName))
                    {
                        sb.Append(schemaName);
                        sb.Append(";");
                    }
                }
            }

        }

        public static HashSet<string> excluded = new HashSet<string>();
        //{   "MDSYS", "SYS", "XS$NULL", "ANONYMOUS", "APPQOSSYS", "AURORA$ORB$UNAUTHENTICATED", "CSMIG",
        //    "CTXSYS", "DBSNMP", "DIP", "DMSYS", "DSSYS", "EXFSYS", "LBACSYS", "MGMT_VIEW", "ORACLE_OCM",
        //    "ORDDATA", "ORDPLUGINS", "ORDSYS", "PERFSTAT", "OWBSYS_AUDIT", "SYSMAN", "SYSTEM", "TOAD",
        //    "TRACESVR", "TSMSYS", "WKPROXY", "WKSYS", "WK_TEST", "WMSYS", "XDB", 
        //};

    }
}
