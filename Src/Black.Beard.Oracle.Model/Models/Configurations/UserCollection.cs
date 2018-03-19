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

    }



}



