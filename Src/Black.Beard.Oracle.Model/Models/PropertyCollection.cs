﻿using System.Collections.Generic;

namespace Bb.Oracle.Models
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

    }

}