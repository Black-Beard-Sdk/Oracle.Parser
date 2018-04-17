using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TeamCollection : IndexedCollection<TeamElement>
    {

        /// <summary>
        /// Ctor
        /// </summary>
        static TeamCollection()
        {
            TeamCollection.Key = IndexedCollection<TeamElement>.GetMethodKey(c => c.Name);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            //foreach (var item in this)
            //    item.Accept(visitor);
        }


    }



}



