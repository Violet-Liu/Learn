using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QZ.Instrument.Utility
{
    /// <summary>
    /// cipher protocal
    /// </summary>
    public class Cipher_Protocol
    {
        #region 静态加载

        /// <summary>
        /// 最小随机填充 不超过255
        /// </summary>
        public const int RANDOM_PAD_MIN = 8;

        /// <summary>
        /// 最大随机填充 不超过 255
        /// </summary>
        public const int RANDOM_PAD_MAX = 64;

        /// <summary>
        /// 默认的密钥偏移
        /// </summary>
        //private static byte[] defaultIV;
        #endregion



        #region 加密协议


        /// <summary>
        /// 指定加密类型和明文，返回按加密协议加密的Base64字符串
        /// </summary>
        /// <param name="input">要加密的明文</param>
        /// <param name="aesKeyStatic">固定密钥</param>
        /// <param name="aesKeyDynamic">动态密钥</param>
        /// <param name="type">按加密协议加密的内存流</param>
        /// <returns></returns>
        public static string EncryptAsBase64(string input, string aesKeyStatic, string aesKeyDynamic, EncryptType type)
        {
            return Convert.ToBase64String(
                EncryptAsBytes(input, aesKeyStatic, aesKeyDynamic, type)
                );
        }

        /// <summary>
        /// 指定加密类型和明文，返回按加密协议加密的Base64字符串
        /// </summary>
        /// <param name="input">要加密的明文字节数组</param>
        /// <param name="aesKeyStatic">固定密钥</param>
        /// <param name="aesKeyDynamic">动态密钥</param>
        /// <param name="type">按加密协议加密的内存流</param>
        /// <returns></returns>
        public static string EncryptAsBase64(byte[] input, string aesKeyStatic, string aesKeyDynamic, EncryptType type)
        {
            return Convert.ToBase64String(
                EncryptAsBytes(input, aesKeyStatic, aesKeyDynamic, type)
                );
        }

        /// <summary>
        /// 指定加密类型和明文，返回按加密协议加密的字节数组
        /// </summary>
        /// <param name="input">要加密的明文</param>
        /// <param name="aesKeyStatic">固定密钥</param>
        /// <param name="aesKeyDynamic">动态密钥</param>
        /// <param name="type">按加密协议加密的内存流</param>
        /// <returns></returns>
        public static byte[] EncryptAsBytes(string input, string aesKeyStatic, string aesKeyDynamic, EncryptType type)
        {
            return Encrypt(input, aesKeyStatic, aesKeyDynamic, type).ToArray();
        }


        /// <summary>
        /// 指定加密类型和明文，返回按加密协议加密的字节数组
        /// </summary>
        /// <param name="input">要加密的明文字节数组</param>
        /// <param name="aesKeyStatic">固定密钥</param>
        /// <param name="aesKeyDynamic">动态密钥</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static byte[] EncryptAsBytes(byte[] input, string aesKeyStatic, string aesKeyDynamic, EncryptType type)
        {
            return Encrypt(input, aesKeyStatic, aesKeyDynamic, type).ToArray();
        }

        /// <summary>
        /// 指定加密类型和明文，返回按加密协议加密的内存流
        /// </summary>
        /// <param name="input">要返回的明文</param>
        /// <param name="type">加密类别</param>
        /// <param name="aesKeyStatic">固定密钥</param>
        /// <param name="aresKeyDynamic">动态密钥</param>
        /// <returns>按加密协议加密的内存流</returns>
        public static MemoryStream Encrypt(string input, string aesKeyStatic, string aesKeyDynamic, EncryptType type)
        {

            byte[] encryptBytes = Encoding.UTF8.GetBytes(input);
            return Encrypt(encryptBytes, aesKeyStatic, aesKeyDynamic, type);

        }


        /// <summary>
        ///  指定加密类型和需要加密字节数组， 返回按加密协议加密的内存流
        /// </summary>
        /// <param name="inputBytes">需要加密字节数组</param>
        /// <param name="aesKeyStatic">固定密钥</param>
        /// <param name="aresKeyDynamic">动态密钥</param>
        /// <param name="type">加密类型</param>
        /// <returns></returns>
        private static MemoryStream Encrypt(byte[] inputBytes, string aesKeyStatic, string aesKeyDynamic, EncryptType type)
        {
            MemoryStream ms = new MemoryStream();

            //随机种子，用来随机填充
            int randomSeed = Math.Abs(Guid.NewGuid().GetHashCode());

            byte[] randomBytes = InternalGetRandomPadding(randomSeed);

            //写入随机填充长度
            ms.WriteByte((byte)randomBytes.Length);

            //写入随机填充
            ms.Write(randomBytes, 0, randomBytes.Length);


            //写入加密类型
            ms.WriteByte((byte)type);


            //获取动态密钥
            byte[] aesKeyBytes = Encoding.UTF8.GetBytes(aesKeyDynamic);

            //偏移后的AES密钥
            byte[] movServerKeyBytes = MovEnc(aesKeyBytes, randomBytes);

            //4字节偏移长度
            //ms.Write(BitConverter.GetBytes(movServerKeyBytes.Length), 0, 4);
            ms.WriteByte((byte)movServerKeyBytes.Length);

            //写入偏移密文
            ms.Write(movServerKeyBytes, 0, movServerKeyBytes.Length);

            //根据类别写入密文

            InternalWriteEncryptDataByType(inputBytes, randomBytes, aesKeyStatic, aesKeyDynamic, type, ms);


            return ms;
        }


        /// <summary>
        /// bytes tostring
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBytesString(byte[] input)
        {
            int len = input.Length - 1;
            StringBuilder sb = new StringBuilder(len * 3 + 3);
            sb.Append("{");
            for (int i = 0; i < len; i++)
            {
                sb.AppendFormat(" {0},", input[i]);
            }
            sb.AppendFormat(" {0}}}", input[len]);
            return sb.ToString();
        }


        /// <summary>
        /// 根据加密类型加密
        /// </summary>
        /// <param name="inputBytes"> 需要加密的正文 </param>
        /// <param name="randomBytes">随机填充</param>
        /// <param name="aesKeyStatic">静态密钥</param>
        /// <param name="aesKeyDynamic">动态密钥</param>
        /// <param name="type">加密类型</param>
        /// <param name="ms">内存流</param>
        private static void InternalWriteEncryptDataByType(byte[] inputBytes, byte[] randomBytes, string aesKeyStatic, string aesKeyDynamic, EncryptType type, MemoryStream ms)
        {



            byte[] encBytes = inputBytes;
            int intType = (int)type;


            //客户端密钥更新强制优先检查
            if ((intType & ((int)EncryptType.ResetKey)) != 0)
            {
                InternalWriteKeyUpdate(aesKeyStatic, randomBytes, ms);
            }



            int maxType = 16;
            int child;
            EncryptType childType;
            int encryptCount = 0;
            while (maxType > 0)
            {
                maxType = maxType >> 1;
                child = maxType & intType;
                if (child == 0)
                    continue;
                encryptCount++;
                childType = (EncryptType)child;

                switch (childType)
                {

                    case EncryptType.ResetKey:
                        break;
                    case EncryptType.AES:
                        encBytes = Cipher_Aes.EncryptToBytes(encBytes, aesKeyDynamic);
                        break;
                    case EncryptType.Gzip:
                        encBytes = Cipher_Gzip.Compress(encBytes);
                        break;
                    case EncryptType.PT:
                        break;
                    default:
                        throw new Exception("未受支持的加密类型");


                }

            }

            if (encryptCount < 0)
                throw new Exception("未识别的加密类型");
            if (encBytes != null)
                ms.Write(encBytes, 0, encBytes.Length);


        }


        /// <summary>
        /// 写入客户端静态密钥偏移
        /// </summary>
        /// <param name="aesKeyStatic">明文静态密钥</param>
        /// <param name="randomBytes">随机填充字节数组</param>
        /// <param name="ms">加密流对象</param>
        private static void InternalWriteKeyUpdate(string aesKeyStatic, byte[] randomBytes, MemoryStream ms)
        {
            byte[] staticKeyBytes = Encoding.UTF8.GetBytes(aesKeyStatic);
            byte[] staticMovKey = MovEnc(staticKeyBytes, randomBytes);
            ms.WriteByte((byte)staticMovKey.Length);
            ms.Write(staticMovKey, 0, staticMovKey.Length);
        }





        /// <summary>
        /// 随机填充
        /// </summary>
        /// <param name="ms">内存流</param>
        /// <param name="randomSeed">随机种子</param>
        private static byte[] InternalGetRandomPadding(int randomSeed)
        {
            Random r = new Random(randomSeed);

            //随机填充长度 8 - 64 字节
            int len = r.Next(RANDOM_PAD_MIN, RANDOM_PAD_MAX);
            //ms.Write(BitConverter.GetBytes(len), 0, 4);
            byte[] bs = new byte[len];
            int i = 0;
            //填充指定长度
            while (i < len)
            {
                bs[i] = (byte)r.Next(0, 255);
                i++;
            }
            return bs;

        }



        #endregion


        #region 根据协议解密

        public static string DecyprtToString(string input)
        {
            return Encoding.UTF8.GetString(Decrypt(input));
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="bytes">按协议加密过的字节数组</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Decrypt(ms);
            }

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="base64String">按协议加密过的base64字符串</param>
        /// <returns></returns>
        public static byte[] Decrypt(string base64String)
        {
            return Decrypt(Convert.FromBase64String(base64String));
        }





        /// <summary>
        /// 输入加密流，得到解密正文
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static byte[] Decrypt(MemoryStream ms)
        {

            //读取首个字节
            int len = ms.ReadByte();

            //实例化长度为len的数组来存放随机填充
            byte[] randomBytes = new byte[len];
            ms.Read(randomBytes, 0, len);

            //读取类型
            int intType = ms.ReadByte();
            EncryptType type = (EncryptType)intType;


            //读取偏移密文长度
            //byte[] movLengthBytes = new byte[4];
            //ms.Read(movLengthBytes, 0, 4);

            //获取密文长度
            int movLength = ms.ReadByte();

            //读取偏移密文
            byte[] movServerKeyBytes = new byte[movLength];
            ms.Read(movServerKeyBytes, 0, movLength);

            //解密偏移密文
            byte[] aesKeyBytes = MovDec(movServerKeyBytes, randomBytes);

            //获得动态密钥
            string aesKeyDynamic = Encoding.UTF8.GetString(aesKeyBytes);


            //检查固定密钥更新
            string aesStaticKey = InternalCheckClientKeyUpdate(intType, randomBytes, ms);


            //按加密类型，解密出正文
            return InternalDecryptReturnContent(intType, aesKeyDynamic, ms);
        }


        /// <summary>
        /// 检查客户端密钥更新协议
        /// </summary>
        /// <param name="intType">协议（加密）类型</param>
        /// <param name="randomBytes">随机填充数组</param>
        /// <param name="ms">按协议加密的内存流</param>
        /// <returns></returns>
        private static string InternalCheckClientKeyUpdate(int intType, byte[] randomBytes, MemoryStream ms)
        {
            //检查是否有客户端密钥返回
            if ((intType & (int)EncryptType.ResetKey) != 0)
            {
                //读取静态偏移密文并解密出静态密钥
                //byte[] staticMovLengthBytes = new byte[4];
                //ms.Read(staticMovLengthBytes, 0, 4);
                int staticKeyMovLength = ms.ReadByte();
                byte[] staticMovBytes = new byte[staticKeyMovLength];
                ms.Read(staticMovBytes, 0, staticKeyMovLength);
                byte[] staticKeyBytes = MovDec(staticMovBytes, randomBytes);
                string staticKey = Encoding.UTF8.GetString(staticKeyBytes);
                //这里客户端实现的时候需要存储这个静态密钥
                return staticKey;
            }
            return null;
        }


        /// <summary>
        /// 解密正文并返回
        /// </summary>
        /// <param name="intType">int32的 加密类型</param>
        /// <param name="aesKeyDynamic">动态加密</param>
        /// <param name="ms">内存协议流</param>
        /// <returns></returns>
        private static byte[] InternalDecryptReturnContent(int intType, string aesKeyDynamic, MemoryStream ms)
        {
            //当前位置
            int currPos = (int)ms.Position;
            int encTextCount = (int)ms.Length - currPos;
            byte[] retBytes = new byte[encTextCount];
            ms.Read(retBytes, 0, retBytes.Length);


            int currType = 1;

            EncryptType state;
            //这里的8是因为当前最大类别就是8
            while (currType <= 8)
            {
                if ((currType & intType) != 0)
                {
                    state = (EncryptType)currType;
                    switch (state)
                    {
                        case EncryptType.AES:

                            retBytes = Cipher_Aes.DecryptToBytes(retBytes, aesKeyDynamic);

                            break;
                        case EncryptType.Gzip:

                            retBytes = Cipher_Gzip.Decompress(retBytes);

                            break;

                        //纯文本，不需要解密
                        case EncryptType.PT:
                            break;

                        //最早判断，已经读取过了
                        case EncryptType.ResetKey:
                            break;
                    }

                }
                currType = currType << 1;
            }
            return retBytes;
        }




        #endregion


        #region 偏移算法



        /// <summary>
        /// 对密钥进行偏移加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="movbytes"></param>
        /// <returns></returns>
        public static byte[] MovEnc(byte[] keyBytes, byte[] movBytes)
        {

            int keyLen = keyBytes.Length;
            int movLen = movBytes.Length;

            byte[] moved = new byte[keyLen * 4];


            int i = 0, j = 0;

            //当前偏移
            int seed;
            //偏移头子节
            byte head = movBytes[0];
            byte end = movBytes[movLen - 1];

            //混合参数
            int mixed = (head | end) & 0xff;

            //密钥字节和偏移字节的和
            int currMix;

            //当前密钥字节
            byte curr;

            //2bit 高位
            int hight;

            //2bit 低位
            int low;

            //偏移范围
            int range;

            //偏移种子的高位
            int seedHight;
            //偏移种子的低位
            int seedLow;

            int b0 = 0, b1 = 0, b2 = 0, b3 = 0;

            // 32 字节 
            while (i < keyLen)
            {

                j = i * 4;

                if (i == 3)
                    Console.WriteLine("here comes.");


                seed = movBytes[i % movLen];
                seedHight = seed >> 4;
                seedLow = seed & 0xF;
                range = seed % 8;
                curr = keyBytes[i];
                hight = curr >> 4;
                low = (curr & 0xF);

                currMix = (mixed + curr) & 0xFF;


                if (range % 8 == 0)
                {
                    b0 = ((hight >> 2) << 6) | ((low & 3) << 4);
                    b1 = ((hight & 3) << 5) | (seedLow >> 1);
                    b2 = ((low >> 2) << 5) | (seedHight >> 1);
                    b3 = currMix;

                }
                else if (range % 7 == 0)
                {
                    b0 = (seedLow << 4) | ((low >> 2) << 2);
                    b1 = ((low & 3) << 6) | seedLow;
                    b2 = (seedHight << 4) | ((hight >> 2) << 2);
                    b3 = ((hight & 3) << 6) | seedHight;

                }
                else if (range % 6 == 0)
                {

                    b0 = (seedHight << 4) | ((hight & 3) << 2);
                    b1 = (seedLow << 4) | ((low & 3) << 2);
                    b2 = (seedHight << 2) | (hight >> 2);
                    b3 = (seedLow << 2) | (low >> 2);

                }

                else if (range % 5 == 0)
                {
                    b0 = ((hight >> 2) << 4) | (seedHight & 3);
                    b1 = ((hight & 3) << 4) | ((low >> 2) << 2);
                    b2 = currMix;
                    b3 = ((low & 3) << 6) | seedHight;

                }
                else if (range % 4 == 0)
                {
                    b0 = (seedHight << 4) | ((hight >> 2) << 2);
                    b1 = ((hight & 3) << 6) | seedHight;
                    b2 = (seedLow << 4) | (low >> 2);
                    b3 = ((low & 3) << 6) | seedLow;

                }
                else if (range % 3 == 0)
                {
                    b0 = (seedLow << 4) | hight;
                    b1 = (seedHight << 4) | low;
                    b2 = currMix;
                    b3 = seedHight << 4;

                }
                else if (range % 2 == 0)
                {

                    b0 = ((hight >> 2) << 6) | (currMix & 0x39);
                    b1 = (seedLow << 4) | ((hight & 3) << 2);
                    b2 = ((low >> 2) << 6) | (currMix & 0x39);
                    b3 = (seedHight << 4) | ((low & 3) << 2);

                }
                else
                {
                    b0 = ((hight & 3) << 6) | (hight >> 2);
                    b1 = currMix;
                    b2 = ((low & 3) << 6) | (mixed >> 2);
                    b3 = ((low >> 2) << 6) | (mixed & 3);

                }


                moved[j] = (byte)b0;
                moved[j + 1] = (byte)b1;
                moved[j + 2] = (byte)b2;
                moved[j + 3] = (byte)b3;

                i++;
            }
            return moved;




        }


        /// <summary>
        /// 从偏移密文中偏正出密钥
        /// </summary>
        /// <param name="movBytes"></param>
        /// <returns></returns>
        public static byte[] MovDec(byte[] movBytes, byte[] movKey)
        {

            int len = movBytes.Length / 4;
            int movKeyLen = movKey.Length;
            int i = 0, j = 0;
            int high, low;

            byte key;

            int b0, b1, b2, b3;

            byte[] bs = new byte[len];
            while (i < len)
            {

                key = (byte)(movKey[i % movKeyLen] % 8);

                b0 = movBytes[j++];
                b1 = movBytes[j++];
                b2 = movBytes[j++];
                b3 = movBytes[j++];


                if (key % 8 == 0)
                {
                    high = ((b0 >> 6) << 2) | (b1 >> 5);
                    low = ((b2 >> 5) << 2) | ((b0 & 0x3F) >> 4);
                }
                else if (key % 7 == 0)
                {

                    high = (b2 & 0xf) | (b3 >> 6);
                    low = (b0 & 0xf) | (b1 >> 6);

                }
                else if (key % 6 == 0)
                {
                    high = ((b2 & 3) << 2) | ((b0 & 0xf) >> 2);

                    low = ((b3 & 3) << 2) | ((b1 & 0xf) >> 2);

                }
                else if (key % 5 == 0)
                {
                    high = ((b0 >> 4) << 2) | (b1 >> 4);
                    low = (((b1 & 0xf) >> 2) << 2) | (b3 >> 6);
                }
                else if (key % 4 == 0)
                {
                    high = (((b0 & 0xf) >> 2) << 2) | (b1 >> 6);
                    low = ((b2 & 0xf) << 2) | (b3 >> 6);
                }
                else if (key % 3 == 0)
                {
                    high = b0 & 0xf;
                    low = b1 & 0xf;

                }
                else if (key % 2 == 0)
                {
                    high = ((b0 >> 6) << 2) | ((b1 & 0xf) >> 2);
                    low = ((b2 >> 6) << 2) | ((b3 & 0xf) >> 2);

                }
                else
                {
                    high = ((b0 & 3) << 2) | (b0 >> 6);
                    low = (b2 >> 6);
                    low = ((b3 >> 6) << 2) | low;

                }
                bs[i++] = (byte)((high << 4) | low);

            }
            return bs;


        }

        #endregion

        #region AES 加解密相关


        /// <summary>
        /// AES密钥类别
        /// </summary>
        public enum AESType
        {
            /// <summary>
            /// 客户端动态加密
            /// </summary>
            client,
            /// <summary>
            /// 服务端动态加密
            /// </summary>
            server
        }


        #endregion
    }

    /// <summary>
    /// 加密类型 除了PlainText之后的顺序可以修改，其他请不要修改！
    /// </summary>
    public enum EncryptType
    {
        /// <summary>
        /// AES加密
        /// </summary>
        AES = 1,
        /// <summary>
        /// gzip压缩的正文
        /// </summary>
        Gzip = 2,

        /// <summary>
        /// 明文
        /// </summary>
        PT = 4,

        /// <summary>
        /// 固定密钥更新
        /// </summary>
        ResetKey = 8

    }
}
