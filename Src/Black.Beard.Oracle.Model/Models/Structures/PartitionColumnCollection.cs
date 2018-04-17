using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PartitionColumnCollection : IndexedCollection<PartitionColumnModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PartitionColumnCollection()
        {
            PartitionColumnCollection.Key = IndexedCollection<PartitionColumnModel>.GetMethodKey(c => c.ColumnName);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
