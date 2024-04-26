using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Lists.LinkLists
{
    /// <summary>
    /// 单链表节点
    /// </summary>
    public class LinkListNode<T>
    {
        /// <summary>
        /// 数据元素
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 下一个节点
        /// </summary>
        public LinkListNode<T> Next { get; set; }
    }
}
