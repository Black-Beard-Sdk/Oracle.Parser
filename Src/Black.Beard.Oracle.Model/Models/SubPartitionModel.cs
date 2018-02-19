namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SubPartitionModel : ItemBase
    {

        /// <summary>
        /// Subpartition Name
        /// </summary>   
        public string SubpartitionName { get; set; }

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
        /// Tablespace Name
        /// </summary>   
        public string TablespaceName { get; set; }

        /// <summary>
        /// Pct Free
        /// </summary>   
        public decimal PctFree { get; set; }

        /// <summary>
        /// Pct Used
        /// </summary>   
        public decimal PctUsed { get; set; }

        /// <summary>
        /// Ini Trans
        /// </summary>   
        public decimal IniTrans { get; set; }

        /// <summary>
        /// Initial Trans
        /// </summary>   
        public decimal InitialTrans { get; set; }

        /// <summary>
        /// Max Trans
        /// </summary>   
        public decimal MaxTrans { get; set; }

        /// <summary>
        /// Initial Extent
        /// </summary>   
        public decimal InitialExtent { get; set; }

        /// <summary>
        /// Min Extent
        /// </summary>   
        public decimal MinExtent { get; set; }

        /// <summary>
        /// Max Extent
        /// </summary>   
        public decimal MaxExtent { get; set; }

        /// <summary>
        /// Next Extent
        /// </summary>   
        public decimal NextExtent { get; set; }

        /// <summary>
        /// Max Size
        /// </summary>   
        public decimal MaxSize { get; set; }

        /// <summary>
        /// Freelists
        /// </summary>   
        public decimal Freelists { get; set; }

        /// <summary>
        /// Freelist Groups
        /// </summary>   
        public decimal FreelistGroups { get; set; }

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
        /// Pct Increase
        /// </summary>   
        public decimal PctIncrease { get; set; }

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

    }
}
