using Bb.Oracle.Structures.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models
{

    public class ParserInformations
    {

        public EventParsers Events { get; set; }

        public Errors Errors { get; set; }

    }

}
