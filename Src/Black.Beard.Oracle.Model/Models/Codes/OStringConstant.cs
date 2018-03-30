using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{

    [DebuggerDisplay("{Value}")]
    public class OStringConstant : OConstant
    {

        public string Value { get; set; }

    }

    [DebuggerDisplay("{Value}")]
    public class OKeyWordConstant : OConstant
    {

        public string Value { get; set; }

    }

}