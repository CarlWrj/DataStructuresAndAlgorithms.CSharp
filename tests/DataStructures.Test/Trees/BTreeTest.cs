using DataStructures.Trees.BTrees;

namespace DataStructures.Test.Trees
{
    public class BTreeTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void InsertTest()
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
             *                                            [80]
             *                                           /    \
             **                                         /      \
             **                      [49,            73]        [88,      93]
             **                     /    \             |        /        /   \
             **                    /      \            |       /        /     \
             **             [25,38]        [60,70] [74,75]   [83,87]  [90,92] [94,99]
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

            bTree.Insert(71);
            bTree.Insert(72);
            bTree.Insert(76);
            bTree.Insert(77);
            bTree.Insert(82);
            bTree.Insert(86);
            //补充叶子结点，准备删除
            /***************************************
             *                                                 [  80  ]
             *                                                /        \
             **                                              /          \
             **                      [49,                 73]            [88,                   93]
             **                     /    \                  |             |                     /   \
             **                    /      \                 |             |                    /     \
             **             [25,38]        [60,70,71,72] [74,75,76,77]   [82,83,86,87]  [90,92] [94,99]
             **
             ***************************************
             */

            bTree.Delete(60);
            //删除叶子结点，并且删除后仍然满足B树定义则直接删除
            /***************************************
             *                                                 [  80  ]
             *                                                /        \
             **                                              /          \
             **                      [49,                 73]            [88,                   93]
             **                     /    \                  |             |                     /   \
             **                    /      \                 |             |                    /     \
             **             [25,38]        [70,71,72]      [74,75,76,77]   [82,83,86,87]  [90,92] [94,99]
             **
             ***************************************
             */
            Assert.Equal(1, bTree.Root.Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[0].Children[0].Keys.Count);
            Assert.Equal(3, bTree.Root.Children[0].Children[1].Keys.Count);
            Assert.Equal(4, bTree.Root.Children[0].Children[2].Keys.Count);
            Assert.Equal(4, bTree.Root.Children[1].Children[0].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[2].Keys.Count);

            bTree.Delete(80);
            //删除非叶子结点，如果有直接后继（或前驱）结点，则获取直接后继（或前驱）结点的关键字替换掉待删除的结点关键字，然后删除直接后继（或前驱）结点
            /***************************************
             *                                                 [  82  ]
             *                                                /        \
             **                                              /          \
             **                      [49,                 73]            [88,                    93]
             **                     /    \                  |             |                     /   \
             **                    /      \                 |             |                    /     \
             **             [25,38]        [70,71,72]      [74,75,76,77]   [83,86,87]    [90,92] [94,99]
             **
             ***************************************
             */
            Assert.Equal(3, bTree.Root.Children[1].Children[0].Keys.Count);

            bTree.Delete(38);
            bTree.Delete(90);
            //删除叶子结点后不满足B树定义，如果左右兄弟的关键字数量大于下限则借向左右兄弟借，
            //具体操作用父结点关键字代替自己，然后用兄弟结点关键字（左兄弟用最右关键字，右兄弟用最左关键字）代替父结点
            /***************************************
             *                                                 [  82  ]
             *                                                /        \
             **                                              /          \
             **                      [70,                 73]            [87,                  93]
             **                     /    \                  |             |                   /   \
             **                    /      \                 |             |                  /     \
             **             [25,49]        [71,72]      [74,75,76,77]   [83,86]         [88,92] [94,99]
             **
             ***************************************
             */
            Assert.Equal(2, bTree.Root.Children[0].Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[1].Children[0].Keys.Count);

            bTree.Delete(49);
            //删除叶子结点后不满足B树定义，并且左右兄弟不够借情况，操作如下
            //1 如果左兄弟不够借，需要把自己所在的结点和左兄弟和父结点合并成一个结点
            //2 如果右兄弟不够借，需要把自己所在的结点和右兄弟和父结点合并成一个结点
            //3 因父结点的关键字下移，需对父结点进行相同递归删除处理，如果处理到最后只剩一个根结点，则直接删除并用合并的节点作为新的根结点
            /***************************************
             *                                           
             *                                           
             **                                          
             **                                          [73,82,             87,           93]
             **                                         /    \              |   \            \          
             **                                        /      \             |    \            \         
             **                            [25,70,71,72] [74,75,76,77]   [83,86]   [88,92]      [94,99]
             **
             ***************************************
             */
            Assert.Equal(4, bTree.Root.Keys.Count);
            Assert.Equal(4, bTree.Root.Children[0].Keys.Count);
            Assert.Equal(4, bTree.Root.Children[1].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[2].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[3].Keys.Count);
            Assert.Equal(2, bTree.Root.Children[4].Keys.Count);
        }
    }
}
