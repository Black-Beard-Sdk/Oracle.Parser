using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }


    }

}
