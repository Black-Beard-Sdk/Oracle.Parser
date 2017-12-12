using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Column
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{ColumnName}")]
    public partial class ColumnModel : ItemBase
    {

        /// <summary>
        /// Column Name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Nullable
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Is Sequence
        /// </summary>
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
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Is Computed
        /// </summary>
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
        /// Objet <see cref="ForeignKeyConstraints" />.");
        /// </returns>
        public ForeignKeyConstraints ForeignKey { get; set; } = new ForeignKeyConstraints();

        /// <summary>
        /// Type
        /// </summary>
        /// <returns>		
        /// Objet <see cref="OracleType" />.");
        /// </returns>
        public OracleType Type { get; set; }

        [JsonIgnore]
        public TableModel Parent { get; set; }


        internal void Initialize()
        {

            this.Constraints = new List<ConstraintModel>();

            foreach (ConstraintModel c in this.Parent.Constraints.OfType<ConstraintModel>())
            {
                foreach (ConstraintColumnModel item in c.Columns)
                {
                    if (item.ColumnName == this.ColumnName)
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

        public override string GetOwner()
        {
            return this.Parent.Name;
        }

        public override string GetName()
        {
            return this.ColumnName;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

        public List<ConstraintModel> Constraints { get; private set; }

        public KindModelEnum KindModel
        {
            get { return KindModelEnum.Column; }
        }

    }

}
