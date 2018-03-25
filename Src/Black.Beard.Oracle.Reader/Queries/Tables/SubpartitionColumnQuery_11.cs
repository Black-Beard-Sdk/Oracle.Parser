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

    public class SubpartitionColumnQuery_11 : DbQueryBase<SubpartitionColumnDto_11>
    {

        string sql =
@"

SELECT * FROM SYS.DBA_SUBPART_KEY_COLUMNS l

{0}

ORDER BY l.COLUMN_POSITION

";

        public override List<SubpartitionColumnDto_11> Resolve(DbContextOracle context, Action<SubpartitionColumnDto_11> action)
        {

            List<SubpartitionColumnDto_11> List = new List<SubpartitionColumnDto_11>();
            var db = context.database;
            this.OracleContext = context;

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
                                table.BlocPartition.SubColumns.Add(p);
                        }
                        else if (t.ObjectType == "INDEX")
                        {
                            IndexModel index;
                            if (db.ResolveIndex(k, out index))
                                index.BlocPartition.SubColumns.Add(p);
                        }
                        else
                        {
                            System.Diagnostics.Debugger.Break();
                        }



                    };

            SubpartitionColumnQueryDescriptor_11 SubpartitionColumn = new SubpartitionColumnQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = SubpartitionColumn.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class SubpartitionColumnQueryDescriptor_11 : StructureDescriptorTable<SubpartitionColumnDto_11>
    {

        public SubpartitionColumnQueryDescriptor_11(string connectionString)
            : base(() => new SubpartitionColumnDto_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)SubpartitionColumnQueryColumns_11.OWNER) };
            public static Field<String> Name = new Field<String>() { ColumnName = "NAME", Read = reader => reader.Field<String>((int)SubpartitionColumnQueryColumns_11.NAME) };
            public static Field<String> ObjectType = new Field<String>() { ColumnName = "OBJECT_TYPE", Read = reader => reader.Field<String>((int)SubpartitionColumnQueryColumns_11.OBJECT_TYPE) };
            public static Field<String> ColumnName = new Field<String>() { ColumnName = "COLUMN_NAME", Read = reader => reader.Field<String>((int)SubpartitionColumnQueryColumns_11.COLUMN_NAME) };
            public static Field<Decimal> ColumnPosition = new Field<Decimal>() { ColumnName = "COLUMN_POSITION", Read = reader => reader.Field<Decimal>((int)SubpartitionColumnQueryColumns_11.COLUMN_POSITION) };


        }

        #region Readers

        public override void Read(IDataReader r, SubpartitionColumnDto_11 item)
        {
            item.Owner = SubpartitionColumnQueryDescriptor_11.Columns.Owner.Read(r);
            item.Name = SubpartitionColumnQueryDescriptor_11.Columns.Name.Read(r);
            item.ObjectType = SubpartitionColumnQueryDescriptor_11.Columns.ObjectType.Read(r);
            item.ColumnName = SubpartitionColumnQueryDescriptor_11.Columns.ColumnName.Read(r);
            item.ColumnPosition = SubpartitionColumnQueryDescriptor_11.Columns.ColumnPosition.Read(r);

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

        public override DataTable GetDataTable(string tableName, IEnumerable<SubpartitionColumnDto_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum SubpartitionColumnQueryColumns_11
    {
        OWNER,
        NAME,
        OBJECT_TYPE,
        COLUMN_NAME,
        COLUMN_POSITION,

    }

    public class SubpartitionColumnDto_11
    {

        public String Owner { get; set; }
        public String Name { get; set; }
        public String ObjectType { get; set; }
        public String ColumnName { get; set; }
        public Decimal ColumnPosition { get; set; }


    }

}

