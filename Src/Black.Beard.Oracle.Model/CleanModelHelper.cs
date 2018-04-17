using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Codes;
using Bb.Oracle.Structures.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Oracle
{

    public class CleanModelHelper : IOracleModelVisitor
    {
        private readonly ModelParser parser;
        private readonly string path;
        private readonly OracleDatabase target;

        private CleanModelHelper(string path, OracleDatabase target, ModelParser parser)
        {
            this.path = path.ToUpper();
            this.target = target;
            this.parser = parser;
        }

        public static void Clean(string path, OracleDatabase target)
        {
            ModelParser parser = new ModelParser();
            CleanModelHelper c = new CleanModelHelper(path, target, parser);
            c.Visit();
        }

        public void VisitArgument(ArgumentModel item)
        {

        }

        public void VisitColumn(ColumnModel item)
        {
            // Action dans le parser ???
            if (InFile(item))
            {

            }
        }

        public void VisitConstraint(ConstraintModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitConstraintColumn(ConstraintColumnModel item)
        {

        }

        public void VisitGrant(GrantModel item)
        {
            if (InFile(item))
                this._sequenceToDelete.Add(item);
        }

        public void VisitIndex(IndexModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitIndexColumn(IndexColumnModel item)
        {

        }

        public void VisitPackage(PackageModel item)
        {
            if (InFile(item))
                this._sequenceToDelete.Add(item);
        }

        public void VisitPartition(PartitionModel item)
        {
            if (InFile(item))
            {

            }

        }

        public void VisitPartitionRef(PartitionRefModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitProcedure(ProcedureModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitSchema(SchemaModel item)
        {

        }

        public void VisitSequence(SequenceModel item)
        {
            if (InFile(item))
                _sequenceToDelete.Add(item);
        }

        public void VisitSynonym(SynonymModel item)
        {
            if (InFile(item))
                _sequenceToDelete.Add(item);
        }

        public void VisitTable(TableModel item)
        {
            if (InFile(item))
            {

                item.Columns.Clear();

            }
        }

        public void VisitTablespace(TablespaceModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitTrigger(TriggerModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitType(TypeItem item)
        {
            if (InFile(item))
                _sequenceToDelete.Add(item);
        }

        private bool InFile(ItemBase item)
        {
            return item.Files.OfType<FileElement>().Where(c => c.Path.ToUpper() == this.path).Any();
        }

        private void Visit()
        {

            this.parser.Visit(this.target, this, a => { });


            foreach (var item in _sequenceToDelete)
            {

                if (item is SequenceModel s)
                    target.Sequences.Remove(s);

                else if (item is PackageModel p)
                    target.Packages.Remove(p);

                else if (item is SynonymModel sy)
                    target.Synonyms.Remove(sy);

                else if (item is TypeItem t)
                    target.Types.Remove(t);

                else if (item is GrantModel g)
                    target.Grants.Remove(g);

                else
                {

                }

            }

        }

        public void VisitStringConstant(OStringConstant oStringConstant)
        {
        }

        public void VisitIntegerConstant(OIntegerConstant oIntegerConstant)
        {
            
        }

        public void VisitUnaryExpression(OUnaryExpression oUnaryExpression)
        {
            
        }

        public void VisitTableTypeDefinition(TableTypeDefinition tableTypeDefinition)
        {
            
        }

        public void VisitTableTypeDef(OTableTypeDef oTableTypeDef)
        {
            
        }

        public void VisitTypeReference(OTypeReference oTypeReference)
        {
            
        }

        public void VisitODecimalConstant(ODecimalConstant oDecimalConstant)
        {
            
        }

        public void VisitTableIndexedByPartExpression(OTableIndexedByPartExpression oTableIndexedByPartExpression)
        {
            
        }

        public void VisitTierceExpression(OTierceExpression oTierceExpression)
        {
            
        }

        public void VisitArrayTypeDef(OArrayTypeDef oArrayTypeDef)
        {
            
        }

        public void VisitMethodArgument(OMethodArgument oMethodArgument)
        {
            
        }

        public void VisitKeyWordConstant(OKeyWordConstant oKeyWordConstant)
        {
            
        }

        public void VisitTableComment(OTableCommentStatement oTableCommentStatement)
        {
            
        }

        public void VisitBoolConstant(OBoolConstant oBoolConstant)
        {
            
        }

        public void VisitRecordTypeDef(ORecordTypeDef oRecordTypeDef)
        {
            
        }

        public void VisitFieldSpecExpression(OFieldSpecExpression oFieldSpecExpression)
        {
            
        }

        public void VisitCodeVariableReferenceExpression(OCodeVariableReferenceExpression oCodeVariableReferenceExpression)
        {
            
        }

        public void VisitCursorDeclarationStatement(OCursorDeclarationStatement oCursorDeclarationStatement)
        {
            
        }

        public void VisitPhysicalAttributes(PhysicalAttributesModel physicalAttributesModel)
        {
            
        }

        public void VisitCodeVariableDeclarationStatement(OCodeVariableDeclarationStatement oCodeVariableDeclarationStatement)
        {
            
        }

        public void VisitVarrayTypeDefinition(OVarrayTypeDefinition oVarrayTypeDefinition)
        {
            
        }

        public void VisitTableColumnComment(OTableColumnCommentStatement oTableColumnCommentStatement)
        {
            
        }

        public void VisitCallMethodReference(OCallMethodReference oCallMethodReference)
        {
            
        }

        public void VisitBinaryExpression(OBinaryExpression oBinaryExpression)
        {
            
        }

        public void VisitRefCursorTypeDef(ORefCursorTypeDef oRefCursorTypeDef)
        {
            
        }

        public void VisitProperty(PropertyModel item)
        {
            
        }

        public void VisitSubPartition(SubPartitionModel item)
        {
            
        }

        public void VisitPrivilege(PrivilegeModel item)
        {
            
        }

        public void VisitBlocPartition(BlocPartitionModel blocPartitionModel)
        {
            
        }

        public void VisitPartitionColumn(PartitionColumnModel item)
        {
            
        }

        public void VisitOracleType(OracleType oracleType)
        {
            
        }

        public void VisitForeignKeyConstraint(ForeignKeyConstraintModel foreignKeyConstraintModel)
        {
            
        }

        private List<ItemBase> _sequenceToDelete = new List<ItemBase>();

    }


}
