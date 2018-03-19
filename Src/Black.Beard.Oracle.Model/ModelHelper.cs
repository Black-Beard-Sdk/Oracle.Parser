using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle
{

    public static class ModelHelper
    {

        public static TableModel AsTable( this object self)
        {

            TableModel Result = self as TableModel;
            if (Result == null)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();

            }

            return Result;

        }



    }
}
