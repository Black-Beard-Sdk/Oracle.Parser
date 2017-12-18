﻿using Newtonsoft.Json;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ConstraintColumnModel : ItemBase
    {

        /// <summary>
        /// Column Name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public int Position { get; set; }

        [JsonIgnore]
        public ConstraintModel Parent { get; set; }

        internal void Initialize()
        {

        }

    }

}