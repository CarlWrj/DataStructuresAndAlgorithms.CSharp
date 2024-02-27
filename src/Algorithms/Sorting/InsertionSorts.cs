using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    public class InsertionSorts
    {
        /// <summary>
        /// 插入排序
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
        /// 数组的第一位不使用，作为哨兵用于存储插入值
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
    }
}
