using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Structures.Models
{

    public class CodeModel
    {

        public CodeModel()
        {
            this.Files = new FileCollection() { Parent = this };

        }

        public FileCollection Files { get; set; }

        public bool IsEmpty { get { return string.IsNullOrEmpty(this.Code); } }

        public int Length { get { return this.Code?.Length ?? 0; } }

        public string Code { get; set; }

        public string GetSource()
        {

            if (string.IsNullOrEmpty(this.Code))
                return string.Empty;

            return Utils.Unserialize(this.Code, true);

        }

    }

}
