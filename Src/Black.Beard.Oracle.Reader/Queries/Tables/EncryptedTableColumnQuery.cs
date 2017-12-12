using Bb.Beard.Oracle.Reader;
using Bb.Beard.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class EncryptedTableColumnQuery : DbQueryBase<EncryptedTableColumnDto>
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

        public override List<EncryptedTableColumnDto> Resolve(DbContextOracle context, Action<EncryptedTableColumnDto> action)
        {

            List<EncryptedTableColumnDto> List = new List<EncryptedTableColumnDto>();
            this.OracleContext = context;
            var db = context.database;

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

            EncryptedTableColumnQueryDescriptor EncryptedTableColumn = new EncryptedTableColumnQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("e", "Owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = EncryptedTableColumn.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class EncryptedTableColumnQueryDescriptor : StructureDescriptorTable<EncryptedTableColumnDto>
    {

        public EncryptedTableColumnQueryDescriptor(string connectionString)
            : base(() => new EncryptedTableColumnDto(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<String> Owner = new Field<String>() { ColumnName = "OWNER", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns.OWNER) };
            public static Field<String> TableName = new Field<String>() { ColumnName = "TABLE_NAME", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns.TABLE_NAME) };
            public static Field<String> ColumnName = new Field<String>() { ColumnName = "COLUMN_NAME", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns.COLUMN_NAME) };
            public static Field<String> EncryptionAlg = new Field<String>() { ColumnName = "ENCRYPTION_ALG", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns.ENCRYPTION_ALG) };
            public static Field<String> Salt = new Field<String>() { ColumnName = "SALT", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns.SALT) };
            public static Field<String> IntegrityAlg = new Field<String>() { ColumnName = "INTEGRITY_ALG", Read = reader => reader.Field<String>((int)EncryptedTableColumnQueryColumns.INTEGRITY_ALG) };


        }

        #region Readers

        public override void Read(IDataReader r, EncryptedTableColumnDto item)
        {
            item.Owner = EncryptedTableColumnQueryDescriptor.Columns.Owner.Read(r);
            item.TableName = EncryptedTableColumnQueryDescriptor.Columns.TableName.Read(r);
            item.ColumnName = EncryptedTableColumnQueryDescriptor.Columns.ColumnName.Read(r);
            item.EncryptionAlg = EncryptedTableColumnQueryDescriptor.Columns.EncryptionAlg.Read(r);
            item.Salt = EncryptedTableColumnQueryDescriptor.Columns.Salt.Read(r);
            item.IntegrityAlg = EncryptedTableColumnQueryDescriptor.Columns.IntegrityAlg.Read(r);

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

        public override DataTable GetDataTable(string tableName, IEnumerable<EncryptedTableColumnDto> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum EncryptedTableColumnQueryColumns
    {
        OWNER,
        TABLE_NAME,
        COLUMN_NAME,
        ENCRYPTION_ALG,
        SALT,
        INTEGRITY_ALG,

    }

    public class EncryptedTableColumnDto
    {

        public String Owner { get; set; }
        public String TableName { get; set; }
        public String ColumnName { get; set; }
        public String EncryptionAlg { get; set; }
        public String Salt { get; set; }
        public String IntegrityAlg { get; set; }


    }

}

