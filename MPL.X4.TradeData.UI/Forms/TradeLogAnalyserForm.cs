using MPL.X4.GameResources.Models.Services;
using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Models;
using MPL.X4.TradeData.UI.Services;
using static MPL.X4.Constants.XmlDataFile.XPath;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements a form that analyses a trade log.
/// </summary>
internal partial class TradeLogAnalyserForm : Form
{
    #region Declarations

    private ITradeLogEntryModelList? _tradeLog;

    #endregion

    #region Constructors

    private TradeLogAnalyserForm()
    {
        InitializeComponent();
        Initialise();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TradeLogAnalyserForm"/> class with the specified parameters.
    /// </summary>
    /// <param name="tradeLog">A <see cref="ITradeLogEntryModelList"/> that is the trade log to analyse.</param>
    internal TradeLogAnalyserForm(
                                  ITradeLogEntryModelList tradeLog)

        : this()
    {
        _tradeLog = tradeLog;
    }

    #endregion

        #region Methods
        /*
        private static ListViewItem GenerateListViewItem(ICargoItemModel source)
        {
            var returnValue = new ListViewItem(source.Name);
            returnValue.SubItems.Add($"{source.Amount:#,##0}");
            returnValue.SubItems.Add($"{source.Value:#,##0}Cr");

            returnValue.Tag = source;

            return returnValue;
        }

        private static ListViewItem GenerateListViewItem(IModificationModel source)
        {
            var name = source.Name;
            if (name.Contains('('))
            {
                name = name[..name.IndexOf('(')].Trim();
            }
            var returnValue = new ListViewItem(source.Type.ToString());
            returnValue.SubItems.Add(name);
            returnValue.SubItems.Add(IMapper.Map(source.Quality));

            returnValue.Tag = source;

            return returnValue;
        }
        */
    private void Initialise()
    {
        Load += TradeLogAnalyserForm_Load;
        OkButton.Click += OkButton_Click;
    }

    private void LoadData()
    {
        if (_tradeLog?.Any() == true)
        {
            var buyerShips = _tradeLog
                                      .Where(x => x.Buyer.Type == TradePartnerType.Ship &&
                                                  x.Buyer.Owner.IsPlayerOwned())
                                      .Select(x => x.Buyer);
            var sellerShips = _tradeLog
                                       .Where(x => x.Seller.Type == TradePartnerType.Ship &&
                                                   x.Seller.Owner.IsPlayerOwned())
                                       .Select(x => x.Seller);
            var allShips = buyerShips
                                     .Concat(sellerShips)
                                     .Distinct()
                                     .ToList();

            // Process each ship
            foreach (var ship in allShips)
            {
                LoadTradeData(ship);

            }
        }
    }

    private void LoadTradeData(ITradePartnerModel source)
    {
        var buys = _tradeLog?
                             .Where(x => x.Buyer == source)
                             .OrderByDescending(x => x.TradeAge)
                             .ToList();
        var sells = _tradeLog?
                              .Where(x => x.Seller == source)
                              .OrderByDescending(x => x.TradeAge)
                              .ToList();

        if (buys?.Count > 0 &&
            sells?.Count > 0)
        {
            if (buys.Count == sells.Count)
            {
                Console.WriteLine("YO");
            }

            for (var i = 0; i < sells.Count; i++)
            {
                var sell = sells[i];
                var buy = buys[i];

                if (buy.Name == sell.Name &&
                    buy.Amount == sell.Amount)
                {
                    var t = string.Format("Bought {0} for {1} and sold for {2}. Profit: {3}", buy.Name, buy.Price, sell.Price, sell.Price - buy.Price);
                    Console.WriteLine(t);
                }
                else
                {
                    Console.WriteLine("HMM");
                }
            }
        }
        else
        {
            Console.WriteLine("HMM");
        }
    }

    /*
    private void LoadCargo(ICargoItemModelList items)
    {
        CargoListView.BeginUpdate();

        CargoListView.Items.Clear();

        foreach (var item in items.OrderBy(x => x.Name))
        {
            CargoListView.Items.Add(GenerateListViewItem(item));
        }

        CargoListView.EndUpdate();
    }

    private void LoadModifications(IShipModel ship)
    {
        ModificationsListView.BeginUpdate();

        ModificationsListView.Items.Clear();

        if (ship.EngineModification is not null)
            ModificationsListView.Items.Add(GenerateListViewItem(ship.EngineModification));

        if (ship.PaintModification is not null)
            ModificationsListView.Items.Add(GenerateListViewItem(ship.PaintModification));

        if (ship.ShieldModification is not null)
            ModificationsListView.Items.Add(GenerateListViewItem(ship.ShieldModification));

        if (ship.ShipModification is not null)
            ModificationsListView.Items.Add(GenerateListViewItem(ship.ShipModification));

        foreach (var item in ship.WeaponModifications.OrderBy(x => x.Name))
        {
            ModificationsListView.Items.Add(GenerateListViewItem(item));
        }

        ModificationsListView.EndUpdate();
    }
    */
    #endregion

    #region Event Handlers

    private void TradeLogAnalyserForm_Load(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void OkButton_Click(object? sender, EventArgs e)
    {
        Close();
    }

    #endregion
}
