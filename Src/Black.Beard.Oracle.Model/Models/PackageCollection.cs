using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Package
    /// </summary>
    public partial class PackageCollection : ItemBaseCollection<PackageModel>
    {

        static PackageCollection()
        {
            PackageCollection.Key = ItemBaseCollection<PackageModel>.GetMethodKey(c => c.Name);
        }

    }
}
