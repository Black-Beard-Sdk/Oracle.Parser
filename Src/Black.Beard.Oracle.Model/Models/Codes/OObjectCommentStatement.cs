using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public abstract class OObjectCommentStatement : OCodeStatement
    {


        public string Comment { get; set; }

    }

    public class OTableCommentStatement : OObjectCommentStatement
    {

        public override KindModelEnum KindModel => KindModelEnum.TableComment;


        public string Table { get; set; }
        public string Owner { get; set; }

    }

    public class OTableColumnCommentStatement : OObjectCommentStatement
    {

        public override KindModelEnum KindModel => KindModelEnum.TableColumnComment;

        public string Table { get; set; }
        public string Owner { get; set; }
        public string Column { get; set; }
    }

}
