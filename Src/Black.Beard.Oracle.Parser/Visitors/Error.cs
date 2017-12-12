using Antlr4.Runtime;
using System;

namespace Bb.Oracle.Visitors
{
    internal class Error
    {
        public Exception Exception { get; internal set; }
        public string Message { get; internal set; }
        public string File { get; internal set; }
    }
}