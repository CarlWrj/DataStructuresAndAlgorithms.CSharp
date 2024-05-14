using Algorithms.Others;
using Algorithms.Strings.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Test.Strings.PatternMatching
{

    public class KMPTest
    {
        /// <summary>
        /// 测试
        /// </summary>
        [Fact]
        public void DoTest()
        {
            var str = "a";
            var pat = "a";
            var index = KMP.Index(str, pat);
            Assert.Equal(0, index);

            str = "a";
            pat = "b";
            index = KMP.Index(str, pat);
            Assert.Equal(-1, index);

            str = "ab";
            pat = "a";
            index = KMP.Index(str, pat);
            Assert.Equal(0, index);

            str = "ab";
            pat = "b";
            index = KMP.Index(str, pat);
            Assert.Equal(1, index);

            str = "ab";
            pat = "c";
            index = KMP.Index(str, pat);
            Assert.Equal(-1, index);

            str = "abbababaababbba";
            pat = "ababaa";
            index = KMP.Index(str, pat);
            Assert.Equal(3, index);

            str = "abbababababbba";
            pat = "ababaa";
            index = KMP.Index(str, pat);
            Assert.Equal(-1, index);
        }
    }
}
