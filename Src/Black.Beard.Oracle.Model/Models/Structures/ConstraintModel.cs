using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Text;
using System.Linq;

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
            TableReference = new ReferenceTable() { Root = this.Root };
            Reference = new ReferenceConstraint() { Root = this.Root };
            IndexReference = new ReferenceIndex() { Root = this.Root };

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

        public ReferenceTable TableReference { get; }

        public TableModel GetTable()
        {
            return this.TableReference.Resolve();
        }

        public string UniqueKeyIndex
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.TableReference.ToString() + "(");
                foreach (ConstraintColumnModel c in this.Columns.OrderBy(c => c.ColumnName))
                {
                    sb.Append(" " + c.ColumnName);

                }
                sb.Append(")");

                switch (this.Type)
                {
                    case "C":   // (check constraint on a table)

                        break;

                    case "U":   // (unique key)
                    case "P":   // (primary key)
                        if (!string.IsNullOrEmpty(this.IndexOwner) && !string.IsNullOrEmpty(this.IndexName))
                        {
                            var index = this.Root.Indexes[$"{this.IndexOwner}.{this.IndexName}"];
                            if (index != null)
                            {

                            }
                        }
                        break;

                    case "R":   // (referential integrity)

                        if (!string.IsNullOrEmpty(this.Reference.Name))
                        {
                            sb.Append(" Reference ");

                            var _ref = this.Reference.Resolve();
                            if (_ref != null)
                                sb.Append(_ref.UniqueKeyIndex);
                        }
                        else if (!string.IsNullOrEmpty(this.Search_Condition))
                        {
                            sb.Append(GetCodeSource());
                        }
                        break;

                    case "V":   // (with check option, on a view)

                        break;

                    case "O":   // (with read only, on a view)

                        break;

                    default:
                        break;
                }

                return sb.ToString();
            }
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

            this.Columns.Root = this.Root;
            this.Reference.Root = this.Root;
            this.TableReference.Root = this.Root;
            
            this.IndexReference.Root = this.Root;
            this.IndexReference.Owner = this.IndexOwner;
            this.IndexReference.Name = this.IndexName;

            var table = this.TableReference.Resolve();
            if (table != null)
            {
                this.Columns.Parent = this.Parent = table;
                this.Columns.Initialize();
            }

        }

        public ReferenceConstraint Reference { get; }

        public ReferenceIndex IndexReference { get; }

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

            _result ^= this.TableReference?.Name?.GetHashCode() ?? 0;
            _result ^= this.TableReference?.Owner?.GetHashCode() ?? 0;

            foreach (ConstraintColumnModel item in this.Columns)
                _result ^= item.ColumnName.GetHashCode();

            _result ^= this.Deferrable.GetHashCode();
            _result ^= this.Deferred.GetHashCode();
            _result ^= this.DeleteRule?.GetHashCode() ?? 0;
            _result ^= this.Generated.GetHashCode();
            _result ^= this.IndexOwner?.GetHashCode() ?? 0;
            _result ^= this.IndexName?.GetHashCode() ?? 0;
            _result ^= this.Name.GetHashCode();
            _result ^= this.Invalid?.GetHashCode() ?? 0;
            _result ^= this.Owner?.GetHashCode() ?? 0;
            _result ^= this.Rely.GetHashCode();
            _result ^= this.Reference.Name?.GetHashCode() ?? 0;
            _result ^= this.Reference.Owner?.GetHashCode() ?? 0;
            _result ^= this.Search_Condition?.GetHashCode() ?? 0;
            _result ^= this.Status.GetHashCode();
            _result ^= this.Type.GetHashCode();
            _result ^= this.Valid.GetHashCode();
            _result ^= this.Validated.GetHashCode();
            _result ^= this.ViewRelated.GetHashCode();

            return _result;
        }

    }



}
