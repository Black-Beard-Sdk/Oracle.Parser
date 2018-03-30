using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;
using Bb.Oracle.Helpers;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Codes;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        /// <summary>
        /// type_declaration :
        ///     TYPE identifier IS(table_type_def | varray_type_def | record_type_def | ref_cursor_type_def) ';'
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitType_declaration([NotNull] PlSqlParser.Type_declarationContext context)
        {

            OTypeDefinition result = null;

            var table_type_def = context.table_type_def();
            if (table_type_def != null)
                result = (OTypeDefinition)VisitTable_type_def(table_type_def);

            else
            {
                var varray_type_def = context.varray_type_def();
                if (varray_type_def != null)
                    result = (OTypeDefinition)VisitVarray_type_def(varray_type_def);

                else
                {
                    var record_type_def = context.record_type_def();
                    if (record_type_def != null)
                        result = (OTypeDefinition)VisitRecord_type_def(record_type_def);

                    else
                    {
                        var ref_cursor_type_def = context.ref_cursor_type_def();
                        result = (OTypeDefinition)VisitRef_cursor_type_def(ref_cursor_type_def);
                    }
                }
            }

            Debug.Assert(result != null);

            var tt = GetText(context);

            var names = context.identifier().GetCleanedTexts();
            result.Name = names[0];

            return result;
        }

        /// <summary>
        /// table_type_def :
        ///     TABLE OF type_spec table_indexed_by_part? (NOT NULL)?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitTable_type_def([NotNull] PlSqlParser.Table_type_defContext context)
        {
            var table_indexed_by_part = context.table_indexed_by_part();
            TableTypeDefinition result = new TableTypeDefinition()
            {
                TableOf = (OTypeReference)VisitType_spec(context.type_spec()),
                IndexedBy = table_indexed_by_part == null
                    ? new OTableIndexedByPartExpression()
                    : (OTableIndexedByPartExpression)VisitTable_indexed_by_part(table_indexed_by_part),
                Nullable = !context.NULL().Exist(),
            };
            return result;
        }

        /// <summary>
        /// table_indexed_by_part : (INDEXED | INDEX) BY type_spec
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitTable_indexed_by_part([NotNull] PlSqlParser.Table_indexed_by_partContext context)
        {
            OTableIndexedByPartExpression result = new OTableIndexedByPartExpression()
            {
                IndexKind = context.INDEXED().Exist()
                    ? OTableIndexedByPartExpressionEnum.Indexed
                    : OTableIndexedByPartExpressionEnum.Index,
                By = (OTypeReference)VisitType_spec(context.type_spec()),
            };
            return result;
        }

        /// <summary>
        /// varray_type_def :
        ///     (VARRAY | VARYING ARRAY) LEFT_PAREN expression RIGHT_PAREN OF type_spec(NOT NULL)?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitVarray_type_def([NotNull] PlSqlParser.Varray_type_defContext context)
        {
            OVarrayTypeDefinition result = new OVarrayTypeDefinition()
            {
                Varying = context.VARYING().Exist(),
                Expression = (OCodeExpression)VisitExpression(context.expression()),
                Type = (OTypeReference)VisitType_spec(context.type_spec()),
                Nullable = !context.NULL().Exist(),
            };
            return result;
        }

        /// <summary>
        /// field_spec :
        ///     column_name type_spec? (NOT NULL)? default_value_part?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitField_spec([NotNull] PlSqlParser.Field_specContext context)
        {
            OFieldSpecExpression result = new OFieldSpecExpression()
            {
                Nullable = !context.NULL().Exist(),
            };

            var type_spec = context.type_spec();
            if (type_spec != null)
                result.Type = (OTypeReference)VisitType_spec(type_spec);

            var default_value_part = context.default_value_part();
            if (default_value_part != null)
                result.DefaultValue = (OCodeExpression)VisitDefault_value_part(default_value_part);

            return result;
        }


        /// <summary>
        /// record_type_def : // Record Declaration Specific Clauses
        ///     RECORD LEFT_PAREN(COMMA? field_spec)+ RIGHT_PAREN 
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitRecord_type_def([NotNull] PlSqlParser.Record_type_defContext context)
        {

            var field_specs = context.field_spec();
            List<OFieldSpecExpression> _types = new List<OFieldSpecExpression>();

            if (field_specs != null)
                foreach (PlSqlParser.Field_specContext field_spec in field_specs)
                {
                    var field = (OFieldSpecExpression)VisitField_spec(field_spec);
                    _types.Add(field);
                }

            ORecordTypeDef result = new ORecordTypeDef()
            {
                Fields = _types,
            };
            return result;
        }

        /// <summary>
        /// ref_cursor_type_def :
        ///     REF CURSOR(RETURN type_spec)?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitRef_cursor_type_def([NotNull] PlSqlParser.Ref_cursor_type_defContext context)
        {

            var result = new ORefCursorTypeDef()
            {

            };

            var type_spec = context.type_spec();
            if (type_spec != null)
            {
                result.Return = (OTypeReference)VisitType_spec(type_spec);
            }

            Debug.Assert(result != null);
            return result;
        }


        /// <summary>
        /// type_spec
        /// : datatype
        /// | REF? type_name(PERCENT_ROWTYPE | PERCENT_TYPE)?
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitType_spec([NotNull] PlSqlParser.Type_specContext context)
        {

            OTypeReference result = null;
            var typename = context.type_name();

            if (typename != null)
            {
                var type_names = typename.GetCleanedTexts();
                result = new OTypeReference() { KindTypeReference = PercentTypeEnum.PercentType };
                if (type_names.Count == 1)
                    result.DataType.Name = type_names[0];
                else if (type_names.Count >= 2)
                {
                    result.DataType.Owner = type_names[0];
                    result.DataType.Name = type_names[1];
                    if (type_names.Count == 3)
                        result.DataType.Name = type_names[2];
                }
            }
            else
            {
                var dataType = context.datatype();
                var _result = (OracleType)this.VisitDatatype(dataType);
                result = new OTypeReference()
                {
                    DataType = _result
                };

                result.DataType.Owner = _result.Owner;
                result.DataType.Name = _result.Name;

            }

            var percent_type = context.PERCENT_TYPE() != null;
            var percent_rowtype = context.PERCENT_ROWTYPE() != null;

            if (percent_type)
                result.KindTypeReference = PercentTypeEnum.PercentType;

            else if (percent_rowtype)
                result.KindTypeReference = PercentTypeEnum.PercentRowType;

            return result;

        }

        /// <summary>
        /// datatype
        ///     : native_datatype_element precision_part? (WITH LOCAL? TIME ZONE | CHARACTER SET char_set_name)?
        ///     | INTERVAL(YEAR | DAY) ('(' expression ')')? TO(MONTH | SECOND) ('(' expression ')')?
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitDatatype([NotNull] PlSqlParser.DatatypeContext context)
        {

            var result = new OracleType()
            {

            };

            var native_datatype_element = context.native_datatype_element();
            if (native_datatype_element != null)
            {

                result.Name = (string)native_datatype_element.Accept<object>(this);

                var precision_part = context.precision_part();
                if (precision_part != null)
                {

                    //  '(' numeric (',' numeric)? (CHAR | BYTE)? ')'

                    var values = precision_part.numeric().Select(c => c.Accept<object>(this)).Cast<int>().ToList();

                    if (values.Count == 1)
                    {
                        result.DataPrecision = values[0];
                    }
                    else if (values.Count == 2)
                    {
                        var _dec = (System.Math.Pow(10, (double)values[1].ToString().Length));
                        var _dec2 = (values[1]) / _dec;
                        result.DataPrecision = new decimal(values[0]) + new decimal(_dec2);
                    }
                    else
                    {
                        Stop();
                    }

                }

                if (context.WITH() != null)
                {

                    result.Name += " WITH";
                    if (context.LOCAL() != null)
                        result.Name += " LOCAL";

                    if (context.TIME() != null && context.ZONE() != null)
                        result.Name += " TIME ZONE";

                }
                else if (context.CHARACTER() != null && context.SET() != null)
                {
                    Stop();
                    context.char_set_name();
                }

            }
            else
            {

                Stop();

                if (context.INTERVAL() != null)
                {
                    var day = context.DAY() != null;
                    var year = context.YEAR() != null;
                }

                PlSqlParser.ExpressionContext expression1 = null;
                PlSqlParser.ExpressionContext expression2 = null;

                var expressions = context.expression();

                var to = context.TO();
                var month = context.MONTH();
                var second = context.SECOND();

                if (expressions.Length == 2)
                {
                    Stop();
                    expression1 = expressions[0];
                    expression2 = expressions[1];
                }
                if (expressions.Length == 1)
                {
                    Stop();
                    expression1 = expressions[0];
                    if (expression1.Start.StartIndex > to.Symbol.StartIndex)
                    {
                        expression2 = expression1;
                        expression1 = null;
                    }
                }
                else
                {
                    Stop();
                }

            }

            return result;

        }

        public override object VisitNative_datatype_element([NotNull] PlSqlParser.Native_datatype_elementContext context)
        {

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < context.ChildCount; i++)
            {
                sb.Append(context.GetChild(i));
                sb.Append(" ");
            }

            return sb.ToString().Trim();

        }

        /// <summary>
        /// UNSIGNED_INTEGER | APPROXIMATE_NUM_LIT
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitNumeric([NotNull] PlSqlParser.NumericContext context)
        {

            var unsigned = context.UNSIGNED_INTEGER();
            if (unsigned != null)
                return GetUnsignedInteger(unsigned);

            else
            {
                // FLOAT_FRAGMENT ('E' ('+'|'-')? (FLOAT_FRAGMENT | [0-9]+))? ('D' | 'F')?;

                var litt = context.APPROXIMATE_NUM_LIT().GetText();
                Stop();
            }

            return base.VisitNumeric(context);

        }

        private static int GetUnsignedInteger(Antlr4.Runtime.Tree.ITerminalNode unsigned)
        {
            return int.Parse(unsigned.GetText());
        }

    }

}

