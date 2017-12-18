using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableCollection : IndexedCollection<TableModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TableCollection()
        {
            IndexedCollection<TableModel>.Key = IndexedCollection<TableModel>.GetMethodKey(c => c.Key);
        }

    }

}
