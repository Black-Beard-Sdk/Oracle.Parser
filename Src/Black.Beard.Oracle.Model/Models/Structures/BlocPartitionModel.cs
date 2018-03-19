namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BlocPartitionModel : ItemBase
    {


        public BlocPartitionModel()
        {
            this.Columns = new PartitionColumnCollection() { Parent = this };
            this.SubColumns = new PartitionColumnCollection() { Parent = this };
        }

        /// <summary>
        /// Partitioning Type
        /// </summary>   
        public string PartitioningType { get; set; }

        /// <summary>
        /// Subpartitioning Type
        /// </summary>   
        public string SubpartitioningType { get; set; }

        /// <summary>
        /// Status
        /// </summary>   
        public string Status { get; set; }

        /// <summary>
        /// Def Tablespace Name
        /// </summary>   
        public string DefTablespaceName { get; set; }

        /// <summary>
        /// Def Pct Free
        /// </summary>   
        public decimal DefPctFree { get; set; }

        /// <summary>
        /// Def Pct Used
        /// </summary>   
        public decimal DefPctUsed { get; set; }

        /// <summary>
        /// Def Ini Trans
        /// </summary>   
        public decimal DefIniTrans { get; set; }

        /// <summary>
        /// Def Max Trans
        /// </summary>   
        public decimal DefMaxTrans { get; set; }

        /// <summary>
        /// Def Initial Extent
        /// </summary>   
        public string DefInitialExtent { get; set; }

        /// <summary>
        /// Def Next Extent
        /// </summary>   
        public string DefNextExtent { get; set; }

        /// <summary>
        /// Def Min Extents
        /// </summary>   
        public string DefMinExtents { get; set; }

        /// <summary>
        /// Def Max Extents
        /// </summary>   
        public string DefMaxExtents { get; set; }

        /// <summary>
        /// Def Max Size
        /// </summary>   
        public string DefMaxSize { get; set; }

        /// <summary>
        /// Def Pct Increase
        /// </summary>   
        public string DefPctIncrease { get; set; }

        /// <summary>
        /// Def Freelists
        /// </summary>   
        public decimal DefFreelists { get; set; }

        /// <summary>
        /// Def Freelist Groups
        /// </summary>   
        public decimal DefFreelistGroups { get; set; }

        /// <summary>
        /// Def Logging
        /// </summary>   
        public bool DefLogging { get; set; }

        /// <summary>
        /// Def Compression
        /// </summary>   
        public string DefCompression { get; set; }

        /// <summary>
        /// Def Compress For
        /// </summary>   
        public string DefCompressFor { get; set; }

        /// <summary>
        /// Def Buffer Pool
        /// </summary>   
        public string DefBufferPool { get; set; }

        /// <summary>
        /// Def Flash Cache
        /// </summary>   
        public string DefFlashCache { get; set; }

        /// <summary>
        /// Def Cell Flash Cache
        /// </summary>   
        public string DefCellFlashCache { get; set; }

        /// <summary>
        /// Ref Ptn Constraint Name
        /// </summary>   
        public string RefPtnConstraintName { get; set; }

        /// <summary>
        /// Interval
        /// </summary>   
        public string Interval { get; set; }

        /// <summary>
        /// Is Nested
        /// </summary>   
        public bool IsNested { get; set; }

        /// <summary>
        /// Def Segment Creation
        /// </summary>   
        public string DefSegmentCreation { get; set; }

        /// <summary>
        /// Locality
        /// </summary>   
        public string Locality { get; set; }

        /// <summary>
        /// Def Parameters
        /// </summary>   
        public string DefParameters { get; set; }

        /// <summary>
        /// Alignment
        /// </summary>   
        public string Alignment { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PartitionColumnCollection" />.");
        /// </returns>   
        public PartitionColumnCollection Columns { get; set; }

        /// <summary>
        /// SubColumns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PartitionColumnCollection" />.");
        /// </returns>   
        public PartitionColumnCollection SubColumns { get; set; }

        public override KindModelEnum KindModel =>  KindModelEnum.BlocPartition;

    }
}
