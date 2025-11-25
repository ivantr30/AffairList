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
                _lines = null;
                KeyDownHandlers = null;
                KeyPressHandlers = null;
                KeyUpHandlers = null;
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
            MinimizeButton.Click += MinimizeButton_Click;
            MinimizeButton.MouseEnter += MinimizeButton_MouseEnter;
            MinimizeButton.MouseLeave += MinimizeButton_MouseLeave;
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(AffairsLab);
            resources.ApplyResources(NameBackground, "NameBackground");
            NameBackground.Name = "NameBackground";
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
            // 
            // CloseButton
            // 
            resources.ApplyResources(CloseButton, "CloseButton");
            CloseButton.Name = "CloseButton";
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            // 
            // AffairsLab
            // 
            resources.ApplyResources(AffairsLab, "AffairsLab");
            AffairsLab.Name = "AffairsLab";
            AffairsLab.MouseDown += AffairsLab_MouseDown;
            AffairsLab.MouseMove += AffairsLab_MouseMove;
            // 
            // ProfileBox
            // 
            ProfileBox.FormattingEnabled = true;
            resources.ApplyResources(ProfileBox, "ProfileBox");
            ProfileBox.Name = "ProfileBox";
            ProfileBox.SelectionChangeCommitted += ProfileBox_SelectionChangeCommitted;
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
            Affairs.SelectedValueChanged += Affairs_SelectedValueChanged;
            Affairs.MouseDown += Affairs_MouseDown;
            Affairs.MouseUp += Affairs_MouseUp;
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
            RenameAffairButton.Click += RenameAffairButton_Click;
            // 
            // AddDeadlineButton
            // 
            AddDeadlineButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(AddDeadlineButton, "AddDeadlineButton");
            AddDeadlineButton.Name = "AddDeadlineButton";
            AddDeadlineButton.UseVisualStyleBackColor = false;
            AddDeadlineButton.Click += AddDeadlineButton_Click;
            // 
            // PriorityButton
            // 
            PriorityButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(PriorityButton, "PriorityButton");
            PriorityButton.Name = "PriorityButton";
            PriorityButton.UseVisualStyleBackColor = false;
            PriorityButton.Click += PriorityButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(DeleteButton, "DeleteButton");
            DeleteButton.Name = "DeleteButton";
            DeleteButton.UseVisualStyleBackColor = false;
            DeleteButton.Click += DeleteButton_Click;
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
            ClearButton.Click += ClearButton_Click;
            // 
            // AddAffairButton
            // 
            AddAffairButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(AddAffairButton, "AddAffairButton");
            AddAffairButton.Name = "AddAffairButton";
            AddAffairButton.UseVisualStyleBackColor = false;
            AddAffairButton.Click += AddAffairButton_Click;
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
