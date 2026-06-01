namespace MPL.X4;

/// <summary>
/// A class that implements the base behaviour of a model that has a transform.
/// </summary>
internal class HasTransformBase : IHasTransform
{
    public override string ToString()
        => $"{Transform}";

    public required ITransform3D Transform { get; init; }
}
