using Newtonsoft.Json;
using System;

namespace Bb.Oracle.Models
{

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{ArgumentName}")]
    public partial class ArgumentModel : ItemBase
    {

        /// <summary>
        /// Argument Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// In
        /// </summary>
        public bool In { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Is Valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Out
        /// </summary>
        public bool Out { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Sequence
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        /// <returns>		
        /// Objet <see cref="OracleType" />.");
        /// </returns>
        public OracleType Type { get; set; } = new OracleType();

        public override KindModelEnum KindModel => KindModelEnum.Argument;


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
