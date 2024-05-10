using Algorithms.Others;
using Algorithms.Strings.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Test.Strings.PatternMatching
{

    public class SimplePatternMatchingTest
    {
        /// <summary>
        /// 测试
        /// </summary>
        [Fact]
        public void DoTest()
        {
            // Arrange
            var str = "ABABAcdeABA";
            var pat = "de";
            var index = SimplePatternMatching.Index(str, pat);

            // Assert
            Assert.Equal(6, index);
        }
    }
}
