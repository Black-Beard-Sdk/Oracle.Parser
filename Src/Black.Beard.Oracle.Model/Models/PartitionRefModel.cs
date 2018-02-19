
using Newtonsoft.Json;

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

        public override void Initialize()
        {
            this.Partition = this.Root.Partitions[this.PartitionName];
        }

        public PartitionModel Partition { get; private set; }

        public override KindModelEnum KindModel => KindModelEnum.PartitionRef;

    }
}
