using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Column
    /// </summary>
    public partial class ColumnCollection : ItemBaseCollection<ColumnModel>
    {

        public int Count { get { return 0; } }

        static ColumnCollection()
        {
            ColumnCollection.Key = ItemBaseCollection<ColumnModel>.GetMethodKey(c => c.Key);
        }

    }

}
