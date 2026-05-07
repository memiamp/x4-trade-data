using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements the base functionality of a model parser.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="serviceProvider">An <see cref="IServiceProvider"/> that is the service provider to use.</param>
internal class ModelParser(
                           ILogger<ModelParser> logger,
                           IServiceProvider serviceProvider)
    : IModelParser
{
    TModel IModelParser.Parse<TData, TModel>(TData source, params object[] parserParameters)
    {
        var parser = serviceProvider.GetService<IModelParser<TData, TModel>>();

        if (parser is null)
        {
            logger.LogWarning("A model parser from {DataType} to {ModelType} was not found", typeof(TData), typeof(TModel));
            throw new InvalidOperationException("A data to model parser for the specified types cannot be found");
        }

        return parser.Parse(source);
    }
}
