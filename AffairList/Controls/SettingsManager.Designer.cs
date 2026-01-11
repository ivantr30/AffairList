namespace AffairList
{
    partial class SettingsManager
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
            ExportButton = new Button();
            DistanceToNotificate = new Label();
            DistanceToNotificateLab = new Label();
            NotificationState = new Label();
            NotificationStateLab = new Label();
            AskToDeleteState = new Label();
            AskToDelete = new Label();
            ThemeBoxCB = new ComboBox();
            CurrentThemeLab = new Label();
            PickBgColorButton = new Button();
            PickTextColorButton = new Button();
            panel1 = new Panel();
            CanBeAlwaysReplacable = new Label();
            CanBeAlwaysReplacableLab = new Label();
            HourDistanceToNotificate = new Label();
            HourDistanceToNotificateLab = new Label();
            CurrentFontComboBox = new ComboBox();
            CurrentFontLab = new Label();
            autostartStateLab = new Label();
            AutostartLab = new Label();
            ListBgTextColorLab = new Label();
            ListTextColorLab = new Label();
            LocationLab = new Label();
            ListLocationLab = new Label();
            ConfirmButton = new Button();
            ResetButton = new Button();
            BackButton = new Button();
            NameBackground = new Panel();
            MinimizeButton = new Label();
            CloseButton = new Label();
            SettingsLab = new Label();
            ColorPicker = new ColorDialog();
            ExportSettingsFileDialog = new FolderBrowserDialog();
            panel1.SuspendLayout();
            NameBackground.SuspendLayout();
            SuspendLayout();
            // 
            // ExportButton
            // 
            ExportButton.BackColor = Color.FromArgb(173, 102, 213);
            ExportButton.FlatStyle = FlatStyle.Flat;
            ExportButton.Location = new Point(12, 473);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(75, 24);
            ExportButton.TabIndex = 29;
            ExportButton.Text = "Export";
            ExportButton.UseVisualStyleBackColor = false;
            ExportButton.Click += ExportButton_Click;
            // 
            // DistanceToNotificate
            // 
            DistanceToNotificate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DistanceToNotificate.AutoSize = true;
            DistanceToNotificate.Font = new Font("Microsoft Sans Serif", 13F);
            DistanceToNotificate.ForeColor = Color.White;
            DistanceToNotificate.Location = new Point(386, 341);
            DistanceToNotificate.Name = "DistanceToNotificate";
            DistanceToNotificate.Size = new Size(20, 22);
            DistanceToNotificate.TabIndex = 28;
            DistanceToNotificate.Text = "0";
            DistanceToNotificate.TextAlign = ContentAlignment.MiddleRight;
            DistanceToNotificate.DoubleClick += DayDistanceToNotificate_DoubleClick;
            DistanceToNotificate.MouseEnter += DistanceToNotificate_MouseEnter;
            DistanceToNotificate.MouseLeave += DistanceToNotificate_MouseLeave;
            // 
            // DistanceToNotificateLab
            // 
            DistanceToNotificateLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DistanceToNotificateLab.AutoSize = true;
            DistanceToNotificateLab.Font = new Font("Microsoft Sans Serif", 13F);
            DistanceToNotificateLab.ForeColor = Color.White;
            DistanceToNotificateLab.Location = new Point(12, 341);
            DistanceToNotificateLab.Name = "DistanceToNotificateLab";
            DistanceToNotificateLab.Size = new Size(214, 22);
            DistanceToNotificateLab.TabIndex = 27;
            DistanceToNotificateLab.Text = "Day Distance to notificate";
            DistanceToNotificateLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NotificationState
            // 
            NotificationState.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            NotificationState.AutoSize = true;
            NotificationState.Font = new Font("Microsoft Sans Serif", 13F);
            NotificationState.ForeColor = Color.White;
            NotificationState.Location = new Point(375, 286);
            NotificationState.Name = "NotificationState";
            NotificationState.Size = new Size(34, 22);
            NotificationState.TabIndex = 26;
            NotificationState.Text = "On";
            NotificationState.TextAlign = ContentAlignment.MiddleCenter;
            NotificationState.MouseDown += NotificationState_MouseDown;
            NotificationState.MouseEnter += NotificationState_MouseEnter;
            NotificationState.MouseLeave += NotificationState_MouseLeave;
            // 
            // NotificationStateLab
            // 
            NotificationStateLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            NotificationStateLab.AutoSize = true;
            NotificationStateLab.Font = new Font("Microsoft Sans Serif", 13F);
            NotificationStateLab.ForeColor = Color.White;
            NotificationStateLab.Location = new Point(12, 286);
            NotificationStateLab.Name = "NotificationStateLab";
            NotificationStateLab.Size = new Size(114, 22);
            NotificationStateLab.TabIndex = 25;
            NotificationStateLab.Text = "Notificate me";
            NotificationStateLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AskToDeleteState
            // 
            AskToDeleteState.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AskToDeleteState.AutoSize = true;
            AskToDeleteState.Font = new Font("Microsoft Sans Serif", 13F);
            AskToDeleteState.ForeColor = Color.White;
            AskToDeleteState.Location = new Point(375, 259);
            AskToDeleteState.Name = "AskToDeleteState";
            AskToDeleteState.Size = new Size(34, 22);
            AskToDeleteState.TabIndex = 24;
            AskToDeleteState.Text = "On";
            AskToDeleteState.TextAlign = ContentAlignment.MiddleCenter;
            AskToDeleteState.MouseDown += AskToDeleteState_MouseDown;
            AskToDeleteState.MouseEnter += AskToDeleteState_MouseEnter;
            AskToDeleteState.MouseLeave += AskToDeleteState_MouseLeave;
            // 
            // AskToDelete
            // 
            AskToDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AskToDelete.AutoSize = true;
            AskToDelete.Font = new Font("Microsoft Sans Serif", 13F);
            AskToDelete.ForeColor = Color.White;
            AskToDelete.Location = new Point(12, 259);
            AskToDelete.Name = "AskToDelete";
            AskToDelete.Size = new Size(184, 22);
            AskToDelete.TabIndex = 23;
            AskToDelete.Text = "Ask to delete an affair";
            AskToDelete.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ThemeBoxCB
            // 
            ThemeBoxCB.FormattingEnabled = true;
            ThemeBoxCB.Location = new Point(286, 395);
            ThemeBoxCB.Name = "ThemeBoxCB";
            ThemeBoxCB.Size = new Size(121, 23);
            ThemeBoxCB.TabIndex = 22;
            ThemeBoxCB.SelectedValueChanged += ThemeBoxCB_SelectedValueChanged;
            // 
            // CurrentThemeLab
            // 
            CurrentThemeLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CurrentThemeLab.AutoSize = true;
            CurrentThemeLab.Font = new Font("Microsoft Sans Serif", 13F);
            CurrentThemeLab.ForeColor = Color.White;
            CurrentThemeLab.Location = new Point(12, 395);
            CurrentThemeLab.Name = "CurrentThemeLab";
            CurrentThemeLab.Size = new Size(124, 22);
            CurrentThemeLab.TabIndex = 21;
            CurrentThemeLab.Text = "Current theme";
            CurrentThemeLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PickBgColorButton
            // 
            PickBgColorButton.BackColor = Color.FromArgb(173, 102, 213);
            PickBgColorButton.FlatAppearance.BorderSize = 0;
            PickBgColorButton.FlatStyle = FlatStyle.Flat;
            PickBgColorButton.Font = new Font("Tahoma", 10F);
            PickBgColorButton.ForeColor = Color.White;
            PickBgColorButton.Location = new Point(311, 196);
            PickBgColorButton.Name = "PickBgColorButton";
            PickBgColorButton.Size = new Size(96, 26);
            PickBgColorButton.TabIndex = 18;
            PickBgColorButton.Text = "Pick color";
            PickBgColorButton.UseVisualStyleBackColor = false;
            PickBgColorButton.Click += PickBgColorButton_Click;
            // 
            // PickTextColorButton
            // 
            PickTextColorButton.BackColor = Color.FromArgb(173, 102, 213);
            PickTextColorButton.FlatAppearance.BorderSize = 0;
            PickTextColorButton.FlatStyle = FlatStyle.Flat;
            PickTextColorButton.Font = new Font("Tahoma", 10F);
            PickTextColorButton.ForeColor = Color.White;
            PickTextColorButton.Location = new Point(311, 165);
            PickTextColorButton.Name = "PickTextColorButton";
            PickTextColorButton.Size = new Size(96, 26);
            PickTextColorButton.TabIndex = 17;
            PickTextColorButton.Text = "Pick color";
            PickTextColorButton.UseVisualStyleBackColor = false;
            PickTextColorButton.Click += PickTextColorButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(CanBeAlwaysReplacable);
            panel1.Controls.Add(CanBeAlwaysReplacableLab);
            panel1.Controls.Add(HourDistanceToNotificate);
            panel1.Controls.Add(HourDistanceToNotificateLab);
            panel1.Controls.Add(CurrentFontComboBox);
            panel1.Controls.Add(CurrentFontLab);
            panel1.Controls.Add(ExportButton);
            panel1.Controls.Add(DistanceToNotificate);
            panel1.Controls.Add(DistanceToNotificateLab);
            panel1.Controls.Add(NotificationState);
            panel1.Controls.Add(NotificationStateLab);
            panel1.Controls.Add(AskToDeleteState);
            panel1.Controls.Add(AskToDelete);
            panel1.Controls.Add(ThemeBoxCB);
            panel1.Controls.Add(CurrentThemeLab);
            panel1.Controls.Add(PickBgColorButton);
            panel1.Controls.Add(PickTextColorButton);
            panel1.Controls.Add(autostartStateLab);
            panel1.Controls.Add(AutostartLab);
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
            panel1.Size = new Size(440, 508);
            panel1.TabIndex = 2;
            // 
            // CanBeAlwaysReplacable
            // 
            CanBeAlwaysReplacable.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CanBeAlwaysReplacable.AutoSize = true;
            CanBeAlwaysReplacable.Font = new Font("Microsoft Sans Serif", 13F);
            CanBeAlwaysReplacable.ForeColor = Color.White;
            CanBeAlwaysReplacable.Location = new Point(375, 313);
            CanBeAlwaysReplacable.Name = "CanBeAlwaysReplacable";
            CanBeAlwaysReplacable.Size = new Size(34, 22);
            CanBeAlwaysReplacable.TabIndex = 35;
            CanBeAlwaysReplacable.Text = "On";
            CanBeAlwaysReplacable.TextAlign = ContentAlignment.MiddleCenter;
            CanBeAlwaysReplacable.MouseDown += CanBeAlwaysReplacable_MouseDown;
            CanBeAlwaysReplacable.MouseEnter += CanBeAlwaysReplacable_MouseEnter;
            CanBeAlwaysReplacable.MouseLeave += CanBeAlwaysReplacable_MouseLeave;
            // 
            // CanBeAlwaysReplacableLab
            // 
            CanBeAlwaysReplacableLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CanBeAlwaysReplacableLab.AutoSize = true;
            CanBeAlwaysReplacableLab.Font = new Font("Microsoft Sans Serif", 13F);
            CanBeAlwaysReplacableLab.ForeColor = Color.White;
            CanBeAlwaysReplacableLab.Location = new Point(12, 313);
            CanBeAlwaysReplacableLab.Name = "CanBeAlwaysReplacableLab";
            CanBeAlwaysReplacableLab.Size = new Size(245, 22);
            CanBeAlwaysReplacableLab.TabIndex = 34;
            CanBeAlwaysReplacableLab.Text = "List can be always replacable";
            CanBeAlwaysReplacableLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HourDistanceToNotificate
            // 
            HourDistanceToNotificate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            HourDistanceToNotificate.AutoSize = true;
            HourDistanceToNotificate.Font = new Font("Microsoft Sans Serif", 13F);
            HourDistanceToNotificate.ForeColor = Color.White;
            HourDistanceToNotificate.Location = new Point(386, 368);
            HourDistanceToNotificate.Name = "HourDistanceToNotificate";
            HourDistanceToNotificate.Size = new Size(20, 22);
            HourDistanceToNotificate.TabIndex = 33;
            HourDistanceToNotificate.Text = "0";
            HourDistanceToNotificate.TextAlign = ContentAlignment.MiddleRight;
            HourDistanceToNotificate.DoubleClick += HourDistanceToNotificate_DoubleClick;
            HourDistanceToNotificate.MouseEnter += HourDistanceToNotificate_MouseEnter;
            HourDistanceToNotificate.MouseLeave += HourDistanceToNotificate_MouseLeave;
            // 
            // HourDistanceToNotificateLab
            // 
            HourDistanceToNotificateLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            HourDistanceToNotificateLab.AutoSize = true;
            HourDistanceToNotificateLab.Font = new Font("Microsoft Sans Serif", 13F);
            HourDistanceToNotificateLab.ForeColor = Color.White;
            HourDistanceToNotificateLab.Location = new Point(12, 368);
            HourDistanceToNotificateLab.Name = "HourDistanceToNotificateLab";
            HourDistanceToNotificateLab.Size = new Size(221, 22);
            HourDistanceToNotificateLab.TabIndex = 32;
            HourDistanceToNotificateLab.Text = "Hour Distance to notificate";
            HourDistanceToNotificateLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CurrentFontComboBox
            // 
            CurrentFontComboBox.FormattingEnabled = true;
            CurrentFontComboBox.Location = new Point(286, 422);
            CurrentFontComboBox.Name = "CurrentFontComboBox";
            CurrentFontComboBox.Size = new Size(121, 23);
            CurrentFontComboBox.TabIndex = 31;
            CurrentFontComboBox.SelectedValueChanged += CurrentFontComboBox_SelectedValueChanged;
            // 
            // CurrentFontLab
            // 
            CurrentFontLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CurrentFontLab.AutoSize = true;
            CurrentFontLab.Font = new Font("Microsoft Sans Serif", 13F);
            CurrentFontLab.ForeColor = Color.White;
            CurrentFontLab.Location = new Point(12, 422);
            CurrentFontLab.Name = "CurrentFontLab";
            CurrentFontLab.Size = new Size(105, 22);
            CurrentFontLab.TabIndex = 30;
            CurrentFontLab.Text = "Current font";
            CurrentFontLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // autostartStateLab
            // 
            autostartStateLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            autostartStateLab.AutoSize = true;
            autostartStateLab.Font = new Font("Microsoft Sans Serif", 13F);
            autostartStateLab.ForeColor = Color.White;
            autostartStateLab.Location = new Point(375, 231);
            autostartStateLab.Name = "autostartStateLab";
            autostartStateLab.Size = new Size(34, 22);
            autostartStateLab.TabIndex = 16;
            autostartStateLab.Text = "On";
            autostartStateLab.TextAlign = ContentAlignment.MiddleCenter;
            autostartStateLab.MouseDown += autostartStateLab_MouseDown;
            autostartStateLab.MouseEnter += autostartStateLab_MouseEnter;
            autostartStateLab.MouseLeave += autostartStateLab_MouseLeave;
            // 
            // AutostartLab
            // 
            AutostartLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AutostartLab.AutoSize = true;
            AutostartLab.Font = new Font("Microsoft Sans Serif", 13F);
            AutostartLab.ForeColor = Color.White;
            AutostartLab.Location = new Point(12, 231);
            AutostartLab.Name = "AutostartLab";
            AutostartLab.Size = new Size(126, 22);
            AutostartLab.TabIndex = 14;
            AutostartLab.Text = "Autostart state";
            AutostartLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ListBgTextColorLab
            // 
            ListBgTextColorLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ListBgTextColorLab.AutoSize = true;
            ListBgTextColorLab.Font = new Font("Microsoft Sans Serif", 13F);
            ListBgTextColorLab.ForeColor = Color.White;
            ListBgTextColorLab.Location = new Point(12, 198);
            ListBgTextColorLab.Name = "ListBgTextColorLab";
            ListBgTextColorLab.Size = new Size(181, 22);
            ListBgTextColorLab.TabIndex = 11;
            ListBgTextColorLab.Text = "List background color";
            ListBgTextColorLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ListTextColorLab
            // 
            ListTextColorLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ListTextColorLab.AutoSize = true;
            ListTextColorLab.Font = new Font("Microsoft Sans Serif", 13F);
            ListTextColorLab.ForeColor = Color.White;
            ListTextColorLab.Location = new Point(12, 167);
            ListTextColorLab.Name = "ListTextColorLab";
            ListTextColorLab.Size = new Size(116, 22);
            ListTextColorLab.TabIndex = 10;
            ListTextColorLab.Text = "List text color";
            ListTextColorLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LocationLab
            // 
            LocationLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LocationLab.Font = new Font("Microsoft Sans Serif", 13F);
            LocationLab.ForeColor = Color.White;
            LocationLab.Location = new Point(286, 136);
            LocationLab.Name = "LocationLab";
            LocationLab.RightToLeft = RightToLeft.No;
            LocationLab.Size = new Size(121, 19);
            LocationLab.TabIndex = 9;
            LocationLab.Text = "0, 0";
            LocationLab.TextAlign = ContentAlignment.MiddleRight;
            LocationLab.DoubleClick += LocationLab_DoubleClick;
            LocationLab.MouseEnter += LocationLab_MouseEnter;
            LocationLab.MouseLeave += LocationLab_MouseLeave;
            // 
            // ListLocationLab
            // 
            ListLocationLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ListLocationLab.AutoSize = true;
            ListLocationLab.Font = new Font("Microsoft Sans Serif", 13F);
            ListLocationLab.ForeColor = Color.White;
            ListLocationLab.Location = new Point(12, 136);
            ListLocationLab.Name = "ListLocationLab";
            ListLocationLab.Size = new Size(133, 22);
            ListLocationLab.TabIndex = 3;
            ListLocationLab.Text = "List x,y location";
            ListLocationLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ConfirmButton
            // 
            ConfirmButton.BackColor = Color.FromArgb(173, 102, 213);
            ConfirmButton.FlatStyle = FlatStyle.Flat;
            ConfirmButton.Location = new Point(191, 473);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.Size = new Size(75, 24);
            ConfirmButton.TabIndex = 8;
            ConfirmButton.Text = "Confirm";
            ConfirmButton.UseVisualStyleBackColor = false;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.FromArgb(173, 102, 213);
            ResetButton.FlatStyle = FlatStyle.Flat;
            ResetButton.Location = new Point(272, 473);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(75, 24);
            ResetButton.TabIndex = 7;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.FlatStyle = FlatStyle.Flat;
            BackButton.Location = new Point(353, 473);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(75, 24);
            BackButton.TabIndex = 6;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(SettingsLab);
            NameBackground.Dock = DockStyle.Top;
            NameBackground.Location = new Point(0, 0);
            NameBackground.Name = "NameBackground";
            NameBackground.Size = new Size(440, 125);
            NameBackground.TabIndex = 0;
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
            // 
            // MinimizeButton
            // 
            MinimizeButton.AutoSize = true;
            MinimizeButton.Font = new Font("Microsoft Sans Serif", 25F, FontStyle.Bold);
            MinimizeButton.Location = new Point(361, 0);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(29, 39);
            MinimizeButton.TabIndex = 25;
            MinimizeButton.Text = "-";
            MinimizeButton.Click += MinimizeButton_Click;
            MinimizeButton.MouseEnter += MinimizeButton_MouseEnter;
            MinimizeButton.MouseLeave += MinimizeButton_MouseLeave;
            // 
            // CloseButton
            // 
            CloseButton.AutoSize = true;
            CloseButton.Font = new Font("Microsoft Sans Serif", 25F, FontStyle.Bold);
            CloseButton.Location = new Point(397, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(41, 39);
            CloseButton.TabIndex = 2;
            CloseButton.Text = "X";
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            // 
            // SettingsLab
            // 
            SettingsLab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SettingsLab.AutoSize = true;
            SettingsLab.Font = new Font("Microsoft Sans Serif", 30F);
            SettingsLab.Location = new Point(138, 46);
            SettingsLab.Name = "SettingsLab";
            SettingsLab.Size = new Size(164, 46);
            SettingsLab.TabIndex = 1;
            SettingsLab.Text = "Settings";
            SettingsLab.TextAlign = ContentAlignment.MiddleCenter;
            SettingsLab.MouseDown += SettingsLab_MouseDown;
            SettingsLab.MouseMove += SettingsLab_MouseMove;
            // 
            // SettingsManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "SettingsManager";
            Size = new Size(440, 508);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            NameBackground.ResumeLayout(false);
            NameBackground.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button ExportButton;
        private Label DistanceToNotificate;
        private Label DistanceToNotificateLab;
        private Label NotificationState;
        private Label NotificationStateLab;
        private Label AskToDeleteState;
        private Label AskToDelete;
        private ComboBox ThemeBoxCB;
        private Label CurrentThemeLab;
        private Button PickBgColorButton;
        private Button PickTextColorButton;
        private Panel panel1;
        private Label autostartStateLab;
        private Label AutostartLab;
        private Label ListBgTextColorLab;
        private Label ListTextColorLab;
        private Label LocationLab;
        private Label ListLocationLab;
        private Button ConfirmButton;
        private Button ResetButton;
        private Button BackButton;
        private Panel NameBackground;
        private Label MinimizeButton;
        private Label CloseButton;
        private Label SettingsLab;
        private ColorDialog ColorPicker;
        private FolderBrowserDialog ExportSettingsFileDialog;
        private Label HourDistanceToNotificate;
        private Label HourDistanceToNotificateLab;
        private ComboBox CurrentFontComboBox;
        private Label CurrentFontLab;
        private Label CanBeAlwaysReplacable;
        private Label CanBeAlwaysReplacableLab;
    }
}
