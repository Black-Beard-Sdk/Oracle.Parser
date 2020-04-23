using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bb.Oracle.Structures.Models;

namespace Black.Beard.Oracle.Parser.UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {

            string path = @"C:\Toolbox\models\ora_inter.config";

            var db = OracleDatabase.ReadFile(path);

        }

    }
}
