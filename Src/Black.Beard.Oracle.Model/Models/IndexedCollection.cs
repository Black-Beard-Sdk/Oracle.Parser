using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bb.Oracle.Models
{

    /// <summary>
    /// Base indexed collection
    /// </summary>
    public abstract class IndexedCollection : INotifyCollectionChanged
    {

        public event NotifyCollectionChangedEventHandler CollectionChanged;


        protected void AppendChanges(NotifyCollectionChangedAction kind, IList changedItems, IList oldChangedItems)
        {
            if (this.CollectionChanged != null)
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(kind, changedItems, oldChangedItems));
        }

        public abstract void Clear();

        public abstract bool Contains(string key);

        public bool IsReadOnly { get { return false; } }

        [JsonIgnore]
        public object Parent { get; internal set; }

    }

}
