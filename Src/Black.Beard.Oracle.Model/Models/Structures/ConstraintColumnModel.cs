using Newtonsoft.Json;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ConstraintColumnModel : ItemBase
    {

        /// <summary>
        /// Column Name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public int Position { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.ConstraintColumn;

    }

}
