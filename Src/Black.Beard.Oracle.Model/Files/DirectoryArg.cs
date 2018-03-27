using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bb.Oracle.Files
{

    public class DirectoryArg : EventArgs
    {

        public DirectoryArg(DirectoryInfo dir)
        {
            this.Directory = dir;
        }


        public DirectoryInfo Directory { get; private set; }

    }

}
