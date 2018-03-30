using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Trigger
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{TriggerName}")]
    public partial class TriggerModel : ItemBase, Ichangable
    {

        public TriggerModel()
        {
            TableReference = new ReferenceTable() { GetDb = () => this.Root };
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Trigger Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base Object Type
        /// </summary>
        public string BaseObjectType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Trigger Status
        /// </summary>
        public string TriggerStatus { get; set; }

        /// <summary>
        /// Trigger Type
        /// </summary>
        public string TriggerType { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Action Type
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        public ReferenceTable TableReference { get; set; }

        public override void Initialize()
        {
            TableReference.GetDb = () => this.Root;
        }

        public void Create(IchangeVisitor visitor)
        {
            visitor.Create(this);
        }

        public string BuildKey()
        {
            return $"{this.TableReference.Owner}.{this.Name}";
        }

        public void Drop(IchangeVisitor visitor)
        {
            visitor.Drop(this);
        }

        public void Alter(IchangeVisitor visitor, Ichangable source, string propertyName)
        {
            visitor.Alter(this, source as TriggerModel, propertyName);
        }

        public override KindModelEnum KindModel
        {
            get { return KindModelEnum.Trigger; }
        }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return this.TableReference.Owner;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

        public string GetCodeSource()
        {
            return Utils.Unserialize(this.Code, true);
        }


    }



}
