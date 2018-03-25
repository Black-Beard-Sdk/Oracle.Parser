using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;
using System.Text;
using Bb.Oracle.Helpers;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor : PlSqlParserBaseVisitor<object>, IFile
    {

        public ConvertScriptToModelVisitor(OracleDatabase db)
        {
            this.Db = db;
        }

        public override object Visit(IParseTree tree)
        {


            var result = base.Visit(tree);
            return result;
        }

        public override object VisitSql_script([NotNull] PlSqlParser.Sql_scriptContext context)
        {

            this._initialSource = new StringBuilder(context.Start.InputStream.ToString());

            if (context.exception != null)
            {
                AppendException(context.exception);
                return null;
            }
            else
                base.VisitSql_script(context);

            return null;

        }

        public override object VisitUnit_statement([NotNull] PlSqlParser.Unit_statementContext context)
        {
            var result = base.VisitUnit_statement(context);
            return result;
        }

        public override object VisitAdd_constraint([NotNull] PlSqlParser.Add_constraintContext context)
        {
            Stop();
            var result = base.VisitAdd_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAdd_rem_container_data([NotNull] PlSqlParser.Add_rem_container_dataContext context)
        {
            Stop();
            var result = base.VisitAdd_rem_container_data(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_attribute_definition([NotNull] PlSqlParser.Alter_attribute_definitionContext context)
        {
            Stop();
            var result = base.VisitAlter_attribute_definition(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_collection_clauses([NotNull] PlSqlParser.Alter_collection_clausesContext context)
        {
            Stop();
            var result = base.VisitAlter_collection_clauses(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_function([NotNull] PlSqlParser.Alter_functionContext context)
        {
            Stop();
            var result = base.VisitAlter_function(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_identified_by([NotNull] PlSqlParser.Alter_identified_byContext context)
        {
            Stop();
            var result = base.VisitAlter_identified_by(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_index([NotNull] PlSqlParser.Alter_indexContext context)
        {
            Stop();
            var result = base.VisitAlter_index(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitAlter_table([NotNull] PlSqlParser.Alter_tableContext context)
        {
            Stop();
            var result = base.VisitAlter_table(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_trigger([NotNull] PlSqlParser.Alter_triggerContext context)
        {
            Stop();
            var result = base.VisitAlter_trigger(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_type([NotNull] PlSqlParser.Alter_typeContext context)
        {
            Stop();
            var result = base.VisitAlter_type(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_user([NotNull] PlSqlParser.Alter_userContext context)
        {
            //this.File
            Stop();
            var result = base.VisitAlter_user(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAlter_user_editions_clause([NotNull] PlSqlParser.Alter_user_editions_clauseContext context)
        {
            Stop();
            var result = base.VisitAlter_user_editions_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAnonymous_block([NotNull] PlSqlParser.Anonymous_blockContext context)
        {
            Stop();
            var result = base.VisitAnonymous_block(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitAssignment_statement([NotNull] PlSqlParser.Assignment_statementContext context)
        {
            Stop();
            var result = base.VisitAssignment_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAttribute_definition([NotNull] PlSqlParser.Attribute_definitionContext context)
        {
            Stop();
            var result = base.VisitAttribute_definition(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAutoextend_clause([NotNull] PlSqlParser.Autoextend_clauseContext context)
        {
            Stop();
            var result = base.VisitAutoextend_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitBetween_bound([NotNull] PlSqlParser.Between_boundContext context)
        {
            Stop();
            var result = base.VisitBetween_bound(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitBetween_elements([NotNull] PlSqlParser.Between_elementsContext context)
        {
            Stop();
            var result = base.VisitBetween_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitBlock([NotNull] PlSqlParser.BlockContext context)
        {
            Stop();
            var result = base.VisitBlock(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitBody([NotNull] PlSqlParser.BodyContext context)
        {
            Stop();
            var result = base.VisitBody(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitBounds_clause([NotNull] PlSqlParser.Bounds_clauseContext context)
        {
            Stop();
            var result = base.VisitBounds_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCall_spec([NotNull] PlSqlParser.Call_specContext context)
        {
            Stop();
            var result = base.VisitCall_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCase_else_part([NotNull] PlSqlParser.Case_else_partContext context)
        {
            Stop();
            var result = base.VisitCase_else_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCase_statement([NotNull] PlSqlParser.Case_statementContext context)
        {
            Stop();
            var result = base.VisitCase_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCell_assignment([NotNull] PlSqlParser.Cell_assignmentContext context)
        {
            Stop();
            var result = base.VisitCell_assignment(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCell_reference_options([NotNull] PlSqlParser.Cell_reference_optionsContext context)
        {
            Stop();
            var result = base.VisitCell_reference_options(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCheck_constraint([NotNull] PlSqlParser.Check_constraintContext context)
        {
            Stop();
            var result = base.VisitCheck_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitClose_statement([NotNull] PlSqlParser.Close_statementContext context)
        {
            Stop();
            var result = base.VisitClose_statement(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitCollect_order_by_part([NotNull] PlSqlParser.Collect_order_by_partContext context)
        {
            Stop();
            var result = base.VisitCollect_order_by_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitColumn_alias([NotNull] PlSqlParser.Column_aliasContext context)
        {
            Stop();
            var result = base.VisitColumn_alias(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitColumn_based_update_set_clause([NotNull] PlSqlParser.Column_based_update_set_clauseContext context)
        {
            Stop();
            var result = base.VisitColumn_based_update_set_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitColumn_list([NotNull] PlSqlParser.Column_listContext context)
        {
            Stop();
            var result = base.VisitColumn_list(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitComment_on_column([NotNull] PlSqlParser.Comment_on_columnContext context)
        {
            Stop();
            var result = base.VisitComment_on_column(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitComment_on_table([NotNull] PlSqlParser.Comment_on_tableContext context)
        {
            Stop();
            var result = base.VisitComment_on_table(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCommit_statement([NotNull] PlSqlParser.Commit_statementContext context)
        {
            Stop();
            var result = base.VisitCommit_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCompiler_parameters_clause([NotNull] PlSqlParser.Compiler_parameters_clauseContext context)
        {
            Stop();
            var result = base.VisitCompiler_parameters_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCompile_type_clause([NotNull] PlSqlParser.Compile_type_clauseContext context)
        {
            Stop();
            var result = base.VisitCompile_type_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCompound_dml_trigger([NotNull] PlSqlParser.Compound_dml_triggerContext context)
        {
            Stop();
            var result = base.VisitCompound_dml_trigger(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCompound_trigger_block([NotNull] PlSqlParser.Compound_trigger_blockContext context)
        {
            Stop();
            var result = base.VisitCompound_trigger_block(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCondition([NotNull] PlSqlParser.ConditionContext context)
        {
            Stop();
            var result = base.VisitCondition(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitConditional_insert_clause([NotNull] PlSqlParser.Conditional_insert_clauseContext context)
        {
            Stop();
            var result = base.VisitConditional_insert_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitConditional_insert_else_part([NotNull] PlSqlParser.Conditional_insert_else_partContext context)
        {
            Stop();
            var result = base.VisitConditional_insert_else_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitConditional_insert_when_part([NotNull] PlSqlParser.Conditional_insert_when_partContext context)
        {
            Stop();
            var result = base.VisitConditional_insert_when_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitConstraint_state([NotNull] PlSqlParser.Constraint_stateContext context)
        {
            Stop();
            var result = base.VisitConstraint_state(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitConstructor_declaration([NotNull] PlSqlParser.Constructor_declarationContext context)
        {
            Stop();
            var result = base.VisitConstructor_declaration(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitConstructor_spec([NotNull] PlSqlParser.Constructor_specContext context)
        {
            Stop();
            var result = base.VisitConstructor_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitContainer_clause([NotNull] PlSqlParser.Container_clauseContext context)
        {
            Stop();
            var result = base.VisitContainer_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitContainer_data_clause([NotNull] PlSqlParser.Container_data_clauseContext context)
        {
            Stop();
            var result = base.VisitContainer_data_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitContinue_statement([NotNull] PlSqlParser.Continue_statementContext context)
        {
            Stop();
            var result = base.VisitContinue_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCost_matrix_clause([NotNull] PlSqlParser.Cost_matrix_clauseContext context)
        {
            Stop();
            var result = base.VisitCost_matrix_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCreate_index([NotNull] PlSqlParser.Create_indexContext context)
        {
            Stop();
            var result = base.VisitCreate_index(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCreate_table([NotNull] PlSqlParser.Create_tableContext context)
        {
            Stop();
            var result = base.VisitCreate_table(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCreate_tablespace([NotNull] PlSqlParser.Create_tablespaceContext context)
        {
            Stop();
            var result = base.VisitCreate_tablespace(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCreate_trigger([NotNull] PlSqlParser.Create_triggerContext context)
        {
            Stop();
            var result = base.VisitCreate_trigger(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCreate_type([NotNull] PlSqlParser.Create_typeContext context)
        {
            Stop();
            var result = base.VisitCreate_type(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCreate_user([NotNull] PlSqlParser.Create_userContext context)
        {
            Stop();
            var result = base.VisitCreate_user(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCreate_view([NotNull] PlSqlParser.Create_viewContext context)
        {
            Stop();
            var result = base.VisitCreate_view(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitCycle_clause([NotNull] PlSqlParser.Cycle_clauseContext context)
        {
            Stop();
            var result = base.VisitCycle_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitC_agent_in_clause([NotNull] PlSqlParser.C_agent_in_clauseContext context)
        {
            Stop();
            var result = base.VisitC_agent_in_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitC_parameters_clause([NotNull] PlSqlParser.C_parameters_clauseContext context)
        {
            Stop();
            var result = base.VisitC_parameters_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitC_spec([NotNull] PlSqlParser.C_specContext context)
        {
            Stop();
            var result = base.VisitC_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDatafile_specification([NotNull] PlSqlParser.Datafile_specificationContext context)
        {
            Stop();
            var result = base.VisitDatafile_specification(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDatafile_tempfile_spec([NotNull] PlSqlParser.Datafile_tempfile_specContext context)
        {
            Stop();
            var result = base.VisitDatafile_tempfile_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitData_manipulation_language_statements([NotNull] PlSqlParser.Data_manipulation_language_statementsContext context)
        {
            Stop();
            var result = base.VisitData_manipulation_language_statements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDeclare_spec([NotNull] PlSqlParser.Declare_specContext context)
        {
            Stop();
            var result = base.VisitDeclare_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDelete_statement([NotNull] PlSqlParser.Delete_statementContext context)
        {
            Stop();
            var result = base.VisitDelete_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDependent_exceptions_part([NotNull] PlSqlParser.Dependent_exceptions_partContext context)
        {
            Stop();
            var result = base.VisitDependent_exceptions_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDependent_handling_clause([NotNull] PlSqlParser.Dependent_handling_clauseContext context)
        {
            Stop();
            var result = base.VisitDependent_handling_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDisable_constraint([NotNull] PlSqlParser.Disable_constraintContext context)
        {
            Stop();
            var result = base.VisitDisable_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDml_event_clause([NotNull] PlSqlParser.Dml_event_clauseContext context)
        {
            Stop();
            var result = base.VisitDml_event_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDml_event_element([NotNull] PlSqlParser.Dml_event_elementContext context)
        {
            Stop();
            var result = base.VisitDml_event_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDml_event_nested_clause([NotNull] PlSqlParser.Dml_event_nested_clauseContext context)
        {
            Stop();
            var result = base.VisitDml_event_nested_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDml_table_expression_clause([NotNull] PlSqlParser.Dml_table_expression_clauseContext context)
        {
            Stop();
            var result = base.VisitDml_table_expression_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDrop_constraint([NotNull] PlSqlParser.Drop_constraintContext context)
        {
            Stop();
            var result = base.VisitDrop_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDrop_function([NotNull] PlSqlParser.Drop_functionContext context)
        {
            Stop();
            var result = base.VisitDrop_function(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDrop_index([NotNull] PlSqlParser.Drop_indexContext context)
        {
            Stop();
            var result = base.VisitDrop_index(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDrop_table([NotNull] PlSqlParser.Drop_tableContext context)
        {
            Stop();
            var result = base.VisitDrop_table(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDrop_trigger([NotNull] PlSqlParser.Drop_triggerContext context)
        {
            Stop();
            var result = base.VisitDrop_trigger(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDrop_type([NotNull] PlSqlParser.Drop_typeContext context)
        {
            Stop();
            var result = base.VisitDrop_type(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitDynamic_returning_clause([NotNull] PlSqlParser.Dynamic_returning_clauseContext context)
        {
            Stop();
            var result = base.VisitDynamic_returning_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitElement_spec([NotNull] PlSqlParser.Element_specContext context)
        {
            Stop();
            var result = base.VisitElement_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitElement_spec_options([NotNull] PlSqlParser.Element_spec_optionsContext context)
        {
            Stop();
            var result = base.VisitElement_spec_options(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitElse_part([NotNull] PlSqlParser.Else_partContext context)
        {
            Stop();
            var result = base.VisitElse_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitElsif_part([NotNull] PlSqlParser.Elsif_partContext context)
        {
            Stop();
            var result = base.VisitElsif_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitEnable_constraint([NotNull] PlSqlParser.Enable_constraintContext context)
        {
            Stop();
            var result = base.VisitEnable_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitError_logging_clause([NotNull] PlSqlParser.Error_logging_clauseContext context)
        {
            Stop();
            var result = base.VisitError_logging_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitError_logging_into_part([NotNull] PlSqlParser.Error_logging_into_partContext context)
        {
            Stop();
            var result = base.VisitError_logging_into_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitError_logging_reject_part([NotNull] PlSqlParser.Error_logging_reject_partContext context)
        {
            Stop();
            var result = base.VisitError_logging_reject_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitExecute_immediate([NotNull] PlSqlParser.Execute_immediateContext context)
        {
            Stop();
            var result = base.VisitExecute_immediate(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitExit_statement([NotNull] PlSqlParser.Exit_statementContext context)
        {
            Stop();
            var result = base.VisitExit_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitExplain_statement([NotNull] PlSqlParser.Explain_statementContext context)
        {
            Stop();
            var result = base.VisitExplain_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitExtent_management_clause([NotNull] PlSqlParser.Extent_management_clauseContext context)
        {
            Stop();
            var result = base.VisitExtent_management_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFactoring_element([NotNull] PlSqlParser.Factoring_elementContext context)
        {
            Stop();
            var result = base.VisitFactoring_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFetch_statement([NotNull] PlSqlParser.Fetch_statementContext context)
        {
            Stop();
            var result = base.VisitFetch_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitField_spec([NotNull] PlSqlParser.Field_specContext context)
        {
            Stop();
            var result = base.VisitField_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFlashback_mode_clause([NotNull] PlSqlParser.Flashback_mode_clauseContext context)
        {
            Stop();
            var result = base.VisitFlashback_mode_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFlashback_query_clause([NotNull] PlSqlParser.Flashback_query_clauseContext context)
        {
            Stop();
            var result = base.VisitFlashback_query_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitForall_statement([NotNull] PlSqlParser.Forall_statementContext context)
        {
            Stop();
            var result = base.VisitForall_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitForeign_key_clause([NotNull] PlSqlParser.Foreign_key_clauseContext context)
        {
            Stop();
            var result = base.VisitForeign_key_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFor_each_row([NotNull] PlSqlParser.For_each_rowContext context)
        {
            Stop();
            var result = base.VisitFor_each_row(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFor_update_clause([NotNull] PlSqlParser.For_update_clauseContext context)
        {
            Stop();
            var result = base.VisitFor_update_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFor_update_of_part([NotNull] PlSqlParser.For_update_of_partContext context)
        {
            Stop();
            var result = base.VisitFor_update_of_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFor_update_options([NotNull] PlSqlParser.For_update_optionsContext context)
        {
            Stop();
            var result = base.VisitFor_update_options(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFrom_clause([NotNull] PlSqlParser.From_clauseContext context)
        {
            Stop();
            var result = base.VisitFrom_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitFunc_decl_in_type([NotNull] PlSqlParser.Func_decl_in_typeContext context)
        {
            Stop();
            var result = base.VisitFunc_decl_in_type(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitGeneral_table_ref([NotNull] PlSqlParser.General_table_refContext context)
        {
            Stop();
            var result = base.VisitGeneral_table_ref(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitGoto_statement([NotNull] PlSqlParser.Goto_statementContext context)
        {
            Stop();
            var result = base.VisitGoto_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitGrouping_sets_clause([NotNull] PlSqlParser.Grouping_sets_clauseContext context)
        {
            Stop();
            var result = base.VisitGrouping_sets_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitGrouping_sets_elements([NotNull] PlSqlParser.Grouping_sets_elementsContext context)
        {
            Stop();
            var result = base.VisitGrouping_sets_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitGroup_by_clause([NotNull] PlSqlParser.Group_by_clauseContext context)
        {
            Stop();
            var result = base.VisitGroup_by_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitGroup_by_elements([NotNull] PlSqlParser.Group_by_elementsContext context)
        {
            Stop();
            var result = base.VisitGroup_by_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitHaving_clause([NotNull] PlSqlParser.Having_clauseContext context)
        {
            Stop();
            var result = base.VisitHaving_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitHierarchical_query_clause([NotNull] PlSqlParser.Hierarchical_query_clauseContext context)
        {
            Stop();
            var result = base.VisitHierarchical_query_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitIdentified_by([NotNull] PlSqlParser.Identified_byContext context)
        {
            Stop();
            var result = base.VisitIdentified_by(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitIdentified_other_clause([NotNull] PlSqlParser.Identified_other_clauseContext context)
        {
            Stop();
            var result = base.VisitIdentified_other_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitIf_statement([NotNull] PlSqlParser.If_statementContext context)
        {
            Stop();
            var result = base.VisitIf_statement(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitInline_constraint([NotNull] PlSqlParser.Inline_constraintContext context)
        {
            Stop();
            var result = base.VisitInline_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitInsert_into_clause([NotNull] PlSqlParser.Insert_into_clauseContext context)
        {
            Stop();
            var result = base.VisitInsert_into_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitInsert_statement([NotNull] PlSqlParser.Insert_statementContext context)
        {
            Stop();
            var result = base.VisitInsert_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitInterval_expression([NotNull] PlSqlParser.Interval_expressionContext context)
        {
            Stop();
            var result = base.VisitInterval_expression(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitInto_clause([NotNull] PlSqlParser.Into_clauseContext context)
        {
            Stop();
            var result = base.VisitInto_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitJava_spec([NotNull] PlSqlParser.Java_specContext context)
        {
            Stop();
            var result = base.VisitJava_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitJoin_clause([NotNull] PlSqlParser.Join_clauseContext context)
        {
            Stop();
            var result = base.VisitJoin_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitJoin_on_part([NotNull] PlSqlParser.Join_on_partContext context)
        {
            Stop();
            var result = base.VisitJoin_on_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitJoin_using_part([NotNull] PlSqlParser.Join_using_partContext context)
        {
            Stop();
            var result = base.VisitJoin_using_part(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitLabel_declaration([NotNull] PlSqlParser.Label_declarationContext context)
        {
            Stop();
            var result = base.VisitLabel_declaration(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitLock_mode([NotNull] PlSqlParser.Lock_modeContext context)
        {
            Stop();
            var result = base.VisitLock_mode(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitLock_table_element([NotNull] PlSqlParser.Lock_table_elementContext context)
        {
            Stop();
            var result = base.VisitLock_table_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitLock_table_statement([NotNull] PlSqlParser.Lock_table_statementContext context)
        {
            Stop();
            var result = base.VisitLock_table_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitLogging_clause([NotNull] PlSqlParser.Logging_clauseContext context)
        {
            Stop();
            var result = base.VisitLogging_clause(context);
            Debug.Assert(result != null);
            return result;
        }



        public override object VisitLoop_statement([NotNull] PlSqlParser.Loop_statementContext context)
        {
            Stop();
            var result = base.VisitLoop_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitLower_bound([NotNull] PlSqlParser.Lower_boundContext context)
        {
            Stop();
            var result = base.VisitLower_bound(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMain_model([NotNull] PlSqlParser.Main_modelContext context)
        {
            Stop();
            var result = base.VisitMain_model(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitMap_order_function_spec([NotNull] PlSqlParser.Map_order_function_specContext context)
        {
            Stop();
            var result = base.VisitMap_order_function_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMap_order_func_declaration([NotNull] PlSqlParser.Map_order_func_declarationContext context)
        {
            Stop();
            var result = base.VisitMap_order_func_declaration(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMaxsize_clause([NotNull] PlSqlParser.Maxsize_clauseContext context)
        {
            Stop();
            var result = base.VisitMaxsize_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMerge_element([NotNull] PlSqlParser.Merge_elementContext context)
        {
            Stop();
            var result = base.VisitMerge_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMerge_insert_clause([NotNull] PlSqlParser.Merge_insert_clauseContext context)
        {
            Stop();
            var result = base.VisitMerge_insert_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMerge_statement([NotNull] PlSqlParser.Merge_statementContext context)
        {
            Stop();
            var result = base.VisitMerge_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMerge_update_clause([NotNull] PlSqlParser.Merge_update_clauseContext context)
        {
            Stop();
            var result = base.VisitMerge_update_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMerge_update_delete_part([NotNull] PlSqlParser.Merge_update_delete_partContext context)
        {
            Stop();
            var result = base.VisitMerge_update_delete_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_clause([NotNull] PlSqlParser.Model_clauseContext context)
        {
            Stop();
            var result = base.VisitModel_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_column([NotNull] PlSqlParser.Model_columnContext context)
        {
            Stop();
            var result = base.VisitModel_column(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_column_clauses([NotNull] PlSqlParser.Model_column_clausesContext context)
        {
            Stop();
            var result = base.VisitModel_column_clauses(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_column_list([NotNull] PlSqlParser.Model_column_listContext context)
        {
            Stop();
            var result = base.VisitModel_column_list(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_column_partition_part([NotNull] PlSqlParser.Model_column_partition_partContext context)
        {
            Stop();
            var result = base.VisitModel_column_partition_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_iterate_clause([NotNull] PlSqlParser.Model_iterate_clauseContext context)
        {
            Stop();
            var result = base.VisitModel_iterate_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_rules_clause([NotNull] PlSqlParser.Model_rules_clauseContext context)
        {
            Stop();
            var result = base.VisitModel_rules_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_rules_element([NotNull] PlSqlParser.Model_rules_elementContext context)
        {
            Stop();
            var result = base.VisitModel_rules_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModel_rules_part([NotNull] PlSqlParser.Model_rules_partContext context)
        {
            Stop();
            var result = base.VisitModel_rules_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitModifier_clause([NotNull] PlSqlParser.Modifier_clauseContext context)
        {
            Stop();
            var result = base.VisitModifier_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMulti_table_element([NotNull] PlSqlParser.Multi_table_elementContext context)
        {
            Stop();
            var result = base.VisitMulti_table_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitMulti_table_insert([NotNull] PlSqlParser.Multi_table_insertContext context)
        {
            Stop();
            var result = base.VisitMulti_table_insert(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitNested_table_type_def([NotNull] PlSqlParser.Nested_table_type_defContext context)
        {
            Stop();
            var result = base.VisitNested_table_type_def(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitNon_dml_event([NotNull] PlSqlParser.Non_dml_eventContext context)
        {
            Stop();
            var result = base.VisitNon_dml_event(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitNon_dml_trigger([NotNull] PlSqlParser.Non_dml_triggerContext context)
        {
            Stop();
            var result = base.VisitNon_dml_trigger(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitNull_statement([NotNull] PlSqlParser.Null_statementContext context)
        {
            Stop();
            var result = base.VisitNull_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitNumeric_function([NotNull] PlSqlParser.Numeric_functionContext context)
        {
            Stop();
            var result = base.VisitNumeric_function(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitNumeric_function_wrapper([NotNull] PlSqlParser.Numeric_function_wrapperContext context)
        {
            Stop();
            var result = base.VisitNumeric_function_wrapper(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitNumeric_negative([NotNull] PlSqlParser.Numeric_negativeContext context)
        {
            Stop();
            var result = base.VisitNumeric_negative(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitObject_as_part([NotNull] PlSqlParser.Object_as_partContext context)
        {
            Stop();
            var result = base.VisitObject_as_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitObject_member_spec([NotNull] PlSqlParser.Object_member_specContext context)
        {
            Stop();
            var result = base.VisitObject_member_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitObject_type_def([NotNull] PlSqlParser.Object_type_defContext context)
        {
            Stop();
            var result = base.VisitObject_type_def(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitObject_under_part([NotNull] PlSqlParser.Object_under_partContext context)
        {
            Stop();
            var result = base.VisitObject_under_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitObject_view_clause([NotNull] PlSqlParser.Object_view_clauseContext context)
        {
            Stop();
            var result = base.VisitObject_view_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOn_delete_clause([NotNull] PlSqlParser.On_delete_clauseContext context)
        {
            Stop();
            var result = base.VisitOn_delete_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOpen_for_statement([NotNull] PlSqlParser.Open_for_statementContext context)
        {
            Stop();
            var result = base.VisitOpen_for_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOpen_statement([NotNull] PlSqlParser.Open_statementContext context)
        {
            Stop();
            var result = base.VisitOpen_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOrder_by_clause([NotNull] PlSqlParser.Order_by_clauseContext context)
        {
            Stop();
            var result = base.VisitOrder_by_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOrder_by_elements([NotNull] PlSqlParser.Order_by_elementsContext context)
        {
            Stop();
            var result = base.VisitOrder_by_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOther_function([NotNull] PlSqlParser.Other_functionContext context)
        {
            Stop();
            var result = base.VisitOther_function(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOuter_join_sign([NotNull] PlSqlParser.Outer_join_signContext context)
        {
            Stop();
            var result = base.VisitOuter_join_sign(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOuter_join_type([NotNull] PlSqlParser.Outer_join_typeContext context)
        {
            Stop();
            var result = base.VisitOuter_join_type(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOut_of_line_constraint([NotNull] PlSqlParser.Out_of_line_constraintContext context)
        {
            Stop();
            var result = base.VisitOut_of_line_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOver_clause([NotNull] PlSqlParser.Over_clauseContext context)
        {
            Stop();
            var result = base.VisitOver_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitOver_clause_keyword([NotNull] PlSqlParser.Over_clause_keywordContext context)
        {
            Stop();
            var result = base.VisitOver_clause_keyword(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitParallel_enable_clause([NotNull] PlSqlParser.Parallel_enable_clauseContext context)
        {
            Stop();
            var result = base.VisitParallel_enable_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitParen_column_list([NotNull] PlSqlParser.Paren_column_listContext context)
        {
            Stop();
            var result = base.VisitParen_column_list(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPartition_by_clause([NotNull] PlSqlParser.Partition_by_clauseContext context)
        {
            Stop();
            var result = base.VisitPartition_by_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPartition_extension_clause([NotNull] PlSqlParser.Partition_extension_clauseContext context)
        {
            Stop();
            var result = base.VisitPartition_extension_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPassword_expire_clause([NotNull] PlSqlParser.Password_expire_clauseContext context)
        {
            Stop();
            var result = base.VisitPassword_expire_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPermanent_tablespace_clause([NotNull] PlSqlParser.Permanent_tablespace_clauseContext context)
        {
            Stop();
            var result = base.VisitPermanent_tablespace_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPipe_row_statement([NotNull] PlSqlParser.Pipe_row_statementContext context)
        {
            Stop();
            var result = base.VisitPipe_row_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPivot_clause([NotNull] PlSqlParser.Pivot_clauseContext context)
        {
            Stop();
            var result = base.VisitPivot_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPivot_element([NotNull] PlSqlParser.Pivot_elementContext context)
        {
            Stop();
            var result = base.VisitPivot_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPivot_for_clause([NotNull] PlSqlParser.Pivot_for_clauseContext context)
        {
            Stop();
            var result = base.VisitPivot_for_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPivot_in_clause([NotNull] PlSqlParser.Pivot_in_clauseContext context)
        {
            Stop();
            var result = base.VisitPivot_in_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPivot_in_clause_element([NotNull] PlSqlParser.Pivot_in_clause_elementContext context)
        {
            Stop();
            var result = base.VisitPivot_in_clause_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPivot_in_clause_elements([NotNull] PlSqlParser.Pivot_in_clause_elementsContext context)
        {
            Stop();
            var result = base.VisitPivot_in_clause_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPrecision_part([NotNull] PlSqlParser.Precision_partContext context)
        {
            Stop();
            var result = base.VisitPrecision_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitPrimary_key_clause([NotNull] PlSqlParser.Primary_key_clauseContext context)
        {
            Stop();
            var result = base.VisitPrimary_key_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitProfile_clause([NotNull] PlSqlParser.Profile_clauseContext context)
        {
            Stop();
            var result = base.VisitProfile_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitProxy_clause([NotNull] PlSqlParser.Proxy_clauseContext context)
        {
            Stop();
            var result = base.VisitProxy_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitQuantified_expression([NotNull] PlSqlParser.Quantified_expressionContext context)
        {
            Stop();
            var result = base.VisitQuantified_expression(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitQuery_block([NotNull] PlSqlParser.Query_blockContext context)
        {
            Stop();
            var result = base.VisitQuery_block(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitQuery_partition_clause([NotNull] PlSqlParser.Query_partition_clauseContext context)
        {
            Stop();
            var result = base.VisitQuery_partition_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitQuota_clause([NotNull] PlSqlParser.Quota_clauseContext context)
        {
            Stop();
            var result = base.VisitQuota_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRaise_statement([NotNull] PlSqlParser.Raise_statementContext context)
        {
            Stop();
            var result = base.VisitRaise_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRecord_type_def([NotNull] PlSqlParser.Record_type_defContext context)
        {
            Stop();
            var result = base.VisitRecord_type_def(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRedo_log_file_spec([NotNull] PlSqlParser.Redo_log_file_specContext context)
        {
            Stop();
            var result = base.VisitRedo_log_file_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitReferences_clause([NotNull] PlSqlParser.References_clauseContext context)
        {
            Stop();
            var result = base.VisitReferences_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitReference_model([NotNull] PlSqlParser.Reference_modelContext context)
        {
            Stop();
            var result = base.VisitReference_model(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitReferencing_clause([NotNull] PlSqlParser.Referencing_clauseContext context)
        {
            Stop();
            var result = base.VisitReferencing_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitReferencing_element([NotNull] PlSqlParser.Referencing_elementContext context)
        {
            Stop();
            var result = base.VisitReferencing_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRef_cursor_type_def([NotNull] PlSqlParser.Ref_cursor_type_defContext context)
        {
            //Stop();
            var result = base.VisitRef_cursor_type_def(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRelies_on_part([NotNull] PlSqlParser.Relies_on_partContext context)
        {
            Stop();
            var result = base.VisitRelies_on_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitReplace_type_clause([NotNull] PlSqlParser.Replace_type_clauseContext context)
        {
            Stop();
            var result = base.VisitReplace_type_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRespect_or_ignore_nulls([NotNull] PlSqlParser.Respect_or_ignore_nullsContext context)
        {
            Stop();
            var result = base.VisitRespect_or_ignore_nulls(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitResult_cache_clause([NotNull] PlSqlParser.Result_cache_clauseContext context)
        {
            Stop();
            var result = base.VisitResult_cache_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitReturn_rows_clause([NotNull] PlSqlParser.Return_rows_clauseContext context)
        {
            Stop();
            var result = base.VisitReturn_rows_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitReturn_statement([NotNull] PlSqlParser.Return_statementContext context)
        {
            Stop();
            var result = base.VisitReturn_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRole_clause([NotNull] PlSqlParser.Role_clauseContext context)
        {
            Stop();
            var result = base.VisitRole_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRollback_statement([NotNull] PlSqlParser.Rollback_statementContext context)
        {
            Stop();
            var result = base.VisitRollback_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRollup_cube_clause([NotNull] PlSqlParser.Rollup_cube_clauseContext context)
        {
            Stop();
            var result = base.VisitRollup_cube_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitRoutine_clause([NotNull] PlSqlParser.Routine_clauseContext context)
        {
            Stop();
            var result = base.VisitRoutine_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSample_clause([NotNull] PlSqlParser.Sample_clauseContext context)
        {
            Stop();
            var result = base.VisitSample_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSavepoint_statement([NotNull] PlSqlParser.Savepoint_statementContext context)
        {
            Stop();
            var result = base.VisitSavepoint_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSearched_case_statement([NotNull] PlSqlParser.Searched_case_statementContext context)
        {
            Stop();
            var result = base.VisitSearched_case_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSearched_case_when_part([NotNull] PlSqlParser.Searched_case_when_partContext context)
        {
            Stop();
            var result = base.VisitSearched_case_when_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSearch_clause([NotNull] PlSqlParser.Search_clauseContext context)
        {
            Stop();
            var result = base.VisitSearch_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSeed_part([NotNull] PlSqlParser.Seed_partContext context)
        {
            Stop();
            var result = base.VisitSeed_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSegment_management_clause([NotNull] PlSqlParser.Segment_management_clauseContext context)
        {
            Stop();
            var result = base.VisitSegment_management_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSelected_element([NotNull] PlSqlParser.Selected_elementContext context)
        {
            Stop();
            var result = base.VisitSelected_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSelected_tableview([NotNull] PlSqlParser.Selected_tableviewContext context)
        {
            Stop();
            var result = base.VisitSelected_tableview(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSelect_list_elements([NotNull] PlSqlParser.Select_list_elementsContext context)
        {
            Stop();
            var result = base.VisitSelect_list_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSelect_statement([NotNull] PlSqlParser.Select_statementContext context)
        {
            Stop();
            var result = base.VisitSelect_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSeq_of_declare_specs([NotNull] PlSqlParser.Seq_of_declare_specsContext context)
        {
            Stop();
            var result = base.VisitSeq_of_declare_specs(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSeq_of_statements([NotNull] PlSqlParser.Seq_of_statementsContext context)
        {
            Stop();
            var result = base.VisitSeq_of_statements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSet_constraint_command([NotNull] PlSqlParser.Set_constraint_commandContext context)
        {
            Stop();
            var result = base.VisitSet_constraint_command(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSet_container_data([NotNull] PlSqlParser.Set_container_dataContext context)
        {
            Stop();
            var result = base.VisitSet_container_data(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSet_transaction_command([NotNull] PlSqlParser.Set_transaction_commandContext context)
        {
            Stop();
            var result = base.VisitSet_transaction_command(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSimple_case_statement([NotNull] PlSqlParser.Simple_case_statementContext context)
        {
            Stop();
            var result = base.VisitSimple_case_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSimple_case_when_part([NotNull] PlSqlParser.Simple_case_when_partContext context)
        {
            Stop();
            var result = base.VisitSimple_case_when_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSimple_dml_trigger([NotNull] PlSqlParser.Simple_dml_triggerContext context)
        {
            Stop();
            var result = base.VisitSimple_dml_trigger(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSingle_table_insert([NotNull] PlSqlParser.Single_table_insertContext context)
        {
            Stop();
            var result = base.VisitSingle_table_insert(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSize_clause([NotNull] PlSqlParser.Size_clauseContext context)
        {
            Stop();
            var result = base.VisitSize_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSqlj_object_type([NotNull] PlSqlParser.Sqlj_object_typeContext context)
        {
            Stop();
            var result = base.VisitSqlj_object_type(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSqlj_object_type_attr([NotNull] PlSqlParser.Sqlj_object_type_attrContext context)
        {
            Stop();
            var result = base.VisitSqlj_object_type_attr(context);
            Debug.Assert(result != null);
            return result;
        }

        //public override object VisitSql_plus_command([NotNull] PlSqlParser.Sql_plus_commandContext context)
        //{
        //    Stop();
        //    var result = base.VisitSql_plus_command(context);
        //}

        //public override object VisitSql_statement([NotNull] PlSqlParser.Sql_statementContext context)
        //{
        //    Stop();
        //    var result = base.VisitSql_statement(context);
        //}

        public override object VisitStandard_function([NotNull] PlSqlParser.Standard_functionContext context)
        {
            Stop();
            var result = base.VisitStandard_function(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitStandard_prediction_function_keyword([NotNull] PlSqlParser.Standard_prediction_function_keywordContext context)
        {
            Stop();
            var result = base.VisitStandard_prediction_function_keyword(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitStart_part([NotNull] PlSqlParser.Start_partContext context)
        {
            Stop();
            var result = base.VisitStart_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitStatement([NotNull] PlSqlParser.StatementContext context)
        {
            Stop();
            var result = base.VisitStatement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitStatic_returning_clause([NotNull] PlSqlParser.Static_returning_clauseContext context)
        {
            Stop();
            var result = base.VisitStatic_returning_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitStreaming_clause([NotNull] PlSqlParser.Streaming_clauseContext context)
        {
            Stop();
            var result = base.VisitStreaming_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitString_function([NotNull] PlSqlParser.String_functionContext context)
        {
            Stop();
            var result = base.VisitString_function(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSubprogram_spec([NotNull] PlSqlParser.Subprogram_specContext context)
        {
            Stop();
            var result = base.VisitSubprogram_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSubprog_decl_in_type([NotNull] PlSqlParser.Subprog_decl_in_typeContext context)
        {
            Stop();
            var result = base.VisitSubprog_decl_in_type(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSubquery([NotNull] PlSqlParser.SubqueryContext context)
        {
            Stop();
            var result = base.VisitSubquery(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSubquery_basic_elements([NotNull] PlSqlParser.Subquery_basic_elementsContext context)
        {
            Stop();
            var result = base.VisitSubquery_basic_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSubquery_factoring_clause([NotNull] PlSqlParser.Subquery_factoring_clauseContext context)
        {
            Stop();
            var result = base.VisitSubquery_factoring_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSubquery_restriction_clause([NotNull] PlSqlParser.Subquery_restriction_clauseContext context)
        {
            Stop();
            var result = base.VisitSubquery_restriction_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSwallow_to_semi([NotNull] PlSqlParser.Swallow_to_semiContext context)
        {
            Stop();
            var result = base.VisitSwallow_to_semi(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTablespace_encryption_spec([NotNull] PlSqlParser.Tablespace_encryption_specContext context)
        {
            Stop();
            var result = base.VisitTablespace_encryption_spec(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTablespace_group_clause([NotNull] PlSqlParser.Tablespace_group_clauseContext context)
        {
            Stop();
            var result = base.VisitTablespace_group_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTablespace_retention_clause([NotNull] PlSqlParser.Tablespace_retention_clauseContext context)
        {
            Stop();
            var result = base.VisitTablespace_retention_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_alias([NotNull] PlSqlParser.Table_aliasContext context)
        {
            Stop();
            var result = base.VisitTable_alias(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_collection_expression([NotNull] PlSqlParser.Table_collection_expressionContext context)
        {
            Stop();
            var result = base.VisitTable_collection_expression(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_element([NotNull] PlSqlParser.Table_elementContext context)
        {
            Stop();
            var result = base.VisitTable_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_indexed_by_part([NotNull] PlSqlParser.Table_indexed_by_partContext context)
        {
            Stop();
            var result = base.VisitTable_indexed_by_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_ref([NotNull] PlSqlParser.Table_refContext context)
        {
            Stop();
            var result = base.VisitTable_ref(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_ref_aux([NotNull] PlSqlParser.Table_ref_auxContext context)
        {
            Stop();
            var result = base.VisitTable_ref_aux(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_ref_aux_internal_one([NotNull] PlSqlParser.Table_ref_aux_internal_oneContext context)
        {
            Stop();
            var result = base.VisitTable_ref_aux_internal_one(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_ref_aux_internal_three([NotNull] PlSqlParser.Table_ref_aux_internal_threeContext context)
        {
            Stop();
            var result = base.VisitTable_ref_aux_internal_three(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_ref_aux_internal_two([NotNull] PlSqlParser.Table_ref_aux_internal_twoContext context)
        {
            Stop();
            var result = base.VisitTable_ref_aux_internal_two(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_ref_list([NotNull] PlSqlParser.Table_ref_listContext context)
        {
            Stop();
            var result = base.VisitTable_ref_list(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTable_type_def([NotNull] PlSqlParser.Table_type_defContext context)
        {
            Stop();
            var result = base.VisitTable_type_def(context);
            Debug.Assert(result != null);
            return result;
        }


        public override object VisitTempfile_specification([NotNull] PlSqlParser.Tempfile_specificationContext context)
        {
            Stop();
            var result = base.VisitTempfile_specification(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTemporary_tablespace_clause([NotNull] PlSqlParser.Temporary_tablespace_clauseContext context)
        {
            Stop();
            var result = base.VisitTemporary_tablespace_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTiming_point_section([NotNull] PlSqlParser.Timing_point_sectionContext context)
        {
            Stop();
            var result = base.VisitTiming_point_section(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTransaction_control_statements([NotNull] PlSqlParser.Transaction_control_statementsContext context)
        {
            Stop();
            var result = base.VisitTransaction_control_statements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTrigger_block([NotNull] PlSqlParser.Trigger_blockContext context)
        {
            Stop();
            var result = base.VisitTrigger_block(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTrigger_body([NotNull] PlSqlParser.Trigger_bodyContext context)
        {
            Stop();
            var result = base.VisitTrigger_body(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTrigger_follows_clause([NotNull] PlSqlParser.Trigger_follows_clauseContext context)
        {
            Stop();
            var result = base.VisitTrigger_follows_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitTrigger_when_clause([NotNull] PlSqlParser.Trigger_when_clauseContext context)
        {
            Stop();
            var result = base.VisitTrigger_when_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitType_body([NotNull] PlSqlParser.Type_bodyContext context)
        {
            Stop();
            var result = base.VisitType_body(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitType_body_elements([NotNull] PlSqlParser.Type_body_elementsContext context)
        {
            Stop();
            var result = base.VisitType_body_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// type_declaration :
        ///     TYPE identifier IS(table_type_def | varray_type_def | record_type_def | ref_cursor_type_def) ';'
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitType_declaration([NotNull] PlSqlParser.Type_declarationContext context)
        {

            var t = context.GetText();
            var names = context.identifier().GetCleanedTexts();

            var table_type_def = context.table_type_def();
            if (table_type_def != null)
            {
                Stop();

            }
            else
            {
                var varray_type_def = context.varray_type_def();
                if (varray_type_def != null)
                {
                    Stop();

                }
                else
                {
                    var record_type_def = context.record_type_def();
                    if (record_type_def!= null)
                    {
                        Stop();

                    }
                    else
                    {
                        Stop();
                        var ref_cursor_type_def = context.ref_cursor_type_def();

                    }
                }
            }

            // TYPE REF CURSOR IS REF CURSOR;
            var result = base.VisitType_declaration(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitType_definition([NotNull] PlSqlParser.Type_definitionContext context)
        {
            Stop();
            var result = base.VisitType_definition(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUndo_tablespace_clause([NotNull] PlSqlParser.Undo_tablespace_clauseContext context)
        {
            Stop();
            var result = base.VisitUndo_tablespace_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUnique_key_clause([NotNull] PlSqlParser.Unique_key_clauseContext context)
        {
            Stop();
            var result = base.VisitUnique_key_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUnpivot_clause([NotNull] PlSqlParser.Unpivot_clauseContext context)
        {
            Stop();
            var result = base.VisitUnpivot_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUnpivot_in_clause([NotNull] PlSqlParser.Unpivot_in_clauseContext context)
        {
            Stop();
            var result = base.VisitUnpivot_in_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUnpivot_in_elements([NotNull] PlSqlParser.Unpivot_in_elementsContext context)
        {
            Stop();
            var result = base.VisitUnpivot_in_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUntil_part([NotNull] PlSqlParser.Until_partContext context)
        {
            Stop();
            var result = base.VisitUntil_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUpdate_set_clause([NotNull] PlSqlParser.Update_set_clauseContext context)
        {
            Stop();
            var result = base.VisitUpdate_set_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUpdate_statement([NotNull] PlSqlParser.Update_statementContext context)
        {
            Stop();
            var result = base.VisitUpdate_statement(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUpper_bound([NotNull] PlSqlParser.Upper_boundContext context)
        {
            Stop();
            var result = base.VisitUpper_bound(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUser_default_role_clause([NotNull] PlSqlParser.User_default_role_clauseContext context)
        {
            Stop();
            var result = base.VisitUser_default_role_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUser_editions_clause([NotNull] PlSqlParser.User_editions_clauseContext context)
        {
            Stop();
            var result = base.VisitUser_editions_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUser_lock_clause([NotNull] PlSqlParser.User_lock_clauseContext context)
        {
            Stop();
            var result = base.VisitUser_lock_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUser_tablespace_clause([NotNull] PlSqlParser.User_tablespace_clauseContext context)
        {
            Stop();
            var result = base.VisitUser_tablespace_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUsing_clause([NotNull] PlSqlParser.Using_clauseContext context)
        {
            Stop();
            var result = base.VisitUsing_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitUsing_element([NotNull] PlSqlParser.Using_elementContext context)
        {
            Stop();
            var result = base.VisitUsing_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitValues_clause([NotNull] PlSqlParser.Values_clauseContext context)
        {
            Stop();
            var result = base.VisitValues_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitVarray_type_def([NotNull] PlSqlParser.Varray_type_defContext context)
        {
            Stop();
            var result = base.VisitVarray_type_def(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitView_alias_constraint([NotNull] PlSqlParser.View_alias_constraintContext context)
        {
            Stop();
            var result = base.VisitView_alias_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitView_options([NotNull] PlSqlParser.View_optionsContext context)
        {
            Stop();
            var result = base.VisitView_options(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitWait_nowait_part([NotNull] PlSqlParser.Wait_nowait_partContext context)
        {
            Stop();
            var result = base.VisitWait_nowait_part(context);
            Debug.Assert(result != null);
            return result;
        }

        //public override object VisitSqlplus_execute_command([NotNull] PlSqlParser.Sqlplus_execute_commandContext context)
        //{
        //    Stop();
        //    var result = base.VisitSqlplus_execute_command(context);
        //}

        public override object VisitSqlplus_set_command([NotNull] PlSqlParser.Sqlplus_set_commandContext context)
        {
            Stop();
            var result = base.VisitSqlplus_set_command(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitSqlplus_whenever_command([NotNull] PlSqlParser.Sqlplus_whenever_commandContext context)
        {
            Stop();
            var result = base.VisitSqlplus_whenever_command(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitWhere_clause([NotNull] PlSqlParser.Where_clauseContext context)
        {
            Stop();
            var result = base.VisitWhere_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitWindowing_clause([NotNull] PlSqlParser.Windowing_clauseContext context)
        {
            Stop();
            var result = base.VisitWindowing_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitWindowing_elements([NotNull] PlSqlParser.Windowing_elementsContext context)
        {
            Stop();
            var result = base.VisitWindowing_elements(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitWindowing_type([NotNull] PlSqlParser.Windowing_typeContext context)
        {
            Stop();
            var result = base.VisitWindowing_type(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitWithin_or_over_clause_keyword([NotNull] PlSqlParser.Within_or_over_clause_keywordContext context)
        {
            Stop();
            var result = base.VisitWithin_or_over_clause_keyword(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitWithin_or_over_part([NotNull] PlSqlParser.Within_or_over_partContext context)
        {
            Stop();
            var result = base.VisitWithin_or_over_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitWrite_clause([NotNull] PlSqlParser.Write_clauseContext context)
        {
            Stop();
            var result = base.VisitWrite_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXmlroot_param_standalone_part([NotNull] PlSqlParser.Xmlroot_param_standalone_partContext context)
        {
            Stop();
            var result = base.VisitXmlroot_param_standalone_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXmlroot_param_version_part([NotNull] PlSqlParser.Xmlroot_param_version_partContext context)
        {
            Stop();
            var result = base.VisitXmlroot_param_version_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXmlserialize_param_enconding_part([NotNull] PlSqlParser.Xmlserialize_param_enconding_partContext context)
        {
            Stop();
            var result = base.VisitXmlserialize_param_enconding_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXmlserialize_param_ident_part([NotNull] PlSqlParser.Xmlserialize_param_ident_partContext context)
        {
            Stop();
            var result = base.VisitXmlserialize_param_ident_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXmlserialize_param_version_part([NotNull] PlSqlParser.Xmlserialize_param_version_partContext context)
        {
            Stop();
            var result = base.VisitXmlserialize_param_version_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXml_attributes_clause([NotNull] PlSqlParser.Xml_attributes_clauseContext context)
        {
            Stop();
            var result = base.VisitXml_attributes_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXml_general_default_part([NotNull] PlSqlParser.Xml_general_default_partContext context)
        {
            Stop();
            var result = base.VisitXml_general_default_part(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXml_multiuse_expression_element([NotNull] PlSqlParser.Xml_multiuse_expression_elementContext context)
        {
            Stop();
            var result = base.VisitXml_multiuse_expression_element(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXml_namespaces_clause([NotNull] PlSqlParser.Xml_namespaces_clauseContext context)
        {
            Stop();
            var result = base.VisitXml_namespaces_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitAdvanced_index_compression([NotNull] PlSqlParser.Advanced_index_compressionContext context)
        {
            var result = base.VisitAdvanced_index_compression(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXml_passing_clause([NotNull] PlSqlParser.Xml_passing_clauseContext context)
        {
            Stop();
            var result = base.VisitXml_passing_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        public override object VisitXml_table_column([NotNull] PlSqlParser.Xml_table_columnContext context)
        {
            Stop();
            var result = base.VisitXml_table_column(context);
            Debug.Assert(result != null);
            return result;
        }


    }

}


//public override object VisitChildren(IRuleNode node)
//{
//    //_models.Push(node);
//    //try
//    //{
//    var result = base.VisitChildren(node);
//    //}
//    //finally
//    //{
//    //_models.Pop();
//    //}
//}


//public override object VisitTerminal(ITerminalNode node)
//{
//    var t = node.GetText();
//    Stop();
//    var result = base.VisitTerminal(node);
//}

