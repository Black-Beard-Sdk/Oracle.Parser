using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pssa.Tools.Databases.Generators.Helpers
{

    public static class OracleServerHelper
    {


        public static List<OracleServer> LoadServers()
        {

            List<OracleServer> _lst = new List<OracleServer>();

            var e = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine)["TNS_ADMIN"] as string;
            if (!string.IsNullOrEmpty(e))
                _lst = LoadServersFromTextFile(Path.Combine(e, "tnsnames.ora"));

            return _lst;
        }


        private static List<OracleServer> LoadServersFromTextFile(string FP)
        {

            List<OracleServer> _lst = new List<OracleServer>();
            string inputString;


            StreamReader streamReader = File.OpenText(FP.Trim()); // FP is the filepath of TNS file

            inputString = streamReader.ReadToEnd();
            string[] temp = inputString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            OracleServer _lstserver = null;
            int lastIndex = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                OracleServer _server = null;
                if (temp[i].Trim(' ', '(').Contains("DESCRIPTION"))
                {
                    string DS = temp[i - 1].Trim('=', ' ');
                    if (DS != "(LOAD_BALANCE = OFF)" && DS != ")")
                    {

                        if (_lstserver != null)
                            PushCode(temp, _lstserver, lastIndex, i);

                        _server = new OracleServer() { Name = DS , index = i - 1 };
                        _lst.Add(_server);
                        lastIndex = i - 1;
                        _lstserver = _server;
                    }
                }
            }

            if (_lstserver != null)
                PushCode(temp, _lstserver, lastIndex, temp.Length);

            streamReader.Close();

            foreach (OracleServer item in _lst)
            {

                string u = item.Value;

                var c = u.IndexOf('=');
                if (c != -1)
                    u = u.Substring(c + 1).Trim();

                u = u.Replace(Environment.NewLine, " ");


                while (item.Value != u)
                {
                    item.Value = u;
                    u = u.Replace("  ", " ");
                    u = u.Replace(") )", "))");
                    u = u.Replace("( (", "((");
                }

            }

            return _lst;

        }


        private static void PushCode(string[] temp, OracleServer _lstserver, int lastIndex, int i)
        {
            StringBuilder sb = new StringBuilder(500);
            for (int _i = lastIndex; _i < i - 3; _i++)
            {
                string line = temp[_i];
                if (!line.Trim().StartsWith("#"))
                    sb.AppendLine(line);

            }
            _lstserver.Value = sb.ToString();
        }

        //private static void Build(List<OracleServer> _lst)
        //{
        //    string pattern = @"[\w.-]+|=|\(|\)|\d+";
        //    RegexOptions regexOptions = RegexOptions.None;
        //    Regex regex = new Regex(pattern, regexOptions);

        //    foreach (var item in _lst)
        //    {
        //        var match = regex.Match(item.Value);
        //        OracleServer o = new OracleServer();
        //        o.Parse(match);
        //    }

        //}

            [System.Diagnostics.DebuggerDisplay("{Name} : {Value}")]
        public class OracleServer : OracleServerItem
        {

            internal int index;

            public OracleServer()
            {

            }

            //internal override Match Parse(Match match)
            //{

            //    if (match.Success)
            //    {
            //        this.Name = match.Value;

            //        match = match.NextMatch();
            //        if (match.Success)
            //        {
            //            if (match.Value == "=")
            //            {
            //                var u = new OracleServerItem();
            //                match = u.Parse(match.NextMatch());
            //                _items.Add(u);
            //            }
            //        }

            //    }

            //    return match;
            //}

            //public override string ToString()
            //{
            //    StringBuilder sb = new StringBuilder();

            //    sb.Append(Name);

            //    sb.Append(" = ");

            //    if (this.Items.Count() == 0)
            //        sb.Append(this.Value);

            //    else
            //    {
            //        foreach (OracleServerItem item in this.Items)
            //            sb.Append(item.ToString());
            //    }

            //    return sb.ToString();
            //}

        }

        public class OracleServerItem
        {

            // protected List<OracleServerItem> _items;

            public OracleServerItem()
            {
                // _items = new List<OracleServerItem>();
            }

            public string Name { get; internal set; }

            public string Value { get; internal set; }

            //public IEnumerable<OracleServerItem> Items { get { return _items; } }

            //internal virtual Match Parse(Match match)
            //{

            //    bool n = true;
            //    while (match.Success)
            //    {

            //        if (match.Value == ")")
            //            break;
            //        else if (match.Value == "(")
            //        {
            //            var u = new OracleServerItem();
            //            match = u.Parse(match.NextMatch());
            //            _items.Add(u);
            //        }
            //        else if (match.Value != "=")
            //        {
            //            if (n)
            //            {
            //                Name = match.Value;
            //                n = false;
            //            }
            //            else
            //                Value = match.Value;
            //            break;
            //        }

            //        match = match.NextMatch();

            //    }

            //    return match;

            //}

            //public override string ToString()
            //{
            //    StringBuilder sb = new StringBuilder();

            //    sb.Append("( ");

            //    sb.Append(Name);

            //    sb.Append(" = ");

            //    if (this._items.Count == 0)
            //        sb.Append(this.Value);

            //    else
            //    {
            //        foreach (OracleServerItem item in this._items)
            //            sb.Append(item.ToString());
            //    }

            //    sb.Append(" )");

            //    return sb.ToString();
            //}

        }

    }

}
