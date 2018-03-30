using System;
using System.Collections.Generic;
using System.Text;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Models.Codes
{


    public enum PercentTypeEnum
    {
        Undefined,
        PercentType,
        PercentRowType,
    }

    public class OTypeReference : OCodeObject
    {

        public OTypeReference()
        {
            DataType = new OracleType() { Name = "Undefined" };
        }

        //public List<string> Path { get; set; }

        public PercentTypeEnum KindTypeReference { get; set; }

        public OracleType DataType { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.TypeReference;

    }

}
