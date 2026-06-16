namespace MPL.X4.TradeData.UI.Forms
{
    partial class ShipViewerForm
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
            NameLabel = new Label();
            NameTextBox = new TextBox();
            ModelTextBox = new TextBox();
            ModelLabel = new Label();
            PositionXTextBox = new TextBox();
            TransformGroupBox = new GroupBox();
            RotationYawTextBox = new TextBox();
            RotationRollTextBox = new TextBox();
            RotationPitchTextBox = new TextBox();
            PositionZTextBox = new TextBox();
            PositionYTextBox = new TextBox();
            SectorLabel = new Label();
            SectorTextBox = new TextBox();
            RotationLabel = new Label();
            PositionLabel = new Label();
            OwnerLabel = new Label();
            OwnerTextBox = new TextBox();
            CanBeCapturedCheckBox = new CheckBox();
            IsWreckCheckBox = new CheckBox();
            CargoLabel = new Label();
            IsKnownCheckBox = new CheckBox();
            CargoListView = new ListView();
            CargoListView_Ware = new ColumnHeader();
            CargoListView_Amount = new ColumnHeader();
            CargoListView_Value = new ColumnHeader();
            ModificationsListView = new ListView();
            ModificationsListView_Type = new ColumnHeader();
            ModificationsListView_Name = new ColumnHeader();
            ModificationsListView_Quality = new ColumnHeader();
            ModificationsLabel = new Label();
            OkButton = new Button();
            ClassTextBox = new TextBox();
            ClassLabel = new Label();
            CodeTextBox = new TextBox();
            TransformGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(23, 15);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(42, 15);
            NameLabel.TabIndex = 0;
            NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            NameTextBox.BackColor = SystemColors.Window;
            NameTextBox.Location = new Point(71, 12);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.ReadOnly = true;
            NameTextBox.Size = new Size(168, 23);
            NameTextBox.TabIndex = 1;
            // 
            // ModelTextBox
            // 
            ModelTextBox.BackColor = SystemColors.Window;
            ModelTextBox.Location = new Point(71, 70);
            ModelTextBox.Name = "ModelTextBox";
            ModelTextBox.ReadOnly = true;
            ModelTextBox.Size = new Size(255, 23);
            ModelTextBox.TabIndex = 3;
            // 
            // ModelLabel
            // 
            ModelLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ModelLabel.AutoSize = true;
            ModelLabel.Location = new Point(21, 73);
            ModelLabel.Name = "ModelLabel";
            ModelLabel.Size = new Size(44, 15);
            ModelLabel.TabIndex = 2;
            ModelLabel.Text = "Model:";
            // 
            // PositionXTextBox
            // 
            PositionXTextBox.BackColor = SystemColors.Window;
            PositionXTextBox.Location = new Point(68, 51);
            PositionXTextBox.Name = "PositionXTextBox";
            PositionXTextBox.ReadOnly = true;
            PositionXTextBox.Size = new Size(49, 23);
            PositionXTextBox.TabIndex = 3;
            // 
            // TransformGroupBox
            // 
            TransformGroupBox.Controls.Add(RotationYawTextBox);
            TransformGroupBox.Controls.Add(RotationRollTextBox);
            TransformGroupBox.Controls.Add(RotationPitchTextBox);
            TransformGroupBox.Controls.Add(PositionZTextBox);
            TransformGroupBox.Controls.Add(PositionYTextBox);
            TransformGroupBox.Controls.Add(SectorLabel);
            TransformGroupBox.Controls.Add(SectorTextBox);
            TransformGroupBox.Controls.Add(RotationLabel);
            TransformGroupBox.Controls.Add(PositionLabel);
            TransformGroupBox.Controls.Add(PositionXTextBox);
            TransformGroupBox.Location = new Point(12, 128);
            TransformGroupBox.Name = "TransformGroupBox";
            TransformGroupBox.Size = new Size(314, 113);
            TransformGroupBox.TabIndex = 6;
            TransformGroupBox.TabStop = false;
            TransformGroupBox.Text = "Location";
            // 
            // RotationYawTextBox
            // 
            RotationYawTextBox.BackColor = SystemColors.Window;
            RotationYawTextBox.Location = new Point(178, 80);
            RotationYawTextBox.Name = "RotationYawTextBox";
            RotationYawTextBox.ReadOnly = true;
            RotationYawTextBox.Size = new Size(49, 23);
            RotationYawTextBox.TabIndex = 9;
            // 
            // RotationRollTextBox
            // 
            RotationRollTextBox.BackColor = SystemColors.Window;
            RotationRollTextBox.Location = new Point(123, 80);
            RotationRollTextBox.Name = "RotationRollTextBox";
            RotationRollTextBox.ReadOnly = true;
            RotationRollTextBox.Size = new Size(49, 23);
            RotationRollTextBox.TabIndex = 8;
            // 
            // RotationPitchTextBox
            // 
            RotationPitchTextBox.BackColor = SystemColors.Window;
            RotationPitchTextBox.Location = new Point(68, 80);
            RotationPitchTextBox.Name = "RotationPitchTextBox";
            RotationPitchTextBox.ReadOnly = true;
            RotationPitchTextBox.Size = new Size(49, 23);
            RotationPitchTextBox.TabIndex = 7;
            // 
            // PositionZTextBox
            // 
            PositionZTextBox.BackColor = SystemColors.Window;
            PositionZTextBox.Location = new Point(178, 51);
            PositionZTextBox.Name = "PositionZTextBox";
            PositionZTextBox.ReadOnly = true;
            PositionZTextBox.Size = new Size(49, 23);
            PositionZTextBox.TabIndex = 5;
            // 
            // PositionYTextBox
            // 
            PositionYTextBox.BackColor = SystemColors.Window;
            PositionYTextBox.Location = new Point(123, 51);
            PositionYTextBox.Name = "PositionYTextBox";
            PositionYTextBox.ReadOnly = true;
            PositionYTextBox.Size = new Size(49, 23);
            PositionYTextBox.TabIndex = 4;
            // 
            // SectorLabel
            // 
            SectorLabel.AutoSize = true;
            SectorLabel.Location = new Point(19, 25);
            SectorLabel.Name = "SectorLabel";
            SectorLabel.Size = new Size(43, 15);
            SectorLabel.TabIndex = 0;
            SectorLabel.Text = "Sector:";
            // 
            // SectorTextBox
            // 
            SectorTextBox.BackColor = SystemColors.Window;
            SectorTextBox.Location = new Point(68, 22);
            SectorTextBox.Name = "SectorTextBox";
            SectorTextBox.ReadOnly = true;
            SectorTextBox.Size = new Size(202, 23);
            SectorTextBox.TabIndex = 1;
            // 
            // RotationLabel
            // 
            RotationLabel.AutoSize = true;
            RotationLabel.Location = new Point(7, 83);
            RotationLabel.Name = "RotationLabel";
            RotationLabel.Size = new Size(55, 15);
            RotationLabel.TabIndex = 6;
            RotationLabel.Text = "Rotation:";
            RotationLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PositionLabel
            // 
            PositionLabel.AutoSize = true;
            PositionLabel.Location = new Point(9, 54);
            PositionLabel.Name = "PositionLabel";
            PositionLabel.Size = new Size(53, 15);
            PositionLabel.TabIndex = 2;
            PositionLabel.Text = "Position:";
            // 
            // OwnerLabel
            // 
            OwnerLabel.AutoSize = true;
            OwnerLabel.Location = new Point(20, 102);
            OwnerLabel.Name = "OwnerLabel";
            OwnerLabel.Size = new Size(45, 15);
            OwnerLabel.TabIndex = 4;
            OwnerLabel.Text = "Owner:";
            // 
            // OwnerTextBox
            // 
            OwnerTextBox.BackColor = SystemColors.Window;
            OwnerTextBox.Location = new Point(71, 99);
            OwnerTextBox.Name = "OwnerTextBox";
            OwnerTextBox.ReadOnly = true;
            OwnerTextBox.Size = new Size(228, 23);
            OwnerTextBox.TabIndex = 5;
            // 
            // CanBeCapturedCheckBox
            // 
            CanBeCapturedCheckBox.AutoSize = true;
            CanBeCapturedCheckBox.Enabled = false;
            CanBeCapturedCheckBox.Location = new Point(32, 247);
            CanBeCapturedCheckBox.Name = "CanBeCapturedCheckBox";
            CanBeCapturedCheckBox.Size = new Size(113, 19);
            CanBeCapturedCheckBox.TabIndex = 7;
            CanBeCapturedCheckBox.Text = "Can be captured";
            CanBeCapturedCheckBox.UseVisualStyleBackColor = true;
            // 
            // IsWreckCheckBox
            // 
            IsWreckCheckBox.AutoSize = true;
            IsWreckCheckBox.Enabled = false;
            IsWreckCheckBox.Location = new Point(151, 247);
            IsWreckCheckBox.Name = "IsWreckCheckBox";
            IsWreckCheckBox.Size = new Size(77, 19);
            IsWreckCheckBox.TabIndex = 8;
            IsWreckCheckBox.Text = "Is a wreck";
            IsWreckCheckBox.UseVisualStyleBackColor = true;
            // 
            // CargoLabel
            // 
            CargoLabel.AutoSize = true;
            CargoLabel.Location = new Point(12, 269);
            CargoLabel.Name = "CargoLabel";
            CargoLabel.Size = new Size(42, 15);
            CargoLabel.TabIndex = 10;
            CargoLabel.Text = "Cargo:";
            // 
            // IsKnownCheckBox
            // 
            IsKnownCheckBox.AutoSize = true;
            IsKnownCheckBox.Enabled = false;
            IsKnownCheckBox.Location = new Point(234, 247);
            IsKnownCheckBox.Name = "IsKnownCheckBox";
            IsKnownCheckBox.Size = new Size(73, 19);
            IsKnownCheckBox.TabIndex = 9;
            IsKnownCheckBox.Text = "Is known";
            IsKnownCheckBox.UseVisualStyleBackColor = true;
            // 
            // CargoListView
            // 
            CargoListView.Columns.AddRange(new ColumnHeader[] { CargoListView_Ware, CargoListView_Amount, CargoListView_Value });
            CargoListView.Location = new Point(12, 287);
            CargoListView.Name = "CargoListView";
            CargoListView.Size = new Size(314, 96);
            CargoListView.TabIndex = 11;
            CargoListView.UseCompatibleStateImageBehavior = false;
            CargoListView.View = View.Details;
            // 
            // CargoListView_Ware
            // 
            CargoListView_Ware.Text = "Ware";
            CargoListView_Ware.Width = 140;
            // 
            // CargoListView_Amount
            // 
            CargoListView_Amount.Text = "Amount";
            CargoListView_Amount.TextAlign = HorizontalAlignment.Right;
            CargoListView_Amount.Width = 70;
            // 
            // CargoListView_Value
            // 
            CargoListView_Value.Text = "Value";
            CargoListView_Value.TextAlign = HorizontalAlignment.Right;
            CargoListView_Value.Width = 80;
            // 
            // ModificationsListView
            // 
            ModificationsListView.Columns.AddRange(new ColumnHeader[] { ModificationsListView_Type, ModificationsListView_Name, ModificationsListView_Quality });
            ModificationsListView.Location = new Point(12, 404);
            ModificationsListView.Name = "ModificationsListView";
            ModificationsListView.Size = new Size(314, 96);
            ModificationsListView.TabIndex = 13;
            ModificationsListView.UseCompatibleStateImageBehavior = false;
            ModificationsListView.View = View.Details;
            // 
            // ModificationsListView_Type
            // 
            ModificationsListView_Type.Text = "Type";
            ModificationsListView_Type.Width = 80;
            // 
            // ModificationsListView_Name
            // 
            ModificationsListView_Name.Text = "Name";
            ModificationsListView_Name.Width = 150;
            // 
            // ModificationsListView_Quality
            // 
            ModificationsListView_Quality.Text = "Quality";
            ModificationsListView_Quality.TextAlign = HorizontalAlignment.Right;
            ModificationsListView_Quality.Width = 50;
            // 
            // ModificationsLabel
            // 
            ModificationsLabel.AutoSize = true;
            ModificationsLabel.Location = new Point(12, 386);
            ModificationsLabel.Name = "ModificationsLabel";
            ModificationsLabel.Size = new Size(83, 15);
            ModificationsLabel.TabIndex = 12;
            ModificationsLabel.Text = "Modifications:";
            // 
            // OkButton
            // 
            OkButton.Location = new Point(251, 506);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(75, 23);
            OkButton.TabIndex = 14;
            OkButton.Text = "OK";
            OkButton.UseVisualStyleBackColor = true;
            // 
            // ClassTextBox
            // 
            ClassTextBox.BackColor = SystemColors.Window;
            ClassTextBox.Location = new Point(71, 41);
            ClassTextBox.Name = "ClassTextBox";
            ClassTextBox.ReadOnly = true;
            ClassTextBox.Size = new Size(90, 23);
            ClassTextBox.TabIndex = 16;
            // 
            // ClassLabel
            // 
            ClassLabel.AutoSize = true;
            ClassLabel.Location = new Point(28, 44);
            ClassLabel.Name = "ClassLabel";
            ClassLabel.Size = new Size(37, 15);
            ClassLabel.TabIndex = 15;
            ClassLabel.Text = "Class:";
            // 
            // CodeTextBox
            // 
            CodeTextBox.BackColor = SystemColors.Window;
            CodeTextBox.Location = new Point(245, 12);
            CodeTextBox.Name = "CodeTextBox";
            CodeTextBox.ReadOnly = true;
            CodeTextBox.Size = new Size(81, 23);
            CodeTextBox.TabIndex = 17;
            // 
            // ShipViewerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(338, 534);
            Controls.Add(CodeTextBox);
            Controls.Add(ClassTextBox);
            Controls.Add(ClassLabel);
            Controls.Add(OkButton);
            Controls.Add(ModificationsListView);
            Controls.Add(ModificationsLabel);
            Controls.Add(CargoListView);
            Controls.Add(IsKnownCheckBox);
            Controls.Add(CargoLabel);
            Controls.Add(IsWreckCheckBox);
            Controls.Add(CanBeCapturedCheckBox);
            Controls.Add(OwnerLabel);
            Controls.Add(OwnerTextBox);
            Controls.Add(TransformGroupBox);
            Controls.Add(ModelLabel);
            Controls.Add(ModelTextBox);
            Controls.Add(NameTextBox);
            Controls.Add(NameLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ShipViewerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ship Viewer";
            TransformGroupBox.ResumeLayout(false);
            TransformGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label NameLabel;
        private TextBox NameTextBox;
        private TextBox ModelTextBox;
        private Label ModelLabel;
        private TextBox PositionXTextBox;
        private GroupBox TransformGroupBox;
        private Label PositionLabel;
        private Label RotationLabel;
        private Label SectorLabel;
        private TextBox SectorTextBox;
        private TextBox RotationYawTextBox;
        private TextBox RotationRollTextBox;
        private TextBox RotationPitchTextBox;
        private TextBox PositionZTextBox;
        private TextBox PositionYTextBox;
        private Label OwnerLabel;
        private TextBox OwnerTextBox;
        private CheckBox CanBeCapturedCheckBox;
        private CheckBox IsWreckCheckBox;
        private Label CargoLabel;
        private CheckBox IsKnownCheckBox;
        private ListView CargoListView;
        private ColumnHeader CargoListView_Ware;
        private ColumnHeader CargoListView_Amount;
        private ColumnHeader CargoListView_Value;
        private ListView ModificationsListView;
        private ColumnHeader ModificationsListView_Type;
        private ColumnHeader ModificationsListView_Name;
        private ColumnHeader ModificationsListView_Quality;
        private Label ModificationsLabel;
        private Button OkButton;
        private TextBox ClassTextBox;
        private Label ClassLabel;
        private TextBox CodeTextBox;
    }
}