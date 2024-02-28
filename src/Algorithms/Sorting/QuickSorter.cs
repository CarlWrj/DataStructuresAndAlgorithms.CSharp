using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    /// <summary>
    /// 快速排序
    /// </summary>
    public class QuickSorter
    {
        public static void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            QuickSort(list, 0, list.Count - 1);
        }

        private static void QuickSort<T>(IList<T> list, int low, int high) where T : IComparable<T>
        {
            if (low < high)
            {
                var privotIndex = Partition(list, low, high);
                QuickSort(list, low, privotIndex - 1);
                QuickSort(list, privotIndex + 1, high);
            }
        }

        private static int Partition<T>(IList<T> list, int low, int high) where T : IComparable<T>
        {
            var pivotValue = list[low];
            while (low < high)
            {
                while (low < high && list[high].CompareTo(pivotValue) >= 0)
                {
                    high--;
                }
                list[low] = list[high];
                while (low < high && list[low].CompareTo(pivotValue) <= 0)
                {
                    low++;
                }
                list[high] = list[low];
            }
            list[low] = pivotValue;
            return low;
        }
    }
}
