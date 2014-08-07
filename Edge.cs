namespace Dijkstra
{
    class Edge
    {
        public Node node;
        public int cost;
        public string name;

        public Edge(Node node, int cost, string name)
        {
            this.node = node;
            this.cost = cost;
            this.name = name;
        }
    }
}
