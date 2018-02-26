
using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        public override object VisitVariable_declaration([NotNull] PlSqlParser.Variable_declarationContext context)
        {
            Stop();
            var t = context.GetText();

            return base.VisitVariable_declaration(context);
        }

        public override object VisitVariable_name([NotNull] PlSqlParser.Variable_nameContext context)
        {
            Stop();
            return base.VisitVariable_name(context);
        }

        public override object VisitSubtype_declaration([NotNull] PlSqlParser.Subtype_declarationContext context)
        {
            Stop();
            return base.VisitSubtype_declaration(context);
        }

        public override object VisitCursor_declaration([NotNull] PlSqlParser.Cursor_declarationContext context)
        {
            Stop();
            return base.VisitCursor_declaration(context);
        }

        public override object VisitCursor_expression([NotNull] PlSqlParser.Cursor_expressionContext context)
        {
            Stop();
            return base.VisitCursor_expression(context);
        }

        public override object VisitCursor_loop_param([NotNull] PlSqlParser.Cursor_loop_paramContext context)
        {
            Stop();
            return base.VisitCursor_loop_param(context);
        }

        public override object VisitCursor_manipulation_statements([NotNull] PlSqlParser.Cursor_manipulation_statementsContext context)
        {
            Stop();
            return base.VisitCursor_manipulation_statements(context);
        }

        public override object VisitCursor_name([NotNull] PlSqlParser.Cursor_nameContext context)
        {
            Stop();
            return base.VisitCursor_name(context);
        }

        public override object VisitException_declaration([NotNull] PlSqlParser.Exception_declarationContext context)
        {
            Stop();
            return base.VisitException_declaration(context);
        }

        public override object VisitException_handler([NotNull] PlSqlParser.Exception_handlerContext context)
        {
            Stop();
            return base.VisitException_handler(context);
        }

        public override object VisitException_name([NotNull] PlSqlParser.Exception_nameContext context)
        {
            Stop();
            return base.VisitException_name(context);
        }

        public override object VisitPragma_clause([NotNull] PlSqlParser.Pragma_clauseContext context)
        {
            Stop();
            return base.VisitPragma_clause(context);
        }

        public override object VisitPragma_declaration([NotNull] PlSqlParser.Pragma_declarationContext context)
        {
            Stop();
            return base.VisitPragma_declaration(context);
        }

        public override object VisitPragma_elements([NotNull] PlSqlParser.Pragma_elementsContext context)
        {
            Stop();
            return base.VisitPragma_elements(context);
        }

        public override object VisitType_procedure_spec([NotNull] PlSqlParser.Type_procedure_specContext context)
        {
            Stop();
            return base.VisitType_procedure_spec(context);
        }

        public override object VisitFunction_spec([NotNull] PlSqlParser.Function_specContext context)
        {

            if (context.exception != null)
            {
                AppendException(context.exception);
                return null;
            }

            //var txt = GetText(context).ToString().Trim();
            //if (txt.StartsWith("CREATE"))
            //    txt = txt.Substring(6).Trim();

            string schema_name = string.Empty;
            string package_name = string.Empty;
            var package = this.Current<PackageModel>();
            if (package != null)
            {
                schema_name = package.GetOwner();
                package_name = package.GetName();
            }


            var fnc = new ProcedureModel()
            {
                Name = CleanName(context.identifier().GetText()),
                IsFunction = true,
                Key = "",
                SchemaName = schema_name,
                PackageName = package_name,
                // Code = "",
                // Description = "",
                // ResultType = new ProcedureResult(),
                // Files = new FileCollection(),
            };

            using (Enqueue(fnc))
            {

                var r = base.VisitFunction_spec(context);

            }

            fnc.Key = "";

            return fnc;

        }

        public override object VisitParameter([NotNull] PlSqlParser.ParameterContext context)
        {

            // : parameter_name(IN | OUT | INOUT | NOCOPY) * type_spec ? default_value_part ?

            var method = this.Current<ProcedureModel>();

            Stop();

            bool _in = context.IN() != null;
            bool _out = context.OUT() != null;
            if (context.INOUT() != null)
                _in = _out = true;
            bool _nocopy = context.NOCOPY() != null;

            var type = context.type_spec().Accept(this);

            var arg = new ArgumentModel()
            {
                Key = method.Arguments.Count().ToString(),
                ArgumentName = CleanName(context.parameter_name().GetText()),
                In = _in,
                Out = _out,
                Description = "",
                Type = (OracleType)type,
            };

            //arg.Files.Add(GetFileElement(context.Start));

            method.Arguments.Add(arg);

            return arg;

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

            var percent_type = context.PERCENT_TYPE() != null;
            var percent_rowtype = context.PERCENT_ROWTYPE() != null;
            var type_name = (string[])context.type_name().Accept(this);

            int length = type_name[0].Length;
            if (percent_type)
            {

                Stop();

                if (length == 2)
                    ResolveColumn(type_name[0], type_name[1]);
                else if (length == 2)
                    ResolveColumn(type_name[0], type_name[1], type_name[2]);
            }
            else if (percent_rowtype)
            {

                Stop();

                if (length == 1)
                    ResolveTable(type_name[0]);
                else if (length == 2)
                    ResolveTable(type_name[0], type_name[1]);
            }

            return base.VisitType_spec(context);

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
            Stop();
            return base.VisitDatatype(context);
        }

        public override object VisitParameter_spec([NotNull] PlSqlParser.Parameter_specContext context)
        {
            Stop();
            return base.VisitParameter_spec(context);
        }


        public override object VisitFunction_argument([NotNull] PlSqlParser.Function_argumentContext context)
        {
            Stop();
            return base.VisitFunction_argument(context);
        }

        public override object VisitFunction_argument_analytic([NotNull] PlSqlParser.Function_argument_analyticContext context)
        {
            Stop();
            return base.VisitFunction_argument_analytic(context);
        }

        public override object VisitFunction_argument_modeling([NotNull] PlSqlParser.Function_argument_modelingContext context)
        {
            Stop();
            return base.VisitFunction_argument_modeling(context);
        }

        public override object VisitFunction_body([NotNull] PlSqlParser.Function_bodyContext context)
        {
            Stop();
            return base.VisitFunction_body(context);
        }

        public override object VisitFunction_call([NotNull] PlSqlParser.Function_callContext context)
        {
            Stop();
            return base.VisitFunction_call(context);
        }

        public override object VisitProcedure_body([NotNull] PlSqlParser.Procedure_bodyContext context)
        {
            Stop();
            return base.VisitProcedure_body(context);
        }

        public override object VisitProcedure_name([NotNull] PlSqlParser.Procedure_nameContext context)
        {
            Stop();
            return base.VisitProcedure_name(context);
        }

        public override object VisitProcedure_spec([NotNull] PlSqlParser.Procedure_specContext context)
        {
            Stop();
            return base.VisitProcedure_spec(context);
        }

        public override object VisitProc_decl_in_type([NotNull] PlSqlParser.Proc_decl_in_typeContext context)
        {
            Stop();
            return base.VisitProc_decl_in_type(context);
        }

        public override object VisitType_function_spec([NotNull] PlSqlParser.Type_function_specContext context)
        {
            Stop();
            return base.VisitType_function_spec(context);
        }

        public override object VisitDrop_procedure([NotNull] PlSqlParser.Drop_procedureContext context)
        {
            Stop();
            return base.VisitDrop_procedure(context);
        }

        public override object VisitAlter_method_spec([NotNull] PlSqlParser.Alter_method_specContext context)
        {
            Stop();
            return base.VisitAlter_method_spec(context);
        }

        public override object VisitAlter_method_element([NotNull] PlSqlParser.Alter_method_elementContext context)
        {
            Stop();
            return base.VisitAlter_method_element(context);
        }

        public override object VisitAlter_procedure([NotNull] PlSqlParser.Alter_procedureContext context)
        {
            Stop();
            return base.VisitAlter_procedure(context);
        }

        public override object VisitCreate_procedure_body([NotNull] PlSqlParser.Create_procedure_bodyContext context)
        {
            Stop();
            return base.VisitCreate_procedure_body(context);
        }









    }

}


//public override object VisitFunction_name([NotNull] PlSqlParser.Function_nameContext context)
//{
//    Stop();
//    return base.VisitFunction_name(context);
//}

//public override object VisitParameter_name([NotNull] PlSqlParser.Parameter_nameContext context)
//{
//    Stop();
//    return base.VisitParameter_name(context);
//}
