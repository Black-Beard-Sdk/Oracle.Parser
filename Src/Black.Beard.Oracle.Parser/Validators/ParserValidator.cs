using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using Bb.Oracle.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Validators
{

    public abstract class ParserValidator
    {

        public virtual bool CanEvaluate(OracleObject item)
        {
            return true;
        }

        public abstract void Evaluate(OracleObject item);

    }

}
