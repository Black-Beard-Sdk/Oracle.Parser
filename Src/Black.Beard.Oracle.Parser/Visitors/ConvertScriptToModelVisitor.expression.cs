using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;
using Bb.Oracle.Models.Codes;
using Bb.Oracle.Helpers;
using System.Text;
using Bb.Oracle.Models.Names;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        /// <summary>
        /// expression :
        ///       cursor_expression
        ///     | logical_expression
        ///     | VARIABLE_SESSION
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitExpression([NotNull] PlSqlParser.ExpressionContext context)
        {
            object result = null;

            var cursor_expression = context.cursor_expression();
            if (cursor_expression != null)
                result = this.VisitCursor_expression(cursor_expression);
            else
            {
                var logical_expression = context.logical_expression();
                if (logical_expression != null)
                    result = this.VisitLogical_expression(logical_expression);
                else
                {
                    var variable_session = context.VARIABLE_SESSION();
                    if (variable_session != null)
                        result = new OCodeVariableReferenceExpression() { Name = variable_session.GetCleanedText() };
                }
            }

            Debug.Assert(result != null);

            return result;

        }

        /// <summary>
        /// (ASSIGN_OP | DEFAULT) expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitDefault_value_part([NotNull] PlSqlParser.Default_value_partContext context)
        {
            var value = this.VisitExpression(context.expression());
            Debug.Assert(value != null);
            return value;
        }

        public override object VisitCursor_expression([NotNull] PlSqlParser.Cursor_expressionContext context)
        {
            Stop();
            return base.VisitCursor_expression(context);
        }

        /// <summary>
        /// multiset_expression : relational_expression(multiset_type= (MEMBER | SUBMULTISET) OF? concatenation)?;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitMultiset_expression([NotNull] PlSqlParser.Multiset_expressionContext context)
        {
            var result = base.VisitMultiset_expression(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// expressions : expression(COMMA expression)*
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitExpressions([NotNull] PlSqlParser.ExpressionsContext context)
        {
            Stop();
            var result = base.VisitExpressions(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// logical_expression :
	    ///       multiset_expression(IS NOT? (NULL | NAN | PRESENT | INFINITE | A_LETTER SET | EMPTY | OF TYPE? LEFT_PAREN ONLY? type_spec1= type_spec(COMMA type_spec2 = type_spec) * RIGHT_PAREN))*
        ///     | NOT logical_expression
        ///     | logical_expression AND logical_expression
        ///     | logical_expression OR logical_expression
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitLogical_expression([NotNull] PlSqlParser.Logical_expressionContext context)
        {
            var result = base.VisitLogical_expression(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// relational_expression :
        ///       relational_expression op = relational_operator relational_expression
        ///     | compound_expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitRelational_expression([NotNull] PlSqlParser.Relational_expressionContext context)
        {
            OCodeObject result = null;

            var compound_expression = context.compound_expression();
            if (compound_expression != null)
                result = (OCodeObject)VisitCompound_expression(compound_expression);

            else
            {

                Stop();

                var relational_expression = context.relational_expression();
                var left = (OCodeExpression)VisitRelational_expression(relational_expression[0]);
                var right = (OCodeExpression)VisitRelational_expression(relational_expression[1]);
                var _operator = (OperatorEnum)VisitRelational_operator(context.relational_operator());

                result = new OBinaryExpression()
                {
                    Left = left,
                    Operator = _operator,
                    Right = right
                };

            }

            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRelational_operator([NotNull] PlSqlParser.Relational_operatorContext context)
        {
            string txt = context.GetText();
            return txt.ConvertToOperator();
        }

        /// <summary>
        ///  compound_expression :
        ///     concatenation1=concatenation
        ///            (
        ///                NOT?
        ///     
        ///                (IN in_elements
        ///                 | BETWEEN between_elements
        ///             | like_type= (LIKE | LIKEC | LIKE2 | LIKE4) concatenation2= concatenation(ESCAPE concatenation3 = concatenation) ?
        ///     
        ///                )
        ///         )?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitCompound_expression([NotNull] PlSqlParser.Compound_expressionContext context)
        {

            object result = null;
            object result2 = null;
            object result3 = null;

            result = VisitConcatenation(context.concatenation1);

            if (context.BETWEEN() != null)
            {
                Stop();
                var between_elements = context.between_elements();
                result2 = VisitBetween_elements(between_elements);
            }
            else if (context.IN() != null)
            {
                Stop();
                var in_elements = context.in_elements();
                result2 = VisitIn_elements(in_elements);
            }
            else if (context.concatenation2 != null)
            {
                Stop();
                var op = context.like_type.ConvertToOperator();
                result2 = VisitConcatenation(context.concatenation2);
                if (context.ESCAPE() != null)
                    result3 = VisitConcatenation(context.concatenation3);

            }

            if (context.NOT() != null)
            {
                result = new OUnaryExpression()
                {
                    Left = result as OCodeExpression,
                    Operator = OperatorEnum.Not
                };
            }

            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// in_elements :
        ///         LEFT_PAREN subquery RIGHT_PAREN 
        ///     | LEFT_PAREN concatenation(COMMA concatenation)* RIGHT_PAREN 
        ///     | constant
        ///     | bind_variable
        ///     | general_element
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIn_elements([NotNull] PlSqlParser.In_elementsContext context)
        {
            Stop();
            var result = base.VisitIn_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// concatenation :
        ///       model_expression (AT (LOCAL | TIME ZONE concatenation) | interval_expression)?
        ///     | concatenation op = (ASTERISK | SOLIDUS) concatenation
        ///     | concatenation op = (PLUS_SIGN | MINUS_SIGN) concatenation
        ///     | concatenation BAR BAR concatenation
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitConcatenation([NotNull] PlSqlParser.ConcatenationContext context)
        {
            object result = null;
            var concatenations = context.concatenation();

            var model_expression = context.model_expression();
            if (model_expression != null)
            {

                result = VisitModel_expression(model_expression);
                if (context.AT() != null)
                {
                    Stop();
                    if (concatenations != null && concatenations.Length == 1)
                    {
                        Stop();
                        //result = 
                    }
                    else
                    {
                        Stop();
                        var interval_expression = context.interval_expression();
                        if (interval_expression != null)
                        {
                            //result = 

                        }
                    }
                }
            }
            else
            {
                Stop();
                result = new OBinaryExpression()
                {
                    Left = VisitConcatenation(concatenations[0]) as OCodeExpression,
                    Operator = context.op.ConvertToOperator(),
                    Right = VisitConcatenation(concatenations[1]) as OCodeExpression
                };
            }

            Debug.Assert(result != null);
            return result;

        }

        /// <summary>
        /// constant :
	    ///     TIMESTAMP(string | bind_variable) (AT TIME ZONE string)?
        ///     | INTERVAL(string | bind_variable | general_element_part) (YEAR | MONTH | DAY | HOUR | MINUTE | SECOND)
        ///       (LEFT_PAREN (UNSIGNED_INTEGER | bind_variable) (COMMA (UNSIGNED_INTEGER | bind_variable) )? RIGHT_PAREN )?
        ///       (TO (DAY | HOUR | MINUTE | SECOND(LEFT_PAREN (UNSIGNED_INTEGER | bind_variable) RIGHT_PAREN )?))?
        ///     | numeric
        ///     | DATE string
        ///     | string
        ///     | NULL
        ///     | TRUE
        ///     | FALSE
        ///     | DBTIMEZONE 
        ///     | SESSIONTIMEZONE
        ///     | MINVALUE
        ///     | MAXVALUE
        ///     | DEFAULT
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitConstant([NotNull] PlSqlParser.ConstantContext context)
        {

            OConstant _result = null;

            if (context.TIMESTAMP() != null)
            {
                Stop();
                // TIMESTAMP (string | bind_variable) (AT TIME ZONE string)?
            }
            else if (context.INTERVAL() != null)
            {
                Stop();
                /*INTERVAL (string | bind_variable | general_element_part) (YEAR | MONTH | DAY | HOUR | MINUTE | SECOND)
                    ( LEFT_PAREN (UNSIGNED_INTEGER | bind_variable) (COMMA (UNSIGNED_INTEGER | bind_variable) )? RIGHT_PAREN )?
                    (TO ( DAY | HOUR | MINUTE | SECOND ( LEFT_PAREN (UNSIGNED_INTEGER | bind_variable) RIGHT_PAREN )?))?*/
            }
            else if (context.DEFAULT() != null)
            {
                Stop();
            }
            else if (context.MAXVALUE() != null)
            {
                Stop();
            }
            else if (context.MINVALUE() != null)
            {
                Stop();
            }
            else if (context.SESSIONTIMEZONE() != null)
            {
                Stop();
            }
            else if (context.DBTIMEZONE() != null)
            {
                Stop();
            }
            else if (context.NULL() != null)
            {
                Stop();
            }
            else if (context.DATE() != null)
            {
                Stop();
            }
            else if (context.FALSE() != null)
                _result = new OBoolConstant() { Value = false };

            else if (context.TRUE() != null)
                _result = new OBoolConstant() { Value = true };

            else
            {

                var _numeric = context.numeric();
                if (_numeric != null)
                {
                    if (_numeric.APPROXIMATE_NUM_LIT() != null)
                    {
                        Stop();
                    }
                    else
                    {
                        var i = _numeric.ToInteger();
                        _result = new OIntegerConstant() { Value = i };
                    }

                }
                else
                {
                    var _string = context.@string();
                    if (_string != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in _string)
                            sb.Append(item.GetText());
                        _result = new OStringConstant() { Value = sb.ToString().Trim('\'') };
                    }
                }

            }

            Debug.Assert(_result != null);

            return _result;

        }

        /// <summary>
        /// unary_expression ('[' model_expression_element ']')?
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitModel_expression([NotNull] PlSqlParser.Model_expressionContext context)
        {
            var result = base.VisitModel_expression(context);

            var model_expression_element = context.model_expression_element();
            if (model_expression_element != null)
            {
                Stop();

            }

            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// unary_expression :
        ///       ('-' | '+') unary_expression
        ///     | PRIOR unary_expression
        ///     | CONNECT_BY_ROOT unary_expression
        ///     | DISTINCT unary_expression
        ///     | ALL unary_expression
        ///     | /*TODO {input.LT(1).getText().equalsIgnoreCase("new") && !input.LT(2).getText().equals(".")}?*/ NEW unary_expression
        ///     | /*TODO{(input.LA(1) == CASE || input.LA(2) == CASE)}?*/ case_statement/*[false]*/
        ///     | quantified_expression
        ///     | standard_function
        ///     | atom
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitUnary_expression([NotNull] PlSqlParser.Unary_expressionContext context)
        {

            object result = null;

            if (context.PRIOR() != null)
            {
                Stop();
                result = VisitUnary_expression(context.unary_expression());
            }
            else if (context.PRIOR() != null)
            {
                Stop();
                result = VisitUnary_expression(context.unary_expression());
            }
            else if (context.CONNECT_BY_ROOT() != null)
            {
                Stop();
                result = VisitUnary_expression(context.unary_expression());
            }
            else if (context.DISTINCT() != null)
            {
                Stop();
                result = VisitUnary_expression(context.unary_expression());
            }
            else if (context.ALL() != null)
            {
                Stop();
                result = VisitUnary_expression(context.unary_expression());
            }
            else if (context.NEW() != null)
            {
                Stop();
                result = VisitUnary_expression(context.unary_expression());
            }
            else
            {
                result = base.VisitUnary_expression(context);

                //var quantified_expression = context.quantified_expression();
                //var standard_function = context.standard_function();
                //var atom = context.atom();
            }


            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// model_expression_element :
        ///         (ANY | expression) (COMMA (ANY | expression))*
        ///     | single_column_for_loop(COMMA single_column_for_loop)*
        ///     | multi_column_for_loop
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitModel_expression_element([NotNull] PlSqlParser.Model_expression_elementContext context)
        {
            Stop();
            var result = base.VisitModel_expression_element(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// single_column_for_loop :
        ///         FOR column_name
        ///            (IN LEFT_PAREN expressions? RIGHT_PAREN 
        ///        | (LIKE expression)? FROM fromExpr= expression TO toExpr = expression
        ///     
        ///              action_type= (INCREMENT | DECREMENT) action_expr= expression)
        ///         ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitSingle_column_for_loop([NotNull] PlSqlParser.Single_column_for_loopContext context)
        {
            Stop();
            var result = base.VisitSingle_column_for_loop(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// multi_column_for_loop :
        ///         FOR paren_column_list
        ///       IN LEFT_PAREN(subquery | LEFT_PAREN expressions? RIGHT_PAREN ) RIGHT_PAREN 
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitMulti_column_for_loop([NotNull] PlSqlParser.Multi_column_for_loopContext context)
        {
            Stop();
            var result = base.VisitMulti_column_for_loop(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// atom :
        ///       table_element outer_join_sign
        ///     | bind_variable
        ///     | constant
        ///     | general_element
        ///     | LEFT_PAREN subquery RIGHT_PAREN subquery_operation_part*
        ///     | LEFT_PAREN expressions RIGHT_PAREN 
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitAtom([NotNull] PlSqlParser.AtomContext context)
        {
            object result = null;

            var constant = context.constant();
            if (constant != null)
            {
                result = VisitConstant(constant);

            }
            else
            {

                Stop();

                var bind_variable = context.bind_variable();
                if (bind_variable != null)
                    result = VisitBind_variable(bind_variable);
                else
                {

                    Stop();

                    var general_element = context.general_element();
                    if (general_element != null)
                        result = VisitGeneral_element(general_element);
                    else
                    {
                        var table_element = context.table_element();
                        if (table_element != null)
                            result = VisitTable_element(table_element);
                        else
                        {

                            Stop();

                            ///     | LEFT_PAREN subquery RIGHT_PAREN subquery_operation_part*
                            var subquery = context.subquery();
                            if (subquery != null)
                            {
                                result = VisitSubquery(subquery);
                                var subquery_operation_part = context.subquery_operation_part();
                                foreach (var item in subquery_operation_part)
                                {

                                }
                            }
                            else
                            {
                                ///     | LEFT_PAREN expressions RIGHT_PAREN 
                                Stop();
                                var expressions = context.expressions();
                                result = VisitExpressions(expressions);
                            }

                            Stop();
                        }
                    }
                }
            }

            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// subquery_operation_part :
        ///     (UNION ALL? | INTERSECT | MINUS) subquery_basic_elements
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitSubquery_operation_part([NotNull] PlSqlParser.Subquery_operation_partContext context)
        {
            Stop();
            var result = base.VisitSubquery_operation_part(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// bind_variable :
        ///     (BINDVAR | ':' UNSIGNED_INTEGER)
        ///     // Pro*C/C++ indicator variables
        ///     (INDICATOR? (BINDVAR | ':' UNSIGNED_INTEGER))?
        ///     ('.' general_element_part)*
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitBind_variable([NotNull] PlSqlParser.Bind_variableContext context)
        {
            Stop();
            var result = base.VisitBind_variable(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// general_element_part :
        ///     (INTRODUCER char_set_name)? id_expressions('@' link_name)? function_arguments?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitGeneral_element_part([NotNull] PlSqlParser.General_element_partContext context)
        {
            Stop();
            var result = base.VisitGeneral_element_part(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// general_element :
	    ///     general_element_part('.' general_element_part)*
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitGeneral_element([NotNull] PlSqlParser.General_elementContext context)
        {
            Stop();
            var result = base.VisitGeneral_element(context);
            Debug.Assert(result != null);
            return result;
        }


        /// <summary>
        /// standard_function :
        ///       string_function
        ///     | numeric_function_wrapper
        ///     | other_function
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitStandard_function([NotNull] PlSqlParser.Standard_functionContext context)
        {
            var result = base.VisitStandard_function(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// other_function :
        ///         over_clause_keyword function_argument_analytic over_clause?
        ///     | /*TODO stantard_function_enabling_using*/ regular_id function_argument_modeling using_clause?
        ///     | NVL LEFT_PAREN expression COMMA expression RIGHT_PAREN 
        ///     | to_date 
        ///     | COUNT LEFT_PAREN(ASTERISK | (DISTINCT | UNIQUE | ALL)? concatenation) RIGHT_PAREN over_clause?
        ///     | (CAST | XMLCAST) LEFT_PAREN(MULTISET LEFT_PAREN subquery RIGHT_PAREN | concatenation) AS type_spec RIGHT_PAREN 
        ///     | COALESCE LEFT_PAREN table_element(COMMA (numeric | string))? RIGHT_PAREN 
        ///     | COLLECT LEFT_PAREN(DISTINCT | UNIQUE)? concatenation collect_order_by_part? RIGHT_PAREN 
        ///     | within_or_over_clause_keyword(function_arguments keep_clause?) within_or_over_part+
        ///     | cursor_name(PERCENT_ISOPEN | PERCENT_FOUND | PERCENT_NOTFOUND | PERCENT_ROWCOUNT )
        ///     | DECOMPOSE LEFT_PAREN concatenation(CANONICAL | COMPATIBILITY)? RIGHT_PAREN 
        ///     | EXTRACT LEFT_PAREN regular_id FROM concatenation RIGHT_PAREN 
        ///     | (FIRST_VALUE | LAST_VALUE) function_argument_analytic respect_or_ignore_nulls? over_clause
        ///     | standard_prediction_function_keyword LEFT_PAREN expressions cost_matrix_clause? using_clause? RIGHT_PAREN 
        ///     | TRANSLATE LEFT_PAREN expression(USING (CHAR_CS | NCHAR_CS))? (COMMA expression) * RIGHT_PAREN 
        ///     | TRANSLATE LEFT_PAREN expression COMMA string COMMA string RIGHT_PAREN 
        ///     | TREAT LEFT_PAREN expression AS REF? type_spec RIGHT_PAREN 
        ///     | XMLAGG LEFT_PAREN expression order_by_clause? RIGHT_PAREN('.' general_element_part)?
        ///     | (XMLCOLATTVAL | XMLFOREST) LEFT_PAREN(COMMA? xml_multiuse_expression_element)+ RIGHT_PAREN('.' general_element_part)?
        ///     | XMLELEMENT LEFT_PAREN(ENTITYESCAPING | NOENTITYESCAPING)? (NAME | EVALNAME)? expression
        ///       (/*TODO{input.LT(2).getText().equalsIgnoreCase("xmlattributes")}?*/ COMMA xml_attributes_clause)?
        ///        (COMMA expression column_alias?)* RIGHT_PAREN('.' general_element_part)?
        ///     | XMLEXISTS LEFT_PAREN expression xml_passing_clause? RIGHT_PAREN 
        ///     | XMLPARSE LEFT_PAREN(DOCUMENT | CONTENT) concatenation WELLFORMED? RIGHT_PAREN('.' general_element_part)?
        ///     | XMLPI
        ///       LEFT_PAREN(NAME identifier | EVALNAME concatenation) (COMMA concatenation)? RIGHT_PAREN('.' general_element_part)?
        ///     | XMLQUERY LEFT_PAREN concatenation xml_passing_clause? RETURNING CONTENT(NULL ON EMPTY)? RIGHT_PAREN('.' general_element_part)?
        ///     | XMLROOT LEFT_PAREN concatenation(COMMA xmlroot_param_version_part)? (COMMA xmlroot_param_standalone_part)? RIGHT_PAREN('.' general_element_part)?
        ///     | XMLSERIALIZE LEFT_PAREN(DOCUMENT | CONTENT) concatenation(AS type_spec)?
        ///         xmlserialize_param_enconding_part? xmlserialize_param_version_part? xmlserialize_param_ident_part? ((HIDE | SHOW) DEFAULTS)? RIGHT_PAREN
        ///         ('.' general_element_part)?
        ///     | XMLTABLE LEFT_PAREN xml_namespaces_clause? concatenation xml_passing_clause? (COLUMNS xml_table_column (COMMA xml_table_column))? RIGHT_PAREN('.' general_element_part)?
        ///     | TRUNC LEFT_PAREN to_date(COMMA string)? RIGHT_PAREN
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitOther_function([NotNull] PlSqlParser.Other_functionContext context)
        {
            OCallMethodReference result = null;

            if (context.NVL().Exist())  // NVL LEFT_PAREN expression COMMA expression RIGHT_PAREN 
            {
                Stop();
                result = new OCallMethodReference(new MethodName("NVL")
                    , context.expression().Select(c => new OMethodArgument() { Value = (OCodeObject)VisitExpression(c) }).First()
                    );
            }
            else if (context.COUNT().Exist())   // COUNT LEFT_PAREN (ASTERISK | (DISTINCT | UNIQUE | ALL)? concatenation) RIGHT_PAREN over_clause?
            {
                Stop();
            }
            else if (context.COALESCE().Exist())    // COALESCE LEFT_PAREN table_element (COMMA (numeric | string))? RIGHT_PAREN 
            {
                Stop();
            }
            else if (context.COLLECT().Exist())    // COLLECT LEFT_PAREN (DISTINCT | UNIQUE)? concatenation collect_order_by_part? RIGHT_PAREN
            {
                Stop();
            }
            else if (context.DECOMPOSE().Exist())    // DECOMPOSE LEFT_PAREN concatenation (CANONICAL | COMPATIBILITY)? RIGHT_PAREN
            {
                Stop();
            }
            else if (context.EXTRACT().Exist())    // EXTRACT LEFT_PAREN regular_id FROM concatenation RIGHT_PAREN 
            {
                Stop();
            }
            else if (context.TRANSLATE().Exist())    
            {
                // TRANSLATE LEFT_PAREN expression (USING (CHAR_CS | NCHAR_CS))? (COMMA expression)* RIGHT_PAREN 
                // TRANSLATE LEFT_PAREN expression COMMA string COMMA string RIGHT_PAREN 
                Stop();
            }
            else if (context.TREAT().Exist())    // TREAT LEFT_PAREN expression AS REF? type_spec RIGHT_PAREN 
            {
                Stop();
            }
            else if (context.XMLAGG().Exist())    // XMLAGG LEFT_PAREN expression order_by_clause? RIGHT_PAREN ('.' general_element_part)?
            {
                Stop();
            }
            else if (context.XMLELEMENT().Exist())    // XMLELEMENT LEFT_PAREN (ENTITYESCAPING | NOENTITYESCAPING)? (NAME | EVALNAME)? expression
            {
                Stop();
            }
            else if (context.XMLEXISTS().Exist())    // XMLEXISTS LEFT_PAREN expression xml_passing_clause? RIGHT_PAREN 
            {
                Stop();
            }
            else if (context.XMLPI().Exist())    // XMLPI LEFT_PAREN (NAME identifier | EVALNAME concatenation) (COMMA concatenation)? RIGHT_PAREN ('.' general_element_part)?
            {
                Stop();
            }
            else if (context.XMLPARSE().Exist())    // XMLPARSE LEFT_PAREN (DOCUMENT | CONTENT) concatenation WELLFORMED? RIGHT_PAREN ('.' general_element_part)?
            {
                Stop();
            }
            else if (context.XMLQUERY().Exist())    // XMLQUERY LEFT_PAREN concatenation xml_passing_clause? RETURNING CONTENT (NULL ON EMPTY)? RIGHT_PAREN ('.' general_element_part)?
            {
                Stop();
            }
            else if (context.XMLROOT().Exist())    // XMLROOT LEFT_PAREN concatenation (COMMA xmlroot_param_version_part)? (COMMA xmlroot_param_standalone_part)? RIGHT_PAREN ('.' general_element_part)?
            {
                Stop();
            }
            else if (context.XMLSERIALIZE().Exist())    // XMLSERIALIZE LEFT_PAREN (DOCUMENT | CONTENT) concatenation (AS type_spec)?
                                            //       xmlserialize_param_enconding_part? xmlserialize_param_version_part? xmlserialize_param_ident_part? ((HIDE | SHOW) DEFAULTS)? RIGHT_PAREN 
                                            //       ('.' general_element_part)?
            {
                Stop();
            }
            else if (context.XMLTABLE().Exist())    // XMLTABLE LEFT_PAREN xml_namespaces_clause? concatenation xml_passing_clause? (COLUMNS xml_table_column (COMMA xml_table_column))? RIGHT_PAREN ('.' general_element_part)?
            {
                Stop();
            }
            else if (context.TRUNC().Exist())    // TRUNC LEFT_PAREN to_date (COMMA string)? RIGHT_PAREN
            {
                Stop();
            }
            else if (context.CAST().Exist() || context.XMLCAST().Exist())    // (CAST | XMLCAST) LEFT_PAREN (MULTISET LEFT_PAREN subquery RIGHT_PAREN | concatenation) AS type_spec RIGHT_PAREN 
            {
                Stop();
            }
            else if (context.XMLCOLATTVAL().Exist() || context.XMLFOREST().Exist())    // (XMLCOLATTVAL | XMLFOREST) LEFT_PAREN (COMMA? xml_multiuse_expression_element)+ RIGHT_PAREN ('.' general_element_part)?
                                    //      (/*TODO{input.LT(2).getText().equalsIgnoreCase("xmlattributes")}?*/ COMMA xml_attributes_clause)?
                                    //      (COMMA expression column_alias?)* RIGHT_PAREN ('.' general_element_part)?
            {
                Stop();
            }
            else if (context.FIRST_VALUE().Exist() || context.LAST_VALUE().Exist())    // (FIRST_VALUE | LAST_VALUE) function_argument_analytic respect_or_ignore_nulls? over_clause
            {
                Stop();
            }
            else
            {
                var to_date = context.to_date();
                if (to_date != null)
                    result = (OCallMethodReference)VisitTo_date(to_date);

                else
                {
                    Stop();
                }
                //over_clause_keyword function_argument_analytic over_clause?
                //   | /*TODO stantard_function_enabling_using*/ regular_id function_argument_modeling using_clause?
                //   | within_or_over_clause_keyword (function_arguments keep_clause?) within_or_over_part+
                //   | cursor_name ( PERCENT_ISOPEN | PERCENT_FOUND | PERCENT_NOTFOUND | PERCENT_ROWCOUNT )
                //   | standard_prediction_function_keyword LEFT_PAREN expressions cost_matrix_clause? using_clause? RIGHT_PAREN 

            }

            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTo_date([NotNull] PlSqlParser.To_dateContext context)
        {
            var result = new OCallMethodReference(new MethodName("TO_DATE")
                 , new OMethodArgument() { Value = (OCodeObject)VisitExpression(context.expression()) }
                 , new OMethodArgument() { Value = context.@string1.ToConstant() }
                 );
            return result;
        }

        /// <summary>
        /// numeric_function :
        ///       SUM LEFT_PAREN(DISTINCT | ALL)? expression RIGHT_PAREN 
        ///     | COUNT LEFT_PAREN(ASTERISK | ((DISTINCT | UNIQUE | ALL)? concatenation)? ) RIGHT_PAREN over_clause?
        ///     | ROUND LEFT_PAREN expression(COMMA UNSIGNED_INTEGER)?RIGHT_PAREN 
        ///     | AVG LEFT_PAREN(DISTINCT | ALL)? expression RIGHT_PAREN 
        ///     | MAX LEFT_PAREN(DISTINCT | ALL)? expression RIGHT_PAREN 
        ///     | LEAST LEFT_PAREN expressions RIGHT_PAREN 
        ///     | GREATEST LEFT_PAREN expressions RIGHT_PAREN
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitNumeric_function([NotNull] PlSqlParser.Numeric_functionContext context)
        {
            Stop();
            var result = base.VisitNumeric_function(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// numeric_function_wrapper :
	    ///     numeric_function(single_column_for_loop | multi_column_for_loop)?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitNumeric_function_wrapper([NotNull] PlSqlParser.Numeric_function_wrapperContext context)
        {
            Stop();
            var result = base.VisitNumeric_function_wrapper(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// string_function :
        ///       SUBSTR LEFT_PAREN expression COMMA expression(COMMA expression)? RIGHT_PAREN 
        ///     | TO_CHAR LEFT_PAREN(table_element | standard_function | expression) (COMMA string1=string)? (COMMA string2=string)? RIGHT_PAREN 
        ///     | DECODE LEFT_PAREN expressions RIGHT_PAREN 
        ///     | CHR LEFT_PAREN concatenation USING NCHAR_CS RIGHT_PAREN 
        ///     | NVL LEFT_PAREN expression COMMA expression RIGHT_PAREN 
        ///     | TRIM LEFT_PAREN((LEADING | TRAILING | BOTH)?  string1=string? FROM)? concatenation RIGHT_PAREN 
        ///     | TO_DATE LEFT_PAREN expression(COMMA string1 = string)? RIGHT_PAREN
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitString_function([NotNull] PlSqlParser.String_functionContext context)
        {
            OCallMethodReference result = null;
            if (context.SUBSTR().Exist())
            {
                Stop();
                result = new OCallMethodReference(new MethodName("SUBSTR")
                    , context.expression().Select(c => new OMethodArgument() { Value = (OCodeObject)VisitExpression(c) }).ToArray()
                    );
            }
            else if (context.TO_CHAR().Exist())
            {
                Stop();
                OCodeObject value = null;
                var table_element = context.table_element();
                if (table_element != null)
                    value = (OCodeObject)VisitTable_element(table_element);

                else
                {

                    var standard_function = context.standard_function();
                    if (standard_function != null)
                        value = (OCodeObject)VisitStandard_function(standard_function);

                    else
                    {
                        var expression = context.expression();
                        value = (OCodeObject)VisitExpression(expression[0]);
                    }
                }

                List<OMethodArgument> args = new List<OMethodArgument>()
                {
                    new OMethodArgument() { Value = value }
                };
                if (context.@string1 != null)
                    args.Add(new OMethodArgument() { Value = context.@string1.ToConstant() });
                if (context.@string2 != null)
                    args.Add(new OMethodArgument() { Value = context.@string2.ToConstant() });

                result = new OCallMethodReference(new MethodName("TO_CHAR")

                    );

            }
            else if (context.DECODE().Exist())
            {
                Stop();
                var e = context.expressions();
                result = new OCallMethodReference(new MethodName("DECODE")
                    , e.expression().Select(c => new OMethodArgument() { Value = (OCodeObject)VisitExpression(c) }).First()
                    );

            }
            else if (context.CHR().Exist())
            {
                Stop();

                var concatenation = context.concatenation();
                var value = (OCodeObject)VisitConcatenation(concatenation);
                if (context.NCHAR_CS().Exist())
                {
                    Stop();
                }

                result = new OCallMethodReference(new MethodName("CHR")
                    , new OMethodArgument() { Value = value }
                    );
            }
            else if (context.TRIM().Exist())
            {
                Stop();
                List<OMethodArgument> args = new List<OMethodArgument>();
                if (context.LEADING().Exist())
                    args.Add(new OMethodArgument() { Value = new OKeyWordConstant() { Value = "LEADING" } });

                else if (context.TRAILING().Exist())
                    args.Add(new OMethodArgument() { Value = new OKeyWordConstant() { Value = "TRAILING" } });

                if (context.BOTH().Exist())
                    args.Add(new OMethodArgument() { Value = new OKeyWordConstant() { Value = "BOTH" } });

                if (context.string1 != null)
                    args.Add(new OMethodArgument() { Value = context.string1.ToConstant() });

                var concatenation = context.concatenation();
                var value = (OCodeObject)VisitConcatenation(concatenation);
                //args.Add(new OMethodArgument() { Value = value });

                result = new OCallMethodReference(new MethodName("TRIM"), args.ToArray());

            }
            else
            {
                Stop();
            }

            return result;
        }

    }

}

