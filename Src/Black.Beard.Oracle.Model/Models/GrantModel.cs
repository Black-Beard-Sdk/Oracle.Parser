﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Oracle.Models
{

    [System.Diagnostics.DebuggerDisplay("grant {PrivilegesToText} {FullObjectName} to {Role}")]
    public partial class GrantModel : ItemBase, Ichangable
    {

        public GrantModel()
        {

            this.Privileges = new PrivilegeCollection() { Parent = this };

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
            visitor.Alter(this, source as GrantModel, propertyName);
        }

        public override KindModelEnum KindModel
        {
            get { return KindModelEnum.Grant; }
        }

        public override string GetName()
        {
            return this.ObjectName;
        }

        public override string GetOwner()
        {
            return this.ObjectSchema;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Object Name
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Privilege
        /// </summary>
        public PrivilegeCollection Privileges { get; set; }

        /// <summary>
        /// Grantable
        /// </summary>
        public bool Grantable { get; set; }

        /// <summary>
        /// Hierarchy
        /// </summary>
        public bool Hierarchy { get; set; }

        /// <summary>
        /// Object Schema
        /// </summary>
        public string ObjectSchema { get; set; }

        /// <summary>
        /// Full Object Name
        /// </summary>
        public string FullObjectName { get; set; }

        /// <summary>
        /// Column Object Name
        /// </summary>
        public string ColumnObjectName { get; set; }


        public string PrivilegesToText { get { return string.Join(", ", this.Privileges); } }

    }
}
