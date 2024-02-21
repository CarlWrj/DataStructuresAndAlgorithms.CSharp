using DataStructures.Graphs;

namespace DataStructures.Test.Graphs
{
    public class AdjacencyListGraph
    {
        /// <summary>
        /// �����ڽӱ����
        /// </summary>
        [Fact]
        public void UnDirectedTest()
        {
            var graph = new AdjacencyListGraph<string>();

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

            //Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            //Assert.True(graph.EdgesCount == 9, "Wrong edges count.");
            //Assert.True(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            //������ȱ���
            Assert.True(graph.DepthFirstWalk("v1").SequenceEqual(new string[] { "v1", "v3", "v7", "v6", "v2", "v5", "v8", "v4" }));

            //������ȱ���
            Assert.True(graph.BreadthFirstWalk("v1").SequenceEqual(new string[] { "v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8" }));

            #region ��С����������
            //����ķ(Prim) �㷨



            #endregion

        }

        /// <summary>
        /// �����ڽӱ����
        /// </summary>
        [Fact]
        public void DirectedTest()
        {

        }

        /// <summary>
        /// �����������
        /// </summary>
        [Fact]
        public void TopologicalSortTest()
        {
            var graph = new AdjacencyListGraph<string>() { IsDirected = true };

            graph.AddVertices(new string[] { "v1", "v2", "v3", "v4", "v5", "v6" });

            graph.AddEdge("v1", "v2");
            graph.AddEdge("v1", "v3");
            graph.AddEdge("v1", "v4");
            graph.AddEdge("v3", "v2");
            graph.AddEdge("v3", "v5");
            graph.AddEdge("v4", "v5");
            graph.AddEdge("v6", "v4");
            graph.AddEdge("v6", "v5");

            graph.TopologicalSort();
        }

        /// <summary>
        /// �ؼ�·������
        /// </summary>
        [Fact]
        public void CriticalPathTest()
        {
            var graph = new AdjacencyListGraph<string>() { IsDirected = true };

            graph.AddVertices(new string[] { "v1", "v2", "v3", "v4", "v5", "v6" });

            graph.AddEdge("v1", "v2", 3);
            graph.AddEdge("v1", "v3", 2);
            graph.AddEdge("v2", "v4", 2);
            graph.AddEdge("v2", "v5", 3);
            graph.AddEdge("v3", "v4", 4);
            graph.AddEdge("v3", "v6", 3);
            graph.AddEdge("v4", "v6", 2);
            graph.AddEdge("v5", "v6", 1);

            graph.CriticalPath();
        }
    }
}
