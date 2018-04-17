using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PropertyCollection : IndexedCollection<PropertyModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PropertyCollection()
        {
            PropertyCollection.Key = IndexedCollection<PropertyModel>.GetMethodKey(c => c.Name);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
