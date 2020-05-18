using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using Bb.Oracle.Contracts;
using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Key}")]
    public partial class TableModel : ItemBase, Ichangable
    {

        public TableModel()
        {
            this.Columns = new ColumnCollection() { Parent = this };
            this.Partitions = new PartitionRefCollection() { Parent = this };
            this.BlocPartition = new BlocPartitionModel() { Parent = this };
            this.PhysicalAttributes = new PhysicalAttributesModel() { Parent = this, };

        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitTable(this);
            this.Columns.Accept(visitor);
            this.Partitions.Accept(visitor);
            this.BlocPartition.Accept(visitor);
            this.PhysicalAttributes.Accept(visitor);
        }



        public override void Initialize()
        {
            this.Columns.Parent = this;
            this.Columns.Initialize();

            this.Partitions.Parent = this;
            this.Partitions.Initialize();
            
            this.BlocPartition.Parent = this;
            this.BlocPartition.Initialize();
            
            this.PhysicalAttributes.Parent = this;
            this.PhysicalAttributes.Initialize();
        
        }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Is View
        /// </summary>
        public bool IsView { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Comment
        /// </summary>
        [DefaultValue("")]
        public string Comment { get; set; }

        /// <summary>
        /// Generated
        /// </summary>
        public bool Generated { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Temporary
        /// </summary>
        public bool Temporary { get; set; }
        
        /// <summary>
        /// Cluster Name
        /// </summary>
        public string ClusterName { get; set; }

        /// <summary>
        /// Table Lock
        /// </summary>
        public string TableLock { get; set; }

        /// <summary>
        /// Cache
        /// </summary>
        public bool Cache { get; set; }

        /// <summary>
        /// Partitioned
        /// </summary>
        public bool Partitioned { get; set; }

        /// <summary>
        /// Secondary
        /// </summary>
        public bool Secondary { get; set; }

        /// <summary>
        /// Buffer Pool
        /// </summary>
        public string BufferPool { get; set; }

        /// <summary>
        /// Nested
        /// </summary>
        public bool Nested { get; set; }

        /// <summary>
        /// Flash Cache
        /// </summary>
        public string FlashCache { get; set; }

        /// <summary>
        /// Cell Flash Cache
        /// </summary>
        public string CellFlashCache { get; set; }

        /// <summary>
        /// Row Movement
        /// </summary>
        public bool RowMovement { get; set; }

        /// <summary>
        /// Global Stats
        /// </summary>
        public bool GlobalStats { get; set; }

        /// <summary>
        /// User Stats
        /// </summary>
        public bool UserStats { get; set; }

        /// <summary>
        /// Duration
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Skip Corrupt
        /// </summary>
        public string SkipCorrupt { get; set; }

        /// <summary>
        /// Monitoring
        /// </summary>
        public bool Monitoring { get; set; }

        /// <summary>
        /// Cluster Owner
        /// </summary>
        public string ClusterOwner { get; set; }

        /// <summary>
        /// Dependencies
        /// </summary>
        public bool Dependencies { get; set; }

        /// <summary>
        /// Compression
        /// </summary>
        public bool Compression { get; set; }

        /// <summary>
        /// Compress For
        /// </summary>
        public string CompressFor { get; set; }

        /// <summary>
        /// Dropped
        /// </summary>
        public bool Dropped { get; set; }

        /// <summary>
        /// Read Only
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Segment Created
        /// </summary>
        public bool SegmentCreated { get; set; }

        /// <summary>
        /// Result Cache
        /// </summary>
        public string ResultCache { get; set; }

        /// <summary>
        /// code View
        /// </summary>
        public string CodeView { get; set; }

        /// <summary>
        /// Is Matrialized View
        /// </summary>
        public bool IsMaterializedView { get; set; }

        /// <summary>
        /// Parsed
        /// </summary>
        public bool Parsed { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ColumnCollection" />.");
        /// </returns>
        public ColumnCollection Columns { get; set; }

        /// <summary>
        /// Partitions
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PartitionRefCollection" />.");
        /// </returns>
        public PartitionRefCollection Partitions { get; set; }

        /// <summary>
        /// TablePartition
        /// </summary>
        /// <returns>		
        /// Objet <see cref="BlocPartitionModel" />.");
        /// </returns>
        public BlocPartitionModel BlocPartition { get; set; }

        /// <summary>
        /// Physical attributes
        /// </summary>
        public PhysicalAttributesModel PhysicalAttributes { get; private set; }

        public string CustomName
        {
            get
            {
                if (string.IsNullOrEmpty(this.Owner))
                    return this.Name;
                return this.Owner + "." + this.Name;
            }
        }

        public IEnumerable<ColumnModel> OrderedColumns
        {
            get
            {
                if (_orderedColumns == null)
                    _orderedColumns = this.Columns.OfType<ColumnModel>().OrderBy(c => c.ColumnId).ToList();
                return _orderedColumns;
            }
        }

        public bool IsValid(HashSet<string> list)
        {

            if (list.Contains(this.Owner))
                return true;

            return false;

        }

        public void Create(IchangeVisitor visitor)
        {
            visitor.Create(this);
        }

        public void Drop(IchangeVisitor visitor)
        {
            visitor.Drop(this);
        }

        public void Alter(IchangeVisitor visitor, Ichangable source, string propertyName)
        {
            visitor.Alter(this, source as TableModel, propertyName);
        }

        public override KindModelEnum KindModel
        {
            get { return KindModelEnum.Table; }
        }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return this.Owner;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

        private IEnumerable<ColumnModel> _orderedColumns;

    }

}
