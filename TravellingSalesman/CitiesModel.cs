using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfTesty
{
    public class CitiesModel
    {
        private Graph graph;
        public IReadOnlyList<Vertex> Vertices => graph.Vertices;
        public IReadOnlyCollection<Edge> Edges => graph.Edges;
        private int currentIndex;

        public CitiesModel() {
            graph = new();
        }


        public void Add() {
            var newVertex = new Vertex {
                Index = currentIndex
            };

            currentIndex++;

            foreach (var graphVertex in graph.Vertices) {
                var edge = new Edge {
                    Vertex1 = graphVertex,
                    Vertex2 = newVertex,
                    Distance = 1
                };

                newVertex.Neighbors.Add(edge);
                graphVertex.Neighbors.Add(edge);
                graph.Edges.Add(edge);
            }

            graph.Vertices.Add(newVertex);
        }


        public void Remove(Vertex city) {
            graph.Vertices.Remove(city);

            foreach (var vertex in graph.Vertices) {
                vertex.Neighbors = vertex.Neighbors.Except(deadEdges).ToList();
            }

            graph.Edges = graph.Edges.Except(deadEdges).ToList();
        }

        public IEnumerable<int> GetShortestRoute() {
            var calculator = new GraphBruteforcer(graph);

            foreach (var vertex in graph.Vertices) {
                Console.WriteLine($"{vertex.Index}: ");
                foreach (var edge in vertex.Neighbors) {
                    Console.WriteLine(edge.Vertex1.Index == vertex.Index
                                          ? $"  -> {edge.Vertex2.Index}: {edge.Distance}"
                                          : $"  -> {edge.Vertex1.Index}: {edge.Distance}");
                }
            }

            var shortestCycle = calculator.ShortestCycle();
            return shortestCycle.Select(vertex => vertex.Index);
        }

        public Edge GetEdge(Vertex selection1, Vertex selection2) {
            return Edges.FirstOrDefault(edge =>
                                            edge.Vertex1 == selection1 && edge.Vertex2 == selection2
                                         || edge.Vertex2 == selection1 && edge.Vertex1 == selection2);
        }
    }
}
