﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Constraint
    /// </summary>
    public partial class ConstraintModel : ItemBase, Ichangable
    {

        public ConstraintModel()
        {
            this.Columns = new ConstraintColumnCollection() { Parent = this };

        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rel_ Constraint_ Name
        /// </summary>
        [DefaultValue("")]
        public string Rel_Constraint_Name { get; set; }

        /// <summary>
        /// Index Name
        /// </summary>
        [DefaultValue("")]
        public string IndexName { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Delete Rule
        /// </summary>
        [DefaultValue("")]
        public string DeleteRule { get; set; }

        /// <summary>
        /// Generated
        /// </summary>
        [DefaultValue("")]
        public string Generated { get; set; }

        /// <summary>
        /// Deferrable
        /// </summary>
        [DefaultValue("")]
        public string Deferrable { get; set; }

        /// <summary>
        /// Deferred
        /// </summary>
        [DefaultValue("")]
        public string Deferred { get; set; }

        /// <summary>
        /// Validated
        /// </summary>
        [DefaultValue("")]
        public string Validated { get; set; }

        /// <summary>
        /// Rely
        /// </summary>
        [DefaultValue("")]
        public string Rely { get; set; }

        /// <summary>
        /// Rel_ Constraint_ Owner
        /// </summary>
        [DefaultValue("")]
        public string Rel_Constraint_Owner { get; set; }

        /// <summary>
        /// Search_ Condition
        /// </summary>
        [DefaultValue("")]
        public string Search_Condition { get; set; }

        /// <summary>
        /// View Related
        /// </summary>
        [DefaultValue("")]
        public string ViewRelated { get; set; }

        /// <summary>
        /// Invalid
        /// </summary>
        [DefaultValue("")]
        public string Invalid { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [DefaultValue("")]
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
        public ConstraintColumnCollection Columns { get; set; } 

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

        public override void Initialize()
        {

            this.Columns.Initialize();

            if (this.Type == "F" || this.Type == "R")
            {

                string key = this.Rel_Constraint_Owner + "." + this.Rel_Constraint_Name;
                var table = this.Parent;
                if (this.Reference == null)
                {

                    ConstraintModel constraint;

                    if ((table as TableModel).Constraints.TryGet(key, out constraint))
                        this.Reference = constraint;

                }
            }

        }

        public ConstraintModel Reference { get; set; }

        public override KindModelEnum KindModel
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
