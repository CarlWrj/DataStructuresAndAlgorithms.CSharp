using DataStructures.Trees.HuffmanTrees;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;

namespace Algorithms.DataCompressions
{
    /// <summary>
    /// 哈夫曼编码
    /// </summary>
    public class HuffmanCode
    {
        /// <summary>
        /// 哈夫曼树
        /// </summary>
        public HuffmanTree<char> HuffmanTree { get; set; }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Compress(string data)
        {
            if (string.IsNullOrEmpty(data) || data.Distinct().Count() == 1)
            {
                throw new ArgumentException();
            }

            //获取字符出现频率
            var occurenceCounts = new Dictionary<char, int>();
            foreach (var ch in data)
            {
                if (!occurenceCounts.ContainsKey(ch))
                {
                    occurenceCounts.Add(ch, 0);
                }

                occurenceCounts[ch]++;
            }
            var huffmanTreeNodes = occurenceCounts.Select(kvp => new HuffmanTreeNode<char>(kvp.Key, kvp.Value)).ToArray();

            //创建哈夫曼树
            HuffmanTree = new HuffmanTree<char>(huffmanTreeNodes.ToList());

            //创建每个叶子结点的哈夫曼编码
            var path = new List<char>();
            var HuffmanCodeDic = new Dictionary<char, string>();
            Dfs(HuffmanTree.Root, path, HuffmanCodeDic);

            //将数据压缩成哈夫曼编码的数据
            var compressedData = string.Empty;
            foreach (var item in data)
            {
                compressedData += HuffmanCodeDic[item];
            }
            return compressedData;
        }

        /// <summary>
        /// 获取每个叶子结点的路径作为哈夫曼编码
        /// </summary>
        /// <param name="huffmanTreeNode"></param>
        /// <param name="path"></param>
        /// <param name="result"></param>
        private void Dfs(HuffmanTreeNode<char> huffmanTreeNode, List<char> path, Dictionary<char, string> result)
        {
            //叶子结点
            if (huffmanTreeNode.LeftChild == null && huffmanTreeNode.RightChild == null)
            {
                result.Add(huffmanTreeNode.Value, string.Join("", path));
                return;
            }

            //向左
            if (huffmanTreeNode.LeftChild != null)
            {
                path.Add('0');
                Dfs(huffmanTreeNode.LeftChild, path, result);
                path.RemoveAt(path.Count - 1);
            }

            //向右
            if (huffmanTreeNode.RightChild != null)
            {
                path.Add('1');
                Dfs(huffmanTreeNode.RightChild, path, result);
                path.RemoveAt(path.Count - 1);
            }
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="compressedData"></param>
        /// <returns></returns>
        public string Decompress(string compressedData)
        {
            var decompressedData = string.Empty;

            var huffmanTreeNode = HuffmanTree.Root;
            foreach (var item in compressedData)
            {
                if (item == '0')
                {
                    huffmanTreeNode = huffmanTreeNode.LeftChild;
                }
                else
                {
                    huffmanTreeNode = huffmanTreeNode.RightChild;
                }

                if (huffmanTreeNode.LeftChild == null && huffmanTreeNode.RightChild == null)
                {
                    decompressedData += huffmanTreeNode.Value;
                    huffmanTreeNode = HuffmanTree.Root;
                }
            }

            return decompressedData;
        }
    }
}
