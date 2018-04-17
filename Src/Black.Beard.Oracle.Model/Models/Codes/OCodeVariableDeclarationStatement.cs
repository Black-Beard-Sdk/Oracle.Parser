using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public class OCodeVariableDeclarationStatement : OCodeStatement
    {

        public string Name { get; set; }
        public bool IsConstant { get; set; }
        public bool CanBeNull { get; set; }
        public OTypeReference Type { get; set; }
        public OCodeExpression DefaultValue { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.VariableDeclaration;

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitCodeVariableDeclarationStatement(this);
            Type.Accept(visitor);
            DefaultValue.Accept(visitor);
        }

    }


}
