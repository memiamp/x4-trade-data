namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a list of colour models.
/// </summary>
internal class ColourModelList : ModelWithIdListBase<IColourModel>, IColourModelList
{
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
}
