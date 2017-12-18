using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TablespaceCollection : IndexedCollection<TablespaceModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TablespaceCollection()
        {
            TablespaceCollection.Key = IndexedCollection<TablespaceModel>.GetMethodKey(c => c.TablespaceName);
        }

    }
}
