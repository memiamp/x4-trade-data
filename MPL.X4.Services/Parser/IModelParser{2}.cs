namespace MPL.X4.Parser;

/// <summary>
/// An interface that defines the behaviour of a model parser.
/// </summary>
/// <typeparam name="TData">The type of the data to be parsed.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
public interface IModelParser<TData, TModel>
{
    /// <summary>
    /// Parses the specified <paramref name="source"/> to a new <typeparamref name="TModel"/>.
    /// </summary>
    /// <param name="source">A <typeparamref name="TData"/> that is the source to parse.</param>
    /// <returns>A <typeparamref name="TModel"/> that is the result.</returns>
    TModel Parse(TData source);
}
