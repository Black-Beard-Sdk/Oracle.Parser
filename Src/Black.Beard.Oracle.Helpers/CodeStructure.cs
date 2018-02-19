using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Helpers
{

    public class CodeStructure
    {


        public CodeStructure(StringBuilder sb, int start, KindCodeStructure kind)
        {
            this._sb = sb ?? throw new ArgumentNullException(nameof(sb));
            this.Start = start;
            this.Kind = kind;
        }

        public int Start { get; }

        public int End { get; internal set; }

        public KindCodeStructure Kind { get; }

        public StringBuilder GetText()
        {

            StringBuilder sb = new StringBuilder(End - Start + 1);
            for (int i = this.Start; i <= this.End; i++)
                sb.Append(this._sb[i]);

            return sb;
        }

        private readonly StringBuilder _sb;


    }

    public enum KindCodeStructure
    {

        Undefined,
        Text,
        Comment,
    }

}
