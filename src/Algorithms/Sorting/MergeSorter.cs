using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    /// <summary>
    /// 归并排序
    /// </summary>
    public class MergeSorter
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            List<T> auxiliaryList = new List<T>(list.Count);
            auxiliaryList.AddRange(list);
            Sort(list, auxiliaryList, 0, list.Count - 1);
        }

        /// <summary>
        /// 排序 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="auxiliaryList"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        private static void Sort<T>(IList<T> list, IList<T> auxiliaryList, int low, int high) where T : IComparable<T>
        {
            if (low < high)
            {
                int mid = (low + high) / 2;
                Sort(list, auxiliaryList, low, mid);
                Sort(list, auxiliaryList, mid + 1, high);
                Merge(list, auxiliaryList, low, mid, high);
            }
        }

        /// <summary>
        /// 归并
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="auxiliaryList"></param>
        /// <param name="low"></param>
        /// <param name="mid"></param>
        /// <param name="high"></param>
        private static void Merge<T>(IList<T> list, IList<T> auxiliaryList, int low, int mid, int high) where T : IComparable<T>
        {
            int i, j, k;
            //将需要排序的数组复制到辅助数组中
            for (k = low; k <= high; k++)
            {
                auxiliaryList[k] = list[k];
            }

            //分组进行对比大小
            for (i = low, j = mid + 1, k = i; i <= mid && j <= high; k++)
            {
                if (auxiliaryList[i].CompareTo(auxiliaryList[j]) < 0)
                {
                    list[k] = auxiliaryList[i++];
                }
                else
                {
                    list[k] = auxiliaryList[j++];
                }
            }

            //剩下的加到最后
            while (i <= mid)
            {
                list[k++] = auxiliaryList[i++];
            }
            while (j <= high)
            {
                list[k++] = auxiliaryList[j++];
            }
        }
    }
}
