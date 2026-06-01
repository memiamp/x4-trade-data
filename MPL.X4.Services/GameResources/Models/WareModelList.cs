using System.Diagnostics.CodeAnalysis;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a list of ware models.
/// </summary>
internal class WareModelList : ModelWithIdListBase<IWareModel>, IWareModelList
{
    bool IWareModelList.TryGetValueByComponentReference(string componentReference, [NotNullWhen(true)] out IWareModel? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(componentReference);

        value = this.FirstOrDefault(x => x.ComponentReference == componentReference);

        return value is not null;
    }
}
