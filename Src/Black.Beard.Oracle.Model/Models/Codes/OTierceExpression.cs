using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{
    [DebuggerDisplay("{Left} {Operator} {Center} AND {Right}")]
    public class OTierceExpression : OCodeExpression
    {
        public OCodeExpression Left { get; set; }
        public OperatorEnum Operator { get; set; }

        public OCodeExpression Center { get; set; }

        public OCodeExpression Right { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.TierceExpression;

    }

}