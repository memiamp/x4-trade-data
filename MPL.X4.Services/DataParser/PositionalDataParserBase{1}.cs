using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.DataParser;

/// <summary>
/// A class that implements the base functionality of a positional data parser for <typeparamref name="TData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
internal abstract class PositionalDataParserBase<TData>(
                                                        ILogger<PositionalDataParserBase<TData>> logger,
                                                        IDataParser<IPosition3D> positionParser)
    : DataParserBase<TData>(logger)
{
    /// <summary>
    /// Updates the specified <paramref name="target"/> by modifying the position using the specified <paramref name="offset"/>.
    /// </summary>
    /// <param name="offset">An <see cref="IPosition3D"/> that is the offset position.</param>
    /// <param name="target">An <see cref="Position3D"/> that is the position to be updated.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private protected static void UpdatePosition(IPosition3D offset, Position3D target)
    {
        target.X += offset.X;
        target.Y += offset.Y;
        target.Z += offset.Z;
    }

    /// <summary>
    /// Updates the specified <paramref name="position"/> by parsing the position at the current location in the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> that is the data reader.</param>
    /// <param name="position">An <see cref="SectorPosition"/> that is the position to update.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private protected async Task UpdatePosition(IXmlReaderWrapper reader, Position3D position)
    {
        var offsetPosition = await positionParser.Parse(reader);

        position.X += offsetPosition.X;
        position.Y += offsetPosition.Y;
        position.Z += offsetPosition.Z;
    }

    /// <summary>
    /// Updates the specified <paramref name="position"/> by parsing the position inside the offset in the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> that is the data reader.</param>
    /// <param name="position">An <see cref="SectorPosition"/> that is the position to update.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private protected async Task UpdatePositionFromOffset(IXmlReaderWrapper reader, Position3D position)
    {
        var offsetSubtree = await reader.ReadSubtree();
        
        while (await offsetSubtree.ReadAsync())
        {
            if (offsetSubtree.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element, 1))
            {
                await UpdatePosition(offsetSubtree, position);
                break;
            }
        }
    }

    /// <summary>
    /// Gets the position parser.
    /// </summary>
    private protected IDataParser<IPosition3D> PositionParser => positionParser;
}
