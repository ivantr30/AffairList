namespace AffairList
{
    partial class Settings
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
            NameBackground = new Panel();
            CloseButton = new Label();
            SettingsLab = new Label();
            panel1 = new Panel();
            MusicVolumeLab = new Label();
            AutostartLab = new Label();
            StateLab = new Label();
            MusicStateLab = new Label();
            ListBgTextColorLab = new Label();
            ListTextColorLab = new Label();
            LocationLab = new Label();
            ListLocationLab = new Label();
            ConfirmButton = new Button();
            ResetButton = new Button();
            BackButton = new Button();
            NameBackground.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(SettingsLab);
            NameBackground.Dock = DockStyle.Top;
            NameBackground.Location = new Point(0, 0);
            NameBackground.Name = "NameBackground";
            NameBackground.Size = new Size(440, 142);
            NameBackground.TabIndex = 0;
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
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
            // SettingsLab
            // 
            SettingsLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SettingsLab.AutoSize = true;
            SettingsLab.Font = new Font("Unispace", 30F);
            SettingsLab.Location = new Point(125, 50);
            SettingsLab.Name = "SettingsLab";
            SettingsLab.Size = new Size(212, 48);
            SettingsLab.TabIndex = 1;
            SettingsLab.Text = "Settings";
            SettingsLab.TextAlign = ContentAlignment.MiddleCenter;
            SettingsLab.MouseDown += SettingsLab_MouseDown;
            SettingsLab.MouseMove += SettingsLab_MouseMove;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(MusicVolumeLab);
            panel1.Controls.Add(AutostartLab);
            panel1.Controls.Add(StateLab);
            panel1.Controls.Add(MusicStateLab);
            panel1.Controls.Add(ListBgTextColorLab);
            panel1.Controls.Add(ListTextColorLab);
            panel1.Controls.Add(LocationLab);
            panel1.Controls.Add(ListLocationLab);
            panel1.Controls.Add(ConfirmButton);
            panel1.Controls.Add(ResetButton);
            panel1.Controls.Add(BackButton);
            panel1.Controls.Add(NameBackground);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(440, 511);
            panel1.TabIndex = 1;
            // 
            // MusicVolumeLab
            // 
            MusicVolumeLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MusicVolumeLab.AutoSize = true;
            MusicVolumeLab.Font = new Font("Unispace", 13F);
            MusicVolumeLab.ForeColor = Color.White;
            MusicVolumeLab.Location = new Point(12, 295);
            MusicVolumeLab.Name = "MusicVolumeLab";
            MusicVolumeLab.Size = new Size(142, 21);
            MusicVolumeLab.TabIndex = 15;
            MusicVolumeLab.Text = "Music volume";
            MusicVolumeLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AutostartLab
            // 
            AutostartLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AutostartLab.AutoSize = true;
            AutostartLab.Font = new Font("Unispace", 13F);
            AutostartLab.ForeColor = Color.White;
            AutostartLab.Location = new Point(12, 330);
            AutostartLab.Name = "AutostartLab";
            AutostartLab.Size = new Size(175, 21);
            AutostartLab.TabIndex = 14;
            AutostartLab.Text = "Autostart state";
            AutostartLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StateLab
            // 
            StateLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            StateLab.AutoSize = true;
            StateLab.Font = new Font("Unispace", 13F);
            StateLab.ForeColor = Color.White;
            StateLab.Location = new Point(364, 260);
            StateLab.Name = "StateLab";
            StateLab.Size = new Size(32, 21);
            StateLab.TabIndex = 13;
            StateLab.Text = "On";
            StateLab.TextAlign = ContentAlignment.MiddleCenter;
            StateLab.MouseDown += StateLab_MouseDown;
            StateLab.MouseEnter += StateLab_MouseEnter;
            StateLab.MouseLeave += StateLab_MouseLeave;
            StateLab.MouseUp += StateLab_MouseUp;
            // 
            // MusicStateLab
            // 
            MusicStateLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MusicStateLab.AutoSize = true;
            MusicStateLab.Font = new Font("Unispace", 13F);
            MusicStateLab.ForeColor = Color.White;
            MusicStateLab.Location = new Point(12, 260);
            MusicStateLab.Name = "MusicStateLab";
            MusicStateLab.Size = new Size(131, 21);
            MusicStateLab.TabIndex = 12;
            MusicStateLab.Text = "Music state";
            MusicStateLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ListBgTextColorLab
            // 
            ListBgTextColorLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ListBgTextColorLab.AutoSize = true;
            ListBgTextColorLab.Font = new Font("Unispace", 13F);
            ListBgTextColorLab.ForeColor = Color.White;
            ListBgTextColorLab.Location = new Point(12, 225);
            ListBgTextColorLab.Name = "ListBgTextColorLab";
            ListBgTextColorLab.Size = new Size(296, 21);
            ListBgTextColorLab.TabIndex = 11;
            ListBgTextColorLab.Text = "List background text color";
            ListBgTextColorLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ListTextColorLab
            // 
            ListTextColorLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ListTextColorLab.AutoSize = true;
            ListTextColorLab.Font = new Font("Unispace", 13F);
            ListTextColorLab.ForeColor = Color.White;
            ListTextColorLab.Location = new Point(12, 190);
            ListTextColorLab.Name = "ListTextColorLab";
            ListTextColorLab.Size = new Size(175, 21);
            ListTextColorLab.TabIndex = 10;
            ListTextColorLab.Text = "List text color";
            ListTextColorLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LocationLab
            // 
            LocationLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LocationLab.AutoSize = true;
            LocationLab.Font = new Font("Unispace", 13F);
            LocationLab.ForeColor = Color.White;
            LocationLab.Location = new Point(353, 154);
            LocationLab.Name = "LocationLab";
            LocationLab.Size = new Size(54, 21);
            LocationLab.TabIndex = 9;
            LocationLab.Text = "0, 0";
            LocationLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ListLocationLab
            // 
            ListLocationLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ListLocationLab.AutoSize = true;
            ListLocationLab.Font = new Font("Unispace", 13F);
            ListLocationLab.ForeColor = Color.White;
            ListLocationLab.Location = new Point(12, 155);
            ListLocationLab.Name = "ListLocationLab";
            ListLocationLab.Size = new Size(197, 21);
            ListLocationLab.TabIndex = 3;
            ListLocationLab.Text = "List x,y location";
            ListLocationLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ConfirmButton
            // 
            ConfirmButton.BackColor = Color.FromArgb(173, 102, 213);
            ConfirmButton.FlatStyle = FlatStyle.Flat;
            ConfirmButton.Location = new Point(191, 472);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.Size = new Size(75, 27);
            ConfirmButton.TabIndex = 8;
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
            ResetButton.TabIndex = 7;
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
            BackButton.TabIndex = 6;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 511);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "Settings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            KeyDown += Settings_KeyDown;
            NameBackground.ResumeLayout(false);
            NameBackground.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel NameBackground;
        private Label CloseButton;
        private Label SettingsLab;
        private Panel panel1;
        private Button ConfirmButton;
        private Button ResetButton;
        private Button BackButton;
        private Label ListLocationLab;
        private Label ListBgTextColorLab;
        private Label ListTextColorLab;
        private Label LocationLab;
        private Label MusicStateLab;
        private Label StateLab;
        private Label MusicVolumeLab;
        private Label AutostartLab;
    }
}