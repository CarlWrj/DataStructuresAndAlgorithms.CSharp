using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Trees.BTrees
{
    public class BTree<T> where T : IComparable<T>
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
        public BTreeNode<T> Root { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="degree"></param>
        /// <exception cref="ArgumentException"></exception>
        public BTree(int degree)
        {
            if (degree < 4)
            {
                throw new ArgumentException("阶数必须大于3");
            }

            Degree = degree;
            MaxKeyCount = degree - 1;
            var d = (double)degree / 2;
            MinKeyCount = (int)(Math.Ceiling(d)) - 1;
            MedianIndex = degree / 2;
        }
        #endregion

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private BTreeNode<T> Find(T value)
        {
            return Find(Root, value);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private BTreeNode<T> Find(BTreeNode<T> node, T value)
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

            //找到则返回
            if (node.Keys.Any(p => p.CompareTo(value) == 0))
            {
                return node;
            }

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
            BTreeNode<T> newLeftNode = null;
            BTreeNode<T> newRightNode = null;
            var finished = false;

            //查找值插入到哪个节点中
            var insertNode = InsertFind(Root, value);
            while (insertNode != null && !finished)
            {
                //插入值到节点中
                Insert(insertNode, value, newRightNode);

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

                    //1 中位数右边一位到最后一位，分裂为一个新的右结点
                    newRightNode = new BTreeNode<T>();
                    var i = 0;
                    for (i = MedianIndex + 1; i <= MaxKeyCount; i++)
                    {
                        newRightNode.Keys.Add(insertNode.Keys[i]);
                        if (insertNode.Children.Count > i)
                        {
                            newRightNode.Children.Add(insertNode.Children[i]);
                            insertNode.Children[i].Parent = newRightNode;
                        }
                    }
                    if (insertNode.Children.Count > i)
                    {
                        newRightNode.Children.Add(insertNode.Children[i]);
                        insertNode.Children[i].Parent = newRightNode;
                    }
                    newRightNode.Parent = insertNode.Parent;

                    //2 原结点作为左结点，移除掉分裂出去的所有值
                    for (i = MedianIndex; i <= MaxKeyCount; i++)
                    {
                        insertNode.Keys.RemoveAt(MedianIndex);
                        if (insertNode.Children.Count > MedianIndex + 1)
                        {
                            insertNode.Children.RemoveAt(MedianIndex + 1);
                        }
                    }
                    newLeftNode = insertNode;

                    //3 将中位数插入到父结点中
                    insertNode = insertNode.Parent;
                }
            }

            //空树或者根结点已被分裂,需添加根结点
            if (!finished)
            {
                var newRootNode = new BTreeNode<T>();
                newRootNode.Keys.Add(value);
                if (newLeftNode != null)
                {
                    newRootNode.Children.Add(newLeftNode);
                    newLeftNode.Parent = newRootNode;
                }
                if (newRightNode != null)
                {
                    newRootNode.Children.Add(newRightNode);
                    newRightNode.Parent = newRootNode;
                }
                Root = newRootNode;
            }
        }

        /// <summary>
        /// 结点插入值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void Insert(BTreeNode<T> node, T value, BTreeNode<T> rightNode)
        {
            var index = node.Keys.ToList().FindLastIndex(p => p.CompareTo(value) < 0) + 1;
            node.Keys.Insert(index, value);

            if (rightNode != null)
            {
                node.Children.Insert(index + 1, rightNode);
            }
        }

        /// <summary>
        /// 查找插入结点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private BTreeNode<T> InsertFind(BTreeNode<T> node, T value)
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
            return InsertFind(node.Children[node.Keys.Count()], value);
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

            //如果有直接后继结点，则获取直接后继结点的关键字替换掉待删除的结点关键字，然后删除直接后继结点
            if (virtualDeleteNode.Children?.Count > actualDeleteIndex + 1)
            {
                var successorNode = virtualDeleteNode.Children[actualDeleteIndex + 1];
                while (successorNode != null && successorNode.Children != null && successorNode.Children.Count > 0)
                {
                    successorNode = successorNode.Children[0];
                }
                virtualDeleteNode.Keys[actualDeleteIndex] = successorNode.Keys[0];
                actualDeleteNode = successorNode;
                actualDeleteIndex = 0;
            }

            #region 或者使用直接前驱结点作为替换
            //if (virtualDeleteNode.Children?.Count > actualDeleteIndex + 1)
            //{
            //    var precursorNode = virtualDeleteNode.Children[actualDeleteIndex];
            //    while (precursorNode != null && precursorNode.Children != null && precursorNode.Children.Count > 0)
            //    {
            //        precursorNode = precursorNode.Children[precursorNode.Children.Count - 1];
            //    }
            //    virtualDeleteNode.Keys[actualDeleteIndex] = precursorNode.Keys[precursorNode.Keys.Count - 1];
            //    actualDeleteIndex = precursorNode.Keys.Count - 1;
            //}
            #endregion

            //进行实际删除
            ActualDelete(actualDeleteIndex, actualDeleteNode, null);
        }

        /// <summary>
        /// 实际删除
        /// </summary>
        /// <param name="deleteKeyIndex"></param>
        /// <param name="deleteNode"></param>
        /// <param name="deleteNodePosition"></param>
        private void ActualDelete(int deleteKeyIndex, BTreeNode<T> deleteNode, string deleteNodePosition)
        {
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
                        if (i != 0 && parentNode.Children.Count - 1 == i)
                        {
                            parentKeyIndex = i - 1;
                        }
                        else
                        {
                            parentKeyIndex = i;
                        }
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
                        //1 移除删除关键字
                        deleteNode.Keys.RemoveAt(deleteKeyIndex);

                        //2 然后将父结点的关键字插入到当前结点的第一位
                        deleteNode.Keys.Insert(0, parentNode.Keys[parentKeyIndex - 1]);

                        //3 最后将左兄弟的最后一个关键字赋值给父结点
                        parentNode.Keys[parentKeyIndex - 1] = leftSiblingNode.Keys[leftSiblingNode.Keys.Count - 1];
                        leftSiblingNode.Keys.RemoveAt(leftSiblingNode.Keys.Count - 1);

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
                        //1 移除删除关键字
                        deleteNode.Keys.RemoveAt(deleteKeyIndex);

                        //2 将父结点的关键字插入到当前结点的最后一位
                        deleteNode.Keys.Add(parentNode.Keys[parentKeyIndex]);

                        //3 将右兄弟的第一个关键字赋值给父结点
                        parentNode.Keys[parentKeyIndex] = rightSiblingNode.Keys[0];
                        rightSiblingNode.Keys.RemoveAt(0);
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
                            deleteNode.Children.RemoveAt(deleteKeyIndex + 1);
                        }

                        //用父结点关键字补到删除结点的头
                        deleteNode.Keys.Insert(0, parentNode.Keys[parentKeyIndex]);

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

                        //因父结点的关键字下移，需对父结点进行相同调整
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
        #endregion
    }
}
