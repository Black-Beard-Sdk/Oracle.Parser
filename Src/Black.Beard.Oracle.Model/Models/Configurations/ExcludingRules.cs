using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Models.Configurations
{

    /// <summary>
    /// 
    /// </summary>
    public partial class ExcludingRules : IndexedCollection<ItemExcludeElement>
    {
		/// <summary>
		/// Ctor
		/// </summary>
		static ExcludingRules()
        {
            ExcludingRules.Key = IndexedCollection<ItemExcludeElement>.GetMethodKey(c => c.Name);
        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            //foreach (var item in this)
            //    item.Accept(visitor);
        }


    }

}
		


