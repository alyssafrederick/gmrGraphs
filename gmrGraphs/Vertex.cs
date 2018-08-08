using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class Vertex<T> where T : IComparable
    {
        public T Value;
        //public Dictionary<Vertex<T>, double> Neighbors = new Dictionary<Vertex<T>, double>();
        public List<Vertex<T>> Neighbors = new List<Vertex<T>>();
        public bool visited = false;

        public Vertex(T value)
        {
            Value = value;
        }
    }
}
