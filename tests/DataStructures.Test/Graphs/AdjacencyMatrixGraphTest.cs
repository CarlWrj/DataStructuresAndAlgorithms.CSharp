using DataStructures.Graphs;

namespace DataStructures.Test.Graphs
{
    public class AdjacencyMatrixGraphTest
    {
        /// <summary>
        /// 无向无权图测试
        /// </summary>
        [Fact]
        public void UndirectedTest()
        {
            var graph = new AdjacencyMatrixGraph<string>();

            graph.AddVertices(new string[] { "v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8" });

            graph.AddEdge("v1", "v2");
            graph.AddEdge("v1", "v3");
            graph.AddEdge("v2", "v4");
            graph.AddEdge("v2", "v5");
            graph.AddEdge("v3", "v6");
            graph.AddEdge("v3", "v7");
            graph.AddEdge("v4", "v8");
            graph.AddEdge("v5", "v8");
            graph.AddEdge("v6", "v7");

            //var allEdges = graph.Edges.ToList();

            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 9, "Wrong edges count.");
            //Assert.True(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.True(graph.DepthFirstWalk("v1").SequenceEqual(new string[] { "v1", "v3", "v7", "v6", "v2", "v5", "v8", "v4" }));
            Assert.True(graph.BreadthFirstWalk("v1").SequenceEqual(new string[] { "v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8" }));
        }

        /// <summary>
        /// 有向无权图测试
        /// </summary>
        [Fact]
        public void DirectedTest()
        {
            var graph = new AdjacencyMatrixGraph<string>();

            graph.AddVertices(new string[] { "v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8" });

            graph.AddEdge("v1", "v2");
            graph.AddEdge("v1", "v3");
            graph.AddEdge("v2", "v4");
            graph.AddEdge("v2", "v5");
            graph.AddEdge("v3", "v6");
            graph.AddEdge("v3", "v7");
            graph.AddEdge("v4", "v8");
            graph.AddEdge("v5", "v8");
            graph.AddEdge("v6", "v7");

            //var allEdges = graph.Edges.ToList();

            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 9, "Wrong edges count.");
            //Assert.True(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.True(graph.DepthFirstWalk("v1").SequenceEqual(new string[] { "v1", "v3", "v7", "v6", "v2", "v5", "v8", "v4" }));
            Assert.True(graph.BreadthFirstWalk("v1").SequenceEqual(new string[] { "v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8" }));
        }

        /// <summary>
        /// 无向有权图测试
        /// </summary>
        [Fact]
        public void UndirectedWeightedTest()
        {
            var graph = new AdjacencyMatrixGraph<string>();

            graph.AddVertices(new string[] { "v1", "v2", "v3", "v4", "v5", "v6" });

            graph.AddEdge("v1", "v2", 6);
            graph.AddEdge("v1", "v3", 1);
            graph.AddEdge("v1", "v4", 5);
            graph.AddEdge("v2", "v3", 5);
            graph.AddEdge("v2", "v5", 3);
            graph.AddEdge("v3", "v4", 5);
            graph.AddEdge("v3", "v5", 6);
            graph.AddEdge("v3", "v6", 4);
            graph.AddEdge("v4", "v6", 2);
            graph.AddEdge("v5", "v6", 6);

            //最小生成树-普里姆算法
            Assert.True(graph.MinSpanTreeByPRIM("v1").SequenceEqual(new string[] { "v1", "v3", "v6", "v4", "v2", "v5" }));
        }

        /// <summary>
        /// 有向有权图测试
        /// </summary>
        [Fact]
        public void DirectedWeightedTest()
        {
            var graph = new AdjacencyMatrixGraph<string>();

            graph.AddVertices(new string[] { "v1", "v2", "v3", "v4", "v5", "v6" });

            graph.AddEdge("v1", "v2");
            graph.AddEdge("v1", "v3");
            graph.AddEdge("v2", "v4");
            graph.AddEdge("v2", "v5");
            graph.AddEdge("v3", "v6");
            graph.AddEdge("v3", "v7");
            graph.AddEdge("v4", "v8");
            graph.AddEdge("v5", "v8");
            graph.AddEdge("v6", "v7");

            //var allEdges = graph.Edges.ToList();

            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 9, "Wrong edges count.");
            //Assert.True(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.True(graph.DepthFirstWalk("v1").SequenceEqual(new string[] { "v1", "v3", "v7", "v6", "v2", "v5", "v8", "v4" }));
            Assert.True(graph.BreadthFirstWalk("v1").SequenceEqual(new string[] { "v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8" }));
        }

        /// <summary>
        /// 最短路径-迪杰斯特拉（Dijkstra）算法
        /// </summary>
        [Fact]
        public void ShortestPathByDijkstraTest()
        {
            var graph = new AdjacencyMatrixGraph<string>() { IsDirected = true };

            graph.AddVertices(new string[] { "v0", "v1", "v2", "v3", "v4", "v5" });

            graph.AddEdge("v0", "v4", 30);
            graph.AddEdge("v0", "v5", 100);
            graph.AddEdge("v0", "v2", 10);
            graph.AddEdge("v1", "v2", 5);
            graph.AddEdge("v2", "v3", 50);
            graph.AddEdge("v3", "v5", 10);
            graph.AddEdge("v4", "v3", 20);
            graph.AddEdge("v4", "v5", 60);

            graph.ShortestPathByDijkstra("v0");
        }

        /// <summary>
        /// 最短路径-弗洛伊德（Floyd）算法
        /// </summary>
        [Fact]
        public void ShortestPathByFloydTest()
        {
            var graph = new AdjacencyMatrixGraph<string>() { IsDirected = true };

            graph.AddVertices(new string[] { "v0", "v1", "v2" });

            graph.AddEdge("v0", "v1", 4);
            graph.AddEdge("v0", "v2", 11);
            graph.AddEdge("v1", "v0", 6);
            graph.AddEdge("v1", "v2", 2);
            graph.AddEdge("v2", "v0", 3);

            graph.ShortestPathByFloyd();
        }
    }
}
