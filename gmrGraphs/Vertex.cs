using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class Vertex<T>
    {
        public T Value;
        public (int X, int Y) Position;

        public Dictionary<Vertex<T>, double> Neighbors = new Dictionary<Vertex<T>, double>();
        //public List<Vertex<T>> Neighbors = new List<Vertex<T>>();


        public bool visited = false;
        public double knownDistance = 0;
        public Vertex<T> founder;

        public double finalDistance;

        public Vertex(T value) : this(value, 0, 0) { }
        public Vertex(T value, int X, int Y)
        {
            Value = value;
            Position.X = X;
            Position.Y = Y;
        }
    }
}
