using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pssa.Sdk.DataAccess.Dao.Contracts.Exceptions
{

    [Serializable]
    public class DBDataException : Exception
    {

        public DBDataException() { }
        public DBDataException(string message) : base(message) { }
        public DBDataException(string message, Exception inner) : base(message, inner) { }
        protected DBDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public DBDataException(Exception ex)
            : this(ex.Message, ex)
        {
        }

    }

}
