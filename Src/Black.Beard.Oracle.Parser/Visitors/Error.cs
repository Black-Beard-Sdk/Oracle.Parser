using Antlr4.Runtime;
using Bb.Oracle.Models;
using System;

namespace Bb.Oracle.Visitors
{

    public class Error
    {

        public Exception Exception { get; internal set; }

        public string Message { get; internal set; }

        public FileElement File { get; internal set; }

    }

}