namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements the base behaviour of a data model that has a transform.
/// </summary>
internal class HasTransformDataBase : IHasTransform
{
    public override string ToString()
        => $"{Transform}";

    public required ITransform3D Transform { get; init; }
}
