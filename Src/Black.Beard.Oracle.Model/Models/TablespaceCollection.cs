using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TablespaceCollection : ItemBaseCollection<TablespaceModel>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TablespaceCollection()
        {
            TablespaceCollection.Key = ItemBaseCollection<TablespaceModel>.GetMethodKey(c => c.TablespaceName);
        }

    }
}
