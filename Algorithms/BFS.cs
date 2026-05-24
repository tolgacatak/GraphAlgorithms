using GraphAlgorithms.Graph;

namespace GraphAlgorithms.Algorithms;

public class BFS
{
    private readonly Dictionary<int, List<(int To, int Weight)>> _adj = GraphBase.GetAdjacencyList();

    private List<int> Run(int startVertex)
    {
        var visited = new bool[GraphBase.VertexCount];
        var order = new List<int>();
        var queue = new Queue<int>();

        visited[startVertex] = true;
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            order.Add(current);

            foreach (var (to, _) in _adj[current])
            {
                if (visited[to]) continue;
                visited[to] = true;
                queue.Enqueue(to);
            }
        }

        return order;
    }

    public void RunAll()
    {
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine(" GENİŞLİK ÖNCE ARAMA (BFS)");
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine();

        for (var i = 0; i < GraphBase.VertexCount; i++)
        {
            var order = Run(i);
            Console.WriteLine($"Başlangıç düğümü {i,2}: [{string.Join(", ", order)}]");
        }

        Console.WriteLine();
    }
}
