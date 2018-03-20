using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Codes;
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

        public static OperatorEnum ConvertToOperator(this IToken token)
        {
            switch (token.Text)
            {

                //case "^=":
                //    return OperatorBinaryEnum;
                //case "~=":
                //    return OperatorBinaryEnum;

                case "<>":
                    return OperatorEnum.NotEqual;
                case "!=":
                    return OperatorEnum.AddNot;
                case "<":
                    return OperatorEnum.LessThan;
                case ">":
                    return OperatorEnum.GreatThan;
                case "<=":
                    return OperatorEnum.LessOrEqualThan;
                case ">=":
                    return OperatorEnum.GreatOrEqualThan;
                case "=":
                    return OperatorEnum.Equal;
                case "LIKE":
                    return OperatorEnum.Like;
                case "LIKEC":
                    return OperatorEnum.LikeC;
                case "LIKE2":
                    return OperatorEnum.Like2;
                case "LIKE4":
                    return OperatorEnum.Like4;
                case "OR":
                    return OperatorEnum.Or;
                case "AND":
                    return OperatorEnum.And;
                case "||":
                    return OperatorEnum.StringConcatenation;
                case "-":
                    return OperatorEnum.Substract;
                case "+":
                    return OperatorEnum.Add;
                case "/":
                    return OperatorEnum.Divider;
                case "*":
                    return OperatorEnum.Time;

                default:
                    if (System.Diagnostics.Debugger.IsAttached)
                        System.Diagnostics.Debugger.Break();
                    return OperatorEnum.Unmanaged;

            }
        }

        //public static string GetReference(this IEnumerable<string> items)
        //{
        //    return new ObjectReference(items);
        //}

        public static string Join(this IEnumerable<string> items)
        {

            if (items != null)
            {

                var values = items
                    .Where(c => !string.IsNullOrEmpty(c))
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToArray();

                return string.Join(".", values);
            }

            return string.Empty;

        }

        public static List<string> GetCleanedTexts([NotNull] this PlSqlParser.Full_identifierContext expressions, List<string> _names = null)
        {

            List<string> _type = new List<string>();

            expressions.identifier().GetCleanedTexts(_names);

            var id = expressions.id_expression();
            if (id != null)
                id.GetCleanedTexts(_names);

            return _names;
        }

        public static List<string> GetCleanedTexts([NotNull] this PlSqlParser.Id_expressionsContext expressions, List<string> _names = null)
        {

            if (_names == null)
                _names = new List<string>();

            if (expressions != null)
                foreach (var item in expressions.id_expression())
                    item.GetCleanedTexts(_names);

            return _names;


        }

        public static List<string> GetCleanedTexts([NotNull] this PlSqlParser.Id_expressionContext expression, List<string> _names = null)
        {

            if (_names == null)
                _names = new List<string>();

            if (expression != null)
                expression.GetCleanedTexts(_names);

            return _names;


        }

        public static List<string> GetCleanedTexts([NotNull] this ITerminalNode _id, List<string> _names = null)
        {

            if (_names == null)
                _names = new List<string>();

            if (_id != null)
            {

                var i = _id.GetText().CleanName();

                if (i != null)
                    _names.Add(i);

            }

            return _names;

        }       

        public static List<string> GetCleanedTexts([NotNull] this PlSqlParser.IdentifiersContext _id, List<string> _names = null)
        {

            if (_names == null)
                _names = new List<string>();

            var i = _id.identifier();
            if (i != null)
                _names.Add(i.GetText().CleanName());

            var j = _id.id_expression();
            if (j != null)
                foreach (var item in j)
                    _names.Add(item.GetText().CleanName());

            return _names;

        }

        public static List<string> GetCleanedTexts([NotNull] this PlSqlParser.IdentifierContext _id, List<string> _names = null)
        {

            if (_names == null)
                _names = new List<string>();

            var j = _id.id_expression();
            if (j != null)
                _names.Add(j.GetText().CleanName());

            return _names;

        }

        public static string CleanName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                name = string.Empty;
            else
                name = name.Trim().Trim('"').Trim().ToUpper();
            return name;
        }

        public static string GetCleanedName([NotNull] this PlSqlParser.Regular_idContext _id)
        {

            if (_id != null)
            {

                var i = _id.GetText().CleanName();

                if (!string.IsNullOrEmpty(i))
                    return i;

            }

            return string.Empty;

        }

        public static string GetCleanedName(this PlSqlParser.Numeric_function_nameContext ctx)
        {
            return ctx.GetText().CleanName();
        }


        public static string GetCleanedName(this PlSqlParser.String_function_nameContext ctx)
        {
            return ctx.GetText().CleanName();
        }



        public static string GetCleanedName(this PlSqlParser.Parameter_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names.FirstOrDefault();
        }

        public static string GetCleanedName(this PlSqlParser.Schema_object_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.id_expression().GetCleanedTexts(_names);
            return _names.FirstOrDefault();
        }

        public static string GetCleanedName(this PlSqlParser.Package_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names.FirstOrDefault();
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Sequence_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.id_expressions().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Type_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.id_expressions().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Char_set_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.id_expressions().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Trigger_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.full_identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Synonym_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Xml_column_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Table_var_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Attribute_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Collection_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.full_identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Column_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifiers().GetCleanedTexts(_names);
            return _names;
        }


        public static List<string> GetCleanedTexts(this PlSqlParser.Constraint_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifiers().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Container_tableview_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.full_identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Cost_class_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Index_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.full_identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Main_model_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Link_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Label_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.id_expression().GetCleanedTexts(_names);
            return _names;
        }
     
        public static List<string> GetCleanedTexts(this PlSqlParser.Query_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Record_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Rollback_segment_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Role_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.id_expression().GetCleanedTexts(_names);
            if (ctx.CONNECT() != null)
                _names.Add("CONNECT");
            return _names;
        }


        public static List<string> GetCleanedTexts(this PlSqlParser.Routine_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifiers().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Savepoint_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Schema_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Reference_model_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Implementation_type_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.full_identifier().GetCleanedTexts(_names);
            return _names;
        }

        public static List<string> GetCleanedTexts(this PlSqlParser.Aggregate_function_nameContext ctx)
        {
            var _names = new List<string>();
            ctx.identifiers().GetCleanedTexts(_names);
            return _names;
        }

        //public static List<string> GetCleanedTexts(this PlSqlParser.ctx)
        //{
        //    var _names = new List<string>();
        //    ctx.identifier().GetCleanedTexts(_names);
        //    return _names;
        //}


    }

}
