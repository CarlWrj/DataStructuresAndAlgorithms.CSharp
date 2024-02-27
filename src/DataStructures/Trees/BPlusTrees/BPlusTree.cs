using DataStructures.Trees.BTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataStructures.Trees.BPlusTrees
{
    /// <summary>
    /// B+树
    /// </summary>
    public class BPlusTree<T> where T : IComparable<T>
    {
        #region 字段和构造函数
        /// <summary>
        /// 阶数
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// 每个结点最多可以存储的关键字数量
        /// </summary>
        public int MaxKeyCount { get; set; }

        /// <summary>
        /// 除根结点外每个结点最少需要有的关键字数量
        /// </summary>
        public int MinKeyCount { get; set; }

        /// <summary>
        /// 中位数下标
        /// </summary>
        public int MedianIndex { get; set; }

        /// <summary>
        /// 根结点
        /// </summary>
        public BPlusTreeNode<T> Root { get; set; }

        /// <summary>
        /// 头结点
        /// </summary>
        public BPlusTreeNode<T> Head { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="degree"></param>
        /// <exception cref="ArgumentException"></exception>
        public BPlusTree(int degree)
        {
            if (degree < 3)
            {
                throw new ArgumentException("阶数必须大于2");
            }

            Degree = degree;
            MaxKeyCount = degree;//B树需要-1，B+树不需要
            var d = (double)degree / 2;
            MinKeyCount = (int)(Math.Ceiling(d));//B树是[阶数/2]-1，B+树是[阶数/2]
            MedianIndex = degree / 2;
        }
        #endregion

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private BPlusTreeNode<T> Find(T value)
        {
            return Find(Root, value);
        }

        /// <summary>
        /// 查找（和B+树的区别：找到不返回而是继续查找）
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private BPlusTreeNode<T> Find(BPlusTreeNode<T> node, T value)
        {
            //如果是叶子结点则不需要再递归查找
            if (node.Children == null || node.Children.Count == 0)
            {
                if (node.Keys.Any(p => p.CompareTo(value) == 0))
                {
                    return node;
                }
                else
                {
                    return null;
                }
            }

            //找到不返回而是继续查找
            //if (node.Keys.Any(p => p.CompareTo(value) == 0))
            //{
            //    return node;
            //}

            //递归查找
            var index = node.Keys.ToList().FindLastIndex(p => p.CompareTo(value) < 0) + 1;
            return Find(node.Children[index], value);
        }
        #endregion

        #region 插入
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="value"></param>
        public void Insert(T value)
        {
            //用于记录分裂情况
            BPlusTreeNode<T> newSmallNode = null;
            BPlusTreeNode<T> newBigNode = null;
            var finished = false;

            //查找值插入到哪个节点中
            var insertNode = InsertFind(Root, value);
            while (insertNode != null && !finished)
            {
                //插入值到节点中
                Insert(insertNode, value, newSmallNode, newBigNode);

                //没有大于最大键值那么不需要向上分裂，此次插入结束
                if (insertNode.Keys.Count <= MaxKeyCount)
                {
                    finished = true;
                }
                //大于最大键值则进行向上分裂
                else
                {
                    //中位数为分裂到父结点的关键字
                    value = insertNode.Keys[MedianIndex];

                    //1 中位数右边一位到最后一位，分裂为一个新的大结点
                    newBigNode = new BPlusTreeNode<T>();
                    var i = 0;
                    for (i = MedianIndex + 1; i <= MaxKeyCount; i++)
                    {
                        newBigNode.Keys.Add(insertNode.Keys[i]);
                        if (insertNode.Children.Count > i)
                        {
                            newBigNode.Children.Add(insertNode.Children[i]);
                            insertNode.Children[i].Parent = newBigNode;
                        }
                    }
                    newBigNode.Parent = insertNode.Parent;

                    //2 原结点移除掉分裂出去的所有值
                    for (i = MedianIndex + 1; i <= MaxKeyCount; i++)
                    {
                        insertNode.Keys.RemoveAt(MedianIndex + 1);
                        if (insertNode.Children.Count > 0)
                        {
                            insertNode.Children.RemoveAt(MedianIndex + 1);
                        }
                    }
                    newSmallNode = insertNode;

                    //3 如果是叶子结点，则更新顺序查找的链表
                    if (insertNode.Children.Count == 0)
                    {
                        newBigNode.Next = newSmallNode.Next;
                        newSmallNode.Next = newBigNode;
                    }

                    //4 将中位数插入到父结点中
                    insertNode = insertNode.Parent;
                }
            }

            //空树或者根结点已被分裂,需添加根结点
            if (!finished)
            {
                var newRootNode = new BPlusTreeNode<T>();
                newRootNode.Keys.Add(value);
                if (newSmallNode != null)
                {
                    newRootNode.Children.Add(newSmallNode);
                    newSmallNode.Parent = newRootNode;
                }
                if (newBigNode != null)
                {
                    var bigValue = newBigNode.Keys.Max();
                    newRootNode.Keys.Add(bigValue);
                    newRootNode.Children.Add(newBigNode);
                    newBigNode.Parent = newRootNode;
                }
                Root = newRootNode;
                if (Head == null)
                {
                    Head = Root;
                }
            }
        }

        /// <summary>
        /// 结点插入值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void Insert(BPlusTreeNode<T> node, T value, BPlusTreeNode<T> smallNode, BPlusTreeNode<T> bigNode)
        {
            if (smallNode != null && bigNode != null)
            {
                var smallNodeKey = smallNode.Keys.Max();
                //不存在关键字，则插入关键字和子结点
                var smallIndex = node.Keys.ToList().FindLastIndex(p => p.CompareTo(smallNodeKey) == 0);
                if (smallIndex == -1)
                {
                    smallIndex = node.Keys.ToList().FindLastIndex(p => p.CompareTo(smallNodeKey) < 0) + 1;
                    node.Keys.Insert(smallIndex, smallNodeKey);
                    node.Children.Insert(smallIndex, smallNode);
                }
                //存在则不插入关键字，只替换子结点
                else
                {
                    node.Children[smallIndex] = smallNode;
                }

                var bigNodeKey = bigNode.Keys.Max();
                var bigIndex = node.Keys.ToList().FindLastIndex(p => p.CompareTo(bigNodeKey) == 0);
                //不存在关键字，则插入关键字和子结点
                if (bigIndex == -1)
                {
                    bigIndex = node.Keys.ToList().FindLastIndex(p => p.CompareTo(bigNodeKey) < 0) + 1;
                    node.Keys.Insert(bigIndex, bigNodeKey);
                    node.Children.Insert(bigIndex, bigNode);
                }
                //存在则不插入关键字，只替换子结点
                else
                {
                    node.Children[bigIndex] = bigNode;
                }

            }
            else
            {
                var index = node.Keys.ToList().FindLastIndex(p => p.CompareTo(value) < 0) + 1;
                node.Keys.Insert(index, value);
            }
        }

        /// <summary>
        /// 查找插入结点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private BPlusTreeNode<T> InsertFind(BPlusTreeNode<T> node, T value)
        {
            if (node == null)
            {
                return null;
            }

            //叶子结点表示查找完成
            if (node.Children.Count == 0)
            {
                return node;
            }

            //最大的关键字的左边查找
            var index = -1;
            for (int i = 0; i < node.Keys.Count; i++)
            {
                if (value.CompareTo(node.Keys[i]) < 0)
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                if (node.Children.Count > index)
                {
                    return InsertFind(node.Children[index], value);
                }
                else
                {
                    return null;
                }
            }

            //最后一个
            return InsertFind(node.Children[node.Keys.Count() - 1], value);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public void Delete(T value)
        {
            var virtualDeleteNode = Find(value);
            if (virtualDeleteNode == null)
            {
                throw new Exception("项不存在树中");
            }

            var virtualDeleteIndex = virtualDeleteNode.Keys.FindIndex(p => p.CompareTo(value) == 0);
            var actualDeleteNode = virtualDeleteNode;
            var actualDeleteIndex = virtualDeleteIndex;

            //进行实际删除
            ActualDelete(actualDeleteIndex, actualDeleteNode, null);
        }

        /// <summary>
        /// 实际删除
        /// </summary>
        /// <param name="deleteKeyIndex"></param>
        /// <param name="deleteNode"></param>
        /// <param name="deleteNodePosition"></param>
        private void ActualDelete(int deleteKeyIndex, BPlusTreeNode<T> deleteNode, string deleteNodePosition)
        {
            var deleteKeyValue = deleteNode.Keys[deleteKeyIndex];

            //根结点
            if (deleteNode.Parent == null)
            {
                //如果根结点只剩一个，则直接删除并用合并的节点作为新的根结点
                if (deleteNode.Keys.Count == 1)
                {
                    deleteNode.Keys.RemoveAt(deleteKeyIndex);
                    if (deleteNodePosition == "left")
                    {
                        deleteNode.Children.RemoveAt(deleteKeyIndex);
                    }
                    else if (deleteNodePosition == "right")
                    {
                        deleteNode.Children.RemoveAt(deleteKeyIndex + 1);
                    }
                    deleteNode.Children[0].Parent = null;
                    Root = deleteNode.Children[0];
                }
                //否则直接删除即可
                else
                {
                    deleteNode.Keys.RemoveAt(deleteKeyIndex);
                    if (deleteNodePosition == "left")
                    {
                        deleteNode.Children.RemoveAt(deleteKeyIndex);
                    }
                    else if (deleteNodePosition == "right")
                    {
                        deleteNode.Children.RemoveAt(deleteKeyIndex + 1);
                    }
                }
            }
            //如果删除后满足最小关键字数量（阶数/2，取上整），则直接删除
            else if (deleteNode.Keys.Count >= MinKeyCount + 1)
            {
                deleteNode.Keys.RemoveAt(deleteKeyIndex);
                if (deleteNodePosition == "left")
                {
                    deleteNode.Children.RemoveAt(deleteKeyIndex);
                }
                else if (deleteNodePosition == "right")
                {
                    deleteNode.Children.RemoveAt(deleteKeyIndex + 1);
                }
            }
            //如果删除后不满足最小关键字数量（阶数/2，取上整），则需向兄弟结点借，具体有以下4种情况
            else
            {
                //获取父节点关键字下标和自己所在父结点的下标
                var parentNode = deleteNode.Parent;
                var parentKeyIndex = -1;
                var parentChilidIndex = -1;
                for (int i = 0; i < parentNode.Children.Count; i++)
                {
                    if (parentNode.Children[i] == deleteNode)
                    {
                        parentKeyIndex = i;
                        parentChilidIndex = i;
                        break;
                    }
                }

                //1 左兄弟够借
                //deleteNodePosition==null表示向上递归合并时不做借兄弟操作
                if (deleteNodePosition == null && parentChilidIndex - 1 >= 0)
                {
                    var leftSiblingNode = parentNode.Children[parentChilidIndex - 1];
                    if (leftSiblingNode.Keys.Count >= MinKeyCount + 1)
                    {
                        //1 移除关键字
                        deleteNode.Keys.RemoveAt(deleteKeyIndex);

                        //2 左兄弟的最后一个关键字移动过来自己的第一个关键字
                        deleteNode.Keys.Insert(0, leftSiblingNode.Keys[leftSiblingNode.Keys.Count - 1]);
                        leftSiblingNode.Keys.RemoveAt(leftSiblingNode.Keys.Count - 1);

                        //3 左兄弟的最大关键字，替换掉左兄弟父结点的关键字
                        parentNode.Keys[parentKeyIndex - 1] = leftSiblingNode.Keys[leftSiblingNode.Keys.Count - 2];

                        //4 如果删除的是最大值，那么需要继续向上替换所有父结点的最大值或最小值关键字
                        if (deleteNode.Keys.Max().CompareTo(deleteKeyValue) < 0)
                        {
                            ReplaceParentKey(deleteNode, deleteKeyValue, deleteNode.Keys.Max());
                        }

                        return;
                    }
                }

                //2 右兄弟够借
                //deleteNodePosition==null表示向上递归合并时不做借兄弟操作
                if (deleteNodePosition == null && parentChilidIndex + 1 < parentNode.Children.Count)
                {
                    var rightSiblingNode = parentNode.Children[parentChilidIndex + 1];
                    if (rightSiblingNode.Keys.Count >= MinKeyCount + 1)
                    {
                        //1 移除关键字
                        deleteNode.Keys.RemoveAt(deleteKeyIndex);

                        //2 右兄弟的第一个关键字移动过来自己的最后一个关键字
                        deleteNode.Keys.Add(rightSiblingNode.Keys[0]);
                        rightSiblingNode.Keys.RemoveAt(0);

                        //3 父结点的关键字更新为新的最后一个关键字
                        parentNode.Keys[parentKeyIndex] = deleteNode.Keys[deleteNode.Keys.Count - 1];

                        return;
                    }
                }

                //3 左兄弟不够借，需要把自己所在的结点和左兄弟和父结点合并成一个结点
                if (parentChilidIndex - 1 >= 0)
                {
                    var leftSiblingNode = parentNode.Children[parentChilidIndex - 1];
                    if (leftSiblingNode.Keys.Count < MinKeyCount + 1)
                    {
                        //移除删除结点
                        deleteNode.Keys.RemoveAt(deleteKeyIndex);
                        if (deleteNode.Children.Count > 0)
                        {
                            deleteNode.Children.RemoveAt(deleteKeyIndex);
                        }

                        //将整个删除结点移动到左兄弟的尾
                        for (int i = 0; i < deleteNode.Keys.Count; i++)
                        {
                            leftSiblingNode.Keys.Add(deleteNode.Keys[i]);
                            if (deleteNode.Children?.Count > 0)
                            {
                                deleteNode.Children[i].Parent = leftSiblingNode;
                                leftSiblingNode.Children.Add(deleteNode.Children[i]);
                            }
                        }

                        //用左兄弟最大的关键字替换父结点关键字
                        parentNode.Keys[parentKeyIndex - 1] = leftSiblingNode.Keys.Max();

                        //因父结点的关键字减少，需对父结点进行相同调整
                        ActualDelete(parentKeyIndex, parentNode, "right");
                        return;
                    }
                }

                //4 右兄弟不够借，需要把自己所在的结点和右兄弟和父结点合并成一个结点
                if (parentChilidIndex + 1 < parentNode.Children.Count)
                {
                    var rightSiblingNode = parentNode.Children[parentChilidIndex + 1];
                    if (rightSiblingNode.Keys.Count < MinKeyCount + 1)
                    {
                        //移除删除结点
                        deleteNode.Keys.RemoveAt(deleteKeyIndex);
                        if (deleteNode.Children.Count > 0)
                        {
                            deleteNode.Children.RemoveAt(deleteKeyIndex);
                        }

                        //用父结点关键字补到删除结点的尾
                        deleteNode.Keys.Add(parentNode.Keys[parentKeyIndex]);

                        //将整个删除结点移动到右兄弟的头
                        for (int i = deleteNode.Keys.Count - 1; i > -1; i--)
                        {
                            rightSiblingNode.Keys.Insert(0, deleteNode.Keys[i]);
                            if (deleteNode.Children?.Count > 0)
                            {
                                deleteNode.Children[i].Parent = rightSiblingNode;
                                rightSiblingNode.Children.Insert(0, deleteNode.Children[i]);
                            }
                        }

                        //因父结点的关键字下移，需对父结点进行相同调整
                        ActualDelete(parentKeyIndex, parentNode, "left");
                    }
                }
            }
        }

        private void ReplaceParentKey(BPlusTreeNode<T> node, T oldValue, T newValue)
        {
            var parent = node.Parent;
            while (parent != null)
            {
                var index = parent.Keys.FindIndex(p => p.CompareTo(oldValue) == 0);
                if (index > -1)
                {
                    parent.Keys[index] = newValue;
                }
                parent = parent.Parent;
            }
        }
        #endregion
    }
}
