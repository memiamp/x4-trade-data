using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="ISectorNameModel"/> from an <see cref="ISectorNameData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="IGameResourceModelParsingScope"/> that is the parsing scope.</param>
internal class SectorNameModelParser(
                                     ILogger<SectorNameModelParser> logger,
                                     IGameResourceModelParsingScope parsingScope)
    : ModelParserBase<ISectorNameData, ISectorNameModel>(logger),
      IModelParser<ISectorNameData, ISectorNameModel>
{
    private protected override ISectorNameModel OnParse(ISectorNameData source)
        => new SectorNameModel
        {
            Id = source.Id,
            Name = parsingScope.Lookup(source.NameResource)
        };
}
