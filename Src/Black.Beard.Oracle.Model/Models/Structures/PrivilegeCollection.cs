using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PrivilegeCollection : IndexedCollection<PrivilegeModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PrivilegeCollection()
        {
            PrivilegeCollection.Key = IndexedCollection<PrivilegeModel>.GetMethodKey(c => c.Name);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
