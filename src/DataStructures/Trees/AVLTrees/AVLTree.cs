using DataStructures.Trees.BinarySearchTrees;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Trees.AVLTrees
{
    /// <summary>
    /// 平衡二叉树
    /// AVL 是大学教授 G.M. Adelson-Velsky 和 E.M. Landis 名称的缩写
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AVLTree<T> where T : IComparable<T>
    {
        #region 字段和构造函数
        /// <summary>
        /// 根节点
        /// </summary>
        public AVLTreeNode<T> Root { get; set; }

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
        public AVLTreeNode<T> Find(T item)
        {
            if (Count == 0)
                throw new Exception("树是空的");

            return FindNode(Root, item);
        }

        /// <summary>
        /// 查找节点（实现和二叉排序树一样）
        /// </summary>
        /// <param name="currentNode">开始查找的节点</param>
        /// <param name="item">查找值</param>
        /// <returns>成功则返回节点，失败则为null</returns>
        private AVLTreeNode<T> FindNode(AVLTreeNode<T> currentNode, T item)
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
        /// 插入一项到树
        /// </summary>
        public void Insert(T item)
        {
            // 实例化新节点
            var newNode = new AVLTreeNode<T>() { Value = item };

            // 调用基类BST的插入方法
            var success = InsertNode(newNode);

            // 重新平衡树
            RebalanceTreeAt(newNode);
        }

        private bool InsertNode(AVLTreeNode<T> newNode)
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
        public bool Remove(T item)
        {
            if (Count == 0)
                throw new Exception("树是空的");

            // BST查找节点
            var node = FindNode(Root, item);

            // BST移除节点
            bool status = RemoveNode(node);

            if (status == true)
            {
                //移除后重新平衡
                RebalanceTreeAt(node);
            }
            return status;
        }
        
        /// <summary>
        /// 移除结点（实现和二叉排序树一样）
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool RemoveNode(AVLTreeNode<T> node)
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

        #region 平衡
        /// <summary>
        /// 围绕节点重新平衡整棵树
        /// </summary>
        private void RebalanceTreeAt(AVLTreeNode<T> node)
        {
            var currentNode = node;
            while (currentNode != null)
            {
                //更新当前节点的高
                currentNode.Height = 1 + Math.Max(GetNodeHeight(currentNode.LeftChild), GetNodeHeight(currentNode.RightChild));

                // 获取左右子树用于比较
                var left = currentNode.LeftChild;
                var right = currentNode.RightChild;

                //左子树比右子树高1以上
                if (GetNodeHeight(left) >= 2 + GetNodeHeight(right))
                {
                    //如这样，则可直接右旋
                    /*************************************
                     **     不平衡     ===>    以3为轴进行右旋
                     **        5                      3       
                     **       / \                    / \      
                     **      3   7     ===>         2   5     
                     **     / \                    /   / \     
                     **    2   4                  1   4   7    
                     **   /                    
                     **  1                      
                     **
                     *************************************
                     */
                    if (currentNode.LeftChild != null && GetNodeHeight(left.LeftChild) >= GetNodeHeight(left.RightChild))
                    {
                        RotateRightAt(currentNode);
                    }
                    //如这样，则需要左旋，然后右旋
                    /*************************************
                     **    不平衡      ===>    以4为轴进行左旋    ===>    以4为轴进行右旋 
                     **        5                      5                          4
                     **       / \                    / \                        / \
                     **      2   7     ===>         4   7        ===>          2   5
                     **     / \                    /                          / \   \
                     **    1   4                  2                          1   3   7
                     **       /                  / \
                     **      3                  1   3
                     **
                     *************************************
                     */
                    else
                    {
                        RotateLeftAt(currentNode.LeftChild as AVLTreeNode<T>);
                        RotateRightAt(currentNode);
                    }
                }
                //右子树比左子树高1以上
                else if (GetNodeHeight(right) >= 2 + GetNodeHeight(left))
                {
                    //如这样，则可直接左旋
                    /*************************************
                     **     不平衡     ===>    以7为轴进行左旋 
                     **        4                      7        
                     **       / \                    / \       
                     **      2   7     ===>         4   8      
                     **         / \                / \   \     
                     **        6   8              2   6   9    
                     **             \                      
                     **              9                      
                     **
                     *************************************
                     */
                    if (currentNode.RightChild != null && GetNodeHeight(right.RightChild) >= GetNodeHeight(right.LeftChild))
                    {
                        RotateLeftAt(currentNode);
                    }
                    //如这样，则需要右旋，然后左旋
                    /*************************************
                     **     不平衡     ===>    以7为轴进行右旋    ===>    以7为轴进行左旋 
                     **        4                      4                          7
                     **       / \                    / \                        / \
                     **      2   9     ===>         2   7        ===>          4   9
                     **         / \                    / \                    / \   \  
                     **        7   8                  6   9                  2   6   8 
                     **       /                            \
                     **      6                              8
                     **
                     *************************************
                     */
                    else
                    {
                        RotateRightAt(currentNode.RightChild as AVLTreeNode<T>);
                        RotateLeftAt(currentNode);
                    }
                }

                currentNode = currentNode.Parent as AVLTreeNode<T>;
            }
        }

        /// <summary>
        /// 在AVL树中向右旋转节点
        /// </summary>
        private void RotateRightAt(AVLTreeNode<T> currentNode)
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

            // 更新每个节点的AVL高度
            UpdateHeightRecursive(currentNode);
        }

        /// <summary>
        /// 在AVL树中向左旋转节点
        /// </summary>
        private void RotateLeftAt(AVLTreeNode<T> currentNode)
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

            // 更新每个节点的AVL高度
            UpdateHeightRecursive(currentNode);
        }

        /// <summary>
        /// 递归更新节点高度和它的父节点，直到根节点
        /// </summary>
        private void UpdateHeightRecursive(AVLTreeNode<T> node)
        {
            if (node == null)
                return;

            // height = 1 + the max between left and right children.
            node.Height = 1 + Math.Max(GetNodeHeight(node.LeftChild), GetNodeHeight(node.RightChild));

            UpdateHeightRecursive(node.Parent as AVLTreeNode<T>);
        }

        /// <summary>
        /// 获取节点高度
        /// </summary>
        private int GetNodeHeight(AVLTreeNode<T> node)
        {
            if (node == null)
                return -1;
            return node.Height;
        }
        #endregion
    }
}
