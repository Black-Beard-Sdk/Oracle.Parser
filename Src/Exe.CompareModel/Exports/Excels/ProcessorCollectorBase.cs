using Bb.Oracle;
using Bb.Oracle.Models.Comparer;
using Bb.Oracle.Structures.Models;
using System.Collections.Generic;

namespace CompareModel
{
    public class ProcessorCollectorBase : ProcessorBase
    {


        public ProcessorCollectorBase()
        {

        }


        protected string ConvertToText(TypeDifferenceEnum kind)
        {
            switch (kind)
            {
                case TypeDifferenceEnum.MissingInTarget:
                    return string.Format("Missing in {0}", EnvironmentTarget);

                case TypeDifferenceEnum.MissingInSource:
                    return string.Format("Missing in {0}", EnvironmentSource);

                case TypeDifferenceEnum.Change:
                    return "Changed";

                case TypeDifferenceEnum.Orphean:
                    return "Orphean";

                case TypeDifferenceEnum.Doublon:
                    return "Duplicated";

                case TypeDifferenceEnum.None:
                    break;
                default:
                    break;
            }

            return kind.ToString();

        }

        public string EnvironmentSource { get; set; }

        public string EnvironmentTarget { get; set; }

        protected override void VisitDuplicateFile(DoublonModel doublonModel)
        {
            this._doublons.Add(doublonModel);
        }

        protected override void Visit(IndexModel source, IndexModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

            var ts = source.GetTable();
            table t;

            if (ts == null)
                return;

            if (!this._tables.TryGetValue(ts.Key, out t))
            {
                t = new table(ts, target != null ? target.GetTable() : null) { Kind = TypeDifferenceEnum.Change, DifferenceModel = item };
                this._tables.Add(ts.Key, t);
            }

            index c;
            if (!t.Indexes.TryGetValue(source.Key, out c))
            {
                c = new index(source, target) { Kind = kind, DifferenceModel = item };
                t.Indexes.Add(source.Key, c);
            }

            switch (kind)
            {
                case TypeDifferenceEnum.Change:
                    c.Changes.Add(propertyName);
                    break;
                case TypeDifferenceEnum.MissingInTarget:
                default:
                    break;
            }

        }


