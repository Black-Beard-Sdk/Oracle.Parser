using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pssa.Tools.Databases.Generators.Helpers
{
    public static class IncludeFileParser
    {
        public static HashSet<string> GetIncludes(string filename)
        {

            HashSet<string> result = new HashSet<string>();

            using (System.IO.FileStream file = System.IO.File.OpenRead(filename))
            {

                int l = (int)file.Length;

                byte[] ar = new byte[l];
                file.Read(ar, 0, l);
                string txt = System.Text.Encoding.UTF8.GetString(ar);

                string[] lst = txt.Split('\n');

                foreach (string i in lst)
                {
                    var a = i.Trim();
                    if (!string.IsNullOrEmpty(a) && !a.StartsWith("#"))
                        result.Add(a);
                }
            }

            return result;
        }
    }
}
