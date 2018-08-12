using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class Graph<T>
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
                if (verticies[i].Value.Equals(vertex.Value))
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
                if (verticies[i].Value.Equals(vertex.Value))
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

            Heap<Vertex<T>> priorityQ = new Heap<Vertex<T>>(Comparer<Vertex<T>>.Create(CompareKnown));

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

            //start at end and work back
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

            Heap<Vertex<T>> priorityQ = new Heap<Vertex<T>>(Comparer<Vertex<T>>.Create(CompareFinal));

            start.knownDistance = 0;
            start.finalDistance = ManhattanHeuristic(start, end);
            priorityQ.Add(start);

            while (priorityQ.Size < 0)
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
                        neighbor.Key.finalDistance = neighbor.Key.knownDistance + ManhattanHeuristic(neighbor.Key, end);
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

            ////////////LOOK AT THIS: there ends up being a null in the stack...
            var stack = new Stack<Vertex<T>>();
            stack.Push(end);
            while (stack.Peek() != start)
            {
                stack.Push(stack.Peek().founder);
            }

            return stack;
        }

        public double D = 1;

        public double ManhattanHeuristic(Vertex<T> start, Vertex<T> end)
        {
            double dx = Math.Abs(start.Position.X - end.Position.X);
            double dy = Math.Abs(start.Position.Y - end.Position.Y);
            return D * (dx + dy);
        }

        public double EuclideanHeuristic(Vertex<T> start, Vertex<T> end)
        {
            double dx = Math.Abs(start.Position.X - end.Position.X);
            double dy = Math.Abs(start.Position.Y - end.Position.Y);
            return D * Math.Sqrt(dx * dx + dy * dy);
        }

        private int CompareKnown(Vertex<T> x, Vertex<T> y)
        {
            return x.knownDistance.CompareTo(y.knownDistance);
        }

        private int CompareFinal(Vertex<T> x, Vertex<T> y)
        {
            return x.finalDistance.CompareTo(y.finalDistance);
        }
    }


}
