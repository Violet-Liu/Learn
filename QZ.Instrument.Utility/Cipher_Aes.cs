using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace QZ.Instrument.Utility
{
    public class Cipher_Aes
    {
        #region Encryption
        public static string EncryptToBase64(string input, string key)
        {
            return EncryptToBase64(Encoding.UTF8.GetBytes(input), key);
        }
        public static string EncryptToBase64(byte[] input, string key)
        {
            var bytes = EncryptToBytes(input, key);
            return bytes == null ? null : Convert.ToBase64String(bytes);
        }
        public static byte[] EncryptToBytes(string input, string key)
        {
            return EncryptToBytes(Encoding.UTF8.GetBytes(input), key);
        }
        public static byte[] EncryptToBytes(byte[] input, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);

            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                    }

                    return ms.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Decryption
        /// <summary>
        /// Decrypt input string with a specified key
        /// A null returned value denotes that input string may be invalid or cipher key is dismatch
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns>if succeed, return decrypted string, or else return null</returns>
        public static string DecryptFromBase64(string input, string key)
        {
            var bytes = DecryptToBytes(Convert.FromBase64String(input), key);
            if (bytes == null)
                return null;
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] DecryptToBytes(byte[] input, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var decrypt = aes.CreateDecryptor();
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    cs.Write(input, 0, input.Length);
                }

                xBuff = ms.ToArray();
            }

            return xBuff;
        }
        #endregion
    }
}
