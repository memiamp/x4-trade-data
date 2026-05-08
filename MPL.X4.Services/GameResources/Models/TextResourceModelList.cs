namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a list of text resource models.
/// </summary>
internal class TextResourceModelList : ModelWithIdListBase<ITextResourceModel>, ITextResourceModelList
{
    private protected override ITextResourceModel GetDefault()
        => TextResourceModel.GetDefault();
}
