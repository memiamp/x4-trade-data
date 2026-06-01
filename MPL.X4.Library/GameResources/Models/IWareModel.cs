namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a ware model.
/// </summary>
public interface IWareModel : IModelWithId
{
    /// <summary>
    /// Gets the component reference for this ware.
    /// </summary>
    string? ComponentReference { get; }

    /// <summary>
    /// Gets the factory name of the ware (if any).
    /// </summary>
    string? FactoryName { get; }

    /// <summary>
    /// Gets the ware group.
    /// </summary>
    string Group { get; }

    /// <summary>
    /// Gets the name of the ware.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the average price for the ware.
    /// </summary>
    int PriceAverage { get; }

    /// <summary>
    /// Gets the maximum price for the ware.
    /// </summary>
    int PriceMaximum { get; }

    /// <summary>
    /// Gets the minimum price for the ware.
    /// </summary>
    int PriceMinimum { get; }

    /// <summary>
    /// Gets the transport mechanism of the ware.
    /// </summary>
    string Transport { get; }

    /// <summary>
    /// Gets the type of the ware.
    /// </summary>
    WareType Type { get; }

    /// <summary>
    /// Gets the volume of the ware.
    /// </summary>
    int Volume { get; }
}
