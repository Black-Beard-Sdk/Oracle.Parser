using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using Bb.Oracle.Contracts;
using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{

    /// <summary>
    /// Column
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{ColumnName}")]
    public partial class ColumnModel : ItemBase
    {

        public ColumnModel()
        {
            this.ForeignKey = new ForeignKeyConstraintModel()
            {
                Parent = this
            };
            this.Type = new OracleType()
            {
                Parent = this
            };

        }

        /// <summary>
        /// Column Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nullable
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [DefaultValue("")]
        public string Description { get; set; }

        /// <summary>
        /// Is Sequence
        /// </summary>
        [DefaultValue(false)]
        public bool IsSequence { get; set; }

        /// <summary>
        /// Column Id
        /// </summary>
        public int ColumnId { get; set; }

        /// <summary>
        /// Charactere Set Name
        /// </summary>
        public string CharactereSetName { get; set; }

        /// <summary>
        /// Data Upgrated
        /// </summary>
        public bool DataUpgrated { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Is Primary Key
        /// </summary>
        [DefaultValue(false)]
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Is Computed
        /// </summary>
        [DefaultValue(false)]
        public bool IsComputed { get; set; }

        /// <summary>
        /// Char Used
        /// </summary>
        public string CharUsed { get; set; }

        /// <summary>
        /// Encryption Alg
        /// </summary>
        public string EncryptionAlg { get; set; }

        /// <summary>
        /// Salt
        /// </summary>
        public bool Salt { get; set; }

        /// Integrity Alg
        /// </summary>
        public string IntegrityAlg { get; set; }

        /// <summary>
        /// ForeignKey
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ForeignKeyConstraintModel" />.");
        /// </returns>
        public ForeignKeyConstraintModel ForeignKey { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        /// <returns>		
        /// Objet <see cref="OracleType" />.");
        /// </returns>
        public OracleType Type { get; set; }

        public override void Initialize()
        {

            this.Constraints = new List<ConstraintModel>();

            var t = this.Parent.AsTable();
            foreach (ConstraintModel c in t.Constraints.OfType<ConstraintModel>())
            {
                foreach (ConstraintColumnModel item in c.Columns)
                {
                    if (item.ColumnName == this.Name)
                        this.Constraints.Add(c);
                }
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
            visitor.Alter(this, source as ColumnModel, propertyName);
        }

        public override string GetOwner() { return (this.Parent as ItemBase).GetOwner(); }

        public override string GetName() { return this.Name; }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

        [JsonIgnore]
        public List<ConstraintModel> Constraints { get; private set; }

        public override KindModelEnum KindModel
        {
            get { return KindModelEnum.Column; }
        }

    }

}
