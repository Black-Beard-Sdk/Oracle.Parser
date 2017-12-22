using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Models
{

    public enum SqlKind
    {

        Undefined,
        
        Index,
        MaterializedView,
        MaterializedViewLog,
        Sequence,
        Synonym,
        Table,
        UserObjectPrivilege,
        View,
        Trigger,
        Procedure,
        Package,
        PackageBodies,
        Function,
        Type,
        Jobs,

        NotImplemented,

    }


}
