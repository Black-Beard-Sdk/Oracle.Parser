using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bb.Beard.Oracle.Reader;
using System.IO;
using Bb.Oracle.Models;

namespace Black.Beard.Oracle.Parser.UnitTest
{
    [TestClass]
    public class OracleLoaderUnitTest
    {

        [TestMethod]
        public void TestMethod1()
        {

            Func<string, bool> act = shema => { return true; };
            string file = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            var ctx = new ArgumentContext()
            {
                Source = "DEV_V",
                Login = "GUEST",
                Pwd = "GUEST",
                Filename = file,
                ExcludeCode = false,
                Name = "DEV",
                OwnerFilter = "*",
            };

            var db1 = Database.GenerateFile(ctx, act);

            var db2 = OracleDatabase.ReadFile(file);

            // C:\Users\g.beard\AppData\Local\Temp\tmp8C5D.tmp

        }

    }
}
