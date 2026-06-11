using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IPaintModificationModel"/> from an <see cref="IPaintModificationData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class PaintModificationModelParser(
                                            ILogger<PaintModificationModelParser> logger,
                                            ISaveGameModelParsingScope parsingScope)
    : ModificationModelParserBase<IPaintModificationData, IPaintModificationModel>(logger, parsingScope)
{
    private protected override IPaintModificationModel OnParse(IPaintModificationData source)
    {
        ParseNameAndQuality(source.Ware, out var name, out var quality);

        return new PaintModificationModel
        {
            Id = source.Ware,
            IsGenerated = source.Generated,
            Name = name,
            Quality = quality
        };
    }
}
