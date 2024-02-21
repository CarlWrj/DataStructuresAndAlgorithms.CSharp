using DataStructures.Trees.AVLTrees;

namespace DataStructures.Test.Trees
{
    public class AVLTreeTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void InsertTest()
        {
            AVLTree<int> avlTree = new AVLTree<int>();

            //
            // CASE #1
            // Insert: 4, 5, 7
            // SIMPLE *left* rotation for node 4.
            //
            /***************************************
             ** UNBALANCED    ===>    BALANCED
             **     4                   5
             **      \                 / \
             **       5       ===>    4   7
             **        \
             **         7
             **
             ***************************************
             */
            avlTree.Insert(4); // insert
            avlTree.Insert(5); // insert
            avlTree.Insert(7); // insert
            Assert.True(avlTree.Root.Value == 5);
            Assert.True(avlTree.Root.LeftChild.Value == 4);
            Assert.True(avlTree.Root.RightChild.Value == 7);

            //
            // CASE #2
            // Insert to the previous tree: 2 and then 1.
            // SIMPLE *right* rotation for node 4.
            //
            /*********************************************
             ** UNBALANCED    ===>    BALANCED
             **        5                 5
             **       / \               / \
             **      4   7    ===>     2   7
             **     /                 / \
             **    2                 1   4
             **   /
             **  1
             **
             *********************************************
             */
            avlTree.Insert(2); // insert
            avlTree.Insert(1); // insert
            Assert.True(avlTree.Root.Value == 5);
            Assert.True(avlTree.Root.LeftChild.Value == 2);
            Assert.True(avlTree.Root.LeftChild.LeftChild.Value == 1);
            Assert.True(avlTree.Root.LeftChild.RightChild.Value == 4);
            Assert.True(avlTree.Root.RightChild.Value == 7);

            //
            // CASE #3
            // Insert to the previous tree: 3.
            // DOUBLE *right* rotation for node 5.
            //
            // The double rotation is achieved by:
            // 1> Simple *left* rotation for node 2, and then
            // 2> Simple *right* rotation for node 5
            //
            /*************************************
             ** UNBALANCED     ===>    TRANSITION (1st R)    ===>    BALANCED (2nd Rt)
             **        5                      5                          4
             **       / \                    / \                        / \
             **      2   7     ===>         4   7        ===>          2   5
             **     / \                    /                          / \   \
             **    1   4                  2                          1   3   7
             **       /                  / \
             **      3                  1   3
             **
             *************************************
             */
            avlTree.Insert(3); // insert
            Assert.True(avlTree.Root.Value == 4);
            Assert.True(avlTree.Root.LeftChild.Value == 2);
            Assert.True(avlTree.Root.LeftChild.LeftChild.Value == 1);
            Assert.True(avlTree.Root.LeftChild.RightChild.Value == 3);
            Assert.True(avlTree.Root.RightChild.Value == 5);
            Assert.True(avlTree.Root.RightChild.RightChild.Value == 7);

            //
            // CASE #4
            // Insert to the previous tree: 6.
            // DOUBLE *right* rotation for node 5.
            //
            // The double rotation is achieved by:
            // 1> Simple *right* rotation for node 7, and then
            // 2> Simple *left* rotation for node 5
            //
            /**************************************************************************
             ** UNBALANCED     ===>    TRANSITION (1st R)    ===>    BALANCED (2nd Rt)
             **        4                      4                          ..4..
             **       / \                    / \                        /     \
             **      2   5     ===>         2   5         ===>         2       6
             **     / \   \                / \   \                    / \     / \
             **    1   3   7              1   3   6                  1   3   5   7
             **           /                        \
             **          6                          7
             **
             **************************************************************************
             */
            avlTree.Insert(6); // insert
            Assert.True(avlTree.Root.Value == 4);
            Assert.True(avlTree.Root.LeftChild.Value == 2);
            Assert.True(avlTree.Root.LeftChild.LeftChild.Value == 1);
            Assert.True(avlTree.Root.LeftChild.RightChild.Value == 3);
            Assert.True(avlTree.Root.RightChild.Value == 6);
            Assert.True(avlTree.Root.RightChild.LeftChild.Value == 5);
            Assert.True(avlTree.Root.RightChild.RightChild.Value == 7);

            //
            // CASE #5
            // REMOVE the tree's root: 4.
            //
            /**************************************************************************
             ** UNBALANCED     ===>    TRANSITION (1st R)    ===>    BALANCED (2nd Rt)
             **       null                                              .5..
             **      /   \                                             /    \
             **     2     6    ===>                      ===>         2      6
             **    / \   / \                                         / \      \
             **   1   3 5   7                                       1   3      7
             **
             **************************************************************************
             */
            avlTree.Remove(avlTree.Root.Value); // REMOVE 4
            Assert.True(avlTree.Root.Value == 5);
            Assert.True(avlTree.Root.LeftChild.Value == 2);
            Assert.True(avlTree.Root.LeftChild.LeftChild.Value == 1);
            Assert.True(avlTree.Root.LeftChild.RightChild.Value == 3);
            Assert.True(avlTree.Root.RightChild.Value == 6);
            Assert.True(avlTree.Root.RightChild.RightChild.Value == 7);
        }
    }
}
