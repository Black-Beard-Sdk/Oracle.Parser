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

    public class SubPartitionsQuery_11 : DbQueryBase<SubPartitionsDto>
    {

        string sql =
@"
SELECT * FROM SYS.DBA_TAB_SUBPARTITIONS l

{0}

";

        public override List<SubPartitionsDto> Resolve(DbContextOracle context, Action<SubPartitionsDto> action)
        {
            List<SubPartitionsDto> List = new List<SubPartitionsDto>();
            var db = context.database;
            this.OracleContext = context;

            if (action == null)
                action =
                    t =>
                    {

                        var partition = db.Partitions[t.PartitionName];

                        if (partition != null)
                        {
                            if (partition.SubPartitions.Contains(t.SubpartitionName))
                            {
                                var sub = new SubPartitionModel()
                                {
                                    Name = t.SubpartitionName,
                                    HighValue = t.HighValue,
                                    HighValueLength = t.HighValueLength,
                                    SubpartitionPosition = t.SubpartitionPosition,
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
                                    Interval = t.Interval.ToBoolean(),
                                    SegmentCreated = t.SegmentCreated.ToBoolean(),
                                };

                                partition.SubPartitions.Add(sub);

                            }

                        }

                    };

            SubPartitionsQueryDescriptor SubPartitions = new SubPartitionsQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "TABLE_OWNER"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = SubPartitions.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class SubPartitionsQueryDescriptor : StructureDescriptorTable<SubPartitionsDto>
    {

        public SubPartitionsQueryDescriptor(string connectionString)
            : base(() => new SubPartitionsDto(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> TableOwner = new Field<String>() { ColumnName = "TABLE_OWNER", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.TABLE_OWNER) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.TABLE_NAME) };
            public static Field<String> PartitionName = new Field<String>() { ColumnName = "PARTITION_NAME", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.PARTITION_NAME) };
            public static Field<String> SubpartitionName = new Field<String>() { ColumnName = "SUBPARTITION_NAME", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.SUBPARTITION_NAME) };
            public static Field<String> HighValue = new Field<String>() { ColumnName = "HIGH_VALUE", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.HIGH_VALUE) };
            public static Field<Decimal> HighValueLength = new Field<Decimal>() { ColumnName = "HIGH_VALUE_LENGTH", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.HIGH_VALUE_LENGTH) };
            public static Field<Decimal> SubpartitionPosition = new Field<Decimal>() { ColumnName = "SUBPARTITION_POSITION", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.SUBPARTITION_POSITION) };
            public static Field<String> TablespaceName = new Field<String>() { ColumnName = "TABLESPACE_NAME", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.TABLESPACE_NAME) };
            public static Field<Decimal> PctFree = new Field<Decimal>() { ColumnName = "PCT_FREE", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.PCT_FREE) };
            public static Field<Decimal> PctUsed = new Field<Decimal>() { ColumnName = "PCT_USED", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.PCT_USED) };
            public static Field<Decimal> IniTrans = new Field<Decimal>() { ColumnName = "INI_TRANS", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.INI_TRANS) };
            public static Field<Decimal> MaxTrans = new Field<Decimal>() { ColumnName = "MAX_TRANS", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.MAX_TRANS) };
            public static Field<Decimal> InitialExtent = new Field<Decimal>() { ColumnName = "INITIAL_EXTENT", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.INITIAL_EXTENT) };
            public static Field<Decimal> NextExtent = new Field<Decimal>() { ColumnName = "NEXT_EXTENT", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.NEXT_EXTENT) };
            public static Field<Decimal> MinExtent = new Field<Decimal>() { ColumnName = "MIN_EXTENT", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.MIN_EXTENT) };
            public static Field<Decimal> MaxExtent = new Field<Decimal>() { ColumnName = "MAX_EXTENT", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.MAX_EXTENT) };
            public static Field<Decimal> MaxSize = new Field<Decimal>() { ColumnName = "MAX_SIZE", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.MAX_SIZE) };
            public static Field<Decimal> PctIncrease = new Field<Decimal>() { ColumnName = "PCT_INCREASE", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.PCT_INCREASE) };
            public static Field<Decimal> Freelists = new Field<Decimal>() { ColumnName = "FREELISTS", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.FREELISTS) };
            public static Field<Decimal> FreelistGroups = new Field<Decimal>() { ColumnName = "FREELIST_GROUPS", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.FREELIST_GROUPS) };
            public static Field<String> Logging = new Field<String>() { ColumnName = "LOGGING", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.LOGGING) };
            public static Field<String> Compression = new Field<String>() { ColumnName = "COMPRESSION", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.COMPRESSION) };
            public static Field<String> CompressFor = new Field<String>() { ColumnName = "COMPRESS_FOR", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.COMPRESS_FOR) };
            public static Field<Decimal> NumRows = new Field<Decimal>() { ColumnName = "NUM_ROWS", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.NUM_ROWS) };
            public static Field<Decimal> Blocks = new Field<Decimal>() { ColumnName = "BLOCKS", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.BLOCKS) };
            public static Field<Decimal> EmptyBlocks = new Field<Decimal>() { ColumnName = "EMPTY_BLOCKS", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.EMPTY_BLOCKS) };
            public static Field<Decimal> AvgSpace = new Field<Decimal>() { ColumnName = "AVG_SPACE", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.AVG_SPACE) };
            public static Field<Decimal> ChainCnt = new Field<Decimal>() { ColumnName = "CHAIN_CNT", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.CHAIN_CNT) };
            public static Field<Decimal> AvgRowLen = new Field<Decimal>() { ColumnName = "AVG_ROW_LEN", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.AVG_ROW_LEN) };
            public static Field<Decimal> SampleSize = new Field<Decimal>() { ColumnName = "SAMPLE_SIZE", Read = reader => reader.Field<Decimal>((int)SubPartitionsQueryColumns.SAMPLE_SIZE) };
            public static Field<DateTime> LastAnalyzed = new Field<DateTime>() { ColumnName = "LAST_ANALYZED", Read = reader => reader.Field<DateTime>((int)SubPartitionsQueryColumns.LAST_ANALYZED) };
            public static Field<String> BufferPool = new Field<String>() { ColumnName = "BUFFER_POOL", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.BUFFER_POOL) };
            public static Field<String> FlashCache = new Field<String>() { ColumnName = "FLASH_CACHE", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.FLASH_CACHE) };
            public static Field<String> CellFlashCache = new Field<String>() { ColumnName = "CELL_FLASH_CACHE", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.CELL_FLASH_CACHE) };
            public static Field<String> GlobalStats = new Field<String>() { ColumnName = "GLOBAL_STATS", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.GLOBAL_STATS) };
            public static Field<String> UserStats = new Field<String>() { ColumnName = "USER_STATS", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.USER_STATS) };
            public static Field<String> Interval = new Field<String>() { ColumnName = "INTERVAL", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.INTERVAL) };
            public static Field<String> SegmentCreated = new Field<String>() { ColumnName = "SEGMENT_CREATED", Read = reader => reader.Field<String>((int)SubPartitionsQueryColumns.SEGMENT_CREATED) };


        }

        #region Readers

        public override void Read(IDataReader r, SubPartitionsDto item)
        {
            item.TableOwner = SubPartitionsQueryDescriptor.Columns.TableOwner.Read(r);
            item.TableName = SubPartitionsQueryDescriptor.Columns.TableName.Read(r);
            item.PartitionName = SubPartitionsQueryDescriptor.Columns.PartitionName.Read(r);
            item.SubpartitionName = SubPartitionsQueryDescriptor.Columns.SubpartitionName.Read(r);
            item.HighValue = SubPartitionsQueryDescriptor.Columns.HighValue.Read(r);
            item.HighValueLength = SubPartitionsQueryDescriptor.Columns.HighValueLength.Read(r);
            item.SubpartitionPosition = SubPartitionsQueryDescriptor.Columns.SubpartitionPosition.Read(r);
            item.TablespaceName = SubPartitionsQueryDescriptor.Columns.TablespaceName.Read(r);
            item.PctFree = SubPartitionsQueryDescriptor.Columns.PctFree.Read(r);
            item.PctUsed = SubPartitionsQueryDescriptor.Columns.PctUsed.Read(r);
            item.IniTrans = SubPartitionsQueryDescriptor.Columns.IniTrans.Read(r);
            item.MaxTrans = SubPartitionsQueryDescriptor.Columns.MaxTrans.Read(r);
            item.InitialExtent = SubPartitionsQueryDescriptor.Columns.InitialExtent.Read(r);
            item.NextExtent = SubPartitionsQueryDescriptor.Columns.NextExtent.Read(r);
            item.MinExtent = SubPartitionsQueryDescriptor.Columns.MinExtent.Read(r);
            item.MaxExtent = SubPartitionsQueryDescriptor.Columns.MaxExtent.Read(r);
            item.MaxSize = SubPartitionsQueryDescriptor.Columns.MaxSize.Read(r);
            item.PctIncrease = SubPartitionsQueryDescriptor.Columns.PctIncrease.Read(r);
            item.Freelists = SubPartitionsQueryDescriptor.Columns.Freelists.Read(r);
            item.FreelistGroups = SubPartitionsQueryDescriptor.Columns.FreelistGroups.Read(r);
            item.Logging = SubPartitionsQueryDescriptor.Columns.Logging.Read(r);
            item.Compression = SubPartitionsQueryDescriptor.Columns.Compression.Read(r);
            item.CompressFor = SubPartitionsQueryDescriptor.Columns.CompressFor.Read(r);
            item.NumRows = SubPartitionsQueryDescriptor.Columns.NumRows.Read(r);
            item.Blocks = SubPartitionsQueryDescriptor.Columns.Blocks.Read(r);
            item.EmptyBlocks = SubPartitionsQueryDescriptor.Columns.EmptyBlocks.Read(r);
            item.AvgSpace = SubPartitionsQueryDescriptor.Columns.AvgSpace.Read(r);
            item.ChainCnt = SubPartitionsQueryDescriptor.Columns.ChainCnt.Read(r);
            item.AvgRowLen = SubPartitionsQueryDescriptor.Columns.AvgRowLen.Read(r);
            item.SampleSize = SubPartitionsQueryDescriptor.Columns.SampleSize.Read(r);
            item.LastAnalyzed = SubPartitionsQueryDescriptor.Columns.LastAnalyzed.Read(r);
            item.BufferPool = SubPartitionsQueryDescriptor.Columns.BufferPool.Read(r);
            item.FlashCache = SubPartitionsQueryDescriptor.Columns.FlashCache.Read(r);
            item.CellFlashCache = SubPartitionsQueryDescriptor.Columns.CellFlashCache.Read(r);
            item.GlobalStats = SubPartitionsQueryDescriptor.Columns.GlobalStats.Read(r);
            item.UserStats = SubPartitionsQueryDescriptor.Columns.UserStats.Read(r);
            item.Interval = SubPartitionsQueryDescriptor.Columns.Interval.Read(r);
            item.SegmentCreated = SubPartitionsQueryDescriptor.Columns.SegmentCreated.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.TableOwner;
            yield return Columns.TableName;
            yield return Columns.PartitionName;
            yield return Columns.SubpartitionName;
            yield return Columns.HighValue;
            yield return Columns.HighValueLength;
            yield return Columns.SubpartitionPosition;
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
            yield return Columns.Interval;
            yield return Columns.SegmentCreated;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<SubPartitionsDto> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum SubPartitionsQueryColumns
    {
        TABLE_OWNER,
        TABLE_NAME,
        PARTITION_NAME,
        SUBPARTITION_NAME,
        HIGH_VALUE,
        HIGH_VALUE_LENGTH,
        SUBPARTITION_POSITION,
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
        INTERVAL,
        SEGMENT_CREATED,

    }

    public class SubPartitionsDto
    {

        public String TableOwner { get; set; }
        public String TableName { get; set; }
        public String PartitionName { get; set; }
        public String SubpartitionName { get; set; }
        public String HighValue { get; set; }
        public Decimal HighValueLength { get; set; }
        public Decimal SubpartitionPosition { get; set; }
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
        public String Interval { get; set; }
        public String SegmentCreated { get; set; }


    }

}

