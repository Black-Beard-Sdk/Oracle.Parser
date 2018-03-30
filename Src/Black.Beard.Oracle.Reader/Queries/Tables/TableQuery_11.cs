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

    public class TableQuery_11 : DbQueryBase<TableDto_11>
    {

        string sql =
@"

SELECT 
  OWNER, 
  TABLE_NAME, 
  TABLESPACE_NAME, 
  CLUSTER_NAME, 
  STATUS, 
  PCT_FREE,
  PCT_USED, 
  INITIAL_EXTENT,
  INI_TRANS,
  l.MAX_TRANS,
  NEXT_EXTENT, 
  MIN_EXTENTS, 
  MAX_EXTENTS
  LOGGING, 
  CACHE, 
  TABLE_LOCK, 
  PARTITIONED, 
  TEMPORARY, 
  SECONDARY, 
  NESTED, 
  BUFFER_POOL, 
  FLASH_CACHE, 
  CELL_FLASH_CACHE, 
  ROW_MOVEMENT, 
  GLOBAL_STATS, 
  USER_STATS, 
  DURATION, 
  SKIP_CORRUPT, 
  MONITORING, 
  CLUSTER_OWNER, 
  DEPENDENCIES, 
  COMPRESSION, 
  COMPRESS_FOR, 
  DROPPED, 
  READ_ONLY, 
  SEGMENT_CREATED, 
  RESULT_CACHE 
FROM SYS.DBA_TABLES l



{0}

ORDER BY OWNER, TABLE_NAME
";

        public override List<TableDto_11> Resolve(DbContextOracle context, Action<TableDto_11> action)
        {

            List<TableDto_11> List = new List<TableDto_11>();
            this.OracleContext = context;
            var db = context.Database;

            if (action == null)
                action =
                    t =>
                    {

                        if (t.TableName.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.Table))
                            return;

                        string key = t.Owner + "." + t.TableName;
                        TableModel table;

                        if (db.Tables.TryGet(key, out table))
                        {

                            table.Tablespace.Name = t.TablespaceName;
                            table.ClusterName = t.ClusterName;
                            table.Status = t.Status;
                            table.PctFree = t.PctFree;
                            table.PctUsed = t.PctUsed;
                            table.InitialExtent = t.InitialExtent;
                            table.NextExtent = t.NextExtent;
                            table.MinExtents = t.MinExtents;
                            table.Logging = t.Logging;
                            table.Cache = t.Cache.ToBoolean();
                            table.TableLock = t.TableLock;
                            table.Partitioned = t.Partitioned.ToBoolean();
                            table.Temporary = t.Temporary.ToBoolean();
                            table.Secondary = t.Secondary.ToBoolean();
                            table.Nested = t.Nested.ToBoolean();
                            table.BufferPool = t.BufferPool;
                            table.FlashCache = t.FlashCache;
                            table.CellFlashCache = t.CellFlashCache;
                            table.RowMovement = t.RowMovement.ToBoolean();
                            table.GlobalStats = t.GlobalStats.ToBoolean();
                            table.UserStats = t.UserStats.ToBoolean();
                            table.Duration = t.Duration;
                            table.SkipCorrupt = t.SkipCorrupt;
                            table.Monitoring = t.Monitoring.ToBoolean();
                            table.ClusterOwner = t.ClusterOwner;
                            table.Dependencies = t.Dependencies.ToBoolean();
                            table.Compression = t.Compression.ToBoolean();
                            table.CompressFor = t.CompressFor;
                            table.Dropped = t.Dropped.ToBoolean();
                            table.ReadOnly = t.ReadOnly.ToBoolean();
                            table.SegmentCreated = t.SegmentCreated.ToBoolean();
                            table.ResultCache = t.ResultCache;
                            table.IniTrans = t.IniTrans;
                            table.MaxTrans = t.MaxTrans;

                        }

                    };

            TableQueryDescriptor_11 Table = new TableQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = Table.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class TableQueryDescriptor_11 : StructureDescriptorTable<TableDto_11>
    {

        public TableQueryDescriptor_11(string connectionString)
            : base(() => new TableDto_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)TableQueryColumns_11.OWNER) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)TableQueryColumns_11.TABLE_NAME) };
            public static Field<String> TablespaceName = new Field<String>() { ColumnName = "TABLESPACE_NAME", Read = reader => reader.Field<String>((int)TableQueryColumns_11.TABLESPACE_NAME) };
            public static Field<String> ClusterName = new Field<String>() { ColumnName = "CLUSTER_NAME", Read = reader => reader.Field<String>((int)TableQueryColumns_11.CLUSTER_NAME) };
            public static Field<String> Status = new Field<String>() { ColumnName = "STATUS", Read = reader => reader.Field<String>((int)TableQueryColumns_11.STATUS) };
            public static Field<Decimal> PctFree = new Field<Decimal>() { ColumnName = "PCT_FREE", Read = reader => reader.Field<Decimal>((int)TableQueryColumns_11.PCT_FREE) };
            public static Field<Decimal> PctUsed = new Field<Decimal>() { ColumnName = "PCT_USED", Read = reader => reader.Field<Decimal>((int)TableQueryColumns_11.PCT_USED) };
            public static Field<Decimal> InitialExtent = new Field<Decimal>() { ColumnName = "INITIAL_EXTENT", Read = reader => reader.Field<Decimal>((int)TableQueryColumns_11.INITIAL_EXTENT) };
            public static Field<Decimal> IniTrans = new Field<Decimal>() { ColumnName = "INI_TRANS", Read = reader => reader.Field<Decimal>((int)TableQueryColumns_11.INI_TRANS) };
            public static Field<Decimal> MaxTrans = new Field<Decimal>() { ColumnName = "MAX_TRANS", Read = reader => reader.Field<Decimal>((int)TableQueryColumns_11.MAX_TRANS) };
            public static Field<Decimal> NextExtent = new Field<Decimal>() { ColumnName = "NEXT_EXTENT", Read = reader => reader.Field<Decimal>((int)TableQueryColumns_11.NEXT_EXTENT) };
            public static Field<Decimal> MinExtents = new Field<Decimal>() { ColumnName = "MIN_EXTENTS", Read = reader => reader.Field<Decimal>((int)TableQueryColumns_11.MIN_EXTENTS) };
            public static Field<Decimal> Logging = new Field<Decimal>() { ColumnName = "LOGGING", Read = reader => reader.Field<Decimal>((int)TableQueryColumns_11.LOGGING) };
            public static Field<String> Cache = new Field<String>() { ColumnName = "CACHE", Read = reader => reader.Field<String>((int)TableQueryColumns_11.CACHE) };
            public static Field<String> TableLock = new Field<String>() { ColumnName = "TABLE_LOCK", Read = reader => reader.Field<String>((int)TableQueryColumns_11.TABLE_LOCK) };
            public static Field<String> Partitioned = new Field<String>() { ColumnName = "PARTITIONED", Read = reader => reader.Field<String>((int)TableQueryColumns_11.PARTITIONED) };
            public static Field<String> Temporary = new Field<String>() { ColumnName = "TEMPORARY", Read = reader => reader.Field<String>((int)TableQueryColumns_11.TEMPORARY) };
            public static Field<String> Secondary = new Field<String>() { ColumnName = "SECONDARY", Read = reader => reader.Field<String>((int)TableQueryColumns_11.SECONDARY) };
            public static Field<String> Nested = new Field<String>() { ColumnName = "NESTED", Read = reader => reader.Field<String>((int)TableQueryColumns_11.NESTED) };
            public static Field<String> BufferPool = new Field<String>() { ColumnName = "BUFFER_POOL", Read = reader => reader.Field<String>((int)TableQueryColumns_11.BUFFER_POOL) };
            public static Field<String> FlashCache = new Field<String>() { ColumnName = "FLASH_CACHE", Read = reader => reader.Field<String>((int)TableQueryColumns_11.FLASH_CACHE) };
            public static Field<String> CellFlashCache = new Field<String>() { ColumnName = "CELL_FLASH_CACHE", Read = reader => reader.Field<String>((int)TableQueryColumns_11.CELL_FLASH_CACHE) };
            public static Field<String> RowMovement = new Field<String>() { ColumnName = "ROW_MOVEMENT", Read = reader => reader.Field<String>((int)TableQueryColumns_11.ROW_MOVEMENT) };
            public static Field<String> GlobalStats = new Field<String>() { ColumnName = "GLOBAL_STATS", Read = reader => reader.Field<String>((int)TableQueryColumns_11.GLOBAL_STATS) };
            public static Field<String> UserStats = new Field<String>() { ColumnName = "USER_STATS", Read = reader => reader.Field<String>((int)TableQueryColumns_11.USER_STATS) };
            public static Field<String> Duration = new Field<String>() { ColumnName = "DURATION", Read = reader => reader.Field<String>((int)TableQueryColumns_11.DURATION) };
            public static Field<String> SkipCorrupt = new Field<String>() { ColumnName = "SKIP_CORRUPT", Read = reader => reader.Field<String>((int)TableQueryColumns_11.SKIP_CORRUPT) };
            public static Field<String> Monitoring = new Field<String>() { ColumnName = "MONITORING", Read = reader => reader.Field<String>((int)TableQueryColumns_11.MONITORING) };
            public static Field<String> ClusterOwner = new Field<String>() { ColumnName = "CLUSTER_OWNER", Read = reader => reader.Field<String>((int)TableQueryColumns_11.CLUSTER_OWNER) };
            public static Field<String> Dependencies = new Field<String>() { ColumnName = "DEPENDENCIES", Read = reader => reader.Field<String>((int)TableQueryColumns_11.DEPENDENCIES) };
            public static Field<String> Compression = new Field<String>() { ColumnName = "COMPRESSION", Read = reader => reader.Field<String>((int)TableQueryColumns_11.COMPRESSION) };
            public static Field<String> CompressFor = new Field<String>() { ColumnName = "COMPRESS_FOR", Read = reader => reader.Field<String>((int)TableQueryColumns_11.COMPRESS_FOR) };
            public static Field<String> Dropped = new Field<String>() { ColumnName = "DROPPED", Read = reader => reader.Field<String>((int)TableQueryColumns_11.DROPPED) };
            public static Field<String> ReadOnly = new Field<String>() { ColumnName = "READ_ONLY", Read = reader => reader.Field<String>((int)TableQueryColumns_11.READ_ONLY) };
            public static Field<String> SegmentCreated = new Field<String>() { ColumnName = "SEGMENT_CREATED", Read = reader => reader.Field<String>((int)TableQueryColumns_11.SEGMENT_CREATED) };
            public static Field<String> ResultCache = new Field<String>() { ColumnName = "RESULT_CACHE", Read = reader => reader.Field<String>((int)TableQueryColumns_11.RESULT_CACHE) };


        }

        #region Readers

        public override void Read(IDataReader r, TableDto_11 item)
        {
            item.Owner = TableQueryDescriptor_11.Columns.Owner.Read(r);
            item.TableName = TableQueryDescriptor_11.Columns.TableName.Read(r);
            item.TablespaceName = TableQueryDescriptor_11.Columns.TablespaceName.Read(r);
            item.ClusterName = TableQueryDescriptor_11.Columns.ClusterName.Read(r);
            item.Status = TableQueryDescriptor_11.Columns.Status.Read(r);
            item.PctFree = TableQueryDescriptor_11.Columns.PctFree.Read(r);
            item.PctUsed = TableQueryDescriptor_11.Columns.PctUsed.Read(r);
            item.InitialExtent = TableQueryDescriptor_11.Columns.InitialExtent.Read(r);
            item.IniTrans = TableQueryDescriptor_11.Columns.IniTrans.Read(r);
            item.MaxTrans = TableQueryDescriptor_11.Columns.MaxTrans.Read(r);
            item.NextExtent = TableQueryDescriptor_11.Columns.NextExtent.Read(r);
            item.MinExtents = TableQueryDescriptor_11.Columns.MinExtents.Read(r);
            item.Logging = TableQueryDescriptor_11.Columns.Logging.Read(r);
            item.Cache = TableQueryDescriptor_11.Columns.Cache.Read(r);
            item.TableLock = TableQueryDescriptor_11.Columns.TableLock.Read(r);
            item.Partitioned = TableQueryDescriptor_11.Columns.Partitioned.Read(r);
            item.Temporary = TableQueryDescriptor_11.Columns.Temporary.Read(r);
            item.Secondary = TableQueryDescriptor_11.Columns.Secondary.Read(r);
            item.Nested = TableQueryDescriptor_11.Columns.Nested.Read(r);
            item.BufferPool = TableQueryDescriptor_11.Columns.BufferPool.Read(r);
            item.FlashCache = TableQueryDescriptor_11.Columns.FlashCache.Read(r);
            item.CellFlashCache = TableQueryDescriptor_11.Columns.CellFlashCache.Read(r);
            item.RowMovement = TableQueryDescriptor_11.Columns.RowMovement.Read(r);
            item.GlobalStats = TableQueryDescriptor_11.Columns.GlobalStats.Read(r);
            item.UserStats = TableQueryDescriptor_11.Columns.UserStats.Read(r);
            item.Duration = TableQueryDescriptor_11.Columns.Duration.Read(r);
            item.SkipCorrupt = TableQueryDescriptor_11.Columns.SkipCorrupt.Read(r);
            item.Monitoring = TableQueryDescriptor_11.Columns.Monitoring.Read(r);
            item.ClusterOwner = TableQueryDescriptor_11.Columns.ClusterOwner.Read(r);
            item.Dependencies = TableQueryDescriptor_11.Columns.Dependencies.Read(r);
            item.Compression = TableQueryDescriptor_11.Columns.Compression.Read(r);
            item.CompressFor = TableQueryDescriptor_11.Columns.CompressFor.Read(r);
            item.Dropped = TableQueryDescriptor_11.Columns.Dropped.Read(r);
            item.ReadOnly = TableQueryDescriptor_11.Columns.ReadOnly.Read(r);
            item.SegmentCreated = TableQueryDescriptor_11.Columns.SegmentCreated.Read(r);
            item.ResultCache = TableQueryDescriptor_11.Columns.ResultCache.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.TableName;
            yield return Columns.TablespaceName;
            yield return Columns.ClusterName;
            yield return Columns.Status;
            yield return Columns.PctFree;
            yield return Columns.PctUsed;
            yield return Columns.InitialExtent;
            yield return Columns.IniTrans;
            yield return Columns.MaxTrans;
            yield return Columns.NextExtent;
            yield return Columns.MinExtents;
            yield return Columns.Logging;
            yield return Columns.Cache;
            yield return Columns.TableLock;
            yield return Columns.Partitioned;
            yield return Columns.Temporary;
            yield return Columns.Secondary;
            yield return Columns.Nested;
            yield return Columns.BufferPool;
            yield return Columns.FlashCache;
            yield return Columns.CellFlashCache;
            yield return Columns.RowMovement;
            yield return Columns.GlobalStats;
            yield return Columns.UserStats;
            yield return Columns.Duration;
            yield return Columns.SkipCorrupt;
            yield return Columns.Monitoring;
            yield return Columns.ClusterOwner;
            yield return Columns.Dependencies;
            yield return Columns.Compression;
            yield return Columns.CompressFor;
            yield return Columns.Dropped;
            yield return Columns.ReadOnly;
            yield return Columns.SegmentCreated;
            yield return Columns.ResultCache;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<TableDto_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum TableQueryColumns_11
    {
        OWNER,
        TABLE_NAME,
        TABLESPACE_NAME,
        CLUSTER_NAME,
        STATUS,
        PCT_FREE,
        PCT_USED,
        INITIAL_EXTENT,
        INI_TRANS,
        MAX_TRANS,
        NEXT_EXTENT,
        MIN_EXTENTS,
        LOGGING,
        CACHE,
        TABLE_LOCK,
        PARTITIONED,
        TEMPORARY,
        SECONDARY,
        NESTED,
        BUFFER_POOL,
        FLASH_CACHE,
        CELL_FLASH_CACHE,
        ROW_MOVEMENT,
        GLOBAL_STATS,
        USER_STATS,
        DURATION,
        SKIP_CORRUPT,
        MONITORING,
        CLUSTER_OWNER,
        DEPENDENCIES,
        COMPRESSION,
        COMPRESS_FOR,
        DROPPED,
        READ_ONLY,
        SEGMENT_CREATED,
        RESULT_CACHE,

    }

    public class TableDto_11
    {

        public String Owner { get; set; }
        public String TableName { get; set; }
        public String TablespaceName { get; set; }
        public String ClusterName { get; set; }
        public String Status { get; set; }
        public Decimal PctFree { get; set; }
        public Decimal PctUsed { get; set; }
        public Decimal InitialExtent { get; set; }
        public Decimal IniTrans { get; set; }
        public Decimal MaxTrans { get; set; }
        public Decimal NextExtent { get; set; }
        public Decimal MinExtents { get; set; }
        public Decimal Logging { get; set; }
        public String Cache { get; set; }
        public String TableLock { get; set; }
        public String Partitioned { get; set; }
        public String Temporary { get; set; }
        public String Secondary { get; set; }
        public String Nested { get; set; }
        public String BufferPool { get; set; }
        public String FlashCache { get; set; }
        public String CellFlashCache { get; set; }
        public String RowMovement { get; set; }
        public String GlobalStats { get; set; }
        public String UserStats { get; set; }
        public String Duration { get; set; }
        public String SkipCorrupt { get; set; }
        public String Monitoring { get; set; }
        public String ClusterOwner { get; set; }
        public String Dependencies { get; set; }
        public String Compression { get; set; }
        public String CompressFor { get; set; }
        public String Dropped { get; set; }
        public String ReadOnly { get; set; }
        public String SegmentCreated { get; set; }
        public String ResultCache { get; set; }


    }

}

