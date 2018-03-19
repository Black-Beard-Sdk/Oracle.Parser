using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Sequence
    /// </summary>
    public partial class SequenceCollection : IndexedCollection<SequenceModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static SequenceCollection()
        {
            SequenceCollection.Key = IndexedCollection<SequenceModel>.GetMethodKey(c => c.Key);
        }

    }



}
