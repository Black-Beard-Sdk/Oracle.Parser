using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class PartitionColumnQuery : DbQueryBase<PartitionColumnDto>
    {

        string sql =
@"


SELECT * FROM SYS.DBA_PART_KEY_COLUMNS l

{0}

ORDER BY l.COLUMN_POSITION    

";

        public override List<PartitionColumnDto> Resolve(DbContextOracle context, Action<PartitionColumnDto>  action)
        {

            List<PartitionColumnDto> List = new List<PartitionColumnDto>();
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
                    else if(t.ObjectType == "INDEX")
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

            PartitionColumnQueryDescriptor PartitionColumn = new PartitionColumnQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = PartitionColumn.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class PartitionColumnQueryDescriptor : StructureDescriptorTable<PartitionColumnDto>
    {

        public PartitionColumnQueryDescriptor(string connectionString)
            : base(() => new PartitionColumnDto(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)PartitionColumnQueryColumns.OWNER) };
            public static Field<String> Name = new Field<String>() { ColumnName = "NAME", Read = reader => reader.Field<String>((int)PartitionColumnQueryColumns.NAME) };
            public static Field<String> ObjectType = new Field<String>() { ColumnName = "OBJECT_TYPE", Read = reader => reader.Field<String>((int)PartitionColumnQueryColumns.OBJECT_TYPE) };
            public static Field<String> ColumnName = new Field<String>() { ColumnName = "COLUMN_NAME", Read = reader => reader.Field<String>((int)PartitionColumnQueryColumns.COLUMN_NAME) };
            public static Field<Decimal> ColumnPosition = new Field<Decimal>() { ColumnName = "COLUMN_POSITION", Read = reader => reader.Field<Decimal>((int)PartitionColumnQueryColumns.COLUMN_POSITION) };

            
        }

        #region Readers

        public override void Read(IDataReader r, PartitionColumnDto item)
        {
            item.Owner = PartitionColumnQueryDescriptor.Columns.Owner.Read(r);
            item.Name = PartitionColumnQueryDescriptor.Columns.Name.Read(r);
            item.ObjectType = PartitionColumnQueryDescriptor.Columns.ObjectType.Read(r);
            item.ColumnName = PartitionColumnQueryDescriptor.Columns.ColumnName.Read(r);
            item.ColumnPosition = PartitionColumnQueryDescriptor.Columns.ColumnPosition.Read(r);

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

        public override DataTable GetDataTable(string tableName, IEnumerable<PartitionColumnDto> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum PartitionColumnQueryColumns
    {
              OWNER,
      NAME,
      OBJECT_TYPE,
      COLUMN_NAME,
      COLUMN_POSITION,

    }

    public class PartitionColumnDto
    {

        public String Owner { get; set; }
        public String Name { get; set; }
        public String ObjectType { get; set; }
        public String ColumnName { get; set; }
        public Decimal ColumnPosition { get; set; }


    }

}

