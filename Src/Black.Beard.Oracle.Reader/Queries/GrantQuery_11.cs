﻿using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Reader.Queries
{

    public class GrantQuery_11 : DbQueryBase<GrantQueryTable_11>
    {

        string sql =
@"

SELECT DISTINCT
    grantor,
    ROLE,
    owner,
    table_name,
    column_name,
    privilege,
    grantable,
    hierarchy

 FROM
(
    SELECT
        t.grantor,
        t.grantee AS ROLE,
        t.owner as owner,
        t.table_name,
        NULL AS column_name,
        t.privilege,
        t.grantable,
        t.hierarchy
    FROM
    dba_tab_privs t
    {0}
    UNION ALL
    SELECT
        t.grantor,
        t.grantee as ROLE,
        t.owner as owner,
        t.table_name,
        t.column_name,
        t.privilege,
        t.grantable,
        NULL AS hierarchy
    FROM
    dba_col_privs t
    {0}
    UNION ALL
    SELECT
        NULL AS grantor,
        r.role AS ROLE,
        r.owner AS owner,
        r.table_name,
        r.column_name,
        r.privilege,
        r.grantable,
        NULL AS hierarchy
    FROM
    role_tab_privs r
    {1}

 ) s
ORDER BY ROLE, privilege
";
        public override List<GrantQueryTable_11> Resolve(DbContextOracle context, Action<GrantQueryTable_11> action)
        {

            GrantModel grant;
            this.OracleContext = context;
            List<GrantQueryTable_11> List = new List<GrantQueryTable_11>();
            var db = context.Database;
            GrantCollection grants = null;
            if (db != null)
                grants = db.Grants;

            HashSet<string> _grants = new HashSet<string>();

            if (action == null)
                action =
                t =>
                {

                    if (!t.Table_name.ExcludIfStartwith(t.Owner, ExcludeKindEnum.Table))
                    {

                        string key = t.Owner + "." + t.Table_name
                                   + (!string.IsNullOrEmpty(t.Column_name)
                                       ? ("." + t.Column_name)
                                       : string.Empty) +
                                   "_to_" + t.Role
                                   ;

                        if (_grants.Add(key))
                        {

                            string FullObjectName = !string.IsNullOrEmpty(t.Column_name) ? @"""" + t.Column_name + @"""" : string.Empty;

                            if (!string.IsNullOrEmpty(t.Table_name))
                            {
                                if (!string.IsNullOrEmpty(FullObjectName))
                                    FullObjectName = "." + FullObjectName;
                                FullObjectName = @"""" + t.Table_name + @"""" + FullObjectName;
                            }

                            if (!string.IsNullOrEmpty(t.Owner))
                            {
                                if (!string.IsNullOrEmpty(FullObjectName))
                                    FullObjectName = "." + FullObjectName;
                                FullObjectName = @"""" + t.Owner + @"""" + FullObjectName;
                            }

                            grant = new GrantModel()
                            {
                                Key = key,
                                Role = t.Role,
                                ObjectSchema = t.Owner,
                                ObjectName = t.Table_name,
                                ColumnObjectName = t.Column_name ?? string.Empty,
                                Grantable = t.Grantable,
                                Hierarchy = t.Hierarchy
                            };

                            grants.Add(grant);

                        }
                        else
                        {
                            grant = grants[key];
                            grant.Grantable = t.Grantable;
                            grant.Hierarchy = t.Hierarchy;
                        }

                        var p = new PrivilegeModel() { Name = t.Privilege };
                        // AddIfNotExist -> can be duplicated because same privilege can given by many grantor
                        grant.Privileges.AddIfNotExist(p);

                    }

                };

            GrantQueryDescriptor_11 Grant = new GrantQueryDescriptor_11(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition("t"), TableQueryWhereCondition("r"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = Grant.ReadAll(reader, action).ToList();
            }

            return List;

        }


    }

    public class GrantQueryDescriptor_11 : StructureDescriptorTable<GrantQueryTable_11>
    {

        public GrantQueryDescriptor_11(string connectionString)
            : base(() => new GrantQueryTable_11(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Grantor = new Field<string>()
            {
                ColumnName = "GRANTOR",
                Read = reader => reader.Field<string>((int)GrantQueryColumns_11.Grantor)
            };

            public static Field<string> Role = new Field<string>()
            {
                ColumnName = "ROLE",
                Read = reader => reader.Field<string>((int)GrantQueryColumns_11.Role)
            };

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)GrantQueryColumns_11.Owner)
            };

            public static Field<string> Table_name = new Field<string>()
            {
                ColumnName = "TABLE_NAME",
                Read = reader => reader.Field<string>((int)GrantQueryColumns_11.Table_name)
            };

            public static Field<string> Column_name = new Field<string>()
            {
                ColumnName = "COLUMN_NAME",
                Read = reader => reader.Field<string>((int)GrantQueryColumns_11.Column_name)
            };

            public static Field<string> Privilege = new Field<string>()
            {
                ColumnName = "PRIVILEGE",
                Read = reader => reader.Field<string>((int)GrantQueryColumns_11.Privilege)
            };

            public static Field<string> Grantable = new Field<string>()
            {
                ColumnName = "Grantable",
                Read = reader => reader.Field<string>((int)GrantQueryColumns_11.Grantable)
            };

            public static Field<string> Hierarchy = new Field<string>()
            {
                ColumnName = "Hierarchy",
                Read = reader => reader.Field<string>((int)GrantQueryColumns_11.Hierarchy)
            };

        }

        #region Readers

        public override void Read(IDataReader r, GrantQueryTable_11 item)
        {
            item.Grantor = GrantQueryDescriptor_11.Columns.Grantor.Read(r);
            item.Role = GrantQueryDescriptor_11.Columns.Role.Read(r);
            item.Owner = GrantQueryDescriptor_11.Columns.Owner.Read(r);
            item.Table_name = GrantQueryDescriptor_11.Columns.Table_name.Read(r);
            item.Column_name = GrantQueryDescriptor_11.Columns.Column_name.Read(r);
            item.Privilege = GrantQueryDescriptor_11.Columns.Privilege.Read(r);
            item.Grantable = GrantQueryDescriptor_11.Columns.Grantable.Read(r) == "YES";
            item.Hierarchy = GrantQueryDescriptor_11.Columns.Hierarchy.Read(r) == "YES";
        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<GrantQueryTable_11> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum GrantQueryColumns_11
    {
        Grantor,
        Role,
        Owner,
        Table_name,
        Column_name,
        Privilege,
        Grantable,
        Hierarchy
    }

    public class GrantQueryTable_11
    {
        public string Grantor { get; set; }
        public string Role { get; set; }
        public string Owner { get; set; }
        public string Table_name { get; set; }
        public string Column_name { get; set; }
        public string Privilege { get; set; }
        public bool Grantable { get; set; }
        public bool Hierarchy { get; set; }

    }

}
