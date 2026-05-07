using System.Diagnostics.CodeAnalysis;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a list of colour models.
/// </summary>
internal class ColourModelList : List<IColourModel>, IColourModelList
{
    IColourModel IColourModelList.GetValue(string id)
    {
        if (((IColourModelList)this).TryGetValue(id, out var returnValue))
        {
            return returnValue;
        }

        throw new ArgumentException("A colour model with the specified identifier was not found", nameof(id));
    }

    IColourModel IColourModelList.GetValueOrDefault(string? id)
    {
        if (!string.IsNullOrWhiteSpace(id) &&
            ((IColourModelList)this).TryGetValue(id, out var returnValue))
        {
            return returnValue;
        }
        else if (this.Count > 0)
        {
            return this[0];
        }

        return ColourModel.GetDefault();
    }

    bool IColourModelList.TryGetValue(string id, [NotNullWhen(true)] out IColourModel? value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

        value = this.FirstOrDefault(x => x.Id == id);

        return value is not null;
    }
}
