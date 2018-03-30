using System.Diagnostics;

namespace Bb.Oracle.Models.Codes
{

    [DebuggerDisplay("{ParameterName} => {Value.ToString()}")]
    public class OMethodArgument : OCodeObject
    {


        public OMethodArgument(string name) : this()
        {
            this.ParameterName = name;
        }

        public OMethodArgument()
        {

        }

        public OCodeObject Value { get; set; }

        public string ParameterName { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.MethodArgument;

    }

}