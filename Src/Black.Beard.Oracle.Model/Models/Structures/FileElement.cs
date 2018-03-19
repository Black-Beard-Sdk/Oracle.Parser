using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Structures.Models
{

    [DebuggerDisplay("{Path} : {Location}")]
    public partial class FileElement
    {

        public bool Exist(string rootSource)
        {
            return File.Exists( System.IO.Path.Combine(rootSource, this.Path));
        }

        public string Path { get; set; }


        public Location Location { get; set; }

        public override string ToString()
        {
            return $"({Path}, {Location?.ToString() ?? string.Empty})";
        }


    }
}
