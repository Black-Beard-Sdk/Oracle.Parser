using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Partition
    /// </summary>
    public partial class PartitionCollection : IndexedCollection<PartitionModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PartitionCollection()
        {
            PartitionCollection.Key = IndexedCollection<PartitionModel>.GetMethodKey(c => c.Name);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }


    }

}
