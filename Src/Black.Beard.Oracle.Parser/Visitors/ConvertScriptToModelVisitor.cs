using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Oracle.Models;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor : PlSqlParserBaseVisitor<object>, IFile
    {


        public ConvertScriptToModelVisitor(OracleDatabase db)
        {
            this.db = db;
        }

        public override object Visit(IParseTree tree)
        {
            return base.Visit(tree);
        }

        public override object VisitSql_script([NotNull] PlSqlParser.Sql_scriptContext context)
        {
            return base.VisitSql_script(context);
        }

        public override object VisitUnit_statement([NotNull] PlSqlParser.Unit_statementContext context)
        {
            //Stop();
            return base.VisitUnit_statement(context);
        }

        public override object VisitAdd_constraint([NotNull] PlSqlParser.Add_constraintContext context)
        {
            Stop();
            return base.VisitAdd_constraint(context);
        }

        public override object VisitAdd_rem_container_data([NotNull] PlSqlParser.Add_rem_container_dataContext context)
        {
            Stop();
            return base.VisitAdd_rem_container_data(context);
        }

        public override object VisitAggregate_function_name([NotNull] PlSqlParser.Aggregate_function_nameContext context)
        {
            Stop();
            return base.VisitAggregate_function_name(context);
        }

        public override object VisitAlter_attribute_definition([NotNull] PlSqlParser.Alter_attribute_definitionContext context)
        {
            Stop();
            return base.VisitAlter_attribute_definition(context);
        }

        public override object VisitAlter_collection_clauses([NotNull] PlSqlParser.Alter_collection_clausesContext context)
        {
            Stop();
            return base.VisitAlter_collection_clauses(context);
        }

        public override object VisitAlter_function([NotNull] PlSqlParser.Alter_functionContext context)
        {
            Stop();
            return base.VisitAlter_function(context);
        }

        public override object VisitAlter_identified_by([NotNull] PlSqlParser.Alter_identified_byContext context)
        {
            Stop();
            return base.VisitAlter_identified_by(context);
        }

        public override object VisitAlter_index([NotNull] PlSqlParser.Alter_indexContext context)
        {
            Stop();
            return base.VisitAlter_index(context);
        }

        public override object VisitAlter_method_element([NotNull] PlSqlParser.Alter_method_elementContext context)
        {
            Stop();
            return base.VisitAlter_method_element(context);
        }

        public override object VisitAlter_method_spec([NotNull] PlSqlParser.Alter_method_specContext context)
        {
            Stop();
            return base.VisitAlter_method_spec(context);
        }

        public override object VisitAlter_package([NotNull] PlSqlParser.Alter_packageContext context)
        {
            Stop();
            return base.VisitAlter_package(context);
        }

        public override object VisitAlter_procedure([NotNull] PlSqlParser.Alter_procedureContext context)
        {
            Stop();
            return base.VisitAlter_procedure(context);
        }

        public override object VisitAlter_sequence([NotNull] PlSqlParser.Alter_sequenceContext context)
        {
            Stop();
            return base.VisitAlter_sequence(context);
        }

        public override object VisitAlter_table([NotNull] PlSqlParser.Alter_tableContext context)
        {
            Stop();
            return base.VisitAlter_table(context);
        }

        public override object VisitAlter_trigger([NotNull] PlSqlParser.Alter_triggerContext context)
        {
            Stop();
            return base.VisitAlter_trigger(context);
        }

        public override object VisitAlter_type([NotNull] PlSqlParser.Alter_typeContext context)
        {
            Stop();
            return base.VisitAlter_type(context);
        }

        public override object VisitAlter_user([NotNull] PlSqlParser.Alter_userContext context)
        {
            Stop();
            return base.VisitAlter_user(context);
        }

        public override object VisitAlter_user_editions_clause([NotNull] PlSqlParser.Alter_user_editions_clauseContext context)
        {
            Stop();
            return base.VisitAlter_user_editions_clause(context);
        }

        public override object VisitAnonymous_block([NotNull] PlSqlParser.Anonymous_blockContext context)
        {
            Stop();
            return base.VisitAnonymous_block(context);
        }

        public override object VisitArgument([NotNull] PlSqlParser.ArgumentContext context)
        {
            Stop();
            return base.VisitArgument(context);
        }

        public override object VisitAssignment_statement([NotNull] PlSqlParser.Assignment_statementContext context)
        {
            Stop();
            return base.VisitAssignment_statement(context);
        }

        public override object VisitAtom([NotNull] PlSqlParser.AtomContext context)
        {
            Stop();
            return base.VisitAtom(context);
        }

        public override object VisitAttribute_definition([NotNull] PlSqlParser.Attribute_definitionContext context)
        {
            Stop();
            return base.VisitAttribute_definition(context);
        }

        public override object VisitAttribute_name([NotNull] PlSqlParser.Attribute_nameContext context)
        {
            Stop();
            return base.VisitAttribute_name(context);
        }

        public override object VisitAutoextend_clause([NotNull] PlSqlParser.Autoextend_clauseContext context)
        {
            Stop();
            return base.VisitAutoextend_clause(context);
        }

        public override object VisitBetween_bound([NotNull] PlSqlParser.Between_boundContext context)
        {
            Stop();
            return base.VisitBetween_bound(context);
        }

        public override object VisitBetween_elements([NotNull] PlSqlParser.Between_elementsContext context)
        {
            Stop();
            return base.VisitBetween_elements(context);
        }

        public override object VisitBind_variable([NotNull] PlSqlParser.Bind_variableContext context)
        {
            Stop();
            return base.VisitBind_variable(context);
        }

        public override object VisitBlock([NotNull] PlSqlParser.BlockContext context)
        {
            Stop();
            return base.VisitBlock(context);
        }

        public override object VisitBody([NotNull] PlSqlParser.BodyContext context)
        {
            Stop();
            return base.VisitBody(context);
        }

        public override object VisitBounds_clause([NotNull] PlSqlParser.Bounds_clauseContext context)
        {
            Stop();
            return base.VisitBounds_clause(context);
        }

        public override object VisitCall_spec([NotNull] PlSqlParser.Call_specContext context)
        {
            Stop();
            return base.VisitCall_spec(context);
        }

        public override object VisitCase_else_part([NotNull] PlSqlParser.Case_else_partContext context)
        {
            Stop();
            return base.VisitCase_else_part(context);
        }

        public override object VisitCase_statement([NotNull] PlSqlParser.Case_statementContext context)
        {
            Stop();
            return base.VisitCase_statement(context);
        }

        public override object VisitCell_assignment([NotNull] PlSqlParser.Cell_assignmentContext context)
        {
            Stop();
            return base.VisitCell_assignment(context);
        }

        public override object VisitCell_reference_options([NotNull] PlSqlParser.Cell_reference_optionsContext context)
        {
            Stop();
            return base.VisitCell_reference_options(context);
        }

        public override object VisitChar_set_name([NotNull] PlSqlParser.Char_set_nameContext context)
        {
            Stop();
            return base.VisitChar_set_name(context);
        }

        public override object VisitCheck_constraint([NotNull] PlSqlParser.Check_constraintContext context)
        {
            Stop();
            return base.VisitCheck_constraint(context);
        }

        public override object VisitClose_statement([NotNull] PlSqlParser.Close_statementContext context)
        {
            Stop();
            return base.VisitClose_statement(context);
        }

        public override object VisitCollection_name([NotNull] PlSqlParser.Collection_nameContext context)
        {
            Stop();
            return base.VisitCollection_name(context);
        }

        public override object VisitCollect_order_by_part([NotNull] PlSqlParser.Collect_order_by_partContext context)
        {
            Stop();
            return base.VisitCollect_order_by_part(context);
        }

        public override object VisitColumn_alias([NotNull] PlSqlParser.Column_aliasContext context)
        {
            Stop();
            return base.VisitColumn_alias(context);
        }

        public override object VisitColumn_based_update_set_clause([NotNull] PlSqlParser.Column_based_update_set_clauseContext context)
        {
            Stop();
            return base.VisitColumn_based_update_set_clause(context);
        }

        public override object VisitColumn_list([NotNull] PlSqlParser.Column_listContext context)
        {
            Stop();
            return base.VisitColumn_list(context);
        }

        public override object VisitColumn_name([NotNull] PlSqlParser.Column_nameContext context)
        {
            Stop();
            return base.VisitColumn_name(context);
        }

        public override object VisitComment_on_column([NotNull] PlSqlParser.Comment_on_columnContext context)
        {
            Stop();
            return base.VisitComment_on_column(context);
        }

        public override object VisitComment_on_table([NotNull] PlSqlParser.Comment_on_tableContext context)
        {
            Stop();
            return base.VisitComment_on_table(context);
        }

        public override object VisitCommit_statement([NotNull] PlSqlParser.Commit_statementContext context)
        {
            Stop();
            return base.VisitCommit_statement(context);
        }

        public override object VisitCompiler_parameters_clause([NotNull] PlSqlParser.Compiler_parameters_clauseContext context)
        {
            Stop();
            return base.VisitCompiler_parameters_clause(context);
        }

        public override object VisitCompile_type_clause([NotNull] PlSqlParser.Compile_type_clauseContext context)
        {
            Stop();
            return base.VisitCompile_type_clause(context);
        }

        public override object VisitCompound_dml_trigger([NotNull] PlSqlParser.Compound_dml_triggerContext context)
        {
            Stop();
            return base.VisitCompound_dml_trigger(context);
        }

        public override object VisitCompound_expression([NotNull] PlSqlParser.Compound_expressionContext context)
        {
            Stop();
            return base.VisitCompound_expression(context);
        }

        public override object VisitCompound_trigger_block([NotNull] PlSqlParser.Compound_trigger_blockContext context)
        {
            Stop();
            return base.VisitCompound_trigger_block(context);
        }

        public override object VisitConcatenation([NotNull] PlSqlParser.ConcatenationContext context)
        {
            Stop();
            return base.VisitConcatenation(context);
        }

        public override object VisitCondition([NotNull] PlSqlParser.ConditionContext context)
        {
            Stop();
            return base.VisitCondition(context);
        }

        public override object VisitConditional_insert_clause([NotNull] PlSqlParser.Conditional_insert_clauseContext context)
        {
            Stop();
            return base.VisitConditional_insert_clause(context);
        }

        public override object VisitConditional_insert_else_part([NotNull] PlSqlParser.Conditional_insert_else_partContext context)
        {
            Stop();
            return base.VisitConditional_insert_else_part(context);
        }

        public override object VisitConditional_insert_when_part([NotNull] PlSqlParser.Conditional_insert_when_partContext context)
        {
            Stop();
            return base.VisitConditional_insert_when_part(context);
        }

        public override object VisitConstant([NotNull] PlSqlParser.ConstantContext context)
        {
            Stop();
            return base.VisitConstant(context);
        }

        public override object VisitConstraint_name([NotNull] PlSqlParser.Constraint_nameContext context)
        {
            Stop();
            return base.VisitConstraint_name(context);
        }

        public override object VisitConstraint_state([NotNull] PlSqlParser.Constraint_stateContext context)
        {
            Stop();
            return base.VisitConstraint_state(context);
        }

        public override object VisitConstructor_declaration([NotNull] PlSqlParser.Constructor_declarationContext context)
        {
            Stop();
            return base.VisitConstructor_declaration(context);
        }

        public override object VisitConstructor_spec([NotNull] PlSqlParser.Constructor_specContext context)
        {
            Stop();
            return base.VisitConstructor_spec(context);
        }

        public override object VisitContainer_clause([NotNull] PlSqlParser.Container_clauseContext context)
        {
            Stop();
            return base.VisitContainer_clause(context);
        }

        public override object VisitContainer_data_clause([NotNull] PlSqlParser.Container_data_clauseContext context)
        {
            Stop();
            return base.VisitContainer_data_clause(context);
        }

        public override object VisitContainer_names([NotNull] PlSqlParser.Container_namesContext context)
        {
            Stop();
            return base.VisitContainer_names(context);
        }

        public override object VisitContainer_tableview_name([NotNull] PlSqlParser.Container_tableview_nameContext context)
        {
            Stop();
            return base.VisitContainer_tableview_name(context);
        }

        public override object VisitContinue_statement([NotNull] PlSqlParser.Continue_statementContext context)
        {
            Stop();
            return base.VisitContinue_statement(context);
        }

        public override object VisitCost_class_name([NotNull] PlSqlParser.Cost_class_nameContext context)
        {
            Stop();
            return base.VisitCost_class_name(context);
        }

        public override object VisitCost_matrix_clause([NotNull] PlSqlParser.Cost_matrix_clauseContext context)
        {
            Stop();
            return base.VisitCost_matrix_clause(context);
        }

        public override object VisitCreate_function_body([NotNull] PlSqlParser.Create_function_bodyContext context)
        {
            Stop();
            return base.VisitCreate_function_body(context);
        }

        public override object VisitCreate_index([NotNull] PlSqlParser.Create_indexContext context)
        {
            Stop();
            return base.VisitCreate_index(context);
        }

        public override object VisitCreate_package([NotNull] PlSqlParser.Create_packageContext context)
        {
            Stop();
            return base.VisitCreate_package(context);
        }

        public override object VisitCreate_package_body([NotNull] PlSqlParser.Create_package_bodyContext context)
        {
            Stop();
            return base.VisitCreate_package_body(context);
        }

        public override object VisitCreate_procedure_body([NotNull] PlSqlParser.Create_procedure_bodyContext context)
        {
            Stop();
            return base.VisitCreate_procedure_body(context);
        }

        public override object VisitCreate_sequence([NotNull] PlSqlParser.Create_sequenceContext context)
        {
            Stop();
            return base.VisitCreate_sequence(context);
        }

        public override object VisitCreate_synonym([NotNull] PlSqlParser.Create_synonymContext context)
        {
            Stop();
            return base.VisitCreate_synonym(context);
        }

        public override object VisitCreate_table([NotNull] PlSqlParser.Create_tableContext context)
        {
            Stop();
            return base.VisitCreate_table(context);
        }

        public override object VisitCreate_tablespace([NotNull] PlSqlParser.Create_tablespaceContext context)
        {
            Stop();
            return base.VisitCreate_tablespace(context);
        }

        public override object VisitCreate_trigger([NotNull] PlSqlParser.Create_triggerContext context)
        {
            Stop();
            return base.VisitCreate_trigger(context);
        }

        public override object VisitCreate_type([NotNull] PlSqlParser.Create_typeContext context)
        {
            Stop();
            return base.VisitCreate_type(context);
        }

        public override object VisitCreate_user([NotNull] PlSqlParser.Create_userContext context)
        {
            Stop();
            return base.VisitCreate_user(context);
        }

        public override object VisitCreate_view([NotNull] PlSqlParser.Create_viewContext context)
        {
            Stop();
            return base.VisitCreate_view(context);
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

        public override object VisitCycle_clause([NotNull] PlSqlParser.Cycle_clauseContext context)
        {
            Stop();
            return base.VisitCycle_clause(context);
        }

        public override object VisitC_agent_in_clause([NotNull] PlSqlParser.C_agent_in_clauseContext context)
        {
            Stop();
            return base.VisitC_agent_in_clause(context);
        }

        public override object VisitC_parameters_clause([NotNull] PlSqlParser.C_parameters_clauseContext context)
        {
            Stop();
            return base.VisitC_parameters_clause(context);
        }

        public override object VisitC_spec([NotNull] PlSqlParser.C_specContext context)
        {
            Stop();
            return base.VisitC_spec(context);
        }

        public override object VisitDatafile_specification([NotNull] PlSqlParser.Datafile_specificationContext context)
        {
            Stop();
            return base.VisitDatafile_specification(context);
        }

        public override object VisitDatafile_tempfile_spec([NotNull] PlSqlParser.Datafile_tempfile_specContext context)
        {
            Stop();
            return base.VisitDatafile_tempfile_spec(context);
        }

        public override object VisitDatatype([NotNull] PlSqlParser.DatatypeContext context)
        {
            Stop();
            return base.VisitDatatype(context);
        }

        public override object VisitDatatype_null_enable([NotNull] PlSqlParser.Datatype_null_enableContext context)
        {
            Stop();
            return base.VisitDatatype_null_enable(context);
        }

        public override object VisitData_manipulation_language_statements([NotNull] PlSqlParser.Data_manipulation_language_statementsContext context)
        {
            Stop();
            return base.VisitData_manipulation_language_statements(context);
        }

        public override object VisitDeclare_spec([NotNull] PlSqlParser.Declare_specContext context)
        {
            Stop();
            return base.VisitDeclare_spec(context);
        }

        public override object VisitDefault_value_part([NotNull] PlSqlParser.Default_value_partContext context)
        {
            Stop();
            return base.VisitDefault_value_part(context);
        }

        public override object VisitDelete_statement([NotNull] PlSqlParser.Delete_statementContext context)
        {
            Stop();
            return base.VisitDelete_statement(context);
        }

        public override object VisitDependent_exceptions_part([NotNull] PlSqlParser.Dependent_exceptions_partContext context)
        {
            Stop();
            return base.VisitDependent_exceptions_part(context);
        }

        public override object VisitDependent_handling_clause([NotNull] PlSqlParser.Dependent_handling_clauseContext context)
        {
            Stop();
            return base.VisitDependent_handling_clause(context);
        }

        public override object VisitDisable_constraint([NotNull] PlSqlParser.Disable_constraintContext context)
        {
            Stop();
            return base.VisitDisable_constraint(context);
        }

        public override object VisitDml_event_clause([NotNull] PlSqlParser.Dml_event_clauseContext context)
        {
            Stop();
            return base.VisitDml_event_clause(context);
        }

        public override object VisitDml_event_element([NotNull] PlSqlParser.Dml_event_elementContext context)
        {
            Stop();
            return base.VisitDml_event_element(context);
        }

        public override object VisitDml_event_nested_clause([NotNull] PlSqlParser.Dml_event_nested_clauseContext context)
        {
            Stop();
            return base.VisitDml_event_nested_clause(context);
        }

        public override object VisitDml_table_expression_clause([NotNull] PlSqlParser.Dml_table_expression_clauseContext context)
        {
            Stop();
            return base.VisitDml_table_expression_clause(context);
        }

        public override object VisitDrop_constraint([NotNull] PlSqlParser.Drop_constraintContext context)
        {
            Stop();
            return base.VisitDrop_constraint(context);
        }

        public override object VisitDrop_function([NotNull] PlSqlParser.Drop_functionContext context)
        {
            Stop();
            return base.VisitDrop_function(context);
        }

        public override object VisitDrop_index([NotNull] PlSqlParser.Drop_indexContext context)
        {
            Stop();
            return base.VisitDrop_index(context);
        }

        public override object VisitDrop_package([NotNull] PlSqlParser.Drop_packageContext context)
        {
            Stop();
            return base.VisitDrop_package(context);
        }

        public override object VisitDrop_procedure([NotNull] PlSqlParser.Drop_procedureContext context)
        {
            Stop();
            return base.VisitDrop_procedure(context);
        }

        public override object VisitDrop_sequence([NotNull] PlSqlParser.Drop_sequenceContext context)
        {
            Stop();
            return base.VisitDrop_sequence(context);
        }

        public override object VisitDrop_table([NotNull] PlSqlParser.Drop_tableContext context)
        {
            Stop();
            return base.VisitDrop_table(context);
        }

        public override object VisitDrop_trigger([NotNull] PlSqlParser.Drop_triggerContext context)
        {
            Stop();
            return base.VisitDrop_trigger(context);
        }

        public override object VisitDrop_type([NotNull] PlSqlParser.Drop_typeContext context)
        {
            Stop();
            return base.VisitDrop_type(context);
        }

        public override object VisitDynamic_returning_clause([NotNull] PlSqlParser.Dynamic_returning_clauseContext context)
        {
            Stop();
            return base.VisitDynamic_returning_clause(context);
        }

        public override object VisitElement_spec([NotNull] PlSqlParser.Element_specContext context)
        {
            Stop();
            return base.VisitElement_spec(context);
        }

        public override object VisitElement_spec_options([NotNull] PlSqlParser.Element_spec_optionsContext context)
        {
            Stop();
            return base.VisitElement_spec_options(context);
        }

        public override object VisitElse_part([NotNull] PlSqlParser.Else_partContext context)
        {
            Stop();
            return base.VisitElse_part(context);
        }

        public override object VisitElsif_part([NotNull] PlSqlParser.Elsif_partContext context)
        {
            Stop();
            return base.VisitElsif_part(context);
        }

        public override object VisitEnable_constraint([NotNull] PlSqlParser.Enable_constraintContext context)
        {
            Stop();
            return base.VisitEnable_constraint(context);
        }

        public override object VisitErrorNode(IErrorNode node)
        {
            Stop();
            return base.VisitErrorNode(node);
        }

        public override object VisitError_logging_clause([NotNull] PlSqlParser.Error_logging_clauseContext context)
        {
            Stop();
            return base.VisitError_logging_clause(context);
        }

        public override object VisitError_logging_into_part([NotNull] PlSqlParser.Error_logging_into_partContext context)
        {
            Stop();
            return base.VisitError_logging_into_part(context);
        }

        public override object VisitError_logging_reject_part([NotNull] PlSqlParser.Error_logging_reject_partContext context)
        {
            Stop();
            return base.VisitError_logging_reject_part(context);
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

        public override object VisitExecute_immediate([NotNull] PlSqlParser.Execute_immediateContext context)
        {
            Stop();
            return base.VisitExecute_immediate(context);
        }

        public override object VisitExit_statement([NotNull] PlSqlParser.Exit_statementContext context)
        {
            Stop();
            return base.VisitExit_statement(context);
        }

        public override object VisitExplain_statement([NotNull] PlSqlParser.Explain_statementContext context)
        {
            Stop();
            return base.VisitExplain_statement(context);
        }

        public override object VisitExpression([NotNull] PlSqlParser.ExpressionContext context)
        {
            Stop();
            return base.VisitExpression(context);
        }

        public override object VisitExpressions([NotNull] PlSqlParser.ExpressionsContext context)
        {
            Stop();
            return base.VisitExpressions(context);
        }

        public override object VisitExtent_management_clause([NotNull] PlSqlParser.Extent_management_clauseContext context)
        {
            Stop();
            return base.VisitExtent_management_clause(context);
        }

        public override object VisitFactoring_element([NotNull] PlSqlParser.Factoring_elementContext context)
        {
            Stop();
            return base.VisitFactoring_element(context);
        }

        public override object VisitFetch_statement([NotNull] PlSqlParser.Fetch_statementContext context)
        {
            Stop();
            return base.VisitFetch_statement(context);
        }

        public override object VisitField_spec([NotNull] PlSqlParser.Field_specContext context)
        {
            Stop();
            return base.VisitField_spec(context);
        }

        public override object VisitFlashback_mode_clause([NotNull] PlSqlParser.Flashback_mode_clauseContext context)
        {
            Stop();
            return base.VisitFlashback_mode_clause(context);
        }

        public override object VisitFlashback_query_clause([NotNull] PlSqlParser.Flashback_query_clauseContext context)
        {
            Stop();
            return base.VisitFlashback_query_clause(context);
        }

        public override object VisitForall_statement([NotNull] PlSqlParser.Forall_statementContext context)
        {
            Stop();
            return base.VisitForall_statement(context);
        }

        public override object VisitForeign_key_clause([NotNull] PlSqlParser.Foreign_key_clauseContext context)
        {
            Stop();
            return base.VisitForeign_key_clause(context);
        }

        public override object VisitFor_each_row([NotNull] PlSqlParser.For_each_rowContext context)
        {
            Stop();
            return base.VisitFor_each_row(context);
        }

        public override object VisitFor_update_clause([NotNull] PlSqlParser.For_update_clauseContext context)
        {
            Stop();
            return base.VisitFor_update_clause(context);
        }

        public override object VisitFor_update_of_part([NotNull] PlSqlParser.For_update_of_partContext context)
        {
            Stop();
            return base.VisitFor_update_of_part(context);
        }

        public override object VisitFor_update_options([NotNull] PlSqlParser.For_update_optionsContext context)
        {
            Stop();
            return base.VisitFor_update_options(context);
        }

        public override object VisitFrom_clause([NotNull] PlSqlParser.From_clauseContext context)
        {
            Stop();
            return base.VisitFrom_clause(context);
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

        public override object VisitFunction_name([NotNull] PlSqlParser.Function_nameContext context)
        {
            Stop();
            return base.VisitFunction_name(context);
        }

        public override object VisitFunction_spec([NotNull] PlSqlParser.Function_specContext context)
        {
            Stop();
            return base.VisitFunction_spec(context);
        }

        public override object VisitFunc_decl_in_type([NotNull] PlSqlParser.Func_decl_in_typeContext context)
        {
            Stop();
            return base.VisitFunc_decl_in_type(context);
        }

        public override object VisitGeneral_element([NotNull] PlSqlParser.General_elementContext context)
        {
            Stop();
            return base.VisitGeneral_element(context);
        }

        public override object VisitGeneral_element_part([NotNull] PlSqlParser.General_element_partContext context)
        {
            Stop();
            return base.VisitGeneral_element_part(context);
        }

        public override object VisitGeneral_table_ref([NotNull] PlSqlParser.General_table_refContext context)
        {
            Stop();
            return base.VisitGeneral_table_ref(context);
        }

        public override object VisitGoto_statement([NotNull] PlSqlParser.Goto_statementContext context)
        {
            Stop();
            return base.VisitGoto_statement(context);
        }

        public override object VisitGrouping_sets_clause([NotNull] PlSqlParser.Grouping_sets_clauseContext context)
        {
            Stop();
            return base.VisitGrouping_sets_clause(context);
        }

        public override object VisitGrouping_sets_elements([NotNull] PlSqlParser.Grouping_sets_elementsContext context)
        {
            Stop();
            return base.VisitGrouping_sets_elements(context);
        }

        public override object VisitGroup_by_clause([NotNull] PlSqlParser.Group_by_clauseContext context)
        {
            Stop();
            return base.VisitGroup_by_clause(context);
        }

        public override object VisitGroup_by_elements([NotNull] PlSqlParser.Group_by_elementsContext context)
        {
            Stop();
            return base.VisitGroup_by_elements(context);
        }

        public override object VisitHaving_clause([NotNull] PlSqlParser.Having_clauseContext context)
        {
            Stop();
            return base.VisitHaving_clause(context);
        }

        public override object VisitHierarchical_query_clause([NotNull] PlSqlParser.Hierarchical_query_clauseContext context)
        {
            Stop();
            return base.VisitHierarchical_query_clause(context);
        }

        public override object VisitIdentified_by([NotNull] PlSqlParser.Identified_byContext context)
        {
            Stop();
            return base.VisitIdentified_by(context);
        }

        public override object VisitIdentified_other_clause([NotNull] PlSqlParser.Identified_other_clauseContext context)
        {
            Stop();
            return base.VisitIdentified_other_clause(context);
        }

        public override object VisitIdentifier([NotNull] PlSqlParser.IdentifierContext context)
        {
            var t = context.GetText();
            Stop();
            return base.VisitIdentifier(context);
        }

        public override object VisitId_expression([NotNull] PlSqlParser.Id_expressionContext context)
        {
            Stop();
            return base.VisitId_expression(context);
        }

        public override object VisitIf_statement([NotNull] PlSqlParser.If_statementContext context)
        {
            Stop();
            return base.VisitIf_statement(context);
        }

        public override object VisitImplementation_type_name([NotNull] PlSqlParser.Implementation_type_nameContext context)
        {
            Stop();
            return base.VisitImplementation_type_name(context);
        }

        public override object VisitIndex_name([NotNull] PlSqlParser.Index_nameContext context)
        {
            Stop();
            return base.VisitIndex_name(context);
        }

        public override object VisitInline_constraint([NotNull] PlSqlParser.Inline_constraintContext context)
        {
            Stop();
            return base.VisitInline_constraint(context);
        }

        public override object VisitInsert_into_clause([NotNull] PlSqlParser.Insert_into_clauseContext context)
        {
            Stop();
            return base.VisitInsert_into_clause(context);
        }

        public override object VisitInsert_statement([NotNull] PlSqlParser.Insert_statementContext context)
        {
            Stop();
            return base.VisitInsert_statement(context);
        }

        public override object VisitInterval_expression([NotNull] PlSqlParser.Interval_expressionContext context)
        {
            Stop();
            return base.VisitInterval_expression(context);
        }

        public override object VisitInto_clause([NotNull] PlSqlParser.Into_clauseContext context)
        {
            Stop();
            return base.VisitInto_clause(context);
        }

        public override object VisitInvoker_rights_clause([NotNull] PlSqlParser.Invoker_rights_clauseContext context)
        {
            Stop();
            return base.VisitInvoker_rights_clause(context);
        }

        public override object VisitIn_elements([NotNull] PlSqlParser.In_elementsContext context)
        {
            Stop();
            return base.VisitIn_elements(context);
        }

        public override object VisitJava_spec([NotNull] PlSqlParser.Java_specContext context)
        {
            Stop();
            return base.VisitJava_spec(context);
        }

        public override object VisitJoin_clause([NotNull] PlSqlParser.Join_clauseContext context)
        {
            Stop();
            return base.VisitJoin_clause(context);
        }

        public override object VisitJoin_on_part([NotNull] PlSqlParser.Join_on_partContext context)
        {
            Stop();
            return base.VisitJoin_on_part(context);
        }

        public override object VisitJoin_using_part([NotNull] PlSqlParser.Join_using_partContext context)
        {
            Stop();
            return base.VisitJoin_using_part(context);
        }

        public override object VisitKeep_clause([NotNull] PlSqlParser.Keep_clauseContext context)
        {
            Stop();
            return base.VisitKeep_clause(context);
        }

        public override object VisitLabel_declaration([NotNull] PlSqlParser.Label_declarationContext context)
        {
            Stop();
            return base.VisitLabel_declaration(context);
        }

        public override object VisitLabel_name([NotNull] PlSqlParser.Label_nameContext context)
        {
            Stop();
            return base.VisitLabel_name(context);
        }

        public override object VisitLink_name([NotNull] PlSqlParser.Link_nameContext context)
        {
            Stop();
            return base.VisitLink_name(context);
        }

        public override object VisitLock_mode([NotNull] PlSqlParser.Lock_modeContext context)
        {
            Stop();
            return base.VisitLock_mode(context);
        }

        public override object VisitLock_table_element([NotNull] PlSqlParser.Lock_table_elementContext context)
        {
            Stop();
            return base.VisitLock_table_element(context);
        }

        public override object VisitLock_table_statement([NotNull] PlSqlParser.Lock_table_statementContext context)
        {
            Stop();
            return base.VisitLock_table_statement(context);
        }

        public override object VisitLogging_clause([NotNull] PlSqlParser.Logging_clauseContext context)
        {
            Stop();
            return base.VisitLogging_clause(context);
        }

        public override object VisitLogical_expression([NotNull] PlSqlParser.Logical_expressionContext context)
        {
            Stop();
            return base.VisitLogical_expression(context);
        }

        public override object VisitLoop_statement([NotNull] PlSqlParser.Loop_statementContext context)
        {
            Stop();
            return base.VisitLoop_statement(context);
        }

        public override object VisitLower_bound([NotNull] PlSqlParser.Lower_boundContext context)
        {
            Stop();
            return base.VisitLower_bound(context);
        }

        public override object VisitMain_model([NotNull] PlSqlParser.Main_modelContext context)
        {
            Stop();
            return base.VisitMain_model(context);
        }

        public override object VisitMain_model_name([NotNull] PlSqlParser.Main_model_nameContext context)
        {
            Stop();
            return base.VisitMain_model_name(context);
        }

        public override object VisitMap_order_function_spec([NotNull] PlSqlParser.Map_order_function_specContext context)
        {
            Stop();
            return base.VisitMap_order_function_spec(context);
        }

        public override object VisitMap_order_func_declaration([NotNull] PlSqlParser.Map_order_func_declarationContext context)
        {
            Stop();
            return base.VisitMap_order_func_declaration(context);
        }

        public override object VisitMaxsize_clause([NotNull] PlSqlParser.Maxsize_clauseContext context)
        {
            Stop();
            return base.VisitMaxsize_clause(context);
        }

        public override object VisitMerge_element([NotNull] PlSqlParser.Merge_elementContext context)
        {
            Stop();
            return base.VisitMerge_element(context);
        }

        public override object VisitMerge_insert_clause([NotNull] PlSqlParser.Merge_insert_clauseContext context)
        {
            Stop();
            return base.VisitMerge_insert_clause(context);
        }

        public override object VisitMerge_statement([NotNull] PlSqlParser.Merge_statementContext context)
        {
            Stop();
            return base.VisitMerge_statement(context);
        }

        public override object VisitMerge_update_clause([NotNull] PlSqlParser.Merge_update_clauseContext context)
        {
            Stop();
            return base.VisitMerge_update_clause(context);
        }

        public override object VisitMerge_update_delete_part([NotNull] PlSqlParser.Merge_update_delete_partContext context)
        {
            Stop();
            return base.VisitMerge_update_delete_part(context);
        }

        public override object VisitModel_clause([NotNull] PlSqlParser.Model_clauseContext context)
        {
            Stop();
            return base.VisitModel_clause(context);
        }

        public override object VisitModel_column([NotNull] PlSqlParser.Model_columnContext context)
        {
            Stop();
            return base.VisitModel_column(context);
        }

        public override object VisitModel_column_clauses([NotNull] PlSqlParser.Model_column_clausesContext context)
        {
            Stop();
            return base.VisitModel_column_clauses(context);
        }

        public override object VisitModel_column_list([NotNull] PlSqlParser.Model_column_listContext context)
        {
            Stop();
            return base.VisitModel_column_list(context);
        }

        public override object VisitModel_column_partition_part([NotNull] PlSqlParser.Model_column_partition_partContext context)
        {
            Stop();
            return base.VisitModel_column_partition_part(context);
        }

        public override object VisitModel_expression([NotNull] PlSqlParser.Model_expressionContext context)
        {
            Stop();
            return base.VisitModel_expression(context);
        }

        public override object VisitModel_expression_element([NotNull] PlSqlParser.Model_expression_elementContext context)
        {
            Stop();
            return base.VisitModel_expression_element(context);
        }

        public override object VisitModel_iterate_clause([NotNull] PlSqlParser.Model_iterate_clauseContext context)
        {
            Stop();
            return base.VisitModel_iterate_clause(context);
        }

        public override object VisitModel_rules_clause([NotNull] PlSqlParser.Model_rules_clauseContext context)
        {
            Stop();
            return base.VisitModel_rules_clause(context);
        }

        public override object VisitModel_rules_element([NotNull] PlSqlParser.Model_rules_elementContext context)
        {
            Stop();
            return base.VisitModel_rules_element(context);
        }

        public override object VisitModel_rules_part([NotNull] PlSqlParser.Model_rules_partContext context)
        {
            Stop();
            return base.VisitModel_rules_part(context);
        }

        public override object VisitModifier_clause([NotNull] PlSqlParser.Modifier_clauseContext context)
        {
            Stop();
            return base.VisitModifier_clause(context);
        }

        public override object VisitMultiset_expression([NotNull] PlSqlParser.Multiset_expressionContext context)
        {
            Stop();
            return base.VisitMultiset_expression(context);
        }

        public override object VisitMulti_column_for_loop([NotNull] PlSqlParser.Multi_column_for_loopContext context)
        {
            Stop();
            return base.VisitMulti_column_for_loop(context);
        }

        public override object VisitMulti_table_element([NotNull] PlSqlParser.Multi_table_elementContext context)
        {
            Stop();
            return base.VisitMulti_table_element(context);
        }

        public override object VisitMulti_table_insert([NotNull] PlSqlParser.Multi_table_insertContext context)
        {
            Stop();
            return base.VisitMulti_table_insert(context);
        }

        public override object VisitNative_datatype_element([NotNull] PlSqlParser.Native_datatype_elementContext context)
        {
            Stop();
            return base.VisitNative_datatype_element(context);
        }

        public override object VisitNested_table_type_def([NotNull] PlSqlParser.Nested_table_type_defContext context)
        {
            Stop();
            return base.VisitNested_table_type_def(context);
        }

        public override object VisitNon_dml_event([NotNull] PlSqlParser.Non_dml_eventContext context)
        {
            Stop();
            return base.VisitNon_dml_event(context);
        }

        public override object VisitNon_dml_trigger([NotNull] PlSqlParser.Non_dml_triggerContext context)
        {
            Stop();
            return base.VisitNon_dml_trigger(context);
        }

        public override object VisitNull_statement([NotNull] PlSqlParser.Null_statementContext context)
        {
            Stop();
            return base.VisitNull_statement(context);
        }

        public override object VisitNumeric([NotNull] PlSqlParser.NumericContext context)
        {
            Stop();
            return base.VisitNumeric(context);
        }

        public override object VisitNumeric_function([NotNull] PlSqlParser.Numeric_functionContext context)
        {
            Stop();
            return base.VisitNumeric_function(context);
        }

        public override object VisitNumeric_function_name([NotNull] PlSqlParser.Numeric_function_nameContext context)
        {
            Stop();
            return base.VisitNumeric_function_name(context);
        }

        public override object VisitNumeric_function_wrapper([NotNull] PlSqlParser.Numeric_function_wrapperContext context)
        {
            Stop();
            return base.VisitNumeric_function_wrapper(context);
        }

        public override object VisitNumeric_negative([NotNull] PlSqlParser.Numeric_negativeContext context)
        {
            Stop();
            return base.VisitNumeric_negative(context);
        }

        public override object VisitObject_as_part([NotNull] PlSqlParser.Object_as_partContext context)
        {
            Stop();
            return base.VisitObject_as_part(context);
        }

        public override object VisitObject_member_spec([NotNull] PlSqlParser.Object_member_specContext context)
        {
            Stop();
            return base.VisitObject_member_spec(context);
        }

        public override object VisitObject_type_def([NotNull] PlSqlParser.Object_type_defContext context)
        {
            Stop();
            return base.VisitObject_type_def(context);
        }

        public override object VisitObject_under_part([NotNull] PlSqlParser.Object_under_partContext context)
        {
            Stop();
            return base.VisitObject_under_part(context);
        }

        public override object VisitObject_view_clause([NotNull] PlSqlParser.Object_view_clauseContext context)
        {
            Stop();
            return base.VisitObject_view_clause(context);
        }

        public override object VisitOn_delete_clause([NotNull] PlSqlParser.On_delete_clauseContext context)
        {
            Stop();
            return base.VisitOn_delete_clause(context);
        }

        public override object VisitOpen_for_statement([NotNull] PlSqlParser.Open_for_statementContext context)
        {
            Stop();
            return base.VisitOpen_for_statement(context);
        }

        public override object VisitOpen_statement([NotNull] PlSqlParser.Open_statementContext context)
        {
            Stop();
            return base.VisitOpen_statement(context);
        }

        public override object VisitOrder_by_clause([NotNull] PlSqlParser.Order_by_clauseContext context)
        {
            Stop();
            return base.VisitOrder_by_clause(context);
        }

        public override object VisitOrder_by_elements([NotNull] PlSqlParser.Order_by_elementsContext context)
        {
            Stop();
            return base.VisitOrder_by_elements(context);
        }

        public override object VisitOther_function([NotNull] PlSqlParser.Other_functionContext context)
        {
            Stop();
            return base.VisitOther_function(context);
        }

        public override object VisitOuter_join_sign([NotNull] PlSqlParser.Outer_join_signContext context)
        {
            Stop();
            return base.VisitOuter_join_sign(context);
        }

        public override object VisitOuter_join_type([NotNull] PlSqlParser.Outer_join_typeContext context)
        {
            Stop();
            return base.VisitOuter_join_type(context);
        }

        public override object VisitOut_of_line_constraint([NotNull] PlSqlParser.Out_of_line_constraintContext context)
        {
            Stop();
            return base.VisitOut_of_line_constraint(context);
        }

        public override object VisitOver_clause([NotNull] PlSqlParser.Over_clauseContext context)
        {
            Stop();
            return base.VisitOver_clause(context);
        }

        public override object VisitOver_clause_keyword([NotNull] PlSqlParser.Over_clause_keywordContext context)
        {
            Stop();
            return base.VisitOver_clause_keyword(context);
        }

        public override object VisitPackage_name([NotNull] PlSqlParser.Package_nameContext context)
        {
            Stop();
            return base.VisitPackage_name(context);
        }

        public override object VisitPackage_obj_body([NotNull] PlSqlParser.Package_obj_bodyContext context)
        {
            Stop();
            return base.VisitPackage_obj_body(context);
        }

        public override object VisitPackage_obj_spec([NotNull] PlSqlParser.Package_obj_specContext context)
        {
            Stop();
            return base.VisitPackage_obj_spec(context);
        }

        public override object VisitParallel_enable_clause([NotNull] PlSqlParser.Parallel_enable_clauseContext context)
        {
            Stop();
            return base.VisitParallel_enable_clause(context);
        }

        public override object VisitParameter([NotNull] PlSqlParser.ParameterContext context)
        {
            Stop();
            return base.VisitParameter(context);
        }

        public override object VisitParameter_name([NotNull] PlSqlParser.Parameter_nameContext context)
        {
            Stop();
            return base.VisitParameter_name(context);
        }

        public override object VisitParameter_spec([NotNull] PlSqlParser.Parameter_specContext context)
        {
            Stop();
            return base.VisitParameter_spec(context);
        }

        public override object VisitParen_column_list([NotNull] PlSqlParser.Paren_column_listContext context)
        {
            Stop();
            return base.VisitParen_column_list(context);
        }

        public override object VisitPartition_by_clause([NotNull] PlSqlParser.Partition_by_clauseContext context)
        {
            Stop();
            return base.VisitPartition_by_clause(context);
        }

        public override object VisitPartition_extension_clause([NotNull] PlSqlParser.Partition_extension_clauseContext context)
        {
            Stop();
            return base.VisitPartition_extension_clause(context);
        }

        public override object VisitPassword_expire_clause([NotNull] PlSqlParser.Password_expire_clauseContext context)
        {
            Stop();
            return base.VisitPassword_expire_clause(context);
        }

        public override object VisitPermanent_tablespace_clause([NotNull] PlSqlParser.Permanent_tablespace_clauseContext context)
        {
            Stop();
            return base.VisitPermanent_tablespace_clause(context);
        }

        public override object VisitPipe_row_statement([NotNull] PlSqlParser.Pipe_row_statementContext context)
        {
            Stop();
            return base.VisitPipe_row_statement(context);
        }

        public override object VisitPivot_clause([NotNull] PlSqlParser.Pivot_clauseContext context)
        {
            Stop();
            return base.VisitPivot_clause(context);
        }

        public override object VisitPivot_element([NotNull] PlSqlParser.Pivot_elementContext context)
        {
            Stop();
            return base.VisitPivot_element(context);
        }

        public override object VisitPivot_for_clause([NotNull] PlSqlParser.Pivot_for_clauseContext context)
        {
            Stop();
            return base.VisitPivot_for_clause(context);
        }

        public override object VisitPivot_in_clause([NotNull] PlSqlParser.Pivot_in_clauseContext context)
        {
            Stop();
            return base.VisitPivot_in_clause(context);
        }

        public override object VisitPivot_in_clause_element([NotNull] PlSqlParser.Pivot_in_clause_elementContext context)
        {
            Stop();
            return base.VisitPivot_in_clause_element(context);
        }

        public override object VisitPivot_in_clause_elements([NotNull] PlSqlParser.Pivot_in_clause_elementsContext context)
        {
            Stop();
            return base.VisitPivot_in_clause_elements(context);
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

        public override object VisitPrecision_part([NotNull] PlSqlParser.Precision_partContext context)
        {
            Stop();
            return base.VisitPrecision_part(context);
        }

        public override object VisitPrimary_key_clause([NotNull] PlSqlParser.Primary_key_clauseContext context)
        {
            Stop();
            return base.VisitPrimary_key_clause(context);
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

        public override object VisitProfile_clause([NotNull] PlSqlParser.Profile_clauseContext context)
        {
            Stop();
            return base.VisitProfile_clause(context);
        }

        public override object VisitProxy_clause([NotNull] PlSqlParser.Proxy_clauseContext context)
        {
            Stop();
            return base.VisitProxy_clause(context);
        }

        public override object VisitQuantified_expression([NotNull] PlSqlParser.Quantified_expressionContext context)
        {
            Stop();
            return base.VisitQuantified_expression(context);
        }

        public override object VisitQuery_block([NotNull] PlSqlParser.Query_blockContext context)
        {
            Stop();
            return base.VisitQuery_block(context);
        }

        public override object VisitQuery_name([NotNull] PlSqlParser.Query_nameContext context)
        {
            Stop();
            return base.VisitQuery_name(context);
        }

        public override object VisitQuery_partition_clause([NotNull] PlSqlParser.Query_partition_clauseContext context)
        {
            Stop();
            return base.VisitQuery_partition_clause(context);
        }

        public override object VisitQuota_clause([NotNull] PlSqlParser.Quota_clauseContext context)
        {
            Stop();
            return base.VisitQuota_clause(context);
        }

        public override object VisitQuoted_string([NotNull] PlSqlParser.Quoted_stringContext context)
        {
            Stop();
            return base.VisitQuoted_string(context);
        }

        public override object VisitRaise_statement([NotNull] PlSqlParser.Raise_statementContext context)
        {
            Stop();
            return base.VisitRaise_statement(context);
        }

        public override object VisitRecord_name([NotNull] PlSqlParser.Record_nameContext context)
        {
            Stop();
            return base.VisitRecord_name(context);
        }

        public override object VisitRecord_type_def([NotNull] PlSqlParser.Record_type_defContext context)
        {
            Stop();
            return base.VisitRecord_type_def(context);
        }

        public override object VisitRedo_log_file_spec([NotNull] PlSqlParser.Redo_log_file_specContext context)
        {
            Stop();
            return base.VisitRedo_log_file_spec(context);
        }

        public override object VisitReferences_clause([NotNull] PlSqlParser.References_clauseContext context)
        {
            Stop();
            return base.VisitReferences_clause(context);
        }

        public override object VisitReference_model([NotNull] PlSqlParser.Reference_modelContext context)
        {
            Stop();
            return base.VisitReference_model(context);
        }

        public override object VisitReference_model_name([NotNull] PlSqlParser.Reference_model_nameContext context)
        {
            Stop();
            return base.VisitReference_model_name(context);
        }

        public override object VisitReferencing_clause([NotNull] PlSqlParser.Referencing_clauseContext context)
        {
            Stop();
            return base.VisitReferencing_clause(context);
        }

        public override object VisitReferencing_element([NotNull] PlSqlParser.Referencing_elementContext context)
        {
            Stop();
            return base.VisitReferencing_element(context);
        }

        public override object VisitRef_cursor_type_def([NotNull] PlSqlParser.Ref_cursor_type_defContext context)
        {
            Stop();
            return base.VisitRef_cursor_type_def(context);
        }

        public override object VisitRegular_id([NotNull] PlSqlParser.Regular_idContext context)
        {
            Stop();
            return base.VisitRegular_id(context);
        }

        public override object VisitRelational_expression([NotNull] PlSqlParser.Relational_expressionContext context)
        {
            Stop();
            return base.VisitRelational_expression(context);
        }

        public override object VisitRelational_operator([NotNull] PlSqlParser.Relational_operatorContext context)
        {
            Stop();
            return base.VisitRelational_operator(context);
        }

        public override object VisitRelies_on_part([NotNull] PlSqlParser.Relies_on_partContext context)
        {
            Stop();
            return base.VisitRelies_on_part(context);
        }

        public override object VisitReplace_type_clause([NotNull] PlSqlParser.Replace_type_clauseContext context)
        {
            Stop();
            return base.VisitReplace_type_clause(context);
        }

        public override object VisitRespect_or_ignore_nulls([NotNull] PlSqlParser.Respect_or_ignore_nullsContext context)
        {
            Stop();
            return base.VisitRespect_or_ignore_nulls(context);
        }

        public override object VisitResult_cache_clause([NotNull] PlSqlParser.Result_cache_clauseContext context)
        {
            Stop();
            return base.VisitResult_cache_clause(context);
        }

        public override object VisitReturn_rows_clause([NotNull] PlSqlParser.Return_rows_clauseContext context)
        {
            Stop();
            return base.VisitReturn_rows_clause(context);
        }

        public override object VisitReturn_statement([NotNull] PlSqlParser.Return_statementContext context)
        {
            Stop();
            return base.VisitReturn_statement(context);
        }

        public override object VisitRole_clause([NotNull] PlSqlParser.Role_clauseContext context)
        {
            Stop();
            return base.VisitRole_clause(context);
        }

        public override object VisitRole_name([NotNull] PlSqlParser.Role_nameContext context)
        {
            Stop();
            return base.VisitRole_name(context);
        }

        public override object VisitRollback_segment_name([NotNull] PlSqlParser.Rollback_segment_nameContext context)
        {
            Stop();
            return base.VisitRollback_segment_name(context);
        }

        public override object VisitRollback_statement([NotNull] PlSqlParser.Rollback_statementContext context)
        {
            Stop();
            return base.VisitRollback_statement(context);
        }

        public override object VisitRollup_cube_clause([NotNull] PlSqlParser.Rollup_cube_clauseContext context)
        {
            Stop();
            return base.VisitRollup_cube_clause(context);
        }

        public override object VisitRoutine_clause([NotNull] PlSqlParser.Routine_clauseContext context)
        {
            Stop();
            return base.VisitRoutine_clause(context);
        }

        public override object VisitRoutine_name([NotNull] PlSqlParser.Routine_nameContext context)
        {
            Stop();
            return base.VisitRoutine_name(context);
        }

        public override object VisitSample_clause([NotNull] PlSqlParser.Sample_clauseContext context)
        {
            Stop();
            return base.VisitSample_clause(context);
        }

        public override object VisitSavepoint_name([NotNull] PlSqlParser.Savepoint_nameContext context)
        {
            Stop();
            return base.VisitSavepoint_name(context);
        }

        public override object VisitSavepoint_statement([NotNull] PlSqlParser.Savepoint_statementContext context)
        {
            Stop();
            return base.VisitSavepoint_statement(context);
        }

        public override object VisitSchema_name([NotNull] PlSqlParser.Schema_nameContext context)
        {
            Stop();
            return base.VisitSchema_name(context);
        }

        public override object VisitSearched_case_statement([NotNull] PlSqlParser.Searched_case_statementContext context)
        {
            Stop();
            return base.VisitSearched_case_statement(context);
        }

        public override object VisitSearched_case_when_part([NotNull] PlSqlParser.Searched_case_when_partContext context)
        {
            Stop();
            return base.VisitSearched_case_when_part(context);
        }

        public override object VisitSearch_clause([NotNull] PlSqlParser.Search_clauseContext context)
        {
            Stop();
            return base.VisitSearch_clause(context);
        }

        public override object VisitSeed_part([NotNull] PlSqlParser.Seed_partContext context)
        {
            Stop();
            return base.VisitSeed_part(context);
        }

        public override object VisitSegment_management_clause([NotNull] PlSqlParser.Segment_management_clauseContext context)
        {
            Stop();
            return base.VisitSegment_management_clause(context);
        }

        public override object VisitSelected_element([NotNull] PlSqlParser.Selected_elementContext context)
        {
            Stop();
            return base.VisitSelected_element(context);
        }

        public override object VisitSelected_tableview([NotNull] PlSqlParser.Selected_tableviewContext context)
        {
            Stop();
            return base.VisitSelected_tableview(context);
        }

        public override object VisitSelect_list_elements([NotNull] PlSqlParser.Select_list_elementsContext context)
        {
            Stop();
            return base.VisitSelect_list_elements(context);
        }

        public override object VisitSelect_statement([NotNull] PlSqlParser.Select_statementContext context)
        {
            Stop();
            return base.VisitSelect_statement(context);
        }

        public override object VisitSequence_name([NotNull] PlSqlParser.Sequence_nameContext context)
        {
            Stop();
            return base.VisitSequence_name(context);
        }

        public override object VisitSequence_spec([NotNull] PlSqlParser.Sequence_specContext context)
        {
            Stop();
            return base.VisitSequence_spec(context);
        }

        public override object VisitSequence_start_clause([NotNull] PlSqlParser.Sequence_start_clauseContext context)
        {
            Stop();
            return base.VisitSequence_start_clause(context);
        }

        public override object VisitSeq_of_declare_specs([NotNull] PlSqlParser.Seq_of_declare_specsContext context)
        {
            Stop();
            return base.VisitSeq_of_declare_specs(context);
        }

        public override object VisitSeq_of_statements([NotNull] PlSqlParser.Seq_of_statementsContext context)
        {
            Stop();
            return base.VisitSeq_of_statements(context);
        }

        public override object VisitSet_command([NotNull] PlSqlParser.Set_commandContext context)
        {
            Stop();
            return base.VisitSet_command(context);
        }

        public override object VisitSet_constraint_command([NotNull] PlSqlParser.Set_constraint_commandContext context)
        {
            Stop();
            return base.VisitSet_constraint_command(context);
        }

        public override object VisitSet_container_data([NotNull] PlSqlParser.Set_container_dataContext context)
        {
            Stop();
            return base.VisitSet_container_data(context);
        }

        public override object VisitSet_transaction_command([NotNull] PlSqlParser.Set_transaction_commandContext context)
        {
            Stop();
            return base.VisitSet_transaction_command(context);
        }

        public override object VisitSimple_case_statement([NotNull] PlSqlParser.Simple_case_statementContext context)
        {
            Stop();
            return base.VisitSimple_case_statement(context);
        }

        public override object VisitSimple_case_when_part([NotNull] PlSqlParser.Simple_case_when_partContext context)
        {
            Stop();
            return base.VisitSimple_case_when_part(context);
        }

        public override object VisitSimple_dml_trigger([NotNull] PlSqlParser.Simple_dml_triggerContext context)
        {
            Stop();
            return base.VisitSimple_dml_trigger(context);
        }

        public override object VisitSingle_column_for_loop([NotNull] PlSqlParser.Single_column_for_loopContext context)
        {
            Stop();
            return base.VisitSingle_column_for_loop(context);
        }

        public override object VisitSingle_table_insert([NotNull] PlSqlParser.Single_table_insertContext context)
        {
            Stop();
            return base.VisitSingle_table_insert(context);
        }

        public override object VisitSize_clause([NotNull] PlSqlParser.Size_clauseContext context)
        {
            Stop();
            return base.VisitSize_clause(context);
        }

        public override object VisitSqlj_object_type([NotNull] PlSqlParser.Sqlj_object_typeContext context)
        {
            Stop();
            return base.VisitSqlj_object_type(context);
        }

        public override object VisitSqlj_object_type_attr([NotNull] PlSqlParser.Sqlj_object_type_attrContext context)
        {
            Stop();
            return base.VisitSqlj_object_type_attr(context);
        }

        public override object VisitSql_plus_command([NotNull] PlSqlParser.Sql_plus_commandContext context)
        {
            Stop();
            return base.VisitSql_plus_command(context);
        }

        public override object VisitSql_statement([NotNull] PlSqlParser.Sql_statementContext context)
        {
            Stop();
            return base.VisitSql_statement(context);
        }

        public override object VisitStandard_function([NotNull] PlSqlParser.Standard_functionContext context)
        {
            Stop();
            return base.VisitStandard_function(context);
        }

        public override object VisitStandard_prediction_function_keyword([NotNull] PlSqlParser.Standard_prediction_function_keywordContext context)
        {
            Stop();
            return base.VisitStandard_prediction_function_keyword(context);
        }

        public override object VisitStart_part([NotNull] PlSqlParser.Start_partContext context)
        {
            Stop();
            return base.VisitStart_part(context);
        }

        public override object VisitStatement([NotNull] PlSqlParser.StatementContext context)
        {
            Stop();
            return base.VisitStatement(context);
        }

        public override object VisitStatic_returning_clause([NotNull] PlSqlParser.Static_returning_clauseContext context)
        {
            Stop();
            return base.VisitStatic_returning_clause(context);
        }

        public override object VisitStreaming_clause([NotNull] PlSqlParser.Streaming_clauseContext context)
        {
            Stop();
            return base.VisitStreaming_clause(context);
        }

        public override object VisitString_function([NotNull] PlSqlParser.String_functionContext context)
        {
            Stop();
            return base.VisitString_function(context);
        }

        public override object VisitString_function_name([NotNull] PlSqlParser.String_function_nameContext context)
        {
            Stop();
            return base.VisitString_function_name(context);
        }

        public override object VisitSubprogram_spec([NotNull] PlSqlParser.Subprogram_specContext context)
        {
            Stop();
            return base.VisitSubprogram_spec(context);
        }

        public override object VisitSubprog_decl_in_type([NotNull] PlSqlParser.Subprog_decl_in_typeContext context)
        {
            Stop();
            return base.VisitSubprog_decl_in_type(context);
        }

        public override object VisitSubquery([NotNull] PlSqlParser.SubqueryContext context)
        {
            Stop();
            return base.VisitSubquery(context);
        }

        public override object VisitSubquery_basic_elements([NotNull] PlSqlParser.Subquery_basic_elementsContext context)
        {
            Stop();
            return base.VisitSubquery_basic_elements(context);
        }

        public override object VisitSubquery_factoring_clause([NotNull] PlSqlParser.Subquery_factoring_clauseContext context)
        {
            Stop();
            return base.VisitSubquery_factoring_clause(context);
        }

        public override object VisitSubquery_operation_part([NotNull] PlSqlParser.Subquery_operation_partContext context)
        {
            Stop();
            return base.VisitSubquery_operation_part(context);
        }

        public override object VisitSubquery_restriction_clause([NotNull] PlSqlParser.Subquery_restriction_clauseContext context)
        {
            Stop();
            return base.VisitSubquery_restriction_clause(context);
        }

        public override object VisitSubtype_declaration([NotNull] PlSqlParser.Subtype_declarationContext context)
        {
            Stop();
            return base.VisitSubtype_declaration(context);
        }

        public override object VisitSwallow_to_semi([NotNull] PlSqlParser.Swallow_to_semiContext context)
        {
            Stop();
            return base.VisitSwallow_to_semi(context);
        }

        public override object VisitSynonym_name([NotNull] PlSqlParser.Synonym_nameContext context)
        {
            Stop();
            return base.VisitSynonym_name(context);
        }

        public override object VisitTablespace_encryption_spec([NotNull] PlSqlParser.Tablespace_encryption_specContext context)
        {
            Stop();
            return base.VisitTablespace_encryption_spec(context);
        }

        public override object VisitTablespace_group_clause([NotNull] PlSqlParser.Tablespace_group_clauseContext context)
        {
            Stop();
            return base.VisitTablespace_group_clause(context);
        }

        public override object VisitTablespace_retention_clause([NotNull] PlSqlParser.Tablespace_retention_clauseContext context)
        {
            Stop();
            return base.VisitTablespace_retention_clause(context);
        }

        public override object VisitTable_alias([NotNull] PlSqlParser.Table_aliasContext context)
        {
            Stop();
            return base.VisitTable_alias(context);
        }

        public override object VisitTable_collection_expression([NotNull] PlSqlParser.Table_collection_expressionContext context)
        {
            Stop();
            return base.VisitTable_collection_expression(context);
        }

        public override object VisitTable_element([NotNull] PlSqlParser.Table_elementContext context)
        {
            Stop();
            return base.VisitTable_element(context);
        }

        public override object VisitTable_indexed_by_part([NotNull] PlSqlParser.Table_indexed_by_partContext context)
        {
            Stop();
            return base.VisitTable_indexed_by_part(context);
        }

        public override object VisitTable_range_partition_by_clause([NotNull] PlSqlParser.Table_range_partition_by_clauseContext context)
        {
            Stop();
            return base.VisitTable_range_partition_by_clause(context);
        }

        public override object VisitTable_ref([NotNull] PlSqlParser.Table_refContext context)
        {
            Stop();
            return base.VisitTable_ref(context);
        }

        public override object VisitTable_ref_aux([NotNull] PlSqlParser.Table_ref_auxContext context)
        {
            Stop();
            return base.VisitTable_ref_aux(context);
        }

        public override object VisitTable_ref_aux_internal_one([NotNull] PlSqlParser.Table_ref_aux_internal_oneContext context)
        {
            Stop();
            return base.VisitTable_ref_aux_internal_one(context);
        }

        public override object VisitTable_ref_aux_internal_three([NotNull] PlSqlParser.Table_ref_aux_internal_threeContext context)
        {
            Stop();
            return base.VisitTable_ref_aux_internal_three(context);
        }

        public override object VisitTable_ref_aux_internal_two([NotNull] PlSqlParser.Table_ref_aux_internal_twoContext context)
        {
            Stop();
            return base.VisitTable_ref_aux_internal_two(context);
        }

        public override object VisitTable_ref_list([NotNull] PlSqlParser.Table_ref_listContext context)
        {
            Stop();
            return base.VisitTable_ref_list(context);
        }

        public override object VisitTable_type_def([NotNull] PlSqlParser.Table_type_defContext context)
        {
            Stop();
            return base.VisitTable_type_def(context);
        }

        public override object VisitTable_var_name([NotNull] PlSqlParser.Table_var_nameContext context)
        {
            Stop();
            return base.VisitTable_var_name(context);
        }

        public override object VisitTempfile_specification([NotNull] PlSqlParser.Tempfile_specificationContext context)
        {
            Stop();
            return base.VisitTempfile_specification(context);
        }

        public override object VisitTemporary_tablespace_clause([NotNull] PlSqlParser.Temporary_tablespace_clauseContext context)
        {
            Stop();
            return base.VisitTemporary_tablespace_clause(context);
        }

        public override object VisitTiming_point_section([NotNull] PlSqlParser.Timing_point_sectionContext context)
        {
            Stop();
            return base.VisitTiming_point_section(context);
        }

        public override object VisitTransaction_control_statements([NotNull] PlSqlParser.Transaction_control_statementsContext context)
        {
            Stop();
            return base.VisitTransaction_control_statements(context);
        }

        public override object VisitTrigger_block([NotNull] PlSqlParser.Trigger_blockContext context)
        {
            Stop();
            return base.VisitTrigger_block(context);
        }

        public override object VisitTrigger_body([NotNull] PlSqlParser.Trigger_bodyContext context)
        {
            Stop();
            return base.VisitTrigger_body(context);
        }

        public override object VisitTrigger_follows_clause([NotNull] PlSqlParser.Trigger_follows_clauseContext context)
        {
            Stop();
            return base.VisitTrigger_follows_clause(context);
        }

        public override object VisitTrigger_name([NotNull] PlSqlParser.Trigger_nameContext context)
        {
            Stop();
            return base.VisitTrigger_name(context);
        }

        public override object VisitTrigger_when_clause([NotNull] PlSqlParser.Trigger_when_clauseContext context)
        {
            Stop();
            return base.VisitTrigger_when_clause(context);
        }

        public override object VisitType_body([NotNull] PlSqlParser.Type_bodyContext context)
        {
            Stop();
            return base.VisitType_body(context);
        }

        public override object VisitType_body_elements([NotNull] PlSqlParser.Type_body_elementsContext context)
        {
            Stop();
            return base.VisitType_body_elements(context);
        }

        public override object VisitType_declaration([NotNull] PlSqlParser.Type_declarationContext context)
        {
            Stop();
            return base.VisitType_declaration(context);
        }

        public override object VisitType_definition([NotNull] PlSqlParser.Type_definitionContext context)
        {
            Stop();
            return base.VisitType_definition(context);
        }

        public override object VisitType_elements_parameter([NotNull] PlSqlParser.Type_elements_parameterContext context)
        {
            Stop();
            return base.VisitType_elements_parameter(context);
        }

        public override object VisitType_function_spec([NotNull] PlSqlParser.Type_function_specContext context)
        {
            Stop();
            return base.VisitType_function_spec(context);
        }

        public override object VisitType_name([NotNull] PlSqlParser.Type_nameContext context)
        {
            Stop();
            return base.VisitType_name(context);
        }

        public override object VisitType_procedure_spec([NotNull] PlSqlParser.Type_procedure_specContext context)
        {
            Stop();
            return base.VisitType_procedure_spec(context);
        }

        public override object VisitType_spec([NotNull] PlSqlParser.Type_specContext context)
        {
            Stop();
            return base.VisitType_spec(context);
        }

        public override object VisitUnary_expression([NotNull] PlSqlParser.Unary_expressionContext context)
        {
            Stop();
            return base.VisitUnary_expression(context);
        }

        public override object VisitUndo_tablespace_clause([NotNull] PlSqlParser.Undo_tablespace_clauseContext context)
        {
            Stop();
            return base.VisitUndo_tablespace_clause(context);
        }

        public override object VisitUnique_key_clause([NotNull] PlSqlParser.Unique_key_clauseContext context)
        {
            Stop();
            return base.VisitUnique_key_clause(context);
        }

        public override object VisitUnpivot_clause([NotNull] PlSqlParser.Unpivot_clauseContext context)
        {
            Stop();
            return base.VisitUnpivot_clause(context);
        }

        public override object VisitUnpivot_in_clause([NotNull] PlSqlParser.Unpivot_in_clauseContext context)
        {
            Stop();
            return base.VisitUnpivot_in_clause(context);
        }

        public override object VisitUnpivot_in_elements([NotNull] PlSqlParser.Unpivot_in_elementsContext context)
        {
            Stop();
            return base.VisitUnpivot_in_elements(context);
        }

        public override object VisitUntil_part([NotNull] PlSqlParser.Until_partContext context)
        {
            Stop();
            return base.VisitUntil_part(context);
        }

        public override object VisitUpdate_set_clause([NotNull] PlSqlParser.Update_set_clauseContext context)
        {
            Stop();
            return base.VisitUpdate_set_clause(context);
        }

        public override object VisitUpdate_statement([NotNull] PlSqlParser.Update_statementContext context)
        {
            Stop();
            return base.VisitUpdate_statement(context);
        }

        public override object VisitUpper_bound([NotNull] PlSqlParser.Upper_boundContext context)
        {
            Stop();
            return base.VisitUpper_bound(context);
        }

        public override object VisitUser_default_role_clause([NotNull] PlSqlParser.User_default_role_clauseContext context)
        {
            Stop();
            return base.VisitUser_default_role_clause(context);
        }

        public override object VisitUser_editions_clause([NotNull] PlSqlParser.User_editions_clauseContext context)
        {
            Stop();
            return base.VisitUser_editions_clause(context);
        }

        public override object VisitUser_lock_clause([NotNull] PlSqlParser.User_lock_clauseContext context)
        {
            Stop();
            return base.VisitUser_lock_clause(context);
        }

        public override object VisitUser_tablespace_clause([NotNull] PlSqlParser.User_tablespace_clauseContext context)
        {
            Stop();
            return base.VisitUser_tablespace_clause(context);
        }

        public override object VisitUsing_clause([NotNull] PlSqlParser.Using_clauseContext context)
        {
            Stop();
            return base.VisitUsing_clause(context);
        }

        public override object VisitUsing_element([NotNull] PlSqlParser.Using_elementContext context)
        {
            Stop();
            return base.VisitUsing_element(context);
        }

        public override object VisitValues_clause([NotNull] PlSqlParser.Values_clauseContext context)
        {
            Stop();
            return base.VisitValues_clause(context);
        }

        public override object VisitVariable_declaration([NotNull] PlSqlParser.Variable_declarationContext context)
        {
            Stop();
            return base.VisitVariable_declaration(context);
        }

        public override object VisitVariable_name([NotNull] PlSqlParser.Variable_nameContext context)
        {
            Stop();
            return base.VisitVariable_name(context);
        }

        public override object VisitVarray_type_def([NotNull] PlSqlParser.Varray_type_defContext context)
        {
            Stop();
            return base.VisitVarray_type_def(context);
        }

        public override object VisitView_alias_constraint([NotNull] PlSqlParser.View_alias_constraintContext context)
        {
            Stop();
            return base.VisitView_alias_constraint(context);
        }

        public override object VisitView_options([NotNull] PlSqlParser.View_optionsContext context)
        {
            Stop();
            return base.VisitView_options(context);
        }

        public override object VisitWait_nowait_part([NotNull] PlSqlParser.Wait_nowait_partContext context)
        {
            Stop();
            return base.VisitWait_nowait_part(context);
        }

        public override object VisitWhenever_command([NotNull] PlSqlParser.Whenever_commandContext context)
        {
            Stop();
            return base.VisitWhenever_command(context);
        }

        public override object VisitWhere_clause([NotNull] PlSqlParser.Where_clauseContext context)
        {
            Stop();
            return base.VisitWhere_clause(context);
        }

        public override object VisitWindowing_clause([NotNull] PlSqlParser.Windowing_clauseContext context)
        {
            Stop();
            return base.VisitWindowing_clause(context);
        }

        public override object VisitWindowing_elements([NotNull] PlSqlParser.Windowing_elementsContext context)
        {
            Stop();
            return base.VisitWindowing_elements(context);
        }

        public override object VisitWindowing_type([NotNull] PlSqlParser.Windowing_typeContext context)
        {
            Stop();
            return base.VisitWindowing_type(context);
        }

        public override object VisitWithin_or_over_clause_keyword([NotNull] PlSqlParser.Within_or_over_clause_keywordContext context)
        {
            Stop();
            return base.VisitWithin_or_over_clause_keyword(context);
        }

        public override object VisitWithin_or_over_part([NotNull] PlSqlParser.Within_or_over_partContext context)
        {
            Stop();
            return base.VisitWithin_or_over_part(context);
        }

        public override object VisitWrite_clause([NotNull] PlSqlParser.Write_clauseContext context)
        {
            Stop();
            return base.VisitWrite_clause(context);
        }

        public override object VisitXmlroot_param_standalone_part([NotNull] PlSqlParser.Xmlroot_param_standalone_partContext context)
        {
            Stop();
            return base.VisitXmlroot_param_standalone_part(context);
        }

        public override object VisitXmlroot_param_version_part([NotNull] PlSqlParser.Xmlroot_param_version_partContext context)
        {
            Stop();
            return base.VisitXmlroot_param_version_part(context);
        }

        public override object VisitXmlserialize_param_enconding_part([NotNull] PlSqlParser.Xmlserialize_param_enconding_partContext context)
        {
            Stop();
            return base.VisitXmlserialize_param_enconding_part(context);
        }

        public override object VisitXmlserialize_param_ident_part([NotNull] PlSqlParser.Xmlserialize_param_ident_partContext context)
        {
            Stop();
            return base.VisitXmlserialize_param_ident_part(context);
        }

        public override object VisitXmlserialize_param_version_part([NotNull] PlSqlParser.Xmlserialize_param_version_partContext context)
        {
            Stop();
            return base.VisitXmlserialize_param_version_part(context);
        }

        public override object VisitXml_attributes_clause([NotNull] PlSqlParser.Xml_attributes_clauseContext context)
        {
            Stop();
            return base.VisitXml_attributes_clause(context);
        }

        public override object VisitXml_column_name([NotNull] PlSqlParser.Xml_column_nameContext context)
        {
            Stop();
            return base.VisitXml_column_name(context);
        }

        public override object VisitXml_general_default_part([NotNull] PlSqlParser.Xml_general_default_partContext context)
        {
            Stop();
            return base.VisitXml_general_default_part(context);
        }

        public override object VisitXml_multiuse_expression_element([NotNull] PlSqlParser.Xml_multiuse_expression_elementContext context)
        {
            Stop();
            return base.VisitXml_multiuse_expression_element(context);
        }

        public override object VisitXml_namespaces_clause([NotNull] PlSqlParser.Xml_namespaces_clauseContext context)
        {
            Stop();
            return base.VisitXml_namespaces_clause(context);
        }

        public override object VisitXml_passing_clause([NotNull] PlSqlParser.Xml_passing_clauseContext context)
        {
            Stop();
            return base.VisitXml_passing_clause(context);
        }

        public override object VisitXml_table_column([NotNull] PlSqlParser.Xml_table_columnContext context)
        {
            Stop();
            return base.VisitXml_table_column(context);
        }



        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }

        public OracleDatabase db { get; }

        public string File { get; set; }
    }
    
}


//public override object VisitTerminal(ITerminalNode node)
//{
//    var t = node.GetText();
//    Stop();
//    return base.VisitTerminal(node);
//}

//public override object VisitChildren(IRuleNode node)
//{
//    Stop();
//    return base.VisitChildren(node);
//}
