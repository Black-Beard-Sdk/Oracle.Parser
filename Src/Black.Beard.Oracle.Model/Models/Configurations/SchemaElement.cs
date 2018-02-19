namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SchemaElement
    {


        public SchemaElement()
        {
            Objects  = new ObjectCollection() { Parent = this};
        }
        /// <summary>
        /// Name
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// Team
        /// </summary>
        public System.String Team { get; set; }

        /// <summary>
        /// Objects
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ObjectCollection" />.");
        /// </returns>
        public ObjectCollection Objects { get; set; }

    }

}



