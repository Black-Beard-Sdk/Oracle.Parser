using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FileCollection : IndexedCollection<FileElement>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static FileCollection()
        {
            FileCollection.Key = IndexedCollection<FileElement>.GetMethodKey(c => c.Path);
        }

    }



}
