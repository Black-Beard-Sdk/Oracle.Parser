using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bb.Beard.Oracle.Reader;
using System.IO;

namespace Black.Beard.Oracle.Parser.UnitTest
{
    [TestClass]
    public class OracleLoaderUnitTest
    {

        [TestMethod]
        public void TestMethod1()
        {

            string connectionString = string.Format(@"Data source={0};USER ID={1};Password={2};", "DEV_V", "GUEST", "GUEST");
            Func<string, bool> act = shema => { return true; };
            string file = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            Database.GenerateFile("sourceName", connectionString, file, act, false, "Name");
            
        }

    }
}
