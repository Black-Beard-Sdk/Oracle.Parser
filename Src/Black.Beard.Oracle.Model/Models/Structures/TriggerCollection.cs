using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Trigger
    /// </summary>
    public partial class TriggerCollection : IndexedCollection<TriggerModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TriggerCollection()
        {
            TriggerCollection.Key = IndexedCollection<TriggerModel>.GetMethodKey(c => c.Key);
        }

    }



}
