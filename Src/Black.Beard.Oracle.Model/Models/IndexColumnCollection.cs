using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IndexColumnCollection : ItemBaseCollection<IndexColumnModel>
    {

        static IndexColumnCollection()
        {
            IndexColumnCollection.Key = ItemBaseCollection<IndexColumnModel>.GetMethodKey(c => c.Name);
        }

    }



}
