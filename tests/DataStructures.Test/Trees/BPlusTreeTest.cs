using DataStructures.Trees.BPlusTrees;
using DataStructures.Trees.BTrees;

namespace DataStructures.Test.Trees
{
    public class BPlusTreeTest
    {
        /// <summary>
        /// 创建测试
        /// </summary>
        /// <returns></returns>
        [Fact]
        public BPlusTree<int> CreateTestBPlusTree()
        {
            BPlusTree<int> bPlusTree = new BPlusTree<int>(3);

            bPlusTree.Insert(1);
            bPlusTree.Insert(2);
            bPlusTree.Insert(4);
            /***************************************
             **
             **    [1 , 2 , 4 ]
             **
             ***************************************
             */
            Assert.Equal(1, bPlusTree.Root.Keys[0]);
            Assert.Equal(2, bPlusTree.Root.Keys[1]);
            Assert.Equal(4, bPlusTree.Root.Keys[2]);

            //向上分裂
            bPlusTree.Insert(3);
            /***************************************
             **
             **                    [2,     4]
             **                     |      |
             **                     |      |
             **                [1, 2] [3, 4]
             **
             ***************************************
             */
            Assert.Equal(2, bPlusTree.Root.Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Keys[1]);
            Assert.Equal(1, bPlusTree.Root.Children[0].Keys[0]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Keys[1]);
            Assert.Equal(3, bPlusTree.Root.Children[1].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[1].Keys[1]);

            //向上分裂
            bPlusTree.Insert(50);
            bPlusTree.Insert(40);
            /***************************************
             **
             **                    [2,      4,      50]
             **                     |       |        |
             **                     |       |        |
             **              [1 , 2 ] [3 , 4] [40 , 50]
             **
             ***************************************
             */
            Assert.Equal(2, bPlusTree.Root.Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Keys[1]);
            Assert.Equal(50, bPlusTree.Root.Keys[2]);
            Assert.Equal(1, bPlusTree.Root.Children[0].Keys[0]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Keys[1]);
            Assert.Equal(3, bPlusTree.Root.Children[1].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[1].Keys[1]);
            Assert.Equal(40, bPlusTree.Root.Children[2].Keys[0]);
            Assert.Equal(50, bPlusTree.Root.Children[2].Keys[1]);

            //向上分裂
            bPlusTree.Insert(10);
            bPlusTree.Insert(15);
            bPlusTree.Insert(30);
            bPlusTree.Insert(35);
            /***************************************
             **
             *                           [4,                          50]
             *                            |                             |
             *                            |                             |
             **                    [2,    4]       [15,      35,      50]
             **                     |     |         |         |         |
             **                     |     |         |         |         |
             **                [1, 2] [3, 4]  [10, 15]  [30,35]  [40, 50]
             **
             ***************************************
             */
            Assert.Equal(4, bPlusTree.Root.Keys[0]);
            Assert.Equal(50, bPlusTree.Root.Keys[1]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Keys[1]);
            Assert.Equal(15, bPlusTree.Root.Children[1].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Keys[1]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Keys[2]);
            Assert.Equal(1, bPlusTree.Root.Children[0].Children[0].Keys[0]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Children[0].Keys[1]);
            Assert.Equal(3, bPlusTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Children[1].Keys[1]);
            Assert.Equal(10, bPlusTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(15, bPlusTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(30, bPlusTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Children[1].Keys[1]);
            Assert.Equal(40, bPlusTree.Root.Children[1].Children[2].Keys[0]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[2].Keys[1]);

            bPlusTree.Insert(60);
            /***************************************
             **
             *                           [4,                            60]
             *                            |                               |
             *                            |                               |
             **                    [2,    4]       [15,      35,        60]
             **                     |     |         |         |           |
             **                     |     |         |         |           |
             **                [1, 2] [3, 4]  [10, 15]  [30,35]  [40,50,60]
             **
             ***************************************
             */
            Assert.Equal(4, bPlusTree.Root.Keys[0]);
            Assert.Equal(60, bPlusTree.Root.Keys[1]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Keys[1]);
            Assert.Equal(15, bPlusTree.Root.Children[1].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Keys[1]);
            Assert.Equal(60, bPlusTree.Root.Children[1].Keys[2]);
            Assert.Equal(1, bPlusTree.Root.Children[0].Children[0].Keys[0]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Children[0].Keys[1]);
            Assert.Equal(3, bPlusTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Children[1].Keys[1]);
            Assert.Equal(10, bPlusTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(15, bPlusTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(30, bPlusTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Children[1].Keys[1]);
            Assert.Equal(40, bPlusTree.Root.Children[1].Children[2].Keys[0]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[2].Keys[1]);
            Assert.Equal(60, bPlusTree.Root.Children[1].Children[2].Keys[2]);

            bPlusTree.Insert(65);
            bPlusTree.Insert(20);
            bPlusTree.Insert(25);
            /***************************************
             **
             *                           [4,                         35,              65]
             *                            |                           |                 |
             *                            |                           |                 |
             **                    [2,    4]       [15,     25,     35]       [50,    65]
             **                     |     |         |        |        |        |        |
             **                     |     |         |        |        |        |        |
             **                [1, 2] [3, 4]  [10, 15] [20,25]  [30,35]  [40,50]  [60,65]
             **
             ***************************************
             */
            Assert.Equal(4, bPlusTree.Root.Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Keys[1]);
            Assert.Equal(65, bPlusTree.Root.Keys[2]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Keys[1]);
            Assert.Equal(15, bPlusTree.Root.Children[1].Keys[0]);
            Assert.Equal(25, bPlusTree.Root.Children[1].Keys[1]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Keys[2]);
            Assert.Equal(50, bPlusTree.Root.Children[2].Keys[0]);
            Assert.Equal(65, bPlusTree.Root.Children[2].Keys[1]);
            Assert.Equal(1, bPlusTree.Root.Children[0].Children[0].Keys[0]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Children[0].Keys[1]);
            Assert.Equal(3, bPlusTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Children[1].Keys[1]);
            Assert.Equal(10, bPlusTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(15, bPlusTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(20, bPlusTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(25, bPlusTree.Root.Children[1].Children[1].Keys[1]);
            Assert.Equal(30, bPlusTree.Root.Children[1].Children[2].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Children[2].Keys[1]);
            Assert.Equal(40, bPlusTree.Root.Children[2].Children[0].Keys[0]);
            Assert.Equal(50, bPlusTree.Root.Children[2].Children[0].Keys[1]);
            Assert.Equal(60, bPlusTree.Root.Children[2].Children[1].Keys[0]);
            Assert.Equal(65, bPlusTree.Root.Children[2].Children[1].Keys[1]);

            bPlusTree.Insert(70);
            bPlusTree.Insert(75);
            bPlusTree.Insert(80);
            bPlusTree.Insert(85);
            /***************************************
             **
             *                                                      [35,                                85]
             *                                                        |                                   |
             *                                                        |                                   |
             *                           [4,                        35]               [65,              85]
             *                            |                           |                 |                 |
             *                            |                           |                 |                 |
             **                    [2,    4]       [15,     25,     35]       [50,    65]      [75,     85]
             **                     |     |         |        |        |        |        |        |        |
             **                     |     |         |        |        |        |        |        |        |
             **                [1, 2] [3, 4]  [10, 15] [20,25]  [30,35]  [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */
            Assert.Equal(35, bPlusTree.Root.Keys[0]);
            Assert.Equal(85, bPlusTree.Root.Keys[1]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[0].Keys[1]);
            Assert.Equal(65, bPlusTree.Root.Children[1].Keys[0]);
            Assert.Equal(85, bPlusTree.Root.Children[1].Keys[1]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Children[0].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Children[0].Keys[1]);
            Assert.Equal(15, bPlusTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(25, bPlusTree.Root.Children[0].Children[1].Keys[1]);
            Assert.Equal(35, bPlusTree.Root.Children[0].Children[1].Keys[2]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(65, bPlusTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(75, bPlusTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(85, bPlusTree.Root.Children[1].Children[1].Keys[1]);
            Assert.Equal(1, bPlusTree.Root.Children[0].Children[0].Children[0].Keys[0]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Children[0].Children[0].Keys[1]);
            Assert.Equal(3, bPlusTree.Root.Children[0].Children[0].Children[1].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Children[0].Children[1].Keys[1]);
            Assert.Equal(10, bPlusTree.Root.Children[0].Children[1].Children[0].Keys[0]);
            Assert.Equal(15, bPlusTree.Root.Children[0].Children[1].Children[0].Keys[1]);
            Assert.Equal(20, bPlusTree.Root.Children[0].Children[1].Children[1].Keys[0]);
            Assert.Equal(25, bPlusTree.Root.Children[0].Children[1].Children[1].Keys[1]);
            Assert.Equal(30, bPlusTree.Root.Children[0].Children[1].Children[2].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[0].Children[1].Children[2].Keys[1]);
            Assert.Equal(40, bPlusTree.Root.Children[1].Children[0].Children[0].Keys[0]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[0].Children[0].Keys[1]);
            Assert.Equal(60, bPlusTree.Root.Children[1].Children[0].Children[1].Keys[0]);
            Assert.Equal(65, bPlusTree.Root.Children[1].Children[0].Children[1].Keys[1]);
            Assert.Equal(70, bPlusTree.Root.Children[1].Children[1].Children[0].Keys[0]);
            Assert.Equal(75, bPlusTree.Root.Children[1].Children[1].Children[0].Keys[1]);
            Assert.Equal(80, bPlusTree.Root.Children[1].Children[1].Children[1].Keys[0]);
            Assert.Equal(85, bPlusTree.Root.Children[1].Children[1].Children[1].Keys[1]);

            bPlusTree.Insert(17);
            bPlusTree.Insert(22);
            /***************************************
             **
             *                                                               [35,                                85]
             *                                                                 |                                   |
             *                                                                 |                                   |
             *                           [4,                20,              35]               [65,              85]
             *                            |                  |                 |                 |                 |
             *                            |                  |                 |                 |                 |
             **                    [2,    4]       [15,    20]      [25,     35]       [50,    65]      [75,     85]
             **                     |     |         |        |        |        |        |        |        |        |
             **                     |     |         |        |        |        |        |        |        |        |
             **                [1, 2] [3, 4]  [10, 15] [17,20]  [22,25]  [30,35]  [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */
            Assert.Equal(35, bPlusTree.Root.Keys[0]);
            Assert.Equal(85, bPlusTree.Root.Keys[1]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Keys[0]);
            Assert.Equal(20, bPlusTree.Root.Children[0].Keys[1]);
            Assert.Equal(35, bPlusTree.Root.Children[0].Keys[2]);
            Assert.Equal(65, bPlusTree.Root.Children[1].Keys[0]);
            Assert.Equal(85, bPlusTree.Root.Children[1].Keys[1]);

            Assert.Equal(2, bPlusTree.Root.Children[0].Children[0].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Children[0].Keys[1]);
            Assert.Equal(15, bPlusTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(20, bPlusTree.Root.Children[0].Children[1].Keys[1]);
            Assert.Equal(25, bPlusTree.Root.Children[0].Children[2].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[0].Children[2].Keys[1]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(65, bPlusTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(75, bPlusTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(85, bPlusTree.Root.Children[1].Children[1].Keys[1]);
            Assert.Equal(1, bPlusTree.Root.Children[0].Children[0].Children[0].Keys[0]);
            Assert.Equal(2, bPlusTree.Root.Children[0].Children[0].Children[0].Keys[1]);
            Assert.Equal(3, bPlusTree.Root.Children[0].Children[0].Children[1].Keys[0]);
            Assert.Equal(4, bPlusTree.Root.Children[0].Children[0].Children[1].Keys[1]);
            Assert.Equal(10, bPlusTree.Root.Children[0].Children[1].Children[0].Keys[0]);
            Assert.Equal(15, bPlusTree.Root.Children[0].Children[1].Children[0].Keys[1]);
            Assert.Equal(17, bPlusTree.Root.Children[0].Children[1].Children[1].Keys[0]);
            Assert.Equal(20, bPlusTree.Root.Children[0].Children[1].Children[1].Keys[1]);
            Assert.Equal(22, bPlusTree.Root.Children[0].Children[2].Children[0].Keys[0]);
            Assert.Equal(25, bPlusTree.Root.Children[0].Children[2].Children[0].Keys[1]);
            Assert.Equal(30, bPlusTree.Root.Children[0].Children[2].Children[1].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[0].Children[2].Children[1].Keys[1]);
            Assert.Equal(40, bPlusTree.Root.Children[1].Children[0].Children[0].Keys[0]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[0].Children[0].Keys[1]);
            Assert.Equal(60, bPlusTree.Root.Children[1].Children[0].Children[1].Keys[0]);
            Assert.Equal(65, bPlusTree.Root.Children[1].Children[0].Children[1].Keys[1]);
            Assert.Equal(70, bPlusTree.Root.Children[1].Children[1].Children[0].Keys[0]);
            Assert.Equal(75, bPlusTree.Root.Children[1].Children[1].Children[0].Keys[1]);
            Assert.Equal(80, bPlusTree.Root.Children[1].Children[1].Children[1].Keys[0]);
            Assert.Equal(85, bPlusTree.Root.Children[1].Children[1].Children[1].Keys[1]);
            return bPlusTree;
        }

        /// <summary>
        /// 删除结点后关键字个数大于⌈M/2⌉，不会破坏B+树结构，则可以直接删除。
        /// </summary>
        [Fact]
        public void DeleteTest1()
        {
            var bPlusTree = CreateTestBPlusTree();
            //补充叶子结点，准备删除
            bPlusTree.Insert(45);
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                20,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4]  [10, 15] [17,20]  [22,25]  [30,35]  [40,45,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            bPlusTree.Delete(45);
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                20,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4]  [10, 15] [17,20]  [22,25]  [30,35]  [40,   50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[0].Children[0].Keys[1]);
        }

        /// <summary>
        /// 删除结点后关键字个数小于⌈M/2⌉，但可向右兄弟借。
        /// </summary>
        [Fact]
        public void DeleteTest2()
        {
            var bPlusTree = CreateTestBPlusTree();
            //补充叶子结点，准备删除
            bPlusTree.Insert(18);
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                20,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4]  [10,15] [17,18,20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            bPlusTree.Delete(15);
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                20,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [17,    20]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4]  [10,17]  [18,20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */
            Assert.Equal(17, bPlusTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(10, bPlusTree.Root.Children[0].Children[1].Children[0].Keys[0]);
            Assert.Equal(17, bPlusTree.Root.Children[0].Children[1].Children[0].Keys[1]);
        }

        /// <summary>
        /// 删除结点后关键字个数小于⌈M/2⌉，但可向左兄弟借。
        /// </summary>
        [Fact]
        public void DeleteTest3()
        {
            var bPlusTree = CreateTestBPlusTree();
            //补充叶子结点，准备删除
            bPlusTree.Insert(13);
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                20,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4] [10,13,15] [17, 20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            bPlusTree.Delete(20);
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                17,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [13,    17]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4]  [10,13] [15, 17]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */
            Assert.Equal(17, bPlusTree.Root.Children[0].Keys[1]);
            Assert.Equal(13, bPlusTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(17, bPlusTree.Root.Children[0].Children[1].Keys[1]);
            Assert.Equal(10, bPlusTree.Root.Children[0].Children[1].Children[0].Keys[0]);
            Assert.Equal(13, bPlusTree.Root.Children[0].Children[1].Children[0].Keys[1]);
            Assert.Equal(15, bPlusTree.Root.Children[0].Children[1].Children[1].Keys[0]);
            Assert.Equal(17, bPlusTree.Root.Children[0].Children[1].Children[1].Keys[1]);
        }

        /// <summary>
        /// 删除结点后左右兄弟都不可借，则与左兄弟或右兄弟合并
        /// </summary>
        [Fact]
        public void DeleteTest4()
        {
            var bPlusTree = CreateTestBPlusTree();
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                20,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4]  [10, 15] [17,20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            bPlusTree.Delete(85);
            /***************************************
             **                                                                       
             *                                             [20,                                                80]
             *                                               |                                                   |
             *                                               |                                                   |
             *                           [4,               20]                   [35,                          80]
             *                            |                  |                    |                              |                 
             *                            |                  |                    |                              |                 
             **                    [2,    4]       [15,    20]         [25,     35]        [50,    65,         80]
             **                     |     |         |        |           |        |         |        |           |        
             **                     |     |         |        |           |        |         |        |           |        
             **                [1, 2] [3, 4]  [10, 15] [17,20]     [22,25]  [30,35]   [40,50]  [60,65]  [70,75,80]
             **
             ***************************************
             */
            Assert.Equal(20, bPlusTree.Root.Keys[0]);
            Assert.Equal(80, bPlusTree.Root.Keys[1]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Keys[0]);
            Assert.Equal(80, bPlusTree.Root.Children[1].Keys[1]);
            Assert.Equal(25, bPlusTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(65, bPlusTree.Root.Children[1].Children[1].Keys[1]);
            Assert.Equal(80, bPlusTree.Root.Children[1].Children[1].Keys[2]);
            Assert.Equal(22, bPlusTree.Root.Children[1].Children[0].Children[0].Keys[0]);
            Assert.Equal(25, bPlusTree.Root.Children[1].Children[0].Children[0].Keys[1]);
            Assert.Equal(30, bPlusTree.Root.Children[1].Children[0].Children[1].Keys[0]);
            Assert.Equal(35, bPlusTree.Root.Children[1].Children[0].Children[1].Keys[1]);
            Assert.Equal(40, bPlusTree.Root.Children[1].Children[1].Children[0].Keys[0]);
            Assert.Equal(50, bPlusTree.Root.Children[1].Children[1].Children[0].Keys[1]);
            Assert.Equal(60, bPlusTree.Root.Children[1].Children[1].Children[1].Keys[0]);
            Assert.Equal(65, bPlusTree.Root.Children[1].Children[1].Children[1].Keys[1]);
            Assert.Equal(70, bPlusTree.Root.Children[1].Children[1].Children[2].Keys[0]);
            Assert.Equal(75, bPlusTree.Root.Children[1].Children[1].Children[2].Keys[1]);
            Assert.Equal(80, bPlusTree.Root.Children[1].Children[1].Children[2].Keys[2]);
        }

        /// <summary>
        /// 删除所有插入的节点
        /// </summary>
        [Fact]
        public void DeleteTest5()
        {
            var bPlusTree = CreateTestBPlusTree();
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                20,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4]  [10, 15] [17,20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                                              [20,              35]                  [65,              85]
             *                                               |                 |                    |                 |
             *                                               |                 |                    |                 |
             **                           [4,       15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                           |         |        |        |        |           |        |        |        |
             **                           |         |        |        |        |           |        |        |        |
             **                    [2,3, 4]  [10, 15]  [17,20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                                              [20,              35]                  [65,              85]
             *                                               |                 |                    |                 |
             *                                               |                 |                    |                 |
             **                                    [15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                                    |         |        |        |          |         |        |        |        |
             **                                    |         |        |        |          |         |        |        |        |
             **                               [4,10, 15]  [17,20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            /***************************************
             **                                                                       
             *                                                                                                      85]
             *                                                                                                        |
             *                                                                                                        |
             *                                                               [35,                  65,              85]
             *                                               |                 |                    |                 |
             *                                               |                 |                    |                 |
             **                                           [20,       25,     35]          [50,    65]      [75,     85]
             **                                    |         |        |        |          |         |        |        |       
             **                                    |         |        |        |          |         |        |        |       
             **                                     [15,17,20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            /***************************************
             **                                                                       
             *                                                                                                      85]
             *                                                                                                        |
             *                                                                                                        |
             *                                                               [35,                  65,              85]
             *                                                                 |                    |                 |
             *                                                                 |                    |                 |
             **                                                     [25,     35]          [50,    65]      [75,     85]        
             **                                                       |        |          |         |        |        |        
             **                                                       |        |          |         |        |        |        
             **                                              [20,22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */


            /***************************************
             **                                                                       
             *                                                                                                      85]
             *                                                                                                        |
             *                                                                                                        |
             *                                                                                    [65,              85]
             *                                                                 |                    |                 |
             *                                                                 |                    |                 |
             **                                                                         [50,      65]      [75,     85]        
             **                                                       |        |          |         |        |        |        
             **                                                       |        |          |         |        |        |        
             **                                                                     [40,50]   [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */

            /***************************************
             **                                                                       
             *                                                                                                     
             *                                                                                                     [85]
             *                                                                 |                    |                 |
             *                                                                 |                    |                 |
             **                                                                                   [65,      75,     85]        
             **                                                       |        |          |         |        |        |        
             **                                                       |        |          |         |        |        |        
             **                                                                            [50,60,65]  [70,75]  [80,85]
             **
             ***************************************
             */
            while (bPlusTree.Head != null)
            {
                bPlusTree.Delete(bPlusTree.Head.Keys[0]);
            }
            Assert.True(bPlusTree.Head == null);
        }

        /// <summary>
        /// 删除所有插入的节点
        /// </summary>
        [Fact]
        public void DeleteTest6()
        {
            var bPlusTree = CreateTestBPlusTree();
            /***************************************
             **                                                                       
             *                                                               [35,                                   85]
             *                                                                 |                                      |
             *                                                                 |                                      |
             *                           [4,                20,              35]                  [65,              85]
             *                            |                  |                 |                    |                 |
             *                            |                  |                 |                    |                 |
             **                    [2,    4]       [15,    20]      [25,     35]          [50,    65]      [75,     85]
             **                     |     |         |        |        |        |           |        |        |        |
             **                     |     |         |        |        |        |           |        |        |        |
             **                [1, 2] [3, 4]  [10, 15] [17,20]  [22,25]  [30,35]     [40,50]  [60,65]  [70,75]  [80,85]
             **
             ***************************************
             */
            while (bPlusTree.Head != null)
            {
                try
                {
                    bPlusTree.Delete(bPlusTree.Head.Keys[bPlusTree.Head.Keys.Count - 1]);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            Assert.True(bPlusTree.Head == null);
        }
    }
}
