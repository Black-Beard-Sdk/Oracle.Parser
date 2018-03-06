using Bb.Oracle.Models;
using Bb.Oracle.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Solutions
{

    [System.Diagnostics.DebuggerDisplay(@"{ExpectedSchema}\{ExpectedKind.ToString()}\{File.Name}, ({File.FullName})")]
    public class ScriptFileInfo
    {


        public ScriptFileInfo(IFilePropertyResolver propertyResolver, FileInfo file)
        {

            this.File = file;
            this.ExpectedName = Path.GetFileNameWithoutExtension(file.Name);

            this.ExpectedSchema = propertyResolver.ResolveSchema(file);
            this.ExpectedKind = propertyResolver.ResolveKind(file);
            this.Priority = propertyResolver.ResolvePriority(this.ExpectedKind);

        }

        public ScriptParser Script()
        {

            if (this.ReadedAt < this.File.LastWriteTime)
                ReadFile();

            else if (this._script == null)
                ReadFile();

            return this._script;

        }

        public void Visit<T>(int cut, Antlr4.Runtime.Tree.IParseTreeVisitor<T> visitor)
        {

            var script = this.Script();
            script.File = this.File.FullName.Substring(cut).Trim(' ', '\\');

            if (script.Content.Length > 0)
                script.Visit<T>(visitor);

            else
            {

            }

        }

        private void ReadFile()
        {
            this._script = ScriptParser.ParsePath(this.File.FullName);
            this.ReadedAt = DateTime.Now;
        }

        public DateTime ReadedAt { get; private set; }

        public string ExpectedSchema { get; }

        public FileInfo File { get; }
        public string ExpectedName { get; }
        public int Priority { get; private set; }
        public SqlKind ExpectedKind { get; private set; }
        public bool Clean { get; private set; }

        private ScriptParser _script;

        internal void Finally()
        {

        }

    }

}
