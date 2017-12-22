﻿using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bb.Oracle.Reader.Queries
{

    public class TriggerQuery : DbQueryBase<TriggerQueryTable>
    {

        string sql =
@"
SELECT
  t.owner,
  t.trigger_name,
  t.table_owner,
  t.base_object_type,
  t.table_name,
  o.status,
  t.status AS trigger_status,
  -- t.TRIGGER_BODY,
  t.TRIGGER_TYPE,
  t.DESCRIPTION,
  t.ACTION_TYPE
FROM dba_triggers t
INNER JOIN dba_objects o ON t.owner = o.owner AND t.trigger_name = o.object_name AND o.object_type = 'TRIGGER'
{0}
";

        public override List<TriggerQueryTable> Resolve(DbContextOracle context, Action<TriggerQueryTable> action)
        {
            List<TriggerQueryTable> List = new List<TriggerQueryTable>();
            var tables = context.database.Tables;
            this.OracleContext = context;

            if (action == null)
                action =
                t =>
                {


                    if (!string.IsNullOrEmpty(t.table_name) && t.table_name.ExcludIfStartwith(t.table_owner, Models.Configurations.ExcludeKindEnum.Table))
                        return;

                    string key = t.table_owner + "." + t.table_name ?? string.Empty;

                    if (tables.Contains(key))
                    {

                        TableModel table = tables[key];

                        TriggerModel trigger = new TriggerModel()
                        {
                            ActionType = t.ACTION_TYPE.Trim(),
                            BaseObjectType = t.base_object_type,
                            Code = string.Empty,
                            Description = t.DESCRIPTION,
                            Name = t.owner + "." + t.trigger_name,
                            Status = t.status,
                            TriggerName = t.trigger_name,
                            TriggerStatus = t.trigger_status,
                            TriggerType = t.TRIGGER_TYPE,
                            Owner = t.owner
                        };

                        table.Triggers.Add(trigger);

                    }

                };

            TriggerQueryDescriptor Trigger = new TriggerQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("t", "owner"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = Trigger.ReadAll(reader, action).ToList();
            }

            return List;

        }

    }

    public class TriggerQueryDescriptor : StructureDescriptorTable<TriggerQueryTable>
    {

        public TriggerQueryDescriptor(string connectionString)
            : base(() => new TriggerQueryTable(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> owner = new Field<string>() { ColumnName = "owner", Read = reader => reader.Field<string>((int)TriggerQueryColumns.owner) };
            public static Field<string> trigger_name = new Field<string>() { ColumnName = "trigger_name", Read = reader => reader.Field<string>((int)TriggerQueryColumns.trigger_name) };
            public static Field<string> table_owner = new Field<string>() { ColumnName = "table_owner", Read = reader => reader.Field<string>((int)TriggerQueryColumns.table_owner) };
            public static Field<string> base_object_type = new Field<string>() { ColumnName = "base_object_type", Read = reader => reader.Field<string>((int)TriggerQueryColumns.base_object_type) };
            public static Field<string> table_name = new Field<string>() { ColumnName = "table_name", Read = reader => reader.Field<string>((int)TriggerQueryColumns.table_name) };
            public static Field<string> status = new Field<string>() { ColumnName = "status", Read = reader => reader.Field<string>((int)TriggerQueryColumns.status) };
            public static Field<string> trigger_status = new Field<string>() { ColumnName = "trigger_status", Read = reader => reader.Field<string>((int)TriggerQueryColumns.trigger_status) };
            // public static Field<string> TRIGGER_BODY          = new Field<string>() { ColumnName = "TRIGGER_BODY"     , Read = reader => reader.Field<string>((int)TriggerQueryColumns.TRIGGER_BODY    ) };
            public static Field<string> TRIGGER_TYPE = new Field<string>() { ColumnName = "TRIGGER_TYPE", Read = reader => reader.Field<string>((int)TriggerQueryColumns.TRIGGER_TYPE) };
            public static Field<string> DESCRIPTION = new Field<string>() { ColumnName = "DESCRIPTION ", Read = reader => reader.Field<string>((int)TriggerQueryColumns.DESCRIPTION) };
            public static Field<string> ACTION_TYPE = new Field<string>() { ColumnName = "ACTION_TYPE", Read = reader => reader.Field<string>((int)TriggerQueryColumns.ACTION_TYPE) };

        }

        #region Readers

        public override void Read(IDataReader r, TriggerQueryTable item)
        {
            //item.Owner = TriggerQueryDescriptor.Columns.Trigger_OWNER.Read(r);


            item.owner = TriggerQueryDescriptor.Columns.owner.Read(r);
            item.trigger_name = TriggerQueryDescriptor.Columns.trigger_name.Read(r);
            item.table_owner = TriggerQueryDescriptor.Columns.table_owner.Read(r);
            item.base_object_type = TriggerQueryDescriptor.Columns.base_object_type.Read(r);
            item.table_name = TriggerQueryDescriptor.Columns.table_name.Read(r);
            item.status = TriggerQueryDescriptor.Columns.status.Read(r);
            item.trigger_status = TriggerQueryDescriptor.Columns.trigger_status.Read(r);
            // item.TRIGGER_BODY = TriggerQueryDescriptor.Columns.TRIGGER_BODY.Read(r);
            item.TRIGGER_TYPE = TriggerQueryDescriptor.Columns.TRIGGER_TYPE.Read(r);
            item.DESCRIPTION = TriggerQueryDescriptor.Columns.DESCRIPTION.Read(r);
            item.ACTION_TYPE = TriggerQueryDescriptor.Columns.ACTION_TYPE.Read(r);

        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.owner;
            yield return Columns.trigger_name;
            yield return Columns.table_owner;
            yield return Columns.base_object_type;
            yield return Columns.table_name;
            yield return Columns.status;
            yield return Columns.trigger_status;
            //yield return Columns.TRIGGER_BODY;
            yield return Columns.TRIGGER_TYPE;
            yield return Columns.DESCRIPTION;
            yield return Columns.ACTION_TYPE;

        }

        public override DataTable GetDataTable(string tableName, IEnumerable<TriggerQueryTable> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum TriggerQueryColumns
    {
        owner,
        trigger_name,
        table_owner,
        base_object_type,
        table_name,
        status,
        trigger_status,
        // TRIGGER_BODY,
        TRIGGER_TYPE,
        DESCRIPTION,
        ACTION_TYPE
    }

    public class TriggerQueryTable
    {

        public string owner { get; set; }
        public string trigger_name { get; set; }
        public string table_owner { get; set; }
        public string base_object_type { get; set; }
        public string table_name { get; set; }
        public string status { get; set; }
        public string trigger_status { get; set; }
        // public string     TRIGGER_BODY           { get; set; }
        public string TRIGGER_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string ACTION_TYPE { get; set; }

    }

}
