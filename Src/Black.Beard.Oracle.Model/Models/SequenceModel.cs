using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Sequence
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{SequenceName}")]
    public partial class SequenceModel : ItemBase, Ichangable
    {

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Sequence Name
        /// </summary>
        public string SequenceName { get; set; }

        /// <summary>
        /// Min Value
        /// </summary>
        public System.Int64 MinValue { get; set; }

        /// <summary>
        /// Max Value
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// Increment By
        /// </summary>
        public int IncrementBy { get; set; }

        /// <summary>
        /// Cycle Flag
        /// </summary>
        public bool CycleFlag { get; set; }

        /// <summary>
        /// Order Flag
        /// </summary>
        public bool OrderFlag { get; set; }

        /// <summary>
        /// Cache Size
        /// </summary>
        public int CacheSize { get; set; }

        /// <summary>
        /// Last Number
        /// </summary>
        public System.Int64 LastNumber { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Generated
        /// </summary>
        public bool Generated { get; set; }

        public bool Temporary { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Parsed
        /// </summary>
        public bool Parsed { get; set; }

        /// <summary>
        /// Max Value Is Specified
        /// </summary>
        public bool MaxValueIsSpecified { get; set; }


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
            visitor.Alter(this, source as SequenceModel, propertyName);
        }

        public KindModelEnum KindModel
        {
            get { return KindModelEnum.Sequence; }
        }

        public override string GetName()
        {
            return SequenceName;
        }

        public override string GetOwner()
        {
            return this.Owner;
        }

        internal void Initialize()
        {

        }

        [JsonIgnore]
        public OracleDatabase Parent { get; internal set; }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }


    }

}
