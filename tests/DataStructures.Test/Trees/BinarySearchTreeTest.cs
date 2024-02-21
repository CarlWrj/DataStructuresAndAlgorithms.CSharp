using DataStructures.Trees.BinarySearchTrees;

namespace DataStructures.Test.Trees
{
    public class BinarySearchTreeTest
    {
        [Fact]
        public static void AssertTreeWithUniqueElements()
        {
            var binarySearchTree = new BinarySearchTree<int>();
            int[] values = new int[24] { 14, 15, 25, 5, 12, 1, 16, 20, 9, 9, 9, 7, 7, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39, 39 };
            foreach (var item in values)
            {
                binarySearchTree.Insert(item);
            }

            Assert.Equal(24, binarySearchTree.Count);

            var findNode = binarySearchTree.Find(13);
            Assert.NotNull(findNode);

            binarySearchTree.Remove(13);
            Assert.Equal(23, binarySearchTree.Count);
        }
    }
}
