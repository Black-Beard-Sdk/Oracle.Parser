using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PartitionColumnCollection : ItemBaseCollection<PartitionColumnModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PartitionColumnCollection()
        {
            PartitionColumnCollection.Key = ItemBaseCollection<PartitionColumnModel>.GetMethodKey(c => c.ColumnName);
        }

    }

}
