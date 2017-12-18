using System.Collections.Generic;

namespace Bb.Oracle.Models
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

    }



}
