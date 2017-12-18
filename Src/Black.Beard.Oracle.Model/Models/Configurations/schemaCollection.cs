using System;
using System.ComponentModel;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Bb.Oracle.Models.Configurations
{


    /// <summary>
    /// 
    /// </summary>
    public partial class schemaCollection : IndexedCollection<SchemaElement>
    {
    
        /// <summary>
        /// Ctor
        /// </summary>
        static schemaCollection()
        {
            schemaCollection.Key = IndexedCollection<SchemaElement>.GetMethodKey(c => c.Name);
        }

    }

}



