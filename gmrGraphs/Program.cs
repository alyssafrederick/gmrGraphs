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
            Graph<string> dirGraph = new Graph<string>();

            var ca = new Vertex<string>("ca");
            var ny = new Vertex<string>("ny");
            var az = new Vertex<string>("az");
            var hi = new Vertex<string>("hi");
            var tx = new Vertex<string>("tx");

            var mi = new Vertex<string>("mi");
            var oh = new Vertex<string>("oh");
            var fl = new Vertex<string>("fl");
            var ky = new Vertex<string>("ky");
            var il = new Vertex<string>("il");



            // undirected graph
            undirGraph.AddVerticies(new List<Vertex<string>>(new Vertex<string>[] { ca, ny, az, hi, tx }));

            undirGraph.AddUndirectedEdge(ca, ny);
            undirGraph.AddUndirectedEdge(az, ny);
            undirGraph.AddUndirectedEdge(az, hi);
            undirGraph.AddUndirectedEdge(hi, tx);
            undirGraph.AddUndirectedEdge(tx, ca);

            //undirGraph.RemoveVerticies(new List<Vertex<string>>(new Vertex<string>[] { ca, az}));
            //undirGraph.DepthFirstTraversal(ny);
            undirGraph.BreadthFirstTraversal(ny);


            /// directed graph
            dirGraph.AddVerticies(new List<Vertex<string>>(new Vertex<string>[] { mi, oh, fl, ky, il }));

            dirGraph.AddDirectedEdge(mi, oh, 10);
            dirGraph.AddDirectedEdge(oh, fl, 2);
            dirGraph.AddDirectedEdge(fl, ky, 7);
            dirGraph.AddDirectedEdge(ky, il, 1);
            dirGraph.AddDirectedEdge(il, mi, 8);
            dirGraph.AddDirectedEdge(fl, mi, 4);

            dirGraph.Dijkstra(fl, mi);




            Console.ReadLine();
        }

    }
}
