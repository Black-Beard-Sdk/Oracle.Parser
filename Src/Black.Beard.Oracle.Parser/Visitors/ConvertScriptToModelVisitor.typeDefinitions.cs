using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;
using Bb.Oracle.Helpers;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Codes;
using System.Text;
using System.Linq;

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
            {
                Stop();
                result = (OTypeDefinition)VisitTable_type_def(table_type_def);
            }
            else
            {
                var varray_type_def = context.varray_type_def();
                if (varray_type_def != null)
                {
                    Stop();
                    result = (OTypeDefinition)VisitVarray_type_def(varray_type_def);
                }
                else
                {
                    var record_type_def = context.record_type_def();
                    if (record_type_def != null)
                    {
                        Stop();
                        result = (OTypeDefinition)VisitRecord_type_def(record_type_def);
                    }
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
            Stop();
            var result = base.VisitTable_type_def(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// table_indexed_by_part :
        ///     (idx1=INDEXED | idx2=INDEX) BY type_spec
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitTable_indexed_by_part([NotNull] PlSqlParser.Table_indexed_by_partContext context)
        {
            Stop();
            var result = base.VisitTable_indexed_by_part(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// record_type_def : // Record Declaration Specific Clauses
        ///     RECORD LEFT_PAREN(COMMA? field_spec)+ RIGHT_PAREN 
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitVarray_type_def([NotNull] PlSqlParser.Varray_type_defContext context)
        {
            Stop();
            var result = base.VisitVarray_type_def(context);
            Debug.Assert(result != null);
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
            Stop();

            var type_spec = context.type_spec();
            var _type_spec = VisitType_spec(type_spec);

            var result = base.VisitField_spec(context);
            Debug.Assert(result != null);
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
            Stop();
            var result = base.VisitRecord_type_def(context);
            Debug.Assert(result != null);
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

            var type_spec = context.type_spec();
            if (type_spec != null)
            {

                Stop();

            }
            var result = new ORefCursorTypeDef()
            {

            };

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
            var method = this.Current<ItemBase>();

            var percent_type = context.PERCENT_TYPE() != null;
            var percent_rowtype = context.PERCENT_ROWTYPE() != null;
            var typename = context.type_name();

            if (typename != null)
            {

                var type_names = typename.GetCleanedTexts();

                if (percent_type)
                    result = new OTypeReference() { Path = type_names, KindTypeReference = PercentTypeEnum.PercentType };

                else if (percent_rowtype)
                    result = new OTypeReference() { Path = type_names, KindTypeReference = PercentTypeEnum.PercentRowType };

            }
            else
            {
                var dataType = context.datatype();
                var _result = (OracleType)this.VisitDatatype(dataType);
                result = new OTypeReference()
                {
                    DataType = _result,
                    Path = new string[] { _result.Owner, _result.Name, _result.DataType }
                    .Where(c => !string.IsNullOrEmpty(c)).ToList(),
                };

            }

            Debug.Assert(result != null);

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

                result.DataType = (string)native_datatype_element.Accept<object>(this);

                var precision_part = context.precision_part();
                if (precision_part != null)
                {

                    //  '(' numeric (',' numeric)? (CHAR | BYTE)? ')'

                    var values = precision_part.numeric().Select(c => c.Accept<object>(this)).Cast<int>().ToList();

                    if (values.Count > 1)
                        Stop();

                    result.DataPrecision = values[0];

                }

                if (context.WITH() != null)
                {
                    Stop();

                    var local = context.LOCAL() != null;
                    var t = context.TIME() != null && context.ZONE() != null;

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

