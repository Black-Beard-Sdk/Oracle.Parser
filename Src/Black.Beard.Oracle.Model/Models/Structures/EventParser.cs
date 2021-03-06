﻿using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Structures.Models
{

    public class EventParser
    {


        public EventParser()
        {

        }

        public EventParser(ItemBase item)
        {
            this.OjectName = $"{item.GetOwner()}.{item.GetName()}";
            this.Kind = item.KindModel;
        }

        public List<FileElement> Files { get; set; } = new List<FileElement>();

        public string Message { get; set; }

        public string OjectName { get; set; }
        public KindModelEnum Kind { get; set; }
        public string Type { get; set; }

        public string ValidatorName { get; set; }

        public Error Error { get; set; }

    }

}
