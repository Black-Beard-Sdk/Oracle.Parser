using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bb.Oracle.Models
{

    public class ItemBaseCollection<T> : IEnumerable<T>, ICollection<T>
    {

        /// <summary>
        /// return the item specified by this index in the collection
        /// </summary>
        /// <param name="index">as a System.String type</param>
        public T this[string key]
        {
            get
            {
                return (T)this._datas[key];
            }
            set
            {
                if (this._datas.ContainsKey(key))
                    this._datas[key] = value;
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

        public bool IsReadOnly { get { return false; } }

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
                this._datas.Add(key, item);
            else
                this._datas[key] = item;

        }

        public void Add(T item)
        {

            string key = Key(item);

            Add(item, key);

        }

        public void Add(T item, string key)
        {

            if (!this._datas.ContainsKey(key))
                this._datas.Add(key, item);
            else
                throw new DuplicateWaitObjectException(key);

        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {

                string key = Key(item);

                if (this._datas.ContainsKey(key))
                    this._datas.Add(key, item);
                else
                    this._datas[key] = item;

            }

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

        public void Clear()
        {
            this._datas.Clear();
        }

        public bool Contains(T item)
        {
            return this._datas.ContainsValue(item);
        }

        public bool Contains(string key)
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
            return this._datas.Remove(key);
        }

        private Dictionary<string, T> _datas = new Dictionary<string, T>();
        protected static Func<T, string> Key;

    }

}
