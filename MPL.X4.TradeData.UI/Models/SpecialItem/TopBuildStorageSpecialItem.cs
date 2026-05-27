using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SpecialItem;

/// <summary>
/// A class that implements a special item for a top-n <see cref="IBuildStorageModel"/>.
/// </summary>
/// <param name="sectorName">A <see cref="string"/> containing the name of the sector the item is in.</param>
/// <param name="source">An <see cref="IBuildStorageModel"/> that is the source of the special item.</param>
internal class TopBuildStorageSpecialItem(
                                          string sectorName,
                                          IBuildStorageModel source)
    : BuildStorageSpecialItemBase(sectorName, source)
{
    private readonly string _description = $"Top, {source.Cargo.Sum(x => x.Amount)}";

    public override string Description => _description;
}
