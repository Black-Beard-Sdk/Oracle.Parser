using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }



}
