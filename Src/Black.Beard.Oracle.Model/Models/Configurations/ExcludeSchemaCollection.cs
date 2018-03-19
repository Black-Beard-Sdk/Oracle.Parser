using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ExcludeSchemaCollection : IndexedCollection<ExcludeSchemaElement>
	{

        static ExcludeSchemaCollection()
        {
            ExcludeSchemaCollection.Key = IndexedCollection<ExcludeSchemaElement>.GetMethodKey(c => c.Name);
        }

    }

}
		


