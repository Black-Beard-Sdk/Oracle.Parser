using Pssa.Sdk.DataAccess.Dao;
using Pssa.Sdk.DataAccess.Dao.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Pssa.Tools.Databases.Generators.Queries.Oracle
{

    public class OwnerNameQuery : DbQueryBase<OwnerNameQueryTable>
    {

        string sql = @"SELECT DISTINCT OWNER FROM SYS.ALL_OBJECTS ORDER BY OWNER";

        public override List<OwnerNameQueryTable> Resolve(DbContextOracle context, Action<OwnerNameQueryTable> action)
        {

            List<OwnerNameQueryTable> List = new List<OwnerNameQueryTable>();
            HashSet<string> excluded = new HashSet<string> { "EXFSYS", "MDSYS", "PUBLIC", "OUTLN", "CTXSYS", "HR", "FLOWS_FILES", "SYSTEM", "ORACLE_OCM", "DBSNMP", "APPQOSSYS", "XDB", "SYS" , "ORDSYS", "ORDPLUGINS" , "SYSMAN", "ORDDATA", "SI_INFORMTN_SCHEMA", "PERFSTAT" };
            this.OracleContext = context;
            var db = context.database;

            StringBuilder _owners = new StringBuilder(100);

            if (action == null)
                action =
                t =>
                {
                    if (!excluded.Contains(t.OWNER) && !t.OWNER.StartsWith("APEX_"))
                    {
                        _owners.Append(t.OWNER);
                        _owners.Append(";");
                    }
                };

            OwnerNameQueryDescriptor OwnerName = new OwnerNameQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("l"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = OwnerName.ReadAll(reader, action).ToList();
            }

            if (_owners.Length > 0)
            {
                _owners.Remove(_owners.Length - 1, 1);
                db.AvailableOwner = _owners.ToString();
            }

            return List;

        }

    }

    public class OwnerNameQueryDescriptor : StructureDescriptorTable<OwnerNameQueryTable>
    {

        public OwnerNameQueryDescriptor(string connectionString)
            : base(() => new OwnerNameQueryTable(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> OWNER = new Field<string>() { ColumnName = "OWNER", Read = reader => reader.Field<string>((int)OwnerNameQueryColumns.OWNER) };

        }

        #region Readers

        public override void Read(IDataReader r, OwnerNameQueryTable item)
        {
            item.OWNER = OwnerNameQueryDescriptor.Columns.OWNER.Read(r);
        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.OWNER;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<OwnerNameQueryTable> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum OwnerNameQueryColumns
    {
        OWNER,
    }

    public class OwnerNameQueryTable
    {

        public string OWNER { get; set; }

    }

}
