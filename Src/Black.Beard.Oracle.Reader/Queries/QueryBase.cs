using Bb.Beard.Oracle.Reader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public abstract class QueryBase
    {

        public List<string> OwnerNames { get; set; }

        public List<string> ProcedureNames { get; set; }

        public List<string> TableNames { get; set; }

        public DbContextOracle OracleContext { get; set; }


        public static List<DbParameter> DbParams = new List<DbParameter>();


        static QueryBase()
        {
            
        }

        public QueryBase()
        {

        }

        /// <summary>
        /// string represente la condition Where
        /// </summary>
        /// <returns></returns>
        public string ProcQueryWhereCondition
        {
            get
            {

                string param1 = OwnerNames == null ? null : "'" + string.Join("','", OwnerNames) + "'";
                string param3 = ProcedureNames == null ? null : "'" + string.Join("','", ProcedureNames) + "'";

                DbParams.Clear();
                string s = string.Empty;
                List<string> tags = new List<string> { " WHERE ", " AND ", " AND " };

                List<string> conditions = new List<string>();
                if (OwnerNames != null && OwnerNames.Any())
                {
                    DbParams.Add(OracleContext.Manager.CreateParameter(param1, ParameterDirection.Input, "'" + string.Join("','", OwnerNames) + "'"));
                    conditions.Add(string.Format("t.OWNER IN (" + param1 + ")"));
                }
                if (ProcedureNames != null && ProcedureNames.Any())
                {
                    DbParams.Add(OracleContext.Manager.CreateParameter(param3, ParameterDirection.Input, "'" + string.Join("','", ProcedureNames) + "'"));
                    conditions.Add(string.Format("t.OBJECT_NAME IN (" + param3 + ")"));
                }

                // additionner toutes les conditions dans la requete
                // 
                int i = 0;
                foreach (string cond in conditions)
                {
                    s += (tags[i++] + cond);
                }

                return s;
            }
        }


        /// <summary>
        /// Gets the table query where condition.
        /// </summary>
        /// <value>
        /// The table query where condition.
        /// </value>
        public string TableQueryWhereCondition(string alias = "t", string ownerFilter = "OWNER", string aliasObjectName = "")
        {
            string s = GetQueryCondition(alias, ownerFilter, aliasObjectName);
            return string.IsNullOrWhiteSpace(s) ? string.Empty : " WHERE" + s;
        }

        /// <summary>
        /// Gets the table query and condition.
        /// </summary>
        /// <value>
        /// The table query and condition.
        /// </value>
        public string TableQueryAndCondition(string alias = "t", string ownerFilter = "OWNER", string aliasObjectName = "")
        {
            string s = GetQueryCondition(alias, ownerFilter, aliasObjectName);
            return string.IsNullOrWhiteSpace(s) ? string.Empty : " AND" + s;
        }


        /// <summary>
        /// Gets the query condition.
        /// </summary>
        /// <returns></returns>
        public string GetQueryCondition(string alias, string ownerFilter, string aliasObjectName)
        {

            //const string param1 = ":ownername";
            //const string param2 = ":tablename";
            string param1 = OwnerNames == null ? null : "'" + string.Join("','", OwnerNames) + "'";
            string param2 = TableNames == null ? null : "'" + string.Join("','", TableNames) + "'";
            string param3 = ProcedureNames == null ? null : "'" + string.Join("','", ProcedureNames) + "'";


            DbParams.Clear();
            string s = string.Empty;
            List<string> tags = new List<string> { " ", " AND ", " AND " };

            List<string> conditions = new List<string>();
            if (OwnerNames != null && OwnerNames.Any())
            {
                DbParams.Add(OracleContext.Manager.CreateParameter(param1, ParameterDirection.Input, "'" + string.Join("','", OwnerNames) + "'"));
                conditions.Add(string.Format(alias + "." + ownerFilter + " IN (" + param1 + ")"));
            }
            if (TableNames != null && TableNames.Any())
            {
                DbParams.Add(OracleContext.Manager.CreateParameter(param2, ParameterDirection.Input, "'" + string.Join("','", TableNames) + "'"));
                conditions.Add(alias + ".TABLE_NAME IN (" + param2 + ")");
            }
            if (ProcedureNames != null && ProcedureNames.Any())
            {
                DbParams.Add(OracleContext.Manager.CreateParameter(param3, ParameterDirection.Input, "'" + string.Join("','", ProcedureNames) + "'"));
                conditions.Add(string.Format(alias + "." + aliasObjectName + " IN (" + param3 + ")"));
            }

            // additionner toutes les conditions dans la requete
            int i = 0;
            foreach (string cond in conditions)
                s += (tags[i++] + cond);

            return s;

        }

        protected static string In(params string[] items)
        {

            return items == null
                ? null
                : "'" + string.Join("','", items) + "'";

        }

    }

    public abstract class QueryBase<T, TDbContext> : QueryBase
        where TDbContext : DbContextBase
    {

        public abstract List<T> Resolve(TDbContext context, Action<T> action);

    }

}
