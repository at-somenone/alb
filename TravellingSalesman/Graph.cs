using System.Collections.Generic;

namespace WpfTesty
{
    public class Vertex
    {
        public Vertex() {
            Neighbors = new();
        }

        public int Index { get; init; }
        public List<Edge> Neighbors { get; set; }
    }

    public class Edge
    {
        public Edge() {
            Vertex1 = new();
            Vertex2 = new();
        }

        public float Distance { get; set; }
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex2 { get; set; }
    }

    public class Graph
    {
        public Graph() {
            Vertices = new();
            Edges = new();
        }

        public List<Vertex> Vertices { get; set; }
        public List<Edge> Edges { get; set; }
    }
}
