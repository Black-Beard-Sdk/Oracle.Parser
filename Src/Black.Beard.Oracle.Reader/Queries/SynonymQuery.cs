using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class SynonymQuery : DbQueryBase<SynonymeTable>
    {

        string sql =
@"
WITH obj AS
(
  SELECT o.OBJECT_TYPE, o.OWNER, o.OBJECT_NAME
  FROM DBA_OBJECTS o
  where o.OBJECT_TYPE IN ('SYNONYM', 'TABLE', 'VIEW', 'SEQUENCE','PROCEDURE', 'FUNCTION', 'PACKAGE', 'MATERIALIZED VIEW'/*, 'JAVA CLASS'*/, 'TYPE')
)
SELECT     t.OWNER SYNONYM_OWNER, t.SYNONYM_NAME
         , o.OBJECT_TYPE, o.OWNER OBJECT_OWNER, o.OBJECT_NAME
         
FROM DBA_SYNONYMS t LEFT OUTER JOIN obj o ON (t.TABLE_OWNER = o.OWNER AND t.TABLE_NAME = o.OBJECT_NAME)

{0}

";

        public override List<SynonymeTable> Resolve(DbContextOracle context, Action<SynonymeTable> action)
        {

            List<SynonymeTable> List = new List<SynonymeTable>();
            var db = context.database;
            this.OracleContext = context;

            if (action == null)
                action =
                t =>
                {

                    if (!context.Use(t.SYNONYME_OWNER))
                        return;

                    string synonymeName = string.Empty;
                    if (!string.IsNullOrEmpty(t.SYNONYME_OWNER))
                        synonymeName = t.SYNONYME_OWNER + ".";
                    synonymeName += t.SYNONYME_NAME;

                    if (!string.IsNullOrEmpty(t.OBJECT_OWNER))
                        if (!ContextLoader.excluded.Contains(t.OBJECT_OWNER))
                        {
                            var syn = new SynonymModel()
                            {
                                Key = synonymeName,
                                ObjectType = t.OBJECT_TYPE,
                                ObjectTarget = $"{t.OBJECT_OWNER}.{t.OBJECT_NAME}",
                                Name = t.SYNONYME_NAME,
                                SchemaName = t.OBJECT_OWNER,
                                SynonymOwner = t.SYNONYME_OWNER,
                                IsPublic = t.SYNONYME_OWNER.ToUpper() == "PUBLIC",
                            };
                            db.Synonymes.Add(syn);
                        }

                };


            SynonymeDescriptor view = new SynonymeDescriptor(context.Manager.ConnectionString);

            sql = string.Format(sql, TableQueryAndCondition("o").Replace("AND", "WHERE"));

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
    public class SynonymeDescriptor : StructureDescriptorTable<SynonymeTable>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SynonymeDescriptor"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SynonymeDescriptor(string connectionString)
            : base(() => new SynonymeTable(), connectionString)
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
                Read = reader => reader.Field<string>((int)SynonymeTableColumns.SYNONYME_OWNER) ?? string.Empty
            };

            public static Field<string> SYNONYME_NAME = new Field<string>()
            {
                ColumnName = "SYNONYME_NAME",
                Read = reader => reader.Field<string>((int)SynonymeTableColumns.SYNONYME_NAME) ?? string.Empty
            };

            public static Field<string> OBJECT_TYPE = new Field<string>()
            {
                ColumnName = "OBJECT_TYPE",
                Read = reader => reader.Field<string>((int)SynonymeTableColumns.OBJECT_TYPE) ?? string.Empty
            };

            public static Field<string> OBJECT_OWNER = new Field<string>()
            {
                ColumnName = "OBJECT_OWNER",
                Read = reader => reader.Field<string>((int)SynonymeTableColumns.OBJECT_OWNER) ?? string.Empty
            };

            public static Field<string> OBJECT_NAME = new Field<string>()
            {
                ColumnName = "OBJECT_NAME",
                Read = reader => reader.Field<string>((int)SynonymeTableColumns.OBJECT_NAME) ?? string.Empty
            };

        }

        #region Readers

        public override void Read(IDataReader r, SynonymeTable item)
        {
            item.SYNONYME_OWNER = SynonymeDescriptor.Columns.SYNONYME_OWNER.Read(r);
            item.SYNONYME_NAME = SynonymeDescriptor.Columns.SYNONYME_NAME.Read(r);
            item.OBJECT_OWNER = SynonymeDescriptor.Columns.OBJECT_OWNER.Read(r);
            item.OBJECT_NAME = SynonymeDescriptor.Columns.OBJECT_NAME.Read(r);
            item.OBJECT_TYPE = SynonymeDescriptor.Columns.OBJECT_TYPE.Read(r);
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

        public override DataTable GetDataTable(string tableName, IEnumerable<SynonymeTable> entities)
        {
            throw new NotImplementedException();
        }

    }

    public enum SynonymeTableColumns
    {
        SYNONYME_OWNER,
        SYNONYME_NAME,
        OBJECT_TYPE,
        OBJECT_OWNER,
        OBJECT_NAME

    }

    public class SynonymeTable
    {
        public string SYNONYME_OWNER { get; set; }
        public string SYNONYME_NAME { get; set; }
        public string OBJECT_TYPE { get; set; }
        public string OBJECT_OWNER { get; set; }
        public string OBJECT_NAME { get; set; }
    }

}
