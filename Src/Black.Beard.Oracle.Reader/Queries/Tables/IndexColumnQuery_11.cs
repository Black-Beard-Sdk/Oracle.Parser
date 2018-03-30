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

    public class IndexColumnQuery_11 : DbQueryBase<IndexColumnQueryTable_11>
    {

        string sql =
@"
SELECT
    c.INDEX_OWNER,
    i.index_name,
    i.table_owner,
    i.table_name,
    i.table_type,
    i.uniqueness,
    i.ityp_owner AS indextype_owner,
    i.ityp_name AS indextype_name,
    i.status,
    i.parameters,
    c.table_owner AS column_table_owner,
    c.table_name AS column_table_name,
    c.column_name,
    c.descend,
    COALESCE(i.tablespace_name, pi.def_tablespace_name) AS tablespace_name,
    COALESCE(i.pct_free, pi.def_pct_free) AS pct_free,
    COALESCE(i.ini_trans, pi.def_ini_trans) AS ini_trans,
    COALESCE(i.max_trans, pi.def_max_trans) AS max_trans,
    COALESCE(CAST(i.initial_extent AS VARCHAR2(40)), pi.def_initial_extent) AS initial_extent,
    COALESCE(CAST(i.next_extent AS VARCHAR2(40)), pi.def_next_extent) AS next_extent,
    COALESCE(CAST(i.min_extents AS VARCHAR2(40)), pi.def_min_extents) AS min_extents,
    COALESCE(CAST(i.max_extents AS VARCHAR2(40)), pi.def_max_extents) AS max_extents,
    COALESCE(CAST(i.pct_increase AS VARCHAR2(40)), pi.def_pct_increase) AS pct_increase,
    COALESCE(i.freelists, pi.def_freelists) AS freelists,
    COALESCE(i.freelist_groups, pi.def_freelist_groups) AS freelist_groups,
    COALESCE(i.buffer_pool, pi.def_buffer_pool) AS buffer_pool,
    i.logging,
    i.pct_threshold,
    i.include_column AS last_key_col,
    i.compression,
    i.prefix_length AS compression_prefix,
    i.degree,
    i.instances,
    i.generated,
    pi.interval,
    i.visibility,
    pi.partitioning_type,
    pi.subpartitioning_type,
--    pi.partition_count,
--    pi.def_subpartition_count,
    pi.interval,
    pi.locality,
    ie.column_expression
FROM
dba_indexes i
INNER JOIN dba_ind_columns  c   ON i.owner = c.index_owner AND i.index_name = c.index_name
LEFT JOIN dba_part_indexes  pi  ON i.owner = pi.owner AND i.index_name = pi.index_name
LEFT JOIN dba_ind_expressions        ie ON ie.index_owner = c.index_owner AND ie.index_name = c.index_name and ie.column_position = c.column_position

{0}

ORDER BY i.table_owner, i.table_name, c.INDEX_OWNER, i.index_name, c.column_position

";

        public override List<IndexColumnQueryTable_11> Resolve(DbContextOracle context, Action<IndexColumnQueryTable_11> action)
        {

            List<IndexColumnQueryTable_11> List = new List<IndexColumnQueryTable_11>();
            this.OracleContext = context;
            var db = context.Database;

            if (action == null)
                action =
                    t =>
                    {

                        if (t.Column_Table_name.ExcludIfStartwith(t.Column_Table_Owner, Models.Configurations.ExcludeKindEnum.Table))
                            return;

                        string key = t.Column_Table_Owner + "." + t.Column_Table_name;
                        var key2 = t.Index_Owner + "." + t.Index_Name;
                        IndexModel index;

                        if (!db.Indexes.Contains(key2))
                        {

                            index = new IndexModel()
                            {
                                Key = key2,
                                Name = t.Index_Name,
                                IndexOwner = t.Index_Owner,
                                IndexType = t.Index_Type,
                                BufferPool = t.buffer_pool,
                                Unique = t.uniqueness == "UNIQUE",
                                // Cache = t.Cache,
                                // Chunk = t.Chunk,
                                Compress = t.Compression,
                                Compression_Prefix = "",
                                // Deduplication = t.Deduplication,
                                FreeListGroups = t.freelist_groups,
                                FreeLists = t.freelists,
                                // FreePools = t.Freepools,
                                // In_Row = t.In_row,
                                InitialExtent = t.initial_extent,
                                Logging = t.Logging,
                                MaxExtents = t.max_extents,
                                MinExtents = t.min_extents,
                                NextExtents = t.next_extent,
                                // PctIncrease = t.Pct_increase,
                                // PctVersion = t.Pctversion,
                                // SecureFile = t.Securefile,
                                // SegmentName = t.Segment_name,
                                // InitialExtent = t.initial_extent,
                                Bitmap = false,
                            };

                            index.TableReference.Owner = t.Column_Table_Owner;
                            index.TableReference.Name = t.Column_Table_name;
                            index.Tablespace.Name = t.tablespace_name;

                            db.Indexes.Add(index);

                        }
                        else
                            index = db.Indexes[key2];

                        IndexColumnModel col1 = new IndexColumnModel() { Name = t.Column_Name, Rule = t.column_expression ?? t.Column_Name, Asc = t.Descend == "ASC" };
                        index.Columns.Add(col1);

                    };

            IndexColumnQueryDescriptor_11 IndexColumn = new IndexColumnQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("i"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = IndexColumn.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class IndexColumnQueryDescriptor_11 : StructureDescriptorTable<IndexColumnQueryTable_11>
    {

        public IndexColumnQueryDescriptor_11(string connectionString)
            : base(() => new IndexColumnQueryTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Index_Owner = new Field<string>() { ColumnName = "INDEX_OWNER", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Index_Owner) };
            public static Field<string> Index_Name = new Field<string>() { ColumnName = "INDEX_NAME", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Index_Name) };
            public static Field<string> Table_owner = new Field<string>() { ColumnName = "TABLE_OWNER", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Table_owner) };
            public static Field<string> Table_name = new Field<string>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Table_name) };
            public static Field<string> Table_Type = new Field<string>() { ColumnName = "TABLE_TYPE", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Table_Type) };
            public static Field<string> uniqueness = new Field<string>() { ColumnName = "UNIQUENESS", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.uniqueness) };
            public static Field<string> indextype_owner = new Field<string>() { ColumnName = "INDEXTYPE_OWNER", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.indextype_owner) };
            public static Field<string> indextype_name = new Field<string>() { ColumnName = "INDEXTYPE_NAME", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.indextype_name) };
            public static Field<string> status = new Field<string>() { ColumnName = "STATUS", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.status) };
            public static Field<string> parameters = new Field<string>() { ColumnName = "PARAMETERS", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.parameters) };
            public static Field<string> Column_Table_Owner = new Field<string>() { ColumnName = "COLUMN_TABLE_OWNER", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Column_Table_Owner) };
            public static Field<string> Column_Table_name = new Field<string>() { ColumnName = "COLUMN_TABLE_NAME", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Column_Table_name) };
            public static Field<string> Column_Name = new Field<string>() { ColumnName = "COLUMN_NAME", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Column_Name) };
            public static Field<string> Descend = new Field<string>() { ColumnName = "DESCEND", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Descend) };
            public static Field<string> tablespace_name = new Field<string>() { ColumnName = "TABLESPACE_NAME", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.tablespace_name) };
            public static Field<string> pct_free = new Field<string>() { ColumnName = "PCT_FREE", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.pct_free) };
            public static Field<string> ini_trans = new Field<string>() { ColumnName = "INI_TRANS", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.ini_trans) };
            public static Field<string> max_trans = new Field<string>() { ColumnName = "MAX_TRANS", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.max_trans) };
            public static Field<string> initial_extent = new Field<string>() { ColumnName = "INITIAL_EXTENT", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.initial_extent) };
            public static Field<string> next_extent = new Field<string>() { ColumnName = "NEXT_EXTENT", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.next_extent) };
            public static Field<string> min_extents = new Field<string>() { ColumnName = "MIN_EXTENTS", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.min_extents) };
            public static Field<string> max_extents = new Field<string>() { ColumnName = "MAX_EXTENTS", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.max_extents) };
            public static Field<string> pct_increase = new Field<string>() { ColumnName = "PCT_INCREASE", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.pct_increase) };
            public static Field<string> freelists = new Field<string>() { ColumnName = "FREELISTS", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.freelists) };
            public static Field<Decimal> freelist_groups = new Field<Decimal>() { ColumnName = "FREELIST_GROUPS", Read = reader => reader.Field<Decimal>((int)IndexColumnQueryColumns_11.freelist_groups) };
            public static Field<string> buffer_pool = new Field<string>() { ColumnName = "BUFFER_POOL", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.buffer_pool) };
            public static Field<string> logging = new Field<string>() { ColumnName = "LOGGING", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.logging) };
            public static Field<string> pct_threshold = new Field<string>() { ColumnName = "PCT_THRESHOLD", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.pct_threshold) };
            public static Field<string> last_key_col = new Field<string>() { ColumnName = "LAST_KEY_COL", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.last_key_col) };
            public static Field<string> compression = new Field<string>() { ColumnName = "COMPRESSION", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.Compression) };
            public static Field<string> compression_prefix = new Field<string>() { ColumnName = "COMPRESSION_PREFIX", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.compression_prefix) };
            public static Field<string> degree = new Field<string>() { ColumnName = "DEGREE", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.degree) };
            public static Field<string> instances = new Field<string>() { ColumnName = "INSTANCES", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.instances) };
            public static Field<string> generated = new Field<string>() { ColumnName = "GENERATED", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.generated) };
            public static Field<string> Interval = new Field<string>() { ColumnName = "INTERVAL", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.interval) };
            public static Field<string> visibility = new Field<string>() { ColumnName = "VISIBILITY", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.visibility) };
            public static Field<string> partitioning_type = new Field<string>() { ColumnName = "PARTITIONING_TYPE", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.partitioning_type) };
            public static Field<string> subpartitioning_type = new Field<string>() { ColumnName = "SUBPARTITIONING_TYPE", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.subpartitioning_type) };
            public static Field<string> interval = new Field<string>() { ColumnName = "INTERVAL", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.interval) };
            public static Field<string> locality = new Field<string>() { ColumnName = "LOCALITY", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.locality) };
            public static Field<string> column_expression = new Field<string>() { ColumnName = "COLUMN_EXPRESSION", Read = reader => reader.Field<string>((int)IndexColumnQueryColumns_11.column_expression) };

        }

        #region Readers

        public override void Read(IDataReader r, IndexColumnQueryTable_11 item)
        {
            item.Index_Owner = IndexColumnQueryDescriptor_11.Columns.Index_Owner.Read(r);
            item.Index_Name = IndexColumnQueryDescriptor_11.Columns.Index_Name.Read(r);
            item.Table_owner = IndexColumnQueryDescriptor_11.Columns.Table_owner.Read(r);
            item.Table_name = IndexColumnQueryDescriptor_11.Columns.Table_name.Read(r);
            item.Table_Type = IndexColumnQueryDescriptor_11.Columns.Table_Type.Read(r);
            item.uniqueness = IndexColumnQueryDescriptor_11.Columns.uniqueness.Read(r);
            item.indextype_owner = IndexColumnQueryDescriptor_11.Columns.indextype_owner.Read(r);
            item.indextype_name = IndexColumnQueryDescriptor_11.Columns.indextype_name.Read(r);
            item.status = IndexColumnQueryDescriptor_11.Columns.status.Read(r);
            item.parameters = IndexColumnQueryDescriptor_11.Columns.parameters.Read(r);
            item.Column_Table_Owner = IndexColumnQueryDescriptor_11.Columns.Column_Table_Owner.Read(r);
            item.Column_Table_name = IndexColumnQueryDescriptor_11.Columns.Column_Table_name.Read(r);
            item.Column_Name = IndexColumnQueryDescriptor_11.Columns.Column_Name.Read(r);
            item.Descend = IndexColumnQueryDescriptor_11.Columns.Descend.Read(r);
            item.tablespace_name = IndexColumnQueryDescriptor_11.Columns.tablespace_name.Read(r);
            item.pct_free = IndexColumnQueryDescriptor_11.Columns.pct_free.Read(r);
            item.ini_trans = IndexColumnQueryDescriptor_11.Columns.ini_trans.Read(r);
            item.max_trans = IndexColumnQueryDescriptor_11.Columns.max_trans.Read(r);
            item.initial_extent = IndexColumnQueryDescriptor_11.Columns.initial_extent.Read(r);
            item.next_extent = IndexColumnQueryDescriptor_11.Columns.next_extent.Read(r);
            item.min_extents = IndexColumnQueryDescriptor_11.Columns.min_extents.Read(r);
            item.max_extents = IndexColumnQueryDescriptor_11.Columns.max_extents.Read(r);
            item.pct_increase = IndexColumnQueryDescriptor_11.Columns.pct_increase.Read(r);
            item.freelists = IndexColumnQueryDescriptor_11.Columns.freelists.Read(r);
            item.freelist_groups = IndexColumnQueryDescriptor_11.Columns.freelist_groups.Read(r);
            item.buffer_pool = IndexColumnQueryDescriptor_11.Columns.buffer_pool.Read(r);
            item.Logging = IndexColumnQueryDescriptor_11.Columns.logging.Read(r);
            item.pct_threshold = IndexColumnQueryDescriptor_11.Columns.pct_threshold.Read(r);
            item.last_key_col = IndexColumnQueryDescriptor_11.Columns.last_key_col.Read(r);
            item.Compression = IndexColumnQueryDescriptor_11.Columns.compression.Read(r);
            item.compression_prefix = IndexColumnQueryDescriptor_11.Columns.compression_prefix.Read(r);
            item.degree = IndexColumnQueryDescriptor_11.Columns.degree.Read(r);
            item.instances = IndexColumnQueryDescriptor_11.Columns.instances.Read(r);
            item.generated = IndexColumnQueryDescriptor_11.Columns.generated.Read(r);
            item.Interval = IndexColumnQueryDescriptor_11.Columns.Interval.Read(r);
            item.visibility = IndexColumnQueryDescriptor_11.Columns.visibility.Read(r);
            item.partitioning_type = IndexColumnQueryDescriptor_11.Columns.partitioning_type.Read(r);
            item.subpartitioning_type = IndexColumnQueryDescriptor_11.Columns.subpartitioning_type.Read(r);
            item.interval = IndexColumnQueryDescriptor_11.Columns.interval.Read(r);
            item.locality = IndexColumnQueryDescriptor_11.Columns.locality.Read(r);
            item.column_expression = IndexColumnQueryDescriptor_11.Columns.column_expression.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {

            yield return Columns.Index_Owner;
            yield return Columns.Index_Name;
            yield return Columns.Table_owner;
            yield return Columns.Table_name;
            yield return Columns.Table_Type;
            yield return Columns.uniqueness;
            yield return Columns.indextype_owner;
            yield return Columns.indextype_name;
            yield return Columns.status;
            yield return Columns.parameters;
            yield return Columns.Column_Table_Owner;
            yield return Columns.Column_Table_name;
            yield return Columns.Column_Name;
            yield return Columns.Descend;
            yield return Columns.tablespace_name;
            yield return Columns.pct_free;
            yield return Columns.ini_trans;
            yield return Columns.max_trans;
            yield return Columns.initial_extent;
            yield return Columns.next_extent;
            yield return Columns.min_extents;
            yield return Columns.max_extents;
            yield return Columns.pct_increase;
            yield return Columns.freelists;
            yield return Columns.freelist_groups;
            yield return Columns.buffer_pool;
            yield return Columns.pct_threshold;
            yield return Columns.last_key_col;
            yield return Columns.compression;
            yield return Columns.compression_prefix;
            yield return Columns.degree;
            yield return Columns.instances;
            yield return Columns.generated;
            yield return Columns.Interval;
            yield return Columns.visibility;
            yield return Columns.partitioning_type;
            yield return Columns.subpartitioning_type;
            yield return Columns.interval;
            yield return Columns.locality;
            yield return Columns.column_expression;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<IndexColumnQueryTable_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum IndexColumnQueryColumns_11
    {
        Index_Owner,
        Index_Name,
        Table_owner,
        Table_name,
        Table_Type,
        uniqueness,
        indextype_owner,
        indextype_name,
        status,
        parameters,
        Column_Table_Owner,
        Column_Table_name,
        Column_Name,
        Descend,
        tablespace_name,
        pct_free,
        ini_trans,
        max_trans,
        initial_extent,
        next_extent,
        min_extents,
        max_extents,
        pct_increase,
        freelists,
        freelist_groups,
        buffer_pool,
        logging,
        pct_threshold,
        last_key_col,
        Compression,
        compression_prefix,
        degree,
        instances,
        generated,
        Interval,
        visibility,
        partitioning_type,
        subpartitioning_type,
        interval,
        locality,
        column_expression,
    }

    public class IndexColumnQueryTable_11
    {

        public string Index_Owner { get; set; }
        public string Index_Name { get; set; }
        public string Index_Type { get; set; }
        public string Table_owner { get; set; }
        public string Table_name { get; set; }
        public string Table_Type { get; set; }
        public string uniqueness { get; set; }
        public string indextype_owner { get; set; }
        public string indextype_name { get; set; }
        public string status { get; set; }
        public string parameters { get; set; }
        public string Column_Table_Owner { get; set; }
        public string Column_Table_name { get; set; }
        public string Column_Name { get; set; }
        public string Descend { get; set; }
        public string tablespace_name { get; set; }
        public string pct_free { get; set; }
        public string ini_trans { get; set; }
        public string max_trans { get; set; }
        public string initial_extent { get; set; }
        public string next_extent { get; set; }
        public string min_extents { get; set; }
        public string max_extents { get; set; }
        public string pct_increase { get; set; }
        public string freelists { get; set; }
        public Decimal freelist_groups { get; set; }
        public string buffer_pool { get; set; }
        public string pct_threshold { get; set; }
        public string last_key_col { get; set; }
        public string compression_prefix { get; set; }
        public string degree { get; set; }
        public string instances { get; set; }
        public string generated { get; set; }
        public string Interval { get; set; }
        public string visibility { get; set; }
        public string partitioning_type { get; set; }
        public string subpartitioning_type { get; set; }
        public string interval { get; set; }
        public string locality { get; set; }
        public string Logging { get; set; }
        public string Compression { get; set; }
        public string column_expression { get; set; }
    }

}
