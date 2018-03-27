using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Reader.Queries
{

    public class SynonymQuery_11 : DbQueryBase<SynonymTable_11>
    {

        string sql =
@"
WITH obj AS
(
  SELECT o.OBJECT_TYPE, o.OWNER OBJECT_OWNER, o.OBJECT_NAME
  FROM DBA_OBJECTS o
  where o.OBJECT_TYPE IN ('SYNONYM', 'TABLE', 'VIEW', 'SEQUENCE','PROCEDURE', 'FUNCTION', 'PACKAGE', 'MATERIALIZED VIEW'/*, 'JAVA CLASS'*/, 'TYPE')
)
SELECT t.OWNER SYNONYM_OWNER, t.SYNONYM_NAME, o.OBJECT_TYPE, o.OBJECT_OWNER, o.OBJECT_NAME, t.DB_LINK
         
FROM DBA_SYNONYMS t LEFT OUTER JOIN obj o ON (t.TABLE_OWNER = o.OBJECT_OWNER AND t.TABLE_NAME = o.OBJECT_NAME)


{0}

";

        public override List<SynonymTable_11> Resolve(DbContextOracle context, Action<SynonymTable_11> action)
        {

            List<SynonymTable_11> List = new List<SynonymTable_11>();
            var db = context.Database;
            this.OracleContext = context;

            if (action == null)
                action =
                t =>
                {

                    if (!context.Use(t.SYNONYME_OWNER))
                        return;

                    string key = string.Empty;
                    if (!string.IsNullOrEmpty(t.SYNONYME_OWNER))
                        key = $"{t.SYNONYME_OWNER}.";
                    else
                    {

                    }
                    key += $"{t.SYNONYME_NAME}:{t.OBJECT_OWNER}.{t.OBJECT_NAME}:{t.DB_LINK ?? string.Empty}";

                    if (!string.IsNullOrEmpty(t.OBJECT_OWNER))
                        if (!ContextLoader.excluded.Contains(t.OBJECT_OWNER))
                        {
                            var syn = new SynonymModel()
                            {
                                Key = key,
                                //ObjectType = t.OBJECT_TYPE,
                                ObjectTargetName = t.OBJECT_NAME,
                                Name = t.SYNONYME_NAME,
                                ObjectTargetOwner = t.OBJECT_OWNER,
                                Owner = t.SYNONYME_OWNER,
                                IsPublic = t.SYNONYME_OWNER.ToUpper() == "PUBLIC",
                                DbLink = t.DB_LINK ?? string.Empty,
                            };
                            db.Synonyms.Add(syn);
                        }

                };


            SynonymDescriptor_11 view = new SynonymDescriptor_11(context.Manager.ConnectionString);

            var c = (TableQueryAndCondition("o", "OBJECT_OWNER")
                + " OR " + GetQueryCondition("t", "OWNER", "")).Trim().Substring(3);
            sql = string.Format(sql, " WHERE " + c);

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }

        

    }

    /// <summary>
    /// 
    /// </summary>
    public class SynonymDescriptor_11 : StructureDescriptorTable<SynonymTable_11>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SynonymDescriptor_11"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SynonymDescriptor_11(string connectionString)
            : base(() => new SynonymTable_11(), connectionString)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public static class Columns
        {

            public static Field<string> SYNONYME_OWNER = new Field<string>()
            {
                ColumnName = "SYNONYME_OWNER",
                Read = reader => reader.Field<string>((int)SynonymTableColumns_11.SYNONYME_OWNER) ?? string.Empty
            };

            public static Field<string> SYNONYME_NAME = new Field<string>()
            {
                ColumnName = "SYNONYME_NAME",
                Read = reader => reader.Field<string>((int)SynonymTableColumns_11.SYNONYME_NAME) ?? string.Empty
            };

            public static Field<string> OBJECT_TYPE = new Field<string>()
            {
                ColumnName = "OBJECT_TYPE",
                Read = reader => reader.Field<string>((int)SynonymTableColumns_11.OBJECT_TYPE) ?? string.Empty
            };

            public static Field<string> OBJECT_OWNER = new Field<string>()
            {
                ColumnName = "OBJECT_OWNER",
                Read = reader => reader.Field<string>((int)SynonymTableColumns_11.OBJECT_OWNER) ?? string.Empty
            };

            public static Field<string> OBJECT_NAME = new Field<string>()
            {
                ColumnName = "OBJECT_NAME",
                Read = reader => reader.Field<string>((int)SynonymTableColumns_11.OBJECT_NAME) ?? string.Empty
            };

            public static Field<string> DB_LINK = new Field<string>()
            {
                ColumnName = "DB_LINK",
                Read = reader => reader.Field<string>((int)SynonymTableColumns_11.DB_LINK) ?? string.Empty
            };

        }

        #region Readers

        public override void Read(IDataReader r, SynonymTable_11 item)
        {
            item.SYNONYME_OWNER = SynonymDescriptor_11.Columns.SYNONYME_OWNER.Read(r);
            item.SYNONYME_NAME = SynonymDescriptor_11.Columns.SYNONYME_NAME.Read(r);
            item.OBJECT_OWNER = SynonymDescriptor_11.Columns.OBJECT_OWNER.Read(r);
            item.OBJECT_NAME = SynonymDescriptor_11.Columns.OBJECT_NAME.Read(r);
            item.OBJECT_TYPE = SynonymDescriptor_11.Columns.OBJECT_TYPE.Read(r);
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.SYNONYME_OWNER;
            yield return Columns.SYNONYME_NAME;
            yield return Columns.OBJECT_TYPE;
            yield return Columns.OBJECT_OWNER;
            yield return Columns.OBJECT_NAME;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<SynonymTable_11> entities)
        {
            throw new NotImplementedException();
        }

    }

    public enum SynonymTableColumns_11
    {
        SYNONYME_OWNER,
        SYNONYME_NAME,
        OBJECT_TYPE,
        OBJECT_OWNER,
        OBJECT_NAME,
        DB_LINK
    }

    public class SynonymTable_11
    {
        public string SYNONYME_OWNER { get; set; }
        public string SYNONYME_NAME { get; set; }
        public string OBJECT_TYPE { get; set; }
        public string OBJECT_OWNER { get; set; }
        public string OBJECT_NAME { get; set; }
        public string DB_LINK { get; set; }
    }

}
