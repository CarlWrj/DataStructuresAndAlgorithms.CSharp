using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    /// <summary>
    /// 插入排序
    /// </summary>
    public class InsertionSorter
    {
        /// <summary>
        /// 直接插入排序
        /// 原理：每次都让n-1的位置有序
        /// 空间复杂度：O(1)
        /// 时间复杂度：O(n²)
        /// 稳定性：稳定
        /// 适用性：线性表、链表都可
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void InsertionSort<T>(IList<T> list) where T : IComparable<T>
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].CompareTo(list[i - 1]) < 0)
                {
                    var temp = list[i];
                    var j = i - 1;
                    while (j >= 0 && list[j].CompareTo(temp) > 0)
                    {
                        list[j + 1] = list[j];
                        j--;
                    }
                    list[j + 1] = temp;
                }
            }
        }

        /// <summary>
        /// 带哨兵插入排序
        /// 原理：和直接插入排序一样，只不过数组的第一位不使用，作为哨兵用于存储插入值
        /// 空间复杂度：O(1)
        /// 时间复杂度：O(n²)
        /// 稳定性：稳定
        /// 适用性：线性表、链表都可
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void InsertionSortBySentinel<T>(IList<T> list) where T : IComparable<T>
        {
            for (int i = 2; i < list.Count; i++)
            {
                if (list[i].CompareTo(list[i - 1]) < 0)
                {
                    //与前面的区别就是这个插入值存在数组的第一位
                    list[0] = list[i];
                    var j = i - 1;
                    //优点：这里不需要判断j>=0
                    while (list[j].CompareTo(list[0]) > 0)
                    {
                        list[j + 1] = list[j];
                        j--;
                    }
                    list[j + 1] = list[0];
                }
            }
        }

        /// <summary>
        /// 折半插入排序
        /// 原理：先折半找到插入的位置，再进行元素的移动和插入
        /// 优点：数据不很大时，能表现出很好的性能
        /// 空间复杂度：O(1)
        /// 时间复杂度：O(n²)
        /// 稳定性：稳定
        /// 适用性：线性表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void InsertionSortByHalf<T>(IList<T> list) where T : IComparable<T>
        {
            for (int i = 2; i < list.Count; i++)
            {
                list[0] = list[i];
                var low = 1;
                var high = i - 1;

                //折半找到要插入的位置
                while (low <= high)
                {
                    var mid = (low + high) / 2;
                    if (list[mid].CompareTo(list[0]) > 0)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }

                //统一后移位置，空出插入位置
                for (int j = i - 1; j >= high + 1; j--)
                {
                    list[j + 1] = list[j];
                }

                //插入
                list[high + 1] = list[0];
            }
        }

        /// <summary>
        /// 希尔排序
        /// 原理：先将待排序表分割成若干形如L[i,i+d,i+2d,⋯,i+kd]的“特殊”子表，
        ///       即把相隔某个“增量”的记录组成一个子表，对各个子表分别进行直接插入排序，
        ///       当整个表中的元素已呈“基本有序”时，再对全体记录进行一次直接插入排序。
        /// 空间复杂度：O(1)
        /// 时间复杂度：当n为某个特定范围时O(n^1.3)，最坏情况下为O(n²)
        /// 稳定性：不稳定
        /// 适用性：线性表
        /// </summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void InsertionSortByShell<T>(IList<T> list) where T : IComparable<T>
        {
            //将数组每次折半分组
            for (var step = list.Count / 2; step > 0; step /= 2)
            {
                //对每个分组进行插入排序
                for (var i = step; i < list.Count; i++)
                {
                    if (list[i].CompareTo(list[i - step]) < 0)
                    {
                        var temp = list[i];
                        var j = i - step;
                        while (j >= 0 && list[j].CompareTo(temp) > 0)
                        {
                            list[j + step] = list[j];
                            j -= step;
                        }
                        list[j + step] = temp;
                    }
                }
            }
        }
    }
}
