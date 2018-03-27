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

    public class TableColumnQuery_11 : DbQueryBase<ModelTable_11>
    {


        string sql =
@"
with tt as
(SELECT DISTINCT(cc.COLUMN_NAME), t.owner,t.TABLE_NAME,t.COLUMN_ID,t.DATA_TYPE,t.DATA_LENGTH,t.DATA_PRECISION,t.DATA_SCALE,t.NULLABLE, 
t.DEFAULT_LENGTH,t.CHARACTER_SET_NAME,t.DATA_UPGRADED,cc.COMMENTS, t.char_used, t.CHAR_COL_DECL_LENGTH, t.CHAR_LENGTH

FROM dba_tab_cols t LEFT OUTER JOIN all_col_comments cc ON  t.TABLE_NAME = cc.TABLE_NAME AND t.OWNER = cc.OWNER
WHERE t.TABLE_NAME = cc.TABLE_NAME
AND t.COLUMN_NAME = cc.COLUMN_NAME
 {0}
)
select tt.owner, tt.TABLE_NAME,tt.COLUMN_NAME,tt.COLUMN_ID,tt.DATA_TYPE,tt.COMMENTS,tt.DATA_LENGTH,tt.DATA_PRECISION,tt.DATA_SCALE,tt.NULLABLE, 
tt.DEFAULT_LENGTH,tt.CHARACTER_SET_NAME,tt.DATA_UPGRADED, tt.char_used, tt.CHAR_COL_DECL_LENGTH, tt.CHAR_LENGTH from tt
GROUP by tt.owner, tt.TABLE_NAME,tt.COLUMN_NAME, tt.COLUMN_ID,tt.DATA_TYPE,tt.COMMENTS,tt.DATA_LENGTH,tt.DATA_PRECISION,tt.DATA_SCALE,tt.NULLABLE, 
tt.DEFAULT_LENGTH,tt.CHARACTER_SET_NAME,tt.DATA_UPGRADED, tt.char_used, tt.CHAR_COL_DECL_LENGTH, tt.CHAR_LENGTH
ORDER BY tt.TABLE_NAME,tt.COLUMN_ID
";
        public override List<ModelTable_11> Resolve(DbContextOracle context, Action<ModelTable_11> action)
        {

            List<ModelTable_11> List = new List<ModelTable_11>();
            this.OracleContext = context;
            var db = context.Database;

            if (action == null)
                action =
                    t =>
                    {

                        if (!context.Use(t.SchemaName))
                            return;

                        if (t.TableName.ExcludIfStartwith(t.SchemaName, Models.Configurations.ExcludeKindEnum.Table))
                            return;

                        string key = t.SchemaName + "." + t.TableName;
                        TableModel table;

                        if (!db.Tables.TryGet(key, out table))
                        {
                            table = new TableModel() { Key = key, Owner = t.SchemaName, Name = t.TableName };
                            db.Tables.Add(table);
                        }
                      
                        //if (!table.Columns.Contains(t.ColumnName))
                        if (!IsColumnExist(table, t.ColumnName))
                        {

                            var index = (table.Columns.Count) + 1;

                            var c = new ColumnModel()
                            {
                                Key = index.ToString(),
                                CharactereSetName = t.CharactereSetName,
                                ColumnId = t.ColumnId,
                                Name = t.ColumnName,
                                Description = t.Comments,
                                Nullable = t.Nullable,
                                DataUpgrated = t.DataUpgraded,
                                IsPrimaryKey = false,
                                IsSequence = false,

                                CharUsed = t.CharUsed,

                            };

                            c.Type.DataDefault = string.Empty;
                            c.Type.DataPrecision = t.DataPrecision;
                            c.Type.DataLength = t.DataLenght;
                            c.Type.DataType = t.DataType;
                            c.Type.defaultLength = t.DefaultLength;

                            if (t.CharUsed == "C")
                            {
                                if (!string.IsNullOrEmpty(t.CHAR_LENGTH))
                                {
                                    int l;
                                    if (int.TryParse(t.CHAR_LENGTH, out l))
                                        if (c.Type.DataLength != l)
                                            c.Type.DataLength = l;

                                }
                            }
                        //else if (c.CharUsed == "B")
                        //{
                        //    if (!string.IsNullOrEmpty(t.CHAR_COL_DECL_LENGTH))
                        //    {
                        //        int l;
                        //        if (int.TryParse(t.CHAR_COL_DECL_LENGTH, out l))
                        //            if (c.Type.DataLength != l)
                        //                c.Type.DataLength = l;

                        //    }
                        //}


                        //c.Type.CsType = TypeMatchExtension.Match(t.DataType, t.DataLenght, t.DataPrecision, 0);

                        c.ForeignKey.IsForeignKey = false;
                            c.ForeignKey.ConstraintName = "";
                            c.ForeignKey.Table = "";
                            c.ForeignKey.Field = "";

                            table.Columns.Add(c);

                        }
                        else
                        {

                        }

                    };


            TableDescriptor_11 view = new TableDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryAndCondition());

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            if (this.OracleContext.Database != null)
            {
                var _db = this.OracleContext.Database;
                foreach (TableModel table in _db.Tables)
                {

                    table.codeView = string.Empty;

                    if (table.IsMatrializedView)
                    {

                        StringBuilder sb = new StringBuilder(table.codeView.Length * 4);
                        sb.Append(Utils.Unserialize(table.codeView, true));

                        if (!string.IsNullOrEmpty(table.Comment))
                        {
                            sb.AppendLine(string.Format(@"COMMENT ON TABLE ""{0}"".""{1}"" is '{2}';", table.Owner, table.Name, table.Comment.Replace("'", "''")));
                        }

                        foreach (ColumnModel item in table.Columns.OfType<ColumnModel>().OrderBy(c => c.Key))
                            if (!string.IsNullOrEmpty(item.Description))
                                sb.AppendLine(string.Format(@"COMMENT ON COLUMN ""{0}"".""{1}"".""{2}"" is '{3}';", table.Owner, table.Name, item.Name, item.Description.Replace("'", "''")));

                        table.codeView = Utils.Serialize(sb.ToString(), true);
                    }
                    else if (table.IsView)
                    {

                        StringBuilder sb = new StringBuilder(table.codeView.Length * 4);
                        sb.Append(Utils.Unserialize(table.codeView, true));

                        if (!string.IsNullOrEmpty(table.Comment))
                        {
                            sb.AppendLine(string.Format(@"COMMENT ON TABLE ""{0}"".""{1}"" is '{2}';", table.Owner, table.Name, table.Comment.Replace("'", "''")));
                        }

                        foreach (ColumnModel item in table.Columns.OfType<ColumnModel>().OrderBy(c => c.Key))
                            if (!string.IsNullOrEmpty(item.Description))
                                sb.AppendLine(string.Format(@"COMMENT ON COLUMN ""{0}"".""{1}"".""{2}"" is '{3}';", table.Owner, table.Name, item.Name, item.Description.Replace("'", "''")));

                        table.codeView = Utils.Serialize(sb.ToString(), true);
                    }
                }

            }
            return List;

        }


        bool IsColumnExist(TableModel table, string columnName)
        {
            foreach (ColumnModel column in table.Columns)
            {
                if (column.Name == columnName)
                    return true;
            }
            return false;

        }



    }

    public class TableDescriptor_11 : StructureDescriptorTable<ModelTable_11>
    {

        public TableDescriptor_11(string connectionString)
            : base(() => new ModelTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.OWNER)
            };

            public static Field<string> TableName = new Field<string>()
            {
                ColumnName = "TABLE_NAME",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.TABLE_NAME)
            };

            public static Field<string> ColumnName = new Field<string>()
            {
                ColumnName = "COLUMN_NAME",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.COLUMN_NAME)
            };

            public static Field<decimal> ColumnId = new Field<decimal>()
            {
                ColumnName = "COLUMN_ID",
                Read = reader => reader.Field<decimal>((int)ModelTableColumns_11.COLUMN_ID)
            };

            public static Field<string> DataType = new Field<string>()
            {
                ColumnName = "DATA_TYPE",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.DATA_TYPE)
            };

            public static Field<string> Comments = new Field<string>()
            {
                ColumnName = "COMMENTS",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.COMMENTS)
            };

            public static Field<decimal> DataLength = new Field<decimal>()
            {
                ColumnName = "DATA_LENGTH",
                Read = reader => reader.Field<decimal>((int)ModelTableColumns_11.DATA_LENGTH)
            };

            public static Field<decimal> DataPrecision = new Field<decimal>()
            {
                ColumnName = "DATA_PRECISION",
                Read = reader => reader.Field<decimal>((int)ModelTableColumns_11.DATA_PRECISION)
            };

            public static Field<decimal> Scale = new Field<decimal>()
            {
                ColumnName = "DATA_SCALE",
                Read = reader => reader.Field<decimal>((int)ModelTableColumns_11.DATA_SCALE)
            };

            public static Field<string> Nullable = new Field<string>()
            {
                ColumnName = "NULLABLE",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.NULLABLE)
            };

            public static Field<decimal> DefaultLength = new Field<decimal>()
            {
                ColumnName = "DEFAULT_LENGTH",
                Read = reader => reader.Field<decimal>((int)ModelTableColumns_11.DEFAULT_LENGTH)
            };

            //public static Field<object> DataDefault = new Field<object>()
            //{
            //    ColumnName = "DATA_DEFAULT",
            //    Read = reader => reader.Field<object>((int)ModelTableColumns.DATA_DEFAULT)
            //};

            public static Field<string> CharactereSetName = new Field<string>()
            {
                ColumnName = "CHARACTER_SET_NAME",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.CHARACTER_SET_NAME)
            };

            public static Field<string> DataUpgraded = new Field<string>()
            {
                ColumnName = "DATA_UPGRADED",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.DATA_UPGRADED)
            };

            public static Field<string> Char_Used = new Field<string>()
            {
                ColumnName = "CHAR_USED",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.Char_Used)
            };

            public static Field<string> CHAR_COL_DECL_LENGTH = new Field<string>()
            {
                ColumnName = "CHAR_COL_DECL_LENGTH",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.CHAR_COL_DECL_LENGTH)
            };

            public static Field<string> CHAR_LENGTH = new Field<string>()
            {
                ColumnName = "CHAR_LENGTH",
                Read = reader => reader.Field<string>((int)ModelTableColumns_11.CHAR_LENGTH)
            };

        }

        #region Readers

        public override void Read(IDataReader r, ModelTable_11 item)
        {
            item.SchemaName = TableDescriptor_11.Columns.Owner.Read(r);
            item.TableName = TableDescriptor_11.Columns.TableName.Read(r);
            item.ColumnName = TableDescriptor_11.Columns.ColumnName.Read(r);
            item.ColumnId = TableDescriptor_11.Columns.ColumnId.Read(r).ToInt32();
            item.DataType = TableDescriptor_11.Columns.DataType.Read(r);
            item.Comments = TableDescriptor_11.Columns.Comments.Read(r);
            item.DataLenght = TableDescriptor_11.Columns.DataLength.Read(r).ToInt32();
            item.DataPrecision = TableDescriptor_11.Columns.DataPrecision.Read(r).ToInt32();
            item.Scale = TableDescriptor_11.Columns.Scale.Read(r).ToInt32();
            item.Nullable = TableDescriptor_11.Columns.Nullable.Read(r).ToBoolean();
            item.DefaultLength = TableDescriptor_11.Columns.DefaultLength.Read(r).ToInt32();
            //item.DataDefault = TableDescriptor.Columns.DataDefault.Read(r);
            item.CharactereSetName = TableDescriptor_11.Columns.CharactereSetName.Read(r);
            item.DataUpgraded = TableDescriptor_11.Columns.DataUpgraded.Read(r).ToBoolean();
            item.CharUsed = TableDescriptor_11.Columns.Char_Used.Read(r);
            item.CHAR_COL_DECL_LENGTH = TableDescriptor_11.Columns.CHAR_COL_DECL_LENGTH.Read(r);
            item.CHAR_LENGTH = TableDescriptor_11.Columns.CHAR_LENGTH.Read(r);
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.TableName;
            yield return Columns.ColumnId;
            yield return Columns.DataType;
            yield return Columns.Comments;
            yield return Columns.DataLength;
            yield return Columns.DataPrecision;
            yield return Columns.Scale;
            yield return Columns.Nullable;
            yield return Columns.DefaultLength;
            //yield return Columns.DataDefault;
            yield return Columns.CharactereSetName;
            yield return Columns.DataUpgraded;
            yield return Columns.Char_Used;
            yield return Columns.CHAR_COL_DECL_LENGTH;
            yield return Columns.CHAR_LENGTH;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ModelTable_11> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum ModelTableColumns_11
    {
        OWNER,
        TABLE_NAME,
        COLUMN_NAME,
        COLUMN_ID,
        DATA_TYPE,
        COMMENTS,
        DATA_LENGTH,
        DATA_PRECISION,
        DATA_SCALE,
        NULLABLE,
        DEFAULT_LENGTH,
        //DATA_DEFAULT,
        CHARACTER_SET_NAME,
        DATA_UPGRADED,
        Char_Used,
        CHAR_COL_DECL_LENGTH,
        CHAR_LENGTH,
    }

    public class ModelTable_11
    {

        public string SchemaName { get; set; }

        public string TableName { get; set; }

        public int ColumnId { get; set; }

        public string ColumnName { get; set; }

        public string DataType { get; set; }

        public int DataLenght { get; set; }

        public int DataPrecision { get; set; }

        public bool Nullable { get; set; }

        public int DefaultLength { get; set; }

        public object DataDefault { get; set; }

        public string CharactereSetName { get; set; }

        public bool DataUpgraded { get; set; }

        public int Scale { get; set; }

        public string Comments { get; set; }

        public string CharUsed { get; set; }

        public string CHAR_COL_DECL_LENGTH { get; set; }

        public string CHAR_LENGTH { get; set; }

    }

}
