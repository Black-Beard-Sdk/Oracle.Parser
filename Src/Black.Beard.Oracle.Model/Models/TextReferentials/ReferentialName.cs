using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.TextReferentials
{

    public class ReferentialNames
    {

        public ReferentialNames()
        {
            this._dic = new Dictionary<string, ReferentialName>();
        }

        public void Add(ItemBase item)
        {

            string key = item.GetName();

            if (!this._dic.TryGetValue(key, out ReferentialName r))
                this._dic.Add(key, r = new ReferentialName(key));

            r.Append(item);



        }

        public void Remove(ItemBase item)
        {

            string key = item.GetName();

            if (this._dic.TryGetValue(key, out ReferentialName r))
            {
                r.Remove(item);
                if (r.IsEmpty)
                    this._dic.Remove(key);
            }

        }

        private readonly Dictionary<string, ReferentialName> _dic;

    }

    public class ReferentialName
    {

        public ReferentialName(string name)
        {
            this.Name = name;
            this._objects = new List<object>();
        }

        public string Name { get; }

        private readonly List<object> _objects;

        public void Append(ItemBase item)
        {
            this._objects.Add(item);
        }

        public void Remove(ItemBase item)
        {
            this._objects.Remove(item);
        }

        public IEnumerable<object> Items { get => this._objects; }
        public bool IsEmpty { get => this._objects.Count == 0; }

    }


}
