namespace MPL.X4.TradeData.UI.Controls
{
    partial class ShipBrowserControl
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
            ShipListView = new ListView();
            ShipListView_Class = new ColumnHeader();
            ShipListView_Model = new ColumnHeader();
            ShipListView_Owner = new ColumnHeader();
            ShipListView_Location = new ColumnHeader();
            ShipListView_Cargo = new ColumnHeader();
            ShipListView_Modifications = new ColumnHeader();
            ShipListView_X = new ColumnHeader();
            ShipListView_Y = new ColumnHeader();
            ShipListView_Z = new ColumnHeader();
            NoItemsLabel = new Label();
            AbandonedCheckBox = new CheckBox();
            CargoCheckBox = new CheckBox();
            ModificationCheckBox = new CheckBox();
            LocationNameLabel = new Label();
            LocationNameComboBox = new ComboBox();
            OwnerLabel = new Label();
            OwnerComboBox = new ComboBox();
            WareComboBox = new ComboBox();
            WareLabel = new Label();
            ViewShipButton = new Button();
            SuspendLayout();
            // 
            // ShipListView
            // 
            ShipListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ShipListView.Columns.AddRange(new ColumnHeader[] { ShipListView_Class, ShipListView_Model, ShipListView_Owner, ShipListView_Location, ShipListView_Cargo, ShipListView_Modifications, ShipListView_X, ShipListView_Y, ShipListView_Z });
            ShipListView.FullRowSelect = true;
            ShipListView.Location = new Point(0, 31);
            ShipListView.Name = "ShipListView";
            ShipListView.Size = new Size(997, 333);
            ShipListView.TabIndex = 10;
            ShipListView.UseCompatibleStateImageBehavior = false;
            ShipListView.View = View.Details;
            // 
            // ShipListView_Class
            // 
            ShipListView_Class.Text = "Class";
            ShipListView_Class.Width = 70;
            // 
            // ShipListView_Model
            // 
            ShipListView_Model.Text = "Name\\Model";
            ShipListView_Model.Width = 220;
            // 
            // ShipListView_Owner
            // 
            ShipListView_Owner.Text = "Owner";
            // 
            // ShipListView_Location
            // 
            ShipListView_Location.Text = "Location";
            ShipListView_Location.Width = 150;
            // 
            // ShipListView_Cargo
            // 
            ShipListView_Cargo.Text = "Cargo";
            ShipListView_Cargo.Width = 150;
            // 
            // ShipListView_Modifications
            // 
            ShipListView_Modifications.Text = "Modifications";
            ShipListView_Modifications.Width = 120;
            // 
            // ShipListView_X
            // 
            ShipListView_X.Text = "X";
            ShipListView_X.TextAlign = HorizontalAlignment.Right;
            // 
            // ShipListView_Y
            // 
            ShipListView_Y.Text = "Y";
            ShipListView_Y.TextAlign = HorizontalAlignment.Right;
            // 
            // ShipListView_Z
            // 
            ShipListView_Z.Text = "Z";
            ShipListView_Z.TextAlign = HorizontalAlignment.Right;
            // 
            // NoItemsLabel
            // 
            NoItemsLabel.Dock = DockStyle.Top;
            NoItemsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NoItemsLabel.Location = new Point(0, 0);
            NoItemsLabel.Name = "NoItemsLabel";
            NoItemsLabel.Size = new Size(997, 30);
            NoItemsLabel.TabIndex = 0;
            NoItemsLabel.Text = "No special items are currently loaded.";
            NoItemsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AbandonedCheckBox
            // 
            AbandonedCheckBox.AutoSize = true;
            AbandonedCheckBox.Location = new Point(3, 5);
            AbandonedCheckBox.Name = "AbandonedCheckBox";
            AbandonedCheckBox.Size = new Size(88, 19);
            AbandonedCheckBox.TabIndex = 1;
            AbandonedCheckBox.Text = "Abandoned";
            AbandonedCheckBox.UseVisualStyleBackColor = true;
            // 
            // CargoCheckBox
            // 
            CargoCheckBox.AutoSize = true;
            CargoCheckBox.Location = new Point(97, 5);
            CargoCheckBox.Name = "CargoCheckBox";
            CargoCheckBox.Size = new Size(58, 19);
            CargoCheckBox.TabIndex = 2;
            CargoCheckBox.Text = "Cargo";
            CargoCheckBox.UseVisualStyleBackColor = true;
            // 
            // ModificationCheckBox
            // 
            ModificationCheckBox.AutoSize = true;
            ModificationCheckBox.Location = new Point(161, 5);
            ModificationCheckBox.Name = "ModificationCheckBox";
            ModificationCheckBox.Size = new Size(99, 19);
            ModificationCheckBox.TabIndex = 3;
            ModificationCheckBox.Text = "Modifications";
            ModificationCheckBox.UseVisualStyleBackColor = true;
            // 
            // LocationNameLabel
            // 
            LocationNameLabel.AutoSize = true;
            LocationNameLabel.Location = new Point(498, 6);
            LocationNameLabel.Name = "LocationNameLabel";
            LocationNameLabel.Size = new Size(56, 15);
            LocationNameLabel.TabIndex = 6;
            LocationNameLabel.Text = "Location:";
            // 
            // LocationNameComboBox
            // 
            LocationNameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LocationNameComboBox.FormattingEnabled = true;
            LocationNameComboBox.Location = new Point(560, 3);
            LocationNameComboBox.Name = "LocationNameComboBox";
            LocationNameComboBox.Size = new Size(200, 23);
            LocationNameComboBox.TabIndex = 7;
            // 
            // OwnerLabel
            // 
            OwnerLabel.AutoSize = true;
            OwnerLabel.Location = new Point(266, 6);
            OwnerLabel.Name = "OwnerLabel";
            OwnerLabel.Size = new Size(45, 15);
            OwnerLabel.TabIndex = 4;
            OwnerLabel.Text = "Owner:";
            // 
            // OwnerComboBox
            // 
            OwnerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            OwnerComboBox.FormattingEnabled = true;
            OwnerComboBox.Location = new Point(317, 3);
            OwnerComboBox.Name = "OwnerComboBox";
            OwnerComboBox.Size = new Size(175, 23);
            OwnerComboBox.TabIndex = 5;
            // 
            // WareComboBox
            // 
            WareComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WareComboBox.FormattingEnabled = true;
            WareComboBox.Location = new Point(809, 3);
            WareComboBox.Name = "WareComboBox";
            WareComboBox.Size = new Size(175, 23);
            WareComboBox.TabIndex = 9;
            // 
            // WareLabel
            // 
            WareLabel.AutoSize = true;
            WareLabel.Location = new Point(766, 6);
            WareLabel.Name = "WareLabel";
            WareLabel.Size = new Size(37, 15);
            WareLabel.TabIndex = 8;
            WareLabel.Text = "Ware:";
            // 
            // ViewShipButton
            // 
            ViewShipButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ViewShipButton.Location = new Point(904, 370);
            ViewShipButton.Name = "ViewShipButton";
            ViewShipButton.Size = new Size(90, 23);
            ViewShipButton.TabIndex = 11;
            ViewShipButton.Text = "View Ship";
            ViewShipButton.UseVisualStyleBackColor = true;
            // 
            // ShipBrowserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ViewShipButton);
            Controls.Add(WareComboBox);
            Controls.Add(WareLabel);
            Controls.Add(LocationNameLabel);
            Controls.Add(LocationNameComboBox);
            Controls.Add(OwnerLabel);
            Controls.Add(OwnerComboBox);
            Controls.Add(ModificationCheckBox);
            Controls.Add(CargoCheckBox);
            Controls.Add(AbandonedCheckBox);
            Controls.Add(ShipListView);
            Controls.Add(NoItemsLabel);
            Name = "ShipBrowserControl";
            Size = new Size(997, 396);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView ShipListView;
        private ColumnHeader ShipListView_Class;
        private ColumnHeader ShipListView_Model;
        private ColumnHeader ShipListView_Location;
        private ColumnHeader ShipListView_X;
        private ColumnHeader ShipListView_Cargo;
        private ColumnHeader ShipListView_Modifications;
        private Label NoItemsLabel;
        private CheckBox AbandonedCheckBox;
        private CheckBox CargoCheckBox;
        private CheckBox ModificationCheckBox;
        private ColumnHeader ShipListView_Owner;
        private ColumnHeader ShipListView_Y;
        private ColumnHeader ShipListView_Z;
        private Label LocationNameLabel;
        private ComboBox LocationNameComboBox;
        private Label OwnerLabel;
        private ComboBox OwnerComboBox;
        private ComboBox WareComboBox;
        private Label WareLabel;
        private Button ViewShipButton;
    }
}
