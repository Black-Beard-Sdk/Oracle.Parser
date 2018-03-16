using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SubPartitionCollection : IndexedCollection<SubPartitionModel>
    {



        /// <summary>
        /// Ctor
        /// </summary>
        static SubPartitionCollection()
        {
            SubPartitionCollection.Key = IndexedCollection<SubPartitionModel>.GetMethodKey(c => c.Name);
        }

    }

}
