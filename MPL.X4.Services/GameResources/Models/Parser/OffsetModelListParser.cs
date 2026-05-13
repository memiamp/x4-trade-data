using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="IOffsetModelList"/> from an <see cref="IEnumerable{IOffsetData}"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class OffsetModelListParser(
                                     ILogger<OffsetModelListParser> logger,
                                     IModelParser modelParser)
    : ModelParserBase<IEnumerable<IOffsetData>, IOffsetModelList>(logger)
{
    private protected override IOffsetModelList OnParse(IEnumerable<IOffsetData> source)
    {
        var returnValue = new OffsetModelList();

        var models = source.Select(x => modelParser.Parse<IOffsetData, IOffsetModel>(x));
        returnValue.AddRange(models);

        return returnValue;
    }
}
