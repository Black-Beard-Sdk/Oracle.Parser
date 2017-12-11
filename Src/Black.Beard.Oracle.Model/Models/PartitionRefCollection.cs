using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Partition
    /// </summary>
    public partial class PartitionRefCollection : ItemBaseCollection<PartitionRefModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PartitionRefCollection()
        {
            PartitionRefCollection.Key = ItemBaseCollection<PartitionRefModel>.GetMethodKey(c => c.PartitionName);
        }

    }



}
