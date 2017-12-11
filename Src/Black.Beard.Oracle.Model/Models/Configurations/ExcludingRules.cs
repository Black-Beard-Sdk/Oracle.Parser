namespace Bb.Oracle.Models.Configurations
{

    /// <summary>
    /// 
    /// </summary>
    public partial class ExcludingRules : ItemBaseCollection<ItemExcludeElement>
    {
		/// <summary>
		/// Ctor
		/// </summary>
		static ExcludingRules()
        {
            ExcludingRules.Key = ItemBaseCollection<ItemExcludeElement>.GetMethodKey(c => c.Name);
        }

    }

}
		


