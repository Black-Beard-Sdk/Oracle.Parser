using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Bb.Oracle.Helpers
{

    public static class ContentHelper
    {

        /// <summary>
        /// Loads the content of the file.
        /// </summary>
        /// <param name="rootSource">The root source.</param>
        /// <returns></returns>
        public static string LoadContentFromFile(params string[] rootSource)
        {
            string _path = System.IO.Path.Combine(rootSource);
            return LoadContentFromFile(_path);
        }

        public static string LoadContentFromFile(string _path)
        {

            string fileContents = string.Empty;
            System.Text.Encoding encoding = null;
            FileInfo _file = new FileInfo(_path);

            using (FileStream fs = _file.OpenRead())
            {

                Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                if (cdet.Charset != null)
                    encoding = System.Text.Encoding.GetEncoding(cdet.Charset);
                else
                    encoding = System.Text.Encoding.UTF8;

                fs.Position = 0;

                byte[] ar = new byte[_file.Length];
                fs.Read(ar, 0, ar.Length);
                fileContents = encoding.GetString(ar);
            }

            if (fileContents.StartsWith("ï»¿"))
                fileContents = fileContents.Substring(3);

            if (encoding != System.Text.Encoding.UTF8)
            {
                var datas = System.Text.Encoding.UTF8.GetBytes(fileContents);
                fileContents = System.Text.Encoding.UTF8.GetString(datas);
            }

            return fileContents;

        }

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
