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
            LayoutControl = new TabControl();
            TradesTab = new TabPage();
            TradeControl = new MPL.X4.TradeData.UI.Controls.TradeControl();
            SpecialItemsTab = new TabPage();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            SpecialItemControl = new MPL.X4.TradeData.UI.Controls.SpecialItemControl();
            LayoutControl.SuspendLayout();
            TradesTab.SuspendLayout();
            SpecialItemsTab.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // LayoutControl
            // 
            LayoutControl.Controls.Add(TradesTab);
            LayoutControl.Controls.Add(SpecialItemsTab);
            LayoutControl.Dock = DockStyle.Fill;
            LayoutControl.Location = new Point(0, 24);
            LayoutControl.Name = "LayoutControl";
            LayoutControl.SelectedIndex = 0;
            LayoutControl.Size = new Size(1080, 537);
            LayoutControl.TabIndex = 0;
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
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1080, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // SpecialItemControl
            // 
            SpecialItemControl.Dock = DockStyle.Fill;
            SpecialItemControl.Location = new Point(3, 3);
            SpecialItemControl.Name = "SpecialItemControl";
            SpecialItemControl.Size = new Size(1066, 503);
            SpecialItemControl.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 561);
            Controls.Add(LayoutControl);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1096, 600);
            Name = "MainForm";
            Text = "Form1";
            LayoutControl.ResumeLayout(false);
            TradesTab.ResumeLayout(false);
            SpecialItemsTab.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl LayoutControl;
        private TabPage TradesTab;
        private TabPage SpecialItemsTab;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Controls.TradeControl TradeControl;
        private Controls.SpecialItemControl SpecialItemControl;
    }
}
