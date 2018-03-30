using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Oracle.Structures.Models
{

    [System.Diagnostics.DebuggerDisplay("grant {PrivilegesToText} ON {FullObjectName} TO {Role}")]
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
            get { return KindModelEnum.UserObjectPrivilege; }
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
        /// Object Schema
        /// </summary>
        public string ObjectSchema { get; set; }

        /// <summary>
        /// Object Name
        /// </summary>
        public string ObjectName { get; set; }


        /// <summary>
        /// Full Object Name
        /// </summary>
        public string FullObjectName
        {
            get
            {
                if (string.IsNullOrEmpty(this._fullObjectName))
                {

                    this._fullObjectName = !string.IsNullOrEmpty(this.ColumnObjectName)
                                ? this.ColumnObjectName
                                : string.Empty;

                    if (!string.IsNullOrEmpty(this.ObjectName))
                    {
                        if (!string.IsNullOrEmpty(_fullObjectName))
                            _fullObjectName = "." + _fullObjectName;

                        _fullObjectName = ObjectName + _fullObjectName;
                    }

                    if (!string.IsNullOrEmpty(this.ObjectSchema))
                    {
                        if (!string.IsNullOrEmpty(_fullObjectName))
                            _fullObjectName = "." + _fullObjectName;
                        _fullObjectName = @"""" + this.ObjectSchema + @"""" + _fullObjectName;
                    }
                }

                return this._fullObjectName;
            }
        }

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
        /// Column Object Name
        /// </summary>
        public string ColumnObjectName { get; set; }


        public string PrivilegesToText { get { return string.Join(", ", this.Privileges.OfType<PrivilegeModel>().Select(c => c.Name).ToArray()); } }


        private string _fullObjectName;


    }
}
