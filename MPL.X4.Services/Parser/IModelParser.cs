namespace MPL.X4.Parser;

/// <summary>
/// An interface that defines the behaviour of a model parser.
/// </summary>
public interface IModelParser
{
    /// <summary>
    /// Parses the specified <paramref name="source"/> to a new <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TData">The type of the data to be parsed.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="source">A <typeparamref name="TData"/> that is the source to parse.</param>
    /// <param name="parserParameters">A <see langword="params"/> array of <see cref="object"/> containing any parameters to be supplied to the parser.</param>
    /// <returns>A <typeparamref name="TModel"/> that is the result.</returns>
    TModel Parse<TData, TModel>(TData source, params object[] parserParameters);
}
