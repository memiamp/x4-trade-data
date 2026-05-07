using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements a parser to a <typeparamref name="TModelList"/> from a <typeparamref name="TDataDictionary"/>.
/// </summary>
/// <typeparam name="TData">The type of the data to be parsed.</typeparam>
/// <typeparam name="TDataDictionary">The type of the data dictionary to be parsed, which must implement <see cref="IDictionaryCollection{TKey, TData}"/>.</typeparam>
/// <typeparam name="TKey">The type of the key for the dictionary, which cannot be nullable.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TModelList">The type of the model list, which must implement <see cref="IList{TModel}"/>.</typeparam>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal abstract class ModelListParser<TData, TDataDictionary, TKey, TModel, TModelList>(
                                                                                          ILogger<ModelListParser<TData, TDataDictionary, TKey, TModel, TModelList>> logger,
                                                                                          IModelParser modelParser)
    : ModelParserBase<TDataDictionary, TModelList>(logger)
    where TDataDictionary : IDictionaryCollection<TKey, TData>
    where TKey : notnull
    where TModelList : IList<TModel>
{
    /// <summary>
    /// Invoked to create that target <typeparamref name="TModelList"/>.
    /// </summary>
    /// <returns>A <typeparamref name="TModelList"/> that is the result.</returns>
    private protected abstract TModelList CreateTarget();

    private protected override TModelList OnParse(TDataDictionary source)
    {
        var returnValue = CreateTarget();
        foreach (var entry in source)
        {
            var mappedFaction = modelParser.Parse<TData, TModel>(entry.Value);
            returnValue.Add(mappedFaction);
        }

        return returnValue;
    }

    /// <summary>
    /// Gets the model parser.
    /// </summary>
    private protected IModelParser ModelParser => modelParser;
}
