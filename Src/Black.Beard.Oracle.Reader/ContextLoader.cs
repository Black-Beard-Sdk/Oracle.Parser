using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pssa.Tools.Databases.Generators.Queries
{

    public static class ContextLoader
    {


        private static string Sql1 = @"SELECT u.USERNAME from DBA_USERS u WHERE u.USERNAME IN ( select p.GRANTEE USERNAME from DBA_ROLE_PRIVS p where p.granted_role like 'PICKUP_USER' AND USERNAME NOT LIKE 'ZZZ\_%' ESCAPE '\' ) ORDER BY u.USERNAME";
        private static string Sql2 = @"SELECT u.USERNAME from DBA_USERS u ORDER BY u.USERNAME";

        public static string GetOwners(string connectionString)
        {

            StringBuilder sb = new StringBuilder();
            LoadSchemas(sb, connectionString, Sql1);

            if (sb.Length == 0)
                LoadSchemas(sb, connectionString, Sql2);

            string result = sb.ToString();
            result = result.Trim(';');
            return result;
        }

        private static void LoadSchemas(StringBuilder sb, string connectionString, string sql)
        {

            sb.Append("PUBLIC;");
            var manager = new Pssa.Sdk.DataAccess.Dao.Oracle.OracleManager(connectionString);
            using (var reader = manager.ExecuteReader(System.Data.CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    string schemaName = reader.GetString(0);
                    if (!exluded.Contains(schemaName))
                    {
                        sb.Append(schemaName);
                        sb.Append(";");
                    }
                }
            }

        }

        public static HashSet<string> exluded = new HashSet<string>()
        {   "MDSYS", "SYS", "XS$NULL", "ANONYMOUS", "APPQOSSYS", "AURORA$ORB$UNAUTHENTICATED", "CSMIG",
            "CTXSYS", "DBSNMP", "DIP", "DMSYS", "DSSYS", "EXFSYS", "LBACSYS", "MGMT_VIEW", "ORACLE_OCM",
            "ORDDATA", "ORDPLUGINS", "ORDSYS", "PERFSTAT", "OWBSYS_AUDIT", "SYSMAN", "SYSTEM", "TOAD",
            "TRACESVR", "TSMSYS", "WKPROXY", "WKSYS", "WK_TEST", "WMSYS", "XDB", 
        };




    }
}
