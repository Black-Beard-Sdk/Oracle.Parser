using System;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ArgumentModel : ItemBase
    {

        /// <summary>
        /// Argument Name
        /// </summary>
        public string ArgumentName { get; set; }

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
        public OracleType Type { get; set; }

        public ProcedureModel Parent { get; set; }

        internal void Initialize()
        {
           
        }

    }

}
