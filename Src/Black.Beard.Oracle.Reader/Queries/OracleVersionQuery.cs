using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Reader.Queries
{

    public class OracleVersionQuery
    {

        public OracleVersionQuery()
        {

        }

        public Version GetVersion(DbContextOracle context)
        {

            string sql = "select BANNER from v$version WHERE BANNER LIKE 'CORE%'";

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {

                while (reader.Read())
                {
                    var str = reader.GetString(0)?.Substring(5).Split('\t')[0];
                    var o = str.Split('.').Select(c => int.Parse(c)).ToArray();
                    var v = new Version(o[0], o[1], o[2], o[3]);
                    return v;
                }

            }

            return null;

        }


    }
}


// 
// select instance from v$thread;