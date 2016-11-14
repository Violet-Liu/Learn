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
    /// 区块结果
    /// </summary>
    public class ChunkResult
    {
        /// <summary>
        /// x开始索引
        /// </summary>
        internal int xStartIndex;

        /// <summary>
        /// y开始索引
        /// </summary>
        internal int yStartIndex;

        /// <summary>
        /// x结束索引
        /// </summary>
        internal int xEndIndex;

        /// <summary>
        /// y结束索引
        /// </summary>
        internal int yEndIndex;


        /// <summary>
        /// x开始索引
        /// </summary>
        public int XStartIndex
        {
            get
            {
                return xStartIndex;
            }
        }

        /// <summary>
        /// y开始索引
        /// </summary>
        public int YStartIndex
        {
            get
            {
                return yStartIndex;
            }
        }

        /// <summary>
        /// x结束索引
        /// </summary>
        public int XEndIndex
        {
            get
            {
                return xEndIndex;
            }
        }

        /// <summary>
        /// y结束索引
        /// </summary>
        public int YEndIndex
        {
            get
            {
                return yEndIndex;
            }
        }



        /// <summary>
        /// 区间字符串
        /// </summary>
        internal string xString;

        /// <summary>
        /// x区间字符串
        /// </summary>
        public string XString
        {
            get
            {
                return xString;
            }
        }

        /// <summary>
        /// y区间字符串
        /// </summary>
        internal string yString;

        /// <summary>
        /// y区间字符串
        /// </summary>
        public string YString
        {
            get
            {
                return yString;
            }
        }


        /// <summary>
        /// 是否是相同区间
        /// </summary>
        internal bool sameChunk;

        /// <summary>
        /// 是否是相同区块
        /// </summary>
        public bool SameChunk
        {
            get
            {
                return sameChunk;
            }
        }






    }
}
