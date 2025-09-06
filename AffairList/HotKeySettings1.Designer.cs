namespace AffairList
{
    partial class HotKeySettings1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotKeySettings1));
            HotKeySettingsLab = new Label();
            NameBackground = new Panel();
            MinimizeButton = new Label();
            CloseButton = new Label();
            panel1 = new Panel();
            BackKeyType = new Label();
            BackKeyLab = new Label();
            CloseKeyType = new Label();
            CloseKeyLab = new Label();
            ConfirmButton = new Button();
            ResetButton = new Button();
            BackButton = new Button();
            NameBackground.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // HotKeySettingsLab
            // 
            HotKeySettingsLab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            HotKeySettingsLab.AutoSize = true;
            HotKeySettingsLab.Font = new Font("Unispace", 30F);
            HotKeySettingsLab.Location = new Point(125, 50);
            HotKeySettingsLab.Name = "HotKeySettingsLab";
            HotKeySettingsLab.Size = new Size(188, 48);
            HotKeySettingsLab.TabIndex = 1;
            HotKeySettingsLab.Text = "HotKeys";
            HotKeySettingsLab.TextAlign = ContentAlignment.MiddleCenter;
            HotKeySettingsLab.MouseDown += HotKeySettingsLab_MouseDown;
            HotKeySettingsLab.MouseMove += HotKeySettingsLab_MouseMove;
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(HotKeySettingsLab);
            NameBackground.Dock = DockStyle.Top;
            NameBackground.Location = new Point(0, 0);
            NameBackground.Name = "NameBackground";
            NameBackground.Size = new Size(440, 142);
            NameBackground.TabIndex = 0;
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
            // 
            // MinimizeButton
            // 
            MinimizeButton.AutoSize = true;
            MinimizeButton.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MinimizeButton.Location = new Point(372, 0);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(31, 33);
            MinimizeButton.TabIndex = 3;
            MinimizeButton.Text = "-";
            MinimizeButton.Click += MinimizeButton_Click;
            MinimizeButton.MouseEnter += MinimizeButton_MouseEnter;
            MinimizeButton.MouseLeave += MinimizeButton_MouseLeave;
            MinimizeButton.MouseUp += MinimizeButton_MouseUp;
            // 
            // CloseButton
            // 
            CloseButton.AutoSize = true;
            CloseButton.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.Location = new Point(409, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(31, 33);
            CloseButton.TabIndex = 2;
            CloseButton.Text = "X";
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(BackKeyType);
            panel1.Controls.Add(BackKeyLab);
            panel1.Controls.Add(CloseKeyType);
            panel1.Controls.Add(CloseKeyLab);
            panel1.Controls.Add(ConfirmButton);
            panel1.Controls.Add(NameBackground);
            panel1.Controls.Add(ResetButton);
            panel1.Controls.Add(BackButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(440, 511);
            panel1.TabIndex = 1;
            // 
            // BackKeyType
            // 
            BackKeyType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BackKeyType.Font = new Font("Unispace", 13F);
            BackKeyType.ForeColor = Color.White;
            BackKeyType.Location = new Point(286, 191);
            BackKeyType.Name = "BackKeyType";
            BackKeyType.RightToLeft = RightToLeft.No;
            BackKeyType.Size = new Size(121, 21);
            BackKeyType.TabIndex = 15;
            BackKeyType.Text = "F6";
            BackKeyType.TextAlign = ContentAlignment.BottomRight;
            BackKeyType.DoubleClick += BackKeyType_DoubleClick;
            BackKeyType.MouseEnter += BackKeyType_MouseEnter;
            BackKeyType.MouseLeave += BackKeyType_MouseLeave;
            BackKeyType.MouseUp += BackKeyType_MouseUp;
            // 
            // BackKeyLab
            // 
            BackKeyLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BackKeyLab.AutoSize = true;
            BackKeyLab.Font = new Font("Unispace", 13F);
            BackKeyLab.ForeColor = Color.White;
            BackKeyLab.Location = new Point(12, 191);
            BackKeyLab.Name = "BackKeyLab";
            BackKeyLab.Size = new Size(98, 21);
            BackKeyLab.TabIndex = 14;
            BackKeyLab.Text = "Back key";
            BackKeyLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CloseKeyType
            // 
            CloseKeyType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseKeyType.Font = new Font("Unispace", 13F);
            CloseKeyType.ForeColor = Color.White;
            CloseKeyType.Location = new Point(286, 156);
            CloseKeyType.Name = "CloseKeyType";
            CloseKeyType.RightToLeft = RightToLeft.No;
            CloseKeyType.Size = new Size(121, 21);
            CloseKeyType.TabIndex = 13;
            CloseKeyType.Text = "F7";
            CloseKeyType.TextAlign = ContentAlignment.BottomRight;
            CloseKeyType.DoubleClick += CloseKeyType_DoubleClick;
            CloseKeyType.MouseEnter += CloseKeyType_MouseEnter;
            CloseKeyType.MouseLeave += CloseKeyType_MouseLeave;
            CloseKeyType.MouseUp += CloseKeyType_MouseUp;
            // 
            // CloseKeyLab
            // 
            CloseKeyLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseKeyLab.AutoSize = true;
            CloseKeyLab.Font = new Font("Unispace", 13F);
            CloseKeyLab.ForeColor = Color.White;
            CloseKeyLab.Location = new Point(12, 156);
            CloseKeyLab.Name = "CloseKeyLab";
            CloseKeyLab.Size = new Size(109, 21);
            CloseKeyLab.TabIndex = 12;
            CloseKeyLab.Text = "Close key";
            CloseKeyLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ConfirmButton
            // 
            ConfirmButton.BackColor = Color.FromArgb(173, 102, 213);
            ConfirmButton.FlatStyle = FlatStyle.Flat;
            ConfirmButton.Location = new Point(191, 472);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.Size = new Size(75, 27);
            ConfirmButton.TabIndex = 11;
            ConfirmButton.Text = "Confirm";
            ConfirmButton.UseVisualStyleBackColor = false;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.FromArgb(173, 102, 213);
            ResetButton.FlatStyle = FlatStyle.Flat;
            ResetButton.Location = new Point(272, 472);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(75, 27);
            ResetButton.TabIndex = 10;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.FlatStyle = FlatStyle.Flat;
            BackButton.Location = new Point(353, 472);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(75, 27);
            BackButton.TabIndex = 9;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // HotKeySettings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 511);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HotKeySettings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HotKeySettings";
            NameBackground.ResumeLayout(false);
            NameBackground.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label HotKeySettingsLab;
        private Panel NameBackground;
        private Label MinimizeButton;
        private Label CloseButton;
        private Panel panel1;
        private Button ConfirmButton;
        private Button ResetButton;
        private Button BackButton;
        private Label BackKeyLab;
        private Label CloseKeyType;
        private Label CloseKeyLab;
        private Label BackKeyType;
    }
}