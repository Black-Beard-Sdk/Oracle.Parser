using Newtonsoft.Json;
using System;
using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{

    public abstract partial class ItemBase : OracleObject
    {

        public ItemBase()
        {
            this.Files = new FileCollection() { Parent = this };
            this.EventParser = new EventParsers();
        }

        public object Tag { get; set; }

        public virtual string GetOwner() { return string.Empty; }

        public virtual string GetName() { return string.Empty; }

        /// <summary>
        /// Valid
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// Files
        /// </summary>
        /// <returns>		
        /// Objet <see cref="FileCollection" />.");
        /// </returns>
        public FileCollection Files { get; set; }

        internal EventParsers EventParser { get; private set; }

        public virtual void Initialize()
        {

        }

        [JsonIgnore]
        public ItemBase Parent { get; set; }

        internal void VisitAlter(OAlter oAlter)
        {
            throw new NotImplementedException();
        }

        [JsonIgnore]
        public OracleDatabase Root { get; internal set; }

        internal void VisitDrop(ODrop oDrop)
        {
            throw new NotImplementedException();
        }
    }

}
