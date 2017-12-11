using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ArgumentCollection : ItemBaseCollection<ArgumentModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ArgumentCollection()
        {
            ArgumentCollection.Key = ItemBaseCollection<ArgumentModel>.GetMethodKey(c => c.Key);
        }

    }



}
