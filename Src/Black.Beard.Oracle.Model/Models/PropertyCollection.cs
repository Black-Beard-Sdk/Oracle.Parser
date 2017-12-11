using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PropertyCollection : ItemBaseCollection<PropertyModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static PropertyCollection()
        {
            PropertyCollection.Key = ItemBaseCollection<PropertyModel>.GetMethodKey(c => c.Name);
        }

    }

}
