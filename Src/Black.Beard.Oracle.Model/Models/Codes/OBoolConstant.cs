using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{


    [DebuggerDisplay("{Value}")]
    public class OBoolConstant : OConstant
    {

        public bool Value { get; set; }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitBoolConstant(this);
        }

    }

}