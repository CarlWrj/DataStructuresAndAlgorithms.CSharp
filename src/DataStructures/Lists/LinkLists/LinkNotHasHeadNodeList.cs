using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DataStructures.Lists.LinkLists
{
    /// <summary>
    /// 单链表-不带头结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkNotHasHeadNodeList<T>
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
                var lenght = 0;
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
        public LinkNotHasHeadNodeList()
        {
        }

        /// <summary>
        /// 尾插法创建单链表
        /// </summary>
        public static LinkNotHasHeadNodeList<T> TailInsert(List<T> list)
        {
            var linkList = new LinkNotHasHeadNodeList<T>();
            var tailNode = linkList.Head;

            foreach (var item in list)
            {
                var nextNode = new LinkListNode<T>() { Data = item };

                if (linkList.Head == null)
                {
                    linkList.Head = tailNode = nextNode;
                    continue;
                }

                tailNode.Next = nextNode;
                tailNode = nextNode;
            }

            return linkList;
        }

        /// <summary>
        /// 头插法建立单链表
        /// </summary>
        /// <param name="isUsedHead"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static LinkNotHasHeadNodeList<T> HeadInsert(List<T> list)
        {
            var linkList = new LinkNotHasHeadNodeList<T>();

            foreach (var item in list)
            {
                var nextNode = new LinkListNode<T>() { Data = item };

                if (linkList.Head == null)
                {
                    linkList.Head = nextNode;
                }
                else
                {
                    nextNode.Next = linkList.Head;
                    linkList.Head = nextNode;
                }
            }

            return linkList;
        }


        /// <summary>
        /// 按位序插入，时间复杂度O(n)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Insert(int index, T element)
        {
            if (index < 0)
            {
                return false;
            }

            //则对第一个元素进行操作时需要更新头节点
            if (index == 0)
            {
                var firstNode = new LinkListNode<T>() { Data = element, Next = Head };
                Head = firstNode;
                return true;
            }

            //查找插入节点
            var currentNode = this[index];
            if (currentNode == null)
            {
                return false;
            }

            //插入节点
            var newNode = new LinkListNode<T>() { Data = element };
            newNode.Next = currentNode.Next;
            currentNode.Next = newNode;

            return true;
        }

        /// <summary>
        /// 在某节点后插入，时间复杂度O(1)
        /// </summary>
        /// <param name="linkListNode"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool InsertNextNode(LinkListNode<T> linkListNode, T element)
        {
            if (linkListNode == null)
            {
                return false;
            }

            var newNode = new LinkListNode<T>() { Data = element, Next = linkListNode.Next };
            linkListNode.Next = newNode;
            return true;
        }

        /// <summary>
        /// 在某节点前插入，时间复杂度O(1)
        /// </summary>
        /// <param name="linkListNode"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool InsertPriorNode(LinkListNode<T> linkListNode, T element)
        {
            if (linkListNode == null)
            {
                return false;
            }

            //按后插更新下一个节点值
            var newNode = new LinkListNode<T>() { Next = linkListNode.Next };
            linkListNode.Next = newNode;
            //交换当前节点与新节点的数据，达到前插的效果
            newNode.Data = linkListNode.Data;
            linkListNode.Data = element;

            return true;
        }

        /// <summary>
        /// 按位序删除，时间复杂度O(n)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Delete(int index)
        {
            if (index < 0 || Head == null)
            {
                return false;
            }

            //对第一个元素进行操作时需要更新头节点
            if (index == 0)
            {
                var firstNode = Head;
                Head = firstNode.Next;
                return true;
            }

            //查找插入节点
            var currentNode = this[index - 1];
            if (currentNode == null || currentNode.Next == null)
            {
                return false;
            }

            var deleteNode = currentNode.Next;
            currentNode.Next = deleteNode.Next;

            return true;
        }

        /// <summary>
        /// 指定节点删除，时间复杂度（最后一个节点为O(n)，其余O(1)）
        /// </summary>
        /// <param name="linkListNode"></param>
        /// <returns></returns>
        public bool DeleteNode(LinkListNode<T> linkListNode)
        {
            if (linkListNode == null)
            {
                return false;
            }

            //如果是最后一个节点，那么需要重头开始找到上一个节点
            if (linkListNode.Next == null)
            {
                var currentNode = Head;
                while (currentNode != null && currentNode != linkListNode)
                {
                    currentNode = currentNode.Next;
                }
                if (currentNode == null)
                {
                    return false;
                }

                currentNode.Next = null;
                linkListNode = null;
            }
            //如果不是最后一个结点，那么采用交换删除节点与下一个节点的数据，达到删除的效果
            else
            {
                var deleteNode = linkListNode.Next;
                linkListNode.Next = deleteNode.Next;
                linkListNode.Data = deleteNode.Data;
                deleteNode = null;
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
                var currentNode = Head;
                while (currentNode != null && currentIndex < index)
                {
                    currentNode = currentNode.Next;
                    currentIndex++;
                }
                return currentNode;
            }
        }

        /// <summary>
        /// 按值查找，时间复杂度O(n)
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public LinkListNode<T> Locate(T element)
        {
            var currentNode = Head;
            while (currentNode != null && currentNode.Data != null && !currentNode.Data.Equals(element))
            {
                currentNode = currentNode.Next;
            }
            return currentNode;
        }
    }
}
