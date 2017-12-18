using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IndexColumnCollection : IndexedCollection<IndexColumnModel>
    {

        static IndexColumnCollection()
        {
            IndexColumnCollection.Key = IndexedCollection<IndexColumnModel>.GetMethodKey(c => c.Name);
        }

    }



}
