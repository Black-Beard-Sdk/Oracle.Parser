﻿using Newtonsoft.Json;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcedureResult
    {

        public ProcedureResult()
        {
            this.Columns = new ColumnCollection() { Parent = this };
            this.Type = new OracleType();
        }

        /// <summary>
        /// Type
        /// </summary>
        /// <returns>		
        /// Objet <see cref="OracleType" />.");
        /// </returns>   
        public OracleType Type { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ColumnCollection" />.");
        /// </returns>   
        public ColumnCollection Columns { get; set; }

        [JsonIgnore]
        public object Parent { get; set; }

    }
}
