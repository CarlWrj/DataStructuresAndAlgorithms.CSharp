using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataStructures.Graphs
{
    /// <summary>
    /// 邻接矩阵
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdjacencyMatrixGraph<T>
    {
        /// <summary>
        /// 空边
        /// </summary>
        public const long EMPTY_DEGE = 0;

        /// <summary>
        /// 是否有向
        /// </summary>
        public bool IsDirected { get; set; }

        /// <summary>
        /// 邻接矩阵
        /// </summary>
        public long[,] AdjacencyMatrix { get; set; }

        /// <summary>
        /// 邻接矩阵大小
        /// </summary>
        public uint Capacity { get; set; }

        /// <summary>
        /// 顶点数
        /// </summary>
        public int VerticesCount { get; set; }

        /// <summary>
        /// 边数量
        /// </summary>
        public int EdgesCount { get; set; }

        /// <summary>
        /// 顶点
        /// </summary>
        public List<T> Vertices { get; set; }

        /// <summary>
        /// 第一个顶点
        /// </summary>
        public T FirstVertice { get; set; }

        public AdjacencyMatrixGraph(uint capacity = 10, bool isDirected = false)
        {
            VerticesCount = 0;
            EdgesCount = 0;
            Vertices = new List<T>();
            IsDirected = isDirected;

            //初始化邻接矩阵
            Capacity = capacity;
            AdjacencyMatrix = new long[capacity, capacity];
            for (int i = 0; i < capacity; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    AdjacencyMatrix[i, j] = EMPTY_DEGE;
                }
            }
        }

        public void AddVertices(IList<T> vertices)
        {
            foreach (var vertice in vertices)
            {
                AddVertice(vertice);
            }
        }

        private void AddVertice(T vertice)
        {
            if (VerticesCount == 0)
            {
                FirstVertice = vertice;
            }
            Vertices.Add(vertice);
            VerticesCount++;
        }

        public void AddEdge(T v1, T v2, long weight = 1)
        {
            var v1Index = Vertices.IndexOf(v1);
            var v2Index = Vertices.IndexOf(v2);

            AdjacencyMatrix[v1Index, v2Index] = weight;
            if (!IsDirected)
            {
                AdjacencyMatrix[v2Index, v1Index] = weight;
            }
            EdgesCount++;
        }

        #region  遍历
        /// <summary>
        /// 深度优先遍历
        /// </summary>
        /// <returns></returns>
        public List<T> DepthFirstWalk(T vertice)
        {
            var statck = new Stack<T>();
            var visited = new HashSet<T>();
            var vertices = new List<T>();

            statck.Push(vertice);

            while (statck.Count > 0)
            {
                var currentVertice = statck.Pop();

                if (!visited.Contains(currentVertice))
                {
                    visited.Add(currentVertice);
                    vertices.Add(currentVertice);

                    // var currentAdjVertices = new List<T>();
                    var currentVerticeIndex = Vertices.IndexOf(currentVertice);
                    for (int i = 0; i < VerticesCount; i++)
                    {
                        if (Vertices[i] != null && AdjacencyMatrix[currentVerticeIndex, i] > 0)
                        {
                            if (!visited.Contains(Vertices[i]))
                            {
                                statck.Push(Vertices[i]);
                            }
                        }
                    }
                }
            }

            return vertices;
        }

        /// <summary>
        /// 广度优先遍历
        /// </summary>
        /// <param name="vertice"></param>
        /// <returns></returns>
        public List<T> BreadthFirstWalk(T vertice)
        {
            var visited = new HashSet<T>();
            var queue = new Queue<T>();
            var vertices = new List<T>();

            visited.Add(vertice);
            queue.Enqueue(vertice);
            vertices.Add(vertice);

            while (queue.Count > 0)
            {
                var currentVertice = queue.Dequeue();
                if (!visited.Contains(currentVertice))
                {
                    visited.Add(currentVertice);
                    vertices.Add(currentVertice);
                }

                var index = Vertices.IndexOf(currentVertice);
                for (int i = 0; i < VerticesCount; i++)
                {
                    if (Vertices[i] != null && AdjacencyMatrix[index, i] > 0)
                    {
                        if (!visited.Contains(Vertices[i]))
                        {
                            queue.Enqueue(Vertices[i]);
                        }
                    }
                }
            }

            return vertices;
        }
        #endregion

        #region 最小生成树
        /// <summary>
        /// 最小生成树-普里姆算法
        /// </summary>
        /// <param name="vertexValue"></param>
        public List<T> MinSpanTreeByPRIM(T source)
        {
            //用于
            List<(T vertice, long weight)> vertices = new List<(T vertice, long weight)>();
            var auxiliaryList = new List<long>();
            var sourceIndex = Vertices.IndexOf(source);

            for (int i = 0; i < VerticesCount; i++)
            {
                if (AdjacencyMatrix[sourceIndex, i] == 0)
                {
                    auxiliaryList.Add(long.MaxValue);
                }
                else
                {
                    auxiliaryList.Add(AdjacencyMatrix[sourceIndex, i]);
                }
            }

            for (int i = 1; i < VerticesCount; i++)
            {
                var minIndex = auxiliaryList.IndexOf(auxiliaryList.Min());
                if (minIndex != -1)
                {
                    auxiliaryList[minIndex] = 0;
                    //vertices.Add(Vertices[minIndex]);

                    for (int j = 0; j < VerticesCount; j++)
                    {
                        if (AdjacencyMatrix[minIndex, j] < auxiliaryList[j])
                        {
                            auxiliaryList[j] = AdjacencyMatrix[minIndex, j];
                        }
                    }
                }
            }

            //return vertices;
            return null;
        }
        #endregion

        #region 最短路径
        /// <summary>
        /// 最短路径-迪杰斯特拉（Dijkstra）算法
        /// </summary>
        public void ShortestPathByDijkstra(T vertice)
        {
            //1、初始化辅助结构
            //final数组：用于记录是否可以到某顶点
            var finals = new List<bool>();
            //dists数组：用于记录到某顶点所需的权值
            var dists = new List<long>();
            //paths数组：用于记录到某顶点的前驱顶点
            var paths = new List<int>();
            var sourceVerticeIndex = Vertices.IndexOf(vertice);
            for (int i = 0; i < VerticesCount; i++)
            {
                //源顶点处理
                if (sourceVerticeIndex == i)
                {
                    finals.Add(true);
                    dists.Add(0);
                    paths.Add(-1);
                }
                else
                {
                    finals.Add(false);
                    //如果源顶点能到某顶点则赋值权值和前驱，否则赋值最大值表示不能到达
                    var value = AdjacencyMatrix[sourceVerticeIndex, i];
                    if (value > 0)
                    {
                        dists.Add(value);
                        paths.Add(sourceVerticeIndex);
                    }
                    else
                    {
                        dists.Add(long.MaxValue);
                        paths.Add(-1);
                    }
                }
            }

            //2、计算最短路径
            for (int i = 1; i < VerticesCount; i++)
            {
                //获取下一个权值最小的顶点
                var min = long.MaxValue;
                var minIndex = -1;
                for (int j = 0; j < VerticesCount; j++)
                {
                    if (!finals[j] && dists[j] < min)
                    {
                        minIndex = j;
                        min = dists[j];
                    }
                }

                if (minIndex == -1)
                {
                    continue;
                }

                //用这个最小的顶点做中转，如果中转后的权值小于现有的权值，则更新权值和前驱顶点
                finals[minIndex] = true;
                for (int j = 0; j < VerticesCount; j++)
                {
                    if (!finals[j] && AdjacencyMatrix[minIndex, j] != 0 && dists[j] > AdjacencyMatrix[minIndex, j] + dists[minIndex])
                    {
                        dists[j] = AdjacencyMatrix[minIndex, j] + dists[minIndex];
                        paths[j] = minIndex;
                    }
                }
            }

            //3、输出最短路径
            for (int i = 0; i < VerticesCount; i++)
            {
                if (sourceVerticeIndex == i)
                {
                    continue;
                }

                var str = string.Empty;
                if (!finals[i])
                {
                    str = $"顶点{Vertices[sourceVerticeIndex]}到顶点{Vertices[i]}没有路径";
                    Console.WriteLine(str);
                }
                else
                {
                    var results = new List<T>() { Vertices[sourceVerticeIndex] };
                    var path = paths[i];
                    var stack = new Stack<T>();
                    while (path > 0)
                    {
                        stack.Push(Vertices[path]);
                        path = paths[path];
                    }
                    while (stack.Count > 0)
                    {
                        results.Add(stack.Pop());
                    }
                    results.Add(Vertices[i]);
                    str = $"顶点{Vertices[sourceVerticeIndex]}到顶点{Vertices[i]}的最短路径为：{string.Join(", ", results)}，长度{dists[i]}";
                    Console.WriteLine(str);
                }
            }
        }

        /// <summary>
        /// 最短路径-弗洛伊德（Floyd）算法
        /// </summary>
        public void ShortestPathByFloyd()
        {
            //记录某顶点到某顶点的最短路径
            var dists = new long[VerticesCount, VerticesCount];
            //记录某顶点到某顶点的前驱路径
            var paths = new int[VerticesCount, VerticesCount];

            //1、初始化
            for (int i = 0; i < VerticesCount; i++)
            {
                for (int j = 0; j < VerticesCount; j++)
                {
                    if (AdjacencyMatrix[i, j] == 0)
                    {
                        dists[i, j] = long.MaxValue;
                    }
                    else
                    {
                        dists[i, j] = AdjacencyMatrix[i, j];
                    }
                    paths[i, j] = -1;
                }
            }

            //2、计算最短路径
            for (int i = 0; i < VerticesCount; i++)
            {
                for (int j = 0; j < VerticesCount; j++)
                {
                    for (int k = 0; k < VerticesCount; k++)
                    {
                        if (i != j && dists[i, k] < long.MaxValue && dists[k, j] < long.MaxValue && dists[i, k] + dists[k, j] < dists[i, j])
                        {
                            dists[i, j] = dists[i, k] + dists[k, j];
                            paths[i, j] = k;
                        }
                    }
                }
            }

            //3、打印2个辅助矩阵
            var print = string.Empty;
            for (int i = 0; i < VerticesCount; i++)
            {
                for (int j = 0; j < VerticesCount; j++)
                {
                    print += dists[i, j] == long.MaxValue ? "∞ " : dists[i, j] + " ";
                }
                print += "\n";
            }
            for (int i = 0; i < VerticesCount; i++)
            {
                for (int j = 0; j < VerticesCount; j++)
                {
                    print += paths[i, j] + " ";
                }
                print += "\n";
            }

            //4、输出每个最短路径，未完成
            for (int i = 0; i < VerticesCount; i++)
            {
                var stack = new Stack<int>();
                var stack2 = new Stack<T>();
                for (int j = 0; j < VerticesCount; j++)
                {
                    if (i != j && dists[i, j] != long.MaxValue)
                    {
                        A(i, j, paths, stack, stack2);

                        var k = paths[i, j];
                        while (k != -1)
                        {
                            k = paths[k, j];
                        }
                        //stack.Push(Vertices[k]);
                    }
                }
            }
        }

        void A(int i, int j, int[,] paths, Stack<int> stack, Stack<T> statck2)
        {
            var k = paths[i, j];
            if (k == -1)
            {
                statck2.Push(Vertices[i]);
            }
            else
            {
                stack.Push(k);

                A(k, j, paths, stack, statck2);

                if (stack.Count > 0)
                {
                    k = stack.Pop();
                    A(i, k, paths, stack, statck2);
                }
            } 
        }
        #endregion
    }
}
