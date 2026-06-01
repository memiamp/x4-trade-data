using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SpecialItem;

/// <summary>
/// A class that implements a special item for an <see cref="ILockboxModel"/>.
/// </summary>
/// <param name="sectorName">A <see cref="string"/> containing the name of the sector the item is in.</param>
/// <param name="source">An <see cref="ILockboxModel"/> that is the source of the special item.</param>
internal class LockboxSpecialItem(
                                  string sectorName,
                                  ILockboxModel source)
    : SpecialItemBase(sectorName, source)
{
    private readonly string _comments = source.Wares.Any()
                                                           ? string.Join(", ", source.Wares)
                                                           : string.Empty;
    private readonly string _description = $"{source.Rarity}, {source.LockCount} lock(s)";

    public override Color Colour => Constants.Colours.Collectable;

    public override string Comments => _comments;
    
    public override string Description => _description;

    public override SpecialItemType Type => SpecialItemType.Lockbox;
}
