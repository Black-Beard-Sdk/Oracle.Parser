using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class ViewQuery : DbQueryBase<ViewQueryTable>
    {

        string sql =
@"
select t.OWNER, t.OBJECT_NAME, t.OBJECT_TYPE
from DBA_OBJECTS t
{0} 
AND t.OBJECT_TYPE IN ('MATERIALIZED VIEW', 'VIEW')
";

        public override List<ViewQueryTable> Resolve(DbContextOracle context, Action<ViewQueryTable> action)
        {
            List<ViewQueryTable> List = new List<ViewQueryTable>();
            var db = context.database;
            this.OracleContext = context;

            if (action == null)
                action =
                    t =>
                    {

                        if (t.ObjectName.ExcludIfStartwith(t.SchemaName, Models.Configurations.ExcludeKindEnum.View))
                            return;

                        string key = t.SchemaName + "." + t.ObjectName;

                        TableModel table;

                        if (db.Tables.TryGet(key, out table))
                        {
                            table.IsView = true;
                        }
                    };


            ViewQueryDescriptor view = new ViewQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition());

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class ViewQueryDescriptor : StructureDescriptorTable<ViewQueryTable>
    {

        public ViewQueryDescriptor(string connectionString)
            : base(() => new ViewQueryTable(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)ViewQueryColumns.OWNER)
            };

            public static Field<string> ObjectName = new Field<string>()
            {
                ColumnName = "OBJECT_NAME",
                Read = reader => reader.Field<string>((int)ViewQueryColumns.OBJECT_NAME)
            };

            public static Field<string> ObjectType = new Field<string>()
            {
                ColumnName = "OBJECT_TYPE",
                Read = reader => reader.Field<string>((int)ViewQueryColumns.OBJECT_TYPE)
            };

        }

        #region Readers

        public override void Read(IDataReader r, ViewQueryTable item)
        {
            item.SchemaName = ViewQueryDescriptor.Columns.Owner.Read(r);
            item.ObjectName = ViewQueryDescriptor.Columns.ObjectName.Read(r);
            item.ObjectType = ViewQueryDescriptor.Columns.ObjectType.Read(r);
        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.ObjectName;
            yield return Columns.ObjectType;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ViewQueryTable> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum ViewQueryColumns
    {
        OWNER,
        OBJECT_NAME,
        OBJECT_TYPE,
    }

    public class ViewQueryTable
    {
        public string SchemaName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
    }

}
