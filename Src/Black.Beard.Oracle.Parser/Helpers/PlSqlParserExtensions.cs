using Antlr4.Runtime.Misc;
using Bb.Oracle.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Helpers
{


    public static class PlSqlParserExtensions
    {

        public static string[] GetCleanedText([NotNull] this PlSqlParser.Id_expressionsContext expressions)
        {

            List<string> _type = new List<string>();

            if (expressions != null)
                foreach (var item in expressions.id_expression())
                    _type.Add(item.GetCleanedName());

            return _type.ToArray();


        }

        public static string[] GetCleanedText([NotNull] this PlSqlParser.Full_identifierContext _id)
        {

            List<string> _names = new List<string>();

            _names.Add(_id.identifier().GetCleanedName());

            var id = _id.id_expression();
            if (id != null)
            {
                var _id1 = id.regular_id().GetCleanedName()
                        ?? id.DELIMITED_ID().GetCleanedName();
                _names.Add(_id1);
            }

            return _names.ToArray();

        }

        public static string GetCleanedName(this Antlr4.Runtime.Tree.IParseTree name)
        {
            if (name != null)
                return name.GetText().CleanName();
            return null;
        }

        public static string CleanName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                name = string.Empty;
            else
                name = name.Trim().Trim('"').Trim().ToUpper();
            return name;
        }


    }

}
