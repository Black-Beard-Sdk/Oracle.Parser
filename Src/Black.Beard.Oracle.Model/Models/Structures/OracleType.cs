﻿
using Bb.Oracle.Contracts;
using Bb.Oracle.Models;
using System.ComponentModel;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OracleType : ItemBase
    {

        public OracleType()
        {

        }

        /// <summary>
        /// Data Length
        /// </summary>   
        public int DataLength { get; set; }

        /// <summary>
        /// default Length
        /// </summary>   
        [DefaultValue(0)]
        public int defaultLength { get; set; }

        /// <summary>
        /// Data Default
        /// </summary>   
        [DefaultValue("")]
        public string DataDefault { get; set; }

        /// <summary>
        /// Data Precision
        /// </summary>   
        public decimal DataPrecision { get; set; }

        /// <summary>
        /// Db Type
        /// </summary>   
        public string DbType { get; set; }

        /// <summary>
        /// Type Owner
        /// </summary>   
        [DefaultValue("")]
        public string Owner { get; set; }

        /// <summary>
        /// Type Name
        /// </summary>   
        [DefaultValue("")]
        public string Name { get; set; }

        /// <summary>
        /// Type Name
        /// </summary>   
        [DefaultValue("")]
        public string Name2 { get; set; }

        public override string GetName() { return Name; }

        public override string GetOwner() { return Owner; }

        /// <summary>
        /// Data Level
        /// </summary>   
        public int DataLevel { get; set; }

        /// <summary>
        /// Is Array
        /// </summary>   
        public bool IsArray { get; set; }

        /// <summary>
        /// Array Of Type
        /// </summary>   
        public string ArrayOfType { get; set; }

        /// <summary>
        /// Is Record
        /// </summary>   
        [DefaultValue(false)]
        public bool IsRecord { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.OracleType;

        public OracleType Clone()
        {
            return new OracleType()
            {
                ArrayOfType = this.ArrayOfType,
                DataDefault = this.DataDefault,
                DataLength = this.DataLength,
                DataLevel = this.DataLevel,
                DataPrecision = this.DataPrecision,
                DbType = this.DbType,
                defaultLength = this.defaultLength,
                IsArray = this.IsArray,
                IsRecord = this.IsRecord,
                Tag = this.ArrayOfType,
                Name = this.Name,
                Owner = this.Owner,
            };
        }

        public override void Accept(IOracleModelVisitor visitor)
        {
            visitor.VisitOracleType(this);
        }

    }
}
