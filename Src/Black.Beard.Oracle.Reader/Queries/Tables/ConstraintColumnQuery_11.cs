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

    public class ConstraintColumnQuery_11 : DbQueryBase<ConstraintColumnTable_11>
    {

        string sql =
@"
SELECT t.OWNER, t.CONSTRAINT_NAME, t.TABLE_NAME, t.COLUMN_NAME, t.POSITION 
FROM dba_Cons_Columns t
{0}
ORDER BY t.OWNER, t.TABLE_NAME, t.CONSTRAINT_NAME
";

        public override List<ConstraintColumnTable_11> Resolve(DbContextOracle context, Action<ConstraintColumnTable_11> action)
        {

            this.OracleContext = context;
            List<ConstraintColumnTable_11> list = new List<ConstraintColumnTable_11>();
            var db = context.database;

            if (action == null)
                action = t =>
            {

                if (!context.Use(t.Owner))
                    return;

                if (t.TableName.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.Table))
                    return;

                string keyTable = t.Owner + "." + t.TableName;

                TableModel table;

                if (db.Tables.TryGet(keyTable, out table))
                {

                    string keyContraint = t.Owner + "." + t.CONSTRAINT_NAME;

                    var ct = table.Constraints[keyContraint];
                    if (ct != null)
                    {

                        ct.Columns.Add(new ConstraintColumnModel() { ColumnName = t.ColumnName, Position = t.Position });

                        // Primary Key check
                        if (ct.Type.Equals("P"))
                        {
                            //table.Columns[t.ColumnName].IsPrimaryKey = true;
                            SetPrimaryKeyColumn(table.Columns, t.ColumnName);
                        }

                        // Foreign Key check
                        if (ct.Type.Equals("F"))
                        {
                            SetForeignKeyColumn(table.Columns, t.ColumnName);
                        }

                    }
                }

            };


            ConstraintColumnDescriptor_11 view = new ConstraintColumnDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition());

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                list = view.ReadAll(reader, action).ToList();
            }

            return list;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="columnName"></param>
        void SetPrimaryKeyColumn(ColumnCollection columns, string columnName)
        {
            foreach (ColumnModel column in columns)
            {
                if (column.Name == columnName)
                {
                    column.IsPrimaryKey = true;
                    break;
                }
            }
        }


        void SetForeignKeyColumn(ColumnCollection columns, string columnName)
        {
            foreach (ColumnModel column in columns)
            {
                if (column.Name == columnName)
                {
                    column.ForeignKey.IsForeignKey = true;
                    break;
                }
            }
        }

    }

    public class ConstraintColumnDescriptor_11 : StructureDescriptorTable<ConstraintColumnTable_11>
    {

        public ConstraintColumnDescriptor_11(string connectionString)
            : base(() => new ConstraintColumnTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> OWNER = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)ForeignTableColumns_11.Owner)
            };

            public static Field<string> CONSTRAINT_NAME = new Field<string>()
            {
                ColumnName = "CONSTRAINT_NAME",
                Read = reader => reader.Field<string>((int)ForeignTableColumns_11.CONSTRAINT_NAME)
            };

            public static Field<string> TableName = new Field<string>()
            {
                ColumnName = "TABLE_NAME",
                Read = reader => reader.Field<string>((int)ForeignTableColumns_11.TABLE_NAME)
            };

            public static Field<string> ColumnName = new Field<string>()
            {
                ColumnName = "COLUMN_NAME",
                Read = reader => reader.Field<string>((int)ForeignTableColumns_11.COLUMN_NAME)
            };

            public static Field<decimal> Position = new Field<decimal>()
            {
                ColumnName = "POSITION",
                Read = reader => reader.Field<decimal>((int)ForeignTableColumns_11.POSITION)
            };

        }

        #region Readers

        public override void Read(IDataReader r, ConstraintColumnTable_11 item)
        {
            item.Owner = ConstraintColumnDescriptor_11.Columns.OWNER.Read(r);
            item.CONSTRAINT_NAME = ConstraintColumnDescriptor_11.Columns.CONSTRAINT_NAME.Read(r);
            item.TableName = ConstraintColumnDescriptor_11.Columns.TableName.Read(r);
            item.ColumnName = ConstraintColumnDescriptor_11.Columns.ColumnName.Read(r);
            item.Position = ConstraintColumnDescriptor_11.Columns.Position.Read(r).ToInt32();
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.OWNER;
            yield return Columns.CONSTRAINT_NAME;
            yield return Columns.TableName;
            yield return Columns.ColumnName;
            yield return Columns.Position;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ConstraintColumnTable_11> entities)
        {
            throw new NotImplementedException();
        }



    }

    public enum ForeignTableColumns_11
    {
        Owner,
        CONSTRAINT_NAME,
        TABLE_NAME,
        COLUMN_NAME,
        POSITION,
    }

    public class ConstraintColumnTable_11
    {
        public string Owner { get; set; }
        public string CONSTRAINT_NAME { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int Position { get; set; }
    }



}
