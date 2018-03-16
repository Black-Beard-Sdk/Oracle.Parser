using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bb.Oracle.Models
{

    public class IndexedCollection<T> : IndexedCollection, IEnumerable<T>, ICollection<T>
    {

        /// <summary>
        /// return the item specified by this index in the collection
        /// </summary>
        /// <param name="index">as a System.String type</param>
        public T this[string key]
        {
            get
            {

                T e;
                if (this._datas.TryGetValue(key, out e))
                    return e;
                return default(T);
            }
            set
            {
                if (this._datas.ContainsKey(key))
                {

                    this._datas[key] = value;

                    var v = value as ItemBase;
                    if (v != null)
                        v.Parent = this.Parent;

                }
                else
                    AddOrUpdate(value);
            }
        }

        public bool AddIfNotExist(T model)
        {
            return AddIfNotExist(Key(model), model);
        }

        public bool AddIfNotExist(string key, T model)
        {

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("message", nameof(key));

            if (!this._datas.ContainsKey(key))
            {
                Add(model, key);
                return true;
            }

            return false;

        }

        public int Count { get { return this._datas.Count; } }

        public bool TryGet(string key, out T model)
        {
            return this._datas.TryGetValue(key, out model);
        }

        public void AddOrUpdate(T item, string key = null)
        {
            if (key == null)
                key = Key(item);

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (!this._datas.ContainsKey(key))
            {
                this._datas.Add(key, item);
                var v = item as ItemBase;
                if (v != null)
                    v.Parent = this.Parent;
            }
            else
            {
                var old = this._datas[key];
                this._datas[key] = item;
                var v = item as ItemBase;
                if (v != null)
                    v.Parent = this.Parent;
                base.AppendChanges(System.Collections.Specialized.NotifyCollectionChangedAction.Replace, new List<T>() { item }, new List<T>() { old });
            }
        }

        public void Add(T item)
        {

            string key = Key(item);

            Add(item, key);

        }

        public void Add(T item, string key)
        {

            if (!this._datas.ContainsKey(key))
            {
                this._datas.Add(key, item);

                base.AppendChanges(System.Collections.Specialized.NotifyCollectionChangedAction.Add, new List<T>() { item }, this.EmptyList);

                var v = item as ItemBase;
                if (v != null)
                    v.Parent = this.Parent;

            }
            else
                throw new DuplicateWaitObjectException(key);

        }

        public void AddRangeOrReplace(IEnumerable<T> items)
        {

            var count = items.Count();
            var newItem = new List<T>(count);
            var oldItem = new List<T>(count);
            var replacedItem = new List<T>(count);

            foreach (var item in items)
            {

                string key = Key(item);

                if (this._datas.ContainsKey(key))
                {
                    this._datas.Add(key, item);
                    newItem.Add(item);
                }
                else
                {
                    oldItem.Add(this._datas[key]);
                    this._datas[key] = item;
                    newItem.Add(item);

                }

                var v = item as ItemBase;
                if (v != null)
                    v.Parent = this.Parent;

            }

            if (newItem.Count > 0)
                base.AppendChanges(System.Collections.Specialized.NotifyCollectionChangedAction.Add, newItem, this.EmptyList);

            if (replacedItem.Count > 0)
                base.AppendChanges(System.Collections.Specialized.NotifyCollectionChangedAction.Replace, replacedItem, oldItem);

        }


        public IEnumerator<T> GetEnumerator()
        {
            return this._datas.Values.OrderBy(c => Key(c)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._datas.Values.OrderBy(c => Key(c)).GetEnumerator();
        }

        protected static Func<T, string> GetMethodKey(Expression<Func<T, string>> expression)
        {
            return expression.Compile();
        }

        public override void Clear()
        {
            base.AppendChanges(System.Collections.Specialized.NotifyCollectionChangedAction.Reset, this.EmptyList, this.EmptyList);
            this._datas.Clear();
        }

        public bool Contains(T item)
        {
            return this._datas.ContainsValue(item);
        }

        public override bool Contains(string key)
        {
            return this._datas.ContainsKey(key);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int length = array.Length;
            var a = this._datas.Values.Skip(arrayIndex).Take(length).ToArray();
            for (int i = 0; i < length; i++)
                array[i] = a[i];
        }

        public bool Remove(T item)
        {
            string key = Key(item);
            base.AppendChanges(System.Collections.Specialized.NotifyCollectionChangedAction.Remove, this.EmptyList, new List<T>() { item });
            return this._datas.Remove(key);
        }

        public void Initialize()
        {

            foreach (T item in this)
            {

                var p = item as ItemBase;

                if (p != null)
                {
                    p.Parent = this.Parent;
                    p.Initialize();
                }

            }


        }

        private Dictionary<string, T> _datas = new Dictionary<string, T>();
        protected static Func<T, string> Key;
        private readonly IList EmptyList = new List<T>() { };
    }

}
