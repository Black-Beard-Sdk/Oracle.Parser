using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bb.Oracle.Parser;
using System.IO;
using System.Collections.Generic;
using Bb.Oracle.Visitors;
using Bb.Oracle.Models;
using System.Linq;
using Black.Beard.Oracle.Helpers;

namespace Black.Beard.Oracle.Parser.UnitTest
{

    [TestClass]
    public class GrantUnitTest : UnitTestBase
    {

        [TestMethod]
        public void TestSerializeGrant()
        {

            string file = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            var db1 = Parse(@"GRANT SELECT ON schema1.TABLE_1 TO ROLE1 WITH GRANT OPTION;");

            db1.WriteFile(file);
            var db2 = OracleDatabase.ReadFile(file);

        }

        [TestMethod]
        public void TestGrantSelectWithGrant()
        {

            var g1 = Grant(@"GRANT SELECT ON SCHEMA1.TABLE1 TO ROLE1 WITH GRANT OPTION;").FirstOrDefault();

            Assert.AreEqual(g1.ObjectSchema, "SCHEMA1");
            Assert.AreEqual(g1.ObjectName, "TABLE1");

            Assert.IsTrue(g1.Privileges.Contains("SELECT"));
            Assert.AreEqual(g1.Role, "ROLE1");

            Assert.AreEqual(g1.Grantable, true);

        }

        [TestMethod]
        public void TestGrantSelect()
        {

            var g1 = Grant(@"GRANT SELECT ON SCHEMA1.TABLE1 TO ROLE1;").FirstOrDefault();

            Assert.AreEqual(g1.ObjectSchema, "SCHEMA1");
            Assert.AreEqual(g1.ObjectName, "TABLE1");

            Assert.IsTrue(g1.Privileges.Contains("SELECT"));
            Assert.AreEqual(g1.Role, "ROLE1");

            Assert.AreEqual(g1.Grantable, false);

        }

        [TestMethod]
        public void TestGrantAnyPrivilges()
        {

            var g1 = Grant(@"GRANT SELECT, INSERT, DELETE ON SCHEMA1.TABLE1 TO ROLE1;").FirstOrDefault();

            Assert.AreEqual(g1.ObjectSchema, "SCHEMA1");
            Assert.AreEqual(g1.ObjectName, "TABLE1");

            Assert.IsTrue(g1.Privileges.Contains("SELECT"));
            Assert.IsTrue(g1.Privileges.Contains("INSERT"));
            Assert.IsTrue(g1.Privileges.Contains("DELETE"));
            Assert.AreEqual(g1.Role, "ROLE1");

        }

    }
}
