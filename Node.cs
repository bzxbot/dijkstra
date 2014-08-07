using System.Collections.Generic;

namespace Dijkstra
{
    class Node
    {
        public List<Edge> edges = new List<Edge>();
        public string name;
        public int distance = 1000;
        public string path = string.Empty;

        public Node(string name)
        {
            this.name = name;
        }
    }
}
