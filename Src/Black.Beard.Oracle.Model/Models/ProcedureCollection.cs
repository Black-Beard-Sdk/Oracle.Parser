using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcedureCollection : IndexedCollection<ProcedureModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ProcedureCollection()
        {
            ProcedureCollection.Key = IndexedCollection<ProcedureModel>.GetMethodKey(c => c.Key);
        }

    }

}
