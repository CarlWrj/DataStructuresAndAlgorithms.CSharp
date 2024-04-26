using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Lists.DLinkLists
{
    /// <summary>
    /// 双链表节点
    /// </summary>
    public class DLinkListNode<T>
    {
        /// <summary>
        /// 数据元素
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 下一个节点
        /// </summary>
        public DLinkListNode<T> Next { get; set; }

        /// <summary>
        /// 上一个节点
        /// </summary>
        public DLinkListNode<T> Prior { get; set; }
    }
}
