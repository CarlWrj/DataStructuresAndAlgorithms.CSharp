using DataStructures.Trees.BTrees;
using System.Security.Cryptography;

namespace DataStructures.Test.Trees
{
    public class BTreeTest
    {
        /// <summary>
        /// 创建测试B树
        /// </summary>
        /// <returns></returns>
        private BTree<int> CreateTestBTree()
        {
            BTree<int> bTree = new BTree<int>(5);

            bTree.Insert(25);
            bTree.Insert(49);
            bTree.Insert(38);
            bTree.Insert(60);
            /***************************************
             **
             **    [25 , 38 , 49 , 60]
             **
             ***************************************
             */
            Assert.Equal(4, bTree.Root.Keys.Count);


            bTree.Insert(80);
            //向上分裂
            /***************************************
             **
             **                      [49]
             **                     /    \
             **                    /      \
             **           [25 , 38]        [60 , 80]
             **
             ***************************************
             */
            Assert.Equal(49, bTree.Root.Keys[0]);
            Assert.Equal(2, bTree.Root.Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Keys.Count);

            bTree.Insert(90);
            bTree.Insert(99);
            bTree.Insert(88);
            //向上分裂
            /***************************************
             **
             **                      [49,        88]
             **                     /    \          \
             **                    /      \          \
             **           [25 , 38]        [60 , 80]  [90, 99]
             **
             ***************************************
             */
            Assert.Equal(2, bTree.Root.Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[2].Keys.Count);

            bTree.Insert(83);
            bTree.Insert(87);
            bTree.Insert(70);
            //向上分裂
            /***************************************
             **
             **                      [49,            80         88]
             **                     /    \          /  \          \
             **                    /      \        /    \          \
             **           [25 , 38]        [60 , 70]     [83, 87]  [90, 99]
             **
             ***************************************
             */
            Assert.Equal(3, bTree.Root.Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[2].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[3].Keys.Count);

            bTree.Insert(92);
            bTree.Insert(93);
            bTree.Insert(94);
            //向上分裂
            /***************************************
             **
             **                      [49,           80,         88,     93]
             **                     /    \            \           \        \
             **                    /      \            \           \        \
             **             [25,38]        [60,70]      [83,87]     [90,92]  [94,99]
             **
             ***************************************
             */
            Assert.Equal(4, bTree.Root.Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[2].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[3].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[4].Keys.Count);

            bTree.Insert(73);
            bTree.Insert(74);
            bTree.Insert(75);
            //向上分裂
            /***************************************
             *                                        [      80     ]
             *                                        |             |
             **                                       |             |
             **                       [49    ,     73]              [88   ,   93]
             **                       |      |       |              |     |     | 
             **                       |      |       |              |     |     |
             **                 [25,38]   [60,70]    [74,75]  [83,87]  [90,92]  [94,99]
             **
             ***************************************
             */
            Assert.Equal(1, bTree.Root.Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Children[2].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[2].Keys.Count);

            return bTree;
        }

