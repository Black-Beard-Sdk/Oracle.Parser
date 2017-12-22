using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class ObjectQuery : DbQueryBase<ModelObject>
    {

        string sql =
@"

SELECT 
  o.OBJECT_NAME, 
  o.object_type, 
  t.COMMENTS, 
  o.OWNER, 
  o.DATA_OBJECT_ID, 
  o.GENERATED, 
  o.STATUS, 
  o.TEMPORARY
  
FROM dba_objects o
LEFT JOIN ALL_tab_comments t ON o.owner = t.owner AND o.OBJECT_NAME = t.TABLE_NAME
WHERE o.OBJECT_TYPE IN ({0}) AND o.OBJECT_NAME = t.TABLE_NAME {1}
ORDER BY o.OWNER, o.OBJECT_NAME

";

        public override List<ModelObject> Resolve(DbContextOracle context, Action<ModelObject> action)
        {

            List<ModelObject> List = new List<ModelObject>();
            var db = context.database;
            this.OracleContext = context;

            if (action == null)
                action =
                t =>
                {

                    if (!context.Use(t.Owner))
                        return;

                    switch (t.Type)
                    {

                        case "INDEX":
                        case "INDEX PARTITION":
                        case "INDEX SUBPARTITION":
                        case "INDEXTYPE":

                        case "FUNCTION":
                        case "TRIGGER":

                        case "CLUSTER":
                        case "CONSUMER GROUP":
                        case "CONTEXT":
                        case "DATABASE LINK":
                        case "DESTINATION":
                        case "DIRECTORY":
                        case "EDITION":
                        case "EVALUATION CONTEXT":
                        case "JAVA CLASS":
                        case "JAVA DATA":
                        case "JAVA RESOURCE":
                        case "JAVA SOURCE":
                        case "JOB":
                        case "JOB CLASS":
                        case "LIBRARY":
                        case "LOB":
                        case "LOB PARTITION":
                        case "OPERATOR":
                        case "PACKAGE":
                        case "PACKAGE BODY":
                        case "PROGRAM":
                        case "QUEUE":
                        case "RESOURCE PLAN":
                        case "RULE":
                        case "RULE SET":
                        case "SCHEDULE":
                        case "SCHEDULER GROUP":
                        case "SYNONYM":
                        case "TABLE PARTITION":
                        case "TABLE SUBPARTITION":
                        case "TYPE":
                        case "TYPE BODY":
                        case "UNDEFINED":
                        case "WINDOW":
                        case "XML SCHEMA":
                            break;

                        //case "SEQUENCE":
                        //    db.Add(new SequenceModel() { SequenceName = t.Name, Name = t.Owner + "." + t.Name, Owner = t.Owner, Comment = t.Comment, Generated = t.GENERATED, Status = t.STATUS, Temporary = t.TEMPORARY, Parsed = true });
                        //    break;

                        case "TABLE":
                            if (!t.Name.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.Table))
                                db.Tables.Add(new TableModel() { Key = t.Owner + "." + t.Name, Name = t.Name, SchemaName = t.Owner, Comment = t.Comment, Generated = t.GENERATED, Status = t.STATUS, Temporary = t.TEMPORARY, Parsed = true });
                            break;

                        //case "MATERIALIZED VIEW":
                        //    db.Add(new TableModel() { Key = t.Owner + "." + t.Name, Name = t.Name, IsView = true, IsMatrializedView = true, SchemaName = t.Owner, Comment = t.Comment, Generated = t.GENERATED, Status = t.STATUS, Temporary = t.TEMPORARY, Parsed = true });
                        //    break;

                        case "VIEW":
                            if (!t.Name.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.View))
                                db.Tables.Add(new TableModel() { Key = t.Owner + "." + t.Name, Name = t.Name, IsView = true, SchemaName = t.Owner, Comment = t.Comment, Generated = t.GENERATED, Status = t.STATUS, Temporary = t.TEMPORARY, Parsed = true });
                            break;
                        default:
                            break;
                    }

                };

            ObjectDescriptor objets = new ObjectDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, QueryBase.In("TABLE", "VIEW", "SEQUENCE"), TableQueryAndCondition("t", "OWNER"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = objets.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class ObjectDescriptor : StructureDescriptorTable<ModelObject>
    {

        public ObjectDescriptor(string connectionString)
            : base(() => new ModelObject(), connectionString)
        {

        }

        public static class Columns
        {
            public static Field<string> Name = new Field<string>() { ColumnName = "OBJECT_NAME", Read = reader => reader.Field<string>((int)ModelObjectColumns.OBJECT_NAME) };
            public static Field<string> Type = new Field<string>() { ColumnName = "OBJECT_TYPE", Read = reader => reader.Field<string>((int)ModelObjectColumns.OBJECT_TYPE) };
            public static Field<string> COMMENTS = new Field<string>() { ColumnName = "COMMENTS", Read = reader => reader.Field<string>((int)ModelObjectColumns.COMMENTS) };
            public static Field<string> SchemaOwner = new Field<string>() { ColumnName = "OWNER", Read = reader => reader.Field<string>((int)ModelObjectColumns.OWNER) };
            public static Field<string> DATA_OBJECT_ID = new Field<string>() { ColumnName = "DATA_OBJECT_ID", Read = reader => reader.Field<string>((int)ModelObjectColumns.DATA_OBJECT_ID) };
            public static Field<string> GENERATED = new Field<string>() { ColumnName = "GENERATED", Read = reader => reader.Field<string>((int)ModelObjectColumns.GENERATED) };
            public static Field<string> STATUS = new Field<string>() { ColumnName = "STATUS", Read = reader => reader.Field<string>((int)ModelObjectColumns.STATUS) };
            public static Field<string> TEMPORARY = new Field<string>() { ColumnName = "TEMPORARY", Read = reader => reader.Field<string>((int)ModelObjectColumns.TEMPORARY) };
        }


        #region Readers

        public override void Read(IDataReader r, ModelObject item)
        {
            item.Name = ObjectDescriptor.Columns.Name.Read(r);
            item.Type = ObjectDescriptor.Columns.Type.Read(r);
            item.Comment = ObjectDescriptor.Columns.COMMENTS.Read(r);
            item.Owner = ObjectDescriptor.Columns.SchemaOwner.Read(r);

            item.DATA_OBJECT_ID = ObjectDescriptor.Columns.DATA_OBJECT_ID.Read(r);
            item.GENERATED = ObjectDescriptor.Columns.GENERATED.Read(r).ToBoolean();
            item.STATUS = ObjectDescriptor.Columns.STATUS.Read(r);
            item.TEMPORARY = ObjectDescriptor.Columns.TEMPORARY.Read(r).ToBoolean();

        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Name;
            yield return Columns.Type;
            yield return Columns.COMMENTS;
            yield return Columns.SchemaOwner;
            yield return Columns.DATA_OBJECT_ID;
            yield return Columns.GENERATED;
            yield return Columns.STATUS;
            yield return Columns.TEMPORARY;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ModelObject> entities)
        {
            throw new NotImplementedException();
        }

    }

    public enum ModelObjectColumns
    {
        OBJECT_NAME,
        OBJECT_TYPE,
        COMMENTS,
        OWNER,
        DATA_OBJECT_ID,
        GENERATED,
        STATUS,
        TEMPORARY,
    }

    public class ModelObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public string Owner { get; set; }

        public string DATA_OBJECT_ID { get; set; }
        public bool GENERATED { get; set; }
        public string STATUS { get; set; }
        public bool TEMPORARY { get; set; }


    }

}
