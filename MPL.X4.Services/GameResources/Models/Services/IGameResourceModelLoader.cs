using MPL.X4.GameResources.Data;

namespace MPL.X4.GameResources.Models.Services;

/// <summary>
/// An interface that defines the behaviour of a game resource model loader.
/// </summary>
public interface IGameResourceModelLoader
{
    /// <summary>
    /// Loads game resource models from the specified <paramref name="data"/>.
    /// </summary>
    /// <param name="data">An <see cref="IGameResourceData"/> to load models from.</param>
    /// <returns>An <see cref="IGameResourceModels"/> that is the result.</returns>
    IGameResourceModels LoadModels(IGameResourceData data);
}
