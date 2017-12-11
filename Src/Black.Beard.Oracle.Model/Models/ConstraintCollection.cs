using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Constraint
    /// </summary>
    public partial class ConstraintCollection : ItemBaseCollection<ConstraintModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ConstraintCollection()
        {
            ConstraintCollection.Key = ItemBaseCollection<ConstraintModel>.GetMethodKey(c => c.Key);
        }

    }



}
