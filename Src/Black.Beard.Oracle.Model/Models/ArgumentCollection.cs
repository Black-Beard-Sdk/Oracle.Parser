using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ArgumentCollection : IndexedCollection<ArgumentModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ArgumentCollection()
        {
            ArgumentCollection.Key = IndexedCollection<ArgumentModel>.GetMethodKey(c => c.Key);
        }

    }



}
