namespace Bb.Oracle.Models
{

    public interface IOracleModelVisitor
    {
        void VisitGrant(GrantModel item);
        void VisitPackage(PackageModel item);
        void VisitPartition(PartitionModel item);
        void VisitProcedure(ProcedureModel item);
        void VisitSequence(SequenceModel item);
        void VisitSynonym(SynonymModel item);
        void VisitTable(TableModel item);
        void VisitTablespace(TablespaceModel item);
        void VisitType(TypeItem item);
        void VisitArgument(ArgumentModel item);
        void VisitSchema(SchemaModel item);
        void VisitColumn(ColumnModel item);
        void VisitConstraint(ConstraintModel item);
        void VisitIndex(IndexModel item);
        void VisitPartitionRef(PartitionRefModel item);
        void VisitTrigger(TriggerModel item);
        void VisitIndexColumn(IndexColumnModel item);
        void VisitConstraintColumn(ConstraintColumnModel item);
    }

}