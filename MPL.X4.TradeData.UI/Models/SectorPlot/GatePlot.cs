//namespace MPL.X4.TradeData.UI.Models.SectorPlot;

///// <summary>
///// A class that implements a sector plot for a <see cref="IGate"/>.
///// </summary>
///// <param name="item">An <see cref="IGate"/> to be plotted.</param>
//internal class GatePlot(
//                        IGate item)
//    : ISectorPlot
//{
//    string? ISectorPlot.ColourId => "holomap_jumpgate";

//    string ISectorPlot.Name => $"{item.Code}";

//    SectorPlotType ISectorPlot.Type => SectorPlotType.JumpGate;

//    double ISectorPlot.X => item.Position.X;

//    double ISectorPlot.Y => item.Position.Y;

//    double ISectorPlot.Z => item.Position.Z;
//}
