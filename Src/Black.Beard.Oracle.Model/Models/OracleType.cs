
namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OracleType : ItemBase
    {

        /// <summary>
        /// Data Type
        /// </summary>   
        public string DataType { get; set; }

        /// <summary>
        /// Data Length
        /// </summary>   
        public int DataLength { get; set; }

        /// <summary>
        /// default Length
        /// </summary>   
        public int defaultLength { get; set; }

        /// <summary>
        /// Data Default
        /// </summary>   
        public string DataDefault { get; set; }

        /// <summary>
        /// Data Precision
        /// </summary>   
        public int DataPrecision { get; set; }

        /// <summary>
        /// Db Type
        /// </summary>   
        public string DbType { get; set; }

        /// <summary>
        /// Type Owner
        /// </summary>   
        public string TypeOwner { get; set; }

        /// <summary>
        /// Type Name
        /// </summary>   
        public string TypeName { get; set; }

        /// <summary>
        /// Data Level
        /// </summary>   
        public int DataLevel { get; set; }

        /// <summary>
        /// Is Array
        /// </summary>   
        public bool IsArray { get; set; }

        /// <summary>
        /// Array Of Type
        /// </summary>   
        public string ArrayOfType { get; set; }

        /// <summary>
        /// Is Record
        /// </summary>   
        public bool IsRecord { get; set; }

    }
}
