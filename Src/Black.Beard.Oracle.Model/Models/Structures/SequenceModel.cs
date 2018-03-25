using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Sequence
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{SequenceName}")]
    public partial class SequenceModel : ItemBase, Ichangable
    {

        /// <summary>
        /// Instance with default values
        /// </summary>
        public readonly static SequenceModel Default = new SequenceModel()
        {
            // Defaults values
            MinValue = "1",
            MaxValue = Constants.DefaultValues.MaxValueSequence,
            IncrementBy = 1,
            CycleFlag = false,
            OrderFlag = false,
            CacheSize = 20,
            Keep = false,
            Session = false,
        };
        public SequenceModel()
        {
            var _default = SequenceModel.Default;
            if (_default != null)
            {
                // Defaults values
                this.MinValue = _default.MinValue;
                this.MaxValue = _default.MaxValue;
                this.IncrementBy = _default.IncrementBy;
                this.CycleFlag = _default.CycleFlag;
                this.OrderFlag = _default.OrderFlag;
                this.CacheSize = _default.CacheSize;
                this.Keep = _default.Keep;
                this.Session = _default.Session;
            }
        }

        /// <summary>
        /// unique key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Owner of the sequence
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Sequence Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specify the minimum value of the sequence. This integer value can have
        /// 28 or fewer digits for positive values and 27 or fewer digits for negative values.
        /// MINVALUE must be less than or equal to START WITH and must be less than MAXVALUE.
        /// </summary>
        public string MinValue { get; set; }

        /// <summary>
        /// Specify the maximum value the sequence can generate. This integer
        /// value can have 28 or fewer digits for positive values and 27 or fewer digits for negative
        /// values.MAXVALUE must be equal to or greater than START WITH and must be greater than
        /// MINVALUE.
        /// </summary>
        public string MaxValue { get; set; }

        /// <summary>
        /// Specify the interval between sequence numbers. This integer value
        /// can be any positive or negative integer, but it cannot be 0. This value can have 28 or
        /// fewer digits for an ascending sequence and 27 or fewer digits for a descending
        /// sequence.The absolute of this value must be less than the difference of MAXVALUE and
        /// MINVALUE.If this value is negative, then the sequence descends. If the value is positive,
        /// then the sequence ascends. If you omit this clause, then the interval defaults to 1
        /// </summary>
        public int IncrementBy { get; set; }

        /// <summary>
        /// Specify CYCLE to indicate that the sequence continues to generate values after
        /// reaching either its maximum or minimum value.After an ascending sequence reaches
        /// its maximum value, it generates its minimum value.After a descending sequence
        /// reaches its minimum, it generates its maximum value.
        /// </summary>
        public bool CycleFlag { get; set; }

        /// <summary>
        /// Specify ORDER to guarantee that sequence numbers are generated in order of
        /// request.This clause is useful if you are using the sequence numbers as timestamps.
        /// Guaranteeing order is usually not important for sequences used to generate primary
        /// keys.
        /// ORDER is necessary only to guarantee ordered generation if you are using Oracle Real
        /// Application Clusters.If you are using exclusive mode, then sequence numbers are
        /// always generated in order.
        /// </summary>
        public bool OrderFlag { get; set; }

        /// <summary>
        /// Specify how many values of the sequence the database preallocates and
        /// keeps in memory for faster access.This integer value can have 28 or fewer digits.The
        /// minimum value for this parameter is 2. For sequences that cycle, this value must be
        /// less than the number of values in the cycle. You cannot cache more values than will fit
        /// in a given cycle of sequence numbers. Therefore, the maximum value allowed for
        /// CACHE must be less than the value determined by the following formula:
        /// (CEIL (MAXVALUE - MINVALUE)) / ABS(INCREMENT)
        /// If a system failure occurs, then all cached sequence values that have not been used in
        /// committed DML statements are lost.The potential number of lost values is equal to the
        /// value of the CACHE parameter.
        /// </summary>
        public int CacheSize { get; set; }

        /// <summary>
        /// Specify KEEP if you want NEXTVAL to retain its original value during replay for
        /// Application Continuity.This behavior will occur only if the user running the
        /// application is the owner of the schema containing the sequence. This clause is useful
        /// for providing bind variable consistency at replay after recoverable errors.Refer to
        /// Oracle Database Development Guide for more information on Application Continuity
        /// </summary>
        public bool Keep { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Generated
        /// </summary>
        public bool Generated { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        ///// <summary>
        ///// Parsed
        ///// </summary>
        //public bool Parsed { get; set; }

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

        public override KindModelEnum KindModel { get { return KindModelEnum.Sequence; } }

        public bool Session { get; set; }

        public override string GetName() { return Name; }

        public override string GetOwner() { return this.Owner; }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }


    }

}
