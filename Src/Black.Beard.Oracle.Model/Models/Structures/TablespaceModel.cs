using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TablespaceModel : ItemBase
    {
        
        /// <summary>
        /// Tablespace Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Block Size
        /// </summary>
        public decimal BlockSize { get; set; }

        /// <summary>
        /// Initial Extent
        /// </summary>
        public decimal InitialExtent { get; set; }
        
        /// <summary>
        /// Min Extents
        /// </summary>   
        public decimal MinExtents { get; set; }
        
        /// <summary>
        /// Next Extent
        /// </summary>   
        public decimal NextExtent { get; set; }

        /// <summary>
        /// Max Size
        /// </summary>   
        public decimal MaxSize { get; set; }

        /// <summary>
        /// Max Extents
        /// </summary>   
        public decimal MaxExtents { get; set; }

        /// <summary>
        /// Pct Increase
        /// </summary>   
        public decimal PctIncrease { get; set; }

        /// <summary>
        /// Min Extlen
        /// </summary>   
        public decimal MinExtlen { get; set; }

        /// <summary>
        /// Status
        /// </summary>   
        public string Status { get; set; }

        /// <summary>
        /// Contents
        /// </summary>   
        public string Contents { get; set; }

        /// <summary>
        /// Logging
        /// </summary>   
        public string Logging { get; set; }

        /// <summary>
        /// Force Logging
        /// </summary>   
        public bool ForceLogging { get; set; }

        /// <summary>
        /// Extent Management
        /// </summary>   
        public string ExtentManagement { get; set; }

        /// <summary>
        /// Allocation Type
        /// </summary>   
        public string AllocationType { get; set; }

        /// <summary>
        /// Plugged In
        /// </summary>   
        public bool PluggedIn { get; set; }

        /// <summary>
        /// Segment Space Management
        /// </summary>   
        public string SegmentSpaceManagement { get; set; }

        /// <summary>
        /// Def Tab Compression
        /// </summary>   
        public string DefTabCompression { get; set; }

        /// <summary>
        /// Retention
        /// </summary>   
        public string Retention { get; set; }

        /// <summary>
        /// Bigfile
        /// </summary>   
        public bool Bigfile { get; set; }

        /// <summary>
        /// Predicate Evaluation
        /// </summary>   
        public string PredicateEvaluation { get; set; }

        /// <summary>
        /// Encrypted
        /// </summary>   
        public bool Encrypted { get; set; }

        /// <summary>
        /// Compress For
        /// </summary>   
        public string CompressFor { get; set; }

        /// <summary>
        /// Group Name
        /// </summary>   
        public string GroupName { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.Tablespace;

    }

}
