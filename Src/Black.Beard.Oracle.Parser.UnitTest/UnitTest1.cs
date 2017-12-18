using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bb.Oracle.Parser;
using System.IO;
using System.Collections.Generic;
using Bb.Oracle.Visitors;
using Bb.Oracle.Models;
using System.Diagnostics;

namespace Black.Beard.Oracle.Parser.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            List<FileInfo> _files = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(@"C:\src\PLSQL\Pickup\Main\Schemas");

            foreach (FileInfo file in dir.GetFiles("*.sql", SearchOption.AllDirectories))
                if (file.Directory.Name.ToLower() == "userobjectprivileges")
                    _files.Add(file);

            var db = new OracleDatabase();

            ConvertScriptToModelVisitor visitor = new ConvertScriptToModelVisitor(db);

            foreach (var item in _files)
            {
                Debug.WriteLine($"parsing {item.FullName}");
                var p = ScriptParser.ParsePath(item.FullName);
                p.Visit<object>(visitor);

            }


        }

        [TestMethod]
        public void TestMethod2()
        {

            var db = new OracleDatabase();

            db.Grants.Add(new GrantModel()
            {
                Key = "toto",
                ColumnObjectName = "col",
                FullObjectName = "",
                Files = new FileCollection() { new FileElement()  { Path = "toto.sql" } },
                Grantable = true,
                Hierarchy = true,
                ObjectName = "obj",
                ObjectSchema = "schema",
                Privileges = new HashSet<string>() { "SELECT", "DELETE" },
                Role = "role",
                Valid = true,
            });

        }


    }
}
