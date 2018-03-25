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

    public class PartitionColumnQuery_11 : DbQueryBase<PartitionColumnDto_11>
    {

        string sql =
@"


SELECT * FROM SYS.DBA_PART_KEY_COLUMNS l

{0}

ORDER BY l.COLUMN_POSITION    

";

        public override List<PartitionColumnDto_11> Resolve(DbContextOracle context, Action<PartitionColumnDto_11> action)
        {

            List<PartitionColumnDto_11> List = new List<PartitionColumnDto_11>();
            this.OracleContext = context;
            var db = context.database;

            if (action == null)
                action =
                t =>
                {

                    if (t.Name.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.Table))
                        return;

                    string k = t.Owner + "." + t.Name;

                    var p = new PartitionColumnModel()
                    {
                        ColumnName = t.ColumnName,
                        ColumnPosition = t.ColumnPosition.ToInt16(),
                    };

                    if (t.ObjectType == "TABLE")
                    {
                        TableModel table;
                        if (db.Tables.TryGet(k, out table))
                            table.BlocPartition.Columns.Add(p);
                    }
                    else if (t.ObjectType == "INDEX")
                    {
                        IndexModel index;
                        if (db.ResolveIndex(k, out index))
                            index.BlocPartition.Columns.Add(p);
                    }
                    else
                    {
                        System.Diagnostics.Debugger.Break();
                    }



                };

            PartitionColumnQueryDescriptor_11 PartitionColumn = new PartitionColumnQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = PartitionColumn.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class PartitionColumnQueryDescriptor_11 : StructureDescriptorTable<PartitionColumnDto_11>
    {

        public PartitionColumnQueryDescriptor_11(string connectionString)
            : base(() => new PartitionColumnDto_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)PartitionColumnQueryColumns_11.OWNER) };
            public static Field<String> Name = new Field<String>() { ColumnName = "NAME", Read = reader => reader.Field<String>((int)PartitionColumnQueryColumns_11.NAME) };
            public static Field<String> ObjectType = new Field<String>() { ColumnName = "OBJECT_TYPE", Read = reader => reader.Field<String>((int)PartitionColumnQueryColumns_11.OBJECT_TYPE) };
            public static Field<String> ColumnName = new Field<String>() { ColumnName = "COLUMN_NAME", Read = reader => reader.Field<String>((int)PartitionColumnQueryColumns_11.COLUMN_NAME) };
            public static Field<Decimal> ColumnPosition = new Field<Decimal>() { ColumnName = "COLUMN_POSITION", Read = reader => reader.Field<Decimal>((int)PartitionColumnQueryColumns_11.COLUMN_POSITION) };


        }

        #region Readers

        public override void Read(IDataReader r, PartitionColumnDto_11 item)
        {
            item.Owner = PartitionColumnQueryDescriptor_11.Columns.Owner.Read(r);
            item.Name = PartitionColumnQueryDescriptor_11.Columns.Name.Read(r);
            item.ObjectType = PartitionColumnQueryDescriptor_11.Columns.ObjectType.Read(r);
            item.ColumnName = PartitionColumnQueryDescriptor_11.Columns.ColumnName.Read(r);
            item.ColumnPosition = PartitionColumnQueryDescriptor_11.Columns.ColumnPosition.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.Name;
            yield return Columns.ObjectType;
            yield return Columns.ColumnName;
            yield return Columns.ColumnPosition;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<PartitionColumnDto_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum PartitionColumnQueryColumns_11
    {
        OWNER,
        NAME,
        OBJECT_TYPE,
        COLUMN_NAME,
        COLUMN_POSITION,

    }

    public class PartitionColumnDto_11
    {

        public String Owner { get; set; }
        public String Name { get; set; }
        public String ObjectType { get; set; }
        public String ColumnName { get; set; }
        public Decimal ColumnPosition { get; set; }


    }

}

