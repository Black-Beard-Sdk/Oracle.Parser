namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcedureResult
    {

        /// <summary>
        /// Type
        /// </summary>
        /// <returns>		
        /// Objet <see cref="OracleType" />.");
        /// </returns>   
        public OracleType Type { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ColumnCollection" />.");
        /// </returns>   
        public ColumnCollection Columns { get; set; }= new ColumnCollection();

    }
}
