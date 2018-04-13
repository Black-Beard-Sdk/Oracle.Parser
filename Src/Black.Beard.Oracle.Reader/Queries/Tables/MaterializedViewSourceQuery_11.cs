using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Reader.Queries
{

    public class MaterializedViewSourceQuery_11 : DbQueryBase<MaterializedViewSourceQueryTable_11>
    {

        //private const string patternMaterializedView = @"MATERIALIZED\s+VIEW";
        //public static System.Text.RegularExpressions.Regex regMaterializedViewEvaluate = new System.Text.RegularExpressions.Regex(patternMaterializedView, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        string sql = @"
SELECT  t.OWNER, t.MVIEW_NAME AS OBJECT_NAME, c.COMMENTS, dbms_metadata.get_ddl(object_type => 'MATERIALIZED_VIEW' ,name => t.MVIEW_NAME , schema=> t.OWNER) SOURCE
FROM    all_mviews t
LEFT JOIN all_mview_comments c ON t.OWNER = c.OWNER AND t.MVIEW_NAME = c.MVIEW_NAME
{0}
";
        public override List<MaterializedViewSourceQueryTable_11> Resolve(DbContextOracle context, Action<MaterializedViewSourceQueryTable_11> action)
        {

            List<MaterializedViewSourceQueryTable_11> List = new List<MaterializedViewSourceQueryTable_11>();
            this.OracleContext = context;
            var db = context.Database;

            if (action == null)
                action =
                    t =>
                    {

                        if (t.ObjectName.ExcludIfStartwith(t.SchemaName, Models.Configurations.ExcludeKindEnum.View))
                            return;

                        string key = t.SchemaName + "." + t.ObjectName;

                        TableModel table;
                        if (!db.Tables.TryGet(key, out table))
                        {
                            table = new TableModel() { Key = key, Name = t.ObjectName, IsView = true, IsMaterializedView = true, Owner = t.SchemaName, Comment = "", Generated = false, Parsed = true };
                            db.Tables.Add(table);

                        }

                        StringBuilder sb = new StringBuilder(t.Source.Length + 200);

                        sb.Append(t.Source.Trim().Trim(' ', '\t', '\r', '\n'));

                        if (sb[sb.Length - 1] != ';')
                        {
                            sb.AppendLine(string.Empty);
                            sb.AppendLine(";");
                        }
                        else
                        {
                            sb.AppendLine(string.Empty);
                        }

                        if (!string.IsNullOrEmpty(t.Comments))
                        {
                            table.Comment = t.Comments;
                        }

                        table.IsView = true;
                        table.IsMaterializedView = true;

                        table.CodeView = GetSource(sb.ToString());


                    };

            MaterializedViewSourceQueryDescriptor_11 view = new MaterializedViewSourceQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition());

//            context.Manager.ExecuteNonQuery(CommandType.Text, @"BEGIN
// DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM,'TABLESPACE', FALSE);
//END;");
            context.Manager.ExecuteNonQuery(CommandType.Text, @"BEGIN
DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM, 'STORAGE', FALSE);
END;");
            context.Manager.ExecuteNonQuery(CommandType.Text, @"BEGIN
DBMS_METADATA.SET_TRANSFORM_PARAM(DBMS_METADATA.SESSION_TRANSFORM, 'SEGMENT_ATTRIBUTES', TRUE);
END;");

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }


        private static string GetSource(string item)
        {

            var u = item.Split('\n');
            StringBuilder sb = new StringBuilder();
            foreach (var line in u)
                if (!line.Trim().StartsWith("ORGANIZATION"))
                {
                    if (!string.IsNullOrEmpty(line.Trim()))
                        sb.AppendLine(line);
                }

            return Utils.Serialize(sb.ToString(), true);

        }
    }

    /*
    CREATE MATERIALIZED VIEW "ADMINBI"."VM_PARCEL_STEPS" ("PARCEL_STEPS_ID", "PARCEL_ID", "STEP_ID", "STEP_DTM", "VALIDATION_DTM", "DURATION", "THRESHOLD_DTM", "SOURCE_ID", "SOURCE_TYPE_ID", "COUNTER", "STEP_MOTIVE_ID", "CREATION_DTM", "RESERVES", "ANOMALY_ACTION_ID", "USER_ID", "LAST_UPDATE_DTM", "PHYS_CARRIER_ID")
  ORGANIZATION HEAP PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS NOLOGGING
  BUILD IMMEDIATE
  USING INDEX 
  REFRESH COMPLETE ON DEMAND
  USING DEFAULT LOCAL ROLLBACK SEGMENT
  USING ENFORCED CONSTRAINTS DISABLE QUERY REWRITE
  AS SELECT PARCEL_STEPS_ID ,
              PARCEL_ID ,
              STEP_ID ,
              STEP_DTM ,
              VALIDATION_DTM ,
              DURATION ,
              THRESHOLD_DTM ,
              SOURCE_ID ,
              SOURCE_TYPE_ID ,
              COUNTER ,
              STEP_MOTIVE_ID ,
              CREATION_DTM ,
              RESERVES ,
              ANOMALY_ACTION_ID ,
              USER_ID ,
              LAST_UPDATE_DTM,
              Phys_Carrier_Id
              FROM MASTER.PARCEL_STEPS
              WHERE 1               =1
              AND (LAST_UPDATE_DTM >= TRUNC(sysdate)-4)
              OR (LAST_UPDATE_DTM  IS NULL
              AND creation_dtm     >= TRUNC(sysdate)-4)
;

*/

    public class MaterializedViewSourceQueryDescriptor_11 : StructureDescriptorTable<MaterializedViewSourceQueryTable_11>
    {

        public MaterializedViewSourceQueryDescriptor_11(string connectionString)
            : base(() => new MaterializedViewSourceQueryTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>() { ColumnName = "OWNER", Read = reader => reader.Field<string>((int)MaterializedViewSourceQueryColumns_11.OWNER) };
            public static Field<string> ObjectName = new Field<string>() { ColumnName = "OBJECT_NAME", Read = reader => reader.Field<string>((int)MaterializedViewSourceQueryColumns_11.OBJECT_NAME) };
            public static Field<string> Source = new Field<string>() { ColumnName = "SOURCE", Read = reader => reader.Field<string>((int)MaterializedViewSourceQueryColumns_11.Source) };
            public static Field<string> Comments = new Field<string>() { ColumnName = "COMMENTS", Read = reader => reader.Field<string>((int)MaterializedViewSourceQueryColumns_11.Comments) };

        }




        #region Readers

        public override void Read(IDataReader r, MaterializedViewSourceQueryTable_11 item)
        {
            item.SchemaName = MaterializedViewSourceQueryDescriptor_11.Columns.Owner.Read(r);
            item.ObjectName = MaterializedViewSourceQueryDescriptor_11.Columns.ObjectName.Read(r);
            item.Source = MaterializedViewSourceQueryDescriptor_11.Columns.Source.Read(r);
            item.Comments = MaterializedViewSourceQueryDescriptor_11.Columns.Comments.Read(r);


        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.ObjectName;
            yield return Columns.Comments;
            yield return Columns.Source;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<MaterializedViewSourceQueryTable_11> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum MaterializedViewSourceQueryColumns_11
    {
        OWNER,
        OBJECT_NAME,
        Comments,
        Source,
    }

    public class MaterializedViewSourceQueryTable_11
    {
        public string SchemaName { get; set; }
        public string ObjectName { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }


    }

}

/*
EDITION
INDEX PARTITION
TABLE SUBPARTITION
CONSUMER GROUP
SEQUENCE
TABLE PARTITION
SCHEDULE
QUEUE
RULE
JAVA DATA
PROCEDURE
OPERATOR
LOB PARTITION
DESTINATION
WINDOW
SCHEDULER GROUP
CHAIN
DATABASE LINK
LOB
PACKAGE
PACKAGE BODY
LIBRARY
PROGRAM
RULE SET
CONTEXT
TYPE BODY
JAVA RESOURCE
XML SCHEMA
TRIGGER
JOB CLASS
UNDEFINED
DIRECTORY
MATERIALIZED VIEW
TABLE
INDEX
SYNONYM
VIEW
FUNCTION
JAVA CLASS
JAVA SOURCE
INDEXTYPE
CLUSTER
TYPE
RESOURCE PLAN
JOB
EVALUATION CONTEXT
*/
