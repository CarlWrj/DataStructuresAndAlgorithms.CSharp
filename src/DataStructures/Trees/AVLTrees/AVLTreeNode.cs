using DataStructures.Trees.BinarySearchTrees;
using System;

namespace DataStructures.Trees.AVLTrees
{
    /// <summary>
    /// AVL树结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AVLTreeNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 父
        /// </summary>
        public AVLTreeNode<T> Parent { get; set; }

        /// <summary>
        /// 左子树
        /// </summary>
        public new AVLTreeNode<T> LeftChild { get; set; }

        /// <summary>
        /// 右子树
        /// </summary>
        public new AVLTreeNode<T> RightChild { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
    }
}
