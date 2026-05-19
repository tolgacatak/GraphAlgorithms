using GraphAlgorithms.Graph;

namespace GraphAlgorithms.Algorithms;

public class BellmanFord
{
    public static void Run(int startVertex, bool useNegativeWeights = false)
    {
        var edges = useNegativeWeights ? GraphBase.GetNegativeWeightEdges() : GraphBase.GetDirectedEdges();
        var n = GraphBase.VertexCount;

        var dist = new int[n];
        var prev = new int[n];
        Array.Fill(dist, int.MaxValue);
        Array.Fill(prev, -1);
        dist[startVertex] = 0;

        for (var i = 0; i < n - 1; i++)
        {
            var updated = false;
            foreach (var edge in edges.Where(edge => dist[edge.From] != int.MaxValue && dist[edge.From] + edge.Weight < dist[edge.To]))
            {
                dist[edge.To] = dist[edge.From] + edge.Weight;
                prev[edge.To] = edge.From;
                updated = true;
            }
            if (!updated) break;
        }

        var hasNegativeCycle = edges.Any(edge => dist[edge.From] != int.MaxValue && dist[edge.From] + edge.Weight < dist[edge.To]);

        PrintResults(startVertex, dist, prev, useNegativeWeights, hasNegativeCycle);

        if (useNegativeWeights)
            PrintComparison(startVertex, dist, prev);
    }

    private static void PrintResults(int startVertex, int[] dist, int[] prev, bool useNegativeWeights, bool hasNegativeCycle)
    {
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine(" BELLMAN-FORD ALGORITHM");
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine();

        if (useNegativeWeights)
            Console.WriteLine("Using graph with NEGATIVE weights.");

        if (hasNegativeCycle)
        {
            Console.WriteLine("⚠ NEGATIVE CYCLE DETECTED! Shortest paths are undefined.");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("No negative cycle detected.");

        Console.WriteLine($"Source vertex: {startVertex}");
        Console.WriteLine();
        Console.WriteLine($"{"Vertex",-10} {"Distance",-12} Path");
        Console.WriteLine(new string('-', 50));

        for (var i = 0; i < GraphBase.VertexCount; i++)
        {
            var distance = dist[i] == int.MaxValue ? "INF" : dist[i].ToString();
            var path = Dijkstra.GetPath(prev, startVertex, i);
            Console.WriteLine($"{i,-10} {distance,-12} {path}");
        }

        Console.WriteLine();
    }

    private static void PrintComparison(int startVertex, int[] bfDist, int[] bfPrev)
    {
        Console.WriteLine("═══════════════════════════════════════════════════");
        Console.WriteLine(" DIJKSTRA vs BELLMAN-FORD (Negative Weights)");
        Console.WriteLine("═══════════════════════════════════════════════════");
        Console.WriteLine();

        var dijkstra = new Dijkstra();
        var (djDist, djPrev) = dijkstra.Run(startVertex, useNegativeWeights: true, printResults: false);

        Console.WriteLine($"Source vertex: {startVertex}");
        Console.WriteLine();
        Console.WriteLine($"{"Vertex",-8} {"Dijkstra",-12} {"Bellman-Ford",-14} {"Match",-8} {"Dijkstra Path",-25} Bellman-Ford Path");
        Console.WriteLine(new string('-', 95));

        for (var i = 0; i < GraphBase.VertexCount; i++)
        {
            var djDistStr = djDist[i] == int.MaxValue ? "INF" : djDist[i].ToString();
            var bfDistStr = bfDist[i] == int.MaxValue ? "INF" : bfDist[i].ToString();
            var match = djDist[i] == bfDist[i];
            var matchStr = match ? "OK" : "[!]";
            var djPath = Dijkstra.GetPath(djPrev, startVertex, i);
            var bfPath = Dijkstra.GetPath(bfPrev, startVertex, i);

            Console.WriteLine($"{i,-8} {djDistStr,-12} {bfDistStr,-14} {matchStr,-8} {djPath,-25} {bfPath}");
        }

        Console.WriteLine();
        Console.WriteLine("[!] = Dijkstra result differs from Bellman-Ford (likely incorrect due to negative weights)");
        Console.WriteLine();
    }
}
