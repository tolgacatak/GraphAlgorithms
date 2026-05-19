using GraphAlgorithms.Algorithms;

while (true)
{
    Console.Clear();
    Console.WriteLine("╔═══════════════════════════════════════════╗");
    Console.WriteLine("║       GRAPH ALGORITHMS - MAIN MENU        ║");
    Console.WriteLine("╠═══════════════════════════════════════════╣");
    Console.WriteLine("║  1. BFS (all start vertices)              ║");
    Console.WriteLine("║  2. Cycle Detection                       ║");
    Console.WriteLine("║  3. Dijkstra (positive weights, vertex 0) ║");
    Console.WriteLine("║  4. Dijkstra (negative weights, vertex 0) ║");
    Console.WriteLine("║  5. Bellman-Ford (positive weights, v. 0) ║");
    Console.WriteLine("║  6. Bellman-Ford (negative weights, v. 0) ║");
    Console.WriteLine("║  7. Dijkstra vs Bellman-Ford comparison   ║");
    Console.WriteLine("║  8. Floyd-Warshall                        ║");
    Console.WriteLine("║  9. Prim (all start vertices)             ║");
    Console.WriteLine("║  0. Exit                                  ║");
    Console.WriteLine("╚═══════════════════════════════════════════╝");
    Console.Write("\nSelect an option: ");

    var key = Console.ReadLine();

    Console.Clear();

    if (key is "1" or "2" or "3" or "4" or "5" or "6" or "7" or "8" or "9")
    {
        var useNegative = key is "4" or "6";
        var edges = useNegative
            ? GraphAlgorithms.Graph.GraphBase.GetNegativeWeightEdges()
            : GraphAlgorithms.Graph.GraphBase.GetDirectedEdges();
        var adj = GraphAlgorithms.Graph.GraphBase.GetAdjacencyList(edges);

        Console.WriteLine("Graph (adjacency list with weights):");
        foreach (var (vertex, neighbors) in adj)
        {
            var neighborsStr = neighbors.Count == 0
                ? "(no outgoing edges)"
                : string.Join(", ", neighbors.Select(n => $"{n.To}({n.Weight})"));
            Console.WriteLine($"  {vertex} → {neighborsStr}");
        }
        Console.WriteLine();
    }

    switch (key)
    {
        case "1":
            new BFS().RunAll();
            break;
        case "2":
            new CycleDetection().Run();
            break;
        case "3":
            new Dijkstra().Run(0);
            break;
        case "4":
            new Dijkstra().Run(0, useNegativeWeights: true);
            break;
        case "5":
            BellmanFord.Run(0);
            break;
        case "6":
            BellmanFord.Run(0, useNegativeWeights: true);
            break;
        case "7":
            Console.WriteLine("Running Bellman-Ford with negative weights (includes Dijkstra comparison)...");
            Console.WriteLine();
            BellmanFord.Run(0, useNegativeWeights: true);
            break;
        case "8":
            new FloydWarshall().Run();
            break;
        case "9":
            new Prim().RunAll();
            break;
        case "0":
            Console.WriteLine("Exiting...");
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }

    Console.WriteLine("Press any key to return to menu...");
    Console.ReadKey(true);
}
