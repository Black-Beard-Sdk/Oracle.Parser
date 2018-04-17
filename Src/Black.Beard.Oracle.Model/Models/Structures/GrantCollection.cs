using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Grant
    /// </summary>
    public partial class GrantCollection : IndexedCollection<GrantModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static GrantCollection()
        {
            GrantCollection.Key = IndexedCollection<GrantModel>.GetMethodKey(c => c.Key);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
