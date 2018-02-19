using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Privilege model in a grant
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{PrivilegeName}")]
    public partial class PrivilegeModel : ItemBase, Ichangable
    {

        /// <summary>
        /// Column Name
        /// </summary>
        public string PrivilegeName { get; set; }
              
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
            return this.PrivilegeName;
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
