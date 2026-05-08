namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a list of colour models.
/// </summary>
internal class ColourModelList : ModelWithIdListBase<IColourModel>, IColourModelList
{
    private protected override IColourModel GetDefault()
        => ColourModel.GetDefault();
}
