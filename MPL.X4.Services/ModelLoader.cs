using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.GameResources.Models;

namespace MPL.X4;

/// <summary>
/// A class that implements a model loader.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ModelLoader(
                           ILogger<ModelLoader> logger)
    : IModelLoader
{
    IEnumerable<IFactionModel> IModelLoader.LoadFactions(IFactionDataDictionary source, ITextResourcePageDictionary textResources, IColourDataDictionary colourMap)
    {
        List<IFactionModel> returnValue = [];

        foreach (var factionData in source.Values)
        {
            
        }

        return returnValue;
    }
}
