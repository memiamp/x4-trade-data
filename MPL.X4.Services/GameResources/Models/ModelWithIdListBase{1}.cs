using System.Diagnostics.CodeAnalysis;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements the base functionality of a list of <typeparamref name="TModel"/> models.
/// </summary>
/// <typeparam name="TModel">The type of the model, which must implement <see cref="IModelWithId"/>.</typeparam>
internal abstract class ModelWithIdListBase<TModel> : List<TModel>, IModelWithIdList<TModel>
    where TModel : IModelWithId
{
    /// <summary>
    /// Gets the default model.
    /// </summary>
    /// <returns>A <typeparamref name="TModel"/> that is the default model.</returns>
    private protected abstract TModel GetDefault();

    TModel IModelWithIdList<TModel>.GetValue(string id)
    {
        if (((IModelWithIdList<TModel>)this).TryGetValue(id, out var returnValue))
        {
            return returnValue;
        }

        throw new ArgumentException("A model with the specified identifier was not found", nameof(id));
    }

    TModel IModelWithIdList<TModel>.GetValueOrDefault(string? id)
    {
        if (!string.IsNullOrWhiteSpace(id) &&
            ((IModelWithIdList<TModel>)this).TryGetValue(id, out var returnValue))
        {
            return returnValue;
        }
        else if (this.Count > 0)
        {
            return this[0];
        }

        return GetDefault();
    }

    bool IModelWithIdList<TModel>.TryGetValue(string id, [NotNullWhen(true)] out TModel? value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

        value = this.FirstOrDefault(x => x.Id == id);

        return value is not null;
    }
}
