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

    public class IndexPartitionColumnQuery : DbQueryBase<IndexPartitionColumnDto>
    {

        string sql =
@"
    SELECT l.* FROM SYS.DBA_PART_INDEXES l
{0}
";

        public override List<IndexPartitionColumnDto> Resolve(DbContextOracle context, Action<IndexPartitionColumnDto> action)
        {

            this.OracleContext = context;
            List<IndexPartitionColumnDto> List = new List<IndexPartitionColumnDto>();
            var db = context.database;

            if (action == null)
                action =
                t =>
                {

                    if (t.TableName.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.Table))
                        return;

                    string k = t.Owner + "." + t.TableName;

                    TableModel table;
                    if (db.Tables.TryGet(k, out table))
                    {

                        string k2 = t.Owner + "." + t.IndexName;

                        IndexModel index;
                        
                        if (!table.Indexes.TryGet(k2, out index))
                        {
                            index = new IndexModel() { Name = k2 };
                            table.Indexes.Add(index);
                        }

                        index.BlocPartition.PartitioningType = t.PartitioningType;
                        index.BlocPartition.SubpartitioningType = t.SubpartitioningType;
                        //index.BlocPartition.PartitionCount = t.PartitionCount;
                        //index.BlocPartition.DefSubpartitionCount = t.DefSubpartitionCount;
                        //index.BlocPartition.PartitioningKeyCount = t.PartitioningKeyCount;
                        //index.BlocPartition.SubpartitioningKeyCount = t.SubpartitioningKeyCount;
                        index.BlocPartition.Locality = t.Locality;
                        index.BlocPartition.Alignment = t.Alignment;
                        index.BlocPartition.DefTablespaceName = t.DefTablespaceName;
                        index.BlocPartition.DefPctFree = t.DefPctFree;
                        index.BlocPartition.DefIniTrans = t.DefIniTrans;
                        index.BlocPartition.DefMaxTrans = t.DefMaxTrans;
                        index.BlocPartition.DefInitialExtent = t.DefInitialExtent;
                        index.BlocPartition.DefNextExtent = t.DefNextExtent;
                        index.BlocPartition.DefMinExtents = t.DefMinExtents;
                        index.BlocPartition.DefMaxExtents = t.DefMaxExtents;
                        index.BlocPartition.DefMaxSize = t.DefMaxSize;
                        index.BlocPartition.DefPctIncrease = t.DefPctIncrease;
                        index.BlocPartition.DefFreelists = t.DefFreelists;
                        index.BlocPartition.DefFreelistGroups = t.DefFreelistGroups;
                        index.BlocPartition.DefLogging = t.DefLogging.ToBoolean();
                        index.BlocPartition.DefBufferPool = t.DefBufferPool;
                        index.BlocPartition.DefFlashCache = t.DefFlashCache;
                        index.BlocPartition.DefCellFlashCache = t.DefCellFlashCache;
                        index.BlocPartition.DefParameters = t.DefParameters;
                        index.BlocPartition.Interval = t.Interval;

                    }


                };

            IndexPartitionColumnQueryDescriptor IndexPartitionColumn = new IndexPartitionColumnQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = IndexPartitionColumn.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class IndexPartitionColumnQueryDescriptor : StructureDescriptorTable<IndexPartitionColumnDto>
    {

        public IndexPartitionColumnQueryDescriptor(string connectionString)
            : base(() => new IndexPartitionColumnDto(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.OWNER) };
            public static Field<String> IndexName = new Field<String>() { ColumnName = "INDEX_NAME", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.INDEX_NAME) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.TABLE_NAME) };
            public static Field<String> PartitioningType = new Field<String>() { ColumnName = "PARTITIONING_TYPE", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.PARTITIONING_TYPE) };
            public static Field<String> SubpartitioningType = new Field<String>() { ColumnName = "SUBPARTITIONING_TYPE", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.SUBPARTITIONING_TYPE) };
            public static Field<Decimal> PartitionCount = new Field<Decimal>() { ColumnName = "PARTITION_COUNT", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.PARTITION_COUNT) };
            public static Field<Decimal> DefSubpartitionCount = new Field<Decimal>() { ColumnName = "DEF_SUBPARTITION_COUNT", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.DEF_SUBPARTITION_COUNT) };
            public static Field<Decimal> PartitioningKeyCount = new Field<Decimal>() { ColumnName = "PARTITIONING_KEY_COUNT", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.PARTITIONING_KEY_COUNT) };
            public static Field<Decimal> SubpartitioningKeyCount = new Field<Decimal>() { ColumnName = "SUBPARTITIONING_KEY_COUNT", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.SUBPARTITIONING_KEY_COUNT) };
            public static Field<String> Locality = new Field<String>() { ColumnName = "LOCALITY", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.LOCALITY) };
            public static Field<String> Alignment = new Field<String>() { ColumnName = "ALIGNMENT", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.ALIGNMENT) };
            public static Field<String> DefTablespaceName = new Field<String>() { ColumnName = "DEF_TABLESPACE_NAME", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_TABLESPACE_NAME) };
            public static Field<Decimal> DefPctFree = new Field<Decimal>() { ColumnName = "DEF_PCT_FREE", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.DEF_PCT_FREE) };
            public static Field<Decimal> DefIniTrans = new Field<Decimal>() { ColumnName = "DEF_INI_TRANS", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.DEF_INI_TRANS) };
            public static Field<Decimal> DefMaxTrans = new Field<Decimal>() { ColumnName = "DEF_MAX_TRANS", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.DEF_MAX_TRANS) };
            public static Field<String> DefInitialExtent = new Field<String>() { ColumnName = "DEF_INITIAL_EXTENT", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_INITIAL_EXTENT) };
            public static Field<String> DefNextExtent = new Field<String>() { ColumnName = "DEF_NEXT_EXTENT", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_NEXT_EXTENT) };
            public static Field<String> DefMinExtents = new Field<String>() { ColumnName = "DEF_MIN_EXTENTS", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_MIN_EXTENTS) };
            public static Field<String> DefMaxExtents = new Field<String>() { ColumnName = "DEF_MAX_EXTENTS", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_MAX_EXTENTS) };
            public static Field<String> DefMaxSize = new Field<String>() { ColumnName = "DEF_MAX_SIZE", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_MAX_SIZE) };
            public static Field<String> DefPctIncrease = new Field<String>() { ColumnName = "DEF_PCT_INCREASE", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_PCT_INCREASE) };
            public static Field<Decimal> DefFreelists = new Field<Decimal>() { ColumnName = "DEF_FREELISTS", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.DEF_FREELISTS) };
            public static Field<Decimal> DefFreelistGroups = new Field<Decimal>() { ColumnName = "DEF_FREELIST_GROUPS", Read = reader => reader.Field<Decimal>((int)IndexPartitionColumnQueryColumns.DEF_FREELIST_GROUPS) };
            public static Field<String> DefLogging = new Field<String>() { ColumnName = "DEF_LOGGING", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_LOGGING) };
            public static Field<String> DefBufferPool = new Field<String>() { ColumnName = "DEF_BUFFER_POOL", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_BUFFER_POOL) };
            public static Field<String> DefFlashCache = new Field<String>() { ColumnName = "DEF_FLASH_CACHE", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_FLASH_CACHE) };
            public static Field<String> DefCellFlashCache = new Field<String>() { ColumnName = "DEF_CELL_FLASH_CACHE", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_CELL_FLASH_CACHE) };
            public static Field<String> DefParameters = new Field<String>() { ColumnName = "DEF_PARAMETERS", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.DEF_PARAMETERS) };
            public static Field<String> Interval = new Field<String>() { ColumnName = "INTERVAL", Read = reader => reader.Field<String>((int)IndexPartitionColumnQueryColumns.INTERVAL) };

            
        }

        #region Readers

        public override void Read(IDataReader r, IndexPartitionColumnDto item)
        {
            item.Owner = IndexPartitionColumnQueryDescriptor.Columns.Owner.Read(r);
            item.IndexName = IndexPartitionColumnQueryDescriptor.Columns.IndexName.Read(r);
            item.TableName = IndexPartitionColumnQueryDescriptor.Columns.TableName.Read(r);
            item.PartitioningType = IndexPartitionColumnQueryDescriptor.Columns.PartitioningType.Read(r);
            item.SubpartitioningType = IndexPartitionColumnQueryDescriptor.Columns.SubpartitioningType.Read(r);
            item.PartitionCount = IndexPartitionColumnQueryDescriptor.Columns.PartitionCount.Read(r);
            item.DefSubpartitionCount = IndexPartitionColumnQueryDescriptor.Columns.DefSubpartitionCount.Read(r);
            item.PartitioningKeyCount = IndexPartitionColumnQueryDescriptor.Columns.PartitioningKeyCount.Read(r);
            item.SubpartitioningKeyCount = IndexPartitionColumnQueryDescriptor.Columns.SubpartitioningKeyCount.Read(r);
            item.Locality = IndexPartitionColumnQueryDescriptor.Columns.Locality.Read(r);
            item.Alignment = IndexPartitionColumnQueryDescriptor.Columns.Alignment.Read(r);
            item.DefTablespaceName = IndexPartitionColumnQueryDescriptor.Columns.DefTablespaceName.Read(r);
            item.DefPctFree = IndexPartitionColumnQueryDescriptor.Columns.DefPctFree.Read(r);
            item.DefIniTrans = IndexPartitionColumnQueryDescriptor.Columns.DefIniTrans.Read(r);
            item.DefMaxTrans = IndexPartitionColumnQueryDescriptor.Columns.DefMaxTrans.Read(r);
            item.DefInitialExtent = IndexPartitionColumnQueryDescriptor.Columns.DefInitialExtent.Read(r);
            item.DefNextExtent = IndexPartitionColumnQueryDescriptor.Columns.DefNextExtent.Read(r);
            item.DefMinExtents = IndexPartitionColumnQueryDescriptor.Columns.DefMinExtents.Read(r);
            item.DefMaxExtents = IndexPartitionColumnQueryDescriptor.Columns.DefMaxExtents.Read(r);
            item.DefMaxSize = IndexPartitionColumnQueryDescriptor.Columns.DefMaxSize.Read(r);
            item.DefPctIncrease = IndexPartitionColumnQueryDescriptor.Columns.DefPctIncrease.Read(r);
            item.DefFreelists = IndexPartitionColumnQueryDescriptor.Columns.DefFreelists.Read(r);
            item.DefFreelistGroups = IndexPartitionColumnQueryDescriptor.Columns.DefFreelistGroups.Read(r);
            item.DefLogging = IndexPartitionColumnQueryDescriptor.Columns.DefLogging.Read(r);
            item.DefBufferPool = IndexPartitionColumnQueryDescriptor.Columns.DefBufferPool.Read(r);
            item.DefFlashCache = IndexPartitionColumnQueryDescriptor.Columns.DefFlashCache.Read(r);
            item.DefCellFlashCache = IndexPartitionColumnQueryDescriptor.Columns.DefCellFlashCache.Read(r);
            item.DefParameters = IndexPartitionColumnQueryDescriptor.Columns.DefParameters.Read(r);
            item.Interval = IndexPartitionColumnQueryDescriptor.Columns.Interval.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
                     yield return Columns.Owner;
         yield return Columns.IndexName;
         yield return Columns.TableName;
         yield return Columns.PartitioningType;
         yield return Columns.SubpartitioningType;
         yield return Columns.PartitionCount;
         yield return Columns.DefSubpartitionCount;
         yield return Columns.PartitioningKeyCount;
         yield return Columns.SubpartitioningKeyCount;
         yield return Columns.Locality;
         yield return Columns.Alignment;
         yield return Columns.DefTablespaceName;
         yield return Columns.DefPctFree;
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
         yield return Columns.DefBufferPool;
         yield return Columns.DefFlashCache;
         yield return Columns.DefCellFlashCache;
         yield return Columns.DefParameters;
         yield return Columns.Interval;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<IndexPartitionColumnDto> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum IndexPartitionColumnQueryColumns
    {
              OWNER,
      INDEX_NAME,
      TABLE_NAME,
      PARTITIONING_TYPE,
      SUBPARTITIONING_TYPE,
      PARTITION_COUNT,
      DEF_SUBPARTITION_COUNT,
      PARTITIONING_KEY_COUNT,
      SUBPARTITIONING_KEY_COUNT,
      LOCALITY,
      ALIGNMENT,
      DEF_TABLESPACE_NAME,
      DEF_PCT_FREE,
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
      DEF_BUFFER_POOL,
      DEF_FLASH_CACHE,
      DEF_CELL_FLASH_CACHE,
      DEF_PARAMETERS,
      INTERVAL,

    }

    public class IndexPartitionColumnDto
    {

        public String Owner { get; set; }
        public String IndexName { get; set; }
        public String TableName { get; set; }
        public String PartitioningType { get; set; }
        public String SubpartitioningType { get; set; }
        public Decimal PartitionCount { get; set; }
        public Decimal DefSubpartitionCount { get; set; }
        public Decimal PartitioningKeyCount { get; set; }
        public Decimal SubpartitioningKeyCount { get; set; }
        public String Locality { get; set; }
        public String Alignment { get; set; }
        public String DefTablespaceName { get; set; }
        public Decimal DefPctFree { get; set; }
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
        public String DefBufferPool { get; set; }
        public String DefFlashCache { get; set; }
        public String DefCellFlashCache { get; set; }
        public String DefParameters { get; set; }
        public String Interval { get; set; }


    }

}

