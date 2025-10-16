namespace AffairList
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            AffairListLab = new Label();
            NameBackground = new Panel();
            MinimizeButton = new Label();
            CloseButton = new Label();
            ErrorHelpLab = new Label();
            ChangeProfileButton = new Button();
            HotKeyButton = new Button();
            SettingsButton = new Button();
            ChangeListButton = new Button();
            ClearListButton = new Button();
            panel1 = new Panel();
            OpenListButton = new Button();
            ReplaceAffairListButton = new Button();
            NameBackground.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // AffairListLab
            // 
            resources.ApplyResources(AffairListLab, "AffairListLab");
            AffairListLab.Name = "AffairListLab";
            AffairListLab.MouseDown += AffairListLab_MouseDown;
            AffairListLab.MouseMove += AffairListLab_MouseMove;
            // 
            // NameBackground
            // 
            resources.ApplyResources(NameBackground, "NameBackground");
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(AffairListLab);
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
            // ErrorHelpLab
            // 
            resources.ApplyResources(ErrorHelpLab, "ErrorHelpLab");
            ErrorHelpLab.ForeColor = SystemColors.ButtonHighlight;
            ErrorHelpLab.Name = "ErrorHelpLab";
            // 
            // ChangeProfileButton
            // 
            resources.ApplyResources(ChangeProfileButton, "ChangeProfileButton");
            ChangeProfileButton.BackColor = Color.FromArgb(173, 102, 213);
            ChangeProfileButton.FlatAppearance.BorderSize = 0;
            ChangeProfileButton.ForeColor = SystemColors.ButtonHighlight;
            ChangeProfileButton.Name = "ChangeProfileButton";
            ChangeProfileButton.UseVisualStyleBackColor = false;
            ChangeProfileButton.Click += ChangeProfileButton_Click;
            // 
            // HotKeyButton
            // 
            resources.ApplyResources(HotKeyButton, "HotKeyButton");
            HotKeyButton.BackColor = Color.FromArgb(173, 102, 213);
            HotKeyButton.FlatAppearance.BorderSize = 0;
            HotKeyButton.ForeColor = SystemColors.ButtonHighlight;
            HotKeyButton.Name = "HotKeyButton";
            HotKeyButton.UseVisualStyleBackColor = false;
            HotKeyButton.Click += HotKeyButton_Click;
            // 
            // SettingsButton
            // 
            resources.ApplyResources(SettingsButton, "SettingsButton");
            SettingsButton.BackColor = Color.FromArgb(173, 102, 213);
            SettingsButton.FlatAppearance.BorderSize = 0;
            SettingsButton.ForeColor = SystemColors.ButtonHighlight;
            SettingsButton.Name = "SettingsButton";
            SettingsButton.UseVisualStyleBackColor = false;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // ChangeListButton
            // 
            resources.ApplyResources(ChangeListButton, "ChangeListButton");
            ChangeListButton.BackColor = Color.FromArgb(173, 102, 213);
            ChangeListButton.FlatAppearance.BorderSize = 0;
            ChangeListButton.ForeColor = SystemColors.ButtonHighlight;
            ChangeListButton.Name = "ChangeListButton";
            ChangeListButton.UseVisualStyleBackColor = false;
            ChangeListButton.Click += ChangeListButton_Click;
            // 
            // ClearListButton
            // 
            resources.ApplyResources(ClearListButton, "ClearListButton");
            ClearListButton.BackColor = Color.FromArgb(173, 102, 213);
            ClearListButton.FlatAppearance.BorderSize = 0;
            ClearListButton.ForeColor = SystemColors.ButtonHighlight;
            ClearListButton.Name = "ClearListButton";
            ClearListButton.TabStop = false;
            ClearListButton.UseVisualStyleBackColor = false;
            ClearListButton.Click += ClearListButton_Click;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
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
            panel1.Name = "panel1";
            // 
            // OpenListButton
            // 
            resources.ApplyResources(OpenListButton, "OpenListButton");
            OpenListButton.BackColor = Color.FromArgb(173, 102, 213);
            OpenListButton.FlatAppearance.BorderSize = 0;
            OpenListButton.ForeColor = SystemColors.ButtonHighlight;
            OpenListButton.Name = "OpenListButton";
            OpenListButton.UseVisualStyleBackColor = false;
            OpenListButton.Click += OpenListButton_Click;
            // 
            // ReplaceAffairListButton
            // 
            resources.ApplyResources(ReplaceAffairListButton, "ReplaceAffairListButton");
            ReplaceAffairListButton.BackColor = Color.FromArgb(173, 102, 213);
            ReplaceAffairListButton.FlatAppearance.BorderSize = 0;
            ReplaceAffairListButton.ForeColor = SystemColors.ButtonHighlight;
            ReplaceAffairListButton.Name = "ReplaceAffairListButton";
            ReplaceAffairListButton.UseVisualStyleBackColor = false;
            ReplaceAffairListButton.Click += ReplaceAffairListButton_Click;
            // 
            // MainMenu
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "MainMenu";
            NameBackground.ResumeLayout(false);
            NameBackground.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label AffairListLab;
        private Panel NameBackground;
        private Label MinimizeButton;
        private Label CloseButton;
        private Label ErrorHelpLab;
        private Button ChangeProfileButton;
        private Button HotKeyButton;
        private Button SettingsButton;
        private Button ChangeListButton;
        private Button ClearListButton;
        private Panel panel1;
        private Button OpenListButton;
        private Button ReplaceAffairListButton;
    }
}
