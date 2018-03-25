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

    public class ProcQuery_11 : DbQueryBase<ModelProc_11>
    {

        string sql =
@"
SELECT t.OWNER, t.OBJECT_NAME, t.PROCEDURE_NAME, t.OBJECT_ID
FROM dba_procedures t 
WHERE NOT EXISTS(SELECT 1 FROM dba_arguments a WHERE a.OBJECT_ID = t.OBJECT_ID) AND (t.OBJECT_TYPE = 'PROCEDURE' OR t.OBJECT_TYPE = 'FUNCTION')
{0}
";

        public override List<ModelProc_11> Resolve(DbContextOracle context, Action<ModelProc_11> action)
        {
            List<ModelProc_11> List = new List<ModelProc_11>();
            string colName = string.Empty;
            var db = context.database;
            this.OracleContext = context;

            HashSet<object> _h = new HashSet<object>();

            if (action == null)
                action =
                t =>
                {

                    ProcedureModel proc = null;

                    if (t.OBJECT_NAME.ExcludIfStartwith(t.OWNER, Models.Configurations.ExcludeKindEnum.Procedure))
                        return;

                    if (t.OBJECT_NAME == t.PROCEDURE_NAME)
                        return;

                    string key = string.Empty;

                    if (!string.IsNullOrEmpty(t.PROCEDURE_NAME))
                        key = t.ObjectId.ToString() + t.OWNER + "::" + t.OBJECT_NAME + "." + t.PROCEDURE_NAME;
                    else
                        key = t.ObjectId.ToString() + t.OWNER + "::" + t.OBJECT_NAME;

                    if (!db.Procedures.TryGet(key, out proc))
                    {

                        proc = new ProcedureModel()
                        {
                            Owner = t.OWNER,
                            PackageName = string.IsNullOrEmpty(t.PROCEDURE_NAME) ? string.Empty : t.OBJECT_NAME,
                            Name = string.IsNullOrEmpty(t.PROCEDURE_NAME) ? t.OBJECT_NAME : t.PROCEDURE_NAME,
                            Key = key,
                            IsFunction = false,
                        };

                        db.Procedures.Add(proc);

                    }
                    else
                    {

                    }

                };

            sql = string.Format(sql, TableQueryAndCondition());
            ProcDescriptor_11 view = new ProcDescriptor_11(context.Manager.ConnectionString);

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class ProcDescriptor_11 : StructureDescriptorTable<ModelProc_11>
    {

        public ProcDescriptor_11(string connectionString)
            : base(() => new ModelProc_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> OBJECT_NAME = new Field<string>()
            {
                ColumnName = "OBJECT_NAME",
                CharactereSetName = "",
                ColumnId = (int)ProcColumns_11.OBJECT_NAME,
                DataDefault = "",
                DataLength = 1,
                DataPrecision = 1,
                DataType = "",
                DataUpgraded = true,
                DefaultLength = 0,
                Nullable = true,

                Read = reader => reader.Field<string>((int)ProcColumns_11.OBJECT_NAME),

            };


            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)ProcColumns_11.OWNER)
            };

            public static Field<string> ProcedureName = new Field<string>()
            {
                ColumnName = "PROCEDURE_NAME",
                Read = reader => reader.Field<string>((int)ProcColumns_11.PROCEDURE_NAME)
            };

            public static Field<int> ObjectId = new Field<int>()
            {
                ColumnName = "OBJECT_ID",
                Read = reader => reader.Field<int>((int)ProcColumns_11.OBJECT_ID)
            };

            // 
        }

        #region Readers

        public override void Read(IDataReader r, ModelProc_11 item)
        {
            item.OWNER = ProcDescriptor_11.Columns.Owner.Read(r);
            item.OBJECT_NAME = ProcDescriptor_11.Columns.OBJECT_NAME.Read(r);
            item.PROCEDURE_NAME = ProcDescriptor_11.Columns.ProcedureName.Read(r);
            item.ObjectId = ProcDescriptor_11.Columns.ObjectId.Read(r);
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.OBJECT_NAME;
            yield return Columns.ProcedureName;
            yield return Columns.ObjectId;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ModelProc_11> entities)
        {
            throw new NotImplementedException();
        }

    }

    public enum ProcColumns_11
    {
        OWNER,
        OBJECT_NAME,
        PROCEDURE_NAME,
        OBJECT_ID,
    }

    public class ModelProc_11
    {


        public string OWNER { get; set; }
        public string OBJECT_NAME { get; set; }
        public string PROCEDURE_NAME { get; set; }
        public int ObjectId { get; set; }

    }

}
