﻿using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Privilege model in a grant
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{PrivilegeName}")]
    public partial class PrivilegeModel : ItemBase, Ichangable
    {

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }


        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitPrivilege(this);
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
            visitor.Alter(this, source as PrivilegeModel, propertyName);
        }

        public override string GetName()
        {
            return this.Name;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

        public override KindModelEnum KindModel
        {
            get { return KindModelEnum.Privilege; }
        }

    }

}
