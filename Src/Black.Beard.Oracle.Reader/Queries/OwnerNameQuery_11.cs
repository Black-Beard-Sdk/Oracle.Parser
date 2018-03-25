using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Reader.Queries
{

    public class OwnerNameQuery_11 : DbQueryBase<OwnerNameQueryTable_11>
    {

        string sql = @"SELECT DISTINCT OWNER FROM SYS.ALL_OBJECTS ORDER BY OWNER";

        public override List<OwnerNameQueryTable_11> Resolve(DbContextOracle context, Action<OwnerNameQueryTable_11> action)
        {

            List<OwnerNameQueryTable_11> List = new List<OwnerNameQueryTable_11>();
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

            OwnerNameQueryDescriptor_11 OwnerName = new OwnerNameQueryDescriptor_11(context.Manager.ConnectionString);
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

    public class OwnerNameQueryDescriptor_11 : StructureDescriptorTable<OwnerNameQueryTable_11>
    {

        public OwnerNameQueryDescriptor_11(string connectionString)
            : base(() => new OwnerNameQueryTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> OWNER = new Field<string>() { ColumnName = "OWNER", Read = reader => reader.Field<string>((int)OwnerNameQueryColumns_11.OWNER) };

        }

        #region Readers

        public override void Read(IDataReader r, OwnerNameQueryTable_11 item)
        {
            item.OWNER = OwnerNameQueryDescriptor_11.Columns.OWNER.Read(r);
        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.OWNER;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<OwnerNameQueryTable_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum OwnerNameQueryColumns_11
    {
        OWNER,
    }

    public class OwnerNameQueryTable_11
    {

        public string OWNER { get; set; }

    }

}
