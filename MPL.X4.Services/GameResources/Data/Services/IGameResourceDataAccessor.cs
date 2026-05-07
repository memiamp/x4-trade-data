namespace MPL.X4.GameResources.Data.Services;

/// <summary>
/// An interface that defines the behaviour of an accessor forgame resource data.
/// </summary>
internal interface IGameResourceDataAccessor
{
    /// <summary>
    /// Gets or sets the game resource data instance.
    /// </summary>
    IGameResourceData Instance { get; set; }
}
