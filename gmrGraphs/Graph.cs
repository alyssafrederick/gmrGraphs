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
                if (verticies[i].Value.CompareTo(vertex.Value) == 0)
                {
                    return;
                }
            }

            verticies.Add(vertex);
        }

        public void AddVerticies(List<Vertex<T>> verticiesToAdd)
        {
            for (int v = 0; v < verticiesToAdd.Count; v++)
            {
                AddVertex(verticiesToAdd[v]);
            }
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            for (int i = 0; i < verticies.Count; i++)
            {
                if (verticies[i].Value.CompareTo(vertex.Value) == 0)
                {
                    while (vertex.Neighbors.Count != 0)
                    {
                        //removes the edge between the vertex and the first neighbor entry
                        RemoveUndirectedEdge(vertex, vertex.Neighbors[0]);
                    }

                    verticies.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveVerticies(List<Vertex<T>> verticiesToRemove)
        {
            for (int v = 0; v < verticiesToRemove.Count; v++)
            {
                RemoveVertex(verticiesToRemove[v]);
            }
        }

        public void AddUndirectedEdge(Vertex<T> start, Vertex<T> end)
        {
            if (start.Neighbors.Contains(end) == true && end.Neighbors.Contains(start) == true)
            {
                return;
            }

            start.Neighbors.Add(end);
            end.Neighbors.Add(start);
        }

        public void RemoveUndirectedEdge(Vertex<T> start, Vertex<T> end)
        {
            if (start.Neighbors.Contains(end) == false && end.Neighbors.Contains(start) == false)
            {
                return;
            }

            start.Neighbors.Remove(end);
            end.Neighbors.Remove(start);
        }

        public void DepthFirstTraversal(Vertex<T> start)
        {
            foreach (var vertex in verticies)
            {
                vertex.visited = false;
            }

            DepthFirstTraversalRecursive(start, new Stack<Vertex<T>>());
        }

        private void DepthFirstTraversalRecursive(Vertex<T> start, Stack<Vertex<T>> stack)
        {
            start.visited = true;

            //action below (the writeline)
            Console.WriteLine(start.Value);

            foreach (var n in start.Neighbors)
            {
                if (!n.visited)
                {
                    stack.Push(n);
                    DepthFirstTraversalRecursive(n, stack);
                }
            }
        }

        public void BreadthFirstTraversal(Vertex<T> start)
        {
            foreach (var vertex in verticies)
            {
                vertex.visited = false;
            }

            BreadthFirstTraversalRecursive(start, new Queue<Vertex<T>>());
        }

        private void BreadthFirstTraversalRecursive(Vertex<T> start, Queue<Vertex<T>> q)
        {
            start.visited = true;
            Console.WriteLine($"{start.Value}");

            foreach (var neighbors in start.Neighbors)
            {
                if (!neighbors.visited)
                {
                    neighbors.visited = true;
                    q.Enqueue(neighbors);
                }
            }            

            for (int i = 0; i < q.Count; i++)
            {
                BreadthFirstTraversalRecursive(q.Dequeue(), q);
            }

        }

    }
}
