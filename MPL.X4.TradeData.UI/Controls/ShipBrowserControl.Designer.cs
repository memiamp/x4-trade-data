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
            ShipListView_Sector = new ColumnHeader();
            ShipListView_Cargo = new ColumnHeader();
            ShipListView_Modifications = new ColumnHeader();
            ShipListView_Position = new ColumnHeader();
            NoItemsLabel = new Label();
            AbandonedCheckBox = new CheckBox();
            CargoCheckBox = new CheckBox();
            ModificationCheckBox = new CheckBox();
            ShipListView_Owner = new ColumnHeader();
            SuspendLayout();
            // 
            // ShipListView
            // 
            ShipListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ShipListView.Columns.AddRange(new ColumnHeader[] { ShipListView_Class, ShipListView_Model, ShipListView_Owner, ShipListView_Sector, ShipListView_Cargo, ShipListView_Modifications, ShipListView_Position });
            ShipListView.FullRowSelect = true;
            ShipListView.Location = new Point(0, 31);
            ShipListView.Name = "ShipListView";
            ShipListView.Size = new Size(997, 365);
            ShipListView.TabIndex = 5;
            ShipListView.UseCompatibleStateImageBehavior = false;
            ShipListView.View = View.Details;
            // 
            // ShipListView_Class
            // 
            ShipListView_Class.Text = "Class";
            // 
            // ShipListView_Model
            // 
            ShipListView_Model.Text = "Model";
            ShipListView_Model.Width = 220;
            // 
            // ShipListView_Sector
            // 
            ShipListView_Sector.Text = "Sector";
            ShipListView_Sector.Width = 150;
            // 
            // ShipListView_Cargo
            // 
            ShipListView_Cargo.Text = "Cargo";
            ShipListView_Cargo.Width = 150;
            // 
            // ShipListView_Modifications
            // 
            ShipListView_Modifications.Text = "Modifications";
            ShipListView_Modifications.Width = 150;
            // 
            // ShipListView_Position
            // 
            ShipListView_Position.Text = "Position";
            ShipListView_Position.TextAlign = HorizontalAlignment.Right;
            ShipListView_Position.Width = 120;
            // 
            // NoItemsLabel
            // 
            NoItemsLabel.Dock = DockStyle.Bottom;
            NoItemsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NoItemsLabel.Location = new Point(0, 366);
            NoItemsLabel.Name = "NoItemsLabel";
            NoItemsLabel.Size = new Size(997, 30);
            NoItemsLabel.TabIndex = 6;
            NoItemsLabel.Text = "No special items are currently loaded.";
            NoItemsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AbandonedCheckBox
            // 
            AbandonedCheckBox.AutoSize = true;
            AbandonedCheckBox.Location = new Point(3, 3);
            AbandonedCheckBox.Name = "AbandonedCheckBox";
            AbandonedCheckBox.Size = new Size(88, 19);
            AbandonedCheckBox.TabIndex = 1;
            AbandonedCheckBox.Text = "Abandoned";
            AbandonedCheckBox.UseVisualStyleBackColor = true;
            // 
            // CargoCheckBox
            // 
            CargoCheckBox.AutoSize = true;
            CargoCheckBox.Location = new Point(97, 3);
            CargoCheckBox.Name = "CargoCheckBox";
            CargoCheckBox.Size = new Size(58, 19);
            CargoCheckBox.TabIndex = 2;
            CargoCheckBox.Text = "Cargo";
            CargoCheckBox.UseVisualStyleBackColor = true;
            // 
            // ModificationCheckBox
            // 
            ModificationCheckBox.AutoSize = true;
            ModificationCheckBox.Location = new Point(161, 3);
            ModificationCheckBox.Name = "ModificationCheckBox";
            ModificationCheckBox.Size = new Size(99, 19);
            ModificationCheckBox.TabIndex = 7;
            ModificationCheckBox.Text = "Modifications";
            ModificationCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShipListView_Owner
            // 
            ShipListView_Owner.Text = "Owner";
            ShipListView_Owner.Width = 170;
            // 
            // ShipBrowserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ModificationCheckBox);
            Controls.Add(NoItemsLabel);
            Controls.Add(CargoCheckBox);
            Controls.Add(AbandonedCheckBox);
            Controls.Add(ShipListView);
            Name = "ShipBrowserControl";
            Size = new Size(997, 396);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView ShipListView;
        private ColumnHeader ShipListView_Class;
        private ColumnHeader ShipListView_Model;
        private ColumnHeader ShipListView_Sector;
        private ColumnHeader ShipListView_Position;
        private ColumnHeader ShipListView_Cargo;
        private ColumnHeader ShipListView_Modifications;
        private Label NoItemsLabel;
        private CheckBox AbandonedCheckBox;
        private CheckBox CargoCheckBox;
        private CheckBox ModificationCheckBox;
        private ColumnHeader ShipListView_Owner;
    }
}
