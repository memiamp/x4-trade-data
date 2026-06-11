namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a build storage.
/// </summary>
internal class BuildStorageModel : StationModelBase, IBuildStorageModel
{
    public override string ToString()
        => $"{Owner} - IsKnown {IsKnown} - {Id}";
}
