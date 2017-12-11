using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Grant
    /// </summary>
    public partial class GrantCollection : ItemBaseCollection<GrantModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static GrantCollection()
        {
            GrantCollection.Key = ItemBaseCollection<GrantModel>.GetMethodKey(c => c.Key);
        }

    }

}
