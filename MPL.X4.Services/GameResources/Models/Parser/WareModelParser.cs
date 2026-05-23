using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IWareModel"/> from an <see cref="IWareData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="IGameResourceModelParsingScope"/> that is the parsing scope.</param>
internal class WareModelParser(
                               ILogger<WareModelParser> logger,
                               IGameResourceModelParsingScope parsingScope)
    : ModelParserBase<IWareData, IWareModel>(logger),
      IModelParser<IWareData, IWareModel>
{
    private protected override IWareModel OnParse(IWareData source)
    {
        var factoryName = source.FactoryNameResource is null
                                                             ? string.Empty
                                                             : parsingScope.Lookup(source.FactoryNameResource);

        var name = source.NameResource is null
                                               ? string.Empty
                                               : parsingScope.Lookup(source.NameResource);

        var type = ParseWareType(source.Transport);

        return new WareModel
        {
            ComponentReference = source.ComponentReference,
            FactoryName = factoryName,
            Group = source.Group,
            Id = source.Id,
            Name = name,
            Transport = source.Transport,
            Type = type,
            Volume = source.Volume
        };
    }

    private static WareType ParseWareType(string transport)
        => transport switch
        {
            Constants.XmlDataFile.AttributeValue.WareTransport.Container => WareType.Container,
            Constants.XmlDataFile.AttributeValue.WareTransport.Equipment => WareType.Equipment,
            Constants.XmlDataFile.AttributeValue.WareTransport.Inventory => WareType.Inventory,
            Constants.XmlDataFile.AttributeValue.WareTransport.Liquid => WareType.Liquid,
            Constants.XmlDataFile.AttributeValue.WareTransport.Ship => WareType.Ship,
            Constants.XmlDataFile.AttributeValue.WareTransport.Solid => WareType.Solid,
            _ => WareType.Other
        };
}
