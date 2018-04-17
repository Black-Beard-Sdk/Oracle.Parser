using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }



}
