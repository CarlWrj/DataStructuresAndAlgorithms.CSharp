using DataStructures.Trees.BinarySearchTrees;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DataStructures.Trees.HuffmanTrees
{
    /// <summary>
    /// 哈夫曼树
    /// </summary>
    public class HuffmanTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// 根结点
        /// </summary>
        public HuffmanTreeNode<T> Root { get; set; }

        /// <summary>
        ///  构造哈夫曼树
        /// </summary>
        /// <param name="huffmanTreeNodes"></param>
        /// <exception cref="ArgumentException"></exception>
        public HuffmanTree(List<HuffmanTreeNode<T>> huffmanTreeNodes)
        {
            if (huffmanTreeNodes == null || huffmanTreeNodes.Count == 0)
            {
                throw new ArgumentException();
            }

            while (huffmanTreeNodes.Count > 1)
            {
                //选取出两棵根结点的权值最小的树，作为新结点的左右子树，并在结点集中移除这两棵树，
                var minWeight = huffmanTreeNodes.Min(p => p.Weight);
                var leftNode = huffmanTreeNodes.First(p => p.Weight == minWeight);
                huffmanTreeNodes.Remove(leftNode);
                minWeight = huffmanTreeNodes.Min(p => p.Weight);
                var rightNode = huffmanTreeNodes.First(p => p.Weight == minWeight);
                huffmanTreeNodes.Remove(rightNode);

                //构造一棵新的二叉树．且置新的二叉树的根结点的权值为其左、右子树上根结点的权值之和
                var newNode = new HuffmanTreeNode<T>(leftNode, rightNode);

                //将新的结点加入结点集中
                huffmanTreeNodes.Add(newNode);
            }

            Root = huffmanTreeNodes[0];
        }

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public HuffmanTreeNode<T> Find(T item)
        {
            if (Root == null)
                throw new Exception("树是空的");

            return FindNode(Root, item);
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="currentNode">开始查找的节点</param>
        /// <param name="item">查找值</param>
        /// <returns>成功则返回节点，失败则为null</returns>
        private HuffmanTreeNode<T> FindNode(HuffmanTreeNode<T> currentNode, T item)
        {
            if (currentNode == null)
                return currentNode;

            if (item.Equals(currentNode.Value))
            {
                return currentNode;
            }

            if (currentNode.LeftChild != null && item.CompareTo(currentNode.Value) < 0)
            {
                return FindNode(currentNode.LeftChild, item);
            }

            if (currentNode.RightChild != null && item.CompareTo(currentNode.Value) > 0)
            {
                return FindNode(currentNode.RightChild, item);
            }

            return null;
        }
        #endregion
    }
}
