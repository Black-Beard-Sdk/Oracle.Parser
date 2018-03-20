using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{
    [DebuggerDisplay("{Value}")]
    public class OStringConstant : OConstant
    {

        public string Value { get; set; }

    }

}