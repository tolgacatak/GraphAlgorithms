using GraphAlgorithms.Graph;

namespace GraphAlgorithms.Algorithms;

public class FloydWarshall
{
    private const int Inf = int.MaxValue / 2;

    public void Run()
    {
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine(" FLOYD-WARSHALL ALGORITHM");
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine();

        var n = GraphBase.VertexCount;
        var dist = new int[n, n];
        var next = new int[n, n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                dist[i, j] = (i == j) ? 0 : Inf;
                next[i, j] = -1;
            }
        }

        var edges = GraphBase.GetDirectedEdges();
        foreach (var edge in edges.Where(edge => edge.Weight < dist[edge.From, edge.To]))
        {
            dist[edge.From, edge.To] = edge.Weight;
            next[edge.From, edge.To] = edge.To;
        }

        for (var i = 0; i < n; i++)
            next[i, i] = i;

        for (var k = 0; k < n; k++)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (dist[i, k] + dist[k, j] >= dist[i, j]) continue;
                    dist[i, j] = dist[i, k] + dist[k, j];
                    next[i, j] = next[i, k];
                }
            }
        }

        PrintPathsFromSource(dist, next, n, sourceVertex: 0);
    }

    private void PrintPathsFromSource(int[,] dist, int[,] next, int n, int sourceVertex)
    {
        Console.WriteLine($"Source vertex: {sourceVertex}");
        Console.WriteLine();
        Console.WriteLine($"{"Vertex",-10} {"Distance",-12} {"Path"}");
        Console.WriteLine(new string('-', 50));

        for (var j = 0; j < n; j++)
        {
            if (j == sourceVertex) continue;
            var distance = dist[sourceVertex, j] >= Inf ? "INF" : dist[sourceVertex, j].ToString();
            var path = dist[sourceVertex, j] >= Inf ? "unreachable" : ReconstructPath(next, sourceVertex, j);
            Console.WriteLine($"{j,-10} {distance,-12} {path}");
        }

        Console.WriteLine();
    }

    private string ReconstructPath(int[,] next, int from, int to)
    {
        if (next[from, to] == -1) return "no path";

        var path = new List<int> { from };
        var current = from;
        while (current != to)
        {
            current = next[current, to];
            if (current == -1) return "no path";
            path.Add(current);
        }

        return string.Join(" → ", path);
    }
}
