namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a list of colour models.
/// </summary>
public interface IColourModelList : IModelWithIdList<IColourModel>
{
    /// <summary>
    /// Gets the value from the list with the specified <paramref name="id"/>, or returns the default value.
    /// </summary>
    /// <param name="id">A <see cref="string"/> containing the identifier to get.</param>
    /// <returns>An <see cref="IColourModel"/> that is the result.</returns>
    IColourModel GetValueOrDefault(string? id);
}
