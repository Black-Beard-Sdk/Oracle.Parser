using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle
{

    public static class LogInitializer
    {

        public static void Initialize()
        {

            if (Trace.Listeners.OfType<DefaultTraceListener>().Count() > 0)
                Trace.Listeners.Clear();

            if (Trace.Listeners.OfType<ConsoleTraceListener>().Count() == 0)
                Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());

        }

    }

}
