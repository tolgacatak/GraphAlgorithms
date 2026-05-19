using GraphAlgorithms.Graph;

namespace GraphAlgorithms.Algorithms;

public class Dijkstra
{
    public (int[] Dist, int[] Prev) Run(int startVertex, bool useNegativeWeights = false, bool printResults = true)
    {
        var edges = useNegativeWeights ? GraphBase.GetNegativeWeightEdges() : GraphBase.GetDirectedEdges();
        var adj = GraphBase.GetAdjacencyList(edges);

        var n = GraphBase.VertexCount;
        var dist = new int[n];
        var prev = new int[n];
        var visited = new bool[n];

        Array.Fill(dist, int.MaxValue);
        Array.Fill(prev, -1);
        dist[startVertex] = 0;

        var pq = new PriorityQueue<int, int>();
        pq.Enqueue(startVertex, 0);

        while (pq.Count > 0)
        {
            var u = pq.Dequeue();
            if (visited[u]) continue;
            visited[u] = true;

            foreach (var (v, w) in adj[u])
            {
                if (dist[u] == int.MaxValue || dist[u] + w >= dist[v]) continue;
                dist[v] = dist[u] + w;
                prev[v] = u;
                pq.Enqueue(v, dist[v]);
            }
        }

        if (printResults)
            PrintResults(startVertex, dist, prev, useNegativeWeights);

        return (dist, prev);
    }

    private void PrintResults(int startVertex, int[] dist, int[] prev, bool useNegativeWeights)
    {
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine(" DIJKSTRA'S ALGORITHM");
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine();

        if (useNegativeWeights)
        {
            Console.WriteLine("⚠ WARNING: Graph contains negative weights!");
            Console.WriteLine("  Dijkstra does NOT guarantee correct results with negative edges.");
            Console.WriteLine("  Results below may be INCORRECT.");
            Console.WriteLine();
        }

        Console.WriteLine($"Source vertex: {startVertex}");
        Console.WriteLine();
        Console.WriteLine($"{"Vertex",-10} {"Distance",-12} {"Path"}");
        Console.WriteLine(new string('-', 50));

        for (var i = 0; i < GraphBase.VertexCount; i++)
        {
            var distance = dist[i] == int.MaxValue ? "INF" : dist[i].ToString();
            var path = GetPath(prev, startVertex, i);
            Console.WriteLine($"{i,-10} {distance,-12} {path}");
        }

        Console.WriteLine();
    }

    public static string GetPath(int[] prev, int start, int end)
    {
        if (start == end) return start.ToString();
        if (prev[end] == -1) return "unreachable";

        var path = new List<int>();
        var current = end;
        while (current != -1)
        {
            path.Add(current);
            current = prev[current];
        }
        path.Reverse();

        return path[0] != start ? "unreachable" : string.Join(" → ", path);
    }
}
