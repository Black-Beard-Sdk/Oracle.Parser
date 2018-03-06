using Bb.Oracle.Models;
using Bb.Oracle.Parser;
using Bb.Oracle.Visitors;
using System.Linq;

namespace Black.Beard.Oracle.Parser.UnitTest
{
    public class UnitTestBase
    {



        protected static GrantModel[] Grant(string text)
        {
            var db = Parse(text);
            var grant = db.Grants.ToArray();
            return grant;
        }

        protected static OracleDatabase Parse(string text)
        {

            OracleDatabase oracleDatabase = new OracleDatabase();
            ConvertScriptToModelVisitor visitor = new ConvertScriptToModelVisitor(oracleDatabase);

            var p = ScriptParser.ParseString(text, "");
            p.Visit<object>(visitor);

            return oracleDatabase;

        }

    }
}