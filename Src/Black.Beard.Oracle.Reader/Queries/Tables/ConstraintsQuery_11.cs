using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Reader.Queries
{

    public class ConstraintsQuery_11 : DbQueryBase<ConstraintsTable_11>
    {
        
        string sql =
@"
SELECT 
    t.OWNER, 
    t.CONSTRAINT_NAME, 
    t.CONSTRAINT_TYPE, 
    t.TABLE_NAME, 
    t.R_CONSTRAINT_NAME, 
    t.STATUS, 
    t.VALIDATED, 
    t.INDEX_NAME,

    t.DELETE_RULE,
    t.GENERATED,
    t.DEFERRABLE,
    t.DEFERRED,
    t.RELY,
    t.search_condition,
    t.VIEW_RELATED,
    t.INVALID,
    t.R_OWNER

FROM dba_constraints t
{0}
ORDER BY t.OWNER, t.TABLE_NAME, t.CONSTRAINT_NAME, t.CONSTRAINT_TYPE, t.R_CONSTRAINT_NAME, t.status, t.VALIDATED, t.INDEX_NAME, t.DELETE_RULE, t.GENERATED,t.DEFERRABLE, t.DEFERRED, t.VALIDATED, t.RELY --, t.search_condition

";
        public override List<ConstraintsTable_11> Resolve(DbContextOracle context, Action<ConstraintsTable_11> action)
        {

            List<ConstraintsTable_11> List = new List<ConstraintsTable_11>();
            this.OracleContext = context;
            var db = context.database;

            if (action == null)
                action =
                t =>
                {

                    if (!context.Use(t.SchemaName))
                        return;

                    if (t.TABLE_NAME.ExcludIfStartwith(t.SchemaName, Models.Configurations.ExcludeKindEnum.Table))
                        return;

                    string key = t.SchemaName + "." + t.TABLE_NAME;
                    TableModel table;

                    if (db.Tables.TryGet(key, out table))
                    {

                        table.Constraints.Add(new ConstraintModel()
                        {
                            Key = t.SchemaName + "." + t.CONSTRAINT_NAME,
                            Name = t.CONSTRAINT_NAME,
                            Owner = t.SchemaName,
                            Rel_Constraint_Name = t.R_CONSTRAINT_NAME,
                            IndexName = t.INDEX_NAME,
                            Type = t.CONSTRAINT_TYPE,
                            DeleteRule = t.DELETE_RULE,
                            Generated = t.GENERATED,
                            Deferrable = t.DEFERRABLE,
                            Deferred = t.DEFERRED,
                            Rely = t.RELY,
                            Rel_Constraint_Owner = t.R_OWNER,
                            Search_Condition = Utils.Serialize(t.search_condition, false),
                            ViewRelated = t.VIEW_RELATED,
                            Invalid = t.INVALID,
                            Status = t.Status,
                        });

                    }
                    else
                    {
                        // System.Diagnostics.Debugger.Break();
                    }



                };

            ConstraintDescriptor_11 view = new ConstraintDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition());

            using (var reader = context.Manager.ExecuteReader(System.Data.CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class ConstraintDescriptor_11 : StructureDescriptorTable<ConstraintsTable_11>
    {

        public ConstraintDescriptor_11(string connectionString)
            : base(() => new ConstraintsTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.OWNER),
            };

            public static Field<string> CONSTRAINT_NAME = new Field<string>()
            {
                ColumnName = "CONSTRAINT_NAME",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.CONSTRAINT_NAME)
            };

            public static Field<string> CONSTRAINT_TYPE = new Field<string>()
            {
                ColumnName = "CONSTRAINT_TYPE",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.CONSTRAINT_TYPE)
            };

            public static Field<string> TABLE_NAME = new Field<string>()
            {
                ColumnName = "TABLE_NAME",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.TABLE_NAME)
            };

            public static Field<string> R_CONSTRAINT_NAME = new Field<string>()
            {
                ColumnName = "R_CONSTRAINT_NAME",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.R_CONSTRAINT_NAME)
            };

            public static Field<string> Status = new Field<string>()
            {
                ColumnName = "status",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.STATUS)
            };

            public static Field<string> VALIDATED = new Field<string>()
            {
                ColumnName = "VALIDATED",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.VALIDATED)
            };

            public static Field<string> INDEX_NAME = new Field<string>()
            {
                ColumnName = "INDEX_NAME",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.INDEX_NAME)
            };

            public static Field<string> DELETE_RULE = new Field<string>()
            {
                ColumnName = "DELETE_RULE",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.DELETE_RULE)
            };

            public static Field<string> GENERATED = new Field<string>()
            {
                ColumnName = "GENERATED",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.GENERATED)
            };

            public static Field<string> DEFERRABLE = new Field<string>()
            {
                ColumnName = "DEFERRABLE",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.DEFERRABLE)
            };

            public static Field<string> DEFERRED = new Field<string>()
            {
                ColumnName = "DEFERRED",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.DEFERRED)
            };

            public static Field<string> RELY = new Field<string>()
            {
                ColumnName = "RELY",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.RELY)
            };

            public static Field<string> R_OWNER = new Field<string>()
            {
                ColumnName = "R_OWNER",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.R_OWNER)
            };

            public static Field<string> Search_Condition = new Field<string>()
            {
                ColumnName = "SEARCH_CONDITION",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.search_condition)
            };

            public static Field<string> VIEW_RELATED = new Field<string>()
            {
                ColumnName = "VIEW_RELATED",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.VIEW_RELATED)
            };

            public static Field<string> INVALID = new Field<string>()
            {
                ColumnName = "INVALID",
                Read = reader => reader.Field<string>((int)PrimaryTableColumns.INVALID)
            };

        }

        #region Readers

        public override void Read(System.Data.IDataReader r, ConstraintsTable_11 item)
        {
            item.SchemaName = ConstraintDescriptor_11.Columns.Owner.Read(r);
            item.CONSTRAINT_NAME = ConstraintDescriptor_11.Columns.CONSTRAINT_NAME.Read(r);
            item.CONSTRAINT_TYPE = ConstraintDescriptor_11.Columns.CONSTRAINT_TYPE.Read(r);
            item.TABLE_NAME = ConstraintDescriptor_11.Columns.TABLE_NAME.Read(r);
            item.R_CONSTRAINT_NAME = ConstraintDescriptor_11.Columns.R_CONSTRAINT_NAME.Read(r);
            item.Status = ConstraintDescriptor_11.Columns.Status.Read(r);
            item.VALIDATED = ConstraintDescriptor_11.Columns.VALIDATED.Read(r) == "VALIDATED";
            item.INDEX_NAME = ConstraintDescriptor_11.Columns.INDEX_NAME.Read(r);

            item.DELETE_RULE = ConstraintDescriptor_11.Columns.DELETE_RULE.Read(r);
            item.GENERATED = ConstraintDescriptor_11.Columns.GENERATED.Read(r);
            item.DEFERRABLE = ConstraintDescriptor_11.Columns.DEFERRABLE.Read(r);
            item.DEFERRED = ConstraintDescriptor_11.Columns.DEFERRED.Read(r);
            item.RELY = ConstraintDescriptor_11.Columns.RELY.Read(r);
            item.R_OWNER = ConstraintDescriptor_11.Columns.R_OWNER.Read(r);
            item.search_condition = ConstraintDescriptor_11.Columns.Search_Condition.Read(r);

            item.VIEW_RELATED = ConstraintDescriptor_11.Columns.VIEW_RELATED.Read(r);
            item.INVALID = ConstraintDescriptor_11.Columns.INVALID.Read(r);

        }

        #endregion Readers


        public override IEnumerable<Field> Fields()
        {
            yield return Columns.CONSTRAINT_NAME;
            yield return Columns.CONSTRAINT_TYPE;
            yield return Columns.TABLE_NAME;
            yield return Columns.R_CONSTRAINT_NAME;
            yield return Columns.Status;
            yield return Columns.VALIDATED;
            yield return Columns.INDEX_NAME;

            yield return Columns.DELETE_RULE;
            yield return Columns.GENERATED;
            yield return Columns.DEFERRABLE;
            yield return Columns.DEFERRED;
            yield return Columns.RELY;
            yield return Columns.R_OWNER;
            yield return Columns.Search_Condition;
            yield return Columns.VIEW_RELATED;
            yield return Columns.INVALID;

        }

        public enum PrimaryTableColumns
        {
            OWNER,
            CONSTRAINT_NAME,
            CONSTRAINT_TYPE,
            TABLE_NAME,
            R_CONSTRAINT_NAME,
            STATUS,
            VALIDATED,
            INDEX_NAME,
            DELETE_RULE,
            GENERATED,
            DEFERRABLE,
            DEFERRED,
            RELY,
            search_condition,
            VIEW_RELATED,
            INVALID,
            R_OWNER

        }

        public override System.Data.DataTable GetDataTable(string tableName, IEnumerable<ConstraintsTable_11> entities)
        {
            throw new NotImplementedException();
        }

    }

    public class ConstraintsTable_11
    {

        public string SchemaName { get; set; }

        public string CONSTRAINT_NAME { get; set; }

        public string CONSTRAINT_TYPE { get; set; }

        public string TABLE_NAME { get; set; }

        public string R_CONSTRAINT_NAME { get; set; }

        public string Status { get; set; }

        public bool VALIDATED { get; set; }

        public string INDEX_NAME { get; set; }

        public string DELETE_RULE { get; set; }

        public string GENERATED { get; set; }

        public string DEFERRABLE { get; set; }

        public string DEFERRED { get; set; }

        public string RELY { get; set; }

        public string R_OWNER { get; set; }

        public string search_condition { get; set; }

        public string VIEW_RELATED { get; set; }

        public string INVALID { get; set; }

    }

}
