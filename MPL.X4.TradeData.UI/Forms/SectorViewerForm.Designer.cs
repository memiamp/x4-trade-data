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
            SectorPlot = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // SectorPlot
            // 
            SectorPlot.Dock = DockStyle.Bottom;
            SectorPlot.Location = new Point(0, 98);
            SectorPlot.Name = "SectorPlot";
            SectorPlot.Size = new Size(946, 580);
            SectorPlot.TabIndex = 0;
            // 
            // SectorViewerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(946, 678);
            Controls.Add(SectorPlot);
            Name = "SectorViewerForm";
            Text = "SectorViewerForm";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot SectorPlot;
    }
}