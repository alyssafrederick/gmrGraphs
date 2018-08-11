using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class Vertex<T> : IComparable where T : IComparable
    {
        public T Value;
        
        public Dictionary<Vertex<T>, double> Neighbors = new Dictionary<Vertex<T>, double>();
        //public List<Vertex<T>> Neighbors = new List<Vertex<T>>();


        public bool visited = false;
        public double knownDistance = 0;
        public Vertex<T> founder;


        public Vertex(T value)
        {
            Value = value;
        }

        public int CompareTo(object obj)
        {
            if (obj is Vertex<T>)
            {
                Vertex<T> vertex = (Vertex<T>)obj;
                return vertex.Value.CompareTo(Value);
            }

            else
            {
                throw new Exception("you didn't give a vertex and it's comparing a vertex sooo");
            }
        }
    }
}
