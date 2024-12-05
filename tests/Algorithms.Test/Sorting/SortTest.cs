using Algorithms.Sorting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Test.Sorting
{
    /// <summary>
    /// 排序测试
    /// </summary>
    public class SortTest
    {
        /// <summary>
        /// 实际的排序值
        /// </summary>

        public List<int> ActualList = new List<int>
        {
            0, 23, 42, 4, 16, 8, 15, 9, 55, 0, 34, 12, 2
        };

        /// <summary>
        /// 期待的排序值
        /// </summary>
        public List<int> ExpectedList = new List<int>
        {
            0, 0, 2, 4, 8, 9, 12, 15, 16, 23, 34, 42, 55
        };

        private void AssertList(List<int> actualList, List<int> expectedList = null)
        {
            expectedList ??= ExpectedList;

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

        #region 插入排序
        [Fact]
        public void InsertionSort()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            InsertionSorter.InsertionSort(actualList);

            AssertList(actualList);
        }

        [Fact]
        public void InsertionSortBySentinel()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            actualList.Insert(0, 0);
            InsertionSorter.InsertionSortBySentinel(actualList);
            actualList.RemoveAt(0);

            AssertList(actualList);
        }

        [Fact]
        public void InsertionSortByHalf()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            actualList.Insert(0, 0);
            InsertionSorter.InsertionSortByHalf(actualList);
            actualList.RemoveAt(0);

            AssertList(actualList);
        }

        [Fact]
        public void InsertionSortByShell()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            InsertionSorter.InsertionSortByShell(actualList);

            AssertList(actualList);
        }
        #endregion

        #region 冒泡排序
        [Fact]
        public void BubbleSort()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            BubbleSorter.Sort(actualList);

            AssertList(actualList);
        }
        #endregion

        #region 快速排序
        [Fact]
        public void QuickSort()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            QuickSorter.Sort(actualList);

            AssertList(actualList);
        }
        #endregion

        #region 简单选择排序
        [Fact]
        public void SelectionSort()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            SelectionSorter.SimpleSelectSort(actualList);

            AssertList(actualList);
        }
        #endregion

        #region 大根堆选择排序
        [Fact]
        public void MaxHeapSort()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            SelectionSorter.MaxHeapSort(actualList);

            AssertList(actualList);
        }
        #endregion

        #region 归并排序
        [Fact]
        public void MergeSort()
        {
            var actualList = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(ActualList));
            MergeSorter.Sort(actualList);

            AssertList(actualList);
        }
        #endregion
    }
}