        /// <summary>
        /// 删除叶子结点，并且删除后仍然满足B树定义则直接删除
        /// </summary>
        [Fact]
        public void DeleteTest1()
        {
            var bTree = CreateTestBTree();
            //补充叶子结点，准备删除
            bTree.Insert(71);
            /***************************************
             *                                        [      80     ]
             *                                        |             |
             **                                       |             |
             **                       [49    ,     73]              [88   ,   93]
             **                       |      |       |              |     |     | 
             **                       |      |       |              |     |     |
             **                 [25,38]  [60,70,71]  [74,75]  [83,87]  [90,92]  [94,99]
             **
             ***************************************
             */

            bTree.Delete(60);
            //删除叶子结点，并且删除后仍然满足B树定义则直接删除
            /***************************************
             *                                        [      80     ]
             *                                        |             |
             **                                       |             |
             **                       [49    ,     73]              [88   ,   93]
             **                       |      |       |              |     |     | 
             **                       |      |       |              |     |     |
             **                 [25,38]   [70,71]    [74,75]  [83,87]  [90,92]  [94,99]
             **
             ***************************************
             */
            Assert.Equal(70, bTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(71, bTree.Root.Children[0].Children[1].Keys[1]);
        }

        /// <summary>
        /// 删除非叶子结点，如果有直接后继（或前驱）结点，则获取直接后继（或前驱）结点的关键字替换掉待删除的结点关键字，然后删除直接后继（或前驱）结点
        /// </summary>
        [Fact]
        public void DeleteTest2()
        {
            //创建B树
            var bTree = CreateTestBTree();
            bTree.Insert(86);
            /***************************************
             *                                    [        80       ]
             *                                    |                 |
             **                                   |                 |
             **                    [49    ,     73]                 [88   ,   93]
             **                    |      |       |                 |     |     | 
             **                    |      |       |                 |     |     |
             **              [25,38]   [60,70]    [74,75]  [83,86,87]  [90,92]  [94,99]
             **
             ***************************************
             */

            //删除非叶子结点，如果有直接后继（或前驱）结点，则获取直接后继（或前驱）结点的关键字替换掉待删除的结点关键字，然后删除直接后继（或前驱）结点
            bTree.Delete(80);
            /***************************************
             *                                        [      83     ]
             *                                        |             |
             **                                       |             |
             **                       [49    ,     73]              [88   ,   93]
             **                       |      |       |              |     |     | 
             **                       |      |       |              |     |     |
             **                 [25,38]  [60,70,71]  [74,75]  [86,87]  [90,92]  [94,99]
             **
             ***************************************
             */
            Assert.Equal(83, bTree.Root.Keys[0]);
            Assert.Equal(86, bTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(87, bTree.Root.Children[1].Children[0].Keys[1]);
        }

        /// <summary>
        /// 删除叶子结点，并且左右兄弟可借
        /// </summary>
        [Fact]
        public void DeleteTest3()
        {
            //创建B树
            var bTree = CreateTestBTree();
            bTree.Insert(71);
            bTree.Insert(85);
            /***************************************
             *                                        [       80       ]
             *                                        |                |
             **                                       |                |
             **                       [49    ,     73]                 [88   ,   93]
             **                       |      |       |                 |     |     | 
             **                       |      |       |                 |     |     |
             **                 [25,38]  [60,70,71]  [74,75]  [83,85,87]  [90,92]  [94,99]
             **
             ***************************************
             */

            //删除叶子结点后不满足B树定义，如果左右兄弟的关键字数量大于下限则向借左右兄弟借，
            bTree.Delete(25);
            bTree.Delete(90);
            /***************************************
             *                                        [       80       ]
             *                                        |                |
             **                                       |                |
             **                       [60    ,     73]                 [87   ,   93]
             **                       |      |       |                 |     |     | 
             **                       |      |       |                 |     |     |
             **                 [38,49]   [70,71]    [74,75]     [83,85]  [88,92]  [94,99]
             **
             ***************************************
             */

            Assert.Equal(38, bTree.Root.Children[0].Children[0].Keys[0]);
            Assert.Equal(49, bTree.Root.Children[0].Children[0].Keys[1]);
            Assert.Equal(60, bTree.Root.Children[0].Keys[0]);
            Assert.Equal(70, bTree.Root.Children[0].Children[1].Keys[0]);
            Assert.Equal(71, bTree.Root.Children[0].Children[1].Keys[1]);

            Assert.Equal(83, bTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(85, bTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(87, bTree.Root.Children[1].Keys[0]);
            Assert.Equal(88, bTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(92, bTree.Root.Children[1].Children[1].Keys[1]);
        }

        /// <summary>
        /// 删除非叶子结点，并且左右兄弟不可借，并且向上合并时右兄弟可借
        /// </summary>
        [Fact]
        public void DeleteTest4()
        {
            //创建B树
            var bTree = CreateTestBTree();
            bTree.Insert(100);
            bTree.Insert(101);
            bTree.Insert(102);
            /***************************************
             *                                        [      80     ]
             *                                        |             |
             **                                       |             |
             **                       [49    ,     73]              [88   ,   93   ,   100]
             **                       |      |       |              |     |        |      | 
             **                       |      |       |              |     |        |      |
             **                 [25,38]   [60,70]    [74,75]  [83,87]  [90,92]  [94,99]   [101,102]
             **
             ***************************************
             */

            bTree.Delete(49);
            /***************************************
             *                                        [      88     ]
             *                                        |             |
             **                                       |             |
             **                       [73    ,     80]              [93   ,     100]
             **                       |      |       |              |     |        |      | 
             **                       |      |       |              |     |        |      |
             **           [25,38,60,70]   [74,75]    [83,87]  [90,92]  [94,99]   [101,102]
             **
             ***************************************
             */
            Assert.Equal(1, bTree.Root.Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Keys.Count);
            Assert.Equal(4, bTree.Root.Children[0].Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Children[2].Keys.Count);

            Assert.Equal(2, bTree.Root.Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[2].Keys.Count);
        }

        /// <summary>
        /// 删除非叶子结点，并且左右兄弟不可借，并且向上合并时左兄弟可借
        /// </summary>
        [Fact]
        public void DeleteTest5()
        {
            //创建B树
            var bTree = CreateTestBTree();
            bTree.Insert(76);
            bTree.Insert(77);
            bTree.Insert(78);
            /***************************************
             *                                        [      80     ]
             *                                        |             |
             **                                       |             |
             **              [49    ,     73   ,    76]              [88   ,   93]
             **              |      |          |      |              |     |     | 
             **              |      |          |      |              |     |     |
             **        [25,38]   [60,70]    [74,75]   [77,78]  [83,87]  [90,92]  [94,99]
             **
             ***************************************
             */

            bTree.Delete(93);
            /***************************************
             *                                        [      76     ]
             *                                        |             |
             **                                       |             |
             **                        [49    ,     73]              [80   ,   88]
             **                        |      |      |              |     |     | 
             **                        |      |      |              |     |     |
             **                  [25,38]   [60,70]   [74,75]  [77,78]  [83,87]  [90,92,94,99]
             **
             ***************************************
             */
            Assert.Equal(76, bTree.Root.Keys[0]);

            Assert.Equal(73, bTree.Root.Children[0].Keys[1]);
            Assert.Equal(74, bTree.Root.Children[0].Children[2].Keys[0]);
            Assert.Equal(75, bTree.Root.Children[0].Children[2].Keys[1]);

            Assert.Equal(80, bTree.Root.Children[1].Keys[0]);
            Assert.Equal(88, bTree.Root.Children[1].Keys[1]);
            Assert.Equal(77, bTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(78, bTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(83, bTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(87, bTree.Root.Children[1].Children[1].Keys[1]);
            Assert.Equal(90, bTree.Root.Children[1].Children[2].Keys[0]);
            Assert.Equal(92, bTree.Root.Children[1].Children[2].Keys[1]);
            Assert.Equal(94, bTree.Root.Children[1].Children[2].Keys[2]);
            Assert.Equal(99, bTree.Root.Children[1].Children[2].Keys[3]);
        }

        /// <summary>
        /// 删除非叶子结点，并且左右兄弟不可借，并且向上合并时左兄弟可借
        /// </summary>
        [Fact]
        public void DeleteTest6()
        {
            //创建B树
            var bTree = CreateTestBTree();
            bTree.Insert(76);
            bTree.Insert(77);
            bTree.Insert(78);
            /***************************************
             *                                        [      80     ]
             *                                        |             |
             **                                       |             |
             **              [49    ,     73   ,    76]              [88   ,   93]
             **              |      |          |      |              |     |     | 
             **              |      |          |      |              |     |     |
             **        [25,38]   [60,70]    [74,75]   [77,78]  [83,87]  [90,92]  [94,99]
             **
             ***************************************
             */

            bTree.Delete(88);
            /***************************************
             *                                        [      76     ]
             *                                        |             |
             **                                       |             |
             **                        [49    ,     73]              [80      ,     93]
             **                        |      |      |              |         |       | 
             **                        |      |      |              |         |       |
             **                  [25,38]   [60,70]   [74,75]  [77,78]  [83,87,90,92]  [94,99]
             **
             ***************************************
             */
            Assert.Equal(76, bTree.Root.Keys[0]);

            Assert.Equal(73, bTree.Root.Children[0].Keys[1]);
            Assert.Equal(74, bTree.Root.Children[0].Children[2].Keys[0]);
            Assert.Equal(75, bTree.Root.Children[0].Children[2].Keys[1]);

            Assert.Equal(80, bTree.Root.Children[1].Keys[0]);
            Assert.Equal(93, bTree.Root.Children[1].Keys[1]);
            Assert.Equal(77, bTree.Root.Children[1].Children[0].Keys[0]);
            Assert.Equal(78, bTree.Root.Children[1].Children[0].Keys[1]);
            Assert.Equal(83, bTree.Root.Children[1].Children[1].Keys[0]);
            Assert.Equal(87, bTree.Root.Children[1].Children[1].Keys[1]);
            Assert.Equal(90, bTree.Root.Children[1].Children[1].Keys[2]);
            Assert.Equal(92, bTree.Root.Children[1].Children[1].Keys[3]);
            Assert.Equal(94, bTree.Root.Children[1].Children[2].Keys[0]);
            Assert.Equal(99, bTree.Root.Children[1].Children[2].Keys[1]);
        }

        /// <summary>
        /// 删除非叶子结点，并且左右兄弟不可借，并且向上合并时左右兄弟不可借
        /// </summary>
        [Fact]
        public void DeleteTest7()
        {
            //创建B树
            var bTree = CreateTestBTree();
            /***************************************
             *                                        [      80     ]
             *                                        |             |
             **                                       |             |
             **                       [49    ,     73]              [88   ,   93]
             **                       |      |       |              |     |     | 
             **                       |      |       |              |     |     |
             **                 [25,38]   [60,70]    [74,75]  [83,87]  [90,92]  [94,99]
             **
             ***************************************
             */

            bTree.Delete(49);
            /***************************************
             * 
             * 
             **                       [73    ,     80      ,      88   ,     93]
             **                       |      |             |           |       |  
             **                       |      |             |           |       | 
             **           [25,38,60,70]    [74,75]       [83,87]    [90,92]    [94,99]
             **
             ***************************************
             */
            Assert.Equal(73, bTree.Root.Keys[0]);
            Assert.Equal(80, bTree.Root.Keys[1]);
            Assert.Equal(88, bTree.Root.Keys[2]);
            Assert.Equal(93, bTree.Root.Keys[3]);
            Assert.Equal(25, bTree.Root.Children[0].Keys[0]);
            Assert.Equal(38, bTree.Root.Children[0].Keys[1]);
            Assert.Equal(60, bTree.Root.Children[0].Keys[2]);
            Assert.Equal(70, bTree.Root.Children[0].Keys[3]);
        }

        /// <summary>
        /// 删除非叶子结点，并且左右兄弟不可借，并且向上合并时左右兄弟不可借
        /// </summary>
        [Fact]
        public void DeleteTest8()
        {
            //创建B树
            var bTree = CreateTestBTree();
            /***************************************
             *                                        [      80     ]
             *                                        |             |
             **                                       |             |
             **                       [49    ,     73]              [88   ,   93]
             **                       |      |       |              |     |     | 
             **                       |      |       |              |     |     |
             **                 [25,38]   [60,70]    [74,75]  [83,87]  [90,92]  [94,99]
             **
             ***************************************
             */

            bTree.Delete(93);
            /***************************************
             * 
             * 
             **                       [49    ,     73      ,      80   ,     88]
             **                       |      |             |           |       |  
             **                       |      |             |           |       | 
             **                [25,38]    [60,70]       [74,75]     [83,87]    [90,92,94,99]
             **
             ***************************************
             */
            Assert.Equal(49, bTree.Root.Keys[0]);
            Assert.Equal(73, bTree.Root.Keys[1]);
            Assert.Equal(80, bTree.Root.Keys[2]);
            Assert.Equal(88, bTree.Root.Keys[3]);
            Assert.Equal(90, bTree.Root.Children[4].Keys[0]);
            Assert.Equal(92, bTree.Root.Children[4].Keys[1]);
            Assert.Equal(94, bTree.Root.Children[4].Keys[2]);
            Assert.Equal(99, bTree.Root.Children[4].Keys[3]);
        }
    }
}
