using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Graphs
{
    /// <summary>
    /// 邻接表
    /// </summary>
    public class AdjacencyListGraph<T>
    {
        #region 属性
        /// <summary>
        /// 边数量
        /// </summary>
        public uint EdgeCount { get; set; }

        /// <summary>
        /// 第一个顶点
        /// </summary>
        public T FirstVertice { get; set; }

        /// <summary>
        /// 是否有向
        /// </summary>
        public bool IsDirected { get; set; }

        /// <summary>
        /// 是否有权
        /// </summary>
        public bool IsWeighted { get; set; }

        /// <summary>
        /// 邻接表
        /// </summary>
        public Dictionary<T, List<WeightedEdge<T>>> AdjacencyList { get; set; }

        /// <summary>
        /// 顶点数量
        /// </summary>
        public int VerticesCount
        {
            get { return AdjacencyList.Count; }
        }
        #endregion

        #region CRUD
        /// <summary>
        /// 构造函数
        /// </summary>
        public AdjacencyListGraph()
        {
            AdjacencyList = new Dictionary<T, List<WeightedEdge<T>>>();
        }

        /// <summary>
        /// 添加多个顶点
        /// </summary>
        /// <param name="vertices"></param>
        public void AddVertices(IList<T> vertices)
        {
            foreach (var item in vertices)
            {
                AddVertice(item);
            }
        }

        /// <summary>
        /// 添加顶点
        /// </summary>
        /// <param name="item"></param>
        public void AddVertice(T item)
        {
            AdjacencyList.Add(item, new List<WeightedEdge<T>>());
        }

        /// <summary>
        /// 添加边
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="weight"></param>
        public void AddEdge(T v1, T v2, long weight = 1)
        {
            AdjacencyList[v1].Add(new WeightedEdge<T>(v1, v2, weight));

            if (!IsDirected)
            {
                AdjacencyList[v2].Add(new WeightedEdge<T>(v2, v1, weight));
            }
            EdgeCount++;
        }

        /// <summary>
        /// 深度优先遍历
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<T> DepthFirstWalk(T source)
        {
            var visited = new HashSet<T>();
            var stack = new Stack<T>();
            var vertices = new List<T>();

            stack.Push(source);

            while (stack.Count > 0)
            {
                var currentVertice = stack.Pop();
                if (!visited.Contains(currentVertice))
                {
                    visited.Add(currentVertice);
                    vertices.Add(currentVertice);
                }

                var linkVertices = AdjacencyList[currentVertice];
                foreach (var linkVertice in linkVertices)
                {
                    if (!visited.Contains(linkVertice.Destination))
                    {
                        stack.Push(linkVertice.Destination);
                    }
                }
            }

            return vertices;
        }

        /// <summary>
        /// 广度优先遍历
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<T> BreadthFirstWalk(T source)
        {
            var visited = new HashSet<T>();
            var queuq = new Queue<T>();
            var vertices = new List<T>();

            queuq.Enqueue(source);

            while (queuq.Count > 0)
            {
                var currentVertice = queuq.Dequeue();
                if (!visited.Contains(currentVertice))
                {
                    visited.Add(currentVertice);
                    vertices.Add(currentVertice);
                }

                var linkVertices = AdjacencyList[currentVertice];
                foreach (var linkVertice in linkVertices)
                {
                    if (!visited.Contains(linkVertice.Destination))
                    {
                        queuq.Enqueue(linkVertice.Destination);
                    }
                }
            }

            return vertices;
        }
        #endregion

        #region 拓扑排序
        /// <summary>
        /// 拓扑排序
        /// </summary>
        public List<T> TopologicalSort()
        {
            //初始化
            var ingrees = new Dictionary<T, int>();
            foreach (var item in AdjacencyList)
            {
                if (!ingrees.ContainsKey(item.Key))
                {
                    ingrees.Add(item.Key, 0);
                }
            }

            //获取所有顶点的入度
            foreach (var item in AdjacencyList)
            {
                if (item.Value != null)
                {
                    foreach (var value in item.Value)
                    {
                        ingrees[value.Destination]++;
                    }
                }
            }

            //拓扑排序
            var topos = new List<T>();
            for (var i = 0; i < VerticesCount; i++)
            {
                var zeroIngreeVertice = ingrees.FirstOrDefault(p => p.Value == 0 && !topos.Contains(p.Key)).Key;
                if (zeroIngreeVertice != null)
                {
                    topos.Add(zeroIngreeVertice);
                    foreach (var item in AdjacencyList[zeroIngreeVertice])
                    {
                        ingrees[item.Destination]--;
                    }
                }
            }

            //输出结果
            var output = string.Empty;
            if (ingrees.All(p => p.Value == 0))
            {
                foreach (var item in topos)
                {
                    output += item + "，";
                }
            }
            else
            {
                output = "";
            }

            return topos;
        }
        #endregion

        #region 关键路径
        /// <summary>
        /// 关键路径
        /// </summary>
        public void CriticalPath()
        {
            var topos = TopologicalSort();

            //1、获取所有顶点的最早发生时间
            var ve = new Dictionary<T, long>();
            foreach (var item in topos)
            {
                ve.Add(item, 0);
                foreach (var Adjacency in AdjacencyList)
                {
                    var ingreeList = Adjacency.Value.Where(p => p.Destination.Equals(item));
                    foreach (var ingree in ingreeList)
                    {
                        if (ve[ingree.Source] + ingree.Weight > ve[item])
                        {
                            ve[item] = ve[ingree.Source] + ingree.Weight;
                        }
                    }
                }
            }

            //2、获取所有顶点的最晚发生时间
            var vl = new Dictionary<T, long>();
            topos.Reverse();
            foreach (var item in topos)
            {
                vl.Add(item, ve.Last().Value);
                foreach (var Adjacency in AdjacencyList)
                {
                    var outDegreeList = Adjacency.Value.Where(p => p.Source.Equals(item));
                    foreach (var outDegree in outDegreeList)
                    {
                        if (vl[item] > vl[outDegree.Destination] - outDegree.Weight)
                        {
                            vl[item] = vl[outDegree.Destination] - outDegree.Weight;
                        }
                    }
                }
            }
            vl.Reverse();

            //3、获取所有边的最早发生时间
            var e = new Dictionary<WeightedEdge<T>, long>();
            //4、获取所有边的最晚发生时间
            var l = new Dictionary<WeightedEdge<T>, long>();
            foreach (var item in AdjacencyList)
            {
                foreach (var edge in item.Value)
                {
                    if (!e.ContainsKey(edge))
                    {
                        e.Add(edge, ve[edge.Source]);
                    }
                    if (!l.ContainsKey(edge))
                    {
                        l.Add(edge, vl[edge.Destination] - edge.Weight);
                    }
                }
            }

            //5、获取关键路径
            var d = new Dictionary<WeightedEdge<T>, long>();
            foreach (var item in e)
            {
                d.Add(item.Key, l[item.Key] - e[item.Key]);
            }

            //6、输出关键路径
            var output = string.Empty;
            foreach (var item in d)
            {
                if (item.Value == 0)
                {
                    output += $"{item.Key.Source}->{item.Key.Destination}\n";
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// 有权图的边
    /// </summary>
    public class WeightedEdge<TVertex>
    {
        /// <summary>
        /// 源顶点
        /// </summary>
        /// <value>The source.</value>
        public TVertex Source { get; set; }

        /// <summary>
        /// 目标顶点
        /// </summary>
        /// <value>The destination.</value>
        public TVertex Destination { get; set; }

        /// <summary>
        /// 权值
        /// </summary>
        /// <value>The weight.</value>
        public Int64 Weight { get; set; }

        /// <summary>
        /// 是否有权值
        /// </summary>
        public bool IsWeighted
        {
            get { return true; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WeightedEdge(TVertex src, TVertex dst, Int64 weight)
        {
            Source = src;
            Destination = dst;
            Weight = weight;
        }
    }
}
