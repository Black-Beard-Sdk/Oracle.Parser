
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Structures.Models
{

    public abstract class Reference<T> where T : ItemBase
    {

        private T _item;

        public string Name { get; set; }

        public T Resolve()
        {
            return _item ?? (_item = Resolve(Root));
        }

        protected abstract T Resolve(OracleDatabase oracleDatabase);

        public void Set(T item)
        {
            _item = item;
        }

        [JsonIgnore]
        public OracleDatabase Root { get; internal set; }

        //internal Func<OracleDatabase> GetDb;

    }

    public class ReferenceTablespace : Reference<TablespaceModel>
    {

        protected override TablespaceModel Resolve(OracleDatabase db)
        {
            return db.Tablespaces[this.Name];
        }

        public override string ToString()
        {
            return this.Name;
        }

    }

    public class ReferenceTable : Reference<TableModel>
    {

        public ReferenceTable()
        {

        }

        public string Owner { get; set; }

        protected override TableModel Resolve(OracleDatabase db)
        {
            return db.Tables[$"{this.Owner}.{this.Name}"];
        }

        public override string ToString()
        {
            return $"{this.Owner}.{this.Name}";
        }

    }

    public class ReferenceIndex : Reference<IndexModel>
    {

        public string Owner { get; set; }

        protected override IndexModel Resolve(OracleDatabase db)
        {
            return db.Indexes[$"{this.Owner}.{this.Name}"];
        }

        public override string ToString()
        {
            return $"{this.Owner}.{this.Name}";
        }

    }

    public class ReferenceConstraint : Reference<ConstraintModel>
    {

        public string Owner { get; set; }

        protected override ConstraintModel Resolve(OracleDatabase db)
        {
            if (!string.IsNullOrEmpty(this.Name))
                return db.Constraints[$"{this.Owner}.{this.Name}"];
            return null;
        }

        public override string ToString()
        {
            return $"{this.Owner}.{this.Name}";
        }

    }

}
