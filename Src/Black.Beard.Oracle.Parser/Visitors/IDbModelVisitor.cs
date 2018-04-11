using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using Bb.Oracle.Validators;
using System.Collections.Generic;

namespace Bb.Oracle.Visitors
{
    public interface IDbModelVisitor
    {

        EventParsers Events { get; }

        List<OracleObject> Items { get; }

    }
}