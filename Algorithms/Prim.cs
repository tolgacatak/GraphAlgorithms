using GraphAlgorithms.Graph;

namespace GraphAlgorithms.Algorithms;

public class Prim
{
    private readonly Dictionary<int, List<(int To, int Weight)>> _adj = GraphBase.GetUndirectedAdjacencyList();

    public void Run(int startVertex)
    {
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine(" PRİM MST ALGORİTMASI");
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine();

        var n = GraphBase.VertexCount;
        var inMst = new bool[n];
        var mstEdges = new List<(int From, int To, int Weight)>();
        var totalWeight = 0;

        var pq = new PriorityQueue<(int Vertex, int Parent), int>();
        pq.Enqueue((startVertex, -1), 0);

        while (pq.Count > 0 && mstEdges.Count < n - 1)
        {
            var (u, parent) = pq.Dequeue();

            if (inMst[u]) continue;
            inMst[u] = true;

            if (parent != -1)
            {
                var weight = 0;
                foreach (var (to, w) in _adj[parent])
                {
                    if (to == u)
                    {
                        weight = w;
                        break;
                    }
                }
                mstEdges.Add((parent, u, weight));
                totalWeight += weight;
            }

            foreach (var (v, w) in _adj[u])
            {
                if (!inMst[v])
                    pq.Enqueue((v, u), w);
            }
        }

        Console.WriteLine($"Kaynak düğüm: {startVertex}");
        Console.WriteLine();

        if (mstEdges.Count < n - 1)
        {
            Console.WriteLine("Graf bu düğümden tamamen bağlı değil.");
        }
        else
        {
            Console.WriteLine($"{"Kenar",-15} {"Ağırlık"}");
            Console.WriteLine(new string('-', 25));
            foreach (var (from, to, weight) in mstEdges)
                Console.WriteLine($"{from,2} — {to,-10} {weight}");
            Console.WriteLine();
            Console.WriteLine($"Toplam MST ağırlığı: {totalWeight}");
        }

        Console.WriteLine();
    }
}
