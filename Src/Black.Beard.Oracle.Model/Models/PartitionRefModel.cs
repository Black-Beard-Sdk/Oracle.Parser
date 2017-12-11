
namespace Bb.Oracle.Models
{
    /// <summary>
    /// Partition
    /// </summary>
    public partial class PartitionRefModel : ItemBase
    {

        /// <summary>
        /// Partition Name
        /// </summary>   
        public string PartitionName { get; set; }



        public TableModel Parent { get; set; }

        internal void Initialize()
        {
            this.Partition = Parent.Parent.Partitions[this.PartitionName];
        }

        public PartitionModel Partition { get; private set; }


    }
}
