using System.Collections.Generic;

namespace Bb.Oracle.Models
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

    }

}
