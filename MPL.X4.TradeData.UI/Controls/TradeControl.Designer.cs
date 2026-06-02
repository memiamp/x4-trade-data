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
            BuyTradePanel = new TradePanelControl();
            SellTradePanel = new TradePanelControl();
            NoItemsLabel = new Label();
            MainLayoutPanel = new TableLayoutPanel();
            TopPanel = new Panel();
            SectorNameLabel = new Label();
            SectorNameComboBox = new ComboBox();
            StationOwnerLabel = new Label();
            StationOwnerComboBox = new ComboBox();
            SectorOwnerLabel = new Label();
            WareComboBox = new ComboBox();
            SectorOwnerComboBox = new ComboBox();
            WareLabel = new Label();
            MainLayoutPanel.SuspendLayout();
            TopPanel.SuspendLayout();
            SuspendLayout();
            // 
            // BuyTradePanel
            // 
            BuyTradePanel.Dock = DockStyle.Fill;
            BuyTradePanel.Location = new Point(3, 3);
            BuyTradePanel.Name = "BuyTradePanel";
            BuyTradePanel.Size = new Size(605, 615);
            BuyTradePanel.TabIndex = 0;
            // 
            // SellTradePanel
            // 
            SellTradePanel.Dock = DockStyle.Fill;
            SellTradePanel.Location = new Point(614, 3);
            SellTradePanel.Name = "SellTradePanel";
            SellTradePanel.Size = new Size(606, 615);
            SellTradePanel.TabIndex = 1;
            // 
            // NoItemsLabel
            // 
            NoItemsLabel.Dock = DockStyle.Top;
            NoItemsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NoItemsLabel.Location = new Point(0, 0);
            NoItemsLabel.Name = "NoItemsLabel";
            NoItemsLabel.Size = new Size(1223, 30);
            NoItemsLabel.TabIndex = 2;
            NoItemsLabel.Text = "No trades are currently loaded.";
            NoItemsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainLayoutPanel
            // 
            MainLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MainLayoutPanel.ColumnCount = 2;
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MainLayoutPanel.Controls.Add(BuyTradePanel, 0, 0);
            MainLayoutPanel.Controls.Add(SellTradePanel, 1, 0);
            MainLayoutPanel.Location = new Point(0, 29);
            MainLayoutPanel.Name = "MainLayoutPanel";
            MainLayoutPanel.RowCount = 1;
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            MainLayoutPanel.Size = new Size(1223, 621);
            MainLayoutPanel.TabIndex = 1;
            // 
            // TopPanel
            // 
            TopPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TopPanel.Controls.Add(SectorNameLabel);
            TopPanel.Controls.Add(SectorNameComboBox);
            TopPanel.Controls.Add(StationOwnerLabel);
            TopPanel.Controls.Add(StationOwnerComboBox);
            TopPanel.Controls.Add(SectorOwnerLabel);
            TopPanel.Controls.Add(WareComboBox);
            TopPanel.Controls.Add(SectorOwnerComboBox);
            TopPanel.Controls.Add(WareLabel);
            TopPanel.Location = new Point(0, 0);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new Size(1223, 30);
            TopPanel.TabIndex = 0;
            // 
            // SectorNameLabel
            // 
            SectorNameLabel.AutoSize = true;
            SectorNameLabel.Location = new Point(758, 3);
            SectorNameLabel.Name = "SectorNameLabel";
            SectorNameLabel.Size = new Size(43, 15);
            SectorNameLabel.TabIndex = 6;
            SectorNameLabel.Text = "Sector:";
            // 
            // SectorNameComboBox
            // 
            SectorNameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SectorNameComboBox.FormattingEnabled = true;
            SectorNameComboBox.Location = new Point(807, 0);
            SectorNameComboBox.Name = "SectorNameComboBox";
            SectorNameComboBox.Size = new Size(200, 23);
            SectorNameComboBox.TabIndex = 7;
            // 
            // StationOwnerLabel
            // 
            StationOwnerLabel.AutoSize = true;
            StationOwnerLabel.Location = new Point(486, 3);
            StationOwnerLabel.Name = "StationOwnerLabel";
            StationOwnerLabel.Size = new Size(85, 15);
            StationOwnerLabel.TabIndex = 4;
            StationOwnerLabel.Text = "Station Owner:";
            // 
            // StationOwnerComboBox
            // 
            StationOwnerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            StationOwnerComboBox.FormattingEnabled = true;
            StationOwnerComboBox.Location = new Point(577, 0);
            StationOwnerComboBox.Name = "StationOwnerComboBox";
            StationOwnerComboBox.Size = new Size(175, 23);
            StationOwnerComboBox.TabIndex = 5;
            // 
            // SectorOwnerLabel
            // 
            SectorOwnerLabel.AutoSize = true;
            SectorOwnerLabel.Location = new Point(223, 3);
            SectorOwnerLabel.Name = "SectorOwnerLabel";
            SectorOwnerLabel.Size = new Size(81, 15);
            SectorOwnerLabel.TabIndex = 2;
            SectorOwnerLabel.Text = "Sector Owner:";
            // 
            // WareComboBox
            // 
            WareComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WareComboBox.FormattingEnabled = true;
            WareComboBox.Location = new Point(42, 0);
            WareComboBox.Name = "WareComboBox";
            WareComboBox.Size = new Size(175, 23);
            WareComboBox.TabIndex = 1;
            // 
            // SectorOwnerComboBox
            // 
            SectorOwnerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SectorOwnerComboBox.FormattingEnabled = true;
            SectorOwnerComboBox.Location = new Point(305, 0);
            SectorOwnerComboBox.Name = "SectorOwnerComboBox";
            SectorOwnerComboBox.Size = new Size(175, 23);
            SectorOwnerComboBox.TabIndex = 3;
            // 
            // WareLabel
            // 
            WareLabel.AutoSize = true;
            WareLabel.Location = new Point(-1, 3);
            WareLabel.Name = "WareLabel";
            WareLabel.Size = new Size(37, 15);
            WareLabel.TabIndex = 0;
            WareLabel.Text = "Ware:";
            // 
            // TradeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NoItemsLabel);
            Controls.Add(TopPanel);
            Controls.Add(MainLayoutPanel);
            Name = "TradeControl";
            Size = new Size(1223, 650);
            MainLayoutPanel.ResumeLayout(false);
            TopPanel.ResumeLayout(false);
            TopPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label NoItemsLabel;
        private TradePanelControl BuyTradePanel;
        private TradePanelControl SellTradePanel;
        private TableLayoutPanel MainLayoutPanel;
        private Panel TopPanel;
        private Label StationOwnerLabel;
        private ComboBox StationOwnerComboBox;
        private Label SectorOwnerLabel;
        private ComboBox WareComboBox;
        private ComboBox SectorOwnerComboBox;
        private Label WareLabel;
        private Label SectorNameLabel;
        private ComboBox SectorNameComboBox;
    }
}
