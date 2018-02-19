namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ExcludeSchemaElement
	{

        public ExcludeSchemaElement()
        {

            this.Items = new ExcludingRules() { Parent = this };

        }

        /// <summary>
        /// Name
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ExcludingRules" />.");
        /// </returns>
        public ExcludingRules Items { get; set; } 

    }

}
		


