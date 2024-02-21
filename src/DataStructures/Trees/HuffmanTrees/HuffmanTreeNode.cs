using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Trees.HuffmanTrees
{
    /// <summary>
    /// 
    /// </summary>
    public class HuffmanTreeNode<T> where T : IComparable<T>
    {
        public HuffmanTreeNode(T value, int weight)
        {
            Value = value;
            Weight = weight;
        }

        public HuffmanTreeNode(HuffmanTreeNode<T> leftChild, HuffmanTreeNode<T> rightChild)
        {

            LeftChild = leftChild;
            RightChild = rightChild;
            Weight = leftChild.Weight + rightChild.Weight;
        }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 权值
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 父
        /// </summary>
        public HuffmanTreeNode<T> Parent { get; set; }

        /// <summary>
        /// 左子树
        /// </summary>
        public HuffmanTreeNode<T> LeftChild { get; set; }

        /// <summary>
        /// 右子树
        /// </summary>
        public HuffmanTreeNode<T> RightChild { get; set; }
    }
}
