using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTP_Clean02.Graph
{
    /*
     * Graph represents mathematical structure Directed Graph. Graph holds user
     * defined data objects (N, E) at each node and edge. These objects also works 
     * as a key, so they must be unique.
     * 
     * TODO: create a GIT repository, for each change create a GIT commit, 
     * cleanup code, add unit tests for all graph operations, including invalid 
     * states. In error cases Graph and other classes should throw relevant exception
     * objects. Test Dijkstra's algorithm. These classes are part of public API - add
     * basic documentation.
     * 
     * For GIT - you can use portable version from: https://git-scm.com/download/win
     */
    public class Graph<N, E> where N : IComparable<N> where E : IComparable<E>
    {
        private Dictionary<N, Node<N, E>> vertexes = new Dictionary<N, Node<N, E>>();
        private Dictionary<E, Edge<N, E>> edges = new Dictionary<E, Edge<N, E>>();

        public void addNode(N node)
        {
            Node<N, E> tmpNode = new Node<N, E>()
            {
                data = node
            };

            vertexes.Add(node, tmpNode);
        }

        public void AddUnidirectedEdge(E edge, N src, N dst)
        {
            Edge<N, E> tmpEdge = new Edge<N, E>()
            {
                data = edge
            };

            Node<N, E> sourceNode = getNode(src);
            Node<N, E> destinitationNode = getNode(dst);

            tmpEdge.firstAdjaencyNode = sourceNode;
            tmpEdge.secondAdjaencyNode = destinitationNode;

            sourceNode.adjaencyList.Add(tmpEdge);

            edges.Add(edge, tmpEdge);
        }

        public void AddBidirectedEdge(E edge, N src, N dst)
        {
            Edge<N, E> tmpEdge = new Edge<N, E>()
            {
                data = edge
            };

            Node<N, E> sourceNode = getNode(src);
            Node<N, E> destinationNode = getNode(dst);
           

            tmpEdge.firstAdjaencyNode = sourceNode;
            tmpEdge.secondAdjaencyNode = destinationNode;

            sourceNode.adjaencyList.Add(tmpEdge);
            destinationNode.adjaencyList.Add(tmpEdge);

            edges.Add(edge, tmpEdge);
        }

        public void removeEdge(E edge)
        {
            Edge<N, E> tmpEdge = getEdge(edge);
            edges.Remove(edge);

            tmpEdge.firstAdjaencyNode.adjaencyList.Remove(tmpEdge);
            tmpEdge.secondAdjaencyNode.adjaencyList.Remove(tmpEdge);

            tmpEdge.firstAdjaencyNode = tmpEdge.secondAdjaencyNode = null;
        }

        public void removeEdge(Edge<N,E> edge)
        {
            edges.Remove(edge.data);
            edge.firstAdjaencyNode = edge.secondAdjaencyNode = null;
        }

        public void removeNode(N node)
        {
            Node<N, E> tmpNode = getNode(node);

            foreach (var e in tmpNode.adjaencyList)
            {
                removeEdge(e);
                Node<N, E> adjancyNode = e.firstAdjaencyNode == tmpNode ? e.secondAdjaencyNode : e.firstAdjaencyNode;
                adjancyNode.adjaencyList.Remove(e);
            }
        }

        internal Edge<N,E>getEdge(E edge)
        {
            return edges[edge];
        }

        internal Node<N,E> getNode(N key)
        {
            return vertexes[key];
        }

        public bool HasNode(N key)
        {
            return vertexes.ContainsKey(key);
        }

        public bool HasEdge(E key)
        {
            return edges.ContainsKey(key);
        }
    }

    public class Node<N, E>
    {
        public List<Edge<N, E>> adjaencyList;
        public N data;

        public Node()
        {
            adjaencyList = new List<Edge<N, E>>();
        }
    }

    public class Edge<N, E>
    {
        public Node<N, E> firstAdjaencyNode;
        public Node<N, E> secondAdjaencyNode;
        public E data;

        public Node<N,E> opposite(Node<N,E> node)
        {
            return node == firstAdjaencyNode ? secondAdjaencyNode : firstAdjaencyNode;
        }

    }

    /*
     * Dijsktras path finding algorithm.
     * 
     * TODO: code cleanup, create unit tests
     *
     */
    public class Djkstra<N, E> where N : IComparable<N> where E : IComparable<E>
    {
        public Func<E, double> costExtractor;// func getWeightOfEdge(edge)
        public N start;
        public N end;
        public Graph<N, E> graph;

        private HashSet<N> finalized = new HashSet<N>();
        private Dictionary<N, double> costs = new Dictionary<N, double>();
        private Dictionary<N, N> prev = new Dictionary<N, N>();

        public List<N> findRoute()
        {
            costs.Add(start, 0);

            while (costs.Where(p =>!finalized.Contains(p.Key)).Count() != 0)
            {
                double minValue = double.MaxValue;
                N minNode = default(N);

                foreach (var item in costs)
                {
                    if (!finalized.Contains(item.Key) && item.Value < minValue)
                    {
                        minNode = item.Key;
                        minValue = item.Value;
                    }
                }

                finalized.Add(minNode);

                foreach (var item in graph.getNode(minNode).adjaencyList)
                {
                    Node<N, E> nearestNode = item.opposite(graph.getNode(minNode));

                    if (finalized.Contains(nearestNode.data))
                        continue;

                    double ncost = minValue + costExtractor(item.data);

                    if (costs.ContainsKey(nearestNode.data))
                    {
                        double ocost = costs[nearestNode.data];
                        if (ncost < ocost)
                        {
                            costs[nearestNode.data] = ncost;
                            prev[nearestNode.data] = minNode;
                        }
                    } else
                    {
                        costs.Add(nearestNode.data, ncost);
                        prev[nearestNode.data] = minNode;
                    }
                }
            }

            List<N> route = new List<N>();

            N t = end;
            while (t != null)
            {
                route.Add(t);
                t = prev.ContainsKey(t) ? prev[t] : default(N);
            }

            route.Reverse();
            return route;
        }
    }
}
