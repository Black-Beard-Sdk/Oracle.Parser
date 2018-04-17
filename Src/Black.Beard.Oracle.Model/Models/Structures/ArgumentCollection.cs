using System.Collections.Generic;
using Bb.Oracle.Contracts;

namespace Bb.Oracle.Structures.Models
{

    /// <summary>
    /// 
    /// </summary>
    public partial class ArgumentCollection : IndexedCollection<ArgumentModel>
    {

        public ArgumentCollection()
        {

        }

        /// <summary>
        /// Ctor
        /// </summary>
        static ArgumentCollection()
        {
            ArgumentCollection.Key = IndexedCollection<ArgumentModel>.GetMethodKey(c => c.Key);
        }

        public override void Accept(IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }

}
