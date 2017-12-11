using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Sequence
    /// </summary>
    public partial class SequenceCollection : ItemBaseCollection<SequenceModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static SequenceCollection()
        {
            SequenceCollection.Key = ItemBaseCollection<SequenceModel>.GetMethodKey(c => c.Name);
        }

    }



}
