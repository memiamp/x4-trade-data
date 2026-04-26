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
            SpecialItemListView = new ListView();
            SpecialItemListView_ItemType = new ColumnHeader();
            SpecialItemListView_Description = new ColumnHeader();
            SpecialItemListView_Sector = new ColumnHeader();
            SpecialItemListView_PositionX = new ColumnHeader();
            SpecialItemListView_PositionY = new ColumnHeader();
            SpecialItemListView_PositionZ = new ColumnHeader();
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
            lblSpecialItems = new Label();
            SuspendLayout();
            // 
            // SpecialItemListView
            // 
            SpecialItemListView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SpecialItemListView.Columns.AddRange(new ColumnHeader[] { SpecialItemListView_ItemType, SpecialItemListView_Description, SpecialItemListView_Sector, SpecialItemListView_PositionX, SpecialItemListView_PositionY, SpecialItemListView_PositionZ });
            SpecialItemListView.FullRowSelect = true;
            SpecialItemListView.Location = new Point(12, 416);
            SpecialItemListView.Name = "SpecialItemListView";
            SpecialItemListView.Size = new Size(1056, 133);
            SpecialItemListView.TabIndex = 1;
            SpecialItemListView.UseCompatibleStateImageBehavior = false;
            SpecialItemListView.View = View.Details;
            // 
            // SpecialItemListView_ItemType
            // 
            SpecialItemListView_ItemType.Text = "Item Type";
            SpecialItemListView_ItemType.Width = 100;
            // 
            // SpecialItemListView_Description
            // 
            SpecialItemListView_Description.Text = "Description";
            SpecialItemListView_Description.Width = 220;
            // 
            // SpecialItemListView_Sector
            // 
            SpecialItemListView_Sector.Text = "Sector";
            SpecialItemListView_Sector.Width = 150;
            // 
            // SpecialItemListView_PositionX
            // 
            SpecialItemListView_PositionX.Text = "X";
            SpecialItemListView_PositionX.TextAlign = HorizontalAlignment.Right;
            SpecialItemListView_PositionX.Width = 100;
            // 
            // SpecialItemListView_PositionY
            // 
            SpecialItemListView_PositionY.Text = "Y";
            SpecialItemListView_PositionY.TextAlign = HorizontalAlignment.Right;
            SpecialItemListView_PositionY.Width = 100;
            // 
            // SpecialItemListView_PositionZ
            // 
            SpecialItemListView_PositionZ.Text = "Z";
            SpecialItemListView_PositionZ.TextAlign = HorizontalAlignment.Right;
            SpecialItemListView_PositionZ.Width = 100;
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
            // lblSpecialItems
            // 
            lblSpecialItems.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSpecialItems.AutoSize = true;
            lblSpecialItems.Location = new Point(12, 398);
            lblSpecialItems.Name = "lblSpecialItems";
            lblSpecialItems.Size = new Size(79, 15);
            lblSpecialItems.TabIndex = 10;
            lblSpecialItems.Text = "Special Items:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 561);
            Controls.Add(lblSpecialItems);
            Controls.Add(lblAverageSellPrice);
            Controls.Add(lblAverageBuyPrice);
            Controls.Add(lblTradeListSell);
            Controls.Add(lblTradeListBuy);
            Controls.Add(TradesListViewSell);
            Controls.Add(WareLabel);
            Controls.Add(WareComboBox);
            Controls.Add(TradesListViewBuy);
            Controls.Add(SpecialItemListView);
            MaximumSize = new Size(1096, 1000);
            MinimumSize = new Size(1096, 600);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView SpecialItemListView;
        private ColumnHeader SpecialItemListView_Description;
        private ColumnHeader SpecialItemListView_Sector;
        private ColumnHeader SpecialItemListView_PositionX;
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
        private Label lblSpecialItems;
        private ColumnHeader SpecialItemListView_PositionY;
        private ColumnHeader SpecialItemListView_PositionZ;
        private ColumnHeader SpecialItemListView_ItemType;
    }
}
