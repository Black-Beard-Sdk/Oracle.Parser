using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TypeCollection : ItemBaseCollection<TypeItem>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TypeCollection()
        {
            TypeCollection.Key = ItemBaseCollection<TypeItem>.GetMethodKey(c => c.Key);
        }

    }

}
