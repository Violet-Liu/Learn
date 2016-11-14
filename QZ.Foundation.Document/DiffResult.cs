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
    /// 对比结果
    /// </summary>
    public class DiffResult
    {

        /// <summary>
        /// 相同区块根节点
        /// </summary>
        internal SameChunk chunksRoot;

        /// <summary>
        /// 旧版
        /// </summary>
        internal string baseVersion;

        /// <summary>
        /// 新版
        /// </summary>
        internal string newVersion;


        /// <summary>
        /// 旧版字符串
        /// </summary>
        public string BaseVersion
        {
            get
            {
                return baseVersion;
            }
        }

        /// <summary>
        /// 新版字符串
        /// </summary>
        public string NewVersion
        {
            get
            {
                return newVersion;
            }
        }


        /// <summary>
        /// 获取对比结果根节点
        /// </summary>
        public SameChunk ChunkRoot
        {
            get
            {
                return chunksRoot;
            }
        }


        /// <summary>
        /// 比较结果块
        /// </summary>
        private List<ChunkResult> chunkList;

        /// <summary>
        /// 获取比较结果
        /// </summary>
        public List<ChunkResult> ChunkList
        {
            get
            {
                if (chunkList != null)
                    return chunkList;
                chunkList = GetDiffResult();
                return chunkList;
            }
        }


        /// <summary>
        /// 获取对比结果
        /// </summary>
        /// <returns>返回对比结果</returns>
        internal List<ChunkResult> GetDiffResult()
        {



            List<ChunkResult> lst = new List<ChunkResult>();
            SameChunk curr = chunksRoot;
            SameChunk temp = null;

            int xlen = baseVersion.Length;
            int ylen = newVersion.Length;
            if (curr == null)
            {
                ChunkResult item = new ChunkResult();
                item.xStartIndex = 0;
                item.yStartIndex = 0;
                item.xEndIndex = xlen - 1;
                item.yEndIndex = ylen - 1;
                item.xString = baseVersion;
                item.yString = newVersion;
                item.sameChunk = false;
                lst.Add(item);
                return lst;


                //return kvList;
            }

            int[] xprevDiff;
            int[] yprevDiff;
            while (curr != null)
            {


                xprevDiff = curr.XPrevDiff();
                yprevDiff = curr.YPrevDiff();

                if (xprevDiff != null || yprevDiff != null)
                {
                    ChunkResult item = new ChunkResult();
                    if (xprevDiff != null)
                    {
                        item.xStartIndex = xprevDiff[0];
                        item.xEndIndex = xprevDiff[1];
                        item.xString = new string(baseVersion.ToCharArray(xprevDiff[0], xprevDiff[1] - xprevDiff[0] + 1));
                    }
                    else
                    {
                        item.xString = string.Empty;
                    }

                    if (yprevDiff != null)
                    {
                        item.yStartIndex = yprevDiff[0];
                        item.yEndIndex = yprevDiff[1];
                        item.yString = new string(newVersion.ToCharArray(yprevDiff[0], yprevDiff[1] - yprevDiff[0] + 1));
                    }
                    else
                    {
                        item.yString = string.Empty;
                    }
                    item.sameChunk = false;

                    lst.Add(item);

                }


                ChunkResult same = new ChunkResult();
                same.xStartIndex = curr.xStartIndex;
                same.xEndIndex = curr.xEndIndex;
                same.yStartIndex = curr.yStartIndex;
                same.yEndIndex = curr.yEndIndex;

                same.xString = new string(baseVersion.ToCharArray(same.xStartIndex, same.xEndIndex - same.xStartIndex + 1));
                same.yString = same.xString;

                same.sameChunk = true;



                lst.Add(same);
                temp = curr;
                curr = curr.next;






            }

            if (temp != null)
            {

                int[] xNextDiff = temp.XNextDiff(xlen);
                int[] yNextDiff = temp.YNextDiff(ylen);

                if (xNextDiff != null || yNextDiff != null)
                {
                    ChunkResult item = new ChunkResult();
                    if (xNextDiff != null)
                    {
                        item.xStartIndex = xNextDiff[0];
                        item.xEndIndex = xNextDiff[1];
                        item.xString = new string(baseVersion.ToCharArray(xNextDiff[0], xNextDiff[1] - xNextDiff[0] + 1));
                    }
                    else
                    {
                        item.xString = string.Empty;
                    }

                    if (yNextDiff != null)
                    {
                        item.yStartIndex = yNextDiff[0];
                        item.yEndIndex = yNextDiff[1];
                        item.yString = new string(newVersion.ToCharArray(yNextDiff[0], yNextDiff[1] - yNextDiff[0] + 1));
                    }
                    else
                    {
                        item.yString = string.Empty;
                    }
                    item.sameChunk = false;

                    lst.Add(item);
                }

            }


            return lst;
        }


        /// <summary>
        /// 复制一份字符串长度相等包含换行符和空白符的印象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CopyShaow(string input)
        {
            int len = input.Length;
            char[] charr = new char[len];
            char c;
            for (int i = 0; i < len; i++)
            {
                c = input[i];
                switch (c)
                {
                    case '\r':
                    case '\n':
                        charr[i] = c;
                        break;
                    default:
                        charr[i] = ' ';
                        break;
                }
            }
            return new string(charr);
        }


        /// <summary>
        /// 对比两个版本的字符串
        /// </summary>
        /// <param name="baseVersion">旧版</param>
        /// <param name="newVersion">新版</param>
        /// <param name="minSegments">最小相同区间偏移量(取值范围 (1-minStringLength))</param>
        /// <returns>对比结果对象</returns>
        public static unsafe DiffResult Diff(string baseVersion, string newVersion, int minSegments)
        {
            //比较文件

            fixed (char* xptr = baseVersion)
            {
                fixed (char* yptr = newVersion)
                {

                    //int minSegments = 100;

                    //x,y 对应的字节流长度
                    int xlen = baseVersion.Length;
                    int ylen = newVersion.Length;


                    //文档最后一次相同的地方
                    int xdiff = -1, ydiff = -1;

                    //遍历开始，和结束
                    int xi = 0, yi = 0;

                    //回溯索引
                    int byi, bxi;

                    //交叉缓冲区索引
                    int xbuf, ybuf;

                    //x，y 字节流对应的字节码
                    char xbyte, ybyte, inBackByte = '\0';

                    SameChunk root = null, prev = null, curr;

                    /*
                     * 
                     * 不匹配缓冲区，用来存储不匹配字节索引
                     * 大小为：256*4 占用1K内存
                     * xbuffer[x不匹配字节]=x_index;
                     * ybuffer[y不匹配字节]=y_index;
                     */

                    int[] xbuffer = new int[UInt16.MaxValue + 1];
                    int[] ybuffer = new int[UInt16.MaxValue + 1];


                    //一些临时变量
                    int xtmp = 0, ytmp = 0, elen = 0;

                    //下列变量标识当前状态是否为回溯
                    bool xInback = false;
                    int xInbackTmp = 0;
                    bool yInback = false;
                    int yInBackTmp = 0;

                    //在回溯失败重检有没有发现同项标识
                    bool find = false;

                    //确保不会溢出
                    while (xi < xlen && yi < ylen)
                    {

                        xbyte = xptr[xi];
                        ybyte = yptr[yi];

                        LABEL_NEQ:
                        if (xbyte != ybyte)
                        {

                            //记录缓冲区数据
                            bxi = xbuffer[xbyte];
                            byi = ybuffer[ybyte];


                            //交叉索引缓冲区数据，看是否可以回溯
                            xbuf = xbuffer[ybyte];
                            ybuf = ybuffer[xbyte];


                            //检查两个缓冲区，如果都存在，回溯最小的对象
                            if (xbuf > xdiff)
                            {
                                if (ybuf > ydiff)
                                {
                                    if (ybuf < xbuf)
                                        goto LABLE_BACK_Y;
                                }

                                goto LABLE_BACK_X;
                            }

                            if (ybuf > ydiff)
                            {
                                goto LABLE_BACK_Y;
                            }

                            goto LABLE_REC_BUF;


                            LABLE_BACK_Y:

                            //开始按XByte回溯Y

                            yInBackTmp = yi;
                            xInbackTmp = xi;
                            yi = ybuffer[xbyte];
                            yInback = true;
                            inBackByte = xbyte;
                            //避免覆盖重复序列
                            if (bxi < xdiff)
                            {
                                xbuffer[xbyte] = xi;
                            }

                            if (byi < ydiff)
                            {
                                ybuffer[ybyte] = yi;
                            }

                            goto LABLE_LPEQ;





                            LABLE_BACK_X:

                            //开始按YByte回溯X

                            xInbackTmp = xi;
                            yInBackTmp = yi;
                            xi = xbuffer[ybyte];
                            inBackByte = ybyte;
                            xInback = true;

                            //避免覆盖重复序列
                            if (bxi < xdiff)
                            {
                                xbuffer[xbyte] = xi;
                            }

                            if (byi < ydiff)
                            {
                                ybuffer[ybyte] = yi;
                            }

                            goto LABLE_LPEQ;



                            LABLE_REC_BUF:

                            //避免覆盖重复序列
                            if (bxi < xdiff)
                            {
                                xbuffer[xbyte] = xi;
                            }

                            if (byi < ydiff)
                            {
                                ybuffer[ybyte] = yi;
                            }
                            goto LABLE_LPED;
                        }

                        //当前字节码相等
                        LABLE_LPEQ:



                        //往上回溯匹配看看是不是有漏网的
                        xtmp = xi - 1;
                        ytmp = yi - 1;
                        while (xtmp > -1 && xtmp > xdiff && ytmp > -1 && ytmp > ydiff)
                        {
                            if (xptr[xtmp] != yptr[ytmp])
                                break;
                            xtmp--;
                            ytmp--;
                        }
                        xi = xtmp + 1;
                        yi = ytmp + 1;


                        //往下匹配
                        xtmp = xi + 1;
                        ytmp = yi + 1;
                        while (xtmp < xlen && ytmp < ylen)
                        {
                            xbyte = xptr[xtmp];
                            ybyte = yptr[ytmp];
                            if (xbyte != ybyte)
                                break;
                            xtmp++;
                            ytmp++;

                        }


                        elen = xtmp - xi;
                        if (elen > minSegments)
                        {
                            xdiff = xtmp - 1;
                            ydiff = ytmp - 1;
                            //切分相同项


                            curr = new SameChunk();
                            curr.xStartIndex = xi;
                            curr.xEndIndex = xdiff;
                            curr.yStartIndex = yi;
                            curr.yEndIndex = ydiff;

                            if (prev != null)
                            {
                                prev.next = curr;
                                curr.prev = prev;
                                prev = curr;
                            }
                            else
                            {
                                prev = curr;
                                if (root == null)
                                    root = prev;

                            }

                            //InternalAddChuncks(xdoc, xi, xtmp);

                            xi = xtmp;
                            yi = ytmp;

                            xInback = false;
                            yInback = false;

                            goto LABEL_NEQ;
                        }
                        else
                        {
                            //说明太短，不视为是相同区间（重复区间）

                            if (xInback)
                            {



                                //回溯失败，尝试向下找同项覆盖当前失败项
                                while (xi < xInbackTmp)
                                {
                                    if (xptr[xi] == inBackByte)
                                    {
                                        xbuffer[inBackByte] = xi;
                                        find = true;
                                        break;
                                    }
                                    xi++;
                                }

                                if (!find)
                                    xbuffer[inBackByte] = 0;
                                find = false;

                                xi = xInbackTmp;
                                yi = yInBackTmp;
                                xInback = false;
                                goto LABLE_LPED;
                            }
                            if (yInback)
                            {

                                while (yi < yInBackTmp)
                                {
                                    if (yptr[yi] == inBackByte)
                                    {
                                        ybuffer[inBackByte] = yi;
                                        find = true;
                                        break;
                                    }
                                    yi++;
                                }

                                if (!find)
                                    ybuffer[inBackByte] = 0;
                                find = false;

                                yi = yInBackTmp;
                                xi = xInbackTmp;
                                yInback = false;
                                goto LABLE_LPED;
                            }



                            xi = xtmp;
                            yi = ytmp;
                            continue;


                        }


                        LABLE_LPED:
                        xi++;
                        yi++;
                    }

                    DiffResult diff = new DiffResult();

                    diff.baseVersion = baseVersion;
                    diff.newVersion = newVersion;
                    diff.chunksRoot = root;

                    return diff;

                }
            }


        }





    }
}
