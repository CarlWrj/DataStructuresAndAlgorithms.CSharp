using DataStructures.Heaps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    /// <summary>
    /// 选择排序
    /// </summary>
    public static class SelectionSorter
    {
        #region 简单选择排序
        /// <summary>
        /// 简单选择排序
        /// 原理：每一趟在待排序元素中选取关键字最小（或最大）的元素加入有序子序列
        /// 空间复杂度：O(1)
        /// 时间复杂度：O(n²)
        /// 稳定性：不稳定
        /// 适用性：顺序表、链表都可
        /// </summary>
        public static void SimpleSelectSort<T>(this IList<T> collection) where T : IComparable<T>
        {
            var i = 0;
            for (; i < collection.Count; i++)
            {
                var min = i;
                for (var j = i + 1; j < collection.Count; j++)
                {
                    if (collection[j].CompareTo(collection[min]) < 0)
                    {
                        min = j;
                    }
                }

                var t = collection[i];
                collection[i] = collection[min];
                collection[min] = t;
            }
        }
        #endregion

        #region 大根堆排序
        /// <summary>
        /// 大根堆排序，得到的是递增序列
        /// 空间复杂度：O(1)
        /// 时间复杂度：O(nlogn)
        /// 稳定性：不稳定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void MaxHeapSort<T>(this IList<T> collection) where T : IComparable<T>
        {
            var binaryMaxHeap = new BinaryMaxHeap<T>(collection);
            binaryMaxHeap.BuildMaxHeap();
            for (var i = binaryMaxHeap.Heap.Count; i > 0; i--)
            {
                //交换头尾值
                var temp = binaryMaxHeap.Heap[0];
                binaryMaxHeap.Heap[0] = binaryMaxHeap.Heap[i - 1];
                binaryMaxHeap.Heap[i - 1] = temp;

                //剩余的待排序元素整理成堆
                binaryMaxHeap.HeapAdjust(1, i - 1);
            }
        }
        #endregion
    }
}
