using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FileCollection : ItemBaseCollection<FileElement>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static FileCollection()
        {
            FileCollection.Key = ItemBaseCollection<FileElement>.GetMethodKey(c => c.Path);
        }

    }



}
