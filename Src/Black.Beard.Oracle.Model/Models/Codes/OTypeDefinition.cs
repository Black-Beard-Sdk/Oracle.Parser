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

    }

    public class ORefCursorTypeDef : OTypeDefinition
    {
        public override KindModelEnum KindModel => KindModelEnum.RefCursorTypeDef;

    }


}
