using DataStructures.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Test.Lists
{
    /// <summary>
    /// 顺序表测试
    /// </summary>
    public class SequenceListTest
    {
        [Fact]
        public static void DoTest()
        {
            var sequenceList = new SequenceList<int>(10);

            //插入
            for (int i = 1; i <= 10; i++)
            {
                sequenceList.Insert(i, i);
            }
            sequenceList.IncreaseSize(5);
            for (int i = 11; i <= 15; i++)
            {
                sequenceList.Insert(i, i);
            }
            Assert.Equal(15, sequenceList.Length);

            //查找
            for (int i = 1; i <= 15; i++)
            {
                Assert.Equal(i, sequenceList[i]);
            }
            for (int i = 1; i <= 15; i++)
            {
                var index = sequenceList.Locate(i);
                Assert.Equal(i, index);
            }

            //删除
            for (int i = 1; i <= 15; i++)
            {
                sequenceList.Delete(1);
            }
            Assert.Equal(0, sequenceList.Length);
        }
    }
}
