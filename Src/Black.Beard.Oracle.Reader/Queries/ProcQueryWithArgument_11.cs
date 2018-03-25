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

    public class ProcQueryWithArgument_11 : DbQueryBase<ModelProcWithArgument_11>
    {

        string sql =@"
SELECT 
    t.subprogram_id, 
    t.PACKAGE_NAME, 
    t.OBJECT_NAME,
    t.ARGUMENT_NAME, 
    t.POSITION, 
    t.DATA_TYPE, 
    t.DATA_LENGTH, 
    t.DEFAULTED, 
    t.IN_OUT,
    t.DEFAULT_VALUE, 
    t.DEFAULT_LENGTH, 
    t.DATA_PRECISION, 
    t.SEQUENCE, 
    T.DATA_LEVEL, 
    t.OWNER, 
    t.TYPE_OWNER, 
    t.TYPE_NAME, 
    t.OBJECT_ID
FROM DBA_ARGUMENTS t 
{0}
ORDER BY t.SUBPROGRAM_ID, t.PACKAGE_NAME, t.OBJECT_NAME, t.SEQUENCE, t.IN_OUT
";

        public override List<ModelProcWithArgument_11> Resolve(DbContextOracle context, Action<ModelProcWithArgument_11> action)
        {

            this.OracleContext = context;
            List<ModelProcWithArgument_11> List = new List<ModelProcWithArgument_11>();
            string colName = string.Empty;
            var db = context.database;

            HashSet<object> _h = new HashSet<object>();
            if (action == null)
                action =
                t =>
                {

                    ProcedureModel proc = null;

                    if (!context.Use(t.PackageName) && !string.IsNullOrEmpty(t.Owner))
                        return;

                    if (t.ObjectName.ExcludIfStartwith(t.Owner, Models.Configurations.ExcludeKindEnum.Procedure))
                        return;

                    if (t.PackageName == t.ObjectName)
                        return;

                    string key = t.ObjectId.ToString() + ":" + t.PackageName + "::" + t.Owner + "." + t.ObjectName;

                    if (!db.Procedures.TryGet(key, out proc))
                    {

                        proc = new ProcedureModel()
                        {
                            Owner = t.Owner,
                            SubProgramId = t.subprogram_id,
                            PackageName = t.PackageName,
                            Name = t.ObjectName,
                            Key = key,
                            IsFunction = false,
                        };

                        proc.ResultType.Type.Owner = t.TypeOwner;
                        proc.ResultType.Type.Name = t.TypeName;

                        db.Procedures.Add(proc);

                    }
                   
                    int index = proc.Arguments.Count;
                    colName = (!string.IsNullOrEmpty(t.ArgumentName) ? t.ArgumentName : "arg" + index.ToString());
                    OracleType _type = null;

                    if (colName != "arg0")
                    {

                        var arg = new ArgumentModel()
                        {
                            Key = (proc.Arguments.Count + 1).ToString(),
                            Name = colName,
                            In = t.In,
                            Out = t.Out,
                            Position = t.Position,
                            Sequence = t.Sequence,
                            IsValid = true,      // !string.IsNullOrEmpty(t.ArgumentName)
                        };
                        _type = arg.Type;
                        proc.Arguments.Add(arg);

                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(t.DataType))
                        {

                            var c = new ColumnModel()
                            {
                                Key = (proc.ResultType.Columns.Count + 1).ToString(),
                                Name = colName,
                                ColumnId = t.Position,
                            };
                            _type = c.Type;
                            proc.ResultType.Columns.Add(c);

                        }                      

                    }

                    if (_type != null)
                    {

                        _type.DataDefault = t.DataDefault != null ? t.DataDefault.ToString()?.Trim() : string.Empty;
                        _type.DataLength = t.DataLength;
                        _type.DataPrecision = t.DataPrecision;
                        _type.DataType = t.DataType;
                        _type.defaultLength = t.DefaultLength;
                        _type.DataLevel = t.Data_Level;
                        _type.Owner = t.TypeOwner;
                        _type.Name = t.TypeName;

                        if (_type.DataType != null)
                        {

                            _type.DbType = TypeMatchExtension.ConvertToDbType(t.DataType).ToString();

                            if (t.DataType.StartsWith("PL/SQL"))
                                _type.IsRecord = t.DataType == "PL/SQL RECORD";

                            else if (_type.DataType == "TABLE")
                                _type.IsRecord = true;

                        }

                    }

                    proc.IsFunction = proc.ResultType.Columns.Count > 0 && (proc.ResultType.Columns.OfType<ColumnModel>().Count(col => col.Name == "arg0") == proc.ResultType.Columns.Count);

                };

            string _sql = string.Format(sql, ProcQueryWhereCondition);
            ProcDescriptorWithArgument_11 view = new ProcDescriptorWithArgument_11(context.Manager.ConnectionString);

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, _sql, QueryBase.DbParams.ToArray()))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class ProcDescriptorWithArgument_11 : StructureDescriptorTable<ModelProcWithArgument_11>
    {

        public ProcDescriptorWithArgument_11(string connectionString)
            : base(() => new ModelProcWithArgument_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<decimal> subprogram_id = new Field<decimal>()
            {
                ColumnName = "SUBPROGRAM_ID",
                CharactereSetName = "",
                ColumnId = (int)ProcWithArgumentColumns_11.SUBPROGRAM_ID,
                DataDefault = "",
                DataLength = 1,
                DataPrecision = 1,
                DataType = "",
                DataUpgraded = true,
                DefaultLength = 0,
                Nullable = true,

                Read = reader => reader.Field<decimal>((int)ProcWithArgumentColumns_11.SUBPROGRAM_ID),

            };

            public static Field<string> PACKAGE_NAME = new Field<string>()
            {
                ColumnName = "PACKAGE_NAME",
                CharactereSetName = "",
                ColumnId = (int)ProcWithArgumentColumns_11.PACKAGE_NAME,
                DataDefault = "",
                DataLength = 1,
                DataPrecision = 1,
                DataType = "",
                DataUpgraded = true,
                DefaultLength = 0,
                Nullable = true,

                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.PACKAGE_NAME),

            };

            public static Field<string> OBJECT_NAME = new Field<string>()
            {
                ColumnName = "OBJECT_NAME",
                CharactereSetName = "",
                ColumnId = (int)ProcWithArgumentColumns_11.OBJECT_NAME,
                DataDefault = "",
                DataLength = 1,
                DataPrecision = 1,
                DataType = "",
                DataUpgraded = true,
                DefaultLength = 0,
                Nullable = true,

                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.OBJECT_NAME),

            };

            public static Field<string> ARGUMENT_NAME = new Field<string>()
            {
                ColumnName = "ARGUMENT_NAME",
                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.ARGUMENT_NAME)
            };

            public static Field<decimal> POSITION = new Field<decimal>()
            {
                ColumnName = "POSITION",
                Read = reader => reader.Field<decimal>((int)ProcWithArgumentColumns_11.POSITION)
            };

            public static Field<string> DataType = new Field<string>()
            {
                ColumnName = "DATA_TYPE",
                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.DATA_TYPE)
            };

            public static Field<decimal> DataLength = new Field<decimal>()
            {
                ColumnName = "DATA_LENGTH",
                Read = reader => reader.Field<decimal>((int)ProcWithArgumentColumns_11.DATA_LENGTH)
            };

            public static Field<string> Defaulted = new Field<string>()
            {
                ColumnName = "DEFAULTED",
                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.DEFAULTED)
            };

            public static Field<string> IN_OUT = new Field<string>()
            {
                ColumnName = "IN_OUT",
                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.IN_OUT)
            };

            public static Field<object> DataDefault = new Field<object>()
            {
                ColumnName = "DEFAULT_VALUE",
                Read = reader => reader.Field<object>((int)ProcWithArgumentColumns_11.DEFAULT_VALUE)
            };

            public static Field<decimal> DefaultLength = new Field<decimal>()
            {
                ColumnName = "DEFAULT_LENGTH",
                Read = reader => reader.Field<decimal>((int)ProcWithArgumentColumns_11.DEFAULT_LENGTH)
            };

            public static Field<decimal> DataPrecision = new Field<decimal>()
            {
                ColumnName = "DATA_PRECISION",
                Read = reader => reader.Field<decimal>((int)ProcWithArgumentColumns_11.DATA_PRECISION)
            };

            public static Field<decimal> Sequence = new Field<decimal>()
            {
                ColumnName = "SEQUENCE",
                Read = reader => reader.Field<decimal>((int)ProcWithArgumentColumns_11.SEQUENCE)
            };

            public static Field<decimal> DataLevel = new Field<decimal>()
            {
                ColumnName = "DATA_LEVEL",
                Read = reader => reader.Field<decimal>((int)ProcWithArgumentColumns_11.DATA_LEVEL)
            };

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.OWNER)
            };

            public static Field<string> TYPE_OWNER = new Field<string>()
            {
                ColumnName = "TYPE_OWNER",
                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.TYPE_OWNER)
            };

            public static Field<string> TYPE_NAME = new Field<string>()
            {
                ColumnName = "TYPE_NAME",
                Read = reader => reader.Field<string>((int)ProcWithArgumentColumns_11.TYPE_NAME)
            };

            public static Field<int> OBJECT_ID = new Field<int>()
            {
                ColumnName = "OBJECT_ID",
                Read = reader => reader.Field<int>((int)ProcWithArgumentColumns_11.OBJECT_ID)
            };

            // 
        }

        #region Readers

        public override void Read(IDataReader r, ModelProcWithArgument_11 item)
        {
            item.subprogram_id = ProcDescriptorWithArgument_11.Columns.subprogram_id.Read(r).ToInt32();
            item.PackageName = ProcDescriptorWithArgument_11.Columns.PACKAGE_NAME.Read(r);
            item.ObjectName = ProcDescriptorWithArgument_11.Columns.OBJECT_NAME.Read(r);
            item.ArgumentName = ProcDescriptorWithArgument_11.Columns.ARGUMENT_NAME.Read(r);
            item.Position = ProcDescriptorWithArgument_11.Columns.POSITION.Read(r).ToInt32();
            item.DataType = ProcDescriptorWithArgument_11.Columns.DataType.Read(r);
            item.DataLength = ProcDescriptorWithArgument_11.Columns.DataLength.Read(r).ToInt32();
            item.Defaulted = ProcDescriptorWithArgument_11.Columns.Defaulted.Read(r).ToBoolean();
            item.In = ProcDescriptorWithArgument_11.Columns.IN_OUT.Read(r).Contains("IN");
            item.Out = ProcDescriptorWithArgument_11.Columns.IN_OUT.Read(r).Contains("OUT");
            item.DataDefault = ProcDescriptorWithArgument_11.Columns.DataDefault.Read(r);
            item.DefaultLength = ProcDescriptorWithArgument_11.Columns.DefaultLength.Read(r).ToInt32();
            item.DataPrecision = ProcDescriptorWithArgument_11.Columns.DataPrecision.Read(r).ToInt32();
            item.Sequence = ProcDescriptorWithArgument_11.Columns.Sequence.Read(r).ToInt32();
            item.Data_Level = ProcDescriptorWithArgument_11.Columns.DataLevel.Read(r).ToInt32();
            item.Owner = ProcDescriptorWithArgument_11.Columns.Owner.Read(r);
            item.TypeOwner = ProcDescriptorWithArgument_11.Columns.TYPE_OWNER.Read(r);
            item.TypeName = ProcDescriptorWithArgument_11.Columns.TYPE_NAME.Read(r);
            item.ObjectId = ProcDescriptorWithArgument_11.Columns.OBJECT_ID.Read(r);
        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.PACKAGE_NAME;
            yield return Columns.OBJECT_NAME;
            yield return Columns.ARGUMENT_NAME;
            yield return Columns.POSITION;
            yield return Columns.DataType;
            yield return Columns.DataLength;
            yield return Columns.Defaulted;
            yield return Columns.IN_OUT;
            yield return Columns.DataDefault;
            yield return Columns.DefaultLength;
            yield return Columns.DataPrecision;
            yield return Columns.Sequence;
            yield return Columns.DataLevel;
            yield return Columns.TYPE_OWNER;
            yield return Columns.TYPE_NAME;
            yield return Columns.OBJECT_ID;


        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ModelProcWithArgument_11> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum ProcWithArgumentColumns_11
    {
        SUBPROGRAM_ID,
        PACKAGE_NAME,
        OBJECT_NAME,
        ARGUMENT_NAME,
        POSITION,
        DATA_TYPE,
        DATA_LENGTH,
        DEFAULTED,
        IN_OUT,
        DEFAULT_VALUE,
        DEFAULT_LENGTH,
        DATA_PRECISION,
        SEQUENCE,
        DATA_LEVEL,
        OWNER,
        TYPE_OWNER,
        TYPE_NAME,
        OBJECT_ID,
    }

    public class ModelProcWithArgument_11
    {



        public string PackageName { get; set; }

        public string ObjectName { get; set; }

        public string ArgumentName { get; set; }

        public int Position { get; set; }

        public string DataType { get; set; }

        public int DataLength { get; set; }

        public bool In { get; set; }

        public bool Out { get; set; }

        public int DefaultLength { get; set; }

        public int DataPrecision { get; set; }

        public bool Defaulted { get; set; }

        public object DataDefault { get; set; }

        public int subprogram_id { get; set; }

        public int Sequence { get; set; }

        public int Data_Level { get; set; }

        public string Owner { get; set; }

        public string TypeOwner { get; set; }

        public string TypeName { get; set; }

        public int ObjectId { get; set; }


    }

}
