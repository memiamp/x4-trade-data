using System.Numerics;

namespace MPL.X4;

/// <summary>
/// A class that implements a 3D transform.
/// </summary>
public class Transform3D() : ITransform3D
{
    public override string ToString()
        => $"{Position} {Rotation} {Quaternion}";

    public required IPosition3D Position { get; init; }

    public required IQuaternion Quaternion { get; init; }

    public required IRotation3D Rotation { get; init; }
}
