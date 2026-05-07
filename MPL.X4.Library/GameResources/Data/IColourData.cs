namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines the data model of a colour.
/// </summary>
public interface IColourData
{
    /// <summary>
    /// Gets the alpha.
    /// </summary>
    int Alpha { get; }

    /// <summary>
    /// Gets the blue.
    /// </summary>
    int Blue { get; }

    /// <summary>
    /// Gets the glow.
    /// </summary>
    float Glow { get; }

    /// <summary>
    /// Gets the green.
    /// </summary>
    int Green { get; }

    /// <summary>
    /// Gets the identifier of the colour.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the red.
    /// </summary>
    int Red { get; }
}
