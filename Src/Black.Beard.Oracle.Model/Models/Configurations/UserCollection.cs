using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserCollection : IndexedCollection<UserElement>
    {
        
        /// <summary>
        /// Ctor
        /// </summary>
        static UserCollection()
        {
            UserCollection.Key = IndexedCollection<UserElement>.GetMethodKey(c => c.Name);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            //foreach (var item in this)
            //    item.Accept(visitor);
        }


    }



}



