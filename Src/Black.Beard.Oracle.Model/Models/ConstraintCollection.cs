using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Constraint
    /// </summary>
    public partial class ConstraintCollection : IndexedCollection<ConstraintModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ConstraintCollection()
        {
            ConstraintCollection.Key = IndexedCollection<ConstraintModel>.GetMethodKey(c => c.Key);
        }

    }



}
