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
            ProfilesLab = new Label();
            NameBackground = new Panel();
            CloseButtonLab = new Label();
            MinimizeButton = new Label();
            CloseButton = new Label();
            ProfilesListLab = new Label();
            panel1 = new Panel();
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
            NameBackground.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ProfilesLab
            // 
            resources.ApplyResources(ProfilesLab, "ProfilesLab");
            ProfilesLab.Name = "ProfilesLab";
            ProfilesLab.MouseDown += ProfilesLab_MouseDown;
            ProfilesLab.MouseMove += ProfilesLab_MouseMove;
            // 
            // NameBackground
            // 
            resources.ApplyResources(NameBackground, "NameBackground");
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(CloseButtonLab);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(ProfilesLab);
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
            // ProfilesListLab
            // 
            resources.ApplyResources(ProfilesListLab, "ProfilesListLab");
            ProfilesListLab.ForeColor = SystemColors.ButtonHighlight;
            ProfilesListLab.Name = "ProfilesListLab";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
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
            panel1.Name = "panel1";
            // 
            // ExportButton
            // 
            resources.ApplyResources(ExportButton, "ExportButton");
            ExportButton.BackColor = Color.FromArgb(173, 102, 213);
            ExportButton.Name = "ExportButton";
            ExportButton.UseVisualStyleBackColor = false;
            ExportButton.Click += ExportButton_Click;
            // 
            // ChangePriorityButton
            // 
            resources.ApplyResources(ChangePriorityButton, "ChangePriorityButton");
            ChangePriorityButton.BackColor = Color.FromArgb(173, 102, 213);
            ChangePriorityButton.Name = "ChangePriorityButton";
            ChangePriorityButton.UseVisualStyleBackColor = false;
            ChangePriorityButton.Click += ChangePriorityButton_Click;
            // 
            // RenameButton
            // 
            resources.ApplyResources(RenameButton, "RenameButton");
            RenameButton.BackColor = Color.FromArgb(173, 102, 213);
            RenameButton.Name = "RenameButton";
            RenameButton.UseVisualStyleBackColor = false;
            RenameButton.Click += RenameButton_Click;
            // 
            // SelectProfileButton
            // 
            resources.ApplyResources(SelectProfileButton, "SelectProfileButton");
            SelectProfileButton.BackColor = Color.FromArgb(173, 102, 213);
            SelectProfileButton.Name = "SelectProfileButton";
            SelectProfileButton.UseVisualStyleBackColor = false;
            SelectProfileButton.Click += SelectProfileButton_Click;
            // 
            // DeleteButton
            // 
            resources.ApplyResources(DeleteButton, "DeleteButton");
            DeleteButton.BackColor = Color.FromArgb(173, 102, 213);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.UseVisualStyleBackColor = false;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // BackButton
            // 
            resources.ApplyResources(BackButton, "BackButton");
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
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
            resources.ApplyResources(ClearButton, "ClearButton");
            ClearButton.BackColor = Color.FromArgb(173, 102, 213);
            ClearButton.Name = "ClearButton";
            ClearButton.UseVisualStyleBackColor = false;
            ClearButton.Click += ClearButton_Click;
            // 
            // AddButton
            // 
            resources.ApplyResources(AddButton, "AddButton");
            AddButton.BackColor = Color.FromArgb(173, 102, 213);
            AddButton.Name = "AddButton";
            AddButton.UseVisualStyleBackColor = false;
            AddButton.Click += AddButton_Click;
            // 
            // ProfileInput
            // 
            resources.ApplyResources(ProfileInput, "ProfileInput");
            ProfileInput.Name = "ProfileInput";
            // 
            // ProfileManager
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "ProfileManager";
            NameBackground.ResumeLayout(false);
            NameBackground.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label ProfilesLab;
        private Panel NameBackground;
        private Label MinimizeButton;
        private Label CloseButton;
        private Label ProfilesListLab;
        private Panel panel1;
        private Button DeleteButton;
        private Button BackButton;
        private ListBox Profiles;
        private Button ClearButton;
        private Button AddButton;
        private TextBox ProfileInput;
        private Label CloseButtonLab;
        private Button SelectProfileButton;
        private Button RenameButton;
        private Button ChangePriorityButton;
        private Button ExportButton;
    }
}