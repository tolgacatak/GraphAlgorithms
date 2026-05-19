using GraphAlgorithms.Graph;

namespace GraphAlgorithms.Algorithms;

public class CycleDetection
{
    private enum Color { White, Gray, Black }

    private readonly Dictionary<int, List<(int To, int Weight)>> _adj = GraphBase.GetAdjacencyList();

    public void Run()
    {
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine(" CYCLE DETECTION (DFS Coloring)");
        Console.WriteLine("═══════════════════════════════");
        Console.WriteLine();

        var color = new Color[GraphBase.VertexCount];
        var parent = new int[GraphBase.VertexCount];
        Array.Fill(parent, -1);

        List<int>? cyclePath = null;

        for (var i = 0; i < GraphBase.VertexCount; i++)
        {
            if (color[i] != Color.White) continue;
            cyclePath = Dfs(i, color, parent);
            if (cyclePath != null)
                break;
        }

        if (cyclePath != null)
        {
            Console.WriteLine("Result: Cycle FOUND!");
            Console.WriteLine($"Cycle path: {string.Join(" → ", cyclePath)}");
        }
        else
        {
            Console.WriteLine("Result: No cycle detected.");
        }

        Console.WriteLine();
    }

    private List<int>? Dfs(int u, Color[] color, int[] parent)
    {
        color[u] = Color.Gray;

        foreach (var (v, _) in _adj[u])
        {
            switch (color[v])
            {
                case Color.Gray:
                {
                    var cycle = new List<int> { v };
                    var node = u;
                    while (node != v)
                    {
                        cycle.Add(node);
                        node = parent[node];
                    }
                    cycle.Add(v);
                    cycle.Reverse();
                    return cycle;
                }
                case Color.White:
                {
                    parent[v] = u;
                    var result = Dfs(v, color, parent);
                    if (result != null)
                        return result;
                    break;
                }
                case Color.Black:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        color[u] = Color.Black;
        return null;
    }
}
