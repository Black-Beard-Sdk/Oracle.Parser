using System;
using System.Collections.Generic;
using System.Text;
using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{

    public class PhysicalAttributesModel : ItemBase
    {

        public PhysicalAttributesModel()
        {

            Tablespace = new ReferenceTablespace() { GetDb = () => this.Root };

        }

        public override void Initialize()
        {
            Tablespace.GetDb = () => this.Root;
        }

        /// <summary>
        /// Buffer Pool
        /// </summary>   
        public string BufferPool { get; set; }

        /// <summary>
        /// Flash Cache
        /// </summary>   
        public string FlashCache { get; set; }

        /// <summary>
        /// Tablespace
        /// </summary>
        public ReferenceTablespace Tablespace { get; set; }

        public override KindModelEnum KindModel => KindModelEnum.PhysicalAttribute;

        /// <summary>
        /// Pct Increase
        /// </summary>   
        public decimal PctIncrease { get; set; }

        /// <summary>
        /// Freelists
        /// </summary>   
        public decimal Freelists { get; set; }

        /// <summary>
        /// Freelist Groups
        /// </summary>   
        public decimal FreelistGroups { get; set; }

        /// <summary>
        /// Pct Free
        /// </summary>
        public decimal PctFree { get; set; }

        /// <summary>
        /// Pct Used
        /// </summary>   
        public decimal PctUsed { get; set; }

        /// <summary>
        /// Ini Trans
        /// </summary>   
        public decimal IniTrans { get; set; }
        
        /// <summary>
        /// Max Trans
        /// </summary>   
        public decimal MaxTrans { get; set; }

        /// <summary>
        /// Initial Extent
        /// </summary>   
        public decimal InitialExtent { get; set; }

        /// <summary>
        /// Min Extent
        /// </summary>   
        public decimal MinExtent { get; set; }

        /// <summary>
        /// Max Extent
        /// </summary>   
        public decimal MaxExtent { get; set; }

        /// <summary>
        /// Next Extent
        /// </summary>   
        public decimal NextExtent { get; set; }

        public decimal MinExtents { get; set; }

        public decimal MaxExtents { get; set; }

        /// <summary>
        /// Logging
        /// </summary>
        public decimal Logging { get; set; }


    }

}
