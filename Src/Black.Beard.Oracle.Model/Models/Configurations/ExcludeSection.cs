namespace Bb.Oracle.Models.Configurations
{
    public partial class ExcludeSection
	{
        
		private static ExcludeSection _configuration;
		public static ExcludeSection Configuration 
		{ 
			get 
			{ 
				return _configuration ?? null; 
			} 
			set
			{
				_configuration = value;
			}
		}

        /// <summary>
        /// Schemas
        /// </summary>
        /// <returns>		
        /// Objet <see cref="Pssa.Tools.Databases.Models.Configurations.ExcludeSchemaCollection" />.");
        /// </returns>
        public ExcludeSchemaCollection Schemas { get; set; } = new ExcludeSchemaCollection();

	}

}
		


