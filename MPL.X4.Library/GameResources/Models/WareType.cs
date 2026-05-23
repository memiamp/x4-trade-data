namespace MPL.X4.GameResources.Models;

/// <summary>
/// An enumeration that defines the type of a ware.
/// </summary>
public enum WareType
{
    /// <summary>
    /// The ware is another type.
    /// </summary>
    Other = 0,

    /// <summary>
    /// The ware is a container.
    /// </summary>
    Container,

    /// <summary>
    /// The ware is an equipment.
    /// </summary>
    Equipment,

    /// <summary>
    /// The ware is an inventory item.
    /// </summary>
    Inventory,

    /// <summary>
    /// The ware is a liquid.
    /// </summary>
    Liquid,

    /// <summary>
    /// The ware is a ship.
    /// </summary>
    Ship,

    /// <summary>
    /// The ware is a solid.
    /// </summary>
    Solid
}
