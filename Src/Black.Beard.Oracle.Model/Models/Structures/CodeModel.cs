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

        /// <summary>
        /// return field Code unserialized
        /// </summary>
        /// <returns></returns>
        public string GetSource()
        {

            if (string.IsNullOrEmpty(this.Code))
                return string.Empty;

            return Utils.Unserialize(this.Code, true);

        }

        /// <summary>
        /// Serialize specified source and store it in Code field
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(string source)
        {

            if (string.IsNullOrEmpty(source))
                throw new ArgumentException("source can't be null or empty", nameof(source));

            this.Code = Utils.Serialize(source, true);

        }

        /// <summary>
        /// Serialize specified source and store it in Code field
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(StringBuilder source)
        {

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Length == 0)
                throw new ArgumentException("source can't be null or empty", nameof(source));

            this.Code = Utils.Serialize(source, true);

        }

    }

}
