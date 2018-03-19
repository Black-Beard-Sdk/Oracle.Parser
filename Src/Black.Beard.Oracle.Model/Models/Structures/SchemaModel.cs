using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Structures.Models
{

    public class SchemaModel : ItemBase
    {

        public string Name { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.Schemas;

    }


}
