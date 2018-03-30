﻿using Bb.Oracle.Models.Names;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    [DebuggerDisplay("{Name.ToString()} ()")]
    public class OCallMethodReference : OCodeExpression
    {

        public OCallMethodReference(MethodName name, params OMethodArgument[] arguments) : this()
        {
            this.Arguments.AddRange(arguments);
        }

        public OCallMethodReference()
        {
            Arguments = new List<OMethodArgument>();
        }

        public MethodName Name { get; set; }

        public List<OMethodArgument> Arguments { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.CallProcedure;

    }

}
