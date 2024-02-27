using DataStructures.Trees.BTrees;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Trees.BPlusTrees
{
    /// <summary>
    /// B+树结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BPlusTreeNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// 结点关键字
        /// </summary>
        public List<T> Keys { get; set; }

        /// <summary>
        /// 子结点
        /// </summary>
        public List<BPlusTreeNode<T>> Children { get; set; }

        /// <summary>
        /// 父结点
        /// </summary>
        public BPlusTreeNode<T> Parent { get; set; }

        /// <summary>
        /// 顺序查找的下一个结点
        /// </summary>
        public BPlusTreeNode<T> Next { get; set; }

        public BPlusTreeNode()
        {
            Keys = new List<T>();
            Children = new List<BPlusTreeNode<T>>();
        }
    }
}
