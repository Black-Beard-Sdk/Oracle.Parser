using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pssa.Sdk.DataAccess.Dao.Contracts.Exceptions
{

    [Serializable]
    public class DBExecProcedureException : DBDataException
    {
        public DBExecProcedureException() { }
        public DBExecProcedureException(string message) : base(message) { }
        public DBExecProcedureException(string message, Exception inner) : base(message, inner) { }
        protected DBExecProcedureException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public DBExecProcedureException(Exception ex)
            : this(ex.Message, ex)
        {
        }

    }

}
