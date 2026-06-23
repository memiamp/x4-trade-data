using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// An interface that defines the behaviour of a mapper.
/// </summary>
internal interface IMapper
{
    /// <summary>
    /// Maps the specified <paramref name="source"/> to a textual equivalent.
    /// </summary>
    /// <param name="source">A <see cref="ModificationQuality"/> that is the source to be mapped.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    static string Map(ModificationQuality source)
        => source switch
        {
            ModificationQuality.Basic => Constants.ShipModifications.QualityBasic,
            ModificationQuality.Enhanced => Constants.ShipModifications.QualityEnhanced,
            ModificationQuality.Exceptional => Constants.ShipModifications.QualityExceptional,
            ModificationQuality.Paint => Constants.ShipModifications.QualityPaint,
            _ => string.Empty
        };

    /// <summary>
    /// Maps the specified <paramref name="source"/> to a textual equivalent.
    /// </summary>
    /// <param name="source">A <see cref="ShipClass"/> that is the source to be mapped.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    static string Map(ShipClass source)
        => source switch
        {
            ShipClass.ExtraLarge => Constants.ShipClass.ExtraLarge,
            ShipClass.ExtraSmall => Constants.ShipClass.ExtraSmall,
            ShipClass.Large => Constants.ShipClass.Large,
            ShipClass.Medium => Constants.ShipClass.Medium,
            ShipClass.Small => Constants.ShipClass.Small,
            _ => "Unknown"
        };
}
