using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public abstract class OObjectCommentStatement : OCodeStatement
    {

        /// <summary>
        /// comment on object
        /// </summary>
        public string Comment { get; set; }

    }

    public class OTableCommentStatement : OObjectCommentStatement
    {

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitTableComment(this);
        }

        public override KindModelEnum KindModel => KindModelEnum.TableComment;

        /// <summary>
        /// Name of table reference
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// schema owner of table reference
        /// </summary>
        public string Owner { get; set; }

    }

    public class OTableColumnCommentStatement : OTableCommentStatement
    {

        public override KindModelEnum KindModel => KindModelEnum.TableColumnComment;

        /// <summary>
        /// name of the referenced column
        /// </summary>
        public string Column { get; set; }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitTableColumnComment(this);
        }


    }

}
