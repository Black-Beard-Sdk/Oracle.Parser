using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Partition
    /// </summary>
    public partial class PartitionRefCollection : IndexedCollection<PartitionRefModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PartitionRefCollection()
        {
            PartitionRefCollection.Key = IndexedCollection<PartitionRefModel>.GetMethodKey(c => c.PartitionName);
        }

    }



}
