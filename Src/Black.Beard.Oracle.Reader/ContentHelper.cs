using PPssa.Tools.Databases.Models.Helpers;
using Pssa.Tools.Databases.Generators.Queries;
using Pssa.Tools.Databases.Generators.Queries.Oracle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptWatcher
{

    public static class ContentHelper
    {

        #region Content From Proc

        public static void BuildContentFromTypes(this DbContextOracle connection, StringBuilder sb, List<string> schemas, List<string> names)
        {
            BuildContentFromProc(connection, sb, schemas, names);
        }
        public static void BuildContentFromTrigger(this DbContextOracle connection, StringBuilder sb, List<string> schemas, List<string> names)
        {
            BuildContentFromProc(connection, sb, schemas, names);
        }

        public static void BuildContentFromProc(this DbContextOracle connection, StringBuilder sb, List<string> schemas, List<string> names)
        {


            var sources = connection.GetCodeFromProc(schemas, names);

            if (sources.Count == 0)
            {

            }
            else if (sources.Count == 1)
            {
                var item = sources.FirstOrDefault();
                sb.Append("CREATE OR REPLACE ");
                sb.Append(Utils.Unserialize(item.Code, true));
            }
            else
            {

                for (int i = 0; i < sources.Count; i++)
                {
                    var item = sources[i];
                    sb.Append("CREATE OR REPLACE ");
                    var t = Utils.Unserialize(item.Code, true);
                    t = t.Trim()
                         .Trim('/')
                         .Trim();
                    sb.Append(t);
                    sb.AppendLine(String.Empty);
                    sb.AppendLine("/");
                }

                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();

            }

        }

        public static void BuildContentFromPackage(this DbContextOracle connection, StringBuilder sb, List<string> schemas, List<string> names, bool isBody)
        {

            var sources = connection.GetCodeFromProc(schemas, names);

            if (isBody)
            {
                foreach (var item in sources)
                {

                    var t = Utils.Unserialize(item.Code, true);
                    var t2 = FormatSource(t);

                    sb.Append("CREATE OR REPLACE ");


                }
            }
            else
            {

            }

            //for (int i = 0; i < sources.Count; i++)
            //{
            //    var item = sources[i];
            //    var t = Utils.Unserialize(item.Code, true);
            //    t = t.Trim()
            //         .Trim('/')
            //         .Trim();
            //    sb.Append(t);
            //    sb.AppendLine(String.Empty);
            //    sb.AppendLine("/");
            //}


        }


        private static List<ContentCodeQuery.CodeSource> GetCodeFromProc(this DbContextOracle connection, List<string> schemas, List<string> names)
        {

            var db = new Pssa.Tools.Databases.Models.OracleDatabase();
            connection.database = db;
            ContentCodeQuery q1 = new ContentCodeQuery()
            {
                OwnerNames = schemas,
                ProcedureNames = names,
            };
            q1.Resolve(connection, null);

            return q1.Sources;

        }

        #endregion

        /// <summary>
        /// prepare and compares the code sources.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        public static bool CompareCodeSources(string s, string t)
        {

            string source = FormatSource(s).Trim();
            string target = FormatSource(t).Trim();

            if (source.ToUpper() == target.ToUpper())
                return false;

            else
            {

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    Debug.WriteLine(string.Empty);
                    Debug.WriteLine(source);
                    Debug.WriteLine(target);
                }

            }

            return true;

        }

        public static string FormatSource(string source)
        {

            string src = source.Replace(@"""", "")
                                .Trim()
                                .Trim('/')
                                .Trim();

            StringBuilder sb = new StringBuilder(source.Length);

            for (int i = 0; i < src.Length; i++)
            {

                char item = source[i];

                deb:
                #region comments

                if (item == '\'')
                {
                    sb.Append(item);

                    if (i + 1 < src.Length)
                    {
                        while (++i < src.Length)
                        {

                            item = src[i];
                            if (item == '\r' || item == '\n')
                                sb.Append(' ');
                            else
                                sb.Append(item);

                            char next = i + 1 < src.Length ? next = src[i + 1] : ' ';

                            if (next != '\'')
                                break;

                            else
                            {
                                sb.Append(next);
                                i++;
                            }

                        }
                        //sb.Append(' ');
                        continue;
                    }
                }

                if (item == '/')
                    if (i + 1 < src.Length && src[i + 1] == '*')
                    {
                        while (++i < src.Length)
                            if (src[i] == '*')
                                if (i + 1 < src.Length && src[i + 1] == '/')
                                {
                                    i++;
                                    item = src[++i];
                                    break;
                                }
                        continue;
                    }

                if (item == '-')
                    if (i + 1 < src.Length && src[i + 1] == '-')
                    {
                        i++;
                        while (++i < src.Length)
                        {
                            item = src[i];
                            if ((item == '\r' || item == '\n') && i + 1 < src.Length)
                            {
                                item = src[i + 1];
                                if (item == '\r' || item == '\n')
                                    i++;
                                break;
                            }
                        }

                        if (item == '-')
                            goto deb;
                        continue;

                    }

                #endregion comments

                if (char.IsLetterOrDigit(item))
                {

                    sb.Append(char.ToUpper(item));

                }
                else if (char.IsPunctuation(item))
                {
                
                    if (sb.Length > 0 && sb[sb.Length - 1] != ' ')
                    {
                        if (chars.Contains(item))
                            sb.Append(' ');

                        else
                        {

                        }
                    }

                    sb.Append(item);

                    if (sb.Length > 0 && sb[sb.Length - 1] != ' ')
                        if (chars.Contains(item))
                            sb.Append(" ");

                }
                else if (char.IsWhiteSpace(item))
                {

                    var l = sb.Length > 0 ? sb[sb.Length - 1] : ' ';
                    if (char.IsWhiteSpace(l))
                    {

                    }
                    else
                    {
                        if (sb.Length > 0 && sb[sb.Length - 1] != ' ')
                            sb.Append(" ");
                    }

                }
                else
                {

                }
            }

            return sb.ToString().Trim();
        }



        private static HashSet<char> chars = new HashSet<char>() { '(', ')', '[', ']', ';', '+', '-', '*', '/', '\\', ':', ',' };

    }

}

