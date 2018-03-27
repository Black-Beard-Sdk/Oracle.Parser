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

    public class PartitionsQuery_11 : DbQueryBase<PartitionsDto_11>
    {

        string sql =
@"
SELECT l.* FROM SYS.DBA_TAB_PARTITIONS l

{0}

ORDER BY l.PARTITION_POSITION
";

        public override List<PartitionsDto_11> Resolve(DbContextOracle context, Action<PartitionsDto_11> action)
        {

            List<PartitionsDto_11> List = new List<PartitionsDto_11>();
            var db = context.Database;
            this.OracleContext = context;

            if (action == null)
                action =
                    t =>
                    {


                        if (!db.Partitions.Contains(t.PartitionName))
                        {
                            var partition = new PartitionModel()
                            {
                                Name = t.PartitionName,
                                Composite = t.Composite.ToBoolean(),
                                HighValue = t.HighValue,
                                HighValueLength = t.HighValueLength,
                                PartitionPosition = t.PartitionPosition,
                                TablespaceName = t.TablespaceName,
                                PctFree = t.PctFree,
                                PctUsed = t.PctUsed,
                                IniTrans = t.IniTrans,
                                MaxTrans = t.MaxTrans,
                                InitialExtent = t.InitialExtent,
                                NextExtent = t.NextExtent,
                                MinExtent = t.MinExtent,
                                MaxExtent = t.MaxExtent,
                                MaxSize = t.MaxSize,
                                PctIncrease = t.PctIncrease,
                                Freelists = t.Freelists,
                                FreelistGroups = t.FreelistGroups,
                                Logging = t.Logging.ToBoolean(),
                                Compression = t.Compression,
                                CompressFor = t.CompressFor,
                                NumRows = t.NumRows,
                                Blocks = t.Blocks,
                                EmptyBlocks = t.EmptyBlocks,
                            //AvgSpace = t.AvgSpace,
                            ChainCnt = t.ChainCnt,
                            //AvgRowLen = t.AvgRowLen,
                            //SampleSize = t.SampleSize,
                            //LastAnalyzed = t.LastAnalyzed,
                            BufferPool = t.BufferPool,
                                FlashCache = t.FlashCache,
                                CellFlashCache = t.CellFlashCache,
                                GlobalStats = t.GlobalStats.ToBoolean(),
                                UserStats = t.UserStats.ToBoolean(),
                                IsNested = t.IsNested.ToBoolean(),
                                ParentTablePartition = t.ParentTablePartition,
                                Interval = t.Interval.ToBoolean(),
                                SegmentCreated = t.SegmentCreated.ToBoolean(),

                            };

                            db.Partitions.Add(partition);

                        }

                        TableModel ta;

                        string key = t.TableOwner + "." + t.TableName;
                        if (db.Tables.TryGet(key, out ta))
                        {

                            var partionRef = new PartitionRefModel() { PartitionName = t.PartitionName };
                            ta.Partitions.Add(partionRef);

                        }


                    };

            PartitionsQueryDescriptor_11 Partitions = new PartitionsQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "TABLE_OWNER"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = Partitions.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class PartitionsQueryDescriptor_11 : StructureDescriptorTable<PartitionsDto_11>
    {

        public PartitionsQueryDescriptor_11(string connectionString)
            : base(() => new PartitionsDto_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> TableOwner = new Field<String>() { ColumnName = "TABLE_OWNER", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.TABLE_OWNER) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.TABLE_NAME) };
            public static Field<String> Composite = new Field<String>() { ColumnName = "COMPOSITE", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.COMPOSITE) };
            public static Field<String> PartitionName = new Field<String>() { ColumnName = "PARTITION_NAME", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.PARTITION_NAME) };
            public static Field<Decimal> SubpartitionCount = new Field<Decimal>() { ColumnName = "SUBPARTITION_COUNT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.SUBPARTITION_COUNT) };
            public static Field<String> HighValue = new Field<String>() { ColumnName = "HIGH_VALUE", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.HIGH_VALUE) };
            public static Field<Decimal> HighValueLength = new Field<Decimal>() { ColumnName = "HIGH_VALUE_LENGTH", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.HIGH_VALUE_LENGTH) };
            public static Field<Decimal> PartitionPosition = new Field<Decimal>() { ColumnName = "PARTITION_POSITION", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.PARTITION_POSITION) };
            public static Field<String> TablespaceName = new Field<String>() { ColumnName = "TABLESPACE_NAME", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.TABLESPACE_NAME) };
            public static Field<Decimal> PctFree = new Field<Decimal>() { ColumnName = "PCT_FREE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.PCT_FREE) };
            public static Field<Decimal> PctUsed = new Field<Decimal>() { ColumnName = "PCT_USED", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.PCT_USED) };
            public static Field<Decimal> IniTrans = new Field<Decimal>() { ColumnName = "INI_TRANS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.INI_TRANS) };
            public static Field<Decimal> MaxTrans = new Field<Decimal>() { ColumnName = "MAX_TRANS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.MAX_TRANS) };
            public static Field<Decimal> InitialExtent = new Field<Decimal>() { ColumnName = "INITIAL_EXTENT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.INITIAL_EXTENT) };
            public static Field<Decimal> NextExtent = new Field<Decimal>() { ColumnName = "NEXT_EXTENT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.NEXT_EXTENT) };
            public static Field<Decimal> MinExtent = new Field<Decimal>() { ColumnName = "MIN_EXTENT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.MIN_EXTENT) };
            public static Field<Decimal> MaxExtent = new Field<Decimal>() { ColumnName = "MAX_EXTENT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.MAX_EXTENT) };
            public static Field<Decimal> MaxSize = new Field<Decimal>() { ColumnName = "MAX_SIZE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.MAX_SIZE) };
            public static Field<Decimal> PctIncrease = new Field<Decimal>() { ColumnName = "PCT_INCREASE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.PCT_INCREASE) };
            public static Field<Decimal> Freelists = new Field<Decimal>() { ColumnName = "FREELISTS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.FREELISTS) };
            public static Field<Decimal> FreelistGroups = new Field<Decimal>() { ColumnName = "FREELIST_GROUPS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.FREELIST_GROUPS) };
            public static Field<String> Logging = new Field<String>() { ColumnName = "LOGGING", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.LOGGING) };
            public static Field<String> Compression = new Field<String>() { ColumnName = "COMPRESSION", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.COMPRESSION) };
            public static Field<String> CompressFor = new Field<String>() { ColumnName = "COMPRESS_FOR", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.COMPRESS_FOR) };
            public static Field<Decimal> NumRows = new Field<Decimal>() { ColumnName = "NUM_ROWS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.NUM_ROWS) };
            public static Field<Decimal> Blocks = new Field<Decimal>() { ColumnName = "BLOCKS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.BLOCKS) };
            public static Field<Decimal> EmptyBlocks = new Field<Decimal>() { ColumnName = "EMPTY_BLOCKS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.EMPTY_BLOCKS) };
            public static Field<Decimal> AvgSpace = new Field<Decimal>() { ColumnName = "AVG_SPACE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.AVG_SPACE) };
            public static Field<Decimal> ChainCnt = new Field<Decimal>() { ColumnName = "CHAIN_CNT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.CHAIN_CNT) };
            public static Field<Decimal> AvgRowLen = new Field<Decimal>() { ColumnName = "AVG_ROW_LEN", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.AVG_ROW_LEN) };
            public static Field<Decimal> SampleSize = new Field<Decimal>() { ColumnName = "SAMPLE_SIZE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns_11.SAMPLE_SIZE) };
            public static Field<DateTime> LastAnalyzed = new Field<DateTime>() { ColumnName = "LAST_ANALYZED", Read = reader => reader.Field<DateTime>((int)PartitionsQueryColumns_11.LAST_ANALYZED) };
            public static Field<String> BufferPool = new Field<String>() { ColumnName = "BUFFER_POOL", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.BUFFER_POOL) };
            public static Field<String> FlashCache = new Field<String>() { ColumnName = "FLASH_CACHE", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.FLASH_CACHE) };
            public static Field<String> CellFlashCache = new Field<String>() { ColumnName = "CELL_FLASH_CACHE", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.CELL_FLASH_CACHE) };
            public static Field<String> GlobalStats = new Field<String>() { ColumnName = "GLOBAL_STATS", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.GLOBAL_STATS) };
            public static Field<String> UserStats = new Field<String>() { ColumnName = "USER_STATS", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.USER_STATS) };
            public static Field<String> IsNested = new Field<String>() { ColumnName = "IS_NESTED", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.IS_NESTED) };
            public static Field<String> ParentTablePartition = new Field<String>() { ColumnName = "PARENT_TABLE_PARTITION", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.PARENT_TABLE_PARTITION) };
            public static Field<String> Interval = new Field<String>() { ColumnName = "INTERVAL", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.INTERVAL) };
            public static Field<String> SegmentCreated = new Field<String>() { ColumnName = "SEGMENT_CREATED", Read = reader => reader.Field<String>((int)PartitionsQueryColumns_11.SEGMENT_CREATED) };


        }

        #region Readers

        public override void Read(IDataReader r, PartitionsDto_11 item)
        {
            item.TableOwner = PartitionsQueryDescriptor_11.Columns.TableOwner.Read(r);
            item.TableName = PartitionsQueryDescriptor_11.Columns.TableName.Read(r);
            item.Composite = PartitionsQueryDescriptor_11.Columns.Composite.Read(r);
            item.PartitionName = PartitionsQueryDescriptor_11.Columns.PartitionName.Read(r);
            item.SubpartitionCount = PartitionsQueryDescriptor_11.Columns.SubpartitionCount.Read(r);
            item.HighValue = PartitionsQueryDescriptor_11.Columns.HighValue.Read(r);
            item.HighValueLength = PartitionsQueryDescriptor_11.Columns.HighValueLength.Read(r);
            item.PartitionPosition = PartitionsQueryDescriptor_11.Columns.PartitionPosition.Read(r);
            item.TablespaceName = PartitionsQueryDescriptor_11.Columns.TablespaceName.Read(r);
            item.PctFree = PartitionsQueryDescriptor_11.Columns.PctFree.Read(r);
            item.PctUsed = PartitionsQueryDescriptor_11.Columns.PctUsed.Read(r);
            item.IniTrans = PartitionsQueryDescriptor_11.Columns.IniTrans.Read(r);
            item.MaxTrans = PartitionsQueryDescriptor_11.Columns.MaxTrans.Read(r);
            item.InitialExtent = PartitionsQueryDescriptor_11.Columns.InitialExtent.Read(r);
            item.NextExtent = PartitionsQueryDescriptor_11.Columns.NextExtent.Read(r);
            item.MinExtent = PartitionsQueryDescriptor_11.Columns.MinExtent.Read(r);
            item.MaxExtent = PartitionsQueryDescriptor_11.Columns.MaxExtent.Read(r);
            item.MaxSize = PartitionsQueryDescriptor_11.Columns.MaxSize.Read(r);
            item.PctIncrease = PartitionsQueryDescriptor_11.Columns.PctIncrease.Read(r);
            item.Freelists = PartitionsQueryDescriptor_11.Columns.Freelists.Read(r);
            item.FreelistGroups = PartitionsQueryDescriptor_11.Columns.FreelistGroups.Read(r);
            item.Logging = PartitionsQueryDescriptor_11.Columns.Logging.Read(r);
            item.Compression = PartitionsQueryDescriptor_11.Columns.Compression.Read(r);
            item.CompressFor = PartitionsQueryDescriptor_11.Columns.CompressFor.Read(r);
            item.NumRows = PartitionsQueryDescriptor_11.Columns.NumRows.Read(r);
            item.Blocks = PartitionsQueryDescriptor_11.Columns.Blocks.Read(r);
            item.EmptyBlocks = PartitionsQueryDescriptor_11.Columns.EmptyBlocks.Read(r);
            item.AvgSpace = PartitionsQueryDescriptor_11.Columns.AvgSpace.Read(r);
            item.ChainCnt = PartitionsQueryDescriptor_11.Columns.ChainCnt.Read(r);
            item.AvgRowLen = PartitionsQueryDescriptor_11.Columns.AvgRowLen.Read(r);
            item.SampleSize = PartitionsQueryDescriptor_11.Columns.SampleSize.Read(r);
            item.LastAnalyzed = PartitionsQueryDescriptor_11.Columns.LastAnalyzed.Read(r);
            item.BufferPool = PartitionsQueryDescriptor_11.Columns.BufferPool.Read(r);
            item.FlashCache = PartitionsQueryDescriptor_11.Columns.FlashCache.Read(r);
            item.CellFlashCache = PartitionsQueryDescriptor_11.Columns.CellFlashCache.Read(r);
            item.GlobalStats = PartitionsQueryDescriptor_11.Columns.GlobalStats.Read(r);
            item.UserStats = PartitionsQueryDescriptor_11.Columns.UserStats.Read(r);
            item.IsNested = PartitionsQueryDescriptor_11.Columns.IsNested.Read(r);
            item.ParentTablePartition = PartitionsQueryDescriptor_11.Columns.ParentTablePartition.Read(r);
            item.Interval = PartitionsQueryDescriptor_11.Columns.Interval.Read(r);
            item.SegmentCreated = PartitionsQueryDescriptor_11.Columns.SegmentCreated.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.TableOwner;
            yield return Columns.TableName;
            yield return Columns.Composite;
            yield return Columns.PartitionName;
            yield return Columns.SubpartitionCount;
            yield return Columns.HighValue;
            yield return Columns.HighValueLength;
            yield return Columns.PartitionPosition;
            yield return Columns.TablespaceName;
            yield return Columns.PctFree;
            yield return Columns.PctUsed;
            yield return Columns.IniTrans;
            yield return Columns.MaxTrans;
            yield return Columns.InitialExtent;
            yield return Columns.NextExtent;
            yield return Columns.MinExtent;
            yield return Columns.MaxExtent;
            yield return Columns.MaxSize;
            yield return Columns.PctIncrease;
            yield return Columns.Freelists;
            yield return Columns.FreelistGroups;
            yield return Columns.Logging;
            yield return Columns.Compression;
            yield return Columns.CompressFor;
            yield return Columns.NumRows;
            yield return Columns.Blocks;
            yield return Columns.EmptyBlocks;
            yield return Columns.AvgSpace;
            yield return Columns.ChainCnt;
            yield return Columns.AvgRowLen;
            yield return Columns.SampleSize;
            yield return Columns.LastAnalyzed;
            yield return Columns.BufferPool;
            yield return Columns.FlashCache;
            yield return Columns.CellFlashCache;
            yield return Columns.GlobalStats;
            yield return Columns.UserStats;
            yield return Columns.IsNested;
            yield return Columns.ParentTablePartition;
            yield return Columns.Interval;
            yield return Columns.SegmentCreated;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<PartitionsDto_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum PartitionsQueryColumns_11
    {
        TABLE_OWNER,
        TABLE_NAME,
        COMPOSITE,
        PARTITION_NAME,
        SUBPARTITION_COUNT,
        HIGH_VALUE,
        HIGH_VALUE_LENGTH,
        PARTITION_POSITION,
        TABLESPACE_NAME,
        PCT_FREE,
        PCT_USED,
        INI_TRANS,
        MAX_TRANS,
        INITIAL_EXTENT,
        NEXT_EXTENT,
        MIN_EXTENT,
        MAX_EXTENT,
        MAX_SIZE,
        PCT_INCREASE,
        FREELISTS,
        FREELIST_GROUPS,
        LOGGING,
        COMPRESSION,
        COMPRESS_FOR,
        NUM_ROWS,
        BLOCKS,
        EMPTY_BLOCKS,
        AVG_SPACE,
        CHAIN_CNT,
        AVG_ROW_LEN,
        SAMPLE_SIZE,
        LAST_ANALYZED,
        BUFFER_POOL,
        FLASH_CACHE,
        CELL_FLASH_CACHE,
        GLOBAL_STATS,
        USER_STATS,
        IS_NESTED,
        PARENT_TABLE_PARTITION,
        INTERVAL,
        SEGMENT_CREATED,

    }

    public class PartitionsDto_11
    {

        public String TableOwner { get; set; }
        public String TableName { get; set; }
        public String Composite { get; set; }
        public String PartitionName { get; set; }
        public Decimal SubpartitionCount { get; set; }
        public String HighValue { get; set; }
        public Decimal HighValueLength { get; set; }
        public Decimal PartitionPosition { get; set; }
        public String TablespaceName { get; set; }
        public Decimal PctFree { get; set; }
        public Decimal PctUsed { get; set; }
        public Decimal IniTrans { get; set; }
        public Decimal MaxTrans { get; set; }
        public Decimal InitialExtent { get; set; }
        public Decimal NextExtent { get; set; }
        public Decimal MinExtent { get; set; }
        public Decimal MaxExtent { get; set; }
        public Decimal MaxSize { get; set; }
        public Decimal PctIncrease { get; set; }
        public Decimal Freelists { get; set; }
        public Decimal FreelistGroups { get; set; }
        public String Logging { get; set; }
        public String Compression { get; set; }
        public String CompressFor { get; set; }
        public Decimal NumRows { get; set; }
        public Decimal Blocks { get; set; }
        public Decimal EmptyBlocks { get; set; }
        public Decimal AvgSpace { get; set; }
        public Decimal ChainCnt { get; set; }
        public Decimal AvgRowLen { get; set; }
        public Decimal SampleSize { get; set; }
        public DateTime LastAnalyzed { get; set; }
        public String BufferPool { get; set; }
        public String FlashCache { get; set; }
        public String CellFlashCache { get; set; }
        public String GlobalStats { get; set; }
        public String UserStats { get; set; }
        public String IsNested { get; set; }
        public String ParentTablePartition { get; set; }
        public String Interval { get; set; }
        public String SegmentCreated { get; set; }


    }

}

