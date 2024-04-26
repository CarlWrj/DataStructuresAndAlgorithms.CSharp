using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Lists.Stacks
{

    /// <summary>
    /// 顺序栈
    /// </summary>
    public class SequenceStack<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T[] Data { get; set; }

        /// <summary>
        /// 栈顶
        /// </summary>
        private int _top;
        /// <summary>
        /// 栈顶
        /// </summary>
        public T Top
        {
            get
            {
                if (_top == -1)
                {
                    return default;
                }
                else
                {
                    return Data[_top];
                }
            }
        }

        /// <summary>
        /// 最大容量
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="maxSize"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public SequenceStack(int maxSize)
        {
            if (maxSize < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (maxSize == 0)
            {
                Data = new T[0];
            }
            else
            {
                Data = new T[maxSize];
            }
            MaxSize = maxSize;
            Length = 0;
            _top = -1;
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Push(T element)
        {
            if (_top == MaxSize - 1)
            {
                return false;
            }

            Data[++_top] = element;

            return true;
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (_top == -1)
            {
                return default;
            }

            return Data[_top--];
        }

        /// <summary>
        /// 读栈顶元素 
        /// </summary>
        /// <returns></returns>
        public T GetTop()
        {
            if (_top == -1)
            {
                return default;
            }

            return Data[_top];
        }
    }
}
