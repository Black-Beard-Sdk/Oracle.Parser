using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }



}
