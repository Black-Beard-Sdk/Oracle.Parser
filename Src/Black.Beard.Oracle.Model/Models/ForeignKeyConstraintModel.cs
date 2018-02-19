namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ForeignKeyConstraintModel : ItemBase
    {

        /// <summary>
        /// Constraint Name
        /// </summary>
        public string ConstraintName { get; set; }

        /// <summary>
        /// Table
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Field
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Is Foreign Key
        /// </summary>
        public bool IsForeignKey { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.ForeignKey;

    }

}
