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
