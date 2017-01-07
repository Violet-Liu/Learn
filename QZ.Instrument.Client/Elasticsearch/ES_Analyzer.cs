using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QZ.Foundation.Segment;

namespace QZ.Instrument.Client.Elasticsearch
{
    public class ES_Analyzer
    {



        public static Tuple<ES_Field[], ES_Field[]> Company_Analyze(string keyword)
        {
            for(int i = 0; i < keyword.Length; i++)
            {

            }
            return null;
        }

        public static void Sentence_Analyze(string keyword)
        {
            var length = keyword.Length;
            for(int i = 0; i < length; i++)
            {

            }
        }

        public unsafe static Tuple<string, string> Area_Pre_Subfix(string input)
        {
            string areaResult = null;
            var parts = new Company_Parts();
            var states = new List<ComType>();
            var hasStock = false;
            ComType t1, t2, t3;
            bool connectFlag = false;
            t1 = t2 = t3 = ComType.N;
            int length = input.Length;
            var resultList = new List<string>();
            int start = 0;
            int end = 0;
            fixed (char* ptr = input)
            {
                char* cursor = ptr;
                
                for(int i = 0; i < length; i++)
                {
                    if (ptr[i] == '股' && i + 1 < length && ptr[i + 1] == '份')
                    {
                        if(t1 == ComType.N)
                        {
                            t1 = ComType.S;
                        }
                        i += 1;
                        continue;
                    }
                    if(ptr[i] == '限')
                    {
                        if(ptr[i - 1] == '有')
                        {
                            if (i + 1 < length && ptr[i + 1] == '公')
                            {
                                if (i + 2 < length && ptr[i + 2] == '司')
                                {
                                    parts.type = "有限公司";
                                    i += 2;
                                    continue;
                                }
                                else
                                {
                                    parts.type = "有限公";
                                    i += 1;
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            if(i + 2 < length && ptr[i + 1] == '公' && ptr[i + 2] == '司')
                            {
                                parts.type = "限公司";
                                i += 2;
                                continue;
                            }
                        }

                        if (ptr[i - 1] == '有' && i + 1 < length && ptr[i + 1] == '公')
                        {
                            if(i + 2 < length && ptr[i + 2] == '司')
                            {
                                parts.type = "有限公司";
                                i += 2;
                                continue;
                            }
                            else
                            {
                                parts.type = "有限公";
                                i += 1;
                                continue;
                            }
                        }
                        // we think that "有限" may be a company identity
                    }

                    int relatedEnd = 0;
                    
                    start = i;
                    

                    if(string.IsNullOrEmpty(areaResult))
                    {
                        Area_Match(ptr + i, Resource.Root, ref relatedEnd, ref areaResult);
                        if (!string.IsNullOrEmpty(areaResult))
                        {
                            end = start + relatedEnd;
                        }
                    }
                    cursor++;
                }
                
            }
            return null;
        }

        /// <summary>
        /// Forward-match an area name
        /// </summary>
        /// <param name="ptr">source string, where we want to find an area name from</param>
        /// <param name="node">area tree object</param>
        /// <param name="curIndex">current index char in the 'ptr', which to be matched</param>
        /// <param name="result">match succeed -> matched string; otherwise the origin value of 'result'</param>
        public unsafe static void Area_Match(char* ptr, AreaNode node, ref int curIndex, ref string result)
        {
            char c = ptr[curIndex];
            if (c == '省' || c == '市' || c == '县')
            {
                result = String_Extract(ptr, curIndex);
                return;
            }

            if (!node.Areas.Any())
            {
                result = String_Extract(ptr, curIndex);
                return;
            }
            // (c != ' ' && !node.Areas.ContainsKey(c))
            if (c == '\0' || !node.Areas.ContainsKey(c) || curIndex > 5) return;


            curIndex++;
            Area_Match(ptr, node.Areas[c], ref curIndex, ref result);
        }

        /// <summary>
        /// Extract a string from a given char pointer. The string to be extracted is located in a range of [0, excludeIndex)
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="excludeIndex">end index of the matched sub-string, where the char holden will be excluded in the matched string</param>
        /// <returns></returns>
        private unsafe static string String_Extract(char* ptr, int excludeIndex)
        {
            var arr = new char[excludeIndex];
            int j = 0;
            for (int i = 0; i < excludeIndex; i++)
            {
                if (ptr[i] != ' ')
                    arr[j++] = ptr[i];
            }
            arr[j] = '\0';
            return new string(arr);
        }

        private unsafe static void Type_Match(char* ptr, ComType t1, ComType t2, ComType t3)
        {
            
        }
    }
}
