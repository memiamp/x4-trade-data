using Microsoft.Extensions.Logging;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements the base functionality of a model parser.
/// </summary>
/// <typeparam name="TData">The type of the data to be parsed.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal abstract class ModelParserBase<TData, TModel>(
                                                       ILogger<ModelParserBase<TData, TModel>> logger)
    : IModelParser<TData, TModel>
{
    /// <summary>
    /// Invoked to parse the specified <paramref name="source"/> to a new <typeparamref name="TModel"/>.
    /// </summary>
    /// <param name="source">A <typeparamref name="TData"/> that is the source to parse.</param>
    /// <returns>A <typeparamref name="TModel"/> that is the result.</returns>
    private protected abstract TModel OnParse(TData source);

    /// <summary>
    /// Gets the logger.
    /// </summary>
    private protected ILogger Logger => logger;

    TModel IModelParser<TData, TModel>.Parse(TData source)
        => OnParse(source);
}
