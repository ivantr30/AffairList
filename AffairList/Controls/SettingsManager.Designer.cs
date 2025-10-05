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
                _settingsUpdater = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsManager));
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
            vScrollBar1 = new VScrollBar();
            SettingsPanel = new Panel();
            panel1.SuspendLayout();
            NameBackground.SuspendLayout();
            SuspendLayout();
            // 
            // ExportButton
            // 
            resources.ApplyResources(ExportButton, "ExportButton");
            ExportButton.BackColor = Color.FromArgb(173, 102, 213);
            ExportButton.Name = "ExportButton";
            ExportButton.UseVisualStyleBackColor = false;
            ExportButton.Click += ExportButton_Click;
            // 
            // DistanceToNotificate
            // 
            resources.ApplyResources(DistanceToNotificate, "DistanceToNotificate");
            DistanceToNotificate.ForeColor = Color.White;
            DistanceToNotificate.Name = "DistanceToNotificate";
            DistanceToNotificate.DoubleClick += DistanceToNotificate_DoubleClick;
            DistanceToNotificate.MouseEnter += DistanceToNotificate_MouseEnter;
            DistanceToNotificate.MouseLeave += DistanceToNotificate_MouseLeave;
            // 
            // DistanceToNotificateLab
            // 
            resources.ApplyResources(DistanceToNotificateLab, "DistanceToNotificateLab");
            DistanceToNotificateLab.ForeColor = Color.White;
            DistanceToNotificateLab.Name = "DistanceToNotificateLab";
            // 
            // NotificationState
            // 
            resources.ApplyResources(NotificationState, "NotificationState");
            NotificationState.ForeColor = Color.White;
            NotificationState.Name = "NotificationState";
            NotificationState.MouseDown += NotificationState_MouseDown;
            NotificationState.MouseEnter += NotificationState_MouseEnter;
            NotificationState.MouseLeave += NotificationState_MouseLeave;
            // 
            // NotificationStateLab
            // 
            resources.ApplyResources(NotificationStateLab, "NotificationStateLab");
            NotificationStateLab.ForeColor = Color.White;
            NotificationStateLab.Name = "NotificationStateLab";
            // 
            // AskToDeleteState
            // 
            resources.ApplyResources(AskToDeleteState, "AskToDeleteState");
            AskToDeleteState.ForeColor = Color.White;
            AskToDeleteState.Name = "AskToDeleteState";
            AskToDeleteState.MouseDown += AskToDeleteState_MouseDown;
            AskToDeleteState.MouseEnter += AskToDeleteState_MouseEnter;
            AskToDeleteState.MouseLeave += AskToDeleteState_MouseLeave;
            // 
            // AskToDelete
            // 
            resources.ApplyResources(AskToDelete, "AskToDelete");
            AskToDelete.ForeColor = Color.White;
            AskToDelete.Name = "AskToDelete";
            // 
            // ThemeBoxCB
            // 
            resources.ApplyResources(ThemeBoxCB, "ThemeBoxCB");
            ThemeBoxCB.FormattingEnabled = true;
            ThemeBoxCB.Name = "ThemeBoxCB";
            ThemeBoxCB.SelectedValueChanged += ThemeBoxCB_SelectedValueChanged;
            // 
            // CurrentThemeLab
            // 
            resources.ApplyResources(CurrentThemeLab, "CurrentThemeLab");
            CurrentThemeLab.ForeColor = Color.White;
            CurrentThemeLab.Name = "CurrentThemeLab";
            // 
            // PickBgColorButton
            // 
            resources.ApplyResources(PickBgColorButton, "PickBgColorButton");
            PickBgColorButton.BackColor = Color.FromArgb(173, 102, 213);
            PickBgColorButton.FlatAppearance.BorderSize = 0;
            PickBgColorButton.ForeColor = Color.White;
            PickBgColorButton.Name = "PickBgColorButton";
            PickBgColorButton.UseVisualStyleBackColor = false;
            PickBgColorButton.Click += PickBgColorButton_Click;
            // 
            // PickTextColorButton
            // 
            resources.ApplyResources(PickTextColorButton, "PickTextColorButton");
            PickTextColorButton.BackColor = Color.FromArgb(173, 102, 213);
            PickTextColorButton.FlatAppearance.BorderSize = 0;
            PickTextColorButton.ForeColor = Color.White;
            PickTextColorButton.Name = "PickTextColorButton";
            PickTextColorButton.UseVisualStyleBackColor = false;
            PickTextColorButton.Click += PickTextColorButton_Click;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(SettingsPanel);
            panel1.Controls.Add(vScrollBar1);
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
            panel1.Name = "panel1";
            // 
            // autostartStateLab
            // 
            resources.ApplyResources(autostartStateLab, "autostartStateLab");
            autostartStateLab.ForeColor = Color.White;
            autostartStateLab.Name = "autostartStateLab";
            autostartStateLab.MouseDown += autostartStateLab_MouseDown;
            autostartStateLab.MouseEnter += autostartStateLab_MouseEnter;
            autostartStateLab.MouseLeave += autostartStateLab_MouseLeave;
            // 
            // AutostartLab
            // 
            resources.ApplyResources(AutostartLab, "AutostartLab");
            AutostartLab.ForeColor = Color.White;
            AutostartLab.Name = "AutostartLab";
            // 
            // ListBgTextColorLab
            // 
            resources.ApplyResources(ListBgTextColorLab, "ListBgTextColorLab");
            ListBgTextColorLab.ForeColor = Color.White;
            ListBgTextColorLab.Name = "ListBgTextColorLab";
            // 
            // ListTextColorLab
            // 
            resources.ApplyResources(ListTextColorLab, "ListTextColorLab");
            ListTextColorLab.ForeColor = Color.White;
            ListTextColorLab.Name = "ListTextColorLab";
            // 
            // LocationLab
            // 
            resources.ApplyResources(LocationLab, "LocationLab");
            LocationLab.ForeColor = Color.White;
            LocationLab.Name = "LocationLab";
            LocationLab.DoubleClick += LocationLab_DoubleClick;
            LocationLab.MouseEnter += LocationLab_MouseEnter;
            LocationLab.MouseLeave += LocationLab_MouseLeave;
            // 
            // ListLocationLab
            // 
            resources.ApplyResources(ListLocationLab, "ListLocationLab");
            ListLocationLab.ForeColor = Color.White;
            ListLocationLab.Name = "ListLocationLab";
            // 
            // ConfirmButton
            // 
            resources.ApplyResources(ConfirmButton, "ConfirmButton");
            ConfirmButton.BackColor = Color.FromArgb(173, 102, 213);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.UseVisualStyleBackColor = false;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // ResetButton
            // 
            resources.ApplyResources(ResetButton, "ResetButton");
            ResetButton.BackColor = Color.FromArgb(173, 102, 213);
            ResetButton.Name = "ResetButton";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // BackButton
            // 
            resources.ApplyResources(BackButton, "BackButton");
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.Name = "BackButton";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // NameBackground
            // 
            resources.ApplyResources(NameBackground, "NameBackground");
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(SettingsLab);
            NameBackground.Name = "NameBackground";
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
            // 
            // MinimizeButton
            // 
            resources.ApplyResources(MinimizeButton, "MinimizeButton");
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Click += MinimizeButton_Click;
            MinimizeButton.MouseEnter += MinimizeButton_MouseEnter;
            MinimizeButton.MouseLeave += MinimizeButton_MouseLeave;
            // 
            // CloseButton
            // 
            resources.ApplyResources(CloseButton, "CloseButton");
            CloseButton.Name = "CloseButton";
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            // 
            // SettingsLab
            // 
            resources.ApplyResources(SettingsLab, "SettingsLab");
            SettingsLab.Name = "SettingsLab";
            SettingsLab.MouseDown += SettingsLab_MouseDown;
            SettingsLab.MouseMove += SettingsLab_MouseMove;
            // 
            // ExportSettingsFileDialog
            // 
            resources.ApplyResources(ExportSettingsFileDialog, "ExportSettingsFileDialog");
            // 
            // vScrollBar1
            // 
            resources.ApplyResources(vScrollBar1, "vScrollBar1");
            vScrollBar1.Name = "vScrollBar1";
            // 
            // SettingsPanel
            // 
            resources.ApplyResources(SettingsPanel, "SettingsPanel");
            SettingsPanel.Name = "SettingsPanel";
            // 
            // SettingsManager
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "SettingsManager";
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
        private Panel SettingsPanel;
        private VScrollBar vScrollBar1;
    }
}
