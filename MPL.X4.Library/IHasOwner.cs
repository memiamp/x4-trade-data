using MPL.X4.GameResources.Models;

namespace MPL.X4;

/// <summary>
/// An interface that defines an element that has an owner.
/// </summary>
public interface IHasOwner
{
    /// <summary>
    /// Gets the owner of the item.
    /// </summary>
    IFactionModel Owner { get; }
}
