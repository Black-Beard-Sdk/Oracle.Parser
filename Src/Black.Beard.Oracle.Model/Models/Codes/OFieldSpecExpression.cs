using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public class OFieldSpecExpression : OCodeExpression
    {

        public override KindModelEnum KindModel => KindModelEnum.FieldSpec;

        public OTypeReference Type { get; set; }
        
        public bool Nullable { get; set; }
        
        public OCodeExpression DefaultValue { get; set; }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitFieldSpecExpression(this);
            Type.Accept(visitor);
            DefaultValue.Accept(visitor);
        }


    }

}
