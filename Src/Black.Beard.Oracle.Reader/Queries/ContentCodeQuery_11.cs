using Bb.Oracle.Reader;
using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Reader.Queries
{

    public class ContentCodeQuery_11 : DbQueryBase<ContentCodeQueryTable_11>
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

        public override List<ContentCodeQueryTable_11> Resolve(DbContextOracle context, Action<ContentCodeQueryTable_11> action)
        {
            List<ContentCodeQueryTable_11> List = new List<ContentCodeQueryTable_11>();
            var db = context.Database;
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

            ContentCodeQueryDescriptor_11 ContentCode = new ContentCodeQueryDescriptor_11(context.Manager.ConnectionString);
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

                        var i = _p[k[2]].Where(c => c.Owner == k[1] && string.IsNullOrEmpty(c.PackageName)).ToList();

                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });

                        if (i.Count == 1)
                            i.First().Code.Code = code;
                        else if (i.Count > 1)
                        {

                        }
                        break;

                    case "PACKAGE":
                        if (!db.Packages.TryGet(key, out pck))
                            db.Packages.Add(pck = new PackageModel() { Key = key, Owner = k[1], Name = k[2] });

                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));
                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });
                        pck.Code.Code = code;
                        break;

                    case "PACKAGE BODY":
                        if (!db.Packages.TryGet(key, out pck))
                            db.Packages.Add(pck = new PackageModel() { Key = key, Owner = k[1], Name = k[2] });

                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));

                        if (pck == null)
                            db.Packages.Add(pck = new PackageModel() { Key = key, Owner = k[1], Name = k[2] });

                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });
                        pck.CodeBody.Code = code;

                        break;

                    case "TRIGGER":
                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));
                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });

                        TriggerModel trigger;
                        if (db.Triggers.TryGet(key, out trigger))
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

                        if (db.Types.TryGet(key, out type))
                            type.Code.Code = code;

                        break;

                    case "TYPE BODY":
                        index = item.Value.Select(c => c.Key).Max();
                        v = item.Value.LastOrDefault().Value;
                        if (!string.IsNullOrEmpty(v) && !string.IsNullOrEmpty(v.Trim()))
                            item.Value.Add(new KeyValuePair<int, string>(++index, "\n"));
                        item.Value.Add(new KeyValuePair<int, string>(++index, "/\n"));
                        code = GetSource(item).ToString();
                        Sources.Add(new CodeSource() { Key = key, Code = code, Type = k[0] });

                        if (db.Types.TryGet(key, out type))
                            type.CodeBody.Code = code;

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

            StringBuilder sb = new StringBuilder(count);
            foreach (KeyValuePair<int, string> lines in source)
                sb.Append(lines.Value);

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

    public class ContentCodeQueryDescriptor_11 : StructureDescriptorTable<ContentCodeQueryTable_11>
    {

        public ContentCodeQueryDescriptor_11(string connectionString)
            : base(() => new ContentCodeQueryTable_11(), connectionString)
        {

        }

        public static class Columns
        {
            public static Field<string> owner = new Field<string>() { ColumnName = "owner", Read = reader => reader.Field<string>((int)ContentCodeQueryColumns_11.owner) };
            public static Field<string> type = new Field<string>() { ColumnName = "type", Read = reader => reader.Field<string>((int)ContentCodeQueryColumns_11.type) };
            public static Field<string> name = new Field<string>() { ColumnName = "name", Read = reader => reader.Field<string>((int)ContentCodeQueryColumns_11.name) };
            public static Field<string> text = new Field<string>() { ColumnName = "text", Read = reader => reader.Field<string>((int)ContentCodeQueryColumns_11.text) };
            public static Field<int> line = new Field<int>() { ColumnName = "line", Read = reader => reader.Field<int>((int)ContentCodeQueryColumns_11.line) };
        }

        #region Readers

        public override void Read(IDataReader r, ContentCodeQueryTable_11 item)
        {
            //item.Owner = ContentCodeQueryDescriptor.Columns.ContentCode_OWNER.Read(r);

            item.owner = ContentCodeQueryDescriptor_11.Columns.owner.Read(r);
            item.type = ContentCodeQueryDescriptor_11.Columns.type.Read(r);
            item.name = ContentCodeQueryDescriptor_11.Columns.name.Read(r);
            item.text = ContentCodeQueryDescriptor_11.Columns.text.Read(r);
            item.line = ContentCodeQueryDescriptor_11.Columns.line.Read(r);
        }

        #endregion Readers

        public override IEnumerable<Field> Fields()
        {
            yield return Columns.type;
            yield return Columns.name;
            yield return Columns.text;
            yield return Columns.line;
        }

        public override DataTable GetDataTable(string tableName, IEnumerable<ContentCodeQueryTable_11> entities)
        {
            throw new NotImplementedException();
        }


    }

    public enum ContentCodeQueryColumns_11
    {
        owner,
        type,
        name,
        text,
        line
    }

    public class ContentCodeQueryTable_11
    {
        public string owner { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public int line { get; set; }
    }

}
