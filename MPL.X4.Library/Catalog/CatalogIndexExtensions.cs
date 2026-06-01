namespace MPL.X4.Catalog;

/// <summary>
/// A class that implements extension methods to an <see cref="ICatalogIndex"/>.
/// </summary>
public static class CatalogIndexExtensions
{
    /// <summary>
    /// Orders the specified <paramref name="indexes"/> by their dependencies using Kahn's algorithm.
    /// </summary>
    /// <param name="indexes">An <see cref="IEnumerable{ICatalogIndex}"/> to be ordered.</param>
    /// <returns>A <see cref="IReadOnlyList{ICatalogIndex}"/> that is the result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a circular dependecny exists in the supplied <paramref name="indexes"/>, or a dependency is missing.</exception>
    public static IReadOnlyList<ICatalogIndex> OrderByDependencies(this IEnumerable<ICatalogIndex> indexes)
    {
        if (!indexes.Any())
        {
            return [];
        }

        var items = indexes.ToList();

        var groups = items
                          .GroupBy(x => x.GamePack)
                          .ToDictionary(
                                        g => g.Key,
                                        g => g.ToList()
                          );

        var gamePacks = groups.Keys.ToList();

        var graph = new Dictionary<string, List<string>>();
        var indegree = new Dictionary<string, int>();

        foreach (var pack in gamePacks)
        {
            graph[pack] = [];
            indegree[pack] = 0;
        }

        foreach (var group in groups)
        {
            var currentPack = group.Key;
            var currentItems = group.Value;

            foreach (var item in currentItems)
            {
                foreach (var depPack in item.Dependencies)
                {
                    if (!groups.ContainsKey(depPack))
                    {
                        throw new InvalidOperationException($"Missing dependency: {depPack} (required by {currentPack})");
                    }

                    if (depPack != currentPack)
                    {
                        graph[depPack].Add(currentPack);
                        indegree[currentPack]++;
                    }
                }
            }
        }

        if (groups.ContainsKey(Constants.CatalogFile.BaseGame))
        {
            foreach (var pack in gamePacks)
            {
                if (pack != Constants.CatalogFile.BaseGame)
                {
                    graph[Constants.CatalogFile.BaseGame].Add(pack);
                    indegree[pack] += 1;
                }
            }
        }

        var queue = new Queue<string>(
                                      gamePacks
                                               .Where(p => indegree[p] == 0)
                                               .OrderBy(p => p != Constants.CatalogFile.BaseGame)
                                               .ThenBy(p => p)
        );

        var returnValue = new List<ICatalogIndex>(items.Count);

        while (queue.Count > 0)
        {
            var currentPack = queue.Dequeue();
            returnValue.AddRange(groups[currentPack]);

            foreach (var dependentPack in graph[currentPack])
            {
                if (--indegree[dependentPack] == 0)
                    queue.Enqueue(dependentPack);
            }
        }

        if (returnValue.Count != items.Count)
        {
            throw new InvalidOperationException("Cyclic dependency detected in catalog indexes");
        }

        return returnValue;
    }
}
