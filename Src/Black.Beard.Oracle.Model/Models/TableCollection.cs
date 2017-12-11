using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableCollection : ItemBaseCollection<TableModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TableCollection()
        {
            ItemBaseCollection<TableModel>.Key = ItemBaseCollection<TableModel>.GetMethodKey(c => c.Key);
        }

    }

}
