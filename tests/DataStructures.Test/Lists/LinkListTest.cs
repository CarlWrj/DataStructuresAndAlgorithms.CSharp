using DataStructures.Lists;
using DataStructures.Lists.LinkLists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Test.Lists
{
    /// <summary>
    /// 链表测试
    /// </summary>
    public class LinkListTest
    {

        /// <summary>
        /// 有头节点
        /// </summary>
        [Fact]
        public void HasHeadNodeTest()
        {
            var list = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }
            var linkList = new LinkHasHeadList<int>();

            //初始化
            var headInsertList = LinkHasHeadList<int>.HeadInsert(list);
            for (int i = 1, j = 10; i <= 10; i++, j--)
            {
                Assert.Equal(i, headInsertList[j].Data);
            }
            var tailInsertList = LinkHasHeadList<int>.TailInsert(list);
            for (int i = 1; i <= 10; i++)
            {
                Assert.Equal(i, tailInsertList[i].Data);
            }

            //插入
            tailInsertList.Insert(1, 1);
            tailInsertList.Delete(1);
            tailInsertList.Insert(10, 11);
            Assert.Equal(11, tailInsertList[11].Data);
            tailInsertList.InsertNextNode(tailInsertList[11], 13);
            Assert.Equal(13, tailInsertList[12].Data);
            tailInsertList.InsertPriorNode(tailInsertList[12], 12);
            Assert.Equal(12, tailInsertList[12].Data);

            //删除
            tailInsertList.Delete(13);
            Assert.Equal(12, tailInsertList.Length);
            tailInsertList.DeleteNode(tailInsertList[1]);
            Assert.Equal(11, tailInsertList.Length);

            //查找
            var node = tailInsertList.Locate(2);
            Assert.NotNull(node);
            Assert.Equal(2, node.Data);
        }

        /// <summary>
        /// 没有头节点
        /// </summary>
        [Fact]
        public void NotUsedHeadNodeTest()
        {
            var list = new List<int>();
            for (int i = 0; i <= 9; i++)
            {
                list.Add(i);
            }
            var linkList = new LinkNotHasHeadList<int>();

            //初始化
            var headInsertList = LinkNotHasHeadList<int>.HeadInsert(list);
            for (int i = 0, j = 9; i < 10; i++, j--)
            {
                Assert.Equal(i, headInsertList[j].Data);
            }
            var tailInsertList = LinkNotHasHeadList<int>.TailInsert(list);
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(i, tailInsertList[i].Data);
            }

            //插入
            tailInsertList.Insert(0, 0);
            tailInsertList.Delete(0);
            tailInsertList.Insert(9, 10);
            Assert.Equal(10, tailInsertList[10].Data);
            tailInsertList.InsertNextNode(tailInsertList[10], 12);
            Assert.Equal(12, tailInsertList[11].Data);
            tailInsertList.InsertPriorNode(tailInsertList[11], 11);
            Assert.Equal(12, tailInsertList[12].Data);

            //删除
            tailInsertList.Delete(12);
            Assert.Equal(12, tailInsertList.Length);
            tailInsertList.DeleteNode(tailInsertList[0]);
            Assert.Equal(11, tailInsertList.Length);

            //查找
            var node = tailInsertList.Locate(2);
            Assert.NotNull(node);
            Assert.Equal(2, node.Data);
        }
    }
}
