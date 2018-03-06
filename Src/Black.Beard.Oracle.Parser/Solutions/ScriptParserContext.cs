using System.IO;

namespace Bb.Oracle.Solutions
{
    public class ScriptParserContext
    {

        public ScriptParserContext(string folderPath, string searhPattern)
        {

            this.FolderPath = folderPath;
            this.FilePropertyResolver = new FilePropertyResolver();
            this.Directory = new DirectoryInfo(FolderPath);

            if (!Directory.Exists)
                throw new DirectoryNotFoundException(folderPath);

            this._cut = this.Directory.FullName.Length;

            this.SearhPattern = searhPattern;

        }

        internal DirectoryInfo Directory { get; }
        internal readonly int _cut;

        public string FolderPath { get; }

        public IFilePropertyResolver FilePropertyResolver { get; set; }

        public string SearhPattern { get; }

    }


}
