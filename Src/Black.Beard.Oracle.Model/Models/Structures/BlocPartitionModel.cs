using Bb.Oracle.Models;

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
            this.PhysicalAttributes = new PhysicalAttributesModel() { Parent = this };
        }

        public override void Initialize()
        {
            this.Columns.Initialize();
            this.SubColumns.Initialize();
            this.PhysicalAttributes.Initialize();
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
        /// Def Max Size
        /// </summary>   
        public string DefMaxSize { get; set; }

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
        public PhysicalAttributesModel PhysicalAttributes { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.BlocPartition;

    }
}
