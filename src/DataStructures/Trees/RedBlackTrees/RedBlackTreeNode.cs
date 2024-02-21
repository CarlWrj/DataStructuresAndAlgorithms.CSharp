using DataStructures.Trees.BinarySearchTrees;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataStructures.Trees.RedBlackTrees
{
    /// <summary>
    /// 颜色类型
    /// </summary>
    public enum RedBlackTreeNodeColor
    {
        Red = 0,
        Black = 1
    };

    /// <summary>
    /// 红黑树结点
    /// </summary>
    public class RedBlackTreeNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// 颜色
        /// </summary>
        public RedBlackTreeNodeColor Color { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 父结点
        /// </summary>
        public RedBlackTreeNode<T> Parent { get; set; }

        /// <summary>
        /// 左子树
        /// </summary>
        public RedBlackTreeNode<T> LeftChild { get; set; }

        /// <summary>
        /// 右子树
        /// </summary>
        public RedBlackTreeNode<T> RightChild { get; set; }
    }
}
