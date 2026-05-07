namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements the data model of a collection of <typeparamref name="TData"/>.
/// </summary>
/// <typeparam name="TData">The type of the data model in the collection.</typeparam>
/// <typeparam name="TKey">The type of the key used to access elements in the dictionary, which must be non-nullable.</typeparam>
internal class DictionaryCollection<TKey, TData> : Dictionary<TKey, TData>, IDictionaryCollection<TKey, TData>
    where TKey : notnull
{
    /// <summary>
    /// Merges the specified <paramref name="source"/> with this collection.
    /// </summary>
    /// <param name="source">A <see cref="IDictionaryCollection{TKey, TData}"/> to be merged.</param>
    public void Merge(IDictionaryCollection<TKey, TData> source)
    {
        foreach (var kvp in source)
        {
            this[kvp.Key] = kvp.Value;
        }
    }

    public override string ToString()
        => $"Data model count: {Count}";
}
