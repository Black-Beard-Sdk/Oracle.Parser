using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{
    [DebuggerDisplay("{Value}")]
    public class ODecimalConstant : OConstant
    {

        public decimal Value { get; set; }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitODecimalConstant(this);
        }


    }

}