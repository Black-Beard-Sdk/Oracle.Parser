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

    }

}



