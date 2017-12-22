using System;

namespace Bb.Oracle.Models
{

    public partial class ItemBase
    {

        public object Tag { get; set; }

        public virtual string GetOwner() { return string.Empty; }

        public virtual string GetName() { return string.Empty; }

        /// <summary>
        /// Valid
        /// </summary>
        public bool Valid { get; set; }

        // TODO : A degager
        /// <summary>
        /// Associated To
        /// </summary>
        public string AssociatedTo { get; set; }

        /// <summary>
        /// Files
        /// </summary>
        /// <returns>		
        /// Objet <see cref="FileCollection" />.");
        /// </returns>
        public FileCollection Files { get; set; } = new FileCollection();

    }
}
