using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bb.Oracle.Parser;
using System.IO;
using System.Collections.Generic;
using Bb.Oracle.Visitors;
using Bb.Oracle.Models;

namespace Black.Beard.Oracle.Parser.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            List<FileInfo> _files = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(@"C:\src\PLSQL\Pickup\Main\Schemas\CONFIG");

            foreach (FileInfo file in dir.GetFiles("*.sql", SearchOption.AllDirectories))
                if (file.Directory.Name.ToLower() == "userobjectprivileges")
                    _files.Add(file);

            ConvertScriptToModelVisitor visitor = new ConvertScriptToModelVisitor(new OracleDatabase());

            foreach (var item in _files)
            {

                var p = ScriptParser.ParsePath(item.FullName);
                p.Visit<object>(visitor);

            }


        }

    }
}
