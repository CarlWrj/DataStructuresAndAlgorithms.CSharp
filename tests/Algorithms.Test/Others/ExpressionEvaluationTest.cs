using Algorithms.Others;
using DataStructures.Lists.LinkLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Test.Others
{
    /// <summary>
    /// 表达式求值测试
    /// </summary>
    public class ExpressionEvaluationTest
    {
        /// <summary>
        /// 测试
        /// </summary>
        [Fact]
        public void DoTest()
        {
            var expectValue = (11 + 22 * (44 - 33) - 55) / 1;
            var actualValue = ExpressionEvaluation.Calculate("(11 + 22 * (44 - 33) - 55) / 1");
            Assert.Equal(expectValue, actualValue);
        }
    }
}
