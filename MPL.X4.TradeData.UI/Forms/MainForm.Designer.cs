namespace MPL.X4.TradeData.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AbandonedShipsListView = new ListView();
            AbandonedShipsListView_Class = new ColumnHeader();
            AbandonedShipsListView_Type = new ColumnHeader();
            AbandonedShipsListView_Sector = new ColumnHeader();
            AbandonedShipsListView_PositionX = new ColumnHeader();
            TradesListViewBuy = new ListView();
            TradesListViewBuy_Sector = new ColumnHeader();
            TradesListViewBuy_Station = new ColumnHeader();
            TradesListViewBuy_Amount = new ColumnHeader();
            TradesListViewBuy_Price = new ColumnHeader();
            WareComboBox = new ComboBox();
            WareLabel = new Label();
            TradesListViewSell = new ListView();
            TradesListViewSell_Sector = new ColumnHeader();
            TradesListViewSell_Station = new ColumnHeader();
            TradesListViewSell_Amount = new ColumnHeader();
            TradesListViewSell_Price = new ColumnHeader();
            lblTradeListBuy = new Label();
            lblTradeListSell = new Label();
            lblAverageBuyPrice = new Label();
            lblAverageSellPrice = new Label();
            lblAbandonedShips = new Label();
            AbandonedShipsListView_PositionY = new ColumnHeader();
            AbandonedShipsListView_PositionZ = new ColumnHeader();
            SuspendLayout();
            // 
            // AbandonedShipsListView
            // 
            AbandonedShipsListView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AbandonedShipsListView.Columns.AddRange(new ColumnHeader[] { AbandonedShipsListView_Class, AbandonedShipsListView_Type, AbandonedShipsListView_Sector, AbandonedShipsListView_PositionX, AbandonedShipsListView_PositionY, AbandonedShipsListView_PositionZ });
            AbandonedShipsListView.FullRowSelect = true;
            AbandonedShipsListView.Location = new Point(12, 416);
            AbandonedShipsListView.Name = "AbandonedShipsListView";
            AbandonedShipsListView.Size = new Size(1056, 133);
            AbandonedShipsListView.TabIndex = 1;
            AbandonedShipsListView.UseCompatibleStateImageBehavior = false;
            AbandonedShipsListView.View = View.Details;
            // 
            // AbandonedShipsListView_Class
            // 
            AbandonedShipsListView_Class.Text = "Class";
            AbandonedShipsListView_Class.Width = 100;
            // 
            // AbandonedShipsListView_Type
            // 
            AbandonedShipsListView_Type.Text = "Type";
            AbandonedShipsListView_Type.Width = 170;
            // 
            // AbandonedShipsListView_Sector
            // 
            AbandonedShipsListView_Sector.Text = "Sector";
            AbandonedShipsListView_Sector.Width = 150;
            // 
            // AbandonedShipsListView_PositionX
            // 
            AbandonedShipsListView_PositionX.Text = "X";
            AbandonedShipsListView_PositionX.TextAlign = HorizontalAlignment.Right;
            AbandonedShipsListView_PositionX.Width = 100;
            // 
            // TradesListViewBuy
            // 
            TradesListViewBuy.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TradesListViewBuy.Columns.AddRange(new ColumnHeader[] { TradesListViewBuy_Sector, TradesListViewBuy_Station, TradesListViewBuy_Amount, TradesListViewBuy_Price });
            TradesListViewBuy.Location = new Point(12, 56);
            TradesListViewBuy.Name = "TradesListViewBuy";
            TradesListViewBuy.Size = new Size(525, 339);
            TradesListViewBuy.TabIndex = 2;
            TradesListViewBuy.UseCompatibleStateImageBehavior = false;
            TradesListViewBuy.View = View.Details;
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
            // WareComboBox
            // 
            WareComboBox.FormattingEnabled = true;
            WareComboBox.Location = new Point(55, 12);
            WareComboBox.Name = "WareComboBox";
            WareComboBox.Size = new Size(198, 23);
            WareComboBox.TabIndex = 3;
            // 
            // WareLabel
            // 
            WareLabel.AutoSize = true;
            WareLabel.Location = new Point(12, 15);
            WareLabel.Name = "WareLabel";
            WareLabel.Size = new Size(37, 15);
            WareLabel.TabIndex = 4;
            WareLabel.Text = "Ware:";
            // 
            // TradesListViewSell
            // 
            TradesListViewSell.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TradesListViewSell.Columns.AddRange(new ColumnHeader[] { TradesListViewSell_Sector, TradesListViewSell_Station, TradesListViewSell_Amount, TradesListViewSell_Price });
            TradesListViewSell.Location = new Point(543, 56);
            TradesListViewSell.Name = "TradesListViewSell";
            TradesListViewSell.Size = new Size(525, 339);
            TradesListViewSell.TabIndex = 5;
            TradesListViewSell.UseCompatibleStateImageBehavior = false;
            TradesListViewSell.View = View.Details;
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
            // lblTradeListBuy
            // 
            lblTradeListBuy.AutoSize = true;
            lblTradeListBuy.Location = new Point(12, 38);
            lblTradeListBuy.Name = "lblTradeListBuy";
            lblTradeListBuy.Size = new Size(101, 15);
            lblTradeListBuy.TabIndex = 6;
            lblTradeListBuy.Text = "Places to SELL TO:";
            // 
            // lblTradeListSell
            // 
            lblTradeListSell.AutoSize = true;
            lblTradeListSell.Location = new Point(543, 38);
            lblTradeListSell.Name = "lblTradeListSell";
            lblTradeListSell.Size = new Size(118, 15);
            lblTradeListSell.TabIndex = 7;
            lblTradeListSell.Text = "Places to BUY FROM:";
            // 
            // lblAverageBuyPrice
            // 
            lblAverageBuyPrice.AutoSize = true;
            lblAverageBuyPrice.Location = new Point(162, 38);
            lblAverageBuyPrice.Name = "lblAverageBuyPrice";
            lblAverageBuyPrice.Size = new Size(0, 15);
            lblAverageBuyPrice.TabIndex = 8;
            // 
            // lblAverageSellPrice
            // 
            lblAverageSellPrice.AutoSize = true;
            lblAverageSellPrice.Location = new Point(691, 38);
            lblAverageSellPrice.Name = "lblAverageSellPrice";
            lblAverageSellPrice.Size = new Size(0, 15);
            lblAverageSellPrice.TabIndex = 9;
            // 
            // lblAbandonedShips
            // 
            lblAbandonedShips.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAbandonedShips.AutoSize = true;
            lblAbandonedShips.Location = new Point(12, 398);
            lblAbandonedShips.Name = "lblAbandonedShips";
            lblAbandonedShips.Size = new Size(103, 15);
            lblAbandonedShips.TabIndex = 10;
            lblAbandonedShips.Text = "Abandoned Ships:";
            // 
            // AbandonedShipsListView_PositionY
            // 
            AbandonedShipsListView_PositionY.Text = "Y";
            AbandonedShipsListView_PositionY.TextAlign = HorizontalAlignment.Right;
            AbandonedShipsListView_PositionY.Width = 100;
            // 
            // AbandonedShipsListView_PositionZ
            // 
            AbandonedShipsListView_PositionZ.Text = "Z";
            AbandonedShipsListView_PositionZ.TextAlign = HorizontalAlignment.Right;
            AbandonedShipsListView_PositionZ.Width = 100;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 561);
            Controls.Add(lblAbandonedShips);
            Controls.Add(lblAverageSellPrice);
            Controls.Add(lblAverageBuyPrice);
            Controls.Add(lblTradeListSell);
            Controls.Add(lblTradeListBuy);
            Controls.Add(TradesListViewSell);
            Controls.Add(WareLabel);
            Controls.Add(WareComboBox);
            Controls.Add(TradesListViewBuy);
            Controls.Add(AbandonedShipsListView);
            MaximumSize = new Size(1096, 1000);
            MinimumSize = new Size(1096, 600);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView AbandonedShipsListView;
        private ColumnHeader AbandonedShipsListView_Class;
        private ColumnHeader AbandonedShipsListView_Type;
        private ColumnHeader AbandonedShipsListView_Sector;
        private ColumnHeader AbandonedShipsListView_PositionX;
        private ListView TradesListViewBuy;
        private ComboBox WareComboBox;
        private Label WareLabel;
        private ColumnHeader TradesListViewBuy_Sector;
        private ColumnHeader TradesListViewBuy_Station;
        private ColumnHeader TradesListViewBuy_Price;
        private ColumnHeader TradesListViewBuy_Amount;
        private ListView TradesListViewSell;
        private ColumnHeader TradesListViewSell_Sector;
        private ColumnHeader TradesListViewSell_Station;
        private ColumnHeader TradesListViewSell_Price;
        private ColumnHeader TradesListViewSell_Amount;
        private Label lblTradeListBuy;
        private Label lblTradeListSell;
        private Label lblAverageBuyPrice;
        private Label lblAverageSellPrice;
        private Label lblAbandonedShips;
        private ColumnHeader AbandonedShipsListView_PositionY;
        private ColumnHeader AbandonedShipsListView_PositionZ;
    }
}
