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

            undirGraph.AddVertex(new Vertex<string>("ca"));
            undirGraph.AddVertex(new Vertex<string>("ny"));
            undirGraph.AddVertex(new Vertex<string>("az"));


            Console.ReadLine();
        }

    }
}
