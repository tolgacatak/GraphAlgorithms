using GraphAlgorithms.Graph;

namespace GraphAlgorithms.Algorithms;

public class Prim
{
    private readonly Dictionary<int, List<(int To, int Weight)>> _adj = GraphBase.GetUndirectedAdjacencyList();

    private void Run(int startVertex)
    {
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

        Console.WriteLine($"  Start vertex {startVertex,2}: ");

        if (mstEdges.Count < n - 1)
        {
            Console.WriteLine("    Graph is not fully connected from this vertex.");
        }
        else
        {
            foreach (var (from, to, weight) in mstEdges)
                Console.WriteLine($"    Edge {from,2} — {to,2}, weight = {weight}");
            Console.WriteLine($"    Total MST weight: {totalWeight}");
        }

        Console.WriteLine();
    }

    public void RunAll()
    {
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine(" PRIM'S MST ALGORITHM");
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine();

        for (var i = 0; i < GraphBase.VertexCount; i++)
            Run(i);
    }
}
