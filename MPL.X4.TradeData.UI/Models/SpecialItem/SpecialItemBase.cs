using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SpecialItem;

/// <summary>
/// A class that implements the base functionality of a special item.
/// </summary>
/// <param name="sectorName">A <see cref="string"/> containing the name of the sector the item is in.</param>
/// <param name="source">An <see cref="IHasTransform"/> that is the source of the special item.</param>
internal abstract class SpecialItemBase(
                                        string sectorName,
                                        IHasTransform source)
    : ISpecialItem
{
    private readonly string _x = source.Transform.Position.X.ToString("0");
    private readonly string _y = source.Transform.Position.Y.ToString("0");
    private readonly string _z = source.Transform.Position.Z.ToString("0");

    public virtual Color Colour => SystemColors.Window;
 
    public abstract string Comments { get; }

    public abstract string Description { get; }

    string ISpecialItem.SectorName => sectorName;

    public abstract SpecialItemType Type { get; }

    string ISpecialItem.X => _x;

    string ISpecialItem.Y => _y;

    string ISpecialItem.Z => _z;
}
