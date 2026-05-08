namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a sector name model.
/// </summary>
internal class SectorNameModel : ModelWithIdBase, ISectorNameModel
{
    /// <summary>
    /// Gets the default model.
    /// </summary>
    /// <returns>An <see cref="ISectorNameModel"/> that is the result.</returns>
    internal static ISectorNameModel GetDefault()
        => new SectorNameModel
        {
            Id = string.Empty,
            Name = string.Empty
        };

    public override string ToString()
        => $"{Name} - {Id}";

    public required string Name { get; init; }
}
