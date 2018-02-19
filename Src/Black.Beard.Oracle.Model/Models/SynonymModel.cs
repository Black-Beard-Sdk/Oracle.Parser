﻿using Newtonsoft.Json;

namespace Bb.Oracle.Models
{
    /// <summary>
    /// Synonym
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Key} -> {ObjectTarget}")]
    public partial class SynonymModel : ItemBase
    {

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Object Type
        /// </summary>
        public string ObjectType { get; set; }

        /// <summary>
        /// Object Target
        /// </summary>
        public string ObjectTarget { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string SchemaName { get; set; }

        /// Synonym Owner
        /// </summary>
        public string SynonymOwner { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.Synonym;

    }

}
