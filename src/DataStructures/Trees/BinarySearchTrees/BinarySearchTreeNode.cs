using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Trees.BinarySearchTrees
{
    /// <summary>
    /// 二叉查找树结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTreeNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 父
        /// </summary>
        public BinarySearchTreeNode<T> Parent { get; set; }

        /// <summary>
        /// 左子树
        /// </summary>
        public BinarySearchTreeNode<T> LeftChild { get; set; }

        /// <summary>
        /// 右子树
        /// </summary>
        public BinarySearchTreeNode<T> RightChild { get; set; }
    }
}
