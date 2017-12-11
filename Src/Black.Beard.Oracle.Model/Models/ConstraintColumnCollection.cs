using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ConstraintColumnCollection : ItemBaseCollection<ConstraintColumnModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ConstraintColumnCollection()
        {
            ConstraintColumnCollection.Key = ItemBaseCollection<ConstraintColumnModel>.GetMethodKey(c => c.ColumnName);
        }

    }



}
