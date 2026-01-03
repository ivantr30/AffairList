namespace AffairList
{
    partial class HotKeySettingsManager
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                _hotkeysUpdater = null;
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            HotKeySettingsLab = new Label();
            NameBackground = new Panel();
            MinimizeButton = new Label();
            CloseButton = new Label();
            BackKeyType = new Label();
            BackKeyLab = new Label();
            CloseKeyType = new Label();
            CloseKeyLab = new Label();
            panel1 = new Panel();
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
            HotKeySettingsLab.Font = new Font("Microsoft Sans Serif", 30F);
            HotKeySettingsLab.ImeMode = ImeMode.NoControl;
            HotKeySettingsLab.Location = new Point(143, 59);
            HotKeySettingsLab.Name = "HotKeySettingsLab";
            HotKeySettingsLab.Size = new Size(214, 58);
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
            NameBackground.Margin = new Padding(3, 4, 3, 4);
            NameBackground.Name = "NameBackground";
            NameBackground.Size = new Size(503, 167);
            NameBackground.TabIndex = 0;
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
            // 
            // MinimizeButton
            // 
            MinimizeButton.AutoSize = true;
            MinimizeButton.Font = new Font("Microsoft Sans Serif", 25F, FontStyle.Bold);
            MinimizeButton.ImeMode = ImeMode.NoControl;
            MinimizeButton.Location = new Point(413, 0);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(35, 48);
            MinimizeButton.TabIndex = 3;
            MinimizeButton.Text = "-";
            MinimizeButton.Click += MinimizeButton_Click;
            MinimizeButton.MouseEnter += MinimizeButton_MouseEnter;
            MinimizeButton.MouseLeave += MinimizeButton_MouseLeave;
            // 
            // CloseButton
            // 
            CloseButton.AutoSize = true;
            CloseButton.Font = new Font("Microsoft Sans Serif", 25F, FontStyle.Bold);
            CloseButton.ImeMode = ImeMode.NoControl;
            CloseButton.Location = new Point(454, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(49, 48);
            CloseButton.TabIndex = 2;
            CloseButton.Text = "X";
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            // 
            // BackKeyType
            // 
            BackKeyType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BackKeyType.Font = new Font("Microsoft Sans Serif", 13F);
            BackKeyType.ForeColor = Color.White;
            BackKeyType.ImeMode = ImeMode.NoControl;
            BackKeyType.Location = new Point(351, 224);
            BackKeyType.Name = "BackKeyType";
            BackKeyType.RightToLeft = RightToLeft.No;
            BackKeyType.Size = new Size(138, 25);
            BackKeyType.TabIndex = 19;
            BackKeyType.Text = "F6";
            BackKeyType.TextAlign = ContentAlignment.BottomRight;
            BackKeyType.DoubleClick += BackKeyType_DoubleClick;
            BackKeyType.MouseEnter += BackKeyType_MouseEnter;
            BackKeyType.MouseLeave += BackKeyType_MouseLeave;
            // 
            // BackKeyLab
            // 
            BackKeyLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BackKeyLab.AutoSize = true;
            BackKeyLab.Font = new Font("Microsoft Sans Serif", 13F);
            BackKeyLab.ForeColor = Color.White;
            BackKeyLab.ImeMode = ImeMode.NoControl;
            BackKeyLab.Location = new Point(14, 224);
            BackKeyLab.Name = "BackKeyLab";
            BackKeyLab.Size = new Size(101, 26);
            BackKeyLab.TabIndex = 18;
            BackKeyLab.Text = "Back key";
            BackKeyLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CloseKeyType
            // 
            CloseKeyType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseKeyType.Font = new Font("Microsoft Sans Serif", 13F);
            CloseKeyType.ForeColor = Color.White;
            CloseKeyType.ImeMode = ImeMode.NoControl;
            CloseKeyType.Location = new Point(351, 182);
            CloseKeyType.Name = "CloseKeyType";
            CloseKeyType.RightToLeft = RightToLeft.No;
            CloseKeyType.Size = new Size(138, 25);
            CloseKeyType.TabIndex = 17;
            CloseKeyType.Text = "F7";
            CloseKeyType.TextAlign = ContentAlignment.BottomRight;
            CloseKeyType.DoubleClick += CloseKeyType_DoubleClick;
            CloseKeyType.MouseEnter += CloseKeyType_MouseEnter;
            CloseKeyType.MouseLeave += CloseKeyType_MouseLeave;
            // 
            // CloseKeyLab
            // 
            CloseKeyLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseKeyLab.AutoSize = true;
            CloseKeyLab.Font = new Font("Microsoft Sans Serif", 13F);
            CloseKeyLab.ForeColor = Color.White;
            CloseKeyLab.ImeMode = ImeMode.NoControl;
            CloseKeyLab.Location = new Point(14, 182);
            CloseKeyLab.Name = "CloseKeyLab";
            CloseKeyLab.Size = new Size(108, 26);
            CloseKeyLab.TabIndex = 16;
            CloseKeyLab.Text = "Close key";
            CloseKeyLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(BackKeyType);
            panel1.Controls.Add(ConfirmButton);
            panel1.Controls.Add(BackKeyLab);
            panel1.Controls.Add(NameBackground);
            panel1.Controls.Add(CloseKeyType);
            panel1.Controls.Add(ResetButton);
            panel1.Controls.Add(CloseKeyLab);
            panel1.Controls.Add(BackButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(503, 601);
            panel1.TabIndex = 3;
            // 
            // ConfirmButton
            // 
            ConfirmButton.BackColor = Color.FromArgb(173, 102, 213);
            ConfirmButton.FlatStyle = FlatStyle.Flat;
            ConfirmButton.ImeMode = ImeMode.NoControl;
            ConfirmButton.Location = new Point(218, 555);
            ConfirmButton.Margin = new Padding(3, 4, 3, 4);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.Size = new Size(86, 32);
            ConfirmButton.TabIndex = 11;
            ConfirmButton.Text = "Confirm";
            ConfirmButton.UseVisualStyleBackColor = false;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.FromArgb(173, 102, 213);
            ResetButton.FlatStyle = FlatStyle.Flat;
            ResetButton.ImeMode = ImeMode.NoControl;
            ResetButton.Location = new Point(311, 555);
            ResetButton.Margin = new Padding(3, 4, 3, 4);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(86, 32);
            ResetButton.TabIndex = 10;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.FlatStyle = FlatStyle.Flat;
            BackButton.ImeMode = ImeMode.NoControl;
            BackButton.Location = new Point(403, 555);
            BackButton.Margin = new Padding(3, 4, 3, 4);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(86, 32);
            BackButton.TabIndex = 9;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // HotKeySettingsManager
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "HotKeySettingsManager";
            Size = new Size(503, 601);
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
        private Label BackKeyType;
        private Label BackKeyLab;
        private Label CloseKeyType;
        private Label CloseKeyLab;
        private Panel panel1;
        private Button ConfirmButton;
        private Button ResetButton;
        private Button BackButton;
    }
}
