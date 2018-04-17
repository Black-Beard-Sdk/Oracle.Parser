using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public class OCodeVariableReferenceExpression : OCodeExpression
    {
        public string Name { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.VariableReference;

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitCodeVariableReferenceExpression(this);
        }


    }
}
