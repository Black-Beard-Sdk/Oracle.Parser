using Bb.Oracle.Models.Codes;
using Newtonsoft.Json;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcedureResult
    {

        public ProcedureResult()
        {
            this.Columns = new ColumnCollection() { Parent = this };
            this.Type = new OTypeReference();
        }


        public void Accept(Contracts.IOracleModelVisitor visitor)
        {
            this.Columns.Accept(visitor);
            this.Type.Accept(visitor);
        }

        /// <summary>
        /// Type
        /// </summary>
        /// <returns>		
        /// Objet <see cref="OracleType" />.");
        /// </returns>   
        public OTypeReference Type { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ColumnCollection" />.");
        /// </returns>   
        public ColumnCollection Columns { get; set; }

        [JsonIgnore]
        public object Parent { get; set; }

    }
}
