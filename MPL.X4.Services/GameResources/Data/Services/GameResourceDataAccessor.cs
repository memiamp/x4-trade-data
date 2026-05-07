namespace MPL.X4.GameResources.Data.Services;

/// <summary>
/// A class that implements a game resource data accessor.
/// </summary>
internal class GameResourceDataAccessor : IGameResourceDataAccessor
{
    public required IGameResourceData Instance { get; set; }
}
