namespace AffairList
{
    partial class ProfileManager
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
                KeyDownHandlers = null;
                KeyPressHandlers = null;
                KeyUpHandlers = null;
                _profileLines = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileManager));
            ExportButton = new Button();
            ChangePriorityButton = new Button();
            RenameButton = new Button();
            SelectProfileButton = new Button();
            DeleteButton = new Button();
            BackButton = new Button();
            Profiles = new ListBox();
            ClearButton = new Button();
            AddButton = new Button();
            ProfileInput = new TextBox();
            panel1 = new Panel();
            ProfilesListLab = new Label();
            NameBackground = new Panel();
            CloseButtonLab = new Label();
            MinimizeButton = new Label();
            CloseButton = new Label();
            ProfilesLab = new Label();
            panel1.SuspendLayout();
            NameBackground.SuspendLayout();
            SuspendLayout();
            // 
            // ExportButton
            // 
            ExportButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(ExportButton, "ExportButton");
            ExportButton.Name = "ExportButton";
            ExportButton.UseVisualStyleBackColor = false;
            ExportButton.Click += ExportButton_Click1;
            // 
            // ChangePriorityButton
            // 
            ChangePriorityButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(ChangePriorityButton, "ChangePriorityButton");
            ChangePriorityButton.Name = "ChangePriorityButton";
            ChangePriorityButton.UseVisualStyleBackColor = false;
            ChangePriorityButton.Click += ChangePriorityButton_Click1;
            // 
            // RenameButton
            // 
            RenameButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(RenameButton, "RenameButton");
            RenameButton.Name = "RenameButton";
            RenameButton.UseVisualStyleBackColor = false;
            RenameButton.Click += RenameButton_Click;
            // 
            // SelectProfileButton
            // 
            SelectProfileButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(SelectProfileButton, "SelectProfileButton");
            SelectProfileButton.Name = "SelectProfileButton";
            SelectProfileButton.UseVisualStyleBackColor = false;
            SelectProfileButton.Click += SelectProfileButton_Click;
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
            // Profiles
            // 
            resources.ApplyResources(Profiles, "Profiles");
            Profiles.FormattingEnabled = true;
            Profiles.Name = "Profiles";
            // 
            // ClearButton
            // 
            ClearButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(ClearButton, "ClearButton");
            ClearButton.Name = "ClearButton";
            ClearButton.UseVisualStyleBackColor = false;
            ClearButton.Click += ClearButton_Click;
            // 
            // AddButton
            // 
            AddButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(AddButton, "AddButton");
            AddButton.Name = "AddButton";
            AddButton.UseVisualStyleBackColor = false;
            AddButton.Click += AddButton_Click;
            // 
            // ProfileInput
            // 
            resources.ApplyResources(ProfileInput, "ProfileInput");
            ProfileInput.Name = "ProfileInput";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(ExportButton);
            panel1.Controls.Add(ChangePriorityButton);
            panel1.Controls.Add(RenameButton);
            panel1.Controls.Add(SelectProfileButton);
            panel1.Controls.Add(ProfilesListLab);
            panel1.Controls.Add(DeleteButton);
            panel1.Controls.Add(BackButton);
            panel1.Controls.Add(Profiles);
            panel1.Controls.Add(ClearButton);
            panel1.Controls.Add(AddButton);
            panel1.Controls.Add(ProfileInput);
            panel1.Controls.Add(NameBackground);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // ProfilesListLab
            // 
            resources.ApplyResources(ProfilesListLab, "ProfilesListLab");
            ProfilesListLab.ForeColor = SystemColors.ButtonHighlight;
            ProfilesListLab.Name = "ProfilesListLab";
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(CloseButtonLab);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(ProfilesLab);
            resources.ApplyResources(NameBackground, "NameBackground");
            NameBackground.Name = "NameBackground";
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
            // 
            // CloseButtonLab
            // 
            resources.ApplyResources(CloseButtonLab, "CloseButtonLab");
            CloseButtonLab.Name = "CloseButtonLab";
            CloseButtonLab.Click += CloseButtonLab_Click;
            CloseButtonLab.MouseEnter += CloseButtonLab_MouseEnter;
            CloseButtonLab.MouseLeave += CloseButtonLab_MouseLeave;
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
            // 
            // ProfilesLab
            // 
            resources.ApplyResources(ProfilesLab, "ProfilesLab");
            ProfilesLab.Name = "ProfilesLab";
            ProfilesLab.MouseDown += ProfilesLab_MouseDown;
            ProfilesLab.MouseMove += ProfilesLab_MouseMove;
            // 
            // ProfileManager
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "ProfileManager";
            KeyDown += ChangeProfileForm_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            NameBackground.ResumeLayout(false);
            NameBackground.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button ExportButton;
        private Button ChangePriorityButton;
        private Button RenameButton;
        private Button SelectProfileButton;
        private Button DeleteButton;
        private Button BackButton;
        private ListBox Profiles;
        private Button ClearButton;
        private Button AddButton;
        private TextBox ProfileInput;
        private Panel panel1;
        private Label ProfilesListLab;
        private Panel NameBackground;
        private Label CloseButtonLab;
        private Label MinimizeButton;
        private Label CloseButton;
        private Label ProfilesLab;
    }
}