using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Trees.BTrees
{
    /// <summary>
    /// B树结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BTreeNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// 结点关键字
        /// </summary>
        public List<T> Keys { get; set; }

        /// <summary>
        /// 子结点
        /// </summary>
        public List<BTreeNode<T>> Children { get; set; }

        /// <summary>
        /// 父结点
        /// </summary>
        public BTreeNode<T> Parent { get; set; }

        public BTreeNode()
        {
            Keys = new List<T>();
            Children = new List<BTreeNode<T>>();
        }
    }
}
