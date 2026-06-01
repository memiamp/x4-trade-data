namespace MPL.X4.TradeData.UI.Forms
{
    partial class SectorViewerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SectorPlot = new MPL.X4.TradeData.UI.Controls.PlotControl();
            LayoutPanel = new TableLayoutPanel();
            LayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // SectorPlot
            // 
            SectorPlot.BackColor = Color.Black;
            SectorPlot.Dock = DockStyle.Fill;
            SectorPlot.Location = new Point(3, 33);
            SectorPlot.Name = "SectorPlot";
            SectorPlot.Size = new Size(742, 645);
            SectorPlot.TabIndex = 0;
            // 
            // LayoutPanel
            // 
            LayoutPanel.ColumnCount = 1;
            LayoutPanel.ColumnStyles.Add(new ColumnStyle());
            LayoutPanel.Controls.Add(SectorPlot, 0, 1);
            LayoutPanel.Dock = DockStyle.Fill;
            LayoutPanel.Location = new Point(0, 0);
            LayoutPanel.Name = "LayoutPanel";
            LayoutPanel.RowCount = 2;
            LayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            LayoutPanel.RowStyles.Add(new RowStyle());
            LayoutPanel.Size = new Size(748, 681);
            LayoutPanel.TabIndex = 1;
            // 
            // SectorViewerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(748, 681);
            Controls.Add(LayoutPanel);
            MinimumSize = new Size(764, 720);
            Name = "SectorViewerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sector Viewer";
            LayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls.PlotControl SectorPlot;
        private TableLayoutPanel LayoutPanel;
    }
}