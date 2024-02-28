using Algorithms.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Test.Sorting
{
    public static class InsertionSorterTest
    {
        [Fact]
        public static void InsertionSort()
        {
            var actualList = new List<int>
            {
                23, 42, 4, 16, 8, 15, 9, 55, 0, 34, 12, 2
            };
            InsertionSorter.InsertionSort(actualList);

            var expectedList = new List<int>
            {
                0, 2, 4, 8, 9, 12, 15, 16, 23, 34, 42, 55
            };

            bool isListEqual = true;
            for (int i = 0; i < actualList.Count; i++)
            {
                if (actualList[i] != expectedList[i])
                {
                    isListEqual = false;
                    break;
                }
            }
            Assert.True(isListEqual);
        }

        [Fact]
        public static void InsertionSortBySentinel()
        {
            var actualList = new List<int>
            {
                0, 23, 42, 4, 16, 8, 15, 9, 55, 0, 34, 12, 2
            };
            InsertionSorter.InsertionSortBySentinel(actualList);

            var expectedList = new List<int>
            {
                0, 0, 2, 4, 8, 9, 12, 15, 16, 23, 34, 42, 55
            };

            bool isListEqual = true;
            for (int i = 1; i < actualList.Count; i++)
            {
                if (actualList[i] != expectedList[i])
                {
                    isListEqual = false;
                    break;
                }
            }
            Assert.True(isListEqual);
        }
    }
}
