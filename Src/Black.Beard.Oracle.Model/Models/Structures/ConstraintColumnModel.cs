using Bb.Oracle.Models;
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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitConstraintColumn(this);
        }

        public ColumnModel GetColumnModel()
        {
           var table = this.Parent.AsTable();
            if (table == null)
                return null;
           return  table.Columns[this.ColumnName];
        }

        public override void Initialize()
        {
            base.Initialize();

        }

    }

}
