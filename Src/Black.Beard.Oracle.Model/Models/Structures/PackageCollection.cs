using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Package
    /// </summary>
    public partial class PackageCollection : IndexedCollection<PackageModel>
    {

        static PackageCollection()
        {
            PackageCollection.Key = IndexedCollection<PackageModel>.GetMethodKey(c => c.Key);
        }

    }
}
