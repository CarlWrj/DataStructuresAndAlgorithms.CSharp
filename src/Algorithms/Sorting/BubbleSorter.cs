using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    /// <summary>
    /// 冒泡排序
    /// </summary>
    public class BubbleSorter
    {
        public static void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                //校验是否有进行交换操作
                var swapped = false;

                //从后往前冒泡，每次将小的值往前交换
                for (int j = list.Count - 1; j > i; j--)
                {
                    if (list[j].CompareTo(list[j - 1]) < 0)
                    {
                        var temp = list[j];
                        list[j] = list[j - 1];
                        list[j - 1] = temp;
                        swapped = true;
                    }
                }

                //如果没有交换表示已经有序，不需要进行后续操作
                if (!swapped)
                {
                    break;
                }
            }
        }
    }
}
