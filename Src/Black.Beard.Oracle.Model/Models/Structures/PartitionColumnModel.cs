
using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PartitionColumnModel : ItemBase
    {

        /// <summary>
        /// Column Name
        /// </summary>   
        public string ColumnName { get; set; }

        /// <summary>
        /// Column Position
        /// </summary>   
        public int ColumnPosition { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.PartitionColumn;

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitPartitionColumn(this);
        }

    }
}
