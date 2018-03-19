using System.Collections.Generic;
using System.Linq;

namespace Bb.Oracle.Structures.Models
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

        public override string ToString()
        {
            return string.Join(", ", this.Select(c => c.Path));
        }

    }

    

}
