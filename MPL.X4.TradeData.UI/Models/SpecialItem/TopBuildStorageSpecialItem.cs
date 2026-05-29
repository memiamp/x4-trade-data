using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SpecialItem;

/// <summary>
/// A class that implements a special item for a top-n <see cref="IBuildStorageModel"/>.
/// </summary>
/// <param name="rank">An <see cref="int"/> indicating the rank of the item.</param>
/// <param name="sectorName">A <see cref="string"/> containing the name of the sector the item is in.</param>
/// <param name="source">An <see cref="IBuildStorageModel"/> that is the source of the special item.</param>
internal class TopBuildStorageSpecialItem(
                                          int rank,
                                          string sectorName,
                                          IBuildStorageModel source)
    : BuildStorageSpecialItemBase(sectorName, source)
{
    private readonly string _description = $"Top ({rank}) - {source.Cargo.TotalValue:#,##0}c ({source.Owner.Acronym})";

    public override string Description => _description;
}
