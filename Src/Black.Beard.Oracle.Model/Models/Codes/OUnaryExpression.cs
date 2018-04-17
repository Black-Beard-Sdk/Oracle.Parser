using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{
    [DebuggerDisplay("{Operator} {Left}")]
    public class OUnaryExpression : OCodeExpression
    {

        public OCodeExpression Left { get; set; }

        public OperatorEnum Operator { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.UnaryExpression;

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitUnaryExpression(this);
            Left.Accept(visitor);
        }


    }

}