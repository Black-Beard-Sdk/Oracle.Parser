using System;
using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    public partial class OracleDatabase
    {

        public string Name { get; set; }

        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Available Owner
        /// </summary>
        public string AvailableOwner { get; set; }

        /// <summary>
        /// Source Script
        /// </summary>
        public bool SourceScript { get; set; }

        /// <summary>
        /// Tables
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TableCollection" />.");
        /// </returns>
        public TableCollection Tables { get; set; } = new TableCollection();

        /// <summary>
        /// Procedures
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ProcedureCollection" />.");
        /// </returns>
        public ProcedureCollection Procedures { get; set; } = new ProcedureCollection();

        /// <summary>
        /// Types
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TypeCollection" />.");
        /// </returns>

        public TypeCollection Types { get; set; } = new TypeCollection();

        /// <summary>
        /// Synonymes
        /// </summary>
        /// <returns>		
        /// Objet <see cref="SynonymCollection" />.");
        /// </returns>
        public SynonymCollection Synonymes { get; set; } = new SynonymCollection();

        /// <summary>
        /// Sequences
        /// </summary>
        /// <returns>		
        /// Objet <see cref="SequenceCollection" />.");
        /// </returns>
        public SequenceCollection Sequences { get; set; } = new SequenceCollection();

        /// <summary>
        /// Grants
        /// </summary>
        /// <returns>		
        /// Objet <see cref="GrantCollection" />.");
        /// </returns>
        public GrantCollection Grants { get; set; } = new GrantCollection();

        /// <summary>
        /// Packages
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PackageCollection" />.");
        /// </returns>
        public PackageCollection Packages { get; set; } = new PackageCollection();

        /// <summary>
        /// Tablespaces
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TablespaceCollection" />.");
        /// </returns>
        public TablespaceCollection Tablespaces { get; set; } = new TablespaceCollection();

        /// <summary>
        /// Partitions
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PartitionCollection" />.");
        /// </returns>
        public PartitionCollection Partitions { get; set; } = new PartitionCollection();

        private Dictionary<string, TableModel> _tables = null;
        private Dictionary<string, SequenceModel> _sequences = null;
        private Dictionary<string, ProcedureModel> _procedures = null;
        private Dictionary<string, ConstraintModel> _constraints = null;
        private Dictionary<string, TypeItem> _types = null;

        public TriggerModel ResolveTrigger(string key)
        {

            TriggerModel trigger = null;

            foreach (TableModel table in Tables)
            {

                trigger = table.Triggers[key];

                if (trigger != null)
                    break;

            }

            return trigger;

        }

        public bool ResolveIndex(string key, out IndexModel index)
        {

            index = null;

            foreach (TableModel table in Tables)
            {

                index = table.Indexes[key];

                if (index != null)
                    break;

            }

            return index != null;

        }

        public void Add(ProcedureModel item)
        {
            if (_procedures == null)
                Initialize();
            _procedures.Add(item.Key, item);
            Procedures.Add(item);
            item.Parent = this;
        }

        public void Add(SequenceModel item)
        {
            if (_sequences == null)
                Initialize();
            _sequences.Add(item.Name, item);
            Sequences.Add(item);
            item.Parent = this;
        }

        public void Add(TableModel item)
        {
            if (_tables == null)
                Initialize();
            _tables.Add(item.Key, item);
            Tables.Add(item);
            item.Parent = this;
        }

        public void Add(TypeItem item)
        {
            if (_types == null)
                Initialize();
            _types.Add(item.Key, item);
            Types.Add(item);
            item.Parent = this;
        }

        public bool ResolveProcedure(string key, out ProcedureModel table)
        {

            if (_procedures == null)
                Initialize();

            return _procedures.TryGetValue(key, out table);

        }

        public bool ResolveTable(string key, out TableModel table)
        {
            if (_tables == null)
                Initialize();

            return _tables.TryGetValue(key, out table);
        }

        public bool ResolveSequence(string key, out SequenceModel sequence)
        {

            if (_sequences == null)
                Initialize();

            return _sequences.TryGetValue(key, out sequence);
        }

        public void Initialize()
        {

            _constraints = new Dictionary<string, ConstraintModel>();
            _tables = new Dictionary<string, TableModel>();
            _sequences = new Dictionary<string, SequenceModel>();
            _procedures = new Dictionary<string, ProcedureModel>();
            _types = new Dictionary<string, TypeItem>();

            foreach (TableModel item in Tables)
            {

                item.Parent = this;
                _tables.Add(item.Key, item);

                foreach (ConstraintModel c in item.Constraints)
                {
                    if (!_constraints.ContainsKey(c.Key))
                        _constraints.Add(c.Key, c);
                }
            }

            foreach (SequenceModel item in this.Sequences)
            {
                item.Parent = this;
                if (!_sequences.ContainsKey(item.Name))
                    _sequences.Add(item.Name, item);
            }

            foreach (ProcedureModel item in this.Procedures)
            {
                item.Parent = this;
                if (!_procedures.ContainsKey(item.Key))
                    _procedures.Add(item.Key, item);
            }

            foreach (TypeItem item in this.Types)
            {
                item.Parent = this;
                if (!_types.ContainsKey(item.Key))
                    _types.Add(item.Key, item);
            }

            foreach (TableModel item in Tables)
                item.Initialize();

            foreach (SequenceModel item in this.Sequences)
                item.Initialize();

            foreach (ProcedureModel item in this.Procedures)
                item.Initialize();

            foreach (TypeItem item in this.Types)
                item.Initialize();

            foreach (GrantModel item in this.Grants)
            {
                item.Parent = this;
            }

            foreach (SynonymModel item in this.Synonymes)
            {
                item.Parent = this;
            }

        }

        internal bool ResolveConstraint(string key, out ConstraintModel constraint)
        {

            if (_constraints == null)
                Initialize();

            return _constraints.TryGetValue(key, out constraint);

        }

        /// <summary>
        /// Annotations for the debug. (this property is not serialized)
        /// </summary>
        public string Annotation { get; set; }

    }


}

