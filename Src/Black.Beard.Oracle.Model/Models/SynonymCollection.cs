using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Synonym
    /// </summary>
    public partial class SynonymCollection : IndexedCollection<SynonymModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static SynonymCollection()
        {
            SynonymCollection.Key = IndexedCollection<SynonymModel>.GetMethodKey(c => c.Key);
        }

    }



}
