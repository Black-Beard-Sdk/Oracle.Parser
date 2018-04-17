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

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitTableIndexedByPartExpression(this);
            this.By.Accept(visitor);
        }


    }

    public enum OTableIndexedByPartExpressionEnum
    {

        Index,
        Indexed,

    }

}
