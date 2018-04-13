
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
            return _item ?? (_item = Resolve(GetDb()));
        }

        protected abstract T Resolve(OracleDatabase oracleDatabase);

        public void Set(T item)
        {
            _item = item;
        }

        internal Func<OracleDatabase> GetDb;

    }

    public class ReferenceTablespace : Reference<TablespaceModel>
    {

        protected override TablespaceModel Resolve(OracleDatabase db)
        {
            return db.Tablespaces[this.Name];
        }

    }

    public class ReferenceTable : Reference<TableModel>
    {

        public string Owner { get; set; }

        protected override TableModel Resolve(OracleDatabase db)
        {
            return db.Tables[$"{this.Owner}.{this.Name}"];
        }

    }

    public class ReferenceConstraint : Reference<ConstraintModel>
    {

        public string Owner { get; set; }

        protected override ConstraintModel Resolve(OracleDatabase db)
        {
            return db.Constraints[$"{this.Owner}.{this.Name}"];
        }

    }

}
