using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SpecialItem;

/// <summary>
/// A class that implements a special item for an abandoned <see cref="IShipModel"/>.
/// </summary>
/// <param name="sectorName">A <see cref="string"/> containing the name of the sector the item is in.</param>
/// <param name="source">An <see cref="IShipModel"/> that is the source of the special item.</param>
internal class AbandonedShipSpecialItem(
                                        string sectorName,
                                        IShipModel source)
    : SpecialItemBase(sectorName, source)
{
    private readonly Color _colour = GetColour(source);
    private readonly string _comments = GetComments(source);
    private readonly string _description = source.Model;
    private readonly SpecialItemType _type = GetType(source);

    private static Color GetColour(IShipModel source)
        => source.Class switch
        {
            SaveGame.Models.ShipClass.ExtraLarge => Constants.Colours.Important,
            SaveGame.Models.ShipClass.Large => Constants.Colours.Important,
            SaveGame.Models.ShipClass.Medium => Constants.Colours.ImportantLess,
            _ => Constants.Colours.Unset
        };

    private static string GetComments(IShipModel source)
    {
        var returnValue = string.Empty;

        var cargoItems = source
                               .Cargo
                               .Where(x => x.Amount > 0)
                               .Select(x => $"{x.Name} ({x.Amount})");
        if (cargoItems.Any())
        {
            returnValue = string.Join(", ", cargoItems);
        }

        var modifications = source
                                  .Modifications
                                  .Select(x => $"{x.Type} ({x.Name})");
        if (modifications.Any())
        {
            var modificationsText = string.Join(", ", modifications);
            if (returnValue.Length > 0)
            {
                returnValue += $", {modificationsText}";
            }
            else
            {
                returnValue = modificationsText;
            }
        }

        return returnValue;
    }

    private static SpecialItemType GetType(IShipModel source)
        => source.Class switch
        {
            SaveGame.Models.ShipClass.ExtraLarge => SpecialItemType.ShipExtraLarge,
            SaveGame.Models.ShipClass.ExtraSmall => SpecialItemType.ShipExtraSmall,
            SaveGame.Models.ShipClass.Large => SpecialItemType.ShipLarge,
            SaveGame.Models.ShipClass.Medium => SpecialItemType.ShipMedium,
            SaveGame.Models.ShipClass.Small => SpecialItemType.ShipSmall,
            _ => SpecialItemType.Ship
        };

    public override Color Colour => _colour;

    public override string Comments => _comments;

    public override string Description => _description;

    public override SpecialItemType Type => _type;
}
