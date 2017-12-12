using Pssa.Sdk.DataAccess.Dao;
using Pssa.Sdk.DataAccess.Dao.Contracts;
using Pssa.Tools.Databases.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class AllObjectQuery : DbQueryBase<AllModelObject>
    {

        string sql =
@"

SELECT 
  o.OBJECT_NAME, 
  o.object_type, 
  o.OWNER, 
  o.DATA_OBJECT_ID, 
  o.GENERATED, 
  o.STATUS, 
  o.TEMPORARY
  
FROM dba_objects o
where o.OWNER IN ({0})
ORDER BY o.OWNER, o.OBJECT_NAME

";

        public override List<AllModelObject> Resolve(DbContextOracle context, Action<AllModelObject> action)
        {

            List<AllModelObject> List = new List<AllModelObject>();
            var db = context.database;
            this.OracleContext = context;

            AllObjectDescriptor objets = new AllObjectDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, In(this.OwnerNames.ToArray()));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = objets.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class AllObjectDescriptor : StructureDescriptorTable<AllModelObject>
    {

        public AllObjectDescriptor(string connectionString)
            : base(() => new AllModelObject(), connectionString)
        {

        }

        public static class Columns
        {
            public static Field<string> Name = new Field<string>() { ColumnName = "OBJECT_NAME", Read = reader => reader.Field<string>((int)AllModelObjectColumns.OBJECT_NAME) };
            public static Field<string> Type = new Field<string>() { ColumnName = "OBJECT_TYPE", Read = reader => reader.Field<string>((int)AllModelObjectColumns.OBJECT_TYPE) };
            public static Field<string> SchemaOwner = new Field<string>() { ColumnName = "OWNER", Read = reader => reader.Field<string>((int)AllModelObjectColumns.OWNER) };
            public static Field<string> DATA_OBJECT_ID = new Field<string>() { ColumnName = "DATA_OBJECT_ID", Read = reader => reader.Field<string>((int)AllModelObjectColumns.DATA_OBJECT_ID) };
            public static Field<string> GENERATED = new Field<string>() { ColumnName = "GENERATED", Read = reader => reader.Field<string>((int)AllModelObjectColumns.GENERATED) };
            public static Field<string> STATUS = new Field<string>() { ColumnName = "STATUS", Read = reader => reader.Field<string>((int)AllModelObjectColumns.STATUS) };
            public static Field<string> TEMPORARY = new Field<string>() { ColumnName = "TEMPORARY", Read = reader => reader.Field<string>((int)AllModelObjectColumns.TEMPORARY) };
        }
        

        #region Readers

        public override void Read(IDataReader r, AllModelObject item)
        {
            item.Name = AllObjectDescriptor.Columns.Name.Read(r);
            item.Type = AllObjectDescriptor.Columns.Type.Read(r);
            item.Owner = AllObjectDescriptor.Columns.SchemaOwner.Read(r);

            item.DATA_OBJECT_ID = AllObjectDescriptor.Columns.DATA_OBJECT_ID.Read(r);
            item.GENERATED = AllObjectDescriptor.Columns.GENERATED.Read(r).ToBoolean();
            item.STATUS = AllObjectDescriptor.Columns.STATUS.Read(r);
            item.TEMPORARY = AllObjectDescriptor.Columns.TEMPORARY.Read(r).ToBoolean();

        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Name;
            yield return Columns.Type;
            yield return Columns.SchemaOwner;
            yield return Columns.DATA_OBJECT_ID;
            yield return Columns.GENERATED;
            yield return Columns.STATUS;
            yield return Columns.TEMPORARY;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<AllModelObject> entities)
        {
            throw new NotImplementedException();
        }

    }

    public enum AllModelObjectColumns
    {
        OBJECT_NAME, 
        OBJECT_TYPE, 
        OWNER,
        DATA_OBJECT_ID,  
        GENERATED,       
        STATUS,          
        TEMPORARY,
    }

    public class AllModelObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }

        public string DATA_OBJECT_ID          { get; set; }
        public bool GENERATED               { get; set; }
        public string STATUS                  { get; set; }
        public bool TEMPORARY               { get; set; }


    }

}
