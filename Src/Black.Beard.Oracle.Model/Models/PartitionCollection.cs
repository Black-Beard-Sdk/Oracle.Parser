using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Partition
    /// </summary>
    public partial class PartitionCollection : IndexedCollection<PartitionModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PartitionCollection()
        {
            PartitionCollection.Key = IndexedCollection<PartitionModel>.GetMethodKey(c => c.PartitionName);
        }

    }

}
