using Bb.Beard.Oracle.Reader;
using Bb.Beard.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class TablePartitionColumnQuery : DbQueryBase<TablePartitionColumnDto>
    {

        string sql =
@"


    SELECT * FROM SYS.DBA_PART_TABLES l



{0}

";

        public override List<TablePartitionColumnDto> Resolve(DbContextOracle context, Action<TablePartitionColumnDto> action)
        {

            List<TablePartitionColumnDto> List = new List<TablePartitionColumnDto>();
            this.OracleContext = context;
            var db = context.database;

            if (action == null)
                action =
                    t =>
                    {

                        string k = t.Owner + "." + t.TableName;

                        if (t.TableName.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.Table))
                            return;

                        TableModel table;
                        if (db.Tables.TryGet(k, out table))
                        {

                            table.BlocPartition.PartitioningType = t.PartitioningType;
                            table.BlocPartition.SubpartitioningType = t.SubpartitioningType;
                        //table.BlocPartition.PartitionCount = t.PartitionCount;
                        //table.BlocPartition.DefSubpartitionCount = t.DefSubpartitionCount;
                        //table.BlocPartition.PartitioningKeyCount = t.PartitioningKeyCount;
                        //table.BlocPartition.SubpartitioningKeyCount = t.SubpartitioningKeyCount;
                        table.BlocPartition.Status = t.Status;
                            table.BlocPartition.DefTablespaceName = t.DefTablespaceName;
                            table.BlocPartition.DefPctFree = t.DefPctFree;
                            table.BlocPartition.DefPctUsed = t.DefPctUsed;
                            table.BlocPartition.DefIniTrans = t.DefIniTrans;
                            table.BlocPartition.DefMaxTrans = t.DefMaxTrans;
                            table.BlocPartition.DefInitialExtent = t.DefInitialExtent;
                            table.BlocPartition.DefNextExtent = t.DefNextExtent;
                            table.BlocPartition.DefMinExtents = t.DefMinExtents;
                            table.BlocPartition.DefMaxExtents = t.DefMaxExtents;
                            table.BlocPartition.DefMaxSize = t.DefMaxSize;
                            table.BlocPartition.DefPctIncrease = t.DefPctIncrease;
                            table.BlocPartition.DefFreelists = t.DefFreelists;
                            table.BlocPartition.DefFreelistGroups = t.DefFreelistGroups;
                            table.BlocPartition.DefLogging = t.DefLogging.ToBoolean();
                            table.BlocPartition.DefCompression = t.DefCompression;
                            table.BlocPartition.DefCompressFor = t.DefCompressFor;
                            table.BlocPartition.DefBufferPool = t.DefBufferPool;
                            table.BlocPartition.DefFlashCache = t.DefFlashCache;
                            table.BlocPartition.DefCellFlashCache = t.DefCellFlashCache;
                            table.BlocPartition.RefPtnConstraintName = t.RefPtnConstraintName;
                            table.BlocPartition.Interval = t.Interval;
                            table.BlocPartition.IsNested = t.IsNested.ToBoolean();
                            table.BlocPartition.DefSegmentCreation = t.DefSegmentCreation;


                        }

                    };

            TablePartitionColumnQueryDescriptor TablePartitionColumn = new TablePartitionColumnQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = TablePartitionColumn.ReadAll(reader, action).ToList();
            }

            return List;
        }

    }

    public class TablePartitionColumnQueryDescriptor : StructureDescriptorTable<TablePartitionColumnDto>
    {

        public TablePartitionColumnQueryDescriptor(string connectionString)
            : base(() => new TablePartitionColumnDto(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.OWNER) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.TABLE_NAME) };
            public static Field<String> PartitioningType = new Field<String>() { ColumnName = "PARTITIONING_TYPE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.PARTITIONING_TYPE) };
            public static Field<String> SubpartitioningType = new Field<String>() { ColumnName = "SUBPARTITIONING_TYPE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.SUBPARTITIONING_TYPE) };
            public static Field<Decimal> PartitionCount = new Field<Decimal>() { ColumnName = "PARTITION_COUNT", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.PARTITION_COUNT) };
            public static Field<Decimal> DefSubpartitionCount = new Field<Decimal>() { ColumnName = "DEF_SUBPARTITION_COUNT", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.DEF_SUBPARTITION_COUNT) };
            public static Field<Decimal> PartitioningKeyCount = new Field<Decimal>() { ColumnName = "PARTITIONING_KEY_COUNT", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.PARTITIONING_KEY_COUNT) };
            public static Field<Decimal> SubpartitioningKeyCount = new Field<Decimal>() { ColumnName = "SUBPARTITIONING_KEY_COUNT", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.SUBPARTITIONING_KEY_COUNT) };
            public static Field<String> Status = new Field<String>() { ColumnName = "STATUS", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.STATUS) };
            public static Field<String> DefTablespaceName = new Field<String>() { ColumnName = "DEF_TABLESPACE_NAME", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_TABLESPACE_NAME) };
            public static Field<Decimal> DefPctFree = new Field<Decimal>() { ColumnName = "DEF_PCT_FREE", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.DEF_PCT_FREE) };
            public static Field<Decimal> DefPctUsed = new Field<Decimal>() { ColumnName = "DEF_PCT_USED", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.DEF_PCT_USED) };
            public static Field<Decimal> DefIniTrans = new Field<Decimal>() { ColumnName = "DEF_INI_TRANS", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.DEF_INI_TRANS) };
            public static Field<Decimal> DefMaxTrans = new Field<Decimal>() { ColumnName = "DEF_MAX_TRANS", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.DEF_MAX_TRANS) };
            public static Field<String> DefInitialExtent = new Field<String>() { ColumnName = "DEF_INITIAL_EXTENT", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_INITIAL_EXTENT) };
            public static Field<String> DefNextExtent = new Field<String>() { ColumnName = "DEF_NEXT_EXTENT", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_NEXT_EXTENT) };
            public static Field<String> DefMinExtents = new Field<String>() { ColumnName = "DEF_MIN_EXTENTS", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_MIN_EXTENTS) };
            public static Field<String> DefMaxExtents = new Field<String>() { ColumnName = "DEF_MAX_EXTENTS", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_MAX_EXTENTS) };
            public static Field<String> DefMaxSize = new Field<String>() { ColumnName = "DEF_MAX_SIZE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_MAX_SIZE) };
            public static Field<String> DefPctIncrease = new Field<String>() { ColumnName = "DEF_PCT_INCREASE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_PCT_INCREASE) };
            public static Field<Decimal> DefFreelists = new Field<Decimal>() { ColumnName = "DEF_FREELISTS", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.DEF_FREELISTS) };
            public static Field<Decimal> DefFreelistGroups = new Field<Decimal>() { ColumnName = "DEF_FREELIST_GROUPS", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns.DEF_FREELIST_GROUPS) };
            public static Field<String> DefLogging = new Field<String>() { ColumnName = "DEF_LOGGING", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_LOGGING) };
            public static Field<String> DefCompression = new Field<String>() { ColumnName = "DEF_COMPRESSION", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_COMPRESSION) };
            public static Field<String> DefCompressFor = new Field<String>() { ColumnName = "DEF_COMPRESS_FOR", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_COMPRESS_FOR) };
            public static Field<String> DefBufferPool = new Field<String>() { ColumnName = "DEF_BUFFER_POOL", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_BUFFER_POOL) };
            public static Field<String> DefFlashCache = new Field<String>() { ColumnName = "DEF_FLASH_CACHE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_FLASH_CACHE) };
            public static Field<String> DefCellFlashCache = new Field<String>() { ColumnName = "DEF_CELL_FLASH_CACHE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_CELL_FLASH_CACHE) };
            public static Field<String> RefPtnConstraintName = new Field<String>() { ColumnName = "REF_PTN_CONSTRAINT_NAME", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.REF_PTN_CONSTRAINT_NAME) };
            public static Field<String> Interval = new Field<String>() { ColumnName = "INTERVAL", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.INTERVAL) };
            public static Field<String> IsNested = new Field<String>() { ColumnName = "IS_NESTED", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.IS_NESTED) };
            public static Field<String> DefSegmentCreation = new Field<String>() { ColumnName = "DEF_SEGMENT_CREATION", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns.DEF_SEGMENT_CREATION) };


        }

        #region Readers

        public override void Read(IDataReader r, TablePartitionColumnDto item)
        {
            item.Owner = TablePartitionColumnQueryDescriptor.Columns.Owner.Read(r);
            item.TableName = TablePartitionColumnQueryDescriptor.Columns.TableName.Read(r);
            item.PartitioningType = TablePartitionColumnQueryDescriptor.Columns.PartitioningType.Read(r);
            item.SubpartitioningType = TablePartitionColumnQueryDescriptor.Columns.SubpartitioningType.Read(r);
            item.PartitionCount = TablePartitionColumnQueryDescriptor.Columns.PartitionCount.Read(r);
            item.DefSubpartitionCount = TablePartitionColumnQueryDescriptor.Columns.DefSubpartitionCount.Read(r);
            item.PartitioningKeyCount = TablePartitionColumnQueryDescriptor.Columns.PartitioningKeyCount.Read(r);
            item.SubpartitioningKeyCount = TablePartitionColumnQueryDescriptor.Columns.SubpartitioningKeyCount.Read(r);
            item.Status = TablePartitionColumnQueryDescriptor.Columns.Status.Read(r);
            item.DefTablespaceName = TablePartitionColumnQueryDescriptor.Columns.DefTablespaceName.Read(r);
            item.DefPctFree = TablePartitionColumnQueryDescriptor.Columns.DefPctFree.Read(r);
            item.DefPctUsed = TablePartitionColumnQueryDescriptor.Columns.DefPctUsed.Read(r);
            item.DefIniTrans = TablePartitionColumnQueryDescriptor.Columns.DefIniTrans.Read(r);
            item.DefMaxTrans = TablePartitionColumnQueryDescriptor.Columns.DefMaxTrans.Read(r);
            item.DefInitialExtent = TablePartitionColumnQueryDescriptor.Columns.DefInitialExtent.Read(r);
            item.DefNextExtent = TablePartitionColumnQueryDescriptor.Columns.DefNextExtent.Read(r);
            item.DefMinExtents = TablePartitionColumnQueryDescriptor.Columns.DefMinExtents.Read(r);
            item.DefMaxExtents = TablePartitionColumnQueryDescriptor.Columns.DefMaxExtents.Read(r);
            item.DefMaxSize = TablePartitionColumnQueryDescriptor.Columns.DefMaxSize.Read(r);
            item.DefPctIncrease = TablePartitionColumnQueryDescriptor.Columns.DefPctIncrease.Read(r);
            item.DefFreelists = TablePartitionColumnQueryDescriptor.Columns.DefFreelists.Read(r);
            item.DefFreelistGroups = TablePartitionColumnQueryDescriptor.Columns.DefFreelistGroups.Read(r);
            item.DefLogging = TablePartitionColumnQueryDescriptor.Columns.DefLogging.Read(r);
            item.DefCompression = TablePartitionColumnQueryDescriptor.Columns.DefCompression.Read(r);
            item.DefCompressFor = TablePartitionColumnQueryDescriptor.Columns.DefCompressFor.Read(r);
            item.DefBufferPool = TablePartitionColumnQueryDescriptor.Columns.DefBufferPool.Read(r);
            item.DefFlashCache = TablePartitionColumnQueryDescriptor.Columns.DefFlashCache.Read(r);
            item.DefCellFlashCache = TablePartitionColumnQueryDescriptor.Columns.DefCellFlashCache.Read(r);
            item.RefPtnConstraintName = TablePartitionColumnQueryDescriptor.Columns.RefPtnConstraintName.Read(r);
            item.Interval = TablePartitionColumnQueryDescriptor.Columns.Interval.Read(r);
            item.IsNested = TablePartitionColumnQueryDescriptor.Columns.IsNested.Read(r);
            item.DefSegmentCreation = TablePartitionColumnQueryDescriptor.Columns.DefSegmentCreation.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.TableName;
            yield return Columns.PartitioningType;
            yield return Columns.SubpartitioningType;
            yield return Columns.PartitionCount;
            yield return Columns.DefSubpartitionCount;
            yield return Columns.PartitioningKeyCount;
            yield return Columns.SubpartitioningKeyCount;
            yield return Columns.Status;
            yield return Columns.DefTablespaceName;
            yield return Columns.DefPctFree;
            yield return Columns.DefPctUsed;
            yield return Columns.DefIniTrans;
            yield return Columns.DefMaxTrans;
            yield return Columns.DefInitialExtent;
            yield return Columns.DefNextExtent;
            yield return Columns.DefMinExtents;
            yield return Columns.DefMaxExtents;
            yield return Columns.DefMaxSize;
            yield return Columns.DefPctIncrease;
            yield return Columns.DefFreelists;
            yield return Columns.DefFreelistGroups;
            yield return Columns.DefLogging;
            yield return Columns.DefCompression;
            yield return Columns.DefCompressFor;
            yield return Columns.DefBufferPool;
            yield return Columns.DefFlashCache;
            yield return Columns.DefCellFlashCache;
            yield return Columns.RefPtnConstraintName;
            yield return Columns.Interval;
            yield return Columns.IsNested;
            yield return Columns.DefSegmentCreation;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<TablePartitionColumnDto> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum TablePartitionColumnQueryColumns
    {
        OWNER,
        TABLE_NAME,
        PARTITIONING_TYPE,
        SUBPARTITIONING_TYPE,
        PARTITION_COUNT,
        DEF_SUBPARTITION_COUNT,
        PARTITIONING_KEY_COUNT,
        SUBPARTITIONING_KEY_COUNT,
        STATUS,
        DEF_TABLESPACE_NAME,
        DEF_PCT_FREE,
        DEF_PCT_USED,
        DEF_INI_TRANS,
        DEF_MAX_TRANS,
        DEF_INITIAL_EXTENT,
        DEF_NEXT_EXTENT,
        DEF_MIN_EXTENTS,
        DEF_MAX_EXTENTS,
        DEF_MAX_SIZE,
        DEF_PCT_INCREASE,
        DEF_FREELISTS,
        DEF_FREELIST_GROUPS,
        DEF_LOGGING,
        DEF_COMPRESSION,
        DEF_COMPRESS_FOR,
        DEF_BUFFER_POOL,
        DEF_FLASH_CACHE,
        DEF_CELL_FLASH_CACHE,
        REF_PTN_CONSTRAINT_NAME,
        INTERVAL,
        IS_NESTED,
        DEF_SEGMENT_CREATION,

    }

    public class TablePartitionColumnDto
    {

        public String Owner { get; set; }
        public String TableName { get; set; }
        public String PartitioningType { get; set; }
        public String SubpartitioningType { get; set; }
        public Decimal PartitionCount { get; set; }
        public Decimal DefSubpartitionCount { get; set; }
        public Decimal PartitioningKeyCount { get; set; }
        public Decimal SubpartitioningKeyCount { get; set; }
        public String Status { get; set; }
        public String DefTablespaceName { get; set; }
        public Decimal DefPctFree { get; set; }
        public Decimal DefPctUsed { get; set; }
        public Decimal DefIniTrans { get; set; }
        public Decimal DefMaxTrans { get; set; }
        public String DefInitialExtent { get; set; }
        public String DefNextExtent { get; set; }
        public String DefMinExtents { get; set; }
        public String DefMaxExtents { get; set; }
        public String DefMaxSize { get; set; }
        public String DefPctIncrease { get; set; }
        public Decimal DefFreelists { get; set; }
        public Decimal DefFreelistGroups { get; set; }
        public String DefLogging { get; set; }
        public String DefCompression { get; set; }
        public String DefCompressFor { get; set; }
        public String DefBufferPool { get; set; }
        public String DefFlashCache { get; set; }
        public String DefCellFlashCache { get; set; }
        public String RefPtnConstraintName { get; set; }
        public String Interval { get; set; }
        public String IsNested { get; set; }
        public String DefSegmentCreation { get; set; }


    }

}

