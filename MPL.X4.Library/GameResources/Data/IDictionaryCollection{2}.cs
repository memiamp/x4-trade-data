namespace MPL.X4.Data;

/// <summary>
/// An interface that defines the data model of a collection of <typeparamref name="TData"/>.
/// </summary>
/// <typeparam name="TData">The type of the data model in the collection.</typeparam>
/// <typeparam name="TKey">The type of the key used to access elements in the dictionary, which must be non-nullable.</typeparam>
public interface IDictionaryCollection<TKey, TData> : IDictionary<TKey, TData>
    where TKey : notnull
{
}
