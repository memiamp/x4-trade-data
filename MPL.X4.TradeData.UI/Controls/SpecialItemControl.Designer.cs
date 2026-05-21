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
            SuspendLayout();
            // 
            // SpecialItemListView
            // 
            SpecialItemListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SpecialItemListView.Columns.AddRange(new ColumnHeader[] { SpecialItemListView_ItemType, SpecialItemListView_Description, SpecialItemListView_Sector, SpecialItemListView_PositionX, SpecialItemListView_PositionY, SpecialItemListView_PositionZ, SpecialItemListView_Comments });
            SpecialItemListView.FullRowSelect = true;
            SpecialItemListView.Location = new Point(0, 0);
            SpecialItemListView.Name = "SpecialItemListView";
            SpecialItemListView.Size = new Size(997, 396);
            SpecialItemListView.TabIndex = 11;
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
            NoItemsLabel.TabIndex = 14;
            NoItemsLabel.Text = "No special items are currently loaded.";
            NoItemsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SpecialItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NoItemsLabel);
            Controls.Add(SpecialItemListView);
            Name = "SpecialItemControl";
            Size = new Size(997, 396);
            ResumeLayout(false);
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
    }
}
