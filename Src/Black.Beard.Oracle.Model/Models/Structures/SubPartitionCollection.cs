using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SubPartitionCollection : IndexedCollection<SubPartitionModel>
    {



        /// <summary>
        /// Ctor
        /// </summary>
        static SubPartitionCollection()
        {
            SubPartitionCollection.Key = IndexedCollection<SubPartitionModel>.GetMethodKey(c => c.Name);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
