namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a gate.
/// </summary>
public interface IGateModel : IHasIsKnown, IHasTransform, IModelWithId
{
    /// <summary>
    /// Gets the code of the gate.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the type of the gate.
    /// </summary>
    GateType Type { get; }
}
