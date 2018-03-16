using Bb.Oracle.Contracts;
using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Package
    /// </summary>
    public partial class PackageModel : ItemBase, Ichangable
    {


        public PackageModel()
        {
            this.Code = new CodeModel();
            this.CodeBody = new CodeModel();
        }
        

        /// <summary>
        /// Name
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public CodeModel Code { get; set; }

        /// <summary>
        /// Code Body
        /// </summary>
        public CodeModel CodeBody { get; set; }

        /// <summary>
        /// Package Owner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Package Name
        /// </summary>
        public string Name { get; set; }


        public bool Excluded { get; set; }


        //public static implicit operator PackageModel(string packageName)
        //{
        //    return new PackageModel(packageName);
        //}

        public byte[] Write()
        {
            return System.Text.Encoding.UTF8.GetBytes(ToString() + "\r\n");
        }

        public override string ToString()
        {
            return (Excluded ? "#" : string.Empty) + Key;
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

        public override KindModelEnum KindModel
        {
            get { return KindModelEnum.Package; }
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
            return manager.Evaluate(this);
        }

    }

}
