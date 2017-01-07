/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QZ.Instrument.Utility
{
    public class Util
    {
        public static unsafe string To_String_X(char* cha, int start, int end)
        {
            int len = end - start;
            char[] arr = new char[len];
            fixed (char* nch = arr)
            {
                char* nch_ptr = nch;
                char* ch_ptr = &(cha[start]);
                //int j = 0;

                if (((int)nch_ptr & 2) != 0)
                {
                    nch_ptr[0] = ch_ptr[0];
                    nch_ptr++;
                    ch_ptr++;
                    len--;
                }
                while (len >= 8)
                {
                    *((uint*)nch_ptr) = *((uint*)ch_ptr);
                    *((uint*)(nch_ptr + 2)) = *((uint*)(ch_ptr + 2));
                    *((uint*)(nch_ptr + 4)) = *((uint*)(ch_ptr + 4));
                    *((uint*)(nch_ptr + 6)) = *((uint*)(ch_ptr + 6));
                    nch_ptr += 8;
                    ch_ptr += 8;
                    len -= 8;

                }
                if ((len & 4) != 0)
                {
                    *((uint*)nch_ptr) = *((uint*)ch_ptr);
                    *((uint*)(nch_ptr + 2)) = *((uint*)(ch_ptr + 2));
                    nch_ptr += 4;
                    ch_ptr += 4;
                    len -= 4;
                }

                if ((len & 2) != 0)
                {
                    *((uint*)nch_ptr) = *((uint*)ch_ptr);
                    nch_ptr += 2;
                    ch_ptr += 2;
                    len -= 2;
                }

                if (len > 0)
                {
                    nch_ptr[0] = ch_ptr[0];

                }


            }
            return new string(arr);
        }

        public static int Length_Get(string input)
        {
            return string.IsNullOrEmpty(input) ? 0 : Encoding.Default.GetBytes(input).Length;
        }


        /// <summary>
        /// 16进制编码
        /// </summary>
        /// <param name="bs">输入流</param>
        /// <returns>16进制小写字符串</returns>
        public static unsafe string HexEncodingString(byte[] bs)
        {

            int len = bs.Length;
            fixed (byte* b = bs)
            {
                byte* bptr = b;
                int i = 0;
                int curr;
                int* tmp = &curr;
                char[] chs = new char[len * 2];
                fixed (char* charr = chs)
                {
                    char* chptr = charr;
                    //int j = 0;
                    while (i < len)
                    {
                        *tmp = bptr[0] & 0xf;
                        chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
                        chptr++;
                        *tmp = bptr[0] >> 4;
                        chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
                        chptr++;
                        bptr++;
                        i++;
                    }
                }
                return new string(chs);

            }
        }

        /// <summary>
        /// 16进制解码
        /// </summary>
        /// <param name="hexStringInput">16进制小写字符串</param>
        /// <returns>字节码</returns>
        public static unsafe byte[] DecodeHexEncodingString(string hexStringInput)
        {
            int strLen = hexStringInput.Length;
            if (strLen % 2 != 0)
                throw new ArgumentOutOfRangeException();
            int len = strLen / 2;
            fixed (char* charr = hexStringInput)
            {
                char* chptr = charr;
                int i = 0;


                byte[] bsArr = new byte[len];
                fixed (byte* bs = bsArr)
                {
                    byte* bptr = bs;
                    int low;
                    int high;

                    while (i < len)
                    {
                        low = chptr[0];
                        low = low < 0x3A ? low - 0x30 : low - 0x57;

                        chptr++;
                        high = chptr[0];
                        high = high < 0x3A ? high - 0x30 : high - 0x57;
                        chptr++;

                        bptr[0] = (byte)((high << 4) | low);
                        bptr++;
                        i++;
                    }
                }
                return bsArr;
            }
        }

        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;

            

            if(span.TotalDays>365)
            {
                double n = Math.Round(span.TotalDays / 365, 1);
                return n.ToString() + "年前";
            }
            if (span.TotalDays > 180)
            {
                return "半年前";
            }
            if (span.TotalDays > 60)
            {
                return "2月前";
            }
            else
            if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else
            if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else
            if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else
            if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else
            if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else
            if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else
            if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }

        public static string To_OpStatus(string od_ext)
        {
            string BusinessStatus = "在业";
            if (string.IsNullOrEmpty(od_ext) || od_ext.Contains("在业") || od_ext.Contains("迁入") || od_ext.Contains("确立") || od_ext.Contains("登记成立"))
            {
                BusinessStatus = "在业";
            }
            else if(od_ext.Contains("迁出"))
            {
                BusinessStatus = "迁出";
            }
            else if (od_ext.Contains("注销"))
            {
                BusinessStatus = "注销";
            }
            else if (od_ext.Contains("吊销") && od_ext.Contains("未注销"))
            {
                BusinessStatus = "吊销,未注销";
            }
            else if (od_ext.Contains("吊销"))
            {
                BusinessStatus = "吊销";
            }
            else if(od_ext.Contains("停业"))
            {
                BusinessStatus = "停业";
            }
            else if(od_ext.Contains("清算"))
            {
                BusinessStatus = "清算";
            }
            else if (od_ext.Contains("存续"))
            {
                BusinessStatus = "存续";
            }
            else if (od_ext.Contains("核准成立") || od_ext.Contains("核准设立"))
            {
                BusinessStatus = "核准设立";
            }
            else if (od_ext.Contains("解散"))
            {
                BusinessStatus = "解散";
            }
            else
            {
                BusinessStatus = "在业";
            }
            return BusinessStatus;
        }
    }
}
