using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Bb.Oracle
{

    public static class Utils
    {


        public static string GetItemName(this ItemBase item)
        {
            string name = string.Empty;
            object src = item;

            if (src is IndexModel)
                name = ((src as IndexModel).Parent as TableModel).Key;

            else if (src is TypeItem)
                name = (src as TypeItem).Name;

            else if (src is GrantModel)
                name = (src as GrantModel).Key;

            else if (src is SynonymModel)
                name = (src as SynonymModel).Name;

            else if (src is ConstraintModel)
                name = ((src as ConstraintModel).Parent as TableModel).Key;

            else if (src is SequenceModel)
                name = (src as SequenceModel).Name;

            else if (src is PackageModel)
                name = (src as PackageModel).Name;

            else if (src is ProcedureModel)
                name = (src as ProcedureModel).Name;

            else if (src is TriggerModel)
                name = (src as TriggerModel).Key;

            else if (src is TableModel)
                name = (src as TableModel).Name;

            else
            {
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();

                else
                    name = src.ToString() + " umanaged";
            }

            return name;

        }

        public static string Serialize(string code, bool compress)
        {

            if (!string.IsNullOrEmpty(code))
            {
                byte[] plainDatas = System.Text.Encoding.UTF8.GetBytes(code);
                byte[] compressedData = compress ? Compression.Compress(plainDatas) : plainDatas;
                string file_message = Convert.ToBase64String(compressedData);
                return file_message;
            }

            return string.Empty;

        }

        public static string Unserialize(string payload, bool compressed)
        {
            if (!string.IsNullOrEmpty(payload))
            {
                byte[] payload_compressed_bytes = Convert.FromBase64String(payload);
                byte[] decompressedData = compressed ? Compression.Decompress(payload_compressed_bytes) : payload_compressed_bytes;
                string code = System.Text.Encoding.UTF8.GetString(decompressedData);
                return code;
            }

            return string.Empty;

        }

    }

}

namespace Bb.Oracle.Models
{
    public class Compression
    {

        public static byte[] Compress(byte[] plainData)
        {
            byte[] compressedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zs.Write(plainData, 0, plainData.Length);
                }

                ms.Position = 0;
                compressedData = ms.ToArray();
            }

            return compressedData;
        }

        public static byte[] Decompress(byte[] compressedData)
        {
            byte[] plainData;
            using (MemoryStream ms = new MemoryStream(compressedData))
            {
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Decompress, true))
                {
                    using (MemoryStream unzippedMs = new MemoryStream())
                    {
                        zs.CopyTo(unzippedMs);
                        plainData = unzippedMs.ToArray();
                    }
                }
            }

            return plainData;
        }

    }
}