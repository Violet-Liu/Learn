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
    public class TextDiff
    {

        /// <summary>
        /// 文本相似度比较
        /// </summary>
        /// <param name="baseVersion"></param>
        /// <param name="newVersion"></param>
        /// <returns>返回值为  (baseVersion.length + newVersion.length - different.length)/(baseVersion.length + newVersion.length)</returns>
        public static decimal Similar(string baseVersion, string newVersion)
        {

            if (baseVersion == newVersion)
                return 1;

            if (string.IsNullOrEmpty(baseVersion) || string.IsNullOrEmpty(newVersion))
                return 0;

            int diffLength = 0;

            DiffResult result = DiffResult.Diff(baseVersion, newVersion, 1);

            List<ChunkResult> chunks = result.ChunkList;

            foreach (ChunkResult chunk in chunks)
            {

                if (!chunk.sameChunk)
                {

                    if (chunk.xString.Length < 1)
                    {
                        diffLength += DiffResult.CopyShaow(chunk.yString).Length;
                        diffLength += chunk.yString.Length;

                    }
                    else if (chunk.yString.Length < 1)
                    {
                        diffLength += chunk.xString.Length;
                        diffLength += DiffResult.CopyShaow(chunk.xString).Length;

                    }
                    else
                    {

                        diffLength += chunk.xString.Length;
                        diffLength += chunk.yString.Length;
                    }

                }


            }

            return Decimal.Divide(baseVersion.Length + newVersion.Length - diffLength, baseVersion.Length + newVersion.Length);
        }
    }
}
