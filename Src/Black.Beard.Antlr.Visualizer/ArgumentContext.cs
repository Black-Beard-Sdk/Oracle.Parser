using Bb.Oracle.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black.Beard.Antlr.Visualizer
{

    public class ArgumentContext
    {

        public ArgumentContext(params string[] args)
        {
            string[] argsResult = ArgumentHelper.MapArguments(args, this);
        }

        public string folder { get; set; }

    }

}
