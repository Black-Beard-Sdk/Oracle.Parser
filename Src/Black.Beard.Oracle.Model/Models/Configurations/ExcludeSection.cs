using Newtonsoft.Json;
using System;
using System.IO;

namespace Bb.Oracle.Models.Configurations
{
    public partial class ExcludeSection
	{
        
		private static ExcludeSection _configuration;
		public static ExcludeSection Configuration 
		{ 
			get 
			{
                return _configuration ?? (_configuration = ReadFile("ExcludeSection.json"));
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
        /// Objet <see cref="ExcludeSchemaCollection" />.");
        /// </returns>
        public ExcludeSchemaCollection Schemas { get; set; } = new ExcludeSchemaCollection();

        public static ExcludeSection LoadFile(string excludeFile)
        {

            FileInfo file = new FileInfo(excludeFile);
            using (StreamReader stream = file.OpenText())
            {
                ExcludeSection result = JsonConvert.DeserializeObject<ExcludeSection>(stream.ReadToEnd());
                return result;
            }
        }



        public void WriteFile(string filename)
        {
            FileInfo file = new FileInfo(filename);
            using (StreamWriter stream = file.CreateText())
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(stream, this);
            }
        }

        public static ExcludeSection ReadFile(string filename)
        {
            FileInfo file = new FileInfo(filename);
            if (file.Exists)
            {
                using (StreamReader stream = file.OpenText())
                {
                    JsonSerializer serializer = new JsonSerializer();
                    ExcludeSection db = (ExcludeSection)serializer.Deserialize(stream, typeof(ExcludeSection));
                    return db;
                }
            }
            return new ExcludeSection();
        }

    }

}
		


