using Bb.Beard.Oracle.Reader;
using Bb.Beard.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class ProcQuery : DbQueryBase<ModelProc>
    {

        string sql =
@"
SELECT t.OWNER, t.OBJECT_NAME, t.PROCEDURE_NAME, t.OBJECT_ID
FROM dba_procedures t 
WHERE NOT EXISTS(SELECT 1 FROM dba_arguments a WHERE a.OBJECT_ID = t.OBJECT_ID) AND (t.OBJECT_TYPE = 'PROCEDURE' OR t.OBJECT_TYPE = 'FUNCTION')
{0}
";

        public override List<ModelProc> Resolve(DbContextOracle context, Action<ModelProc> action)
        {
            List<ModelProc> List = new List<ModelProc>();
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
                            SchemaName = t.OWNER,
                            PackageName = string.IsNullOrEmpty(t.PROCEDURE_NAME) ? string.Empty : t.OBJECT_NAME,
                            Name = string.IsNullOrEmpty(t.PROCEDURE_NAME) ? t.OBJECT_NAME : t.PROCEDURE_NAME,
                            Key = key,
                            Filename = string.Empty,
                            IsFunction = false,
                        };

                        db.Procedures.Add(proc);

                    }
                    else
                    {

                    }

                };

            sql = string.Format(sql, TableQueryAndCondition());
            ProcDescriptor view = new ProcDescriptor(context.Manager.ConnectionString);

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class ProcDescriptor : StructureDescriptorTable<ModelProc>
    {

        public ProcDescriptor(string connectionString)
            : base(() => new ModelProc(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> OBJECT_NAME = new Field<string>()
            {
                ColumnName = "OBJECT_NAME",
                CharactereSetName = "",
                ColumnId = (int)ProcColumns.OBJECT_NAME,
                DataDefault = "",
                DataLength = 1,
                DataPrecision = 1,
                DataType = "",
                DataUpgraded = true,
                DefaultLength = 0,
                Nullable = true,

                Read = reader => reader.Field<string>((int)ProcColumns.OBJECT_NAME),

            };


            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)ProcColumns.OWNER)
            };

            public static Field<string> ProcedureName = new Field<string>()
            {
                ColumnName = "PROCEDURE_NAME",
                Read = reader => reader.Field<string>((int)ProcColumns.PROCEDURE_NAME)
            };

            public static Field<int> ObjectId = new Field<int>()
            {
                ColumnName = "OBJECT_ID",
                Read = reader => reader.Field<int>((int)ProcColumns.OBJECT_ID)
            };

            // 
        }

        #region Readers

        public override void Read(IDataReader r, ModelProc item)
        {
            item.OWNER = ProcDescriptor.Columns.Owner.Read(r);
            item.OBJECT_NAME = ProcDescriptor.Columns.OBJECT_NAME.Read(r);
            item.PROCEDURE_NAME = ProcDescriptor.Columns.ProcedureName.Read(r);
            item.ObjectId = ProcDescriptor.Columns.ObjectId.Read(r);
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.OBJECT_NAME;
            yield return Columns.ProcedureName;
            yield return Columns.ObjectId;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ModelProc> entities)
        {
            throw new NotImplementedException();
        }

    }

    public enum ProcColumns
    {
        OWNER,
        OBJECT_NAME,
        PROCEDURE_NAME,
        OBJECT_ID,
    }

    public class ModelProc
    {


        public string OWNER { get; set; }
        public string OBJECT_NAME { get; set; }
        public string PROCEDURE_NAME { get; set; }
        public int ObjectId { get; set; }

    }

}
