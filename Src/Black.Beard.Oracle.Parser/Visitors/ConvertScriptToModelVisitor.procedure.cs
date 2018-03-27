﻿
using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;
using System.Diagnostics;
using System.Text;
using Bb.Oracle.Helpers;
using Bb.Oracle.Models.Names;
using Bb.Oracle.Models.Codes;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        /// <summary>
        /// function_call :
        ///     CALL? routine_name function_arguments? keep_clause?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitFunction_call([NotNull] PlSqlParser.Function_callContext context)
        {

            var names = ObjectName.ProcedureName(context.routine_name().GetCleanedTexts());

            var method = new OCallMethodReference()
            {
                Name = names
            };

            var function_arguments = context.function_arguments();
            if (function_arguments != null)
            {
                method.Arguments = VisitFunction_arguments(function_arguments) as List<OMethodArgument>;
            }

            var keep_clause = context.keep_clause();
            if (keep_clause != null)
            {
                Stop();
                var tt = VisitKeep_clause(keep_clause);
                //method.Keep = tt;
            }

            //EvaluateGrants(method);

            return method;

        }

        public override object VisitKeep_clause([NotNull] PlSqlParser.Keep_clauseContext context)
        {
            Stop();
            var result = base.VisitKeep_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFunction_arguments([NotNull] PlSqlParser.Function_argumentsContext context)
        {
            var arguments = VisitArguments(context.arguments());
            return arguments;
        }


        public override object VisitArguments([NotNull] PlSqlParser.ArgumentsContext context)
        {
            List<OMethodArgument> _result = new List<OMethodArgument>();
            foreach (PlSqlParser.ArgumentContext argument in context.argument())
            {
                var arg = VisitArgument(argument) as OMethodArgument;
                _result.Add(arg);
            }
            return _result;
        }


        /// <summary>
        /// argument :
        ///       regular_id BIND_VAR /*EQUALS_OP GREATER_THAN_OP*/ expression
        ///     | expression
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitArgument([NotNull] PlSqlParser.ArgumentContext context)
        {

            var value = VisitExpression(context.expression());
            var arg = new OMethodArgument()
            {
                ParameterName = context.BIND_VAR() != null ? context.regular_id().GetCleanedName() : string.Empty,
                Value = value as OCodeExpression,
            };

            return arg;

        }

        /// <summary>
        /// variable_declaration :
        ///     identifier CONSTANT? type_spec(NOT NULL)? default_value_part? ';'
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitVariable_declaration([NotNull] PlSqlParser.Variable_declarationContext context)
        {

            OCodeVariableDeclarationStatement result = new OCodeVariableDeclarationStatement()
            {

                Name = context.identifier().GetCleanedTexts().First(),
                IsConstant = context.CONSTANT() != null,
                CanBeNull = !(context.NOT() != null && context.NULL() != null),
                Type = (OTypeReference)VisitType_spec(context.type_spec())
            };

            var default_value_part = context.default_value_part();
            if (default_value_part != null)
            {
                var defaultValue = (OCodeExpression)this.VisitDefault_value_part(default_value_part);
                result.DefaultValue = defaultValue;
            }

            return result;

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

        /// <summary>
        /// PROCEDURE procedure_name '(' type_elements_parameter (',' type_elements_parameter)* ')' ((IS | AS) call_spec)?
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitProcedure_spec([NotNull] PlSqlParser.Procedure_specContext context)
        {

            if (context.exception != null)
            {
                AppendException(context.exception);
                return null;
            }

            string schema_name = string.Empty;
            string package_name = string.Empty;
            var package = this.Current<PackageModel>();
            if (package != null)
            {
                schema_name = package.GetOwner();
                package_name = package.GetName();
            }

            string procName = context.identifier().GetCleanedTexts().Join();

            var proc = new ProcedureModel()
            {
                Name = procName,
                IsFunction = false,
                Owner = schema_name,
                PackageName = package_name,
                // Code = "",
                // Description = "",
                // ResultType = new ProcedureResult(),
                // Files = new FileCollection(),
            };

            proc.Code.SetSource(GetText(context));

            using (Enqueue(proc))
            {

                var r = base.VisitProcedure_spec(context);


            }

            proc.Key = proc.BuildKey();

            return proc;

        }

        public override object VisitFunction_spec([NotNull] PlSqlParser.Function_specContext context)
        {

            if (context.exception != null)
            {
                AppendException(context.exception);
                return null;
            }

            string schema_name = string.Empty;
            string package_name = string.Empty;
            var package = this.Current<PackageModel>();
            if (package != null)
            {
                schema_name = package.GetOwner();
                package_name = package.GetName();
            }

            string funcName = context.identifier().GetCleanedTexts().Join();

            var fnc = new ProcedureModel()
            {
                Name = funcName,
                IsFunction = true,
                Key = "",
                Owner = schema_name,
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

        /// <summary>
        /// parameter_name type_spec
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitType_elements_parameter([NotNull] PlSqlParser.Type_elements_parameterContext context)
        {
            Stop();

            var method = this.Current<ProcedureModel>();

            return base.VisitType_elements_parameter(context);
        }

        public override object VisitParameter([NotNull] PlSqlParser.ParameterContext context)
        {

            // : parameter_name(IN | OUT | INOUT | NOCOPY) * type_spec ? default_value_part ?

            var method = this.Current<ProcedureModel>();

            bool _in = context.IN() != null;
            bool _out = context.OUT() != null;
            if (context.INOUT() != null)
                _in = _out = true;
            bool _nocopy = context.NOCOPY() != null;

            var type = context.type_spec().Accept(this);

            var arg = new ArgumentModel()
            {
                Key = method.Arguments.Count().ToString(),
                Name = context.parameter_name().GetCleanedName(),
                In = _in,
                Out = _out,
                Description = "",
                Type = (OTypeReference)type,
            };

            //arg.Files.Add(GetFileElement(context.Start));

            method.Arguments.Add(arg);

            return arg;

        }

        public override object VisitParameter_spec([NotNull] PlSqlParser.Parameter_specContext context)
        {
            Stop();
            return base.VisitParameter_spec(context);
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

        public override object VisitProcedure_body([NotNull] PlSqlParser.Procedure_bodyContext context)
        {
            Stop();
            return base.VisitProcedure_body(context);
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

//public override object VisitProcedure_name([NotNull] PlSqlParser.Procedure_nameContext context)
//{
//    Stop();
//    return base.VisitProcedure_name(context);
//}


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
