using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="ILockbox"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
internal class LockboxParser(
                             ILogger<LockboxParser> logger,
                             IDataParser<ISectorPosition> positionParser)
    : PositionalDataParserBase<ILockbox>(logger, positionParser)
{
    private protected override async Task<ILockbox> OnParse(IXmlReaderWrapper reader)
    //private protected override async Task<ILockbox> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var lockCount = 0;
        var position = new SectorPosition();
        //var position = new SectorPosition(positionOffset);
        Lockbox? returnValue;

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? type))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new Lockbox
            {
                Code = code,
                Id = id,
                IsKnown = isKnown,
                LockCount = 0,
                Position = position,
                Type = type
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
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Lock))
            {
                lockCount++;
            }
        }

        returnValue.LockCount = lockCount;

        return returnValue;
    }
}
