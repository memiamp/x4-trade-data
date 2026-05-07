using MPL.X4.GameResources.Data;
using MPL.X4.GameResources.Models;

namespace MPL.X4;

/// <summary>
/// An interface that defines the behaviour of a model loader.
/// </summary>
public interface IModelLoader
{
    /// <summary>
    /// Loads faction models from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IFactionDataDictionary"/> that is the source data to be loaded.</param>
    /// <param name="textResources">An <see cref="ITextResourcePageDictionary"/> that are the text resources to use.</param>
    /// <param name="colourMap">An <see cref="IColourDataDictionary"/> that is the colour map to use.</param>
    /// <returns>An <see cref="IEnumerable{IFaction}"/> that is the result.</returns>
    IEnumerable<IFactionModel> LoadFactions(IFactionDataDictionary source, ITextResourcePageDictionary textResources, IColourDataDictionary colourMap);
}
