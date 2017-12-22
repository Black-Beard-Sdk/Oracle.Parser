using Bb.Oracle.Reader;
using Bb.Oracle.Models.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black.Beard.Oracle.DdlReader
{
    class Program
    {

        static void Main(string[] args)
        {

            var ctx = new ArgumentContext(args);

            Func<string, bool> act = shema => { return true; };

            Database.GenerateFile(ctx, act);

            string directory = new FileInfo(ctx.Filename).Directory.FullName;

        }

    }
}
