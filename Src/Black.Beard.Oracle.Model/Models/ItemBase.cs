using Newtonsoft.Json;
using System;

namespace Bb.Oracle.Models
{

    public abstract partial class ItemBase
    {

        public ItemBase()
        {
            this.Files = new FileCollection() { Parent = this};
        }

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
        public FileCollection Files { get; set; } 

        public virtual void Initialize()
        {

        }

        [JsonIgnore]
        public abstract KindModelEnum KindModel { get; }

        [JsonIgnore]
        public object Parent { get; set; }

        [JsonIgnore]
        public OracleDatabase Root
        {
            get
            {

                var r = this.Parent as OracleDatabase;
                if (r != null)
                    return r;

                var i =this.Parent as ItemBase;
                if (i != null)
                    return i.Root;

                return null;
                    
            }
        }


    }
}
