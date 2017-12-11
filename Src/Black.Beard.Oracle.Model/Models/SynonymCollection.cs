using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Synonym
    /// </summary>
    public partial class SynonymCollection : ItemBaseCollection<SynonymModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static SynonymCollection()
        {
            SynonymCollection.Key = ItemBaseCollection<SynonymModel>.GetMethodKey(c => c.Key);
        }

    }



}
