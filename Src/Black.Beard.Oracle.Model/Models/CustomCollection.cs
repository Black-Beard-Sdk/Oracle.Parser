using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Bb.Oracle.Models
{

    public class CustomCollection<T> : ICollection<T>, INotifyCollectionChanged
    {


        public CustomCollection()
        {
            this._list = new List<T>();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void AppendChanges(NotifyCollectionChangedAction kind, IList changedItems, IList oldChangedItems)
        {

            if (this.CollectionChanged != null)
            {
                var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, changedItems, oldChangedItems);
                this.CollectionChanged(this, arg);
            }

        }

        public int Count => this._list.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            this._list.Add(item);
        }

        public void Clear()
        {
            this._list.Clear();
        }

        public bool Contains(T item)
        {
            return this._list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this._list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        public bool Remove(T item)
        {
            return this._list.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        private List<T> _list;

    }

}
