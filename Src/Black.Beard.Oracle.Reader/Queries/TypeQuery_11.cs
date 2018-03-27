using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Reader.Queries
{

    public class TypeQuery_11 : DbQueryBase<TypeTable_11>
    {

        string sql =
@"
select t.OWNER, t.TYPE_NAME, t1.ATTR_NAME, t1.ATTR_TYPE_NAME, t1.length, t1.PRECISION, t1.ATTR_NO, t.supertype_owner, t.supertype_name, t1.Inherited, t.typecode
,t2.owner as CollectionType, t2.type_name as CollectionName
FROM dba_types t 
LEFT JOIN dba_type_attrs t1 ON t1.OWNER = t.OWNER AND t1.TYPE_NAME = t.TYPE_NAME
LEFT JOIN sys.COLLECTION$ c ON c.toid = t.type_oid
LEFT JOIN dba_types t2 ON t2.type_oid = c.elem_toid
{0}
ORDER BY t1.OWNER,t1.TYPE_NAME,t1.ATTR_NO

";

        public override List<TypeTable_11> Resolve(DbContextOracle context, Action<TypeTable_11> action)
        {

            var db = context.Database;
            List<TypeTable_11> List = new List<TypeTable_11>();
            this.OracleContext = context;

            if (action == null)
                action =
                t =>
                {

                    if (!string.IsNullOrEmpty(t.OWNER) && t.TYPE_NAME.ExcludIfStartwith(t.OWNER, Models.Configurations.ExcludeKindEnum.Type))
                        return;

                    if (!context.Use(t.OWNER))
                        return;

                    string key = t.OWNER + "." + t.TYPE_NAME;
                    TypeItem type;

                    if (!db.Types.TryGet(key, out type))
                    {
                        string superType = string.Empty;
                        if (!string.IsNullOrEmpty(t.SUPERTYPE_OWNER))
                            superType += t.SUPERTYPE_OWNER + ".";
                        if (!string.IsNullOrEmpty(t.SUPERTYPE_NAME))
                            superType += t.SUPERTYPE_NAME;

                        type = new TypeItem()
                        {
                            Key = key,
                            Name = t.TYPE_NAME,
                            SuperType = superType,
                            Owner = t.OWNER,
                            TypeCode = t.TYPECODE,
                            CollectionSchemaName = t.COLLECTIONTYPE,
                            CollectionTypeName = t.COLLECTIONNAME
                        };
                        db.Types.Add(type);
                    }

                    var p = new PropertyModel()
                    {
                        Name = !string.IsNullOrEmpty(t.ATTR_NAME) ? Regex.Replace(t.ATTR_NAME, "[^\\w\\._]", "") : string.Empty,
                        Inherited = t.INHERITED,
                        IsNotNull = false,
                    };

                    p.Type.DataType = t.ATTR_TYPE_NAME;
                    //p.Type.CsType = TypeMatchExtension.Match(t.ATTR_TYPE_NAME, t.Lenght, t.Precision, 0);
                    p.Type.DataLength = t.Lenght;
                    p.Type.DataPrecision = t.Precision;

                    type.Properties.Add(p);


                };


            TypeDescriptor_11 view = new TypeDescriptor_11(context.Manager.ConnectionString);

            sql = string.Format(sql, ProcQueryWhereCondition);

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class TypeDescriptor_11 : StructureDescriptorTable<TypeTable_11>
    {

        public TypeDescriptor_11(string connectionString)
            : base(() => new TypeTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.OWNER)
            };

            public static Field<string> TYPE_NAME = new Field<string>()
            {
                ColumnName = "TYPE_NAME",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.TYPE_NAME)
            };

            public static Field<string> ATTR_NAME = new Field<string>()
            {
                ColumnName = "ATTR_NAME",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.ATTR_NAME)
            };

            public static Field<string> ATTR_TYPE_NAME = new Field<string>()
            {
                ColumnName = "ATTR_TYPE_NAME",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.ATTR_TYPE_NAME)
            };

            public static Field<decimal> Lenght = new Field<decimal>()
            {
                ColumnName = "length",
                Read = reader => reader.Field<decimal>((int)TypeTableColumns_11.Length)
            };

            public static Field<decimal> PRECISION = new Field<decimal>()
            {
                ColumnName = "PRECISION",
                Read = reader => reader.Field<decimal>((int)TypeTableColumns_11.PRECISION)
            };

            public static Field<decimal> ATTR_NO = new Field<decimal>()
            {
                ColumnName = "ATTR_NO",
                Read = reader => reader.Field<decimal>((int)TypeTableColumns_11.ATTR_NO)
            };

            public static Field<string> SUPERTYPE_OWNER = new Field<string>()
            {
                ColumnName = "SUPERTYPE_OWNER",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.SUPERTYPE_OWNER) ?? string.Empty
            };

            public static Field<string> SUPERTYPE_NAME = new Field<string>()
            {
                ColumnName = "SUPERTYPE_NAME",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.SUPERTYPE_NAME) ?? string.Empty
            };

            public static Field<bool> INHERITED = new Field<bool>()
            {
                ColumnName = "INHERITED",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.INHERITED) == "YES"
            };

            public static Field<string> TYPECODE = new Field<string>()
            {
                ColumnName = "TYPECODE",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.TYPECODE) ?? string.Empty
            };

            public static Field<string> CollectionType = new Field<string>()
            {
                ColumnName = "COLLECTIONTYPE",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.COLLECTIONTYPE) ?? string.Empty
            };

            public static Field<string> CollectionName = new Field<string>()
            {
                ColumnName = "COLLECTIONNAME",
                Read = reader => reader.Field<string>((int)TypeTableColumns_11.COLLECTIONNAME) ?? string.Empty
            };

        }

        #region Readers

        public override void Read(IDataReader r, TypeTable_11 item)
        {
            item.OWNER = TypeDescriptor_11.Columns.Owner.Read(r);
            item.TYPE_NAME = TypeDescriptor_11.Columns.TYPE_NAME.Read(r);
            item.ATTR_NAME = TypeDescriptor_11.Columns.ATTR_NAME.Read(r);
            item.ATTR_TYPE_NAME = TypeDescriptor_11.Columns.ATTR_TYPE_NAME.Read(r);
            item.Lenght = TypeDescriptor_11.Columns.Lenght.Read(r).ToInt32();
            item.Precision = TypeDescriptor_11.Columns.PRECISION.Read(r).ToInt32();
            item.ATTR_NO = TypeDescriptor_11.Columns.ATTR_NO.Read(r).ToInt32();
            item.SUPERTYPE_OWNER = TypeDescriptor_11.Columns.SUPERTYPE_OWNER.Read(r);
            item.SUPERTYPE_NAME = TypeDescriptor_11.Columns.SUPERTYPE_NAME.Read(r);
            item.INHERITED = TypeDescriptor_11.Columns.INHERITED.Read(r);
            item.TYPECODE = TypeDescriptor_11.Columns.TYPECODE.Read(r);
            item.COLLECTIONTYPE = TypeDescriptor_11.Columns.CollectionType.Read(r);
            item.COLLECTIONNAME = TypeDescriptor_11.Columns.CollectionName.Read(r);
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.TYPE_NAME;
            yield return Columns.ATTR_NAME;
            yield return Columns.ATTR_TYPE_NAME;
            yield return Columns.Lenght;
            yield return Columns.PRECISION;
            yield return Columns.ATTR_NO;
            yield return Columns.SUPERTYPE_OWNER;
            yield return Columns.SUPERTYPE_NAME;
            yield return Columns.INHERITED;
            yield return Columns.TYPECODE;
            yield return Columns.CollectionType;
            yield return Columns.CollectionName;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<TypeTable_11> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum TypeTableColumns_11
    {
        OWNER,
        TYPE_NAME,
        ATTR_NAME,
        ATTR_TYPE_NAME,
        Length,
        PRECISION,
        ATTR_NO,
        SUPERTYPE_OWNER,
        SUPERTYPE_NAME,
        INHERITED,
        TYPECODE,
        COLLECTIONTYPE,
        COLLECTIONNAME,
    }

    public class TypeTable_11
    {
        public string OWNER { get; set; }
        public string TYPE_NAME { get; set; }
        public string ATTR_NAME { get; set; }
        public string ATTR_TYPE_NAME { get; set; }
        public int Lenght { get; set; }
        public int Precision { get; set; }
        public int ATTR_NO { get; set; }
        public string SUPERTYPE_OWNER { get; set; }
        public string SUPERTYPE_NAME { get; set; }
        public bool INHERITED { get; set; }
        public string TYPECODE { get; set; }
        public string COLLECTIONTYPE { get; set; }
        public string COLLECTIONNAME { get; set; }
    }

}
