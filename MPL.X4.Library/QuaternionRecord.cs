using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPL.X4;

/// <summary>
/// A record that implements a quaternion.
/// </summary>
public readonly record struct QuaternionRecord(
                                               double W,
                                               double X,
                                               double Y,
                                               double Z)
    : IQuaternion
{
    /// <summary>
    /// Creates a new instance of the <see cref="QuaternionRecord"/> record by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IQuaternion"/> to copy values from.</param>
    public QuaternionRecord(IQuaternion source)
        : this(source.W, source.X, source.Y, source.Z)
    {
    }

    public override string ToString()
        => $"{X},{Y},{Z},{W}";
}