        protected override void Visit(PropertyModel source, PropertyModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected override void Visit(PackageModel source, PackageModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            package t;
            if (!this._packages.TryGetValue(source.Name, out t))
            {
                t = new package(source, target)
                {
                    Kind = kind,
                    Package = propertyName == "Code",
                    PackageBody = propertyName == "CodeBody",
                    DifferenceModel = item

                };
                this._packages.Add(source.Name, t);
            }
        }

        protected override void Visit(ProcedureModel source, ProcedureModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            procedure t;
            if (!this._procedures.TryGetValue(source.Key, out t))
            {
                t = new procedure(source, target) { Kind = kind, DifferenceModel = item };
                this._procedures.Add(source.Key, t);
            }
        }

        protected override void Visit(ArgumentModel source, ArgumentModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected override void Visit(TriggerModel source, TriggerModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

            var ts = source.GetTable();
            
            if (ts == null)
                return;

            table t;
            if (!this._tables.TryGetValue(ts.Key, out t))
            {
                t = new table(ts, target != null ? target.GetTable() : null) { Kind = TypeDifferenceEnum.None };
                this._tables.Add(ts.Key, t);
            }
            else
            {
                if (t.Target == null && target != null)
                {
                    t.Target = target.Parent.AsTable();
                }
            }

            trigger c;
            if (!t.Triggers.TryGetValue(source.Name, out c))
            {
                c = new trigger(source, target) { Kind = kind, DifferenceModel = item };
                t.Triggers.Add(source.Name, c);
            }

            if (kind == TypeDifferenceEnum.Change)
                c.Changes.Add(propertyName);

        }

        protected override void Visit(TableModel source, TableModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            if (source.Name == "NETWORK")
            {

            }

            table t;
            if (!this._tables.TryGetValue(source.Key, out t))
            {
                t = new table(source, target) { Kind = kind, DifferenceModel = item };
                this._tables.Add(source.Key, t);
            }

            switch (kind)
            {
                case TypeDifferenceEnum.Change:
                    t.Changes.Add(propertyName);
                    break;
                case TypeDifferenceEnum.MissingInTarget:
                default:
                    break;
            }

        }

        protected override void Visit(ColumnModel source, ColumnModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

            var ts = source.Parent.AsTable();
            table t;
            if (!this._tables.TryGetValue(ts.Key, out t))
            {
                t = new table(ts, target != null ? target.Parent.AsTable() : null) { Kind = TypeDifferenceEnum.Change, DifferenceModel = item };
                this._tables.Add(ts.Key, t);
            }
            else
            {
                if (t.Target == null && target != null)
                {
                    t.Target = target.Parent.AsTable();
                }
            }

            column c;
            if (!t.Columns.TryGetValue(source.Name, out c))
            {
                c = new column(source, target) { Kind = kind, DifferenceModel = item };
                t.Columns.Add(source.Name, c);
            }

            switch (kind)
            {
                case TypeDifferenceEnum.Change:
                    c.Changes.Add(propertyName);
                    break;
                case TypeDifferenceEnum.MissingInTarget:
                default:
                    break;
            }

        }

        protected override void Visit(ConstraintModel source, ConstraintModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            var ts = source.GetTable();

            if (ts == null)
                return;

            table t;
            if (!this._tables.TryGetValue(ts.Key, out t))
            {
                t = new table(ts, target != null ? target.GetTable() : null) { Kind = TypeDifferenceEnum.Change, DifferenceModel = item };
                this._tables.Add(ts.Key, t);
            }
            else
            {
                if (t.Target == null && target != null)
                {
                    t.Target = target.Parent.AsTable();
                }
            }

            constraint c;
            if (!t.Constraints.TryGetValue(source.Name, out c))
            {
                c = new constraint(source, target) { Kind = kind, DifferenceModel = item };
                t.Constraints.Add(source.Name, c);
            }

            if (kind == TypeDifferenceEnum.Change)
                c.Changes.Add(propertyName);

        }

        protected override void Visit(SequenceModel source, SequenceModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            sequence t;
            if (!this._sequences.TryGetValue(source.Name, out t))
            {
                t = new sequence(source, target) { Kind = kind, DifferenceModel = item };
                this._sequences.Add(source.Name, t);
            }

            if (kind == TypeDifferenceEnum.Change)
                t.Changes.Add(propertyName);

        }

        protected override void Visit(SynonymModel source, SynonymModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            synonym t;
            if (!this._synonyms.TryGetValue(source.Key, out t))
            {
                t = new synonym(source, target) { Kind = kind, DifferenceModel = item };
                this._synonyms.Add(source.Key, t);
            }
        }

        protected override void Visit(TypeItem source, TypeItem target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            type t;
            if (!this._types.TryGetValue(source.Key, out t))
            {
                t = new type(source, target) { Kind = kind, DifferenceModel = item };
                this._types.Add(source.Key, t);
            }
        }

        protected override void Visit(GrantModel source, GrantModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

            if (!source.FullObjectName.Contains(".BIN$"))
            {
                grant t;
                if (!this._grants.TryGetValue(source.Key, out t))
                {
                    t = new grant(source, target) { Kind = kind, DifferenceModel = item };
                    this._grants.Add(source.Key, t);
                }
            }
        }

        protected List<DoublonModel> _doublons = new List<DoublonModel>();
        protected Dictionary<string, table> _tables = new Dictionary<string, table>();
        protected Dictionary<string, grant> _grants = new Dictionary<string, grant>();
        protected Dictionary<string, type> _types = new Dictionary<string, type>();
        protected Dictionary<string, synonym> _synonyms = new Dictionary<string, synonym>();
        protected Dictionary<string, sequence> _sequences = new Dictionary<string, sequence>();
        protected Dictionary<string, procedure> _procedures = new Dictionary<string, procedure>();
        protected Dictionary<string, package> _packages = new Dictionary<string, package>();

        public class trigger
        {

            public TriggerModel Source { get; set; }

            public TriggerModel Target { get; set; }

            public trigger(TriggerModel source, TriggerModel target)
            {
                this.Source = source;
                this.Target = target;
                this.Changes = new List<string>();
            }

            public TypeDifferenceEnum Kind { get; internal set; }

            public List<string> Changes { get; set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }


        public class procedure
        {

            public ProcedureModel Source { get; set; }

            public ProcedureModel Target { get; set; }

            public procedure(ProcedureModel source, ProcedureModel target)
            {
                this.Source = source;
                this.Target = target;
            }

            public TypeDifferenceEnum Kind { get; internal set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class package
        {

            public PackageModel Source { get; set; }

            public PackageModel Target { get; set; }

            public package(PackageModel source, PackageModel target)
            {
                this.Source = source;
                this.Target = target;
            }

            public TypeDifferenceEnum Kind { get; internal set; }
            public bool PackageBody { get; internal set; }
            public bool Package { get; internal set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class constraint
        {

            public ConstraintModel Source { get; set; }

            public ConstraintModel Target { get; set; }

            public constraint(ConstraintModel source, ConstraintModel target)
            {
                this.Source = source;
                this.Target = target;
                this.Changes = new List<string>();
            }

            public TypeDifferenceEnum Kind { get; internal set; }

            public List<string> Changes { get; set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class index
        {

            public IndexModel Source { get; set; }

            public IndexModel Target { get; set; }

            public index(IndexModel source, IndexModel target)
            {
                this.Source = source;
                this.Target = target;
                this.Changes = new List<string>();
            }

            public TypeDifferenceEnum Kind { get; internal set; }

            public List<string> Changes { get; set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class sequence
        {

            public SequenceModel Source { get; set; }

            public SequenceModel Target { get; set; }

            public sequence(SequenceModel source, SequenceModel target)
            {
                this.Source = source;
                this.Target = target;
                this.Changes = new List<string>();
            }

            public TypeDifferenceEnum Kind { get; internal set; }

            public List<string> Changes { get; set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class type
        {

            public TypeItem Source { get; set; }

            public TypeItem Target { get; set; }

            public type(TypeItem source, TypeItem target)
            {
                this.Source = source;
                this.Target = target;
            }

            public TypeDifferenceEnum Kind { get; internal set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class synonym
        {

            public SynonymModel Source { get; set; }

            public SynonymModel Target { get; set; }

            public synonym(SynonymModel source, SynonymModel target)
            {
                this.Source = source;
                this.Target = target;
            }

            public TypeDifferenceEnum Kind { get; internal set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class grant
        {

            public GrantModel Source { get; set; }
            public GrantModel Target { get; set; }

            public grant(GrantModel source, GrantModel target)
            {
                this.Source = source;
                this.Target = target;
            }

            public TypeDifferenceEnum Kind { get; internal set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class table
        {

            public TableModel Source { get; set; }
            public TableModel Target { get; set; }

            public table(TableModel source, TableModel target)
            {
                this.Source = source;
                this.Target = target;
                Columns = new Dictionary<string, column>();
                this.Changes = new List<string>();
            }

            public TypeDifferenceEnum Kind { get; internal set; }

            public Dictionary<string, column> Columns { get; set; }

            public Dictionary<string, constraint> Constraints = new Dictionary<string, constraint>();

            public Dictionary<string, index> Indexes = new Dictionary<string, index>();

            public Dictionary<string, trigger> Triggers = new Dictionary<string, trigger>();

            public List<string> Changes { get; set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }

        public class column
        {

            public ColumnModel Source { get; set; }
            public ColumnModel Target { get; set; }

            public column(ColumnModel source, ColumnModel target)
            {
                this.Source = source;
                this.Target = target;
                this.Changes = new List<string>();
            }

            public TypeDifferenceEnum Kind { get; internal set; }

            public List<string> Changes { get; set; }
            public DifferenceModel DifferenceModel { get; internal set; }
        }


    }

}

