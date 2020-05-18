using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Text;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Constraint
    /// </summary>
    public partial class ConstraintModel : ItemBase, Ichangable
    {

        public ConstraintModel()
        {
            this.Columns = new ConstraintColumnCollection() { Parent = this };
            TableReference = new ReferenceTable() { GetDb = () => this.Root };
            Reference = new ReferenceConstraint() { GetDb = () => this.Root };

            Status = "ENABLE";
            Deferred = "IMMEDIATE";
            Rely = "RELY";
            Validated = "VALIDATE";

        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitConstraint(this);
            this.Columns.Accept(visitor);
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Index Name
        /// </summary>
        [DefaultValue("")]
        public string IndexOwner { get; set; }

        /// <summary>
        /// Index Name
        /// </summary>
        [DefaultValue("")]
        public string IndexName { get; set; }

        /// <summary>
        /// Constraint type
        ///     C (check constraint on a table)
        ///     P(primary key)
        ///     U(unique key)
        ///     R(referential integrity)
        ///     V(with check option, on a view)
        ///     O(with read only, on a view)
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
        /// Key
        /// </summary>
        public string Key { get; set; }

        public bool IsGeneratedName { get => Name.ToUpper().StartsWith("C_SYS_") || Name.ToUpper().StartsWith("SYS_"); }

        public string BuildKey()
        {

            if (string.IsNullOrEmpty(this.Name) || this.Name.StartsWith("C_SYS_"))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(this.Type);

                sb.Append(":");
                sb.Append(this.Owner);

                sb.Append(":");
                sb.Append(this.TableReference.Owner);

                sb.Append(":");
                sb.Append(this.TableReference.Name);

                foreach (var item in this.Columns)
                {
                    sb.Append(":");
                    sb.Append(item.ColumnName);
                }

                this.Name = "C_SYS_" + Crc32.Calculate(sb.ToString()).ToString();
            }

            return this.Owner + "." + this.Name;

        }

        /// <summary>
        /// Columns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ConstraintColumnCollection" />.");
        /// </returns>
        public ConstraintColumnCollection Columns { get; set; }

        public ReferenceTable TableReference { get; set; }

        public TableModel GetTable()
        {
            return this.TableReference.Resolve();
        }

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

            TableReference.GetDb = () => this.Root;
            Reference.GetDb = () => this.Root;
            this.Columns.Initialize();

            //if (this.Type == "F" || this.Type == "R")
            //{
            //    string key = this.Rel_Constraint_Owner + "." + this.Rel_Constraint_Name;
            //    var table = this.Parent;
            //    if (this.Reference == null)
            //    {
            //        if (this.Root.Constraints.TryGet(key, out ConstraintModel constraint))
            //            this.Reference = constraint;
            //    }
            //}

        }

        ///// <summary>
        ///// Rel_Constraint_Owner
        ///// </summary>
        //[DefaultValue("")]
        //public string Rel_Constraint_Owner { get; set; }

        ///// <summary>
        ///// Rel_ Constraint_ Name
        ///// </summary>
        //[DefaultValue("")]
        //public string Rel_Constraint_Name { get; set; }

        public ReferenceConstraint Reference { get; set; }

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
            _result ^= this.Reference.Name.GetHashCode();
            _result ^= this.Reference.Owner.GetHashCode();
            _result ^= this.Search_Condition.GetHashCode();
            _result ^= this.Status.GetHashCode();
            _result ^= this.Type.GetHashCode();

            return _result;
        }

    }



}
