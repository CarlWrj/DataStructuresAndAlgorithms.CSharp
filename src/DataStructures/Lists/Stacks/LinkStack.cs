using DataStructures.Lists.LinkLists;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Lists.Stacks
{
    public class LinkStackNode<T>
    {
        /// <summary>
        /// 数据元素
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 下一个节点
        /// </summary>
        public LinkStackNode<T> Next { get; set; }
    }

    /// <summary>
    /// 链栈
    /// </summary>
    public class LinkStack<T>
    {
        /// <summary>
        /// 栈顶
        /// </summary>
        private LinkStackNode<T> _top;

        /// <summary>
        /// 栈顶值
        /// </summary>
        public T Top
        {
            get
            {
                if (_top == null)
                {
                    return default;
                }
                else
                {
                    return _top.Data;
                }
            }
        }

        public bool Empty
        {
            get
            {
                return _top == null;
            }
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Push(T element)
        {
            var linkNode = new LinkStackNode<T>()
            {
                Data = element,
                Next = _top
            };
            _top = linkNode;

            return true;
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (_top == null)
            {
                return default;
            }

            var linkNode = _top;
            _top = _top.Next;
            return linkNode.Data;
        }
    }
}
