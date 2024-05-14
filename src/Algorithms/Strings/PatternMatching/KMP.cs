using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Strings.PatternMatching
{
    /// <summary>
    /// KMP算法
    /// </summary>
    public class KMP
    {
        /// <summary>
        /// 匹配字符串
        /// </summary>
        /// <param name="str">主串</param>
        /// <param name="pat">模式串</param>
        /// <returns></returns>
        public static int Index(string str, string pat)
        {
            if (str.Length == 1 && pat.Length == 1)
            {
                if (str == pat)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }

            var i = 0;
            var j = 0;
            var next = GetNextval(pat);

            while (i < str.Length && j < pat.Length)
            {
                if (j == -1 || str[i] == pat[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = next[j];
                }
            }

            if (j >= pat.Length)
            {
                return i - pat.Length;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取Next数组
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int[] GetNext(string pattern)
        {
            var kmpTable = new int[pattern.Length];

            if (kmpTable.Length < 2)
            {
                if (kmpTable.Length > 0)
                    kmpTable[0] = -1;

                return kmpTable;
            }

            int tableIndex = 2; // current position in table for computation
            int patSubstrIndex = 0; // index in the pattern of the current substring

            //头2个值固定为-1和0
            kmpTable[0] = -1;

            // Build table
            while (tableIndex < kmpTable.Length)
            {
                // If the substring continues
                if (pattern[tableIndex - 1] == pattern[patSubstrIndex])
                {
                    kmpTable[tableIndex++] = ++patSubstrIndex;
                }
                // It does not but we can fall back
                else if (patSubstrIndex != 0)
                {
                    patSubstrIndex = kmpTable[patSubstrIndex];
                }
                // If we ran out of candidates
                else
                {
                    kmpTable[tableIndex++] = 0;
                }
            }

            return kmpTable;
        }

        /// <summary>
        /// 获取Nextval数组
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int[] GetNextval(string pattern)
        {
            var next = GetNext(pattern);
            var nextval = new int[pattern.Length];
            nextval[0] = next[0];
            for (int i = 1; i < pattern.Length; i++)
            {
                if (pattern[next[i]] == pattern[i])
                {
                    nextval[i] = nextval[next[i]];
                }
                else
                {
                    nextval[i] = next[i];
                }
            }

            return nextval;
        }
    }
}
