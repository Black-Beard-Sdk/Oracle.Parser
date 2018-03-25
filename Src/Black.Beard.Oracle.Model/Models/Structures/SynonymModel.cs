using Bb.Oracle.Models;
using Newtonsoft.Json;

namespace Bb.Oracle.Structures.Models
{
    /// <summary>
    /// Synonym
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SYNONYM {Owner}.{Name} FOR {ObjectTargetOwner}.{ObjectTargetName}")]
    public partial class SynonymModel : ItemBase
    {

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Object Target
        /// </summary>
        public string ObjectTargetName { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string ObjectTargetOwner { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// Synonym Owner
        /// </summary>
        public string Owner { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.Synonym;

        public bool IsPublic { get; set; }
        public string DbLink { get; set; }
    }

}
