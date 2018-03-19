using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PropertyModel : ItemBase, Ichangable
    {


        public PropertyModel()
        {
            this.Type = new OracleType() { Parent = this };
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Inherited
        /// </summary>
        public bool Inherited { get; set; }

        /// <summary>
        /// Is Not Null
        /// </summary>
        public bool IsNotNull { get; set; }

        /// <summary>
        /// Is Array
        /// </summary>
        public bool IsArray { get; set; }

        /// <summary>
        /// Array Of Type
        /// </summary>
        public string ArrayOfType { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        /// <returns>		
        /// Objet <see cref="OracleType" />.");
        /// </returns>
        public OracleType Type { get; set; } 


        public override KindModelEnum KindModel { get { return KindModelEnum.Property; } }

        public void Alter(IchangeVisitor visitor, Ichangable source, string propertyName)
        {
            visitor.Alter(this, source as PropertyModel, propertyName);
        }

        public void Create(IchangeVisitor visitor)
        {
            visitor.Create(this);
        }

        public void Drop(IchangeVisitor visitor)
        {
            visitor.Drop(this);
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            foreach (var item in manager.Evaluate(this))
                yield return item;
        }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return (this.Parent as ItemBase).GetOwner();
        }

        
    }



}
