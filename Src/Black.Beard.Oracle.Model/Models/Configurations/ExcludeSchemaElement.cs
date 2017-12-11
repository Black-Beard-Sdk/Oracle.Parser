namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ExcludeSchemaElement
	{
			
		/// <summary>
		/// Name
		/// </summary>
		public System.String Name { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        /// <returns>		
        /// Objet <see cref="Pssa.Tools.Databases.Models.Configurations.ExcludingRules" />.");
        /// </returns>
        public ExcludingRules Items { get; set; } = new ExcludingRules();

    }

}
		


