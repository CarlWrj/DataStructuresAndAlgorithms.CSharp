using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Lists
{
    /// <summary>
    /// 顺序表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SequenceList<T> where T : IComparable
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T[] Data { get; set; }

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
        public SequenceList(int maxSize)
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
        }

        /// <summary>
        /// 释放
        /// </summary>
        ~SequenceList()
        {
            //Data = null;
        }

        /// <summary>
        /// 增加容量
        /// </summary>
        /// <param name="length"></param>
        public void IncreaseSize(int length)
        {
            var tempData = Data;
            Data = new T[MaxSize + length];
            for (int i = 0; i < Length; i++)
            {
                Data[i] = tempData[i];
            }

            MaxSize = MaxSize + length;
        }

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="position">从1开始算</param>
        /// <param name="element"></param>
        public bool Insert(int position, T element)
        {
            if (position < 1)
            {
                return false;
            }
            if (position > MaxSize)
            {
                return false;
            }

            for (int i = Length; i >= position; i--)
            {
                Data[i] = Data[i - 1];
            }

            Data[position - 1] = element;
            Length++;
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Delete(int position)
        {
            if (position < 1 || position > Length)
            {
                return false;
            }

            for (int i = position; i < Length; i++)
            {
                Data[i - 1] = Data[i];
            }
            Data[Length - 1] = default;
            Length--;
            return true;
        }

        /// <summary>
        /// 按位查找-重载下标
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return Data[index - 1];
            }
            set
            {
                Data[index - 1] = value;
            }
        }

        /// <summary>
        /// 按值查找
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public int Locate(T element)
        {
            if (Length == 0)
            {
                return 0;
            }

            for (int i = 0; i < Length; i++)
            {
                if (Data[i].Equals(element))
                {
                    return i + 1;
                }
            }

            return 0;
        }
    }
}
