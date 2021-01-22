using Microsoft.VisualStudio.TestTools.UnitTesting;
using INPTP_Clean02.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTP_Clean02.Graph.Tests
{
    [TestClass()]
    public class GraphTests
    {
        [TestMethod()]
        public void EmptyGraphShouldNotHaveAnyNodes()
        {
            Graph<int, int> graph = new Graph<int, int>();

            // do nothing

            Assert.IsFalse(graph.HasNode(10));
        }

        [TestMethod()]
        public void AddNodeTest()
        {
            Graph<int, int> graph = new Graph<int, int>();

            graph.addNode(10);

            Assert.IsTrue(graph.HasNode(10));
        }

        [TestMethod()]
        public void AddUniEdgeTest()
        {
            Graph<int, int> graph = new Graph<int, int>();

            graph.addNode(10);
            graph.addNode(20);
            graph.AddUni(100, 10, 20);

            Assert.IsTrue(graph.HasEdge(100));
        }

        [TestMethod()]
        public void AddBidirectedEdge()
        {
            Graph<int, int> graph = new Graph<int, int>();

            graph.addNode(10);
            graph.addNode(20);
            graph.AddBidirectedEdge(100, 10, 20);

            Assert.IsTrue(graph.HasEdge(100));
        }

        [TestMethod()]
        public void AddBidirectedEdge()
        {
            Graph<int, int> graph = new Graph<int, int>();

            graph.addNode(10);
            graph.addNode(20);
            graph.AddBidirectedEdge(100, 10, 20);

            Assert.IsTrue(graph.HasEdge(100));
        }

        [TestMethod()]
        public void removeEdge(E edge)
        {
           private Dictionary<E, Edge<N, E>> edges = new Dictionary<E, Edge<N, E>>();


    }


}