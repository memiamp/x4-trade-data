using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements a parser to a <typeparamref name="TModelList"/> from an <see cref="IEnumerable{TData}"/>
/// </summary>
/// <typeparam name="TData">The type of the data to be parsed.</typeparam>
/// <typeparam name="TKey">The type of the key for the dictionary, which cannot be nullable.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TModelList">The type of the model list, which must implement <see cref="IList{TModel}"/>.</typeparam>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal abstract class ModelListParser<TData, TKey, TModel, TModelList>(
                                                                         ILogger<ModelListParser<TData, TKey, TModel, TModelList>> logger,
                                                                         IModelParser modelParser)
    : ModelParserBase<IEnumerable<TData>, TModelList>(logger)
    where TKey : notnull
    where TModelList : IList<TModel>
{
    /// <summary>
    /// Invoked to create that target <typeparamref name="TModelList"/>.
    /// </summary>
    /// <returns>A <typeparamref name="TModelList"/> that is the result.</returns>
    private protected abstract TModelList CreateTarget();

    private protected override TModelList OnParse(IEnumerable<TData> source)
    {
        var returnValue = CreateTarget();
        foreach (var item in source)
        {
            var mappedFaction = modelParser.Parse<TData, TModel>(item);
            returnValue.Add(mappedFaction);
        }

        return returnValue;
    }

    /// <summary>
    /// Gets the model parser.
    /// </summary>
    private protected IModelParser ModelParser => modelParser;
}
