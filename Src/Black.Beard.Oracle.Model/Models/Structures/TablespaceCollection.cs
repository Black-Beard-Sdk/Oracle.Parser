﻿using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
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
            TablespaceCollection.Key = IndexedCollection<TablespaceModel>.GetMethodKey(c => c.Name);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            foreach (var item in this)
                item.Accept(visitor);
        }

    }
}
