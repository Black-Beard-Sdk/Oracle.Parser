using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public class OTableIndexedByPartExpression : OCodeExpression
    {

        public override KindModelEnum KindModel => KindModelEnum.TableIndexedByPart;


        public OTableIndexedByPartExpressionEnum IndexKind { get; set; }
        public OTypeReference By { get; set; }
    }

    public enum OTableIndexedByPartExpressionEnum
    {

        Index,
        Indexed,

    }

}
