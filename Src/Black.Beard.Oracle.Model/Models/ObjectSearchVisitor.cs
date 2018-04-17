using Bb.Oracle.Contracts;
using Bb.Oracle.Models.Codes;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Models
{

    public class ObjectSearchVisitor : IOracleModelVisitor
    {
        private Func<ItemBase, bool> _filter;
        List<object> _items = new List<object>();

        public IEnumerable<object> Parse(OracleDatabase model, Action<string> _log, Func<ItemBase, bool> filter = null)
        {

            if (_log == null)
                _log = (c) => { };

            this._filter = filter;
            ModelParser parser = new ModelParser();
            parser.Visit(model, this, _log);
            return _items;
        }

        public void VisitArrayTypeDef(OArrayTypeDef oArrayTypeDef)
        {
        }

        public void VisitBinaryExpression(OBinaryExpression oBinaryExpression)
        {
        }

        public void VisitBlocPartition(BlocPartitionModel blocPartitionModel)
        {
        }

        public void VisitBoolConstant(OBoolConstant oBoolConstant)
        {
        }

        public void VisitCallMethodReference(OCallMethodReference oCallMethodReference)
        {
        }

        public void VisitCodeVariableDeclarationStatement(OCodeVariableDeclarationStatement oCodeVariableDeclarationStatement)
        {
        }

        public void VisitCodeVariableReferenceExpression(OCodeVariableReferenceExpression oCodeVariableReferenceExpression)
        {
        }

        public void VisitCursorDeclarationStatement(OCursorDeclarationStatement oCursorDeclarationStatement)
        {
        }

        public void VisitFieldSpecExpression(OFieldSpecExpression oFieldSpecExpression)
        {
        }

        public void VisitForeignKeyConstraint(ForeignKeyConstraintModel foreignKeyConstraintModel)
        {
        }

        public void VisitIntegerConstant(OIntegerConstant oIntegerConstant)
        {
        }

        public void VisitKeyWordConstant(OKeyWordConstant oKeyWordConstant)
        {
        }

        public void VisitMethodArgument(OMethodArgument oMethodArgument)
        {
        }

        public void VisitODecimalConstant(ODecimalConstant oDecimalConstant)
        {
        }

        public void VisitOracleType(OracleType oracleType)
        {
        }

        public void VisitPartitionColumn(PartitionColumnModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        public void VisitPhysicalAttributes(PhysicalAttributesModel physicalAttributesModel)
        {
        }

        public void VisitPrivilege(PrivilegeModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        public void VisitProperty(PropertyModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        public void VisitRecordTypeDef(ORecordTypeDef oRecordTypeDef)
        {
        }

        public void VisitRefCursorTypeDef(ORefCursorTypeDef oRefCursorTypeDef)
        {
        }

        public void VisitStringConstant(OStringConstant oStringConstant)
        {
        }

        public void VisitSubPartition(SubPartitionModel item)
        {
            if (this._filter(item)) _items.Add(item);
        }

        public void VisitTableColumnComment(OTableColumnCommentStatement oTableColumnCommentStatement)
        {
        }

        public void VisitTableComment(OTableCommentStatement oTableCommentStatement)
        {
        }

        public void VisitTableIndexedByPartExpression(OTableIndexedByPartExpression oTableIndexedByPartExpression)
        {
        }

        public void VisitTableTypeDef(OTableTypeDef oTableTypeDef)
        {
        }

        public void VisitTableTypeDefinition(TableTypeDefinition tableTypeDefinition)
        {
        }

        public void VisitTierceExpression(OTierceExpression oTierceExpression)
        {
        }

        public void VisitTypeReference(OTypeReference oTypeReference)
        {
        }

        public void VisitUnaryExpression(OUnaryExpression oUnaryExpression)
        {
        }

        public void VisitVarrayTypeDefinition(OVarrayTypeDefinition oVarrayTypeDefinition)
        {
        }

        void IOracleModelVisitor.VisitArgument(ArgumentModel item)
        {

        }

        void IOracleModelVisitor.VisitColumn(ColumnModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitConstraint(ConstraintModel item)
        {
            if (this._filter(item))

                _items.Add(item);
        }

        void IOracleModelVisitor.VisitConstraintColumn(ConstraintColumnModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitGrant(GrantModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitIndex(IndexModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitIndexColumn(IndexColumnModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitPackage(PackageModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitPartition(PartitionModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitPartitionRef(PartitionRefModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitProcedure(ProcedureModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitSchema(SchemaModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitSequence(SequenceModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitSynonym(SynonymModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitTable(TableModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitTablespace(TablespaceModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitTrigger(TriggerModel item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

        void IOracleModelVisitor.VisitType(TypeItem item)
        {
            if (this._filter(item))
                _items.Add(item);
        }

    }



}
