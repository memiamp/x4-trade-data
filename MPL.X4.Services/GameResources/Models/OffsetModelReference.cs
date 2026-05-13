using System.Xml.Linq;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a offset model reference.
/// </summary>
internal class OffsetModelReference : IOffsetModelReference
{
    public override string ToString()
        => $"Sectors: {Sectors.Count} Zones: {Zones.Count} Other: {Other.Count}";

    public required IDictionary<string, IOffsetModelList> Other { get; init; }

    public required IOffsetModelList Sectors { get; init; }

    public required IOffsetModelList Zones { get; init; }
}
