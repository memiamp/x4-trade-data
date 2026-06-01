using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IOffsetModel"/> from an <see cref="IOffsetData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class OffsetModelParser(
                                  ILogger<OffsetModelParser> logger)
    : ModelParserBase<IOffsetData, IOffsetModel>(logger),
      IModelParser<IOffsetData, IOffsetModel>
{
    private protected override IOffsetModel OnParse(IOffsetData source)
        => new OffsetModel
        {
            Id = source.Macro,
            Name = source.Name,
            Offset = source.Offset
        };
}
