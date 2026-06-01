using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements the base functionality of a data parser for a <typeparamref name="TDataModel"/>.
/// </summary>
/// <typeparam name="TDataModel">The type of the data model which must implement <see cref="ICollectableDropData{TDrop}"/>.</typeparam>
/// <typeparam name="TDrop">The type of the item in the drop.</typeparam>
/// <typeparam name="TDropParserType">The type of the parser for the drop.</typeparam>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal abstract class CollectableDropDataParser<TDataModel, TDrop, TDropParserType>(
                                                                                      IDataParser dataParser,
                                                                                      ILogger<CollectableDropDataParser<TDataModel, TDrop, TDropParserType>> logger)
    : DataParserBase<TDataModel>(dataParser, logger)
    where TDataModel : ICollectableDropData<TDrop>
{
    /// <summary>
    /// Overridden to create a result data model from the specified parameters.
    /// </summary>
    /// <param name="isKnown">A <see cref="bool"/> indicating whether the drop is known to the player.</param>
    /// <param name="items">An <see cref="IEnumerable{TDrop}"/> that are the drop items.</param>
    /// <param name="macro">A <see cref="string"/> that is the drop macro.</param>
    /// <param name="state">A nullable <see cref="string"/> that is the drop state.</param>
    /// <param name="transform">An <see cref="ITransform3D"/> that is the transform of the drop.</param>
    /// <returns>A <typeparamref name="TDataModel"/> that is the result.</returns>
    private protected abstract TDataModel CreateResult(bool isKnown, IEnumerable<TDrop> items, string macro, string? state, ITransform3D transform);

    /// <summary>
    /// Gets the results of the data parsed by <typeparamref name="TDropParserType"/>.
    /// </summary>
    /// <param name="data">A <typeparamref name="TDropParserType"/> to get the results from.</param>
    /// <returns>An <see cref="IEnumerable{TDrop}"/> that is the result.</returns>
    private protected abstract IEnumerable<TDrop> GetParserResult(TDropParserType data);

    private protected override async Task<TDataModel> OnParse(IXmlReaderWrapper reader)
    {
        TDataModel returnValue;

        if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro))
        {
            var isKnown = GetIsKnownToPlayer(reader);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.State, out string? state);

            var (transform, items) = await ParseElements(reader);

            returnValue = CreateResult(
                                       isKnown,
                                       items,
                                       macro,
                                       state,
                                       transform);
        }
        else
        {
            logger.LogWarning("Could not load collectable drop");
            throw new ArgumentException("Could not load collectable drop", nameof(reader));
        }

        return returnValue;
    }

    private async Task<(ITransform3D, IEnumerable<TDrop>)> ParseElements(IXmlReaderWrapper reader)
    {
        List<TDrop> items = [];
        ITransform3D transform = ITransform3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(DropNodeName, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<TDropParserType>(subtree);

                var dropItems = GetParserResult(data);

                items.AddRange(dropItems);
            }
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                transform = await DataParser.Parse<ITransform3D>(subtree);
            }
        }

        return (transform, items);
    }

    /// <summary>
    /// Gets the node name of the drop node for <typeparamref name="TDrop"/>.
    /// </summary>
    private protected abstract string DropNodeName { get; }
}
