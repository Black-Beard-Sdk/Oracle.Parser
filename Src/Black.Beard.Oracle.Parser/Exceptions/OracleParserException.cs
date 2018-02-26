using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Exceptions
{


    [Serializable]
    public class OracleParserException : Exception
    {
        public OracleParserException() { }
        public OracleParserException(string message) : base(message) { }
        public OracleParserException(string message, Exception inner) : base(message, inner) { }
        protected OracleParserException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
