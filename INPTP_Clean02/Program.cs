using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPTP_Clean02.Graph;

namespace INPTP_Clean02
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string, int> graph = new Graph<string, int>();
            graph.addNode("start");
            graph.addNode("end");
            graph.addNode("v1");
            graph.addNode("v2");
            graph.addNode("v3");

            graph.AddUnidirectedEdge(100, "start", "end");

            graph.AddUnidirectedEdge(5, "start", "v1");
            graph.AddBidirectedEdge(6, "v2", "v1");
            graph.AddUnidirectedEdge(7, "v2", "v3");
            graph.AddUnidirectedEdge(8, "v3", "end");

            graph.AddUnidirectedEdge(1, "v3", "v1");
            graph.AddUnidirectedEdge(2, "start", "v2");

            graph.removeEdge(6);

            Djkstra<string, int> djkstra = new Djkstra<string, int>()
            {
                costExtractor = i => i,
                start = "start",
                end = "end",
                graph = graph
            };

            var result = djkstra.findRoute();
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
