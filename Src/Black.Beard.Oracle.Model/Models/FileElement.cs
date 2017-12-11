using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Models
{

    public partial class FileElement
    {

        public bool Exist(string rootSource)
        {
            return File.Exists( System.IO.Path.Combine(rootSource, this.Path));
        }

        ///// <summary>
        ///// Loads the content of the file.
        ///// </summary>
        ///// <param name="rootSource">The root source.</param>
        ///// <returns></returns>
        //public string LoadContent(string rootSource)
        //{

        //    string fileContents = string.Empty;
        //    System.Text.Encoding encoding = null;
        //    string _path = System.IO.Path.Combine(rootSource, this.Path);
        //    FileInfo _file = new FileInfo(_path);
        //    using (FileStream fs =_file.OpenRead())
        //    {

        //        Ude.CharsetDetector cdet = new Ude.CharsetDetector();
        //        cdet.Feed(fs);
        //        cdet.DataEnd();
        //        if (cdet.Charset != null)
        //            encoding = System.Text.Encoding.GetEncoding(cdet.Charset);
        //        else
        //            encoding = System.Text.Encoding.UTF8;

        //        fs.Position = 0;

        //        byte[] ar = new byte[_file.Length];
        //        fs.Read(ar, 0, ar.Length);
        //        fileContents = encoding.GetString(ar);
        //    }

        //    if (fileContents.StartsWith("ï»¿"))
        //        fileContents = fileContents.Substring(3);

        //    if (encoding != System.Text.Encoding.UTF8)
        //    {
        //        var datas = System.Text.Encoding.UTF8.GetBytes(fileContents);
        //        fileContents = System.Text.Encoding.UTF8.GetString(datas);
        //    }

        //    return fileContents;
        //}

        public string Path { get; set; }

    }
}
