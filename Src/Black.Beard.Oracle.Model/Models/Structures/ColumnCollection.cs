using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
