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

    }

}
		


