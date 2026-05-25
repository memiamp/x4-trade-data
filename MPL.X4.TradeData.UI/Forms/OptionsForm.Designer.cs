namespace MPL.X4.TradeData.UI.Forms
{
    partial class OptionsForm
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
            CatalogFilePathLabel = new Label();
            CatalogFilePathTextBox = new TextBox();
            CatalogFilePathBrowseButton = new Button();
            CheckIntervalSecondsNumericUpDown = new NumericUpDown();
            CheckIntervalSecondsLabel = new Label();
            SaveGameFileAgeSecondsNumericUpDown = new NumericUpDown();
            SaveGameFileAgeSecondsLabel = new Label();
            SaveGameFileFilterLabel = new Label();
            SaveGameFileFilterTextBox = new TextBox();
            SaveGameFilePathBrowseButton = new Button();
            SaveGameFilePathTextBox = new TextBox();
            SaveGameFilePathLabel = new Label();
            OkButton = new Button();
            CancelChangeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)CheckIntervalSecondsNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SaveGameFileAgeSecondsNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // CatalogFilePathLabel
            // 
            CatalogFilePathLabel.AutoSize = true;
            CatalogFilePathLabel.Location = new Point(42, 16);
            CatalogFilePathLabel.Name = "CatalogFilePathLabel";
            CatalogFilePathLabel.Size = new Size(99, 15);
            CatalogFilePathLabel.TabIndex = 0;
            CatalogFilePathLabel.Text = "Catalog File Path:";
            CatalogFilePathLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CatalogFilePathTextBox
            // 
            CatalogFilePathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CatalogFilePathTextBox.Location = new Point(147, 12);
            CatalogFilePathTextBox.Name = "CatalogFilePathTextBox";
            CatalogFilePathTextBox.Size = new Size(392, 23);
            CatalogFilePathTextBox.TabIndex = 1;
            // 
            // CatalogFilePathBrowseButton
            // 
            CatalogFilePathBrowseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CatalogFilePathBrowseButton.Location = new Point(537, 12);
            CatalogFilePathBrowseButton.Name = "CatalogFilePathBrowseButton";
            CatalogFilePathBrowseButton.Size = new Size(75, 23);
            CatalogFilePathBrowseButton.TabIndex = 2;
            CatalogFilePathBrowseButton.Text = "Browse";
            CatalogFilePathBrowseButton.UseVisualStyleBackColor = true;
            // 
            // CheckIntervalSecondsNumericUpDown
            // 
            CheckIntervalSecondsNumericUpDown.Location = new Point(147, 41);
            CheckIntervalSecondsNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            CheckIntervalSecondsNumericUpDown.Name = "CheckIntervalSecondsNumericUpDown";
            CheckIntervalSecondsNumericUpDown.Size = new Size(73, 23);
            CheckIntervalSecondsNumericUpDown.TabIndex = 3;
            CheckIntervalSecondsNumericUpDown.TextAlign = HorizontalAlignment.Right;
            CheckIntervalSecondsNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CheckIntervalSecondsLabel
            // 
            CheckIntervalSecondsLabel.AutoSize = true;
            CheckIntervalSecondsLabel.Location = new Point(40, 43);
            CheckIntervalSecondsLabel.Name = "CheckIntervalSecondsLabel";
            CheckIntervalSecondsLabel.Size = new Size(101, 15);
            CheckIntervalSecondsLabel.TabIndex = 4;
            CheckIntervalSecondsLabel.Text = "Check Interval (s):";
            CheckIntervalSecondsLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SaveGameFileAgeSecondsNumericUpDown
            // 
            SaveGameFileAgeSecondsNumericUpDown.Location = new Point(147, 70);
            SaveGameFileAgeSecondsNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SaveGameFileAgeSecondsNumericUpDown.Name = "SaveGameFileAgeSecondsNumericUpDown";
            SaveGameFileAgeSecondsNumericUpDown.Size = new Size(73, 23);
            SaveGameFileAgeSecondsNumericUpDown.TabIndex = 5;
            SaveGameFileAgeSecondsNumericUpDown.TextAlign = HorizontalAlignment.Right;
            SaveGameFileAgeSecondsNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // SaveGameFileAgeSecondsLabel
            // 
            SaveGameFileAgeSecondsLabel.AutoSize = true;
            SaveGameFileAgeSecondsLabel.Location = new Point(12, 72);
            SaveGameFileAgeSecondsLabel.Name = "SaveGameFileAgeSecondsLabel";
            SaveGameFileAgeSecondsLabel.Size = new Size(129, 15);
            SaveGameFileAgeSecondsLabel.TabIndex = 6;
            SaveGameFileAgeSecondsLabel.Text = "Save Game File Age (s):";
            // 
            // SaveGameFileFilterLabel
            // 
            SaveGameFileFilterLabel.AutoSize = true;
            SaveGameFileFilterLabel.Location = new Point(23, 102);
            SaveGameFileFilterLabel.Name = "SaveGameFileFilterLabel";
            SaveGameFileFilterLabel.Size = new Size(118, 15);
            SaveGameFileFilterLabel.TabIndex = 7;
            SaveGameFileFilterLabel.Text = "Save Game File Filter:";
            // 
            // SaveGameFileFilterTextBox
            // 
            SaveGameFileFilterTextBox.Location = new Point(147, 99);
            SaveGameFileFilterTextBox.Name = "SaveGameFileFilterTextBox";
            SaveGameFileFilterTextBox.Size = new Size(132, 23);
            SaveGameFileFilterTextBox.TabIndex = 8;
            // 
            // SaveGameFilePathBrowseButton
            // 
            SaveGameFilePathBrowseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SaveGameFilePathBrowseButton.Location = new Point(537, 128);
            SaveGameFilePathBrowseButton.Name = "SaveGameFilePathBrowseButton";
            SaveGameFilePathBrowseButton.Size = new Size(75, 23);
            SaveGameFilePathBrowseButton.TabIndex = 11;
            SaveGameFilePathBrowseButton.Text = "Browse";
            SaveGameFilePathBrowseButton.UseVisualStyleBackColor = true;
            // 
            // SaveGameFilePathTextBox
            // 
            SaveGameFilePathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SaveGameFilePathTextBox.Location = new Point(147, 128);
            SaveGameFilePathTextBox.Name = "SaveGameFilePathTextBox";
            SaveGameFilePathTextBox.Size = new Size(392, 23);
            SaveGameFilePathTextBox.TabIndex = 10;
            // 
            // SaveGameFilePathLabel
            // 
            SaveGameFilePathLabel.AutoSize = true;
            SaveGameFilePathLabel.Location = new Point(25, 131);
            SaveGameFilePathLabel.Name = "SaveGameFilePathLabel";
            SaveGameFilePathLabel.Size = new Size(116, 15);
            SaveGameFilePathLabel.TabIndex = 9;
            SaveGameFilePathLabel.Text = "Save Game File Path:";
            SaveGameFilePathLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // OkButton
            // 
            OkButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OkButton.Location = new Point(456, 166);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(75, 23);
            OkButton.TabIndex = 12;
            OkButton.Text = "OK";
            OkButton.UseVisualStyleBackColor = true;
            // 
            // CancelChangeButton
            // 
            CancelChangeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CancelChangeButton.Location = new Point(537, 166);
            CancelChangeButton.Name = "CancelChangeButton";
            CancelChangeButton.Size = new Size(75, 23);
            CancelChangeButton.TabIndex = 13;
            CancelChangeButton.Text = "Cancel";
            CancelChangeButton.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 201);
            Controls.Add(CancelChangeButton);
            Controls.Add(OkButton);
            Controls.Add(SaveGameFilePathBrowseButton);
            Controls.Add(SaveGameFilePathTextBox);
            Controls.Add(SaveGameFilePathLabel);
            Controls.Add(SaveGameFileFilterTextBox);
            Controls.Add(SaveGameFileFilterLabel);
            Controls.Add(SaveGameFileAgeSecondsLabel);
            Controls.Add(SaveGameFileAgeSecondsNumericUpDown);
            Controls.Add(CheckIntervalSecondsLabel);
            Controls.Add(CheckIntervalSecondsNumericUpDown);
            Controls.Add(CatalogFilePathBrowseButton);
            Controls.Add(CatalogFilePathTextBox);
            Controls.Add(CatalogFilePathLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(640, 240);
            MinimizeBox = false;
            MinimumSize = new Size(640, 240);
            Name = "OptionsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Options";
            ((System.ComponentModel.ISupportInitialize)CheckIntervalSecondsNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)SaveGameFileAgeSecondsNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label CatalogFilePathLabel;
        private TextBox CatalogFilePathTextBox;
        private Button CatalogFilePathBrowseButton;
        private NumericUpDown CheckIntervalSecondsNumericUpDown;
        private Label CheckIntervalSecondsLabel;
        private NumericUpDown SaveGameFileAgeSecondsNumericUpDown;
        private Label SaveGameFileAgeSecondsLabel;
        private Label SaveGameFileFilterLabel;
        private TextBox SaveGameFileFilterTextBox;
        private Button SaveGameFilePathBrowseButton;
        private TextBox SaveGameFilePathTextBox;
        private Label SaveGameFilePathLabel;
        private Button OkButton;
        private Button CancelChangeButton;
    }
}