using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public partial class TypeItem : ItemBase, Ichangable
    {

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string SchemaName { get; set; }

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
        public string Code { get; set; }

        /// <summary>
        /// Code Body
        /// </summary>
        public string CodeBody { get; set; }

        /// <summary>
        /// Properties
        /// </summary>
        /// <returns>		
        /// Objet <see cref="PropertyCollection" />.");
        /// </returns>
        public PropertyCollection Properties { get; set; } = new PropertyCollection();

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

        public KindModelEnum KindModel
        {
            get { return KindModelEnum.Type; }
        }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetOwner()
        {
            return this.SchemaName;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            foreach (var item in manager.Evaluate(this))
                yield return item;

            foreach (PropertyModel item in this.Properties)
                foreach (var item2 in manager.Evaluate(item))
                    yield return item2;

        }

        internal void Initialize()
        {
            foreach (PropertyModel item in Properties)
            {
                item.Parent = this;
                item.Initialize();
            }

        }

        public OracleDatabase Parent { get; internal set; }

        public string GetCodeSource()
        {
            return Utils.Unserialize(this.Code, true);
        }

        public string GetCodeBodySource()
        {
            return Utils.Unserialize(this.CodeBody, true);
        }

    }



}
