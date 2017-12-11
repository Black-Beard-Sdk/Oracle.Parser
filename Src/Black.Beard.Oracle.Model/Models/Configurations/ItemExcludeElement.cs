using Bb.Oracle.Models;

namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ItemExcludeElement
	{
			
		/// <summary>
		/// Name
		/// </summary>
		public System.String Name { get; set; }

        /// <summary>
        /// Kind
        /// </summary>
        public ExcludeKindEnum Kind { get; set; }

        /// <summary>
        /// Pattern
        /// </summary>
        public System.String Pattern { get; set; }

        /// <summary>
        /// Ignore Case
        /// </summary>
        public System.Boolean IgnoreCase { get; set; }

        public bool Check(string name)
        {
            if (reg == null)
            {
                var option = this.IgnoreCase ? System.Text.RegularExpressions.RegexOptions.IgnoreCase : System.Text.RegularExpressions.RegexOptions.None;
                reg = new System.Text.RegularExpressions.Regex(this.Pattern, option);
            }
            return reg.IsMatch(name);
        }

        System.Text.RegularExpressions.Regex reg;

    }



}
		


