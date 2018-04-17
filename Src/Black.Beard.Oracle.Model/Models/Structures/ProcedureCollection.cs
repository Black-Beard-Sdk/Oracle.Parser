using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
