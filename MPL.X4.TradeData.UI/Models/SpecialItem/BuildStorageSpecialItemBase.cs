using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SpecialItem;

/// <summary>
/// A class that implements the base functionality for a special item for an <see cref="IBuildStorageModel"/>.
/// </summary>
/// <param name="sectorName">A <see cref="string"/> containing the name of the sector the item is in.</param>
/// <param name="source">An <see cref="IBuildStorageModel"/> that is the source of the special item.</param>
internal abstract class BuildStorageSpecialItemBase(
                                                    string sectorName,
                                                    IBuildStorageModel source)
    : SpecialItemBase(sectorName, source)
{
    private readonly string _comments = GetComments(source);

    private static string GetComments(IBuildStorageModel source)
    {
        var returnValue = "No cargo";

        var cargoItems = source
                               .Cargo
                               .Where(x => x.Amount > 0)
                               .Select(x => $"{x.Name} ({x.Amount})");
        if (cargoItems.Any())
        {
            returnValue = string.Join(", ", cargoItems);
        }

        return returnValue;
    }

    public override string Comments => _comments;

    public override SpecialItemType Type => SpecialItemType.BuildStorage;
}
