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
