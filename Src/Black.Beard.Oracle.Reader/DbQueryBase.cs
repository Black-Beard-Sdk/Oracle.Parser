using Bb.Oracle.Reader.Queries;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Bb.Oracle.Reader
{

    public abstract class DbQueryBase<T> : QueryBase<T, DbContextOracle>
    {

    }

}
