using DataStructures.Trees.AVLTrees;
using DataStructures.Trees.BinarySearchTrees;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DataStructures.Trees.RedBlackTrees
{
    /// <summary>
    /// 红黑树
    /// </summary>
    public class RedBlackTree<T> where T : IComparable<T>
    {
        #region 字段和构造函数
        /// <summary>
        /// 根节点
        /// </summary>
        public RedBlackTreeNode<T> Root { get; set; }

        /// <summary>
        /// 节点数量
        /// </summary>
        public int Count { get; set; } = 0;
        #endregion

        #region 插入
        public bool Insert(T value)
        {
            var newNode = new RedBlackTreeNode<T>() { Color = RedBlackTreeNodeColor.Red, Value = value };
            if (Root == null)
            {
                newNode.Color = RedBlackTreeNodeColor.Black;
            }

            InsertNode(newNode);

            if (newNode != Root && newNode.Parent.Color == RedBlackTreeNodeColor.Red)
            {
                AdjustTreeAfterInsertion(newNode);
            }

            return true;
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="newNode">新节点</param>
        /// <returns></returns>
        protected virtual bool InsertNode(RedBlackTreeNode<T> newNode)
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

        /// <summary>
        /// 插入之后进行调整树
        /// </summary>
        /// <param name="newNode"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void AdjustTreeAfterInsertion(RedBlackTreeNode<T> newNode)
        {
            //根结点
            if (Root == newNode)
            {
                newNode.Color = RedBlackTreeNodeColor.Black;
                return;
            }

            //如果父结点是根结点，不需处理
            if (newNode.Parent == Root)
            {
                return;
            }

            //黑叔
            if (GetSibling(newNode.Parent).Color == RedBlackTreeNodeColor.Black)
            {
                //LL:父爷换色+右旋
                if (newNode.Parent == newNode.Parent.Parent.LeftChild && newNode == newNode.Parent.LeftChild)
                {
                    newNode.Parent.Color = RedBlackTreeNodeColor.Black;
                    newNode.Parent.Parent.Color = RedBlackTreeNodeColor.Red;

                    RotateRightAt(newNode.Parent.Parent);
                }
                //RR:父爷换色+左旋
                else if (newNode.Parent == newNode.Parent.Parent.RightChild && newNode == newNode.Parent.RightChild)
                {
                    newNode.Parent.Color = RedBlackTreeNodeColor.Black;
                    newNode.Parent.Parent.Color = RedBlackTreeNodeColor.Red;

                    RotateLeftAt(newNode.Parent.Parent);
                }
                //LR:自己和爷换色+左旋+右旋
                else if (newNode.Parent == newNode.Parent.Parent.LeftChild && newNode == newNode.Parent.RightChild)
                {
                    newNode.Color = RedBlackTreeNodeColor.Black;
                    newNode.Parent.Parent.Color = RedBlackTreeNodeColor.Red;

                    RotateLeftAt(newNode.Parent);
                    RotateRightAt(newNode.Parent);
                }
                //RL:自己和爷换色+右旋+左旋
                else if (newNode.Parent == newNode.Parent.Parent.RightChild && newNode == newNode.Parent.LeftChild)
                {
                    newNode.Color = RedBlackTreeNodeColor.Black;
                    newNode.Parent.Parent.Color = RedBlackTreeNodeColor.Red;

                    RotateRightAt(newNode.Parent);
                    RotateLeftAt(newNode.Parent);
                }
            }
            //红叔
            else
            {
                //叔父爷换色
                GetSibling(newNode.Parent).Color = RedBlackTreeNodeColor.Black;
                newNode.Parent.Color = RedBlackTreeNodeColor.Black;
                newNode.Parent.Parent.Color = RedBlackTreeNodeColor.Red;

                //爷是新结点进行递归
                AdjustTreeAfterInsertion(newNode.Parent.Parent);
            }
        }

        /// <summary>
        /// 获取兄弟结点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private RedBlackTreeNode<T> GetSibling(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> siblingNode = null;
            if (node.Parent.LeftChild == node)
            {
                siblingNode = node.Parent.RightChild;
            }
            else
            {
                siblingNode = node.Parent.LeftChild;
            }

            //空代表是叶子节点，则是黑
            if (siblingNode == null)
            {
                siblingNode = new RedBlackTreeNode<T>() { Color = RedBlackTreeNodeColor.Black };
            }
            return siblingNode;
        }

        /// <summary>
        /// 在树中向右旋转节点
        /// </summary>
        private void RotateRightAt(RedBlackTreeNode<T> currentNode)
        {
            // 我们检查左侧子节点，因为它将成为旋转的轴心节点
            if (currentNode == null || currentNode.LeftChild == null)
                return;

            // 左子树作为支点
            var pivotNode = currentNode.LeftChild;
            // 父节点
            var parent = currentNode.Parent;
            // 检查当前节点是否是其父节点的左子节点
            bool isLeftChild = currentNode.Parent != null && currentNode.Parent.LeftChild == currentNode;
            // 检查当前节点是否根节点
            bool isRootNode = (currentNode == this.Root);

            // 执行旋转
            currentNode.LeftChild = pivotNode.RightChild;
            pivotNode.RightChild = currentNode;
            // 更新父节点引用
            currentNode.Parent = pivotNode;
            pivotNode.Parent = parent;
            if (currentNode.LeftChild != null)
                currentNode.LeftChild.Parent = currentNode;

            // 如有必要，更新整个树根
            if (isRootNode)
                this.Root = pivotNode;
            // 更新原始父节点的子节点
            if (isLeftChild)
                parent.LeftChild = pivotNode;
            else if (parent != null)
                parent.RightChild = pivotNode;
        }

        /// <summary>
        /// 在树中向左旋转节点
        /// </summary>
        private void RotateLeftAt(RedBlackTreeNode<T> currentNode)
        {
            // 我们检查右侧子节点，因为它将成为旋转的轴心节点
            if (currentNode == null || currentNode.RightChild == null)
                return;

            // 轴位于右子树上
            var pivotNode = currentNode.RightChild;
            // 父节点
            var parent = currentNode.Parent;
            // 检查当前节点是否是其父节点的左子节点
            bool isLeftChild = currentNode.Parent != null && currentNode.Parent.LeftChild == currentNode;
            // 检查currentNode是否为根
            bool isRootNode = (currentNode == this.Root);

            // 执行旋转
            currentNode.RightChild = pivotNode.LeftChild;
            pivotNode.LeftChild = currentNode;
            // 更新父引用
            currentNode.Parent = pivotNode;
            pivotNode.Parent = parent;
            if (currentNode.RightChild != null)
                currentNode.RightChild.Parent = currentNode;

            // 如有必要，更新整个树根
            if (isRootNode)
                this.Root = pivotNode;
            // 更新原始父节点的子节点
            if (isLeftChild)
                parent.LeftChild = pivotNode;
            else if (parent != null)
                parent.RightChild = pivotNode;
        }
        #endregion

        #region 删除
        #endregion

    }
}
