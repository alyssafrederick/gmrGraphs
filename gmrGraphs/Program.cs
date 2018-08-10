using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string> undirGraph = new Graph<string>();

            var ca = new Vertex<string>("ca");
            var ny = new Vertex<string>("ny");
            var az = new Vertex<string>("az");
            var hi = new Vertex<string>("hi");
            var tx = new Vertex<string>("tx");

            undirGraph.AddVerticies(new List<Vertex<string>>(new Vertex<string>[] { ca, ny, az, hi, tx }));

            undirGraph.AddUndirectedEdge(ca, ny);
            undirGraph.AddUndirectedEdge(az, ny);
            undirGraph.AddUndirectedEdge(az, hi);
            undirGraph.AddUndirectedEdge(hi, tx);
            undirGraph.AddUndirectedEdge(tx, ca);

            //undirGraph.RemoveVerticies(new List<Vertex<string>>(new Vertex<string>[] { ca, az}));

            //undirGraph.DepthFirstTraversal(ny);

            undirGraph.BreadthFirstTraversal(ny);

            Console.ReadLine();
        }

    }
}
