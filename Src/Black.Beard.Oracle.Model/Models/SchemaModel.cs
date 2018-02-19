using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models
{

    public class SchemaModel : ItemBase
    {
        public string Name { get; internal set; }

        public override KindModelEnum KindModel => KindModelEnum.Schemas;

    }


}
