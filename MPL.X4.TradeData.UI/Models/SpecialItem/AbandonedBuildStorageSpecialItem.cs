using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SpecialItem;

/// <summary>
/// A class that implements a special item for an abandoned <see cref="IBuildStorageModel"/>.
/// </summary>
/// <param name="sectorName">A <see cref="string"/> containing the name of the sector the item is in.</param>
/// <param name="source">An <see cref="IBuildStorageModel"/> that is the source of the special item.</param>
internal class AbandonedBuildStorageSpecialItem(
                                                string sectorName,
                                                IBuildStorageModel source)
    : BuildStorageSpecialItemBase(sectorName, source)
{
    private readonly string _description = $"Abandoned - {source.Cargo.TotalValue:#,##0}c ({source.Owner.Acronym})";

    public override Color Colour => Constants.Colours.ImportantLess;

    public override string Description => _description;
}
