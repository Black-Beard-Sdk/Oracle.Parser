using Bb.Oracle.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bb.Oracle.Structures.Models
{

    /// <summary>
    /// Base indexed collection
    /// </summary>
    public abstract class IndexedCollection : INotifyCollectionChanged
    {


        public IndexedCollection()
        {


        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void AppendChanges(NotifyCollectionChangedAction kind, IList changedItems, IList oldChangedItems)
        {

            var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, changedItems, oldChangedItems);

            if (this.CollectionChanged != null)
                this.CollectionChanged(this, arg);

            var root = this.Root;
            if (root != null)
                this.Root.Changes(this, arg);

        }

        public abstract void Accept(Contracts.IOracleModelVisitor visitor);

        public abstract void Clear();

        public abstract bool Contains(string key);

        public bool IsReadOnly { get { return false; } }
        
        [JsonIgnore]
        public object Parent { get; internal set; }

        [JsonIgnore]
        public OracleDatabase Root { get; internal set; }


    }

}
