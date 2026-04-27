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
            SectorListView_Code = new ColumnHeader();
            SectorListView_AbandonedShips = new ColumnHeader();
            SectorListView_LockBoxes = new ColumnHeader();
            SectorListView_Ships = new ColumnHeader();
            SectorListView_Stations = new ColumnHeader();
            ViewSectorButton = new Button();
            NoItemsLabel = new Label();
            SuspendLayout();
            // 
            // SectorListView
            // 
            SectorListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SectorListView.Columns.AddRange(new ColumnHeader[] { SectorListView_Name, SectorListView_Code, SectorListView_AbandonedShips, SectorListView_LockBoxes, SectorListView_Ships, SectorListView_Stations });
            SectorListView.FullRowSelect = true;
            SectorListView.Location = new Point(0, 0);
            SectorListView.Name = "SectorListView";
            SectorListView.Size = new Size(985, 503);
            SectorListView.TabIndex = 12;
            SectorListView.UseCompatibleStateImageBehavior = false;
            SectorListView.View = View.Details;
            // 
            // SectorListView_Name
            // 
            SectorListView_Name.Text = "Name";
            SectorListView_Name.Width = 200;
            // 
            // SectorListView_Code
            // 
            SectorListView_Code.Text = "Code";
            SectorListView_Code.Width = 70;
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
            // ViewSectorButton
            // 
            ViewSectorButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ViewSectorButton.Location = new Point(892, 509);
            ViewSectorButton.Name = "ViewSectorButton";
            ViewSectorButton.Size = new Size(90, 23);
            ViewSectorButton.TabIndex = 13;
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
            NoItemsLabel.TabIndex = 15;
            NoItemsLabel.Text = "No sectors are currently loaded.";
            NoItemsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SectorListControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NoItemsLabel);
            Controls.Add(ViewSectorButton);
            Controls.Add(SectorListView);
            Name = "SectorListControl";
            Size = new Size(985, 535);
            ResumeLayout(false);
        }

        #endregion

        private ListView SectorListView;
        private ColumnHeader SectorListView_Name;
        private ColumnHeader SectorListView_Code;
        private ColumnHeader SectorListView_AbandonedShips;
        private ColumnHeader SectorListView_LockBoxes;
        private ColumnHeader SectorListView_Ships;
        private ColumnHeader SectorListView_Stations;
        private Button ViewSectorButton;
        private Label NoItemsLabel;
    }
}
