using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using Bb.Oracle.Validators;
using System.Collections.Generic;

namespace Bb.Oracle.Visitors
{
    public interface IDbModelVisitor
    {

        List<ParserValidator> Validators { get; }

        Errors Errors { get; }

        EventParsers Events { get; }

        OracleDatabase Db { get; }

    }
}