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
            lblTradeListSellTo = new Label();
            TradesListViewBuyFrom = new ListView();
            TradesListViewSell_Sector = new ColumnHeader();
            TradesListViewSell_Station = new ColumnHeader();
            TradesListViewSell_Amount = new ColumnHeader();
            TradesListViewSell_Price = new ColumnHeader();
            MainLayoutPanel = new TableLayoutPanel();
            BuyLabelPanel = new Panel();
            lblAverageBuyFromPrice = new Label();
            lblTradeListBuyFrom = new Label();
            SellLabelPanel = new Panel();
            WareLabel = new Label();
            WareComboBox = new ComboBox();
            lblAverageSellToPrice = new Label();
            TradesListViewSellTo = new ListView();
            TradesListViewBuy_Sector = new ColumnHeader();
            TradesListViewBuy_Station = new ColumnHeader();
            TradesListViewBuy_Amount = new ColumnHeader();
            TradesListViewBuy_Price = new ColumnHeader();
            lblNoItems = new Label();
            MainLayoutPanel.SuspendLayout();
            BuyLabelPanel.SuspendLayout();
            SellLabelPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTradeListSellTo
            // 
            lblTradeListSellTo.AutoSize = true;
            lblTradeListSellTo.Location = new Point(0, 26);
            lblTradeListSellTo.Name = "lblTradeListSellTo";
            lblTradeListSellTo.Size = new Size(101, 15);
            lblTradeListSellTo.TabIndex = 10;
            lblTradeListSellTo.Text = "Places to SELL TO:";
            // 
            // TradesListViewBuyFrom
            // 
            TradesListViewBuyFrom.Columns.AddRange(new ColumnHeader[] { TradesListViewSell_Sector, TradesListViewSell_Station, TradesListViewSell_Amount, TradesListViewSell_Price });
            TradesListViewBuyFrom.Dock = DockStyle.Fill;
            TradesListViewBuyFrom.Location = new Point(598, 47);
            TradesListViewBuyFrom.Name = "TradesListViewBuyFrom";
            TradesListViewBuyFrom.Size = new Size(590, 972);
            TradesListViewBuyFrom.TabIndex = 9;
            TradesListViewBuyFrom.UseCompatibleStateImageBehavior = false;
            TradesListViewBuyFrom.View = View.Details;
            // 
            // TradesListViewSell_Sector
            // 
            TradesListViewSell_Sector.Text = "Sector";
            TradesListViewSell_Sector.Width = 150;
            // 
            // TradesListViewSell_Station
            // 
            TradesListViewSell_Station.Text = "Station";
            TradesListViewSell_Station.Width = 150;
            // 
            // TradesListViewSell_Amount
            // 
            TradesListViewSell_Amount.Text = "Amount";
            TradesListViewSell_Amount.TextAlign = HorizontalAlignment.Right;
            TradesListViewSell_Amount.Width = 80;
            // 
            // TradesListViewSell_Price
            // 
            TradesListViewSell_Price.Text = "Price";
            TradesListViewSell_Price.TextAlign = HorizontalAlignment.Right;
            TradesListViewSell_Price.Width = 80;
            // 
            // MainLayoutPanel
            // 
            MainLayoutPanel.ColumnCount = 2;
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MainLayoutPanel.Controls.Add(BuyLabelPanel, 1, 0);
            MainLayoutPanel.Controls.Add(SellLabelPanel, 0, 0);
            MainLayoutPanel.Controls.Add(TradesListViewSellTo, 0, 1);
            MainLayoutPanel.Controls.Add(TradesListViewBuyFrom, 1, 1);
            MainLayoutPanel.Dock = DockStyle.Fill;
            MainLayoutPanel.Location = new Point(0, 0);
            MainLayoutPanel.Name = "MainLayoutPanel";
            MainLayoutPanel.RowCount = 2;
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            MainLayoutPanel.RowStyles.Add(new RowStyle());
            MainLayoutPanel.Size = new Size(1191, 1022);
            MainLayoutPanel.TabIndex = 12;
            // 
            // BuyLabelPanel
            // 
            BuyLabelPanel.Controls.Add(lblAverageBuyFromPrice);
            BuyLabelPanel.Controls.Add(lblTradeListBuyFrom);
            BuyLabelPanel.Dock = DockStyle.Fill;
            BuyLabelPanel.Location = new Point(598, 3);
            BuyLabelPanel.Name = "BuyLabelPanel";
            BuyLabelPanel.Size = new Size(590, 38);
            BuyLabelPanel.TabIndex = 11;
            // 
            // lblAverageBuyFromPrice
            // 
            lblAverageBuyFromPrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAverageBuyFromPrice.Location = new Point(260, 22);
            lblAverageBuyFromPrice.Name = "lblAverageBuyFromPrice";
            lblAverageBuyFromPrice.Size = new Size(225, 15);
            lblAverageBuyFromPrice.TabIndex = 13;
            lblAverageBuyFromPrice.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTradeListBuyFrom
            // 
            lblTradeListBuyFrom.AutoSize = true;
            lblTradeListBuyFrom.Location = new Point(0, 26);
            lblTradeListBuyFrom.Name = "lblTradeListBuyFrom";
            lblTradeListBuyFrom.Size = new Size(118, 15);
            lblTradeListBuyFrom.TabIndex = 12;
            lblTradeListBuyFrom.Text = "Places to BUY FROM:";
            // 
            // SellLabelPanel
            // 
            SellLabelPanel.Controls.Add(WareLabel);
            SellLabelPanel.Controls.Add(WareComboBox);
            SellLabelPanel.Controls.Add(lblAverageSellToPrice);
            SellLabelPanel.Controls.Add(lblTradeListSellTo);
            SellLabelPanel.Dock = DockStyle.Fill;
            SellLabelPanel.Location = new Point(3, 3);
            SellLabelPanel.Name = "SellLabelPanel";
            SellLabelPanel.Size = new Size(589, 38);
            SellLabelPanel.TabIndex = 10;
            // 
            // WareLabel
            // 
            WareLabel.AutoSize = true;
            WareLabel.Location = new Point(3, 3);
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
            // lblAverageSellToPrice
            // 
            lblAverageSellToPrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAverageSellToPrice.Location = new Point(260, 22);
            lblAverageSellToPrice.Name = "lblAverageSellToPrice";
            lblAverageSellToPrice.Size = new Size(225, 15);
            lblAverageSellToPrice.TabIndex = 11;
            lblAverageSellToPrice.Text = "SSS";
            lblAverageSellToPrice.TextAlign = ContentAlignment.TopRight;
            // 
            // TradesListViewSellTo
            // 
            TradesListViewSellTo.Columns.AddRange(new ColumnHeader[] { TradesListViewBuy_Sector, TradesListViewBuy_Station, TradesListViewBuy_Amount, TradesListViewBuy_Price });
            TradesListViewSellTo.Dock = DockStyle.Fill;
            TradesListViewSellTo.Location = new Point(3, 47);
            TradesListViewSellTo.Name = "TradesListViewSellTo";
            TradesListViewSellTo.Size = new Size(589, 972);
            TradesListViewSellTo.TabIndex = 12;
            TradesListViewSellTo.UseCompatibleStateImageBehavior = false;
            TradesListViewSellTo.View = View.Details;
            // 
            // TradesListViewBuy_Sector
            // 
            TradesListViewBuy_Sector.Text = "Sector";
            TradesListViewBuy_Sector.Width = 150;
            // 
            // TradesListViewBuy_Station
            // 
            TradesListViewBuy_Station.Text = "Station";
            TradesListViewBuy_Station.Width = 150;
            // 
            // TradesListViewBuy_Amount
            // 
            TradesListViewBuy_Amount.Text = "Amount";
            TradesListViewBuy_Amount.TextAlign = HorizontalAlignment.Right;
            TradesListViewBuy_Amount.Width = 80;
            // 
            // TradesListViewBuy_Price
            // 
            TradesListViewBuy_Price.Text = "Price";
            TradesListViewBuy_Price.TextAlign = HorizontalAlignment.Right;
            TradesListViewBuy_Price.Width = 80;
            // 
            // lblNoItems
            // 
            lblNoItems.Dock = DockStyle.Top;
            lblNoItems.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoItems.Location = new Point(0, 0);
            lblNoItems.Name = "lblNoItems";
            lblNoItems.Size = new Size(1191, 30);
            lblNoItems.TabIndex = 13;
            lblNoItems.Text = "No trades currently loaded.";
            lblNoItems.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TradeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblNoItems);
            Controls.Add(MainLayoutPanel);
            Name = "TradeControl";
            Size = new Size(1191, 1022);
            MainLayoutPanel.ResumeLayout(false);
            BuyLabelPanel.ResumeLayout(false);
            BuyLabelPanel.PerformLayout();
            SellLabelPanel.ResumeLayout(false);
            SellLabelPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblTradeListSellTo;
        private ListView TradesListViewBuyFrom;
        private ColumnHeader TradesListViewSell_Sector;
        private ColumnHeader TradesListViewSell_Station;
        private ColumnHeader TradesListViewSell_Amount;
        private ColumnHeader TradesListViewSell_Price;
        private TableLayoutPanel MainLayoutPanel;
        private Panel SellLabelPanel;
        private Label lblAverageSellToPrice;
        private Panel BuyLabelPanel;
        private Label lblTradeListBuyFrom;
        private Label lblAverageBuyFromPrice;
        private ListView TradesListViewSellTo;
        private ColumnHeader TradesListViewBuy_Sector;
        private ColumnHeader TradesListViewBuy_Station;
        private ColumnHeader TradesListViewBuy_Amount;
        private ColumnHeader TradesListViewBuy_Price;
        private Label WareLabel;
        private ComboBox WareComboBox;
        private Label lblNoItems;
    }
}
