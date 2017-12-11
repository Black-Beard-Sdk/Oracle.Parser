using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Package
    /// </summary>
    public partial class PackageModel : ItemBase
    {

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Code Body
        /// </summary>
        public string CodeBody { get; set; }

        /// <summary>
        /// Package Owner
        /// </summary>
        public string PackageOwner { get; set; }

        /// <summary>
        /// Package Name
        /// </summary>
        public string PackageName { get; set; }


        public PackageModel()
        {

        }

        public PackageModel(string name)
        {

            if (name.StartsWith("#"))
            {
                name = name.Substring(1);
                Excluded = true;
            }

            this.Name = name;

        }

        public bool Excluded { get; set; }

        public static implicit operator PackageModel(string packageName)
        {
            return new PackageModel(packageName);
        }

        public byte[] Write()
        {
            return System.Text.Encoding.UTF8.GetBytes(ToString() + "\r\n");
        }

        public override string ToString()
        {
            return (Excluded ? "#" : string.Empty) + Name;
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
            visitor.Alter(this, source as PackageModel, propertyName);
        }

        public KindModelEnum KindModel
        {
            get { return KindModelEnum.Package; }
        }


        public override string GetName()
        {
            return this.PackageName;
        }

        public override string GetOwner()
        {
            return this.PackageOwner;
        }

        public IEnumerable<Anomaly> Evaluate(IEvaluateManager manager)
        {
            return manager.Evaluate(this);
        }

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
