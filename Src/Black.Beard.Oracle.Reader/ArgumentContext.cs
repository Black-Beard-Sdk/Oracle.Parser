using Black.Beard.Oracle.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Beard.Oracle.Reader
{
    public class ArgumentContext
    {
        public ArgumentContext(params string[] args)
        {
            string[] argsResult = ArgumentHelper.MapArguments(args, this);
        }

        public string Name { get; set; }

        public string Filename { get; set; }

        public string Login { get; set; }

        public string Pwd { get; set; }

        public string Source { get; set; }

        public string OwnerFilter { get; set; }

        public string Procedures { get; set; }

        public string Tables { get; set; }

        public string ExcludeFile { get; set; }

        public bool ExcludeCode { get; set; }
      
    }

}
