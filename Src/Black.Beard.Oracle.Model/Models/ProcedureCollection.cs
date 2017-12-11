using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcedureCollection : ItemBaseCollection<ProcedureModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ProcedureCollection()
        {
            ProcedureCollection.Key = ItemBaseCollection<ProcedureModel>.GetMethodKey(c => c.Key);
        }

    }

}
