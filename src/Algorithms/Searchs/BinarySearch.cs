using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Searchs
{
    public class BinarySearch
    {
        public static int FindIndex<T>(T[] sortedData, T item) where T : IComparable<T>
        {
            var low = 0;
            var high = sortedData.Length - 1;

            while (low < high)
            {
                var middle = (low + high) / 2;
                if (item.CompareTo(sortedData[middle]) == 0)
                {
                    return middle;
                }
                else if (item.CompareTo(sortedData[middle]) > 0)
                {
                    low = middle + 1;
                }
                else if (item.CompareTo(sortedData[middle]) < 0)
                {
                    high = middle - 1;
                }
            }

            return -1;
        }
    }
}
