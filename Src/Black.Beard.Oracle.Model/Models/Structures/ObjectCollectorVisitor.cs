using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Structures.Models
{


    //public class ObjectCollectorItem
    //{
    //    public string Name { get; internal set; }
    //    public string ParentName { get; internal set; }
    //    public string SchemaName { get; internal set; }
    //    public ItemBase Tag { get; internal set; }
    //}

    public class ObjectCollectorVisitor : IOracleModelVisitor
    {

        List<object> _items = new List<object>();

        public List<object> Parse(OracleDatabase model, Action<string> _log)
        {
            if (_items.Count == 0)
            {
                ModelParser parser = new ModelParser();
                parser.Visit(model, this, _log);
            }
            return _items;
        }

        public void VisitArrayTypeDef(OArrayTypeDef oArrayTypeDef)
        {
            throw new NotImplementedException();
        }

        public void VisitBinaryExpression(OBinaryExpression oBinaryExpression)
        {
            throw new NotImplementedException();
        }

        public void VisitBlocPartition(BlocPartitionModel blocPartitionModel)
        {
            throw new NotImplementedException();
        }

        public void VisitBoolConstant(OBoolConstant oBoolConstant)
        {
            throw new NotImplementedException();
        }

        public void VisitCallMethodReference(OCallMethodReference oCallMethodReference)
        {
            throw new NotImplementedException();
        }

        public void VisitCodeVariableDeclarationStatement(OCodeVariableDeclarationStatement oCodeVariableDeclarationStatement)
        {
            throw new NotImplementedException();
        }

        public void VisitCodeVariableReferenceExpression(OCodeVariableReferenceExpression oCodeVariableReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public void VisitCursorDeclarationStatement(OCursorDeclarationStatement oCursorDeclarationStatement)
        {
            throw new NotImplementedException();
        }

        public void VisitFieldSpecExpression(OFieldSpecExpression oFieldSpecExpression)
        {
            throw new NotImplementedException();
        }

        public void VisitForeignKeyConstraint(ForeignKeyConstraintModel foreignKeyConstraintModel)
        {
            throw new NotImplementedException();
        }

        public void VisitIntegerConstant(OIntegerConstant oIntegerConstant)
        {
            throw new NotImplementedException();
        }

        public void VisitKeyWordConstant(OKeyWordConstant oKeyWordConstant)
        {
            throw new NotImplementedException();
        }

        public void VisitMethodArgument(OMethodArgument oMethodArgument)
        {
            throw new NotImplementedException();
        }

        public void VisitODecimalConstant(ODecimalConstant oDecimalConstant)
        {
            throw new NotImplementedException();
        }

        public void VisitOracleType(OracleType oracleType)
        {
            throw new NotImplementedException();
        }

        public void VisitPartitionColumn(PartitionColumnModel item)
        {
            throw new NotImplementedException();
        }

        public void VisitPhysicalAttributes(PhysicalAttributesModel physicalAttributesModel)
        {
            throw new NotImplementedException();
        }

        public void VisitPrivilege(PrivilegeModel item)
        {
            throw new NotImplementedException();
        }

        public void VisitProperty(PropertyModel item)
        {
            throw new NotImplementedException();
        }

        public void VisitRecordTypeDef(ORecordTypeDef oRecordTypeDef)
        {
            throw new NotImplementedException();
        }

        public void VisitRefCursorTypeDef(ORefCursorTypeDef oRefCursorTypeDef)
        {
            throw new NotImplementedException();
        }

        public void VisitStringConstant(OStringConstant oStringConstant)
        {
            throw new NotImplementedException();
        }

        public void VisitSubPartition(SubPartitionModel item)
        {
            throw new NotImplementedException();
        }

        public void VisitTableColumnComment(OTableColumnCommentStatement oTableColumnCommentStatement)
        {
            throw new NotImplementedException();
        }

        public void VisitTableComment(OTableCommentStatement oTableCommentStatement)
        {
            throw new NotImplementedException();
        }

        public void VisitTableIndexedByPartExpression(OTableIndexedByPartExpression oTableIndexedByPartExpression)
        {
            throw new NotImplementedException();
        }

        public void VisitTableTypeDef(OTableTypeDef oTableTypeDef)
        {
            throw new NotImplementedException();
        }

        public void VisitTableTypeDefinition(TableTypeDefinition tableTypeDefinition)
        {
            throw new NotImplementedException();
        }

        public void VisitTierceExpression(OTierceExpression oTierceExpression)
        {
            throw new NotImplementedException();
        }

        public void VisitTypeReference(OTypeReference oTypeReference)
        {
            throw new NotImplementedException();
        }

        public void VisitUnaryExpression(OUnaryExpression oUnaryExpression)
        {
            throw new NotImplementedException();
        }

        public void VisitVarrayTypeDefinition(OVarrayTypeDefinition oVarrayTypeDefinition)
        {
            throw new NotImplementedException();
        }

        void IOracleModelVisitor.VisitArgument(ArgumentModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitColumn(ColumnModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitConstraint(ConstraintModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitConstraintColumn(ConstraintColumnModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitGrant(GrantModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitIndex(IndexModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitIndexColumn(IndexColumnModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitPackage(PackageModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitPartition(PartitionModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitPartitionRef(PartitionRefModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitProcedure(ProcedureModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitSchema(SchemaModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitSequence(SequenceModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitSynonym(SynonymModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitTable(TableModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitTablespace(TablespaceModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitTrigger(TriggerModel item)
        {
            _items.Add(item);
        }

        void IOracleModelVisitor.VisitType(TypeItem item)
        {
            _items.Add(item);
        }
    }



}
