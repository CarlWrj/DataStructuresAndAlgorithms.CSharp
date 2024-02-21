using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Trees.BinarySearchTrees
{
    /// <summary>
    /// 二叉查找树或二叉排序树
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        #region 字段和构造函数
        /// <summary>
        /// 根节点
        /// </summary>
        public BinarySearchTreeNode<T> Root { get; set; }

        /// <summary>
        /// 节点数量
        /// </summary>
        public int Count { get; set; } = 0;
        #endregion

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BinarySearchTreeNode<T> Find(T item)
        {
            if (Count == 0)
                throw new Exception("树是空的");

            return FindNode(Root, item);
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="currentNode">开始查找的节点</param>
        /// <param name="item">查找值</param>
        /// <returns>成功则返回节点，失败则为null</returns>
        private BinarySearchTreeNode<T> FindNode(BinarySearchTreeNode<T> currentNode, T item)
        {
            if (currentNode == null)
                return currentNode;

            if (item.Equals(currentNode.Value))
            {
                return currentNode;
            }

            if (currentNode.LeftChild != null && item.CompareTo(currentNode.Value) < 0)
            {
                return FindNode(currentNode.LeftChild, item);
            }

            if (currentNode.RightChild != null && item.CompareTo(currentNode.Value) > 0)
            {
                return FindNode(currentNode.RightChild, item);
            }

            return null;
        }
        #endregion

        #region 插入
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Insert(T item)
        {
            var newNode = new BinarySearchTreeNode<T>() { Value = item };
            return InsertNode(newNode);
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="newNode">新节点</param>
        /// <returns></returns>
        private bool InsertNode(BinarySearchTreeNode<T> newNode)
        {
            if (Root == null)
            {
                Root = newNode;
                Count++;
                return true;
            }

            if (newNode.Parent == null)
                newNode.Parent = this.Root;

            //父节点大于新节点，则往左找
            if (newNode.Parent.Value.CompareTo(newNode.Value) > 0)
            {
                if (newNode.Parent.LeftChild == null)
                {
                    newNode.Parent.LeftChild = newNode;
                    Count++;
                    return true;
                }

                newNode.Parent = newNode.Parent.LeftChild;
                return InsertNode(newNode);
            }
            //父节点小于新节点，则往右找
            else
            {
                if (newNode.Parent.RightChild == null)
                {
                    newNode.Parent.RightChild = newNode;
                    Count++;
                    return true;
                }

                newNode.Parent = newNode.Parent.RightChild;
                return InsertNode(newNode);
            }
        }
        #endregion

        #region 移除
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Remove(T item)
        {
            if (Count == 0)
                throw new Exception("树是空的");

            var node = FindNode(Root, item);
            return RemoveNode(node);
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="node">Node.</param>
        /// <returns>>true表示成功，false表示没找到</returns>
        private bool RemoveNode(BinarySearchTreeNode<T> node)
        {
            if (node == null)
                return false;

            //如果左右子树都存在，
            //则用直接后继（右子树中最小的节点），或者直接前驱（左子树中最大的节点）替代
            //然后从树中删除这个直接后继或直接前缀
            if (node.LeftChild != null && node.RightChild != null)
            {
                //查找直接后继
                var successor = node.RightChild;
                var leftChild = successor.LeftChild;
                while (leftChild != null)
                {
                    successor = leftChild;
                    leftChild = leftChild.LeftChild;
                }
                node.Value = successor.Value;

                //移除直接后继
                return (true && RemoveNode(successor));
            }
            //如果左右子树只存在一个，则用左右子树代替自己，即可完成移除
            else if (node.LeftChild != null)
            {
                if (node.Parent?.LeftChild == node)
                {
                    node.Parent.LeftChild = node.LeftChild;
                }
                else
                {
                    node.Parent.RightChild = node.LeftChild;
                }

                node.LeftChild.Parent = node.Parent;
                Count--;
            }
            else if (node.RightChild != null)
            {
                if (node.Parent?.LeftChild == node)
                {
                    node.Parent.LeftChild = node.RightChild;
                }
                else
                {
                    node.Parent.RightChild = node.RightChild;
                }

                node.RightChild.Parent = node.Parent;
                Count--;
            }
            //如果没有子树,那么直接移除即可
            else
            {
                if (node.Parent != null)
                {
                    if (node.Parent.LeftChild == node)
                    {
                        node.Parent.LeftChild = null;
                    }
                    else
                    {
                        node.Parent.RightChild = null;
                    }
                }
                else
                {
                    Root = null;
                }
                Count--;
            }

            return true;
        }
        #endregion
    }
}
