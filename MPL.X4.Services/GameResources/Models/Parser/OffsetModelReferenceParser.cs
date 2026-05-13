using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IOffsetModelReference"/> from an <see cref="IOffsetDataDictionary"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class OffsetModelReferenceParser(
                                          ILogger<OffsetModelReferenceParser> logger,
                                          IModelParser modelParser)
    : ModelParserBase<IOffsetDataDictionary, IOffsetModelReference>(logger)
{
    private static readonly IEnumerable<string> _excludedReferences = [
                                                                       Constants.XmlDataFile.AttributeValue.Reference.Sectors,
                                                                       Constants.XmlDataFile.AttributeValue.Reference.Zones];

    private protected override IOffsetModelReference OnParse(IOffsetDataDictionary source)
    {
        var others = ParseOthers(source);
        var sectors = ParseReferenceType(source, Constants.XmlDataFile.AttributeValue.Reference.Sectors);
        var zones = ParseReferenceType(source, Constants.XmlDataFile.AttributeValue.Reference.Zones);

        return new OffsetModelReference
        {
            Other = others,
            Sectors = sectors,
            Zones = zones
        };
    }

    private Dictionary<string, IOffsetModelList> ParseOthers(IOffsetDataDictionary source, IEnumerable<string>? excludedReferences = null)
    {
        Dictionary<string, IOffsetModelList> returnValue = [];

        excludedReferences ??= _excludedReferences;

        var models = source
                           .Where(x => !_excludedReferences.Contains(x.Value.ReferenceType))
                           .GroupBy(
                                    x => x.Value.ReferenceType,
                                    (x, v) => new { ReferenceType = x, OffsetData = v.Select(x => x.Value) });
        foreach (var item in models)
        {
            var items = modelParser.Parse<IEnumerable<IOffsetData>, IOffsetModelList>(item.OffsetData);
            returnValue[item.ReferenceType] = items;
        }

        return returnValue;
    }

    private IOffsetModelList ParseReferenceType(IOffsetDataDictionary source, string referenceType)
    {
        var models = source
                           .Where(x => x.Value.ReferenceType == referenceType)
                           .Select(x => x.Value);

        return modelParser.Parse<IEnumerable<IOffsetData>, IOffsetModelList>(models);
    }
}
