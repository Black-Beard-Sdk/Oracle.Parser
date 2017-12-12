using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Constraint
    /// </summary>
    public partial class ConstraintModel : ItemBase, Ichangable
    {

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rel_ Constraint_ Name
        /// </summary>
        public string Rel_Constraint_Name { get; set; }

        /// <summary>
        /// Index Name
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Delete Rule
        /// </summary>
        public string DeleteRule { get; set; }

        /// <summary>
        /// Generated
        /// </summary>
        public string Generated { get; set; }

        /// <summary>
        /// Deferrable
        /// </summary>
        public string Deferrable { get; set; }

        /// <summary>
        /// Deferred
        /// </summary>
        public string Deferred { get; set; }

        /// <summary>
        /// Validated
        /// </summary>
        public string Validated { get; set; }

        /// <summary>
        /// Rely
        /// </summary>
        public string Rely { get; set; }

        /// <summary>
        /// Rel_ Constraint_ Owner
        /// </summary>
        public string Rel_Constraint_Owner { get; set; }

        /// <summary>
        /// Search_ Condition
        /// </summary>
        public string Search_Condition { get; set; }

        /// <summary>
        /// View Related
        /// </summary>
        public string ViewRelated { get; set; }

        /// <summary>
        /// Invalid
        /// </summary>
        public string Invalid { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ConstraintColumnCollection" />.");
        /// </returns>
        public ConstraintColumnCollection Columns { get; set; } = new ConstraintColumnCollection();

        public void Create(IchangeVisitor visitor)
        {
            visitor.Create(this);
        }

        public void Drop(IchangeVisitor visitor)
        {
            visitor.Drop(this);
        }

        public void Alter(IchangeVisitor visitor, Ichangable source, string propertyName)
        {
            visitor.Drop(this);
            visitor.Create(this);
        }


        [JsonIgnore]
        public TableModel Parent { get; set; }

        internal void Initialize()
        {

            foreach (ConstraintColumnModel item in this.Columns)
            {
                item.Parent = this;
                item.Initialize();
            }

            if (this.Type == "F" || this.Type == "R")
            {

                string key = this.Rel_Constraint_Owner + "." + this.Rel_Constraint_Name;
                var table = this.Parent;
                if (this.Reference == null)
                {

                    ConstraintModel constraint;

                    if (table.Constraints.TryGet(key, out constraint))
                        this.Reference = constraint;

                }
            }

        }

        public ConstraintModel Reference { get; set; }

        public KindModelEnum KindModel
        {
            get { return KindModelEnum.Constraint; }
        }


        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return this.Owner;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }


        public string GetCodeSource()
        {
            return Utils.Unserialize(this.Search_Condition, false);
        }

        public override string ToString()
        {
            return Key.ToString();
        }

        public override int GetHashCode()
        {

            int _result = 0;

            foreach (ConstraintColumnModel item in this.Columns)
                _result ^= item.ColumnName.GetHashCode();

            _result ^= this.Deferrable.GetHashCode();
            _result ^= this.Deferred.GetHashCode();
            _result ^= this.DeleteRule.GetHashCode();
            _result ^= this.Generated.GetHashCode();
            _result ^= this.IndexName.GetHashCode();
            _result ^= this.Name.GetHashCode();
            _result ^= this.Owner.GetHashCode();
            _result ^= this.Rely.GetHashCode();
            _result ^= this.Rel_Constraint_Name.GetHashCode();
            _result ^= this.Rel_Constraint_Owner.GetHashCode();
            _result ^= this.Search_Condition.GetHashCode();
            _result ^= this.Status.GetHashCode();
            _result ^= this.Type.GetHashCode();

            return _result;
        }

    }



}
