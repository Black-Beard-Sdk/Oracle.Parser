using Pssa.Sdk.DataAccess.Dao;
using Pssa.Sdk.DataAccess.Dao.Contracts;
using Pssa.Tools.Databases.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Pssa.Tools.Databases.Generators.Queries.Oracle
{

    public class SubpartitionColumnQuery : DbQueryBase<SubpartitionColumnDto>
    {

        string sql =
@"

SELECT * FROM SYS.DBA_SUBPART_KEY_COLUMNS l

{0}

ORDER BY l.COLUMN_POSITION

";

        public override List<SubpartitionColumnDto> Resolve(DbContextOracle context, Action<SubpartitionColumnDto> action)
        {

            List<SubpartitionColumnDto> List = new List<SubpartitionColumnDto>();
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
                            if (db.ResolveTable(k, out table))
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

            SubpartitionColumnQueryDescriptor SubpartitionColumn = new SubpartitionColumnQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = SubpartitionColumn.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class SubpartitionColumnQueryDescriptor : StructureDescriptorTable<SubpartitionColumnDto>
    {

        public SubpartitionColumnQueryDescriptor(string connectionString)
            : base(() => new SubpartitionColumnDto(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)SubpartitionColumnQueryColumns.OWNER) };
            public static Field<String> Name = new Field<String>() { ColumnName = "NAME", Read = reader => reader.Field<String>((int)SubpartitionColumnQueryColumns.NAME) };
            public static Field<String> ObjectType = new Field<String>() { ColumnName = "OBJECT_TYPE", Read = reader => reader.Field<String>((int)SubpartitionColumnQueryColumns.OBJECT_TYPE) };
            public static Field<String> ColumnName = new Field<String>() { ColumnName = "COLUMN_NAME", Read = reader => reader.Field<String>((int)SubpartitionColumnQueryColumns.COLUMN_NAME) };
            public static Field<Decimal> ColumnPosition = new Field<Decimal>() { ColumnName = "COLUMN_POSITION", Read = reader => reader.Field<Decimal>((int)SubpartitionColumnQueryColumns.COLUMN_POSITION) };


        }

        #region Readers

        public override void Read(IDataReader r, SubpartitionColumnDto item)
        {
            item.Owner = SubpartitionColumnQueryDescriptor.Columns.Owner.Read(r);
            item.Name = SubpartitionColumnQueryDescriptor.Columns.Name.Read(r);
            item.ObjectType = SubpartitionColumnQueryDescriptor.Columns.ObjectType.Read(r);
            item.ColumnName = SubpartitionColumnQueryDescriptor.Columns.ColumnName.Read(r);
            item.ColumnPosition = SubpartitionColumnQueryDescriptor.Columns.ColumnPosition.Read(r);

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

        public override DataTable GetDataTable(string tableName, IEnumerable<SubpartitionColumnDto> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum SubpartitionColumnQueryColumns
    {
        OWNER,
        NAME,
        OBJECT_TYPE,
        COLUMN_NAME,
        COLUMN_POSITION,

    }

    public class SubpartitionColumnDto
    {

        public String Owner { get; set; }
        public String Name { get; set; }
        public String ObjectType { get; set; }
        public String ColumnName { get; set; }
        public Decimal ColumnPosition { get; set; }


    }

}

