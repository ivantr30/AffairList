namespace AffairList
{
    partial class HotkeySettingsManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotkeySettingsManager));
            HotKeySettingsLab = new Label();
            NameBackground = new Panel();
            MinimizeButton = new Label();
            CloseButton = new Label();
            panel1 = new Panel();
            BackKeyType = new Label();
            ConfirmButton = new Button();
            BackKeyLab = new Label();
            CloseKeyType = new Label();
            ResetButton = new Button();
            CloseKeyLab = new Label();
            BackButton = new Button();
            NameBackground.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // HotKeySettingsLab
            // 
            resources.ApplyResources(HotKeySettingsLab, "HotKeySettingsLab");
            HotKeySettingsLab.Name = "HotKeySettingsLab";
            // 
            // NameBackground
            // 
            resources.ApplyResources(NameBackground, "NameBackground");
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(HotKeySettingsLab);
            NameBackground.Name = "NameBackground";
            // 
            // MinimizeButton
            // 
            resources.ApplyResources(MinimizeButton, "MinimizeButton");
            MinimizeButton.Name = "MinimizeButton";
            // 
            // CloseButton
            // 
            resources.ApplyResources(CloseButton, "CloseButton");
            CloseButton.Name = "CloseButton";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(BackKeyType);
            panel1.Controls.Add(ConfirmButton);
            panel1.Controls.Add(BackKeyLab);
            panel1.Controls.Add(NameBackground);
            panel1.Controls.Add(CloseKeyType);
            panel1.Controls.Add(ResetButton);
            panel1.Controls.Add(CloseKeyLab);
            panel1.Controls.Add(BackButton);
            panel1.Name = "panel1";
            // 
            // BackKeyType
            // 
            resources.ApplyResources(BackKeyType, "BackKeyType");
            BackKeyType.ForeColor = Color.White;
            BackKeyType.Name = "BackKeyType";
            // 
            // ConfirmButton
            // 
            resources.ApplyResources(ConfirmButton, "ConfirmButton");
            ConfirmButton.BackColor = Color.FromArgb(173, 102, 213);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.UseVisualStyleBackColor = false;
            // 
            // BackKeyLab
            // 
            resources.ApplyResources(BackKeyLab, "BackKeyLab");
            BackKeyLab.ForeColor = Color.White;
            BackKeyLab.Name = "BackKeyLab";
            // 
            // CloseKeyType
            // 
            resources.ApplyResources(CloseKeyType, "CloseKeyType");
            CloseKeyType.ForeColor = Color.White;
            CloseKeyType.Name = "CloseKeyType";
            // 
            // ResetButton
            // 
            resources.ApplyResources(ResetButton, "ResetButton");
            ResetButton.BackColor = Color.FromArgb(173, 102, 213);
            ResetButton.Name = "ResetButton";
            ResetButton.UseVisualStyleBackColor = false;
            // 
            // CloseKeyLab
            // 
            resources.ApplyResources(CloseKeyLab, "CloseKeyLab");
            CloseKeyLab.ForeColor = Color.White;
            CloseKeyLab.Name = "CloseKeyLab";
            // 
            // BackButton
            // 
            resources.ApplyResources(BackButton, "BackButton");
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.Name = "BackButton";
            BackButton.UseVisualStyleBackColor = false;
            // 
            // HotkeySettingsManager
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "HotkeySettingsManager";
            KeyDown += HotkeySettingsManager_KeyDown;
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
        private Label BackKeyType;
        private Label BackKeyLab;
        private Label CloseKeyType;
        private Label CloseKeyLab;
    }
}
