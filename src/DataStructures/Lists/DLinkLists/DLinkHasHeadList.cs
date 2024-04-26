using DataStructures.Lists.LinkLists;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Lists.DLinkLists
{
    /// <summary>
    /// 双链表-带头结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DLinkHasHeadList<T>
    {
        /// <summary>
        /// 头节点
        /// </summary>
        public LinkListNode<T> Head { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length
        {
            get
            {
                var lenght = -1;
                var currentNode = Head;
                while (currentNode != null)
                {
                    currentNode = currentNode.Next;
                    lenght++;
                }
                return lenght;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="isUsedHead">是否使用头节点</param>
        public DLinkHasHeadList()
        {
            Head = new LinkListNode<T>();
        }


        /// <summary>
        /// 在某节点后插入，时间复杂度O(1)
        /// </summary>
        /// <param name="linkListNode"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool InsertNextNode(DLinkListNode<T> linkListNode, T element)
        {
            if (linkListNode == null)
            {
                return false;
            }

            var newNode = new DLinkListNode<T>()
            {
                Data = element,
                Next = linkListNode.Next,
                Prior = linkListNode
            };
            if (newNode.Next != null)
            {
                newNode.Next.Prior = newNode;
            }
            linkListNode.Next = newNode;
            return true;
        }

        /// <summary>
        /// 指定节点删除，时间复杂度O(1)
        /// </summary>
        /// <param name="dLinkListNode"></param>
        /// <returns></returns>
        public bool DeleteNode(DLinkListNode<T> dLinkListNode)
        {
            if (dLinkListNode == null)
            {
                return false;
            }

            if (dLinkListNode.Prior != null)
            {
                dLinkListNode.Prior.Next = dLinkListNode.Next;
            }
            if (dLinkListNode.Next != null)
            {
                dLinkListNode.Next.Prior = dLinkListNode.Prior;
            }

            return true;
        }

        /// <summary>
        /// 按位查找，时间复杂度O(n)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public LinkListNode<T> this[int index]
        {
            get
            {
                if (index < 0)
                {
                    return null;
                }

                var currentIndex = 0;
                var currentNode = Head.Next;
                while (currentNode != null && currentIndex < index - 1)
                {
                    currentNode = currentNode.Next;
                    currentIndex++;
                }
                return currentNode;
            }
        }
    }
}
