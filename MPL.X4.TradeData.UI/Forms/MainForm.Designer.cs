namespace MPL.X4.TradeData.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainTabControl = new TabControl();
            SectorListTab = new TabPage();
            SectorListControl = new MPL.X4.TradeData.UI.Controls.SectorListControl();
            ShipTab = new TabPage();
            ShipControl = new MPL.X4.TradeData.UI.Controls.ShipBrowserControl();
            SpecialItemsTab = new TabPage();
            SpecialItemControl = new MPL.X4.TradeData.UI.Controls.SpecialItemControl();
            TradesTab = new TabPage();
            TradeControl = new MPL.X4.TradeData.UI.Controls.TradeControl();
            TradeLogTab = new TabPage();
            TradeLogViewerControl = new MPL.X4.TradeData.UI.Controls.TradeLogViewerControl();
            menuStrip1 = new MenuStrip();
            FileMenu = new ToolStripMenuItem();
            FileMenu_Exit = new ToolStripMenuItem();
            ToolMenu = new ToolStripMenuItem();
            ToolMenu_Options = new ToolStripMenuItem();
            MainTabControl.SuspendLayout();
            SectorListTab.SuspendLayout();
            ShipTab.SuspendLayout();
            SpecialItemsTab.SuspendLayout();
            TradesTab.SuspendLayout();
            TradeLogTab.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // MainTabControl
            // 
            MainTabControl.Controls.Add(SectorListTab);
            MainTabControl.Controls.Add(ShipTab);
            MainTabControl.Controls.Add(SpecialItemsTab);
            MainTabControl.Controls.Add(TradesTab);
            MainTabControl.Controls.Add(TradeLogTab);
            MainTabControl.Dock = DockStyle.Fill;
            MainTabControl.Location = new Point(0, 24);
            MainTabControl.Name = "MainTabControl";
            MainTabControl.SelectedIndex = 0;
            MainTabControl.Size = new Size(1080, 537);
            MainTabControl.TabIndex = 0;
            // 
            // SectorListTab
            // 
            SectorListTab.Controls.Add(SectorListControl);
            SectorListTab.Location = new Point(4, 24);
            SectorListTab.Name = "SectorListTab";
            SectorListTab.Padding = new Padding(3);
            SectorListTab.Size = new Size(1072, 509);
            SectorListTab.TabIndex = 2;
            SectorListTab.Text = "Sectors";
            SectorListTab.UseVisualStyleBackColor = true;
            // 
            // SectorListControl
            // 
            SectorListControl.Dock = DockStyle.Fill;
            SectorListControl.Location = new Point(3, 3);
            SectorListControl.Name = "SectorListControl";
            SectorListControl.Size = new Size(1066, 503);
            SectorListControl.TabIndex = 0;
            // 
            // ShipTab
            // 
            ShipTab.Controls.Add(ShipControl);
            ShipTab.Location = new Point(4, 24);
            ShipTab.Name = "ShipTab";
            ShipTab.Padding = new Padding(3);
            ShipTab.Size = new Size(1072, 509);
            ShipTab.TabIndex = 3;
            ShipTab.Text = "Ships";
            ShipTab.UseVisualStyleBackColor = true;
            // 
            // ShipControl
            // 
            ShipControl.Dock = DockStyle.Fill;
            ShipControl.Location = new Point(3, 3);
            ShipControl.Name = "ShipControl";
            ShipControl.Size = new Size(1066, 503);
            ShipControl.TabIndex = 0;
            // 
            // SpecialItemsTab
            // 
            SpecialItemsTab.Controls.Add(SpecialItemControl);
            SpecialItemsTab.Location = new Point(4, 24);
            SpecialItemsTab.Name = "SpecialItemsTab";
            SpecialItemsTab.Padding = new Padding(3);
            SpecialItemsTab.Size = new Size(1072, 509);
            SpecialItemsTab.TabIndex = 1;
            SpecialItemsTab.Text = "Special Items";
            SpecialItemsTab.UseVisualStyleBackColor = true;
            // 
            // SpecialItemControl
            // 
            SpecialItemControl.Dock = DockStyle.Fill;
            SpecialItemControl.Location = new Point(3, 3);
            SpecialItemControl.Name = "SpecialItemControl";
            SpecialItemControl.Size = new Size(1066, 503);
            SpecialItemControl.TabIndex = 0;
            // 
            // TradesTab
            // 
            TradesTab.Controls.Add(TradeControl);
            TradesTab.Location = new Point(4, 24);
            TradesTab.Name = "TradesTab";
            TradesTab.Padding = new Padding(3);
            TradesTab.Size = new Size(1072, 509);
            TradesTab.TabIndex = 0;
            TradesTab.Text = "Trades";
            TradesTab.UseVisualStyleBackColor = true;
            // 
            // TradeControl
            // 
            TradeControl.Dock = DockStyle.Fill;
            TradeControl.Location = new Point(3, 3);
            TradeControl.Name = "TradeControl";
            TradeControl.Size = new Size(1066, 503);
            TradeControl.TabIndex = 0;
            // 
            // TradeLogTab
            // 
            TradeLogTab.Controls.Add(TradeLogViewerControl);
            TradeLogTab.Location = new Point(4, 24);
            TradeLogTab.Name = "TradeLogTab";
            TradeLogTab.Padding = new Padding(3);
            TradeLogTab.Size = new Size(1072, 509);
            TradeLogTab.TabIndex = 4;
            TradeLogTab.Text = "Trade Log";
            TradeLogTab.UseVisualStyleBackColor = true;
            // 
            // TradeLogViewerControl
            // 
            TradeLogViewerControl.Dock = DockStyle.Fill;
            TradeLogViewerControl.Location = new Point(3, 3);
            TradeLogViewerControl.Name = "TradeLogViewerControl";
            TradeLogViewerControl.Size = new Size(1066, 503);
            TradeLogViewerControl.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileMenu, ToolMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1080, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "MainMenu";
            // 
            // FileMenu
            // 
            FileMenu.DropDownItems.AddRange(new ToolStripItem[] { FileMenu_Exit });
            FileMenu.Name = "FileMenu";
            FileMenu.Size = new Size(37, 20);
            FileMenu.Text = "&File";
            // 
            // FileMenu_Exit
            // 
            FileMenu_Exit.Name = "FileMenu_Exit";
            FileMenu_Exit.Size = new Size(93, 22);
            FileMenu_Exit.Text = "E&xit";
            // 
            // ToolMenu
            // 
            ToolMenu.DropDownItems.AddRange(new ToolStripItem[] { ToolMenu_Options });
            ToolMenu.Name = "ToolMenu";
            ToolMenu.Size = new Size(46, 20);
            ToolMenu.Text = "&Tools";
            // 
            // ToolMenu_Options
            // 
            ToolMenu_Options.Name = "ToolMenu_Options";
            ToolMenu_Options.Size = new Size(125, 22);
            ToolMenu_Options.Text = "&Options...";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 561);
            Controls.Add(MainTabControl);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1096, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MPL X4 Trade Data";
            MainTabControl.ResumeLayout(false);
            SectorListTab.ResumeLayout(false);
            ShipTab.ResumeLayout(false);
            SpecialItemsTab.ResumeLayout(false);
            TradesTab.ResumeLayout(false);
            TradeLogTab.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl MainTabControl;
        private TabPage TradesTab;
        private TabPage SpecialItemsTab;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileMenu;
        private ToolStripMenuItem FileMenu_Exit;
        private Controls.SpecialItemControl SpecialItemControl;
        private TabPage SectorListTab;
        private Controls.SectorListControl SectorListControl;
        private Controls.TradeControl TradeControl;
        private ToolStripMenuItem ToolMenu;
        private ToolStripMenuItem ToolMenu_Options;
        private TabPage ShipTab;
        private Controls.ShipBrowserControl ShipControl;
        private TabPage TradeLogTab;
        private Controls.TradeLogViewerControl TradeLogViewerControl;
    }
}
