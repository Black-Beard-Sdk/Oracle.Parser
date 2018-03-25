using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public enum OperatorEnum
    {

        Undefined,

        Not,
        Between,

        Unmanaged,
        StringConcatenation,
        Substract,
        Add,
        Divider,
        Time,
        And,
        Or,
        Like,
        LikeC,
        Like2,
        Like4,
        Equal,
        NotEqual,
        //AddNot,
        LessThan,
        GreatThan,
        LessOrEqualThan,
        GreatOrEqualThan,
    }

}
