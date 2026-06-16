namespace MPL.X4.TradeData.UI.Controls
{
    partial class SpecialItemControl
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
            SpecialItemListView = new ListView();
            SpecialItemListView_ItemType = new ColumnHeader();
            SpecialItemListView_Description = new ColumnHeader();
            SpecialItemListView_Sector = new ColumnHeader();
            SpecialItemListView_PositionX = new ColumnHeader();
            SpecialItemListView_PositionY = new ColumnHeader();
            SpecialItemListView_PositionZ = new ColumnHeader();
            SpecialItemListView_Comments = new ColumnHeader();
            NoItemsLabel = new Label();
            BuildStorageAbandonedCheckBox = new CheckBox();
            AbandonedShipCheckBox = new CheckBox();
            LockboxCheckBox = new CheckBox();
            BuildStorageTopCheckBox = new CheckBox();
            BuildStorageTopNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)BuildStorageTopNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // SpecialItemListView
            // 
            SpecialItemListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SpecialItemListView.Columns.AddRange(new ColumnHeader[] { SpecialItemListView_ItemType, SpecialItemListView_Description, SpecialItemListView_Sector, SpecialItemListView_PositionX, SpecialItemListView_PositionY, SpecialItemListView_PositionZ, SpecialItemListView_Comments });
            SpecialItemListView.FullRowSelect = true;
            SpecialItemListView.Location = new Point(0, 31);
            SpecialItemListView.Name = "SpecialItemListView";
            SpecialItemListView.Size = new Size(997, 365);
            SpecialItemListView.TabIndex = 5;
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
            // SpecialItemListView_Comments
            // 
            SpecialItemListView_Comments.Text = "Comments";
            SpecialItemListView_Comments.Width = 200;
            // 
            // NoItemsLabel
            // 
            NoItemsLabel.Dock = DockStyle.Top;
            NoItemsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NoItemsLabel.Location = new Point(0, 0);
            NoItemsLabel.Name = "NoItemsLabel";
            NoItemsLabel.Size = new Size(997, 30);
            NoItemsLabel.TabIndex = 6;
            NoItemsLabel.Text = "No special items are currently loaded.";
            NoItemsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BuildStorageAbandonedCheckBox
            // 
            BuildStorageAbandonedCheckBox.AutoSize = true;
            BuildStorageAbandonedCheckBox.Location = new Point(3, 5);
            BuildStorageAbandonedCheckBox.Name = "BuildStorageAbandonedCheckBox";
            BuildStorageAbandonedCheckBox.Size = new Size(161, 19);
            BuildStorageAbandonedCheckBox.TabIndex = 0;
            BuildStorageAbandonedCheckBox.Text = "Abandoned Build Storage";
            BuildStorageAbandonedCheckBox.UseVisualStyleBackColor = true;
            // 
            // AbandonedShipCheckBox
            // 
            AbandonedShipCheckBox.AutoSize = true;
            AbandonedShipCheckBox.Location = new Point(170, 5);
            AbandonedShipCheckBox.Name = "AbandonedShipCheckBox";
            AbandonedShipCheckBox.Size = new Size(114, 19);
            AbandonedShipCheckBox.TabIndex = 1;
            AbandonedShipCheckBox.Text = "Abandoned Ship";
            AbandonedShipCheckBox.UseVisualStyleBackColor = true;
            // 
            // LockboxCheckBox
            // 
            LockboxCheckBox.AutoSize = true;
            LockboxCheckBox.Location = new Point(290, 5);
            LockboxCheckBox.Name = "LockboxCheckBox";
            LockboxCheckBox.Size = new Size(71, 19);
            LockboxCheckBox.TabIndex = 2;
            LockboxCheckBox.Text = "Lockbox";
            LockboxCheckBox.UseVisualStyleBackColor = true;
            // 
            // BuildStorageTopCheckBox
            // 
            BuildStorageTopCheckBox.AutoSize = true;
            BuildStorageTopCheckBox.Location = new Point(367, 5);
            BuildStorageTopCheckBox.Name = "BuildStorageTopCheckBox";
            BuildStorageTopCheckBox.Size = new Size(118, 19);
            BuildStorageTopCheckBox.TabIndex = 3;
            BuildStorageTopCheckBox.Text = "Top Build Storage";
            BuildStorageTopCheckBox.UseVisualStyleBackColor = true;
            // 
            // BuildStorageTopNumericUpDown
            // 
            BuildStorageTopNumericUpDown.Location = new Point(491, 3);
            BuildStorageTopNumericUpDown.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            BuildStorageTopNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            BuildStorageTopNumericUpDown.Name = "BuildStorageTopNumericUpDown";
            BuildStorageTopNumericUpDown.Size = new Size(59, 23);
            BuildStorageTopNumericUpDown.TabIndex = 4;
            BuildStorageTopNumericUpDown.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // SpecialItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(BuildStorageTopNumericUpDown);
            Controls.Add(BuildStorageTopCheckBox);
            Controls.Add(LockboxCheckBox);
            Controls.Add(AbandonedShipCheckBox);
            Controls.Add(BuildStorageAbandonedCheckBox);
            Controls.Add(SpecialItemListView);
            Controls.Add(NoItemsLabel);
            Name = "SpecialItemControl";
            Size = new Size(997, 396);
            ((System.ComponentModel.ISupportInitialize)BuildStorageTopNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView SpecialItemListView;
        private ColumnHeader SpecialItemListView_ItemType;
        private ColumnHeader SpecialItemListView_Description;
        private ColumnHeader SpecialItemListView_Sector;
        private ColumnHeader SpecialItemListView_PositionX;
        private ColumnHeader SpecialItemListView_PositionY;
        private ColumnHeader SpecialItemListView_PositionZ;
        private Label NoItemsLabel;
        private ColumnHeader SpecialItemListView_Comments;
        private CheckBox BuildStorageAbandonedCheckBox;
        private CheckBox AbandonedShipCheckBox;
        private CheckBox LockboxCheckBox;
        private CheckBox BuildStorageTopCheckBox;
        private NumericUpDown BuildStorageTopNumericUpDown;
    }
}
