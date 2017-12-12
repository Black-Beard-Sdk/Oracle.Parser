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


    }

}
		


