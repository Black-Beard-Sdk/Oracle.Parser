using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Exceptions
{


    [Serializable]
    public class AmbiguousObjectException : OracleParserException
    {
        public AmbiguousObjectException() { }
        public AmbiguousObjectException(string message) : base(message) { }
        public AmbiguousObjectException(string message, Exception inner) : base(message, inner) { }
        protected AmbiguousObjectException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
