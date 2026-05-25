namespace MPL.X4.TradeData.UI.Controls
{
    partial class TradePanelControl
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
            TradesListView = new ListView();
            TradesListView_Ware = new ColumnHeader();
            TradesListView_Sector = new ColumnHeader();
            TradesListView_Station = new ColumnHeader();
            TradesListView_Amount = new ColumnHeader();
            TradesListView_Price = new ColumnHeader();
            AveragePriceLabel = new Label();
            TradeListLabel = new Label();
            SuspendLayout();
            // 
            // TradesListView
            // 
            TradesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TradesListView.Columns.AddRange(new ColumnHeader[] { TradesListView_Ware, TradesListView_Sector, TradesListView_Station, TradesListView_Amount, TradesListView_Price });
            TradesListView.Location = new Point(0, 18);
            TradesListView.Name = "TradesListView";
            TradesListView.Size = new Size(643, 579);
            TradesListView.TabIndex = 15;
            TradesListView.UseCompatibleStateImageBehavior = false;
            TradesListView.View = View.Details;
            // 
            // TradesListView_Ware
            // 
            TradesListView_Ware.Text = "Ware";
            TradesListView_Ware.Width = 100;
            // 
            // TradesListView_Sector
            // 
            TradesListView_Sector.Text = "Sector";
            TradesListView_Sector.Width = 150;
            // 
            // TradesListView_Station
            // 
            TradesListView_Station.Text = "Station";
            TradesListView_Station.Width = 150;
            // 
            // TradesListView_Amount
            // 
            TradesListView_Amount.Text = "Amount";
            TradesListView_Amount.TextAlign = HorizontalAlignment.Right;
            TradesListView_Amount.Width = 80;
            // 
            // TradesListView_Price
            // 
            TradesListView_Price.Text = "Price";
            TradesListView_Price.TextAlign = HorizontalAlignment.Right;
            TradesListView_Price.Width = 80;
            // 
            // AveragePriceLabel
            // 
            AveragePriceLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AveragePriceLabel.Location = new Point(415, 0);
            AveragePriceLabel.Name = "AveragePriceLabel";
            AveragePriceLabel.Size = new Size(225, 15);
            AveragePriceLabel.TabIndex = 14;
            AveragePriceLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // TradeListLabel
            // 
            TradeListLabel.AutoSize = true;
            TradeListLabel.Location = new Point(0, 0);
            TradeListLabel.Name = "TradeListLabel";
            TradeListLabel.Size = new Size(59, 15);
            TradeListLabel.TabIndex = 13;
            TradeListLabel.Text = "Trade List:";
            // 
            // TradePanelControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TradesListView);
            Controls.Add(AveragePriceLabel);
            Controls.Add(TradeListLabel);
            Name = "TradePanelControl";
            Size = new Size(643, 597);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView TradesListView;
        private ColumnHeader TradesListView_Ware;
        private ColumnHeader TradesListView_Sector;
        private ColumnHeader TradesListView_Station;
        private ColumnHeader TradesListView_Amount;
        private ColumnHeader TradesListView_Price;
        private Label AveragePriceLabel;
        private Label TradeListLabel;
    }
}
