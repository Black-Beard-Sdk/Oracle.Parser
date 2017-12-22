using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models
{
    public class EventParser
    {

        public EventParser()
        {

        }

        public List<FileElement> Files { get; set; } = new List<FileElement>();

        public string Message { get; set; }

        public string OjectName { get; set; }

        public string Type { get; set; }

        public string ValidatorName { get; set; }

    }

}
