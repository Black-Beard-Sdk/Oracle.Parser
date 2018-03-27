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

    public class TablespacesQuery_11 : DbQueryBase<TablespacesDto_11>
    {

        string sql =
@"

SELECT t.*, g.GROUP_NAME FROM SYS.DBA_TABLESPACES t
LEFT JOIN SYS.DBA_TABLESPACE_GROUPS g ON t.TABLESPACE_NAME = g.TABLESPACE_NAME


{0}

";

        public override List<TablespacesDto_11> Resolve(DbContextOracle context, Action<TablespacesDto_11> action)
        {
        
            List<TablespacesDto_11> List = new List<TablespacesDto_11>();
            var db = context.Database;
            this.OracleContext = context;

            if (action == null)
                action =
                t =>
                {


                    var obj = new TablespaceModel()
                    {
                        Name = t.TablespaceName,
                        BlockSize = t.BlockSize,
                        InitialExtent = t.InitialExtent,
                        NextExtent = t.NextExtent,
                        MinExtents = t.MinExtents,
                        MaxExtents = t.MaxExtents,
                        MaxSize = t.MaxSize,
                        PctIncrease = t.PctIncrease,
                        MinExtlen = t.MinExtlen,
                        Status = t.Status,
                        Contents = t.Contents,
                        Logging = t.Logging,
                        ForceLogging = t.ForceLogging.ToBoolean(),
                        ExtentManagement = t.ExtentManagement,
                        AllocationType = t.AllocationType,
                        PluggedIn = t.PluggedIn.ToBoolean(),
                        SegmentSpaceManagement = t.SegmentSpaceManagement,
                        DefTabCompression = t.DefTabCompression,
                        Retention = t.Retention,
                        Bigfile = t.Bigfile.ToBoolean(),
                        PredicateEvaluation = t.PredicateEvaluation,
                        Encrypted = t.Encrypted.ToBoolean(),
                        CompressFor = t.CompressFor,
                        GroupName = t.GroupName,
                    };



                };

            TablespacesQueryDescriptor_11 Tablespaces = new TablespacesQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = Tablespaces.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class TablespacesQueryDescriptor_11 : StructureDescriptorTable<TablespacesDto_11>
    {

        public TablespacesQueryDescriptor_11(string connectionString)
            : base(() => new TablespacesDto_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> TablespaceName = new Field<String>() { ColumnName = "TABLESPACE_NAME", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.TABLESPACE_NAME) };
            public static Field<Decimal> BlockSize = new Field<Decimal>() { ColumnName = "BLOCK_SIZE", Read = reader => reader.Field<Decimal>((int)TablespacesQueryColumns_11.BLOCK_SIZE) };
            public static Field<Decimal> InitialExtent = new Field<Decimal>() { ColumnName = "INITIAL_EXTENT", Read = reader => reader.Field<Decimal>((int)TablespacesQueryColumns_11.INITIAL_EXTENT) };
            public static Field<Decimal> NextExtent = new Field<Decimal>() { ColumnName = "NEXT_EXTENT", Read = reader => reader.Field<Decimal>((int)TablespacesQueryColumns_11.NEXT_EXTENT) };
            public static Field<Decimal> MinExtents = new Field<Decimal>() { ColumnName = "MIN_EXTENTS", Read = reader => reader.Field<Decimal>((int)TablespacesQueryColumns_11.MIN_EXTENTS) };
            public static Field<Decimal> MaxExtents = new Field<Decimal>() { ColumnName = "MAX_EXTENTS", Read = reader => reader.Field<Decimal>((int)TablespacesQueryColumns_11.MAX_EXTENTS) };
            public static Field<Decimal> MaxSize = new Field<Decimal>() { ColumnName = "MAX_SIZE", Read = reader => reader.Field<Decimal>((int)TablespacesQueryColumns_11.MAX_SIZE) };
            public static Field<Decimal> PctIncrease = new Field<Decimal>() { ColumnName = "PCT_INCREASE", Read = reader => reader.Field<Decimal>((int)TablespacesQueryColumns_11.PCT_INCREASE) };
            public static Field<Decimal> MinExtlen = new Field<Decimal>() { ColumnName = "MIN_EXTLEN", Read = reader => reader.Field<Decimal>((int)TablespacesQueryColumns_11.MIN_EXTLEN) };
            public static Field<String> Status = new Field<String>() { ColumnName = "STATUS", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.STATUS) };
            public static Field<String> Contents = new Field<String>() { ColumnName = "CONTENTS", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.CONTENTS) };
            public static Field<String> Logging = new Field<String>() { ColumnName = "LOGGING", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.LOGGING) };
            public static Field<String> ForceLogging = new Field<String>() { ColumnName = "FORCE_LOGGING", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.FORCE_LOGGING) };
            public static Field<String> ExtentManagement = new Field<String>() { ColumnName = "EXTENT_MANAGEMENT", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.EXTENT_MANAGEMENT) };
            public static Field<String> AllocationType = new Field<String>() { ColumnName = "ALLOCATION_TYPE", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.ALLOCATION_TYPE) };
            public static Field<String> PluggedIn = new Field<String>() { ColumnName = "PLUGGED_IN", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.PLUGGED_IN) };
            public static Field<String> SegmentSpaceManagement = new Field<String>() { ColumnName = "SEGMENT_SPACE_MANAGEMENT", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.SEGMENT_SPACE_MANAGEMENT) };
            public static Field<String> DefTabCompression = new Field<String>() { ColumnName = "DEF_TAB_COMPRESSION", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.DEF_TAB_COMPRESSION) };
            public static Field<String> Retention = new Field<String>() { ColumnName = "RETENTION", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.RETENTION) };
            public static Field<String> Bigfile = new Field<String>() { ColumnName = "BIGFILE", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.BIGFILE) };
            public static Field<String> PredicateEvaluation = new Field<String>() { ColumnName = "PREDICATE_EVALUATION", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.PREDICATE_EVALUATION) };
            public static Field<String> Encrypted = new Field<String>() { ColumnName = "ENCRYPTED", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.ENCRYPTED) };
            public static Field<String> CompressFor = new Field<String>() { ColumnName = "COMPRESS_FOR", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.COMPRESS_FOR) };
            public static Field<String> GroupName = new Field<String>() { ColumnName = "GROUP_NAME", Read = reader => reader.Field<String>((int)TablespacesQueryColumns_11.GROUP_NAME) };

            
        }

        #region Readers

        public override void Read(IDataReader r, TablespacesDto_11 item)
        {
            item.TablespaceName = TablespacesQueryDescriptor_11.Columns.TablespaceName.Read(r);
            item.BlockSize = TablespacesQueryDescriptor_11.Columns.BlockSize.Read(r);
            item.InitialExtent = TablespacesQueryDescriptor_11.Columns.InitialExtent.Read(r);
            item.NextExtent = TablespacesQueryDescriptor_11.Columns.NextExtent.Read(r);
            item.MinExtents = TablespacesQueryDescriptor_11.Columns.MinExtents.Read(r);
            item.MaxExtents = TablespacesQueryDescriptor_11.Columns.MaxExtents.Read(r);
            item.MaxSize = TablespacesQueryDescriptor_11.Columns.MaxSize.Read(r);
            item.PctIncrease = TablespacesQueryDescriptor_11.Columns.PctIncrease.Read(r);
            item.MinExtlen = TablespacesQueryDescriptor_11.Columns.MinExtlen.Read(r);
            item.Status = TablespacesQueryDescriptor_11.Columns.Status.Read(r);
            item.Contents = TablespacesQueryDescriptor_11.Columns.Contents.Read(r);
            item.Logging = TablespacesQueryDescriptor_11.Columns.Logging.Read(r);
            item.ForceLogging = TablespacesQueryDescriptor_11.Columns.ForceLogging.Read(r);
            item.ExtentManagement = TablespacesQueryDescriptor_11.Columns.ExtentManagement.Read(r);
            item.AllocationType = TablespacesQueryDescriptor_11.Columns.AllocationType.Read(r);
            item.PluggedIn = TablespacesQueryDescriptor_11.Columns.PluggedIn.Read(r);
            item.SegmentSpaceManagement = TablespacesQueryDescriptor_11.Columns.SegmentSpaceManagement.Read(r);
            item.DefTabCompression = TablespacesQueryDescriptor_11.Columns.DefTabCompression.Read(r);
            item.Retention = TablespacesQueryDescriptor_11.Columns.Retention.Read(r);
            item.Bigfile = TablespacesQueryDescriptor_11.Columns.Bigfile.Read(r);
            item.PredicateEvaluation = TablespacesQueryDescriptor_11.Columns.PredicateEvaluation.Read(r);
            item.Encrypted = TablespacesQueryDescriptor_11.Columns.Encrypted.Read(r);
            item.CompressFor = TablespacesQueryDescriptor_11.Columns.CompressFor.Read(r);
            item.GroupName = TablespacesQueryDescriptor_11.Columns.GroupName.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
                     yield return Columns.TablespaceName;
         yield return Columns.BlockSize;
         yield return Columns.InitialExtent;
         yield return Columns.NextExtent;
         yield return Columns.MinExtents;
         yield return Columns.MaxExtents;
         yield return Columns.MaxSize;
         yield return Columns.PctIncrease;
         yield return Columns.MinExtlen;
         yield return Columns.Status;
         yield return Columns.Contents;
         yield return Columns.Logging;
         yield return Columns.ForceLogging;
         yield return Columns.ExtentManagement;
         yield return Columns.AllocationType;
         yield return Columns.PluggedIn;
         yield return Columns.SegmentSpaceManagement;
         yield return Columns.DefTabCompression;
         yield return Columns.Retention;
         yield return Columns.Bigfile;
         yield return Columns.PredicateEvaluation;
         yield return Columns.Encrypted;
         yield return Columns.CompressFor;
         yield return Columns.GroupName;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<TablespacesDto_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum TablespacesQueryColumns_11
    {
              TABLESPACE_NAME,
      BLOCK_SIZE,
      INITIAL_EXTENT,
      NEXT_EXTENT,
      MIN_EXTENTS,
      MAX_EXTENTS,
      MAX_SIZE,
      PCT_INCREASE,
      MIN_EXTLEN,
      STATUS,
      CONTENTS,
      LOGGING,
      FORCE_LOGGING,
      EXTENT_MANAGEMENT,
      ALLOCATION_TYPE,
      PLUGGED_IN,
      SEGMENT_SPACE_MANAGEMENT,
      DEF_TAB_COMPRESSION,
      RETENTION,
      BIGFILE,
      PREDICATE_EVALUATION,
      ENCRYPTED,
      COMPRESS_FOR,
      GROUP_NAME,

    }

    public class TablespacesDto_11
    {

        public String TablespaceName { get; set; }
        public Decimal BlockSize { get; set; }
        public Decimal InitialExtent { get; set; }
        public Decimal NextExtent { get; set; }
        public Decimal MinExtents { get; set; }
        public Decimal MaxExtents { get; set; }
        public Decimal MaxSize { get; set; }
        public Decimal PctIncrease { get; set; }
        public Decimal MinExtlen { get; set; }
        public String Status { get; set; }
        public String Contents { get; set; }
        public String Logging { get; set; }
        public String ForceLogging { get; set; }
        public String ExtentManagement { get; set; }
        public String AllocationType { get; set; }
        public String PluggedIn { get; set; }
        public String SegmentSpaceManagement { get; set; }
        public String DefTabCompression { get; set; }
        public String Retention { get; set; }
        public String Bigfile { get; set; }
        public String PredicateEvaluation { get; set; }
        public String Encrypted { get; set; }
        public String CompressFor { get; set; }
        public String GroupName { get; set; }


    }

}

