
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Structures.Models
{

    public abstract class Reference<T> where T: ItemBase
    {

        public string Name { get; set; }

        public abstract T Resolve();

        internal Func<OracleDatabase> GetDb;

    }

    public class ReferenceTablespace : Reference<TablespaceModel>
    {

        public override TablespaceModel Resolve()
        {
            return GetDb().Tablespaces[this.Name];
        }

    }

    public class ReferenceTable : Reference<TableModel>
    {

        public string Owner { get; set; }

        public override TableModel Resolve()
        {
            return GetDb().Tables[$"{this.Owner}.{this.Name}"];
        }

    }

    public class ReferenceConstraint : Reference<ConstraintModel>
    {

        public string Owner { get; set; }

        public override ConstraintModel Resolve()
        {
            return GetDb().Constraints[$"{this.Owner}.{this.Name}"];
        }

    }

}
