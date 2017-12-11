using Pssa.Sdk.DataAccess.Dao;
using Pssa.Sdk.DataAccess.Dao.Contracts;
using Pssa.Tools.Databases.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Pssa.Tools.Databases.Generators.Queries.Oracle
{

    public class SequenceQuery : DbQueryBase<SequenceTable>
    {

        //select tabs.table_name,
        //  trigs.trigger_name,
        //  seqs.sequence_name
        //from all_tables tabs
        //join all_triggers trigs
        //  on trigs.table_owner = tabs.owner
        //  and trigs.table_name = tabs.table_name
        //join all_dependencies deps
        //  on deps.owner = trigs.owner
        //  and deps.name = trigs.trigger_name
        //join all_sequences seqs
        //  on seqs.sequence_owner = deps.referenced_owner
        //  and seqs.sequence_name = deps.referenced_name
        //where tabs.TABLE_NAME = 'ADDRESS';



        string sql =
@"
SELECT
  s.sequence_owner AS owner,
  s.sequence_name,
  s.min_value,
  s.max_value,
  s.increment_by,
  s.cycle_flag,
  s.order_flag,
  s.cache_size,
  s.last_number
FROM
  dba_sequences s
{0}
ORDER BY s.sequence_owner, s.sequence_name
";

        public override List<SequenceTable> Resolve(DbContextOracle context, Action<SequenceTable> action)
        {

            List<SequenceTable> List = new List<SequenceTable>();
            this.OracleContext = context;
            var db = context.database;

            if (action == null)
                action =
                    t =>
                    {
                        var name = t.Owner + "." + t.Sequence_name;
                        SequenceModel s;

                        if (db.ResolveSequence(name, out s))
                        {
                            s.MinValue = t.Min_value;
                            s.MaxValue = t.Max_value;
                            s.IncrementBy = t.Increment_by;
                            s.CycleFlag = t.Cycle_flag;
                            s.OrderFlag = t.Order_flag;
                            s.CacheSize = t.Cache_size;
                            s.LastNumber = t.Last_number;
                            s.MaxValueIsSpecified = true;
                        }
                        else
                        {
                            s = new SequenceModel()
                            {
                                Name = name,
                                Owner = t.Owner,
                                SequenceName = t.Sequence_name,
                                MinValue = t.Min_value,
                                MaxValue = t.Max_value,
                                IncrementBy = t.Increment_by,
                                CycleFlag = t.Cycle_flag,
                                OrderFlag = t.Order_flag,
                                CacheSize = t.Cache_size,
                                LastNumber = t.Last_number,
                                MaxValueIsSpecified = true

                            };

                            db.Sequences.Add(s);
                            db.Add(s);
                        }

                    };


            SequenceDescriptor view = new SequenceDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("s", "sequence_owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class SequenceDescriptor : StructureDescriptorTable<SequenceTable>
    {

        public SequenceDescriptor(string connectionString)
            : base(() => new SequenceTable(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)SequenceTableColumns.Owner)
            };

            public static Field<string> Sequence_name = new Field<string>()
            {
                ColumnName = "SEQUENCE_NAME",
                Read = reader => reader.Field<string>((int)SequenceTableColumns.Sequence_name)
            };

            public static Field<long> Min_value = new Field<long>()
            {
                ColumnName = "MIN_VALUE",
                Read = reader => reader.Field<long>((int)SequenceTableColumns.Min_value)
            };

            public static Field<decimal> Max_value = new Field<decimal>()
            {
                ColumnName = "MAX_VALUE",
                Read = reader => reader.Field<decimal>((int)SequenceTableColumns.Max_value)
            };

            public static Field<int> Increment_by = new Field<int>()
            {
                ColumnName = "Increment_by",
                Read = reader => reader.Field<int>((int)SequenceTableColumns.Increment_by)
            };

            public static Field<string> Cycle_flag = new Field<string>()
            {
                ColumnName = "CYCLE_FLAG",
                Read = reader => reader.Field<string>((int)SequenceTableColumns.Cycle_flag)
            };

            public static Field<string> Order_flag = new Field<string>()
            {
                ColumnName = "ORDER_FLAG",
                Read = reader => reader.Field<string>((int)SequenceTableColumns.Order_flag)
            };

            public static Field<int> Cache_size = new Field<int>()
            {
                ColumnName = "CACHE_SIZE",
                Read = reader => reader.Field<int>((int)SequenceTableColumns.Cache_size)
            };

            public static Field<long> Last_number = new Field<long>()
            {
                ColumnName = "LAST_NUMBER",
                Read = reader => reader.Field<long>((int)SequenceTableColumns.Last_number)
            };

        }

        #region Readers

        public override void Read(IDataReader r, SequenceTable item)
        {
            item.Owner = SequenceDescriptor.Columns.Owner.Read(r);
            item.Sequence_name = SequenceDescriptor.Columns.Sequence_name.Read(r);
            item.Min_value = SequenceDescriptor.Columns.Min_value.Read(r);
            item.Max_value = SequenceDescriptor.Columns.Max_value.Read(r);
            item.Increment_by = SequenceDescriptor.Columns.Increment_by.Read(r);
            item.Cycle_flag = SequenceDescriptor.Columns.Cycle_flag.Read(r) == "Y";
            item.Order_flag = SequenceDescriptor.Columns.Order_flag.Read(r) == "Y";
            item.Cache_size = SequenceDescriptor.Columns.Cache_size.Read(r);
            item.Last_number = SequenceDescriptor.Columns.Last_number.Read(r);
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.Sequence_name;
            yield return Columns.Min_value;
            yield return Columns.Max_value;
            yield return Columns.Increment_by;
            yield return Columns.Cycle_flag;
            yield return Columns.Order_flag;
            yield return Columns.Cache_size;
            yield return Columns.Last_number;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<SequenceTable> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum SequenceTableColumns
    {
        Owner,
        Sequence_name,
        Min_value,
        Max_value,
        Increment_by,
        Cycle_flag,
        Order_flag,
        Cache_size,
        Last_number,
    }

    public class SequenceTable
    {
        public string Owner { get; set; }
        public string Sequence_name { get; set; }
        public long Min_value { get; set; }
        public decimal Max_value { get; set; }
        public int Increment_by { get; set; }
        public bool Cycle_flag { get; set; }
        public bool Order_flag { get; set; }
        public int Cache_size { get; set; }
        public long Last_number { get; set; }
    }

}
