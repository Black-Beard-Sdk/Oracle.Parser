using Bb.Oracle.Models;
using Bb.Oracle.Models.Configurations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Reader.Dao
{

    internal static class FilterHelper
    {

        static FilterHelper()
        {
            if (ExcludeSection.Configuration == null)
                ExcludeSection.Configuration = new ExcludeSection();

            FilterHelper.schemas = ExcludeSection.Configuration.Schemas.OfType<ExcludeSchemaElement>().ToDictionary(c => c.Name.ToLower());

        }

        private static string[] _toExclude = new string[] { "BIN$", "SYSTP", "SYS_PLSQL", "SYS_LOB", "SYS_IL" };
        private static readonly Dictionary<string, ExcludeSchemaElement> schemas;

        public static bool ExcludIfStartwith(this string txt, string owner, ExcludeKindEnum kind)
        {

            bool result = false;

            foreach (var item in _toExclude)
                if (txt.StartsWith(item))
                {
                    result = true;
                    break;
                }

            if (!result)
            {

                ExcludeSchemaElement schema;

                if (schemas.TryGetValue(owner.ToLower(), out schema))
                {
                    List<ItemExcludeElement> kinds = schema.Items.OfType<ItemExcludeElement>().Where(c => c.Kind == kind).ToList();
                    foreach (var item in kinds)
                    {
                        if (item.Check(txt))
                        {
                            result = true;
                            break;
                        }
                    }

                }
            }


            return result;

        }

    }

}
