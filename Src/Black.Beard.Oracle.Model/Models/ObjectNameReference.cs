using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Models
{

    public class ObjectReference
    {

        public ObjectReference(IEnumerable<string> paths) 
            : this(paths.ToArray())
        {

        }

        public ObjectReference(params string[] path)
        {
            this.Path = path;
        }

        public string[] Path { get; }

        public string SchemaCaller { get; set; }

    }


}
