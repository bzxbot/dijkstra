using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            #if DEBUG
            args = new string[2] { "grafo.txt", "Canoas" };
            #endif

            if (args.Length < 2)
            {
                Console.WriteLine("Uso: Dijkstra.exe caminho_arquivo cidade_origem");
                return;
            }

            Graph graph = new Graph();

            try
            {
                StreamReader reader = new StreamReader(args[0]);
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] values = line.Split(',');
                    string from = values[0];
                    string to = values[1];
                    int cost = int.Parse(values[2]);
                    string path = values[3];

                    Node source = graph.nodes.Find(node => node.name == from);

                    if (source == null)
                    {
                        source = new Node(from);
                        graph.nodes.Add(source);
                    }

                    Node dest = graph.nodes.Find(node => node.name == to);

                    if (dest == null)
                    {
                        dest = new Node(to);
                        graph.nodes.Add(dest);
                    }

                    source.edges.Add(new Edge(dest, cost, path));
                    line = reader.ReadLine();
                }
            }
            catch (IOException)
            {
                Console.Write("Erro: Não foi possível ler o arquivo especificado.");
                return;
            }
            catch (Exception)
            {
                Console.Write("Erro: O conteúdo do arquivo não está em um formato válido, consulte a documentação do aplicativo.");
                return;
            }

            if (graph.nodes.Count > 0)
            {
                Node source = graph.nodes.Find(n => n.name == args[1]);

                if (source != null)
                {
                    Dijkstra(graph, source);

                    Console.WriteLine("Feito!");
                    Console.WriteLine("Distâncias encontradas:");

                    foreach (Node node in graph.nodes)
                    {
                        string output = source.name + " - " + node.name + " -> " + node.distance + " km";
                        if (!string.IsNullOrEmpty(node.path))
                        {
                            output += " pela " + node.path.Trim(';').Replace(";", ", ");
                            // Encontra a última ocorrência da vírgula e faz a substituição por 'e'.
                            Regex regex = new Regex(",(?!.*,)");
                            output = regex.Replace(output, " e", 1, 0);
                        }
                        Console.WriteLine(output);
                    }
                    Console.Write("Execução concluida.");
                }
                else
                {
                    Console.WriteLine("Erro: Nó origem não encontrado.");
                    Console.Write("Dica: Cidades com nomes que contém espaços devem ter aspas ao seu redor.");
                }
            }

            #if DEBUG
            Console.Read();
            #endif
        }

        static void Dijkstra(Graph graph, Node source)
        {
            graph.nodes.Find(node => node.name == source.name).distance = 0;
            PriorityQueue priority = new PriorityQueue();
            priority.Enqueue(graph.nodes);
            while (priority.list.Count > 0)
            {
                Node node = priority.Dequeue();
                foreach (Edge edge in node.edges)
                {
                    Node adjacent = edge.node;
                    int cost = edge.cost + node.distance;
                    if (cost < adjacent.distance)
                    {
                        adjacent.distance = cost;
                        adjacent.path = string.Empty;
                        if (!string.IsNullOrEmpty(node.path))
                            adjacent.path += node.path;
                        adjacent.path += edge.name + ";";
                        priority.Update(node);
                    }
                }
            }
        }
    }
}