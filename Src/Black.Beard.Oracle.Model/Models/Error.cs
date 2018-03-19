using Bb.Oracle.Structures.Models;
using Newtonsoft.Json;
using System;

namespace Bb.Oracle.Models
{

    public class Error
    {

        [JsonIgnore]
        public Exception Exception { get; set; }

        public string Message { get; set; }

        public FileElement File { get; set; }

    }

}