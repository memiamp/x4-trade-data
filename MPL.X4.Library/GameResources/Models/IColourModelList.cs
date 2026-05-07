using System.Diagnostics.CodeAnalysis;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a list of colour models.
/// </summary>
public interface IColourModelList : IList<IColourModel>
{
    /// <summary>
    /// Gets the value from the list with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">A <see cref="string"/> containing the identifier to get.</param>
    /// <returns>An <see cref="IColourModel"/> that is the result.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified <paramref name="id"/> does not exist.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or empty.</exception>
    IColourModel GetValue(string id);

    /// <summary>
    /// Gets the value from the list with the specified <paramref name="id"/>, or returns the default value.
    /// </summary>
    /// <param name="id">A <see cref="string"/> containing the identifier to get.</param>
    /// <returns>An <see cref="IColourModel"/> that is the result.</returns>
    IColourModel GetValueOrDefault(string? id);

    /// <summary>
    /// Tries to get the value from the list with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">A <see cref="string"/> containing the identifier to get.</param>
    /// <param name="value">A nullable <see cref="IColourModel"/> that will be set to the value, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or empty.</exception>
    bool TryGetValue(string id, [NotNullWhen(true)] out IColourModel? value);
}
