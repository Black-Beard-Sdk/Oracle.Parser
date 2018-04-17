using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{

    [DebuggerDisplay("{Value}")]
    public class OStringConstant : OConstant
    {

        public string Value { get; set; }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitStringConstant(this);
        }

    }

    [DebuggerDisplay("{Value}")]
    public class OKeyWordConstant : OConstant
    {

        public string Value { get; set; }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitKeyWordConstant(this);
        }


    }

}