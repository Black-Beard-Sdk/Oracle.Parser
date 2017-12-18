using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TypeCollection : IndexedCollection<TypeItem>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TypeCollection()
        {
            TypeCollection.Key = IndexedCollection<TypeItem>.GetMethodKey(c => c.Key);
        }

    }

}
