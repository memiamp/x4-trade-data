namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a gate.
/// </summary>
internal class GateModel : ModelWithIdBase, IGateModel
{
    public override string ToString()
        => $"{Type} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required bool IsKnown { get; init; }

    public required ITransform3D Transform { get; init; }

    public required GateType Type { get; init; }
}
