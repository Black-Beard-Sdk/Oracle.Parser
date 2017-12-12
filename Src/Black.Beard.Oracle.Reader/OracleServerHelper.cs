using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Beard.Oracle.Reader
{

    public static class OracleServerHelper
    {


        public static List<OracleServer> LoadServersFromTNSName()
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

                        _server = new OracleServer() { Name = DS, index = i - 1 };
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
    
        [System.Diagnostics.DebuggerDisplay("{Name} : {Value}")]
        public class OracleServer : OracleServerItem
        {

            internal int index;

            public OracleServer()
            {

            }

        }

        public class OracleServerItem
        {

            public OracleServerItem()
            {
            }

            public string Name { get; internal set; }

            public string Value { get; internal set; }

        }

    }


}
