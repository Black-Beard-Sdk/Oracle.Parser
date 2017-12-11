using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Bb.Oracle.Models
{

    public static class Utils
    {

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