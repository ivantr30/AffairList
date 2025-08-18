namespace AffairList
{
    partial class AffairList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AffairList));
            panel1 = new Panel();
            ErrorHelpLab = new Label();
            ChangeProfileButton = new Button();
            HotKeyButton = new Button();
            SettingsButton = new Button();
            OpenListButton = new Button();
            ReplaceAffairListButton = new Button();
            ChangeListButton = new Button();
            ClearListButton = new Button();
            NameBackground = new Panel();
            MinimizeButton = new Label();
            CloseButton = new Label();
            AffairListLab = new Label();
            panel1.SuspendLayout();
            NameBackground.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(ErrorHelpLab);
            panel1.Controls.Add(ChangeProfileButton);
            panel1.Controls.Add(HotKeyButton);
            panel1.Controls.Add(SettingsButton);
            panel1.Controls.Add(OpenListButton);
            panel1.Controls.Add(ReplaceAffairListButton);
            panel1.Controls.Add(ChangeListButton);
            panel1.Controls.Add(ClearListButton);
            panel1.Controls.Add(NameBackground);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 482);
            panel1.TabIndex = 0;
            // 
            // ErrorHelpLab
            // 
            ErrorHelpLab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ErrorHelpLab.AutoSize = true;
            ErrorHelpLab.Font = new Font("Unispace", 10F);
            ErrorHelpLab.ForeColor = SystemColors.ButtonHighlight;
            ErrorHelpLab.Location = new Point(25, 88);
            ErrorHelpLab.Name = "ErrorHelpLab";
            ErrorHelpLab.Size = new Size(403, 16);
            ErrorHelpLab.TabIndex = 4;
            ErrorHelpLab.Text = "Found an error? - ivanchikinator30 @ivantr30";
            ErrorHelpLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ChangeProfileButton
            // 
            ChangeProfileButton.BackColor = Color.FromArgb(173, 102, 213);
            ChangeProfileButton.FlatAppearance.BorderSize = 0;
            ChangeProfileButton.FlatStyle = FlatStyle.Flat;
            ChangeProfileButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            ChangeProfileButton.ForeColor = SystemColors.ButtonHighlight;
            ChangeProfileButton.Location = new Point(315, 125);
            ChangeProfileButton.Name = "ChangeProfileButton";
            ChangeProfileButton.Size = new Size(100, 100);
            ChangeProfileButton.TabIndex = 8;
            ChangeProfileButton.Text = "Change Profile";
            ChangeProfileButton.UseVisualStyleBackColor = false;
            ChangeProfileButton.Click += ChangeProfileButton_Click;
            // 
            // HotKeyButton
            // 
            HotKeyButton.BackColor = Color.FromArgb(173, 102, 213);
            HotKeyButton.FlatAppearance.BorderSize = 0;
            HotKeyButton.FlatStyle = FlatStyle.Flat;
            HotKeyButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            HotKeyButton.ForeColor = SystemColors.ButtonHighlight;
            HotKeyButton.Location = new Point(35, 125);
            HotKeyButton.Name = "HotKeyButton";
            HotKeyButton.Size = new Size(100, 100);
            HotKeyButton.TabIndex = 6;
            HotKeyButton.Text = "Hotkey Settings";
            HotKeyButton.UseVisualStyleBackColor = false;
            HotKeyButton.Click += HotKeyButton_Click;
            // 
            // SettingsButton
            // 
            SettingsButton.BackColor = Color.FromArgb(173, 102, 213);
            SettingsButton.FlatAppearance.BorderSize = 0;
            SettingsButton.FlatStyle = FlatStyle.Flat;
            SettingsButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            SettingsButton.ForeColor = SystemColors.ButtonHighlight;
            SettingsButton.Location = new Point(175, 125);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(100, 100);
            SettingsButton.TabIndex = 5;
            SettingsButton.Text = "Settings-Customize";
            SettingsButton.UseVisualStyleBackColor = false;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // OpenListButton
            // 
            OpenListButton.BackColor = Color.FromArgb(173, 102, 213);
            OpenListButton.FlatAppearance.BorderSize = 0;
            OpenListButton.FlatStyle = FlatStyle.Flat;
            OpenListButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            OpenListButton.ForeColor = SystemColors.ButtonHighlight;
            OpenListButton.Location = new Point(35, 370);
            OpenListButton.Name = "OpenListButton";
            OpenListButton.Size = new Size(380, 100);
            OpenListButton.TabIndex = 4;
            OpenListButton.Text = "OpenList";
            OpenListButton.UseVisualStyleBackColor = false;
            OpenListButton.Click += OpenListButton_Click;
            // 
            // ReplaceAffairListButton
            // 
            ReplaceAffairListButton.BackColor = Color.FromArgb(173, 102, 213);
            ReplaceAffairListButton.FlatAppearance.BorderSize = 0;
            ReplaceAffairListButton.FlatStyle = FlatStyle.Flat;
            ReplaceAffairListButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            ReplaceAffairListButton.ForeColor = SystemColors.ButtonHighlight;
            ReplaceAffairListButton.Location = new Point(315, 250);
            ReplaceAffairListButton.Name = "ReplaceAffairListButton";
            ReplaceAffairListButton.Size = new Size(100, 100);
            ReplaceAffairListButton.TabIndex = 3;
            ReplaceAffairListButton.Text = "ReplaceList";
            ReplaceAffairListButton.UseVisualStyleBackColor = false;
            ReplaceAffairListButton.Click += ReplaceAffairListButton_Click;
            // 
            // ChangeListButton
            // 
            ChangeListButton.BackColor = Color.FromArgb(173, 102, 213);
            ChangeListButton.FlatAppearance.BorderSize = 0;
            ChangeListButton.FlatStyle = FlatStyle.Flat;
            ChangeListButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            ChangeListButton.ForeColor = SystemColors.ButtonHighlight;
            ChangeListButton.Location = new Point(175, 250);
            ChangeListButton.Name = "ChangeListButton";
            ChangeListButton.Size = new Size(100, 100);
            ChangeListButton.TabIndex = 2;
            ChangeListButton.Text = "ChangeList";
            ChangeListButton.UseVisualStyleBackColor = false;
            ChangeListButton.Click += ChangeListButton_Click;
            // 
            // ClearListButton
            // 
            ClearListButton.BackColor = Color.FromArgb(173, 102, 213);
            ClearListButton.FlatAppearance.BorderSize = 0;
            ClearListButton.FlatStyle = FlatStyle.Flat;
            ClearListButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            ClearListButton.ForeColor = SystemColors.ButtonHighlight;
            ClearListButton.Location = new Point(35, 250);
            ClearListButton.Name = "ClearListButton";
            ClearListButton.Size = new Size(100, 100);
            ClearListButton.TabIndex = 1;
            ClearListButton.Text = "ClearList";
            ClearListButton.UseVisualStyleBackColor = false;
            ClearListButton.Click += ClearListButton_Click;
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(AffairListLab);
            NameBackground.Dock = DockStyle.Top;
            NameBackground.Location = new Point(0, 0);
            NameBackground.Name = "NameBackground";
            NameBackground.Size = new Size(450, 75);
            NameBackground.TabIndex = 0;
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
            // 
            // MinimizeButton
            // 
            MinimizeButton.AutoSize = true;
            MinimizeButton.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MinimizeButton.Location = new Point(384, 0);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(31, 33);
            MinimizeButton.TabIndex = 3;
            MinimizeButton.Text = "-";
            MinimizeButton.Click += MinimizeButton_Click;
            MinimizeButton.MouseEnter += MinimizeButton_MouseEnter;
            MinimizeButton.MouseLeave += MinimizeButton_MouseLeave;
            // 
            // CloseButton
            // 
            CloseButton.AutoSize = true;
            CloseButton.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.Location = new Point(419, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(31, 33);
            CloseButton.TabIndex = 2;
            CloseButton.Text = "X";
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            // 
            // AffairListLab
            // 
            AffairListLab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AffairListLab.AutoSize = true;
            AffairListLab.Font = new Font("Unispace", 30F);
            AffairListLab.Location = new Point(100, 15);
            AffairListLab.Name = "AffairListLab";
            AffairListLab.Size = new Size(260, 48);
            AffairListLab.TabIndex = 1;
            AffairListLab.Text = "AffairList";
            AffairListLab.TextAlign = ContentAlignment.MiddleCenter;
            AffairListLab.MouseDown += AffairListLab_MouseDown;
            AffairListLab.MouseMove += AffairListLab_MouseMove;
            // 
            // AffairList
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 482);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "AffairList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AffairList";
            FormClosing += AffairList_FormClosing;
            Load += AffairList_Load;
            Shown += AffairList_Shown;
            KeyDown += AffairList_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            NameBackground.ResumeLayout(false);
            NameBackground.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel NameBackground;
        private Label AffairListLab;
        private Button ReplaceAffairListButton;
        private Button ChangeListButton;
        private Button ClearListButton;
        private Label CloseButton;
        private Button OpenListButton;
        private Button SettingsButton;
        private Label MinimizeButton;
        private Button ChangeProfileButton;
        private Button HotKeyButton;
        private Label ErrorHelpLab;
    }
}
