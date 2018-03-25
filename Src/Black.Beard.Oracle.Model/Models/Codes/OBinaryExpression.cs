using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{
    [DebuggerDisplay("{Left} {Operator} {Right}")]
    public class OBinaryExpression : OCodeExpression
    {

        public OCodeExpression Left { get; set; }
        public OCodeExpression Right { get; set; }
        public OperatorEnum Operator { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.BinaryExpression;

    }

}