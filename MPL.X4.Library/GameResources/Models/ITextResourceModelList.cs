using System.Diagnostics.CodeAnalysis;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a list of text resource models.
/// </summary>
public interface ITextResourceModelList : IModelWithIdList<ITextResourceModel>
{
    /// <summary>
    /// Tries to get the value from the list with the specified <paramref name="reference"/>.
    /// </summary>
    /// <param name="reference">A <see cref="ITextResourceReference"/> containing the reference to get.</param>
    /// <param name="value">A nullable <see cref="ITextResourceModel"/> that will be set to the value, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    bool TryGetValue(ITextResourceReference reference, [NotNullWhen(true)] out ITextResourceModel? value);
}
