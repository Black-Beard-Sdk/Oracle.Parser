using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Trigger
    /// </summary>
    public partial class TriggerCollection : ItemBaseCollection<TriggerModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TriggerCollection()
        {
            TriggerCollection.Key = ItemBaseCollection<TriggerModel>.GetMethodKey(c => c.Name);
        }

    }



}
