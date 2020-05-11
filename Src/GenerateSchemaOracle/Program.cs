using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bb.Oracle.Reader;

namespace GenerateSchemaOracle
{
    class Program
    {
        static void Main(string[] args)
        {

            LogInitializer.Initialize();

            var ctx = new ArgumentContext(args);

            if (string.IsNullOrEmpty(ctx.ExcludedSchemas))
                ctx.ExcludedSchemas = System.Configuration.ConfigurationManager.AppSettings["EXCLUDED_SCHEMAS"] ?? string.Empty;

            Func<string, bool> act = shema => { return true; };

            Database.GenerateFile(ctx, act);

            //string directory = new FileInfo(ctx.Filename).Directory.FullName;

        }
    }

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
