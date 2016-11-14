using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace QZ.Instrument.Utility
{
    public class Cipher_Gzip
    {
        /// <summary>
        /// gzip解压
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] inputBytes)
        {
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {

                using (System.IO.Compression.GZipStream gzip = new GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
                {
                    using (MemoryStream dcms = new MemoryStream())
                    {

                        gzip.CopyTo(dcms, 0x2000);
                        return dcms.ToArray();

                    }
                }

            }
        }

        public static string DeCompress(string input)
        {
            using (var inStream = new MemoryStream(Convert.FromBase64String(input)))
            using (var zipStream = new GZipStream(inStream, CompressionMode.Decompress))
            using (var outStream = new MemoryStream())
            {
                zipStream.CopyTo(outStream);
                return Encoding.UTF8.GetString(outStream.ToArray());
            }
        }

        /// <summary>
        /// gzip压缩
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] inputBytes)
        {
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress))
                {
                    gzip.Write(inputBytes, 0, inputBytes.Length);
                    gzip.Flush();
                    gzip.Close();
                }
                return ms.ToArray();
            }
        }
    }
}
