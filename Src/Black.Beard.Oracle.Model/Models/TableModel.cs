using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Key}")]
    public partial class TableModel : ItemBase, Ichangable
    {

        public string Name { get; set; }

        /// <summary>
        /// Is View
        /// </summary>
        public bool IsView { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
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
        /// Tablespace Name
        /// </summary>
        public string TablespaceName { get; set; }

        /// <summary>
        /// Cluster Name
        /// </summary>
        public string ClusterName { get; set; }

        /// <summary>
        /// Logging
        /// </summary>
        public decimal Logging { get; set; }

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
        /// Pct Free
        /// </summary>
        public decimal PctFree { get; set; }

        /// <summary>
        /// Initial Extent
        /// </summary>
        public decimal InitialExtent { get; set; }

        /// <summary>
        /// Pct Used
        /// </summary>
        public decimal PctUsed { get; set; }

        /// <summary>
        /// Next Extent
        /// </summary>
        public decimal NextExtent { get; set; }

        /// <summary>
        /// Min Extents
        /// </summary>
        public decimal MinExtents { get; set; }

        /// <summary>
        /// Max Extents
        /// </summary>
        public decimal MaxExtents { get; set; }

        /// <summary>
        /// Ini Trans
        /// </summary>
        public decimal IniTrans { get; set; }

        /// <summary>
        /// Max Trans
        /// </summary>
        public decimal MaxTrans { get; set; }

        /// <summary>
        /// code View
        /// </summary>
        public string codeView { get; set; }

        /// <summary>
        /// Is Matrialized View
        /// </summary>
        public bool IsMatrializedView { get; set; }

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
        public ColumnCollection Columns { get; set; } = new ColumnCollection();

        /// <summary>
        /// Constraints
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ConstraintCollection" />.");
        /// </returns>
        public ConstraintCollection Constraints { get; set; } = new ConstraintCollection();

        /// <summary>
        /// Indexes
        /// </summary>
        /// <returns>		
        /// Objet <see cref="IndexCollection" />.");
        /// </returns>
        public IndexCollection Indexes { get; set; } = new IndexCollection();

        /// <summary>
        /// Triggers
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TriggerCollection" />.");
        /// </returns>
        public TriggerCollection Triggers { get; set; } = new TriggerCollection();

        /// <summary>
        /// Partitions
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PartitionRefCollection" />.");
        /// </returns>
        public PartitionRefCollection Partitions { get; set; } = new PartitionRefCollection();

        /// <summary>
        /// TablePartition
        /// </summary>
        /// <returns>		
        /// Objet <see cref="BlocPartitionModel" />.");
        /// </returns>
        public BlocPartitionModel BlocPartition { get; set; }= new BlocPartitionModel();

        public string CustomName
        {
            get
            {
                if (string.IsNullOrEmpty(this.SchemaName))
                    return this.Name;
                return this.SchemaName + "." + this.Name;
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

            if (list.Contains(this.SchemaName))
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

        public void Initialize()
        {

            foreach (ColumnModel item in this.Columns)
            {
                item.Parent = this;
                item.Initialize();
            }

            foreach (IndexModel item in this.Indexes)
            {
                item.Parent = this;
                item.Initialize();
            }

            foreach (ConstraintModel item in this.Constraints)
            {
                item.Parent = this;
                item.Initialize();
            }

            foreach (PartitionRefModel item in this.Partitions)
            {
                item.Parent = this;
                item.Initialize();
            }

            foreach (TriggerModel item in this.Triggers)
            {
                item.Parent = this;
                item.Initialize();
            }


        }

        [JsonIgnore]
        public OracleDatabase Parent { get; set; }

        public KindModelEnum KindModel
        {
            get { return KindModelEnum.Table; }
        }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return this.SchemaName;
        }
     
        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

        private IEnumerable<ColumnModel> _orderedColumns;

    }

}
