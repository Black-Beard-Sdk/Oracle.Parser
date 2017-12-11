using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Index
    /// </summary>
    public partial class IndexCollection : ItemBaseCollection<IndexModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static IndexCollection()
        {
            IndexCollection.Key = ItemBaseCollection<IndexModel>.GetMethodKey(c => c.Name);
        }

    }

}
