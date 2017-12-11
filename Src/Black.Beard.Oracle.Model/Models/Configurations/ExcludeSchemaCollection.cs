using Bb.Oracle.Models;

namespace Bb.Oracle.Models.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ExcludeSchemaCollection : ItemBaseCollection<ExcludeSchemaElement>
	{

        static ExcludeSchemaCollection()
        {
            ExcludeSchemaCollection.Key = ItemBaseCollection<ExcludeSchemaElement>.GetMethodKey(c => c.Name);
        }

    }

}
		


