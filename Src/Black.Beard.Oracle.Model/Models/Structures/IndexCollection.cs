using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Index
    /// </summary>
    public partial class IndexCollection : IndexedCollection<IndexModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static IndexCollection()
        {
            IndexCollection.Key = IndexedCollection<IndexModel>.GetMethodKey(c => c.Key);
        }

    }

}
