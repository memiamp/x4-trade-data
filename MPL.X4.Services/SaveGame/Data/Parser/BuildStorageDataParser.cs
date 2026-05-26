using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

using BuildProcessorElements = (
                                string? BuildAnchorId,
                                string? BuildAnchorConnectionId);

using BuildStorageElements = (
                              string? BuildAnchorId,
                              string? BuildAnchorConnectionId,
                              ICargoData CargoData,
                              IEnumerable<ITradeData> Trades,
                              ITransform3D Transform);

/// <summary>
/// A class that implements a data parser for a <see cref="IBuildStorageData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class BuildStorageDataParser(
                                      IDataParser dataParser,
                                      ILogger<BuildStorageDataParser> logger)
    : DataParserBase<IBuildStorageData>(dataParser, logger)
{
    private protected override async Task<IBuildStorageData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Owner, out string? owner) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.BuildStorageId, out string? id))
        {
            logger.LogWarning("Could not load build storage");
            throw new ArgumentException("Could not load build storage", nameof(reader));
        }

        var isKnown = GetIsKnownToPlayer(reader);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.State, out string? state);

        var (buildAnchorId, buildAnchorConnectionId, cargo, trades, transform) = await ParseElements(reader);

        return new BuildStorageData
        {
            BuildAnchorConnectionId = buildAnchorConnectionId,
            BuildAnchorId = buildAnchorId,
            Cargo = cargo,
            Code = code,
            Id = id,
            IsKnown = isKnown,
            Macro = macro,
            Owner = owner,
            State = state,
            Trades = trades,
            Transform = transform
        };
    }

    private async Task<IEnumerable<IWareItemData>> ParseCargo(IXmlReaderWrapper reader)
    {
        List<IWareItemData> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Cargo, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<ICargoData>(subtree);

                returnValue.AddRange(data.Items);
            }
        }

        return returnValue;
    }

    private static BuildProcessorElements ParseComponentBuildStorage(IXDocumentWrapper xdocument)
    {
        xdocument.TryGetAttribute(Constants.XmlDataFile.XPath.BuildStorage.BuildAnchorConnection, Constants.XmlDataFile.AttributeName.BuildAnchorId, out var buildAnchorId);
        xdocument.TryGetAttribute(Constants.XmlDataFile.XPath.BuildStorage.BuildAnchorConnected, Constants.XmlDataFile.AttributeName.Connection, out var buildAnchorConnectionId);

        return (buildAnchorId, buildAnchorConnectionId);
    }

    private async Task<IEnumerable<IWareItemData>> ParseComponentStorage(IXmlReaderWrapper reader)
    {
        List<IWareItemData> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Storage))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await ParseCargo(subtree);

                returnValue.AddRange(data);
            }
        }

        return returnValue;
    }

    private async Task<(BuildProcessorElements, IEnumerable<IWareItemData>)> ParseConnections(IXmlReaderWrapper reader)
    {
        string? buildAnchorConnectionId = null;
        string? buildAnchorId = null;
        List<IWareItemData> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connection, XmlNodeType.Element, 1))
            {
                if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Connection, x => x.Contains(Constants.XmlDataFile.AttributeValue.Connection.BuildModule)))
                {
                    var document = await reader.ReadSubtreeToXDocument();

                    (buildAnchorId, buildAnchorConnectionId) = ParseComponentBuildStorage(document);
                }
                else if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Connection, x => x.Contains(Constants.XmlDataFile.AttributeValue.Connection.Storage)))
                {
                    using var subtree = await reader.ReadSubtree();

                    var data = await ParseComponentStorage(subtree);

                    returnValue.AddRange(data);
                }
            }
        }

        return ((buildAnchorId, buildAnchorConnectionId), returnValue);
    }

    private async Task<BuildStorageElements> ParseElements(IXmlReaderWrapper reader)
    {
        string? buildAnchorConnectionId = null;
        string? buildAnchorId = null;
        var cargoItems = new List<IWareItemData>();
        List<ITradeData> trades = [];
        ITransform3D transform = ITransform3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                transform = await DataParser.Parse<ITransform3D>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connections, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var (buildingData, data) = await ParseConnections(subtree);

                cargoItems.AddRange(data);

                buildAnchorConnectionId = buildingData.BuildAnchorConnectionId;
                buildAnchorId = buildingData.BuildAnchorId;
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Trade, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await ParseTrades(subtree);

                trades.AddRange(data);
            }
        }

        return (buildAnchorId, buildAnchorConnectionId, new CargoData { Items = cargoItems }, trades, transform);
    }

    private async Task<IEnumerable<ITradeData>> ParseTrade(IXmlReaderWrapper reader)
    {
        List<ITradeData> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Trade, XmlNodeType.Element))
            {
                var data = await DataParser.Parse<ITradeData>(reader);

                returnValue.Add(data);
            }
        }

        return returnValue;
    }

    private async Task<IEnumerable<ITradeData>> ParseTrades(IXmlReaderWrapper reader)
    {
        IEnumerable<ITradeData> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Production, XmlNodeType.Element, 2))
            {
                using var subtree = await reader.ReadSubtree();

                returnValue = await ParseTrade(subtree);

                break;
            }
        }

        return returnValue;
    }
}
