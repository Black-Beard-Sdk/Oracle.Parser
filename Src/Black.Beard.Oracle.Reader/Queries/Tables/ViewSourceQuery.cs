using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Reader.Queries
{

    public class ViewSourceQuery : DbQueryBase<ViewSourceQueryTable>
    {

        //private const string patternMaterializedView = @"MATERIALIZED\s+VIEW";
        //public static System.Text.RegularExpressions.Regex regMaterializedViewEvaluate = new System.Text.RegularExpressions.Regex(patternMaterializedView, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        string sql =
@"SELECT  t.OWNER, t.OBJECT_NAME, dbms_metadata.get_ddl(object_type => t.OBJECT_TYPE ,name => t.OBJECT_NAME , schema=> t.OWNER) SOURCE
FROM    DBA_OBJECTS t
{0} 
AND t.OBJECT_TYPE = 'VIEW' 
";
        public override List<ViewSourceQueryTable> Resolve(DbContextOracle context, Action<ViewSourceQueryTable> action)
        {

            List<ViewSourceQueryTable> List = new List<ViewSourceQueryTable>();
            this.OracleContext = context;
            var db = context.database;

            if (action == null)
                action =
                    t =>
                    {

                        if (t.ObjectName.ExcludIfStartwith(t.SchemaName, Models.Configurations.ExcludeKindEnum.View))
                            return;

                        string key = t.SchemaName + "." + t.ObjectName;

                        TableModel table;

                        if (db.Tables.TryGet(key, out table))
                        {

                            StringBuilder sb = new StringBuilder(t.Source.Length + 2);
                            sb.Append(t.Source.Trim().Trim(' ', '\t', '\r', '\n'));

                            if (!t.Source.Trim().EndsWith(";"))
                            {
                                sb.AppendLine(string.Empty);
                                sb.AppendLine(";");
                            }
                            else
                            {
                                sb.AppendLine(string.Empty);
                            }

                            //if (!string.IsNullOrEmpty(table.Comment))
                            //    sb.AppendLine(string.Format(@"COMMENT ON TABLE ""{0}"".""{1}"" is '{2}';", table.SchemaName, table.Name, table.Comment.Replace("'", "''")));

                            //foreach (ColumnModel item in table.Columns.OfType<ColumnModel>().OrderBy(c => c.Key))
                            //    if (!string.IsNullOrEmpty(item.Description))
                            //        sb.AppendLine(string.Format(@"COMMENT ON COLUMN ""{0}"".""{1}"".""{2}"" is '{3}';", table.SchemaName, table.Name, item.ColumnName, item.Description.Replace("'", "''")));

                            table.IsView = true;

                            table.codeView = GetSource(sb.ToString());

                        }
                        else
                        {

                        }
                    };


            ViewSourceQueryDescriptor view = new ViewSourceQueryDescriptor(context.Manager.ConnectionString);
            sql = string.Format(sql, TableQueryWhereCondition());

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, QueryBase.DbParams.ToArray()))
            {
                List = view.ReadAll(reader, action).ToList();
            }

            return List;

        }


        private static string GetSource(string item)
        {
            return Utils.Serialize(item, true);
        }
    }


    public class ViewSourceQueryDescriptor : StructureDescriptorTable<ViewSourceQueryTable>
    {

        public ViewSourceQueryDescriptor(string connectionString)
            : base(() => new ViewSourceQueryTable(), connectionString)
        {

        }

        public static class Columns
        {

            public static Field<string> Owner = new Field<string>()
            {
                ColumnName = "OWNER",
                Read = reader => reader.Field<string>((int)ViewSourceQueryColumns.OWNER)
            };

            public static Field<string> ObjectName = new Field<string>()
            {
                ColumnName = "OBJECT_NAME",
                Read = reader => reader.Field<string>((int)ViewSourceQueryColumns.OBJECT_NAME)
            };

            public static Field<string> Source = new Field<string>()
            {
                ColumnName = "SOURCE",
                Read = reader => reader.Field<string>((int)ViewSourceQueryColumns.Source)
            };

        }

        #region Readers

        public override void Read(IDataReader r, ViewSourceQueryTable item)
        {
            item.SchemaName = ViewSourceQueryDescriptor.Columns.Owner.Read(r);
            item.ObjectName = ViewSourceQueryDescriptor.Columns.ObjectName.Read(r);
            item.Source = ViewSourceQueryDescriptor.Columns.Source.Read(r);
        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.Owner;
            yield return Columns.ObjectName;
            yield return Columns.Source;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ViewSourceQueryTable> entities)
        {
            throw new NotImplementedException();
        }
    }

    public enum ViewSourceQueryColumns
    {
        OWNER,
        OBJECT_NAME,
        Source,
    }

    public class ViewSourceQueryTable
    {
        public string SchemaName { get; set; }
        public string ObjectName { get; set; }
        public string Source { get; set; }
    }

}
