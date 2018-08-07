using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class Graph<T> where T : IComparable
    {
        List<Vertex<T>> verticies = new List<Vertex<T>>();

        public void AddVertex(Vertex<T> vertex)
        {
            if (verticies.Count == 0)
            {
                verticies.Add(vertex);
            }

            for (int i = 0; i < verticies.Count; i++)
            {
                if(verticies[i].Value.CompareTo(vertex.Value) == 0)
                {
                    return;
                }
            }

            verticies.Add(vertex);
            return;
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            for (int i = 0; i < verticies.Count; i++)
            {
                if(verticies[i].Value.CompareTo(vertex.Value) == 0)
                {
                    verticies.RemoveAt(i);
                    return;
                }
            }
            return;
        }

        public void AddEdge(Vertex<T> start, Vertex<T> end)
        {
            start.Neighbors.Add(end);
            end.Neighbors.Add(start);
        }

        public void RemoveEdge(Vertex<T> start, Vertex<T> end)
        {
            start.Neighbors.Remove(end);
            end.Neighbors.Remove(start);
        }

    }
}
