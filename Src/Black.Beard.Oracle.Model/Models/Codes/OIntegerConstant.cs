using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{
    [DebuggerDisplay("{Value}")]
    public class OIntegerConstant : OConstant
    {
        public int Value { get; set; }

    }

}