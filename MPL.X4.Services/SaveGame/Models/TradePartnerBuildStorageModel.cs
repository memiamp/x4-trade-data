using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a build storage trade partner.
/// </summary>
/// <param name="tradePartner">An <see cref="IStationModel"/> that is the trade partner.</param>
internal class TradePartnerBuildStorageModel(
                                             IBuildStorageModel tradePartner)
    : TradePartnerModelBase<IBuildStorageModel>(tradePartner, TradePartnerType.BuildStorage)
{
    public override string Name => $"{TradePartner.Owner.Acronym} Abandoned Build Storage";
}
