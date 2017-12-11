using PPssa.Tools.Databases.Models.Helpers;
using Pssa.Sdk.DataAccess.Dao;
using Pssa.Sdk.DataAccess.Dao.Contracts;
using Pssa.Tools.Databases.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Pssa.Tools.Databases.Generators.Queries.Oracle
{

    public class ContentCodeQuery : DbQueryBase<ContentCodeQueryTable>
    {

        string sql =
@"

SELECT
    s.owner,
    s.type,
    s.name,
    s.text,
    s.line
FROM dba_source s

-- WHERE s.type IN ( 'FUNCTION', 'PACKAGE', 'PACKAGE BODY', 'PROCEDURE', 'TYPE', 'TYPE BODY', 'TRIGGER' ) 

{0}

ORDER BY owner, s.type, s.name, s.line

";

        public override List<ContentCodeQueryTable> Resolve(DbContextOracle context, Action<ContentCodeQueryTable> action)
        {
            List<ContentCodeQueryTable> List = new List<ContentCodeQueryTable>();
            var db = context.database;
            this.OracleContext = context;
            var _p = db.Procedures.Cast<ProcedureModel>().ToLookup(c => c.Name);

            Dictionary<string, List<KeyValuePair<int, string>>> _dic = new Dictionary<string, List<KeyValuePair<int, string>>>();

            if (action == null)
                action =
                    t =>
                    {
                        if (t.name.ExcludIfStartwith(t.owner, Models.Configurations.ExcludeKindEnum.Procedure))
                            return;

                        string key = t.type + "." + t.owner + "." + t.name;                       

                        List<KeyValuePair<int, string>> sb;
                        if (!_dic.TryGetValue(key, out sb))
                        {
                            sb = new List<KeyValuePair<int, string>>();
                            _dic.Add(key, sb);
                        }
                        sb.Add(new KeyValuePair<int, string>(t.line, t.text));
                    };

            ContentCodeQueryDescriptor ContentCode = new ContentCodeQueryDescriptor(context.Manager.ConnectionString);
            string _sql = string.Format(sql, TableQueryWhereCondition("s", "owner", "name"));

            using (var reader = context.Manager.ExecuteReader(CommandType.Text, _sql, QueryBase.DbParams.ToArray()))
            {
                List = ContentCode.ReadAll(reader, action).ToList();
            }

            Sources = new List<CodeSource>();

            Build(db, _p, _dic);

            return List;

        }

        public List<CodeSource> Sources { get; private set; }

        private void Build(OracleDatabase db, ILookup<string, ProcedureModel> _p, Dictionary<string, List<KeyValuePair<int, string>>> _dic)
        {
            foreach (var item in _dic)
            {

                int index;
                string v;

                var k = item.Key.Split('.');
                string code;
                string key = k[1] + "." + k[2];

                PackageModel pck;
                TypeItem type;


                switch (k[0])
                {

                    case "FUNCTION":
                    case "PROCEDURE":

                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));

                        var i = _p[k[2]].Where(c => c.SchemaName == k[1] && string.IsNullOrEmpty(c.PackageName)).ToList();

                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });

                        if (i.Count == 1)
                            i.First().Code = code;
                        else if (i.Count > 1)
                        {

                        }
                        break;

                    case "PACKAGE":
                        pck = db.Packages[key];
                        if (pck == null)
                            db.Packages.Add(pck = new PackageModel() { Name = key, PackageOwner = k[1], PackageName = k[2] });

                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));
                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });
                        pck.Code = code;
                        break;

                    case "PACKAGE BODY":
                        pck = db.Packages[key];
                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));

                        if (pck == null)
                            db.Packages.Add(pck = new PackageModel() { Name = key, PackageOwner = k[1], PackageName = k[2] });

                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });
                        pck.CodeBody = code;

                        break;

                    case "TRIGGER":
                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));
                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });

                        var trigger = db.ResolveTrigger(key);
                        if (trigger != null)
                            trigger.Code = code;
                        break;

                    case "TYPE":
                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));
                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });
                        type = db.Types[key];
                        if (type != null)
                            type.Code = code;
                        break;

                    case "TYPE BODY":
                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));
                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });
                        type = db.Types[key];
                        if (type != null)
                            type.CodeBody = code;
                        break;

                    case "JAVA SOURCE":
                    case "LIBRARY":
                        //System.Diagnostics.Debugger.Break();
                        break;

                    default:
                        if (System.Diagnostics.Debugger.IsAttached)
                            System.Diagnostics.Debugger.Break();
                        break;

                }

            }
        }

        private static string GetSource(KeyValuePair<string, List<KeyValuePair<int, string>>> item)
        {

            var source = item.Value.OrderBy(c => c.Key).ToList();

            int count = 0;
            foreach (KeyValuePair<int, string> lines in source)
                count += lines.Value.Length;

            string aa = string.Empty;

            StringBuilder sb = new StringBuilder(count);
            foreach (KeyValuePair<int, string> lines in source)
            {
                aa += lines.Value;
                sb.Append(lines.Value);
            }

            string result = sb.ToString();

            var ar = item.Key.Split('.');

            string s = ar[1];
            string n = ar[2];

            string pattern = ar[0] + @"\s+""?" + n + @"""?\s*";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(result);

            if (m.Success)
            {
                var t = m.Value.ToUpper().Replace(@"""", "");
                t = t.Replace(n, string.Format(@"""{0}"".""{1}""", s, n));

                var t1 = t.Replace("  ", " ");
                while (t1 != t)
                {
                    t = t1;
                    t1 = t.Replace("  ", " ");
                }

                string s1 = result.Substring(0, m.Index);
                string s3 = result.Substring(m.Index + m.Length);
                result = string.Format("{0} {1} {2}", s1, t.Trim(), s3);

            }
            else
            {

            }

            int len = 0;

            while (len != result.Length)
            {
                len = result.Length;
                result = result.Trim(' ', '\t', '\r', '\n');
                if (result.EndsWith("/"))
                {
                    if (!result.EndsWith("*/"))
                        result = result.Trim('/');
                }
            }

            return Utils.Serialize(result, true);

        }


        public class CodeSource
        {
            public string Code { get; internal set; }
            public string Key { get; internal set; }
            public string Type { get; internal set; }
        }

    }

    public class ContentCodeQueryDescriptor : StructureDescriptorTable<ContentCodeQueryTable>
    {

        public ContentCodeQueryDescriptor(string connectionString)
            : base(() => new ContentCodeQueryTable(), connectionString)
        {

        }

        public static class Columns
        {
            public static Field<string> owner = new Field<string>() { ColumnName = "owner", Read = reader => reader.Field<string>((int)ContentCodeQueryColumns.owner) };
            public static Field<string> type = new Field<string>() { ColumnName = "type", Read = reader => reader.Field<string>((int)ContentCodeQueryColumns.type) };
            public static Field<string> name = new Field<string>() { ColumnName = "name", Read = reader => reader.Field<string>((int)ContentCodeQueryColumns.name) };
            public static Field<string> text = new Field<string>() { ColumnName = "text", Read = reader => reader.Field<string>((int)ContentCodeQueryColumns.text) };
            public static Field<int> line = new Field<int>() { ColumnName = "line", Read = reader => reader.Field<int>((int)ContentCodeQueryColumns.line) };
        }

        #region Readers

        public override void Read(IDataReader r, ContentCodeQueryTable item)
        {
            //item.Owner = ContentCodeQueryDescriptor.Columns.ContentCode_OWNER.Read(r);

            item.owner = ContentCodeQueryDescriptor.Columns.owner.Read(r);
            item.type = ContentCodeQueryDescriptor.Columns.type.Read(r);
            item.name = ContentCodeQueryDescriptor.Columns.name.Read(r);
            item.text = ContentCodeQueryDescriptor.Columns.text.Read(r);
            item.line = ContentCodeQueryDescriptor.Columns.line.Read(r);
        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.type;
            yield return Columns.name;
            yield return Columns.text;
            yield return Columns.line;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ContentCodeQueryTable> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum ContentCodeQueryColumns
    {
        owner,
        type,
        name,
        text,
        line
    }

    public class ContentCodeQueryTable
    {
        public string owner { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public int line { get; set; }
    }

}
