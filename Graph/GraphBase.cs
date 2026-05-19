namespace GraphAlgorithms.Graph;

public record Edge(int From, int To, int Weight);

public static class GraphBase
{
    public const int VertexCount = 12;

    public static List<Edge> GetDirectedEdges()
    {
        return
        [
            new Edge(0, 1, 4),
            new Edge(0, 2, 2),
            new Edge(1, 3, 5),
            new Edge(1, 4, 10),
            new Edge(2, 1, 1),
            new Edge(2, 5, 8),
            new Edge(3, 4, 2),
            new Edge(3, 6, 6),
            new Edge(4, 7, 3),
            new Edge(5, 4, 1),
            new Edge(5, 8, 7),
            new Edge(6, 7, 1),
            new Edge(6, 9, 4),
            new Edge(7, 10, 5),
            new Edge(8, 5, 3),
            new Edge(8, 9, 2),
            new Edge(9, 10, 6),
            new Edge(9, 11, 3),
            new Edge(10, 11, 2),
            new Edge(11, 0, 9),
            new Edge(3, 5, 3),
            new Edge(1, 6, 12),
            new Edge(4, 8, 4),
            new Edge(2, 3, 7)
        ];
    }

    public static Dictionary<int, List<(int To, int Weight)>> GetAdjacencyList(List<Edge>? edges = null)
    {
        edges ??= GetDirectedEdges();
        var adj = new Dictionary<int, List<(int To, int Weight)>>();
        for (int i = 0; i < VertexCount; i++)
            adj[i] = new List<(int, int)>();

        foreach (var edge in edges)
            adj[edge.From].Add((edge.To, edge.Weight));

        return adj;
    }

    public static Dictionary<int, List<(int To, int Weight)>> GetUndirectedAdjacencyList()
    {
        var edges = GetDirectedEdges();
        var edgeMap = new Dictionary<(int, int), int>();

        foreach (var edge in edges)
        {
            int u = Math.Min(edge.From, edge.To);
            int v = Math.Max(edge.From, edge.To);
            var key = (u, v);

            if (!edgeMap.ContainsKey(key) || edge.Weight < edgeMap[key])
                edgeMap[key] = edge.Weight;
        }

        var adj = new Dictionary<int, List<(int To, int Weight)>>();
        for (int i = 0; i < VertexCount; i++)
            adj[i] = new List<(int, int)>();

        foreach (var kvp in edgeMap)
        {
            int u = kvp.Key.Item1;
            int v = kvp.Key.Item2;
            int w = kvp.Value;
            adj[u].Add((v, w));
            adj[v].Add((u, w));
        }

        return adj;
    }

    public static List<Edge> GetNegativeWeightEdges()
    {
        return
        [
            new Edge(0, 1, 4),
            new Edge(0, 2, 2),
            new Edge(1, 3, 5),
            new Edge(1, 4, 10),
            new Edge(2, 1, -1),
            new Edge(2, 5, 8),
            new Edge(3, 4, 2),
            new Edge(3, 6, 6),
            new Edge(4, 7, 3),
            new Edge(5, 4, -2),
            new Edge(5, 8, 7),
            new Edge(6, 7, 1),
            new Edge(6, 9, 4),
            new Edge(7, 10, 5),
            new Edge(8, 5, 3),
            new Edge(8, 9, 2),
            new Edge(9, 10, 6),
            new Edge(9, 11, -3),
            new Edge(10, 11, 2),
            new Edge(11, 0, 9),
            new Edge(3, 5, -1),
            new Edge(1, 6, 12),
            new Edge(4, 8, 4),
            new Edge(2, 3, 7)
        ];
    }
}
