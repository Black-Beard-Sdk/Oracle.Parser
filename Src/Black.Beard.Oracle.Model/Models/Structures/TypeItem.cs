using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public partial class TypeItem : ItemBase, Ichangable
    {

        public TypeItem()
        {
            this.Properties = new PropertyCollection() { Parent = this };
            this.Code = new CodeModel();
            this.CodeBody = new CodeModel();
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitType(this);
            this.Properties.Accept(visitor);
        }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Super Type
        /// </summary>
        public string SuperType { get; set; }

        /// <summary>
        /// Package Name
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Type Code
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// Collection Schema Name
        /// </summary>
        public string CollectionSchemaName { get; set; }

        /// <summary>
        /// Collection Type Name
        /// </summary>
        public string CollectionTypeName { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public CodeModel Code { get; set; }

        /// <summary>
        /// Code Body
        /// </summary>
        public CodeModel CodeBody { get; set; }

        /// <summary>
        /// Properties
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PropertyCollection" />.");
        /// </returns>
        public PropertyCollection Properties { get; set; }

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
            visitor.Alter(this, source as TypeItem, propertyName);
        }

        public override KindModelEnum KindModel
        {
            get { return KindModelEnum.Type; }
        }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return this.Owner;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            foreach (var item in manager.Evaluate(this))
                yield return item;

            foreach (PropertyModel item in this.Properties)
                foreach (var item2 in manager.Evaluate(item))
                    yield return item2;

        }

        public override void Initialize()
        {
            foreach (PropertyModel item in Properties)
            {
                item.Root = this.Root;
                item.Parent = this;
                item.Initialize();
            }

        }

        
    }



}
