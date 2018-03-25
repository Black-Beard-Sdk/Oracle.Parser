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

    public class TablePartitionColumnQuery_11 : DbQueryBase<TablePartitionColumnDto_11>
    {

        string sql =
@"


    SELECT * FROM SYS.DBA_PART_TABLES l



{0}

";

        public override List<TablePartitionColumnDto_11> Resolve(DbContextOracle context, Action<TablePartitionColumnDto_11> action)
        {

            List<TablePartitionColumnDto_11> List = new List<TablePartitionColumnDto_11>();
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

            TablePartitionColumnQueryDescriptor_11 TablePartitionColumn = new TablePartitionColumnQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = TablePartitionColumn.ReadAll(reader, action).ToList();
            }

            return List;
        }

    }

    public class TablePartitionColumnQueryDescriptor_11 : StructureDescriptorTable<TablePartitionColumnDto_11>
    {

        public TablePartitionColumnQueryDescriptor_11(string connectionString)
            : base(() => new TablePartitionColumnDto_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.OWNER) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.TABLE_NAME) };
            public static Field<String> PartitioningType = new Field<String>() { ColumnName = "PARTITIONING_TYPE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.PARTITIONING_TYPE) };
            public static Field<String> SubpartitioningType = new Field<String>() { ColumnName = "SUBPARTITIONING_TYPE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.SUBPARTITIONING_TYPE) };
            public static Field<Decimal> PartitionCount = new Field<Decimal>() { ColumnName = "PARTITION_COUNT", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.PARTITION_COUNT) };
            public static Field<Decimal> DefSubpartitionCount = new Field<Decimal>() { ColumnName = "DEF_SUBPARTITION_COUNT", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.DEF_SUBPARTITION_COUNT) };
            public static Field<Decimal> PartitioningKeyCount = new Field<Decimal>() { ColumnName = "PARTITIONING_KEY_COUNT", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.PARTITIONING_KEY_COUNT) };
            public static Field<Decimal> SubpartitioningKeyCount = new Field<Decimal>() { ColumnName = "SUBPARTITIONING_KEY_COUNT", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.SUBPARTITIONING_KEY_COUNT) };
            public static Field<String> Status = new Field<String>() { ColumnName = "STATUS", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.STATUS) };
            public static Field<String> DefTablespaceName = new Field<String>() { ColumnName = "DEF_TABLESPACE_NAME", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_TABLESPACE_NAME) };
            public static Field<Decimal> DefPctFree = new Field<Decimal>() { ColumnName = "DEF_PCT_FREE", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.DEF_PCT_FREE) };
            public static Field<Decimal> DefPctUsed = new Field<Decimal>() { ColumnName = "DEF_PCT_USED", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.DEF_PCT_USED) };
            public static Field<Decimal> DefIniTrans = new Field<Decimal>() { ColumnName = "DEF_INI_TRANS", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.DEF_INI_TRANS) };
            public static Field<Decimal> DefMaxTrans = new Field<Decimal>() { ColumnName = "DEF_MAX_TRANS", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.DEF_MAX_TRANS) };
            public static Field<String> DefInitialExtent = new Field<String>() { ColumnName = "DEF_INITIAL_EXTENT", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_INITIAL_EXTENT) };
            public static Field<String> DefNextExtent = new Field<String>() { ColumnName = "DEF_NEXT_EXTENT", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_NEXT_EXTENT) };
            public static Field<String> DefMinExtents = new Field<String>() { ColumnName = "DEF_MIN_EXTENTS", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_MIN_EXTENTS) };
            public static Field<String> DefMaxExtents = new Field<String>() { ColumnName = "DEF_MAX_EXTENTS", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_MAX_EXTENTS) };
            public static Field<String> DefMaxSize = new Field<String>() { ColumnName = "DEF_MAX_SIZE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_MAX_SIZE) };
            public static Field<String> DefPctIncrease = new Field<String>() { ColumnName = "DEF_PCT_INCREASE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_PCT_INCREASE) };
            public static Field<Decimal> DefFreelists = new Field<Decimal>() { ColumnName = "DEF_FREELISTS", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.DEF_FREELISTS) };
            public static Field<Decimal> DefFreelistGroups = new Field<Decimal>() { ColumnName = "DEF_FREELIST_GROUPS", Read = reader => reader.Field<Decimal>((int)TablePartitionColumnQueryColumns_11.DEF_FREELIST_GROUPS) };
            public static Field<String> DefLogging = new Field<String>() { ColumnName = "DEF_LOGGING", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_LOGGING) };
            public static Field<String> DefCompression = new Field<String>() { ColumnName = "DEF_COMPRESSION", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_COMPRESSION) };
            public static Field<String> DefCompressFor = new Field<String>() { ColumnName = "DEF_COMPRESS_FOR", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_COMPRESS_FOR) };
            public static Field<String> DefBufferPool = new Field<String>() { ColumnName = "DEF_BUFFER_POOL", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_BUFFER_POOL) };
            public static Field<String> DefFlashCache = new Field<String>() { ColumnName = "DEF_FLASH_CACHE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_FLASH_CACHE) };
            public static Field<String> DefCellFlashCache = new Field<String>() { ColumnName = "DEF_CELL_FLASH_CACHE", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_CELL_FLASH_CACHE) };
            public static Field<String> RefPtnConstraintName = new Field<String>() { ColumnName = "REF_PTN_CONSTRAINT_NAME", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.REF_PTN_CONSTRAINT_NAME) };
            public static Field<String> Interval = new Field<String>() { ColumnName = "INTERVAL", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.INTERVAL) };
            public static Field<String> IsNested = new Field<String>() { ColumnName = "IS_NESTED", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.IS_NESTED) };
            public static Field<String> DefSegmentCreation = new Field<String>() { ColumnName = "DEF_SEGMENT_CREATION", Read = reader => reader.Field<String>((int)TablePartitionColumnQueryColumns_11.DEF_SEGMENT_CREATION) };


        }

        #region Readers

        public override void Read(IDataReader r, TablePartitionColumnDto_11 item)
        {
            item.Owner = TablePartitionColumnQueryDescriptor_11.Columns.Owner.Read(r);
            item.TableName = TablePartitionColumnQueryDescriptor_11.Columns.TableName.Read(r);
            item.PartitioningType = TablePartitionColumnQueryDescriptor_11.Columns.PartitioningType.Read(r);
            item.SubpartitioningType = TablePartitionColumnQueryDescriptor_11.Columns.SubpartitioningType.Read(r);
            item.PartitionCount = TablePartitionColumnQueryDescriptor_11.Columns.PartitionCount.Read(r);
            item.DefSubpartitionCount = TablePartitionColumnQueryDescriptor_11.Columns.DefSubpartitionCount.Read(r);
            item.PartitioningKeyCount = TablePartitionColumnQueryDescriptor_11.Columns.PartitioningKeyCount.Read(r);
            item.SubpartitioningKeyCount = TablePartitionColumnQueryDescriptor_11.Columns.SubpartitioningKeyCount.Read(r);
            item.Status = TablePartitionColumnQueryDescriptor_11.Columns.Status.Read(r);
            item.DefTablespaceName = TablePartitionColumnQueryDescriptor_11.Columns.DefTablespaceName.Read(r);
            item.DefPctFree = TablePartitionColumnQueryDescriptor_11.Columns.DefPctFree.Read(r);
            item.DefPctUsed = TablePartitionColumnQueryDescriptor_11.Columns.DefPctUsed.Read(r);
            item.DefIniTrans = TablePartitionColumnQueryDescriptor_11.Columns.DefIniTrans.Read(r);
            item.DefMaxTrans = TablePartitionColumnQueryDescriptor_11.Columns.DefMaxTrans.Read(r);
            item.DefInitialExtent = TablePartitionColumnQueryDescriptor_11.Columns.DefInitialExtent.Read(r);
            item.DefNextExtent = TablePartitionColumnQueryDescriptor_11.Columns.DefNextExtent.Read(r);
            item.DefMinExtents = TablePartitionColumnQueryDescriptor_11.Columns.DefMinExtents.Read(r);
            item.DefMaxExtents = TablePartitionColumnQueryDescriptor_11.Columns.DefMaxExtents.Read(r);
            item.DefMaxSize = TablePartitionColumnQueryDescriptor_11.Columns.DefMaxSize.Read(r);
            item.DefPctIncrease = TablePartitionColumnQueryDescriptor_11.Columns.DefPctIncrease.Read(r);
            item.DefFreelists = TablePartitionColumnQueryDescriptor_11.Columns.DefFreelists.Read(r);
            item.DefFreelistGroups = TablePartitionColumnQueryDescriptor_11.Columns.DefFreelistGroups.Read(r);
            item.DefLogging = TablePartitionColumnQueryDescriptor_11.Columns.DefLogging.Read(r);
            item.DefCompression = TablePartitionColumnQueryDescriptor_11.Columns.DefCompression.Read(r);
            item.DefCompressFor = TablePartitionColumnQueryDescriptor_11.Columns.DefCompressFor.Read(r);
            item.DefBufferPool = TablePartitionColumnQueryDescriptor_11.Columns.DefBufferPool.Read(r);
            item.DefFlashCache = TablePartitionColumnQueryDescriptor_11.Columns.DefFlashCache.Read(r);
            item.DefCellFlashCache = TablePartitionColumnQueryDescriptor_11.Columns.DefCellFlashCache.Read(r);
            item.RefPtnConstraintName = TablePartitionColumnQueryDescriptor_11.Columns.RefPtnConstraintName.Read(r);
            item.Interval = TablePartitionColumnQueryDescriptor_11.Columns.Interval.Read(r);
            item.IsNested = TablePartitionColumnQueryDescriptor_11.Columns.IsNested.Read(r);
            item.DefSegmentCreation = TablePartitionColumnQueryDescriptor_11.Columns.DefSegmentCreation.Read(r);

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

        public override DataTable GetDataTable(string tableName, IEnumerable<TablePartitionColumnDto_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum TablePartitionColumnQueryColumns_11
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

    public class TablePartitionColumnDto_11
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

