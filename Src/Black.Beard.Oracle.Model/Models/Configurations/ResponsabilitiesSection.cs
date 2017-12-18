using Newtonsoft.Json;
using System.IO;

namespace Bb.Oracle.Models.Configurations
{

    public partial class ResponsabilitiesSection
    {

        private static ResponsabilitiesSection _configuration;
        public static ResponsabilitiesSection Configuration
        {
            get
            {
                return _configuration ?? (_configuration = ReadFile("ResponsabilitiesSection.json"));
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
        /// Objet <see cref="schemaCollection" />.");
        /// </returns>
        public schemaCollection Schemas { get; set; } = new schemaCollection();

        /// <summary>
        /// Teams
        /// </summary>
        /// <returns>		
        /// Objet <see cref="TeamCollection" />.");
        /// </returns>
        public TeamCollection Teams { get; set; } = new TeamCollection();


        public void WriteFile(string filename)
        {
            FileInfo file = new FileInfo(filename);
            using (StreamWriter stream = file.CreateText())
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(stream, this);
            }
        }

        public static ResponsabilitiesSection ReadFile(string filename)
        {
            FileInfo file = new FileInfo(filename);
            if (file.Exists)
                using (StreamReader stream = file.OpenText())
                {
                    JsonSerializer serializer = new JsonSerializer();
                    ResponsabilitiesSection db = (ResponsabilitiesSection)serializer.Deserialize(stream, typeof(ResponsabilitiesSection));
                    return db;
                }
            return new ResponsabilitiesSection();
        }


    }



}



