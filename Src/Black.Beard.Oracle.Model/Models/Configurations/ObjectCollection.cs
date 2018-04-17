using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ObjectCollection : IndexedCollection<ObjectElement>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static ObjectCollection()
        {
            ObjectCollection.Key = IndexedCollection<ObjectElement>.GetMethodKey(c => c.Fullname);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            //foreach (var item in this)
            //    item.Accept(visitor);
        }


    }

}



