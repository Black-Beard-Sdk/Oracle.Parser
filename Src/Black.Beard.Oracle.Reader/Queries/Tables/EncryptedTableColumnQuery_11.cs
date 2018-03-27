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

    public class EncryptedTableColumnQuery_11 : DbQueryBase<EncryptedTableColumnDto_11>
    {

        string sql =
@"
 SELECT 
  e.OWNER, 
  e.TABLE_NAME, 
  e.COLUMN_NAME, 
  e.ENCRYPTION_ALG, 
  e.SALT, 
  e.INTEGRITY_ALG
 FROM SYS.DBA_ENCRYPTED_COLUMNS e
{0}

";

        public override List<EncryptedTableColumnDto_11> Resolve(DbContextOracle context, Action<EncryptedTableColumnDto_11> action)
        {

            List<EncryptedTableColumnDto_11> List = new List<EncryptedTableColumnDto_11>();
            this.OracleContext = context;
            var db = context.Database;

            if (action == null)
                action =
                    t =>
                    {

                        if (t.TableName.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.Table))
                            return;

                        string key = t.Owner + "." + t.TableName;
                        TableModel table;

                        if (db.Tables.TryGet(key, out table))
                        {
                            var u = table.Columns[t.ColumnName];
                            if (u != null)
                            {
                                u.EncryptionAlg = t.EncryptionAlg;
                                u.Salt = t.Salt.ToBoolean();
                                u.IntegrityAlg = t.IntegrityAlg;
                            }
                        }


                    };

            EncryptedTableColumnQueryDescriptor_11 EncryptedTableColumn = new EncryptedTableColumnQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("e", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = EncryptedTableColumn.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class EncryptedTableColumnQueryDescriptor_11 : StructureDescriptorTable<EncryptedTableColumnDto_11>
    {

        public EncryptedTableColumnQueryDescriptor_11(string connectionString)
            : base(() => new EncryptedTableColumnDto_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns_11.OWNER) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns_11.TABLE_NAME) };
            public static Field<String> ColumnName = new Field<String>() { ColumnName = "COLUMN_NAME", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns_11.COLUMN_NAME) };
            public static Field<String> EncryptionAlg = new Field<String>() { ColumnName = "ENCRYPTION_ALG", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns_11.ENCRYPTION_ALG) };
            public static Field<String> Salt = new Field<String>() { ColumnName = "SALT", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns_11.SALT) };
            public static Field<String> IntegrityAlg = new Field<String>() { ColumnName = "INTEGRITY_ALG", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns_11.INTEGRITY_ALG) };


        }

        #region Readers

        public override void Read(IDataReader r, EncryptedTableColumnDto_11 item)
        {
            item.Owner = EncryptedTableColumnQueryDescriptor_11.Columns.Owner.Read(r);
            item.TableName = EncryptedTableColumnQueryDescriptor_11.Columns.TableName.Read(r);
            item.ColumnName = EncryptedTableColumnQueryDescriptor_11.Columns.ColumnName.Read(r);
            item.EncryptionAlg = EncryptedTableColumnQueryDescriptor_11.Columns.EncryptionAlg.Read(r);
            item.Salt = EncryptedTableColumnQueryDescriptor_11.Columns.Salt.Read(r);
            item.IntegrityAlg = EncryptedTableColumnQueryDescriptor_11.Columns.IntegrityAlg.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.TableName;
            yield return Columns.ColumnName;
            yield return Columns.EncryptionAlg;
            yield return Columns.Salt;
            yield return Columns.IntegrityAlg;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<EncryptedTableColumnDto_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum EncryptedTableColumnQueryColumns_11
    {
        OWNER,
        TABLE_NAME,
        COLUMN_NAME,
        ENCRYPTION_ALG,
        SALT,
        INTEGRITY_ALG,

    }

    public class EncryptedTableColumnDto_11
    {

        public String Owner { get; set; }
        public String TableName { get; set; }
        public String ColumnName { get; set; }
        public String EncryptionAlg { get; set; }
        public String Salt { get; set; }
        public String IntegrityAlg { get; set; }


    }

}

