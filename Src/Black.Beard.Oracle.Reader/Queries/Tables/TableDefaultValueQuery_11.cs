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


    /*
     * 
     * DECLARE
     * long_var LONG;
     * var_var  VARCHAR2(2000);
     * BEGIN
     *    SELECT text_column INTO long_var
     *    FROM table_with_long
     *    WHERE rownum < 2;
     *    var_var := substr(long_var,1,2000);
     *    INSERT INTO table_b
     *    VALUES (var_var);
     * END;
     * 
     */

    public class TableDefaultValueQuery_11 : DbQueryBase<TableDefaultValueTable_11>
    {

        string sql =
@"
SELECT t.OWNER, t.TABLE_NAME, t.COLUMN_NAME, t.DATA_TYPE, t.DEFAULT_LENGTH, t.DATA_DEFAULT
FROM DBA_TAB_COLS t
WHERE t.DATA_DEFAULT is not NULL {0}
";
        public override List<TableDefaultValueTable_11> Resolve(DbContextOracle context, Action<TableDefaultValueTable_11> action)
        {
            List<TableDefaultValueTable_11> List = new List<TableDefaultValueTable_11>();
            var db = context.Database;
            this.OracleContext = context;

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
                        if (db.Tables.TryGet(key, out table))
                        {

                            if (t.DefaultLenght > 0)
                            {
                                if (table != null)
                                {

                                    var c = table.Columns.OfType<ColumnModel>().FirstOrDefault(d => d.Name == t.ColumnName);
                                    if (c != null)
                                    {
                                        var d = t.DefaultValue;
                                        if (d != null)
                                        {

                                            c.Type.DataDefault = d.ToString()?.Trim();
                                            c.Type.defaultLength = c.Type.DataDefault.Length;

                                        //if (c.Type.DataType.ToUpper().Equals("NUMBER"))
                                        //{                                           
                                        //    Regex digitsOnly = new Regex(@"[^\d]");
                                        //    c.Type.DataDefault = digitsOnly.Replace(d.ToString(), "").PadLeft(c.Type.defaultLength, '0');
                                        //}
                                        //else
                                        //{
                                        //    c.Type.DataDefault = defaultvalue;
                                        //}

                                        }
                                    }

                                }
                            }

                        }
                    };


            TableDefaultValueDescriptor_11 view = new TableDefaultValueDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryAndCondition());

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class TableDefaultValueDescriptor_11 : StructureDescriptorTable<TableDefaultValueTable_11>
    {

        public TableDefaultValueDescriptor_11(string connectionString)
            : base(() => new TableDefaultValueTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)TableDefaultValueTableColumns_11.OWNER)
            };

            public static Field<string> TableName = new Field<string>()
            {
                ColumnName = "TABLE_NAME",
                Read = reader => reader.Field<string>((int)TableDefaultValueTableColumns_11.TABLE_NAME)
            };

            public static Field<string> ColumnName = new Field<string>()
            {
                ColumnName = "COLUMN_NAME",
                Read = reader => reader.Field<string>((int)TableDefaultValueTableColumns_11.COLUMN_NAME)
            };

            public static Field<string> DataType = new Field<string>()
            {
                ColumnName = "DATA_TYPE",
                Read = reader => reader.Field<string>((int)TableDefaultValueTableColumns_11.DATA_TYPE)
            };
            public static Field<decimal> DefaultLenght = new Field<decimal>()
            {
                ColumnName = "DEFAULT_LENGTH",
                Read = reader => reader.Field<decimal>((int)TableDefaultValueTableColumns_11.DEFAULT_LENGTH)
            };
            public static Field<object> DefaultValue = new Field<object>()
            {
                ColumnName = "DEFAULT_VALUE",
                Read = reader => reader.Field<object>((int)TableDefaultValueTableColumns_11.DEFAULT_VALUE)
            };
        }

        #region Readers

        public override void Read(IDataReader r, TableDefaultValueTable_11 item)
        {
            item.SchemaName = TableDefaultValueDescriptor_11.Columns.Owner.Read(r);
            item.TableName = TableDefaultValueDescriptor_11.Columns.TableName.Read(r);
            item.ColumnName = TableDefaultValueDescriptor_11.Columns.ColumnName.Read(r);
            item.DataType = TableDefaultValueDescriptor_11.Columns.DataType.Read(r);
            item.DefaultLenght = TableDefaultValueDescriptor_11.Columns.DefaultLenght.Read(r).ToInt32();
            item.DefaultValue = TableDefaultValueDescriptor_11.Columns.DefaultValue.Read(r);
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.TableName;
            yield return Columns.ColumnName;
            yield return Columns.DataType;
            yield return Columns.DefaultLenght;
            yield return Columns.DefaultValue;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<TableDefaultValueTable_11> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum TableDefaultValueTableColumns_11
    {
        OWNER,
        TABLE_NAME,
        COLUMN_NAME,
        DATA_TYPE,
        DEFAULT_LENGTH,
        DEFAULT_VALUE,
    }

    public class TableDefaultValueTable_11
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public int DefaultLenght { get; set; }
        public object DefaultValue { get; set; }
    }

}
