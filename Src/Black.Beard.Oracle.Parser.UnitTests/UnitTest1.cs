//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Bb.Oracle.Solutions;
//using Bb.Oracle.Models;
//using Bb.Oracle.Visitors;
//using Bb.Oracle.Structures.Models;

//namespace Black.Beard.Oracle.Parser.UnitTests
//{
//    [TestClass]
//    public class UnitTest1
//    {

//        [TestMethod]
//        public void TestMethod1()
//        {

//            string path = @"C:\Toolbox\tfs\PLSQL\Pickup\Main\Schemas";

//            SolutionFolder sln = new SolutionFolder(new ScriptParserContext(path, "*.sql"));

//            OracleDatabase db = new OracleDatabase()
//            {
//                SourceScript = true,
//                Name = "branch " //+ this._ctx.Directory.Parent.Name
//            };

//            //if (!string.IsNullOrEmpty(this._ctx.Name))
//            //    outputTarget.Name = this._ctx.Name;

//            var visitor = new ConvertScriptToModelVisitor();

//            sln.Visit(visitor);

//        }

//    }
//}
