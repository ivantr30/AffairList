namespace AffairList
{
    partial class AffairsManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AffairsManager));
            MinimizeButton = new Label();
            NameBackground = new Panel();
            CloseButton = new Label();
            AffairsLab = new Label();
            ProfileBox = new ComboBox();
            DeadlineLab = new Label();
            Affairs = new ListBox();
            AffairInput = new TextBox();
            panel1 = new Panel();
            RenameAffairButton = new Button();
            AddDeadlineButton = new Button();
            PriorityButton = new Button();
            DeleteButton = new Button();
            BackButton = new Button();
            ClearButton = new Button();
            AddAffairButton = new Button();
            NameBackground.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // MinimizeButton
            // 
            resources.ApplyResources(MinimizeButton, "MinimizeButton");
            MinimizeButton.Name = "MinimizeButton";
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(AffairsLab);
            resources.ApplyResources(NameBackground, "NameBackground");
            NameBackground.Name = "NameBackground";
            // 
            // CloseButton
            // 
            resources.ApplyResources(CloseButton, "CloseButton");
            CloseButton.Name = "CloseButton";
            // 
            // AffairsLab
            // 
            resources.ApplyResources(AffairsLab, "AffairsLab");
            AffairsLab.Name = "AffairsLab";
            // 
            // ProfileBox
            // 
            ProfileBox.FormattingEnabled = true;
            resources.ApplyResources(ProfileBox, "ProfileBox");
            ProfileBox.Name = "ProfileBox";
            // 
            // DeadlineLab
            // 
            resources.ApplyResources(DeadlineLab, "DeadlineLab");
            DeadlineLab.ForeColor = SystemColors.ButtonHighlight;
            DeadlineLab.Name = "DeadlineLab";
            // 
            // Affairs
            // 
            resources.ApplyResources(Affairs, "Affairs");
            Affairs.FormattingEnabled = true;
            Affairs.Name = "Affairs";
            // 
            // AffairInput
            // 
            resources.ApplyResources(AffairInput, "AffairInput");
            AffairInput.Name = "AffairInput";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(RenameAffairButton);
            panel1.Controls.Add(ProfileBox);
            panel1.Controls.Add(AddDeadlineButton);
            panel1.Controls.Add(DeadlineLab);
            panel1.Controls.Add(PriorityButton);
            panel1.Controls.Add(DeleteButton);
            panel1.Controls.Add(BackButton);
            panel1.Controls.Add(Affairs);
            panel1.Controls.Add(ClearButton);
            panel1.Controls.Add(AddAffairButton);
            panel1.Controls.Add(AffairInput);
            panel1.Controls.Add(NameBackground);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // RenameAffairButton
            // 
            RenameAffairButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(RenameAffairButton, "RenameAffairButton");
            RenameAffairButton.Name = "RenameAffairButton";
            RenameAffairButton.UseVisualStyleBackColor = false;
            // 
            // AddDeadlineButton
            // 
            AddDeadlineButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(AddDeadlineButton, "AddDeadlineButton");
            AddDeadlineButton.Name = "AddDeadlineButton";
            AddDeadlineButton.UseVisualStyleBackColor = false;
            // 
            // PriorityButton
            // 
            PriorityButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(PriorityButton, "PriorityButton");
            PriorityButton.Name = "PriorityButton";
            PriorityButton.UseVisualStyleBackColor = false;
            // 
            // DeleteButton
            // 
            DeleteButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(DeleteButton, "DeleteButton");
            DeleteButton.Name = "DeleteButton";
            DeleteButton.UseVisualStyleBackColor = false;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(BackButton, "BackButton");
            BackButton.Name = "BackButton";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(ClearButton, "ClearButton");
            ClearButton.Name = "ClearButton";
            ClearButton.UseVisualStyleBackColor = false;
            // 
            // AddAffairButton
            // 
            AddAffairButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(AddAffairButton, "AddAffairButton");
            AddAffairButton.Name = "AddAffairButton";
            AddAffairButton.UseVisualStyleBackColor = false;
            // 
            // AffairsManager
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "AffairsManager";
            NameBackground.ResumeLayout(false);
            NameBackground.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label MinimizeButton;
        private Panel NameBackground;
        private Label AffairsLab;
        private ComboBox ProfileBox;
        private Label DeadlineLab;
        private ListBox Affairs;
        private TextBox AffairInput;
        private Panel panel1;
        private Button RenameAffairButton;
        private Button AddDeadlineButton;
        private Button PriorityButton;
        private Button DeleteButton;
        private Button BackButton;
        private Button ClearButton;
        private Button AddAffairButton;
        private Label CloseButton;
    }
}
