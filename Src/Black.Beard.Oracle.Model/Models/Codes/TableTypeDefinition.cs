using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public class TableTypeDefinition : OTypeDefinition
    {

        public override KindModelEnum KindModel => KindModelEnum.TableTypeDefinition;

        public OTypeReference TableOf { get; set; }
        public OTableIndexedByPartExpression IndexedBy { get; set; }
        public bool Nullable { get; set; }

    }

}
