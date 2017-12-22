using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Reader.Dao.Exceptions
{

    [Serializable]
    public class DBConnectionException : DBDataException
    {
        public DBConnectionException() { }
        public DBConnectionException(string message) : base(message) { }
        public DBConnectionException(string message, Exception inner) : base(message, inner) { }
        protected DBConnectionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public DBConnectionException(Exception ex)
            : this(ex.Message, ex)
        {
        }

    }

}
