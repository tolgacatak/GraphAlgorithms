using GraphAlgorithms.Algorithms;

while (true)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════════════╗");
    Console.WriteLine("║     GRAF ALGORİTMALARI - ANA MENÜ          ║");
    Console.WriteLine("╠════════════════════════════════════════════╣");
    Console.WriteLine("║  1. BFS (tüm başlangıç düğümleri)          ║");
    Console.WriteLine("║  2. Döngü Tespiti                          ║");
    Console.WriteLine("║  3. Dijkstra (pozitif ağırlıklar, d. 0)    ║");
    Console.WriteLine("║  4. Dijkstra (negatif ağırlıklar, d. 0)    ║");
    Console.WriteLine("║  5. Bellman-Ford (poz. ağırlıklar, d. 0)   ║");
    Console.WriteLine("║  6. Bellman-Ford (neg. ağırlıklar, d. 0)   ║");
    Console.WriteLine("║  7. Dijkstra - Bellman-Ford kıyası         ║");
    Console.WriteLine("║  8. Floyd-Warshall                         ║");
    Console.WriteLine("║  9. Prim (düğüm 0'dan)                     ║");
    Console.WriteLine("║  0. Çıkış                                  ║");
    Console.WriteLine("╚════════════════════════════════════════════╝");
    Console.Write("\nSeçim yapın: ");

    var key = Console.ReadLine();

    Console.Clear();

    if (key is "1" or "2" or "3" or "4" or "5" or "6" or "7" or "8" or "9")
    {
        var useNegative = key is "4" or "6" or "7";
        var edges = useNegative
            ? GraphAlgorithms.Graph.GraphBase.GetNegativeWeightEdges()
            : GraphAlgorithms.Graph.GraphBase.GetDirectedEdges();
        var adj = GraphAlgorithms.Graph.GraphBase.GetAdjacencyList(edges);

        Console.WriteLine("Graf (ağırlıklı komşuluk listesi):");
        foreach (var (vertex, neighbors) in adj)
        {
            var neighborsStr = neighbors.Count == 0
                ? "(giden kenar yok)"
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
            BellmanFord.RunComparison(0);
            break;
        case "8":
            new FloydWarshall().Run();
            break;
        case "9":
            new Prim().Run(0);
            break;
        case "0":
            Console.WriteLine("Çıkılıyor...");
            return;
        default:
            Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
            break;
    }

    Console.WriteLine("Menüye dönmek için bir tuşa basın...");
    Console.ReadKey(true);
}
