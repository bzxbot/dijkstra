using System.Collections.Generic;

namespace Dijkstra
{
    class PriorityQueue
    {
        public List<Node> list = new List<Node>();

        public void Enqueue(List<Node> list)
        {
            foreach (Node node in list)
            {
                this.Enqueue(node);
            }
        }

        public void Enqueue(Node node)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (node.distance < list[i].distance)
                {
                    list.Insert(i, node);
                    return;
                }
            }
            list.Add(node);
        }

        public Node Dequeue()
        {
            Node node = list[0];
            list.RemoveAt(0);
            return node;
        }

        public void Update(Node node)
        {
            list.Remove(node);
            this.Enqueue(node);
        }
    }
}