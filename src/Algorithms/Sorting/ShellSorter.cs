using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    /// <summary>
    /// 希尔排序
    /// </summary>
    public class ShellSorter
    {
        public static void Sort<T>(IList<T> list) where T : IComparable<T>
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
