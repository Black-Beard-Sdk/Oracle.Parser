namespace Bb.Oracle.Models
{
    [System.Diagnostics.DebuggerDisplay("{Argument.ArgumentName}")]
    public partial class ArgumentInfoModel
    {
        //private string _defaultValue;

        public ArgumentModel Argument { get; set; }

        public ArgumentInfoModel(ArgumentModel c)
        {
            // TODO: Complete member initialization
            this.Argument = c;
            this.Comma = ", ";

            //var p = System.Type.GetType(c.CsType);
            //if (p != null)
            //{
            //    this.Type = p.FullName;
            //    this._defaultValue = GetDefaultValue(p);
            //}
            //else
            //{
            //    this.Type = typeof(object).FullName;
            //    this._defaultValue = null;
            //}

        }

        private string GetDefaultValue(System.Type p)
        {
            if (p.IsClass)
                return "null";
            if (p == typeof(string))
                return "String.Empty";

            if (p == typeof(sbyte)
                || p == typeof(byte)
                || p == typeof(short)
                || p == typeof(ushort)
                || p == typeof(int)
                || p == typeof(uint)
                || p == typeof(long)
                || p == typeof(ulong)
                || p == typeof(float)
                || p == typeof(double)
                || p == typeof(decimal))
                return "0";

            return "null";

        }

        //public string GetDefaultValue()
        //{
        //    return _defaultValue;
        //}

        public string Comma { get; set; }

        //public string Type { get; set; }

        //public override string ToString()
        //{
        //    return Type + " " + Argument.ArgumentName;
        //}

    }

}