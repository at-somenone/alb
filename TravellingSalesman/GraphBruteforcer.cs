using System.Collections.Generic;

namespace WpfTesty
{
    public class GraphBruteforcer
    {
        private int counter;
        private double distance;
        private List<Vertex> shortestPath;

        private Vertex startVertex;
        private Stack<Vertex> usedVertices;
        private Stack<Vertex> verticesStack;

        private Graph graph;
        public double minDistance { get; private set; }

        public GraphBruteforcer(Graph graph) {
            this.graph = graph;
            verticesStack = new();
            usedVertices = new();
            shortestPath = new();
            minDistance = 0;
        }

        public List<Vertex> ShortestCycle() {
            startVertex = graph.Vertices[0];
            CycleRecursive(startVertex);
            return shortestPath;
        }

        private void CycleRecursive(Vertex vertex) {
            counter++;
            usedVertices.Push(vertex);
            verticesStack.Push(vertex);

            foreach (var edge in vertex.Neighbors) {
                var otherVertex = edge.Vertex1 == vertex ? edge.Vertex2 : edge.Vertex1;
                if (otherVertex == startVertex) {
                    if (counter == graph.Vertices.Count) {
                        verticesStack.Push(otherVertex);
                        distance += edge.Distance;

                        if (minDistance == 0 || distance < minDistance) {
                            shortestPath.Clear();
                            shortestPath.AddRange(verticesStack);
                            minDistance = distance;
                        }

                        distance -= edge.Distance;
                        verticesStack.Pop();
                    }
                }

                if (!usedVertices.Contains(otherVertex)) {
                    distance += edge.Distance;
                    CycleRecursive(otherVertex);
                    distance -= edge.Distance;
                }
            }

            verticesStack.Pop();
            usedVertices.Pop();
            counter--;
        }
    }
}
