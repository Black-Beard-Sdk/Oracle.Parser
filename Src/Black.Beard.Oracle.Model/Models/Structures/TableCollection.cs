using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
