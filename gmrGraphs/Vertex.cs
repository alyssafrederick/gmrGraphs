using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class Vertex<T> : IComparable<Vertex<T>> where T : IComparable<T>
    {
        public T Value;

        public Dictionary<Vertex<T>, double> Neighbors = new Dictionary<Vertex<T>, double>();
        //public List<Vertex<T>> Neighbors = new List<Vertex<T>>();


        public bool visited = false;
        public double knownDistance = 0;
        public Vertex<T> founder;


        public int x;
        public int y;
        public double finalDistance;

        public Vertex(T value)
        {
            Value = value;
        }

        public int CompareTo(Vertex<T> other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
