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
    
    public class SequenceQuery_11 : DbQueryBase<SequenceTable_11>
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

        public override List<SequenceTable_11> Resolve(DbContextOracle context, Action<SequenceTable_11> action)
        {

            List<SequenceTable_11> List = new List<SequenceTable_11>();
            this.OracleContext = context;
            var db = context.database;

            if (action == null)
                action =
                    t =>
                    {
                        var name = t.Owner + "." + t.Sequence_name;
                        SequenceModel s;

                        if (db.Sequences.TryGet(name, out s))
                        {
                            s.MinValue = t.Min_value;
                            s.MaxValue = t.Max_value;
                            s.IncrementBy = t.Increment_by;
                            s.CycleFlag = t.Cycle_flag;
                            s.OrderFlag = t.Order_flag;
                            s.CacheSize = t.Cache_size;
                            
                        }
                        else
                        {
                            s = new SequenceModel()
                            {
                                Key = name,
                                Owner = t.Owner,
                                Name = t.Sequence_name,
                                MinValue = t.Min_value,
                                MaxValue = t.Max_value,
                                IncrementBy = t.Increment_by,
                                CycleFlag = t.Cycle_flag,
                                OrderFlag = t.Order_flag,
                                CacheSize = t.Cache_size,
                                Keep = SequenceModel.Default.Keep,
                                Session = SequenceModel.Default.Session,
                        };

                            db.Sequences.Add(s);
                        }

                    };


            SequenceDescriptor_11 view = new SequenceDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("s", "sequence_owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class SequenceDescriptor_11 : StructureDescriptorTable<SequenceTable_11>
    {

        public SequenceDescriptor_11(string connectionString)
            : base(() => new SequenceTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)SequenceTableColumns_11.Owner)
            };

            public static Field<string> Sequence_name = new Field<string>()
            {
                ColumnName = "SEQUENCE_NAME",
                Read = reader => reader.Field<string>((int)SequenceTableColumns_11.Sequence_name)
            };

            public static Field<string> Min_value = new Field<string>()
            {
                ColumnName = "MIN_VALUE",
                Read = reader => reader.Field<string>((int)SequenceTableColumns_11.Min_value)
            };

            public static Field<string> Max_value = new Field<string>()
            {
                ColumnName = "MAX_VALUE",
                Read = reader => reader.Field<string>((int)SequenceTableColumns_11.Max_value)
            };

            public static Field<int> Increment_by = new Field<int>()
            {
                ColumnName = "Increment_by",
                Read = reader => reader.Field<int>((int)SequenceTableColumns_11.Increment_by)
            };

            public static Field<string> Cycle_flag = new Field<string>()
            {
                ColumnName = "CYCLE_FLAG",
                Read = reader => reader.Field<string>((int)SequenceTableColumns_11.Cycle_flag)
            };

            public static Field<string> Order_flag = new Field<string>()
            {
                ColumnName = "ORDER_FLAG",
                Read = reader => reader.Field<string>((int)SequenceTableColumns_11.Order_flag)
            };

            public static Field<int> Cache_size = new Field<int>()
            {
                ColumnName = "CACHE_SIZE",
                Read = reader => reader.Field<int>((int)SequenceTableColumns_11.Cache_size)
            };

            public static Field<string> Last_number = new Field<string>()
            {
                ColumnName = "LAST_NUMBER",
                Read = reader => reader.Field<string>((int)SequenceTableColumns_11.Last_number)
            };

            //public static Field<bool> Extend_Flag = new Field<bool>()
            //{
            //    ColumnName = "EXTEND_FLAG",
            //    Read = reader => reader.Field<bool>((int)SequenceTableColumns_11.Extend_Flag)
            //};

            //public static Field<bool> Session_Flag = new Field<bool>()
            //{
            //    ColumnName = "SESSION_FLAG",
            //    Read = reader => reader.Field<bool>((int)SequenceTableColumns_11.Session_Flag)
            //};

            //public static Field<bool> Keep_Value = new Field<bool>()
            //{
            //    ColumnName = "KEEP_VALUE",
            //    Read = reader => reader.Field<bool>((int)SequenceTableColumns_11.Keep_Value)
            //};

        }

        #region Readers

        public override void Read(IDataReader r, SequenceTable_11 item)
        {
            item.Owner = SequenceDescriptor_11.Columns.Owner.Read(r);
            item.Sequence_name = SequenceDescriptor_11.Columns.Sequence_name.Read(r);
            item.Min_value = SequenceDescriptor_11.Columns.Min_value.Read(r);
            item.Max_value = SequenceDescriptor_11.Columns.Max_value.Read(r);
            item.Increment_by = SequenceDescriptor_11.Columns.Increment_by.Read(r);
            item.Cycle_flag = SequenceDescriptor_11.Columns.Cycle_flag.Read(r) == "Y";
            item.Order_flag = SequenceDescriptor_11.Columns.Order_flag.Read(r) == "Y";
            item.Cache_size = SequenceDescriptor_11.Columns.Cache_size.Read(r);
            item.Last_number = SequenceDescriptor_11.Columns.Last_number.Read(r);
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

        public override DataTable GetDataTable(string tableName, IEnumerable<SequenceTable_11> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum SequenceTableColumns_11
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

    public class SequenceTable_11
    {
        public string Owner { get; set; }
        public string Sequence_name { get; set; }
        public string Min_value { get; set; }
        public string Max_value { get; set; }
        public int Increment_by { get; set; }
        public bool Cycle_flag { get; set; }
        public bool Order_flag { get; set; }
        public int Cache_size { get; set; }
        public string Last_number { get; set; }
    }

}


/*

Table : DBA_SEQUENCES
     
Column	        Datatype	    NULL	    Description
-------------------------------------------------------------------------------
SEQUENCE_OWNER	VARCHAR2(30)	NOT NULL	Name of the owner of the sequence
SEQUENCE_NAME	VARCHAR2(30)	NOT NULL	Sequence name
MIN_VALUE	    NUMBER	 	                Minimum value of the sequence
MAX_VALUE	    NUMBER	 	                Maximum value of the sequence
INCREMENT_BY	NUMBER	        NOT NULL	Value by which sequence is incremented
CYCLE_FLAG	    VARCHAR2(1)	 	            Does sequence wrap around on reaching limit
ORDER_FLAG	    VARCHAR2(1)	 	            Are sequence numbers generated in order
CACHE_SIZE	    NUMBER	        NOT NULL    Number of sequence numbers to cache
LAST_NUMBER	    NUMBER	        NOT NULL	Last sequence number written to disk. 
                                            If a sequence uses caching, the number written to disk 
                                            is the last number placed in the sequence cache. 
                                            This number is likely to be greater than the last 
                                            sequence number that was used.
     */
