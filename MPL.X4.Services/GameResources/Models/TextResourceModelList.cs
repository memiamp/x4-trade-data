using System.Diagnostics.CodeAnalysis;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a list of text resource models.
/// </summary>
internal class TextResourceModelList : ModelWithIdListBase<ITextResourceModel>, ITextResourceModelList
{
    bool ITextResourceModelList.TryGetValue(ITextResourceReference reference, [NotNullWhen(true)] out ITextResourceModel? value)
    {
        var id = TextResourceModel.MakeId(reference);

        return ((ITextResourceModelList)this).TryGetValue(id, out value);
    }
}
