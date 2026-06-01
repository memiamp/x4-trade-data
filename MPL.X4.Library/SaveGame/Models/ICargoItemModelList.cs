namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a list of cargo item models.
/// </summary>
public interface ICargoItemModelList : IModelWithIdList<ICargoItemModel>
{
    /// <summary>
    /// Gets the total amount of the cargo items.
    /// </summary>
    int TotalAmount => this.Sum(x => x.Amount);

    /// <summary>
    /// Gets the total value of the cargo items.
    /// </summary>
    int TotalValue => this.Sum(x => x.Value);
}
