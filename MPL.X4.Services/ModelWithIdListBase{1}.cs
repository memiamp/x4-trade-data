using System.Diagnostics.CodeAnalysis;

namespace MPL.X4;

/// <summary>
/// A class that implements the base functionality of a list of <typeparamref name="TModel"/> models.
/// </summary>
/// <typeparam name="TModel">The type of the model, which must implement <see cref="IModelWithId"/>.</typeparam>
internal abstract class ModelWithIdListBase<TModel> : List<TModel>, IModelWithIdList<TModel>
    where TModel : IModelWithId
{
    TModel IModelWithIdList<TModel>.GetValue(string id)
    {
        if (((IModelWithIdList<TModel>)this).TryGetValue(id, out var returnValue))
        {
            return returnValue;
        }

        throw new ArgumentException("A model with the specified identifier was not found", nameof(id));
    }

    bool IModelWithIdList<TModel>.TryGetValue(string id, [NotNullWhen(true)] out TModel? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        value = this.FirstOrDefault(x => x.Id == id);

        return value is not null;
    }
}
