using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Model
{
    public class ScalaInteger
    {
        private const long m = 0x3F;
        private const long vm = 0xF;
        public static long ToFixValue(string input)
        {
            long v = 0;
            long sum = 0;
            for(int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c <= '9')
                {
                    v = c - '0';
                }
                else if (c < 'a')
                {
                    v = c - 'A' + 10;
                }
                else
                {
                    throw new Exception("oc_code has lowercase char");
                }
                //else
                //    v = c - 'a' + 10;

                sum |= (v & m) << (i * 6);
            }
            return sum;
        }



        public static string FromFixValue(long val)
        {
            var cs = new char[9];
            for(int i = 0; i < 9; i++)
            {
                var v = (int)((val & (m << (i * 6))) >> (i * 6));
                if(v < 10)
                {
                    cs[i] = (char)('0' + v);
                }
                else if(v < 36)
                {
                    cs[i] = (char)('A' + v - 10);
                }
                else
                {
                    throw new Exception("Value can not map into 0-9,A-Z");
                }
            }
            return new string(cs);
        }

        /// <summary>
        /// Length-variable integer
        /// if char is in [0-9,A-F], then use 5 bits to represent it, else if char is in [G-Z], then use 7 bits to represent it
        /// From right to left, the fifth bit is a flag, if it is 1, then 7 bits, otherwise, 5 bits
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long ToVInt(string input)
        {
            long v = 0;
            int count = 0;  // 两单位长度数值的个数
            long sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c <= '9')
                {
                    v = ((long)(c - '0') & 0xF) << (i * 5 + count * 2);
                }
                else if(c < 'G')
                {
                    v = ((long)(c - 'A' + 10) & 0xF) << (i * 5 + count * 2);
                }
                else
                {
                    var val = (long)(c - 'A' + 10);
                    var low = val & 0xF;
                    var high = val >> 4;
                    v = ((high << 5) | (0x10 | low)) << (i * 5 + count * 2);
                    count++;
                }
                sum |= v;
            }
            return sum;
        }

        public static string FromVInt(long val)
        {
            int count = 0;
            var cs = new char[9];
            for (int i = 0; i < 9; i++)
            {
                var v = (val >> (i * 5 + count * 2)) & 0x1F;
                if((v & 0x10) != 0)         // 两单位数值，对应字符> 'F'
                {
                    
                    var low = v & 0xF;
                    var high = (val >> ((i + 1) * 5 + count * 2)) & 0x3;
                    int t = (int)((high << 4) | low);
                    cs[i] = (char)(t - 15 + 'F');
                    count++;
                }
                else
                {
                    if(v > 9)
                    {
                        cs[i] = (char)(v - 10 + 'A');
                    }
                    else
                    {
                        cs[i] = (char)(v + '0');
                    }
                }
            }
            return new string(cs);
        }

        /// <summary>
        /// 0-9,A-Z compose a 36 base number system
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long ToLong(string input)
        {
            long sum = 0;
            for(int i = 0; i < input.Length; i++)
            {
                if(input[i] <= '9')
                {
                    sum += (long)(input[i] - '0') * (36L ^ i);
                }
                else
                {
                    sum += (long)(input[i] - 'A' + 10) * (36L ^ i);
                }
            }
            return sum;
        }

        public static long FromLong(long val)
        {
            throw new NotImplementedException();
        }
    }
}
