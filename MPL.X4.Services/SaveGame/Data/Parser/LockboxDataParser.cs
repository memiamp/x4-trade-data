using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.DataParser;
using MPL.X4.Parser;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ILockboxData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{IPosition3D}"/> that is the position parser to use.</param>
internal class LockboxDataParser(
                                 ILogger<LockboxDataParser> logger,
                                 IDataParser<IPosition3D> positionParser)
    : PositionalDataParserBase<ILockboxData>(logger, positionParser)
{
    private protected override async Task<ILockboxData> OnParse(IXmlReaderWrapper reader)
    //private protected override async Task<ILockbox> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var lockCount = 0;
        var position = new Position3D();
        //var position = new SectorPosition(positionOffset);
        LockboxData? returnValue;
        List<string> wares = [];

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? type))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new LockboxData
            {
                Code = code,
                Id = id,
                IsKnown = isKnown,
                LockCount = 0,
                Position = position,
                Type = type,
                Wares = wares
            };
        }
        else
        {
            Logger.LogWarning("Could not load lockbox");
            throw new ArgumentException("Could not load lockbox", nameof(reader));
        }

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element, 2))
            {
                await UpdatePosition(reader, position);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Wares, XmlNodeType.Element, 1))
            {
                using var waresSubtree = await reader.ReadSubtree();

                var lockboxWares = await ProcessWares(waresSubtree);

                wares.AddRange(lockboxWares);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Lock))
            {
                lockCount++;
            }
        }

        returnValue.LockCount = lockCount;

        return returnValue;
    }

    private async Task<IEnumerable<string>> ProcessWares(IXmlReaderWrapper reader)
    {
        /*
		<wares>
			<ware ware="inv_quantum_data_shard"/>
			<ware ware="inv_seminar_piloting_4"/>
			<ware ware="inv_kyondevice_03"/>
		</wares>
        */
        var returnValue = new List<string>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ware, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
                returnValue.Add(ware);
            }
        }

        return returnValue;
    }
}
