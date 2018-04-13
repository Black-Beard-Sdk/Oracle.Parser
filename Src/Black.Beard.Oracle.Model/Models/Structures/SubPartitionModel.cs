using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SubPartitionModel : ItemBase
    {

        public SubPartitionModel()
        {
            this.PhysicalAttributes = new PhysicalAttributesModel() { Parent = this };
        }

        /// <summary>
        /// Subpartition Name
        /// </summary>   
        public string Name { get; set; }

        /// <summary>
        /// High Value
        /// </summary>   
        public string HighValue { get; set; }

        /// <summary>
        /// High Value Length
        /// </summary>   
        public decimal HighValueLength { get; set; }

        /// <summary>
        /// Subpartition Position
        /// </summary>   
        public decimal SubpartitionPosition { get; set; }

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
        /// Buffer Pool
        /// </summary>   
        public string BufferPool { get; set; }

        /// <summary>
        /// Flash Cache
        /// </summary>   
        public string FlashCache { get; set; }

        /// <summary>
        /// Chain Cnt
        /// </summary>   
        public decimal ChainCnt { get; set; }

        /// <summary>
        /// Global Stats
        /// </summary>   
        public bool GlobalStats { get; set; }

        /// <summary>
        /// User Stats
        /// </summary>   
        public bool UserStats { get; set; }

        /// <summary>
        /// Segment Created
        /// </summary>   
        public bool SegmentCreated { get; set; }

        /// <summary>
        /// Interval
        /// </summary>   
        public bool Interval { get; set; }

        /// <summary>
        /// Cell Flash Cache
        /// </summary>   
        public string CellFlashCache { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.SubPartition;

        public PhysicalAttributesModel PhysicalAttributes { get; private set; }
    }
}
