using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Strings.PatternMatching
{
    /// <summary>
    /// 简单模式匹配
    /// </summary>
    public class SimplePatternMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">主串</param>
        /// <param name="pat">模式串</param>
        /// <returns></returns>
        public static int Index(string str, string pat)
        {
            var i = 0;
            var j = 0;
            while (i < str.Length && j < pat.Length)
            {
                if (str[i] == pat[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    i = i - j + 1;
                    j = 0;
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
    }
}
