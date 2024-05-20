using DataStructures.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Test.Heaps
{
    /// <summary>
    /// 大根堆测试
    /// </summary>
    public class MaxHeapTest
    {
        [Fact]
        public static void Test()
        {
            BinaryMaxHeap<long> maxHeap = new BinaryMaxHeap<long>();
            maxHeap.Add(23);
            maxHeap.Add(42);
            maxHeap.Add(4);
            maxHeap.Add(16);
            maxHeap.Add(8);
            maxHeap.Add(1);
            maxHeap.Add(3);
            maxHeap.Add(100);
            maxHeap.Add(5);
            maxHeap.Add(7);
            /***************************************
             **              100       
             **            /     \       
             **           42      4      
             **          /  \    /  \
             **        23    8  1    3
             **       / \   /
             *      16   5 7
             * 
             ***************************************
             */
            var expected = new List<long>() { 100, 42, 4, 23, 8, 1, 3, 16, 5, 7 };
            for (int i = 0; i < maxHeap.Heap.Count; i++)
            {
                Assert.Equal(expected[i], maxHeap.Heap[i]);
            }

            maxHeap.Delete(42);
            /***************************************
             **              100       
             **            /     \       
             **           23       4      
             **          /  \    /  \
             **         16   8  1    3
             **        / \   
             *        7  5 
             * 
             ***************************************
             */
            expected = new List<long>() { 100, 23, 4, 16, 8, 1, 3, 7, 5 };
            for (int i = 0; i < maxHeap.Heap.Count; i++)
            {
                Assert.Equal(expected[i], maxHeap.Heap[i]);
            }
        }

        [Fact]
        public static void BuildTest()
        {
            List<long> list = new List<long>()
            {
                1,2,3,4,5,6,7,8,
            };
            BinaryMaxHeap<long> maxHeap = new BinaryMaxHeap<long>(list);
            maxHeap.BuildMaxHeap();

            var expected = new List<long>() { 8, 5, 7, 4, 1, 6, 3, 2 };
            for (int i = 0; i < maxHeap.Heap.Count; i++)
            {
                Assert.Equal(expected[i], maxHeap.Heap[i]);
            }
        }
    }
}
