using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SubPartitionCollection : ItemBaseCollection<SubPartitionModel>
    {



        /// <summary>
        /// Ctor
        /// </summary>
        static SubPartitionCollection()
        {
            SubPartitionCollection.Key = ItemBaseCollection<SubPartitionModel>.GetMethodKey(c => c.SubpartitionName);
        }

    }

}
