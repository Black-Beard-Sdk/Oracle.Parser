
using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Partition
    /// </summary>
    public partial class PartitionModel : ItemBase
    {

        public PartitionModel()
        {
            this.SubPartitions = new SubPartitionCollection() { Parent = this };
            this.PhysicalAttributes = new PhysicalAttributesModel() { Parent = this };
        }

        public override void Initialize()
        {
            this.SubPartitions.Initialize();
            this.PhysicalAttributes.Initialize();
        }

        /// <summary>
        /// Partition Name
        /// </summary>   
        public string Name { get; set; }

        /// <summary>
        /// Composite
        /// </summary>   
        public bool Composite { get; set; }

        /// <summary>
        /// High Value
        /// </summary>   
        public string HighValue { get; set; }

        /// <summary>
        /// High Value Length
        /// </summary>   
        public decimal HighValueLength { get; set; }

        /// <summary>
        /// Partition Position
        /// </summary>   
        public decimal PartitionPosition { get; set; }
        
        /// <summary>
        /// Max Size
        /// </summary>   
        public decimal MaxSize { get; set; }
        
        /// <summary>
        /// Logging
        /// </summary>   
        public bool Logging { get; set; }

        /// <summary>
        /// Compression
        /// </summary>   
        public string Compression { get; set; }

        /// <summary>
        /// Compress For
        /// </summary>   
        public string CompressFor { get; set; }

        /// <summary>
        /// Num Rows
        /// </summary>   
        public decimal NumRows { get; set; }

        /// <summary>
        /// Blocks
        /// </summary>   
        public decimal Blocks { get; set; }

        /// <summary>
        /// Empty Blocks
        /// </summary>   
        public decimal EmptyBlocks { get; set; }

        /// <summary>
        /// Chain Cnt
        /// </summary>   
        public decimal ChainCnt { get; set; }

        /// <summary>
        /// Cell Flash Cache
        /// </summary>   
        public string CellFlashCache { get; set; }

        /// <summary>
        /// Global Stats
        /// </summary>   
        public bool GlobalStats { get; set; }

        /// <summary>
        /// Is Nested
        /// </summary>   
        public bool IsNested { get; set; }

        /// <summary>
        /// User Stats
        /// </summary>   
        public bool UserStats { get; set; }

        /// <summary>
        /// Parent Table Partition
        /// </summary>   
        public string ParentTablePartition { get; set; }

        /// <summary>
        /// Interval
        /// </summary>   
        public bool Interval { get; set; }

        /// <summary>
        /// Segment Created
        /// </summary>   
        public bool SegmentCreated { get; set; }

        /// <summary>
        /// SubPartitions
        /// </summary>
        /// <returns>		
        /// Objet <see cref="SubPartitionCollection" />.");
        /// </returns>   
        public SubPartitionCollection SubPartitions { get; set; }
        public PhysicalAttributesModel PhysicalAttributes { get; private set; }

        public override KindModelEnum KindModel =>  KindModelEnum.Partition;


    }



}
