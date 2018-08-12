using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class Graph<T> where T : IComparable<T>
    {
        List<Vertex<T>> verticies = new List<Vertex<T>>();

        /// unweighted, undirected graphs
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
                        RemoveUndirectedEdge(vertex, vertex.Neighbors.Keys.First());
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
            if (start.Neighbors.ContainsKey(end) == true && end.Neighbors.ContainsKey(start) == true)
            {
                return;
            }

            start.Neighbors.Add(end, 0);
            end.Neighbors.Add(start, 0);
        }

        public void RemoveUndirectedEdge(Vertex<T> start, Vertex<T> end)
        {
            if (start.Neighbors.Keys.Contains(end) == false && end.Neighbors.Keys.Contains(start) == false)
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

            foreach (var n in start.Neighbors.Keys)
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
                if (!neighbors.Key.visited)
                {
                    neighbors.Key.visited = true;
                    q.Enqueue(neighbors.Key);
                }
            }

            for (int i = 0; i < q.Count; i++)
            {
                BreadthFirstTraversalRecursive(q.Dequeue(), q);
            }

        }


        /// weighted, directed graphs
        public void AddDirectedEdge(Vertex<T> start, Vertex<T> end, double weight)
        {
            if (start.Neighbors.ContainsKey(end) == true && start.Neighbors[end] == weight)
            {
                return;
            }

            start.Neighbors.Add(end, weight);
        }

        public void RemoveDirectedEdge(Vertex<T> start, Vertex<T> end)
        {
            if (start.Neighbors.Keys.Contains(end) == false)
            {
                return;
            }

            start.Neighbors.Remove(end);
        }

        public IEnumerable<Vertex<T>> Dijkstra(Vertex<T> start, Vertex<T> end)
        {
            //primms

            foreach (var vertex in verticies)
            {
                vertex.visited = false;
                vertex.knownDistance = float.PositiveInfinity;
                vertex.founder = null;
            }

            MinHeap<Vertex<T>> priorityQ = new MinHeap<Vertex<T>>();

            start.knownDistance = 0;
            priorityQ.Add(start);

            while (priorityQ.Size != 0)
            {
                Vertex<T> current = priorityQ.Pop();
                current.visited = true;

                if (current == end)
                {
                    break;
                }


                foreach (var neighbor in current.Neighbors)
                {
                    double tentativeDistance = current.knownDistance + neighbor.Value;
                    if (tentativeDistance < neighbor.Key.knownDistance)
                    {
                        neighbor.Key.knownDistance = tentativeDistance;
                        neighbor.Key.founder = current;
                        neighbor.Key.visited = false;
                    }
                    else
                    {
                        neighbor.Key.visited = true;
                    }

                    if (neighbor.Key.visited == false && priorityQ.Contains(neighbor.Key) == false)
                    {
                        priorityQ.Add(neighbor.Key);
                    }
                }
            }

            //start and end and work back
            var stack = new Stack<Vertex<T>>();
            stack.Push(end);
            while (stack.Peek() != start)
            {
                stack.Push(stack.Peek().founder);
            }

            return stack;
        }

        public IEnumerable<Vertex<T>> AStar(Vertex<T> start, Vertex<T> end)
        {
            foreach (var vertex in verticies)
            {
                vertex.visited = false;
                vertex.knownDistance = float.PositiveInfinity;
                vertex.finalDistance = float.PositiveInfinity;
                vertex.founder = null;
            }

            MinHeap<Vertex<T>> priorityQ = new MinHeap<Vertex<T>>();

            start.knownDistance = 0;
            priorityQ.Add(start);

            throw new NotImplementedException();
        }
    }


}
