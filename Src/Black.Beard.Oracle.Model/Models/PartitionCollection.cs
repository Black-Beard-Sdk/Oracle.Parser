using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Partition
    /// </summary>
    public partial class PartitionCollection : ItemBaseCollection<PartitionModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PartitionCollection()
        {
            PartitionCollection.Key = ItemBaseCollection<PartitionModel>.GetMethodKey(c => c.PartitionName);
        }

    }

}
