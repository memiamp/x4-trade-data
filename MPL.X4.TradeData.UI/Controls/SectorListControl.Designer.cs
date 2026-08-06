namespace MPL.X4.TradeData.UI.Controls
{
    partial class SectorListControl
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
            SectorListView = new ListView();
            SectorListView_Name = new ColumnHeader();
            SectorListView_Owner = new ColumnHeader();
            SectorListView_AbandonedShips = new ColumnHeader();
            SectorListView_LockBoxes = new ColumnHeader();
            SectorListView_Ships = new ColumnHeader();
            SectorListView_Stations = new ColumnHeader();
            SectorListView_Drops = new ColumnHeader();
            SectorListView_BuildStorages = new ColumnHeader();
            SectorListView_Shipwrecks = new ColumnHeader();
            SectorListView_StationWrecks = new ColumnHeader();
            ViewSectorButton = new Button();
            NoItemsLabel = new Label();
            SectorOwnerComboBox = new ComboBox();
            SectorOwnerLabel = new Label();
            AbandonedShipsCheckBox = new CheckBox();
            LockboxesCheckBox = new CheckBox();
            DropsCheckBox = new CheckBox();
            BuildStorageCheckBox = new CheckBox();
            ShipwreckCheckBox = new CheckBox();
            StationWreckCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // SectorListView
            // 
            SectorListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SectorListView.Columns.AddRange(new ColumnHeader[] { SectorListView_Name, SectorListView_Owner, SectorListView_AbandonedShips, SectorListView_BuildStorages, SectorListView_Drops, SectorListView_LockBoxes, SectorListView_Ships, SectorListView_Shipwrecks, SectorListView_Stations, SectorListView_StationWrecks });
            SectorListView.FullRowSelect = true;
            SectorListView.Location = new Point(0, 32);
            SectorListView.Name = "SectorListView";
            SectorListView.Size = new Size(985, 471);
            SectorListView.TabIndex = 6;
            SectorListView.UseCompatibleStateImageBehavior = false;
            SectorListView.View = View.Details;
            // 
            // SectorListView_Name
            // 
            SectorListView_Name.Text = "Name";
            SectorListView_Name.Width = 200;
            // 
            // SectorListView_Owner
            // 
            SectorListView_Owner.Text = "Owner";
            SectorListView_Owner.Width = 170;
            // 
            // SectorListView_AbandonedShips
            // 
            SectorListView_AbandonedShips.Text = "Abandoned";
            SectorListView_AbandonedShips.TextAlign = HorizontalAlignment.Right;
            // 
            // SectorListView_LockBoxes
            // 
            SectorListView_LockBoxes.Text = "Lockboxes";
            SectorListView_LockBoxes.TextAlign = HorizontalAlignment.Right;
            // 
            // SectorListView_Ships
            // 
            SectorListView_Ships.Text = "Ships";
            SectorListView_Ships.TextAlign = HorizontalAlignment.Right;
            // 
            // SectorListView_Stations
            // 
            SectorListView_Stations.Text = "Stations";
            SectorListView_Stations.TextAlign = HorizontalAlignment.Right;
            // 
            // SectorListView_Drops
            // 
            SectorListView_Drops.Text = "Drops";
            SectorListView_Drops.TextAlign = HorizontalAlignment.Right;
            // 
            // SectorListView_BuildStorages
            // 
            SectorListView_BuildStorages.Text = "Build Storages";
            SectorListView_BuildStorages.TextAlign = HorizontalAlignment.Right;
            // 
            // SectorListView_Shipwrecks
            // 
            SectorListView_Shipwrecks.Text = "Shipwrecks";
            SectorListView_Shipwrecks.TextAlign = HorizontalAlignment.Right;
            // 
            // SectorListView_StationWrecks
            // 
            SectorListView_StationWrecks.Text = "Station Wrecks";
            SectorListView_StationWrecks.TextAlign = HorizontalAlignment.Right;
            // 
            // ViewSectorButton
            // 
            ViewSectorButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ViewSectorButton.Location = new Point(892, 509);
            ViewSectorButton.Name = "ViewSectorButton";
            ViewSectorButton.Size = new Size(90, 23);
            ViewSectorButton.TabIndex = 7;
            ViewSectorButton.Text = "View Sector";
            ViewSectorButton.UseVisualStyleBackColor = true;
            // 
            // NoItemsLabel
            // 
            NoItemsLabel.Dock = DockStyle.Top;
            NoItemsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NoItemsLabel.Location = new Point(0, 0);
            NoItemsLabel.Name = "NoItemsLabel";
            NoItemsLabel.Size = new Size(985, 30);
            NoItemsLabel.TabIndex = 8;
            NoItemsLabel.Text = "No sectors are currently loaded.";
            NoItemsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SectorOwnerComboBox
            // 
            SectorOwnerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SectorOwnerComboBox.FormattingEnabled = true;
            SectorOwnerComboBox.Location = new Point(87, 3);
            SectorOwnerComboBox.Name = "SectorOwnerComboBox";
            SectorOwnerComboBox.Size = new Size(175, 23);
            SectorOwnerComboBox.TabIndex = 1;
            // 
            // SectorOwnerLabel
            // 
            SectorOwnerLabel.AutoSize = true;
            SectorOwnerLabel.Location = new Point(0, 6);
            SectorOwnerLabel.Name = "SectorOwnerLabel";
            SectorOwnerLabel.Size = new Size(81, 15);
            SectorOwnerLabel.TabIndex = 0;
            SectorOwnerLabel.Text = "Sector Owner:";
            // 
            // AbandonedShipsCheckBox
            // 
            AbandonedShipsCheckBox.AutoSize = true;
            AbandonedShipsCheckBox.Location = new Point(268, 5);
            AbandonedShipsCheckBox.Name = "AbandonedShipsCheckBox";
            AbandonedShipsCheckBox.Size = new Size(118, 19);
            AbandonedShipsCheckBox.TabIndex = 2;
            AbandonedShipsCheckBox.Text = "Abandoned ships";
            AbandonedShipsCheckBox.UseVisualStyleBackColor = true;
            // 
            // LockboxesCheckBox
            // 
            LockboxesCheckBox.AutoSize = true;
            LockboxesCheckBox.Location = new Point(562, 5);
            LockboxesCheckBox.Name = "LockboxesCheckBox";
            LockboxesCheckBox.Size = new Size(82, 19);
            LockboxesCheckBox.TabIndex = 5;
            LockboxesCheckBox.Text = "Lockboxes";
            LockboxesCheckBox.UseVisualStyleBackColor = true;
            // 
            // DropsCheckBox
            // 
            DropsCheckBox.AutoSize = true;
            DropsCheckBox.Location = new Point(499, 5);
            DropsCheckBox.Name = "DropsCheckBox";
            DropsCheckBox.Size = new Size(57, 19);
            DropsCheckBox.TabIndex = 4;
            DropsCheckBox.Text = "Drops";
            DropsCheckBox.UseVisualStyleBackColor = true;
            // 
            // BuildStorageCheckBox
            // 
            BuildStorageCheckBox.AutoSize = true;
            BuildStorageCheckBox.Location = new Point(392, 5);
            BuildStorageCheckBox.Name = "BuildStorageCheckBox";
            BuildStorageCheckBox.Size = new Size(101, 19);
            BuildStorageCheckBox.TabIndex = 3;
            BuildStorageCheckBox.Text = "Build Storages";
            BuildStorageCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShipwreckCheckBox
            // 
            ShipwreckCheckBox.AutoSize = true;
            ShipwreckCheckBox.Location = new Point(650, 5);
            ShipwreckCheckBox.Name = "ShipwreckCheckBox";
            ShipwreckCheckBox.Size = new Size(85, 19);
            ShipwreckCheckBox.TabIndex = 9;
            ShipwreckCheckBox.Text = "Shipwrecks";
            ShipwreckCheckBox.UseVisualStyleBackColor = true;
            // 
            // StationWreckCheckBox
            // 
            StationWreckCheckBox.AutoSize = true;
            StationWreckCheckBox.Location = new Point(741, 5);
            StationWreckCheckBox.Name = "StationWreckCheckBox";
            StationWreckCheckBox.Size = new Size(104, 19);
            StationWreckCheckBox.TabIndex = 10;
            StationWreckCheckBox.Text = "Station Wrecks";
            StationWreckCheckBox.UseVisualStyleBackColor = true;
            // 
            // SectorListControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(StationWreckCheckBox);
            Controls.Add(ShipwreckCheckBox);
            Controls.Add(BuildStorageCheckBox);
            Controls.Add(DropsCheckBox);
            Controls.Add(LockboxesCheckBox);
            Controls.Add(AbandonedShipsCheckBox);
            Controls.Add(SectorOwnerLabel);
            Controls.Add(SectorOwnerComboBox);
            Controls.Add(ViewSectorButton);
            Controls.Add(SectorListView);
            Controls.Add(NoItemsLabel);
            Name = "SectorListControl";
            Size = new Size(985, 535);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView SectorListView;
        private ColumnHeader SectorListView_Name;
        private ColumnHeader SectorListView_Owner;
        private ColumnHeader SectorListView_AbandonedShips;
        private ColumnHeader SectorListView_LockBoxes;
        private ColumnHeader SectorListView_Ships;
        private ColumnHeader SectorListView_Stations;
        private Button ViewSectorButton;
        private Label NoItemsLabel;
        private ComboBox SectorOwnerComboBox;
        private Label SectorOwnerLabel;
        private CheckBox AbandonedShipsCheckBox;
        private CheckBox LockboxesCheckBox;
        private ColumnHeader SectorListView_Drops;
        private CheckBox DropsCheckBox;
        private ColumnHeader SectorListView_BuildStorages;
        private CheckBox BuildStorageCheckBox;
        private ColumnHeader SectorListView_Shipwrecks;
        private CheckBox ShipwreckCheckBox;
        private CheckBox StationWreckCheckBox;
        private ColumnHeader SectorListView_StationWrecks;
    }
}
