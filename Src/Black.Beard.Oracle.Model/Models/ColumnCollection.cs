using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Column
    /// </summary>
    public partial class ColumnCollection : IndexedCollection<ColumnModel>
    {

        public ColumnCollection()
        {

        }

        static ColumnCollection()
        {
            ColumnCollection.Key = IndexedCollection<ColumnModel>.GetMethodKey(c => c.Key);
        }

    }

}
