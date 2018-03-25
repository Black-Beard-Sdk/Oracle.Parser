using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{

    [DebuggerDisplay("{ParameterName} => {Value.ToString()}")]
    public class OMethodArgument : OCodeObject
    {

        //public string ArgumentName { get; set; }

        public OCodeObject Value { get; set; }
        public string ParameterName { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.MethodArgument;

    }

}