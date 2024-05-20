using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Heaps
{
    /// <summary>
    /// 大根堆
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryMaxHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// 堆
        /// </summary>
        public IList<T> Heap { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BinaryMaxHeap() : this(new List<T>())
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BinaryMaxHeap(IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            Heap = list;
        }

        /// <summary>
        /// 构建大根堆
        /// </summary>
        /// <param name="list"></param>
        public void BuildMaxHeap()
        {
            if (Heap.Count <= 1)
            {
                return;
            }

            //从后往前调整
            for (int i = Heap.Count / 2; i > 0; i--)
            {
                HeapAdjust(i, Heap.Count);
            }
        }

        /// <summary>
        /// 堆调整
        /// </summary>
        /// <param name="list"></param>
        /// <param name="k"></param>
        public void HeapAdjust(int k, int len)
        {
            var temp = Heap[k - 1];
            for (int i = 2 * k; i <= len; i *= 2)
            {
                --i;

                //获取左右节点中较大的一个
                if (i < len - 1 && Heap[i].CompareTo(Heap[i + 1]) < 0)
                {
                    i++;
                }
                //如果父结点比左右节点都大，那么不需要调整
                if (temp.CompareTo(Heap[i]) > 0)
                {
                    break;
                }
                //否则将左右节点中较大的调整到父节点，并将比较的下标调整为子节点下标以供继续向下调整
                else
                {
                    Heap[k - 1] = Heap[i];
                    k = ++i;
                }
            }
            Heap[k - 1] = temp;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            //添加到末尾
            Heap.Add(value);
            if (Heap.Count == 1)
            {
                return;
            }

            //与父级比较，如果大于父级那么进行交换，并且继续向上比较
            var index = Heap.Count - 1;
            var parentIndex = Heap.Count / 2;
            while (index >= 0 && parentIndex > 0 && Heap[parentIndex - 1].CompareTo(Heap[index]) < 0)
            {
                //交换
                var temp = Heap[parentIndex - 1];
                Heap[parentIndex - 1] = Heap[index];
                Heap[index] = temp;

                index = parentIndex - 1;
                parentIndex = parentIndex / 2;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="value"></param>
        public void Delete(T value)
        {
            //用最后一个元素代替删除的元素
            var deleteIndex = Heap.IndexOf(value);
            Heap[deleteIndex] = Heap[Heap.Count - 1];
            Heap.RemoveAt(Heap.Count - 1);

            //对代替的元素进行重新向下调整
            HeapAdjust(deleteIndex + 1, Heap.Count);
        }
    }
}
