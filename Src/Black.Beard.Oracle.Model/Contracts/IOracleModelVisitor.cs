using Bb.Oracle.Models;
using Bb.Oracle.Models.Codes;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Contracts
{

    public interface IOracleModelVisitor
    {
        void VisitGrant(GrantModel item);
        void VisitPackage(PackageModel item);
        void VisitPartition(PartitionModel item);
        void VisitProcedure(ProcedureModel item);
        void VisitStringConstant(OStringConstant oStringConstant);
        void VisitIntegerConstant(OIntegerConstant oIntegerConstant);
        void VisitUnaryExpression(OUnaryExpression oUnaryExpression);
        void VisitTableTypeDefinition(TableTypeDefinition tableTypeDefinition);
        void VisitTableTypeDef(OTableTypeDef oTableTypeDef);
        void VisitTypeReference(OTypeReference oTypeReference);
        void VisitODecimalConstant(ODecimalConstant oDecimalConstant);
        void VisitTableIndexedByPartExpression(OTableIndexedByPartExpression oTableIndexedByPartExpression);
        void VisitForeignKeyConstraint(ForeignKeyConstraintModel foreignKeyConstraintModel);
        void VisitTierceExpression(OTierceExpression oTierceExpression);
        void VisitArrayTypeDef(OArrayTypeDef oArrayTypeDef);
        void VisitMethodArgument(OMethodArgument oMethodArgument);
        void VisitKeyWordConstant(OKeyWordConstant oKeyWordConstant);
        void VisitTableComment(OTableCommentStatement oTableCommentStatement);
        void VisitBoolConstant(OBoolConstant oBoolConstant);
        void VisitSequence(SequenceModel item);
        void VisitRecordTypeDef(ORecordTypeDef oRecordTypeDef);
        void VisitFieldSpecExpression(OFieldSpecExpression oFieldSpecExpression);
        void VisitCodeVariableReferenceExpression(OCodeVariableReferenceExpression oCodeVariableReferenceExpression);
        void VisitCursorDeclarationStatement(OCursorDeclarationStatement oCursorDeclarationStatement);
        void VisitSynonym(SynonymModel item);
        void VisitTable(TableModel item);
        void VisitPhysicalAttributes(PhysicalAttributesModel physicalAttributesModel);
        void VisitCodeVariableDeclarationStatement(OCodeVariableDeclarationStatement oCodeVariableDeclarationStatement);
        void VisitVarrayTypeDefinition(OVarrayTypeDefinition oVarrayTypeDefinition);
        void VisitTableColumnComment(OTableColumnCommentStatement oTableColumnCommentStatement);
        void VisitCallMethodReference(OCallMethodReference oCallMethodReference);
        void VisitBinaryExpression(OBinaryExpression oBinaryExpression);
        void VisitTablespace(TablespaceModel item);
        void VisitType(TypeItem item);
        void VisitArgument(ArgumentModel item);
        void VisitRefCursorTypeDef(ORefCursorTypeDef oRefCursorTypeDef);
        void VisitProperty(PropertyModel item);
        void VisitSubPartition(SubPartitionModel item);
        void VisitPrivilege(PrivilegeModel item);
        void VisitSchema(SchemaModel item);
        void VisitBlocPartition(BlocPartitionModel blocPartitionModel);
        void VisitColumn(ColumnModel item);
        void VisitPartitionColumn(PartitionColumnModel item);
        void VisitConstraint(ConstraintModel item);
        void VisitIndex(IndexModel item);
        void VisitPartitionRef(PartitionRefModel item);
        void VisitTrigger(TriggerModel item);
        void VisitIndexColumn(IndexColumnModel item);
        void VisitConstraintColumn(ConstraintColumnModel item);
        void VisitOracleType(OracleType oracleType);
    }

}