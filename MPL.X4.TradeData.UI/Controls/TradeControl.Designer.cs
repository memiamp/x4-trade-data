namespace MPL.X4.TradeData.UI.Controls
{
    partial class TradeControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TradeListSellToLabel = new Label();
            TradesListViewBuyFrom = new ListView();
            TradesListViewBuyFrom_Ware = new ColumnHeader();
            TradesListViewBuyFrom_Sector = new ColumnHeader();
            TradesListViewBuyFrom_Station = new ColumnHeader();
            TradesListViewBuyFrom_Amount = new ColumnHeader();
            TradesListViewBuyFrom_Price = new ColumnHeader();
            TradeLayoutPanel = new TableLayoutPanel();
            BuyLabelPanel = new Panel();
            AverageBuyFromPriceLabel = new Label();
            TradeListBuyFromLabel = new Label();
            SellLabelPanel = new Panel();
            AverageSellToPriceLabel = new Label();
            TradesListViewSellTo = new ListView();
            TradesListViewSellTo_Ware = new ColumnHeader();
            TradesListViewSellTo_Sector = new ColumnHeader();
            TradesListViewSellTo_Station = new ColumnHeader();
            TradesListViewSellTo_Amount = new ColumnHeader();
            TradesListViewSellTo_Price = new ColumnHeader();
            SectorOwnerLabel = new Label();
            SectorOwnerComboBox = new ComboBox();
            WareLabel = new Label();
            WareComboBox = new ComboBox();
            NoItemsLabel = new Label();
            MainLayoutPanel = new TableLayoutPanel();
            FilterPanel = new Panel();
            StationOwnerLabel = new Label();
            StationOwnerComboBox = new ComboBox();
            TradeLayoutPanel.SuspendLayout();
            BuyLabelPanel.SuspendLayout();
            SellLabelPanel.SuspendLayout();
            MainLayoutPanel.SuspendLayout();
            FilterPanel.SuspendLayout();
            SuspendLayout();
            // 
            // TradeListSellToLabel
            // 
            TradeListSellToLabel.AutoSize = true;
            TradeListSellToLabel.Location = new Point(0, 0);
            TradeListSellToLabel.Name = "TradeListSellToLabel";
            TradeListSellToLabel.Size = new Size(101, 15);
            TradeListSellToLabel.TabIndex = 10;
            TradeListSellToLabel.Text = "Places to SELL TO:";
            // 
            // TradesListViewBuyFrom
            // 
            TradesListViewBuyFrom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TradesListViewBuyFrom.Columns.AddRange(new ColumnHeader[] { TradesListViewBuyFrom_Ware, TradesListViewBuyFrom_Sector, TradesListViewBuyFrom_Station, TradesListViewBuyFrom_Amount, TradesListViewBuyFrom_Price });
            TradesListViewBuyFrom.Location = new Point(595, 25);
            TradesListViewBuyFrom.Name = "TradesListViewBuyFrom";
            TradesListViewBuyFrom.Size = new Size(587, 958);
            TradesListViewBuyFrom.TabIndex = 9;
            TradesListViewBuyFrom.UseCompatibleStateImageBehavior = false;
            TradesListViewBuyFrom.View = View.Details;
            // 
            // TradesListViewBuyFrom_Ware
            // 
            TradesListViewBuyFrom_Ware.Text = "Ware";
            TradesListViewBuyFrom_Ware.Width = 100;
            // 
            // TradesListViewBuyFrom_Sector
            // 
            TradesListViewBuyFrom_Sector.Text = "Sector";
            TradesListViewBuyFrom_Sector.Width = 150;
            // 
            // TradesListViewBuyFrom_Station
            // 
            TradesListViewBuyFrom_Station.Text = "Station";
            TradesListViewBuyFrom_Station.Width = 150;
            // 
            // TradesListViewBuyFrom_Amount
            // 
            TradesListViewBuyFrom_Amount.Text = "Amount";
            TradesListViewBuyFrom_Amount.TextAlign = HorizontalAlignment.Right;
            TradesListViewBuyFrom_Amount.Width = 80;
            // 
            // TradesListViewBuyFrom_Price
            // 
            TradesListViewBuyFrom_Price.Text = "Price";
            TradesListViewBuyFrom_Price.TextAlign = HorizontalAlignment.Right;
            TradesListViewBuyFrom_Price.Width = 80;
            // 
            // TradeLayoutPanel
            // 
            TradeLayoutPanel.ColumnCount = 2;
            TradeLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TradeLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TradeLayoutPanel.Controls.Add(BuyLabelPanel, 1, 0);
            TradeLayoutPanel.Controls.Add(SellLabelPanel, 0, 0);
            TradeLayoutPanel.Controls.Add(TradesListViewSellTo, 0, 1);
            TradeLayoutPanel.Controls.Add(TradesListViewBuyFrom, 1, 1);
            TradeLayoutPanel.Dock = DockStyle.Fill;
            TradeLayoutPanel.Location = new Point(3, 33);
            TradeLayoutPanel.Name = "TradeLayoutPanel";
            TradeLayoutPanel.RowCount = 2;
            TradeLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            TradeLayoutPanel.RowStyles.Add(new RowStyle());
            TradeLayoutPanel.Size = new Size(1185, 986);
            TradeLayoutPanel.TabIndex = 12;
            // 
            // BuyLabelPanel
            // 
            BuyLabelPanel.Controls.Add(AverageBuyFromPriceLabel);
            BuyLabelPanel.Controls.Add(TradeListBuyFromLabel);
            BuyLabelPanel.Dock = DockStyle.Fill;
            BuyLabelPanel.Location = new Point(595, 3);
            BuyLabelPanel.Name = "BuyLabelPanel";
            BuyLabelPanel.Size = new Size(587, 16);
            BuyLabelPanel.TabIndex = 11;
            // 
            // AverageBuyFromPriceLabel
            // 
            AverageBuyFromPriceLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AverageBuyFromPriceLabel.Location = new Point(257, 0);
            AverageBuyFromPriceLabel.Name = "AverageBuyFromPriceLabel";
            AverageBuyFromPriceLabel.Size = new Size(225, 15);
            AverageBuyFromPriceLabel.TabIndex = 13;
            AverageBuyFromPriceLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TradeListBuyFromLabel
            // 
            TradeListBuyFromLabel.AutoSize = true;
            TradeListBuyFromLabel.Location = new Point(0, 0);
            TradeListBuyFromLabel.Name = "TradeListBuyFromLabel";
            TradeListBuyFromLabel.Size = new Size(118, 15);
            TradeListBuyFromLabel.TabIndex = 12;
            TradeListBuyFromLabel.Text = "Places to BUY FROM:";
            // 
            // SellLabelPanel
            // 
            SellLabelPanel.Controls.Add(AverageSellToPriceLabel);
            SellLabelPanel.Controls.Add(TradeListSellToLabel);
            SellLabelPanel.Dock = DockStyle.Fill;
            SellLabelPanel.Location = new Point(3, 3);
            SellLabelPanel.Name = "SellLabelPanel";
            SellLabelPanel.Size = new Size(586, 16);
            SellLabelPanel.TabIndex = 10;
            // 
            // AverageSellToPriceLabel
            // 
            AverageSellToPriceLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AverageSellToPriceLabel.Location = new Point(257, 0);
            AverageSellToPriceLabel.Name = "AverageSellToPriceLabel";
            AverageSellToPriceLabel.Size = new Size(225, 15);
            AverageSellToPriceLabel.TabIndex = 11;
            AverageSellToPriceLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // TradesListViewSellTo
            // 
            TradesListViewSellTo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TradesListViewSellTo.Columns.AddRange(new ColumnHeader[] { TradesListViewSellTo_Ware, TradesListViewSellTo_Sector, TradesListViewSellTo_Station, TradesListViewSellTo_Amount, TradesListViewSellTo_Price });
            TradesListViewSellTo.Location = new Point(3, 25);
            TradesListViewSellTo.Name = "TradesListViewSellTo";
            TradesListViewSellTo.Size = new Size(586, 958);
            TradesListViewSellTo.TabIndex = 12;
            TradesListViewSellTo.UseCompatibleStateImageBehavior = false;
            TradesListViewSellTo.View = View.Details;
            // 
            // TradesListViewSellTo_Ware
            // 
            TradesListViewSellTo_Ware.Text = "Ware";
            TradesListViewSellTo_Ware.Width = 100;
            // 
            // TradesListViewSellTo_Sector
            // 
            TradesListViewSellTo_Sector.Text = "Sector";
            TradesListViewSellTo_Sector.Width = 150;
            // 
            // TradesListViewSellTo_Station
            // 
            TradesListViewSellTo_Station.Text = "Station";
            TradesListViewSellTo_Station.Width = 150;
            // 
            // TradesListViewSellTo_Amount
            // 
            TradesListViewSellTo_Amount.Text = "Amount";
            TradesListViewSellTo_Amount.TextAlign = HorizontalAlignment.Right;
            TradesListViewSellTo_Amount.Width = 80;
            // 
            // TradesListViewSellTo_Price
            // 
            TradesListViewSellTo_Price.Text = "Price";
            TradesListViewSellTo_Price.TextAlign = HorizontalAlignment.Right;
            TradesListViewSellTo_Price.Width = 80;
            // 
            // SectorOwnerLabel
            // 
            SectorOwnerLabel.AutoSize = true;
            SectorOwnerLabel.Location = new Point(278, 3);
            SectorOwnerLabel.Name = "SectorOwnerLabel";
            SectorOwnerLabel.Size = new Size(81, 15);
            SectorOwnerLabel.TabIndex = 15;
            SectorOwnerLabel.Text = "Sector Owner:";
            // 
            // SectorOwnerComboBox
            // 
            SectorOwnerComboBox.FormattingEnabled = true;
            SectorOwnerComboBox.Location = new Point(360, 0);
            SectorOwnerComboBox.Name = "SectorOwnerComboBox";
            SectorOwnerComboBox.Size = new Size(229, 23);
            SectorOwnerComboBox.TabIndex = 14;
            // 
            // WareLabel
            // 
            WareLabel.AutoSize = true;
            WareLabel.Location = new Point(0, 3);
            WareLabel.Name = "WareLabel";
            WareLabel.Size = new Size(37, 15);
            WareLabel.TabIndex = 13;
            WareLabel.Text = "Ware:";
            // 
            // WareComboBox
            // 
            WareComboBox.FormattingEnabled = true;
            WareComboBox.Location = new Point(43, 0);
            WareComboBox.Name = "WareComboBox";
            WareComboBox.Size = new Size(229, 23);
            WareComboBox.TabIndex = 12;
            // 
            // NoItemsLabel
            // 
            NoItemsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NoItemsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NoItemsLabel.Location = new Point(0, 0);
            NoItemsLabel.Name = "NoItemsLabel";
            NoItemsLabel.Size = new Size(1191, 30);
            NoItemsLabel.TabIndex = 13;
            NoItemsLabel.Text = "No trades are currently loaded.";
            NoItemsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainLayoutPanel
            // 
            MainLayoutPanel.ColumnCount = 1;
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            MainLayoutPanel.Controls.Add(FilterPanel, 0, 0);
            MainLayoutPanel.Controls.Add(TradeLayoutPanel, 0, 1);
            MainLayoutPanel.Dock = DockStyle.Fill;
            MainLayoutPanel.Location = new Point(0, 0);
            MainLayoutPanel.Name = "MainLayoutPanel";
            MainLayoutPanel.RowCount = 2;
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            MainLayoutPanel.RowStyles.Add(new RowStyle());
            MainLayoutPanel.Size = new Size(1191, 1022);
            MainLayoutPanel.TabIndex = 14;
            // 
            // FilterPanel
            // 
            FilterPanel.Controls.Add(StationOwnerLabel);
            FilterPanel.Controls.Add(StationOwnerComboBox);
            FilterPanel.Controls.Add(SectorOwnerLabel);
            FilterPanel.Controls.Add(WareComboBox);
            FilterPanel.Controls.Add(SectorOwnerComboBox);
            FilterPanel.Controls.Add(WareLabel);
            FilterPanel.Dock = DockStyle.Fill;
            FilterPanel.Location = new Point(3, 3);
            FilterPanel.Name = "FilterPanel";
            FilterPanel.Size = new Size(1185, 24);
            FilterPanel.TabIndex = 16;
            // 
            // StationOwnerLabel
            // 
            StationOwnerLabel.AutoSize = true;
            StationOwnerLabel.Location = new Point(601, 3);
            StationOwnerLabel.Name = "StationOwnerLabel";
            StationOwnerLabel.Size = new Size(85, 15);
            StationOwnerLabel.TabIndex = 17;
            StationOwnerLabel.Text = "Station Owner:";
            // 
            // StationOwnerComboBox
            // 
            StationOwnerComboBox.FormattingEnabled = true;
            StationOwnerComboBox.Location = new Point(692, 0);
            StationOwnerComboBox.Name = "StationOwnerComboBox";
            StationOwnerComboBox.Size = new Size(229, 23);
            StationOwnerComboBox.TabIndex = 16;
            // 
            // TradeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainLayoutPanel);
            Controls.Add(NoItemsLabel);
            Name = "TradeControl";
            Size = new Size(1191, 1022);
            TradeLayoutPanel.ResumeLayout(false);
            BuyLabelPanel.ResumeLayout(false);
            BuyLabelPanel.PerformLayout();
            SellLabelPanel.ResumeLayout(false);
            SellLabelPanel.PerformLayout();
            MainLayoutPanel.ResumeLayout(false);
            FilterPanel.ResumeLayout(false);
            FilterPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label TradeListSellToLabel;
        private ListView TradesListViewBuyFrom;
        private ColumnHeader TradesListViewBuyFrom_Sector;
        private ColumnHeader TradesListViewBuyFrom_Station;
        private ColumnHeader TradesListViewBuyFrom_Amount;
        private ColumnHeader TradesListViewBuyFrom_Price;
        private TableLayoutPanel TradeLayoutPanel;
        private Panel SellLabelPanel;
        private Label AverageSellToPriceLabel;
        private Panel BuyLabelPanel;
        private Label TradeListBuyFromLabel;
        private Label AverageBuyFromPriceLabel;
        private ListView TradesListViewSellTo;
        private ColumnHeader TradesListViewSellTo_Sector;
        private ColumnHeader TradesListViewSellTo_Station;
        private ColumnHeader TradesListViewSellTo_Amount;
        private ColumnHeader TradesListViewSellTo_Price;
        private Label WareLabel;
        private ComboBox WareComboBox;
        private Label NoItemsLabel;
        private Label SectorOwnerLabel;
        private ComboBox SectorOwnerComboBox;
        private TableLayoutPanel MainLayoutPanel;
        private Panel FilterPanel;
        private Label StationOwnerLabel;
        private ComboBox StationOwnerComboBox;
        private ColumnHeader TradesListViewSellTo_Ware;
        private ColumnHeader TradesListViewBuyFrom_Ware;
    }
}
