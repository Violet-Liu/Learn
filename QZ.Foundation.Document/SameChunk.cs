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

namespace QZ.Foundation.Document
{
    /// <summary>
    /// 相同区块双向链表
    /// </summary>
    public class SameChunk
    {

        /// <summary>
        /// 旧版开始索引
        /// </summary>
        internal int xStartIndex;

        /// <summary>
        /// 旧版结束索引
        /// </summary>
        internal int xEndIndex;

        /// <summary>
        /// 新版开始索引
        /// </summary>
        internal int yStartIndex;

        /// <summary>
        /// 新版结束索引
        /// </summary>
        internal int yEndIndex;


        /// <summary>
        /// 前一个相同区块
        /// </summary>
        internal SameChunk prev;

        /// <summary>
        /// 后一个相同区块
        /// </summary>
        internal SameChunk next;



        /// <summary>
        /// 获取开始索引
        /// </summary>
        public int XStartIndex
        {
            get
            {
                return xStartIndex;
            }
        }

        /// <summary>
        /// 获取结束索引
        /// </summary>
        public int XEndIndex
        {
            get
            {
                return xEndIndex;
            }
        }

        /// <summary>
        /// 获取新版的开始索引
        /// </summary>
        public int YStartIndex
        {
            get
            {
                return yStartIndex;
            }
        }

        /// <summary>
        /// 获取新版的结束索引
        /// </summary>
        public int YEndIndex
        {
            get
            {
                return yEndIndex;
            }
        }


        /// <summary>
        /// 获取前一个相同区块
        /// </summary>
        public SameChunk Prev
        {
            get
            {
                return prev;
            }
        }

        /// <summary>
        /// 获取后一个相同区块
        /// </summary>
        public SameChunk Next
        {
            get
            {
                return next;
            }
        }




        /// <summary>
        /// （旧版，被比较对象）获取前面的不匹配的索引区间
        /// </summary>
        /// <returns>不匹配区间 int[len=2] int[0]不匹配区间开始 int[1] 不匹配区间结束 没有不匹配区间了返回NULL </returns>
        public int[] XPrevDiff()
        {

            if (prev != null)
            {
                int difflen = xStartIndex - prev.xEndIndex;
                if (difflen > 1)
                {
                    return new int[2] { prev.xEndIndex + 1, xStartIndex - 1 };
                }
            }
            else
            {

                if (xStartIndex != 0)
                {
                    return new int[2] { 0, xStartIndex - 1 };
                }

            }
            return null;

        }

        /// <summary>
        /// (新版，比较对象) 获取前面的不匹配的索引区间
        /// </summary>
        /// <returns>不匹配区间 int[len=2] int[0]不匹配区间开始 int[1] 不匹配区间结束 没有不匹配区间了返回NULL </returns>
        public int[] YPrevDiff()
        {

            if (prev != null)
            {
                int difflen = yStartIndex - prev.yEndIndex;
                if (difflen > 1)
                {
                    return new int[2] { prev.yEndIndex + 1, yStartIndex - 1 };
                }
            }
            else
            {

                if (yStartIndex != 0)
                {
                    return new int[2] { 0, yStartIndex - 1 };
                }

            }
            return null;

        }

        /// <summary>
        /// （旧版，被比较对象）获取下一个不匹配的索引区间
        /// </summary>
        /// <param name="maxLen">最大的一个字符串对象的长度</param>
        /// <returns>不匹配区间 int[len=2] int[0]不匹配区间开始 int[1] 不匹配区间结束 没有不匹配区间了返回NULL </returns>
        public int[] XNextDiff(int maxLen)
        {
            if (next != null)
            {
                int diffLen = xEndIndex - next.xStartIndex;
                if (diffLen > 1)
                {
                    return new int[2] { xEndIndex + 1, next.xStartIndex - 1 };
                }

            }
            else
            {
                if (xEndIndex < (maxLen - 1))
                {
                    return new int[2] { xEndIndex + 1, maxLen - 1 };
                }
            }
            return null;
        }

        /// <summary>
        /// (新版，比较对象)获取下一个不匹配的索引区间
        /// </summary>
        /// <param name="maxLen">最大的一个字符串对象的长度</param>
        /// <returns>不匹配区间 int[len=2] int[0]不匹配区间开始 int[1] 不匹配区间结束 没有不匹配区间了返回NULL </returns>
        public int[] YNextDiff(int maxLen)
        {
            if (next != null)
            {
                int diffLen = yEndIndex - next.yStartIndex;
                if (diffLen > 1)
                {
                    return new int[2] { yEndIndex + 1, next.yStartIndex - 1 };
                }

            }
            else
            {
                if (yEndIndex < (maxLen - 1))
                {
                    return new int[2] { yEndIndex + 1, maxLen - 1 };
                }
            }
            return null;
        }


    }
}
