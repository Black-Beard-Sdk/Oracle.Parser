using System.Collections.Generic;

namespace Bb.Oracle.Models
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
            SequenceCollection.Key = IndexedCollection<SequenceModel>.GetMethodKey(c => c.Name);
        }

    }



}
