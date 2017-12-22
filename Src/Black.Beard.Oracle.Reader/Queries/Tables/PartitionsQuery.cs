using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class PartitionsQuery : DbQueryBase<PartitionsDto>
    {

        string sql =
@"
SELECT l.* FROM SYS.DBA_TAB_PARTITIONS l

{0}

ORDER BY l.PARTITION_POSITION
";

        public override List<PartitionsDto> Resolve(DbContextOracle context, Action<PartitionsDto> action)
        {

            List<PartitionsDto> List = new List<PartitionsDto>();
            var db = context.database;
            this.OracleContext = context;

            if (action == null)
                action =
                    t =>
                    {


                        if (!db.Partitions.Contains(t.PartitionName))
                        {
                            var partition = new PartitionModel()
                            {
                                PartitionName = t.PartitionName,
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

            PartitionsQueryDescriptor Partitions = new PartitionsQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "TABLE_OWNER"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = Partitions.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class PartitionsQueryDescriptor : StructureDescriptorTable<PartitionsDto>
    {

        public PartitionsQueryDescriptor(string connectionString)
            : base(() => new PartitionsDto(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> TableOwner = new Field<String>() { ColumnName = "TABLE_OWNER", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.TABLE_OWNER) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.TABLE_NAME) };
            public static Field<String> Composite = new Field<String>() { ColumnName = "COMPOSITE", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.COMPOSITE) };
            public static Field<String> PartitionName = new Field<String>() { ColumnName = "PARTITION_NAME", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.PARTITION_NAME) };
            public static Field<Decimal> SubpartitionCount = new Field<Decimal>() { ColumnName = "SUBPARTITION_COUNT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.SUBPARTITION_COUNT) };
            public static Field<String> HighValue = new Field<String>() { ColumnName = "HIGH_VALUE", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.HIGH_VALUE) };
            public static Field<Decimal> HighValueLength = new Field<Decimal>() { ColumnName = "HIGH_VALUE_LENGTH", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.HIGH_VALUE_LENGTH) };
            public static Field<Decimal> PartitionPosition = new Field<Decimal>() { ColumnName = "PARTITION_POSITION", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.PARTITION_POSITION) };
            public static Field<String> TablespaceName = new Field<String>() { ColumnName = "TABLESPACE_NAME", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.TABLESPACE_NAME) };
            public static Field<Decimal> PctFree = new Field<Decimal>() { ColumnName = "PCT_FREE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.PCT_FREE) };
            public static Field<Decimal> PctUsed = new Field<Decimal>() { ColumnName = "PCT_USED", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.PCT_USED) };
            public static Field<Decimal> IniTrans = new Field<Decimal>() { ColumnName = "INI_TRANS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.INI_TRANS) };
            public static Field<Decimal> MaxTrans = new Field<Decimal>() { ColumnName = "MAX_TRANS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.MAX_TRANS) };
            public static Field<Decimal> InitialExtent = new Field<Decimal>() { ColumnName = "INITIAL_EXTENT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.INITIAL_EXTENT) };
            public static Field<Decimal> NextExtent = new Field<Decimal>() { ColumnName = "NEXT_EXTENT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.NEXT_EXTENT) };
            public static Field<Decimal> MinExtent = new Field<Decimal>() { ColumnName = "MIN_EXTENT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.MIN_EXTENT) };
            public static Field<Decimal> MaxExtent = new Field<Decimal>() { ColumnName = "MAX_EXTENT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.MAX_EXTENT) };
            public static Field<Decimal> MaxSize = new Field<Decimal>() { ColumnName = "MAX_SIZE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.MAX_SIZE) };
            public static Field<Decimal> PctIncrease = new Field<Decimal>() { ColumnName = "PCT_INCREASE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.PCT_INCREASE) };
            public static Field<Decimal> Freelists = new Field<Decimal>() { ColumnName = "FREELISTS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.FREELISTS) };
            public static Field<Decimal> FreelistGroups = new Field<Decimal>() { ColumnName = "FREELIST_GROUPS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.FREELIST_GROUPS) };
            public static Field<String> Logging = new Field<String>() { ColumnName = "LOGGING", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.LOGGING) };
            public static Field<String> Compression = new Field<String>() { ColumnName = "COMPRESSION", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.COMPRESSION) };
            public static Field<String> CompressFor = new Field<String>() { ColumnName = "COMPRESS_FOR", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.COMPRESS_FOR) };
            public static Field<Decimal> NumRows = new Field<Decimal>() { ColumnName = "NUM_ROWS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.NUM_ROWS) };
            public static Field<Decimal> Blocks = new Field<Decimal>() { ColumnName = "BLOCKS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.BLOCKS) };
            public static Field<Decimal> EmptyBlocks = new Field<Decimal>() { ColumnName = "EMPTY_BLOCKS", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.EMPTY_BLOCKS) };
            public static Field<Decimal> AvgSpace = new Field<Decimal>() { ColumnName = "AVG_SPACE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.AVG_SPACE) };
            public static Field<Decimal> ChainCnt = new Field<Decimal>() { ColumnName = "CHAIN_CNT", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.CHAIN_CNT) };
            public static Field<Decimal> AvgRowLen = new Field<Decimal>() { ColumnName = "AVG_ROW_LEN", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.AVG_ROW_LEN) };
            public static Field<Decimal> SampleSize = new Field<Decimal>() { ColumnName = "SAMPLE_SIZE", Read = reader => reader.Field<Decimal>((int)PartitionsQueryColumns.SAMPLE_SIZE) };
            public static Field<DateTime> LastAnalyzed = new Field<DateTime>() { ColumnName = "LAST_ANALYZED", Read = reader => reader.Field<DateTime>((int)PartitionsQueryColumns.LAST_ANALYZED) };
            public static Field<String> BufferPool = new Field<String>() { ColumnName = "BUFFER_POOL", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.BUFFER_POOL) };
            public static Field<String> FlashCache = new Field<String>() { ColumnName = "FLASH_CACHE", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.FLASH_CACHE) };
            public static Field<String> CellFlashCache = new Field<String>() { ColumnName = "CELL_FLASH_CACHE", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.CELL_FLASH_CACHE) };
            public static Field<String> GlobalStats = new Field<String>() { ColumnName = "GLOBAL_STATS", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.GLOBAL_STATS) };
            public static Field<String> UserStats = new Field<String>() { ColumnName = "USER_STATS", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.USER_STATS) };
            public static Field<String> IsNested = new Field<String>() { ColumnName = "IS_NESTED", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.IS_NESTED) };
            public static Field<String> ParentTablePartition = new Field<String>() { ColumnName = "PARENT_TABLE_PARTITION", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.PARENT_TABLE_PARTITION) };
            public static Field<String> Interval = new Field<String>() { ColumnName = "INTERVAL", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.INTERVAL) };
            public static Field<String> SegmentCreated = new Field<String>() { ColumnName = "SEGMENT_CREATED", Read = reader => reader.Field<String>((int)PartitionsQueryColumns.SEGMENT_CREATED) };


        }

        #region Readers

        public override void Read(IDataReader r, PartitionsDto item)
        {
            item.TableOwner = PartitionsQueryDescriptor.Columns.TableOwner.Read(r);
            item.TableName = PartitionsQueryDescriptor.Columns.TableName.Read(r);
            item.Composite = PartitionsQueryDescriptor.Columns.Composite.Read(r);
            item.PartitionName = PartitionsQueryDescriptor.Columns.PartitionName.Read(r);
            item.SubpartitionCount = PartitionsQueryDescriptor.Columns.SubpartitionCount.Read(r);
            item.HighValue = PartitionsQueryDescriptor.Columns.HighValue.Read(r);
            item.HighValueLength = PartitionsQueryDescriptor.Columns.HighValueLength.Read(r);
            item.PartitionPosition = PartitionsQueryDescriptor.Columns.PartitionPosition.Read(r);
            item.TablespaceName = PartitionsQueryDescriptor.Columns.TablespaceName.Read(r);
            item.PctFree = PartitionsQueryDescriptor.Columns.PctFree.Read(r);
            item.PctUsed = PartitionsQueryDescriptor.Columns.PctUsed.Read(r);
            item.IniTrans = PartitionsQueryDescriptor.Columns.IniTrans.Read(r);
            item.MaxTrans = PartitionsQueryDescriptor.Columns.MaxTrans.Read(r);
            item.InitialExtent = PartitionsQueryDescriptor.Columns.InitialExtent.Read(r);
            item.NextExtent = PartitionsQueryDescriptor.Columns.NextExtent.Read(r);
            item.MinExtent = PartitionsQueryDescriptor.Columns.MinExtent.Read(r);
            item.MaxExtent = PartitionsQueryDescriptor.Columns.MaxExtent.Read(r);
            item.MaxSize = PartitionsQueryDescriptor.Columns.MaxSize.Read(r);
            item.PctIncrease = PartitionsQueryDescriptor.Columns.PctIncrease.Read(r);
            item.Freelists = PartitionsQueryDescriptor.Columns.Freelists.Read(r);
            item.FreelistGroups = PartitionsQueryDescriptor.Columns.FreelistGroups.Read(r);
            item.Logging = PartitionsQueryDescriptor.Columns.Logging.Read(r);
            item.Compression = PartitionsQueryDescriptor.Columns.Compression.Read(r);
            item.CompressFor = PartitionsQueryDescriptor.Columns.CompressFor.Read(r);
            item.NumRows = PartitionsQueryDescriptor.Columns.NumRows.Read(r);
            item.Blocks = PartitionsQueryDescriptor.Columns.Blocks.Read(r);
            item.EmptyBlocks = PartitionsQueryDescriptor.Columns.EmptyBlocks.Read(r);
            item.AvgSpace = PartitionsQueryDescriptor.Columns.AvgSpace.Read(r);
            item.ChainCnt = PartitionsQueryDescriptor.Columns.ChainCnt.Read(r);
            item.AvgRowLen = PartitionsQueryDescriptor.Columns.AvgRowLen.Read(r);
            item.SampleSize = PartitionsQueryDescriptor.Columns.SampleSize.Read(r);
            item.LastAnalyzed = PartitionsQueryDescriptor.Columns.LastAnalyzed.Read(r);
            item.BufferPool = PartitionsQueryDescriptor.Columns.BufferPool.Read(r);
            item.FlashCache = PartitionsQueryDescriptor.Columns.FlashCache.Read(r);
            item.CellFlashCache = PartitionsQueryDescriptor.Columns.CellFlashCache.Read(r);
            item.GlobalStats = PartitionsQueryDescriptor.Columns.GlobalStats.Read(r);
            item.UserStats = PartitionsQueryDescriptor.Columns.UserStats.Read(r);
            item.IsNested = PartitionsQueryDescriptor.Columns.IsNested.Read(r);
            item.ParentTablePartition = PartitionsQueryDescriptor.Columns.ParentTablePartition.Read(r);
            item.Interval = PartitionsQueryDescriptor.Columns.Interval.Read(r);
            item.SegmentCreated = PartitionsQueryDescriptor.Columns.SegmentCreated.Read(r);

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

        public override DataTable GetDataTable(string tableName, IEnumerable<PartitionsDto> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum PartitionsQueryColumns
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

    public class PartitionsDto
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

