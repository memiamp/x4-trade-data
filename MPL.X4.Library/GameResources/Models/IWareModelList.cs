using System.Diagnostics.CodeAnalysis;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a list of ware models.
/// </summary>
public interface IWareModelList : IModelWithIdList<IWareModel>
{
    /// <summary>
    /// Tries to get the ware from the list with the specified <paramref name="componentReference"/>.
    /// </summary>
    /// <param name="componentReference">A <see cref="string"/> containing the component reference.</param>
    /// <param name="value">A nullable <see cref="IWareModel"/> that will be set to the value, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="componentReference"/> is null or empty.</exception>
    bool TryGetValueByComponentReference(string componentReference, [NotNullWhen(true)] out IWareModel? value);
}
