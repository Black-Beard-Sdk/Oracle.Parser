using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public abstract class OTypeDefinition : OCodeStatement
    {
        public string Name { get; set; }
    }


    public class OTableTypeDef : OTypeDefinition
    {

        public override KindModelEnum KindModel =>  KindModelEnum.TableTypeRef;

    }

    public class OArrayTypeDef : OTypeDefinition
    {

        public override KindModelEnum KindModel => KindModelEnum.ArrayTypeDef;

    }

    public class ORecordTypeDef : OTypeDefinition
    {

        public override KindModelEnum KindModel => KindModelEnum.RecordTypeDef;

        public List<OFieldSpecExpression> Fields { get; set; }
    }

    public class OVarrayTypeDefinition : OTypeDefinition
    {

        public override KindModelEnum KindModel => KindModelEnum.VarrayTypeDef;

        public OTypeReference Type { get; set; }
        public bool Varying { get; set; }
        public OCodeExpression Expression { get; set; }
        public bool Nullable { get; set; }
    }

    public class ORefCursorTypeDef : OTypeDefinition
    {

        public ORefCursorTypeDef()
        {
            Return = new OTypeReference();
        }

        public override KindModelEnum KindModel => KindModelEnum.RefCursorTypeDef;

        public OTypeReference Return { get; set; }

    }


}
