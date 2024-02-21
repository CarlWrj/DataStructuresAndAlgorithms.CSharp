using DataStructures.Trees.RedBlackTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Test.Trees
{

    public class RedBlackTreeTest
    {
        /** Input tree for test cases, (r -> red, b -> black):
        **          11(b)
        **         /    \
        **      (r)3    13(b)
        **       / \      \
        **   (b)1  7(b)   15(r)
        **         / \
        **     (r)5   8(r)
        **/
        //private RedBlackTree<int> redBlackTree;

        public RedBlackTreeTest()
        {

        }
        [Fact]
        public void Test()
        {
            var redBlackTree = new RedBlackTree<int>();

            redBlackTree.Insert(20);
            redBlackTree.Insert(10);
            redBlackTree.Insert(5);
            redBlackTree.Insert(30);
            redBlackTree.Insert(40);
            redBlackTree.Insert(57);
            redBlackTree.Insert(3);
            redBlackTree.Insert(2);
            redBlackTree.Insert(4);
            redBlackTree.Insert(35);
            redBlackTree.Insert(25);
            redBlackTree.Insert(18);
            redBlackTree.Insert(22);
            redBlackTree.Insert(23);
            redBlackTree.Insert(24);
            redBlackTree.Insert(19);
            redBlackTree.Insert(18);

            var root = redBlackTree.Root;
            Assert.Equal(RedBlackTreeNodeColor.Black, root.Color);
            Assert.Equal(10, root.Value);

            //左
            Assert.Equal(RedBlackTreeNodeColor.Black, root.LeftChild.Color);
            Assert.Equal(3, root.LeftChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Black, root.LeftChild.LeftChild.Color);
            Assert.Equal(2, root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Black, root.LeftChild.RightChild.Color);
            Assert.Equal(5, root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Red, root.LeftChild.RightChild.LeftChild.Color);
            Assert.Equal(4, root.LeftChild.RightChild.LeftChild.Value);

            //右
            Assert.Equal(RedBlackTreeNodeColor.Black, root.RightChild.Color);
            Assert.Equal(23, root.RightChild.Value);
            //左
            Assert.Equal(RedBlackTreeNodeColor.Red, root.RightChild.LeftChild.Color);
            Assert.Equal(20, root.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Black, root.RightChild.LeftChild.LeftChild.Color);
            Assert.Equal(18, root.RightChild.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Red, root.RightChild.LeftChild.LeftChild.LeftChild.Color);
            Assert.Equal(18, root.RightChild.LeftChild.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Red, root.RightChild.LeftChild.LeftChild.RightChild.Color);
            Assert.Equal(19, root.RightChild.LeftChild.LeftChild.RightChild.Value);
            //右
            Assert.Equal(RedBlackTreeNodeColor.Red, root.RightChild.RightChild.Color);
            Assert.Equal(30, root.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Black, root.RightChild.RightChild.LeftChild.Color);
            Assert.Equal(25, root.RightChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Red, root.RightChild.RightChild.LeftChild.LeftChild.Color);
            Assert.Equal(24, root.RightChild.RightChild.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Black, root.RightChild.RightChild.RightChild.Color);
            Assert.Equal(40, root.RightChild.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Red, root.RightChild.RightChild.RightChild.LeftChild.Color);
            Assert.Equal(35, root.RightChild.RightChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeNodeColor.Red, root.RightChild.RightChild.RightChild.RightChild.Color);
            Assert.Equal(57, root.RightChild.RightChild.RightChild.RightChild.Value);
        }
    }
}
