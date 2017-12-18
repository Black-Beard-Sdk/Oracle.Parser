using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PartitionColumnCollection : IndexedCollection<PartitionColumnModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PartitionColumnCollection()
        {
            PartitionColumnCollection.Key = IndexedCollection<PartitionColumnModel>.GetMethodKey(c => c.ColumnName);
        }

    }

}
