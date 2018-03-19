using System;
using System.Collections.Generic;
using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Models.Comparer
{

    public class DifferenceModel
    {

        public string ColumnSource { get; internal set; }
        public string ColumnTarget { get; internal set; }

        public TypeDifferenceEnum Kind { get; set; }

        public string PropertyName { get; internal set; }

        public object Source { get; internal set; }
     
        public object Target { get; internal set; }
        public FileElement targetFile { get; set; }

        public List<FileElement> Files { get; set; }

        public string Team { get; set; }

        internal void Addfile(string path)
        {
            Files.Add(new FileElement() { Path = path });
        }

    }

}