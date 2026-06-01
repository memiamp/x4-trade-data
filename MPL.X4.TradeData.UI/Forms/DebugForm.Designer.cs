namespace MPL.X4.TradeData.UI.Forms
{
    partial class DebugForm
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
            OutputTextBox = new TextBox();
            SuspendLayout();
            // 
            // OutputTextBox
            // 
            OutputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OutputTextBox.BackColor = SystemColors.Window;
            OutputTextBox.Location = new Point(12, 12);
            OutputTextBox.Multiline = true;
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.ReadOnly = true;
            OutputTextBox.ScrollBars = ScrollBars.Both;
            OutputTextBox.Size = new Size(780, 437);
            OutputTextBox.TabIndex = 0;
            OutputTextBox.WordWrap = false;
            // 
            // DebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 461);
            Controls.Add(OutputTextBox);
            MinimumSize = new Size(820, 500);
            Name = "DebugForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Debug Output";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox OutputTextBox;
    }
}