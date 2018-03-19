using Bb.Oracle.Models.Names;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public class OCallMethodReference
    {

        public OCallMethodReference()
        {
            Arguments = new List<OMethodArgument>();
        }

        public MethodName Name { get; set; }

        public List<OMethodArgument> Arguments { get; set; }


    }

}
