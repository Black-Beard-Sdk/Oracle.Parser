
using System.ComponentModel;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OracleType : ItemBase
    {

        public OracleType()
        {

        }

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
        [DefaultValue(0)]
        public int defaultLength { get; set; }

        /// <summary>
        /// Data Default
        /// </summary>   
        [DefaultValue("")]
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

        public override string GetName()
        {
            return TypeName;
        }

        public override string GetOwner()
        {
            return TypeOwner;
        }

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
        [DefaultValue(false)]
        public bool IsRecord { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.OracleType;

    }
}
