using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ConstraintColumnCollection : IndexedCollection<ConstraintColumnModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ConstraintColumnCollection()
        {
            ConstraintColumnCollection.Key = IndexedCollection<ConstraintColumnModel>.GetMethodKey(c => c.ColumnName);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }



}
