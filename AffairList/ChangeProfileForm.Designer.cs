namespace AffairList
{
    partial class ChangeProfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeProfileForm));
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
            ProfilesLab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProfilesLab.AutoSize = true;
            ProfilesLab.Font = new Font("Unispace", 30F);
            ProfilesLab.Location = new Point(203, 24);
            ProfilesLab.Name = "ProfilesLab";
            ProfilesLab.Size = new Size(212, 48);
            ProfilesLab.TabIndex = 1;
            ProfilesLab.Text = "Profiles";
            ProfilesLab.TextAlign = ContentAlignment.MiddleCenter;
            ProfilesLab.MouseDown += ProfilesLab_MouseDown;
            ProfilesLab.MouseMove += ProfilesLab_MouseMove;
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(CloseButtonLab);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(ProfilesLab);
            NameBackground.Dock = DockStyle.Top;
            NameBackground.Location = new Point(0, 0);
            NameBackground.Name = "NameBackground";
            NameBackground.Size = new Size(600, 86);
            NameBackground.TabIndex = 0;
            NameBackground.MouseDown += NameBackground_MouseDown;
            NameBackground.MouseMove += NameBackground_MouseMove;
            // 
            // CloseButtonLab
            // 
            CloseButtonLab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButtonLab.AutoSize = true;
            CloseButtonLab.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButtonLab.Location = new Point(569, 0);
            CloseButtonLab.Name = "CloseButtonLab";
            CloseButtonLab.Size = new Size(31, 33);
            CloseButtonLab.TabIndex = 11;
            CloseButtonLab.Text = "X";
            CloseButtonLab.Click += CloseButtonLab_Click;
            CloseButtonLab.MouseEnter += CloseButtonLab_MouseEnter;
            CloseButtonLab.MouseLeave += CloseButtonLab_MouseLeave;
            // 
            // MinimizeButton
            // 
            MinimizeButton.AutoSize = true;
            MinimizeButton.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MinimizeButton.Location = new Point(532, 0);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(31, 33);
            MinimizeButton.TabIndex = 4;
            MinimizeButton.Text = "-";
            MinimizeButton.Click += MinimizeButton_Click;
            MinimizeButton.MouseEnter += MinimizeButton_MouseEnter;
            MinimizeButton.MouseLeave += MinimizeButton_MouseLeave;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.AutoSize = true;
            CloseButton.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.Location = new Point(863, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(31, 33);
            CloseButton.TabIndex = 2;
            CloseButton.Text = "X";
            // 
            // ProfilesListLab
            // 
            ProfilesListLab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProfilesListLab.AutoSize = true;
            ProfilesListLab.Font = new Font("Unispace", 15F);
            ProfilesListLab.ForeColor = SystemColors.ButtonHighlight;
            ProfilesListLab.Location = new Point(10, 89);
            ProfilesListLab.Name = "ProfilesListLab";
            ProfilesListLab.Size = new Size(106, 24);
            ProfilesListLab.TabIndex = 3;
            ProfilesListLab.Text = "Profiles";
            ProfilesListLab.TextAlign = ContentAlignment.MiddleCenter;
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
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 405);
            panel1.TabIndex = 2;
            // 
            // ExportButton
            // 
            ExportButton.BackColor = Color.FromArgb(173, 102, 213);
            ExportButton.FlatStyle = FlatStyle.Flat;
            ExportButton.Location = new Point(12, 371);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(62, 27);
            ExportButton.TabIndex = 30;
            ExportButton.Text = "Export";
            ExportButton.UseVisualStyleBackColor = false;
            ExportButton.Click += ExportButton_Click;
            // 
            // ChangePriorityButton
            // 
            ChangePriorityButton.BackColor = Color.FromArgb(173, 102, 213);
            ChangePriorityButton.FlatStyle = FlatStyle.Flat;
            ChangePriorityButton.Font = new Font("Segoe UI", 9.75F);
            ChangePriorityButton.Location = new Point(80, 371);
            ChangePriorityButton.Name = "ChangePriorityButton";
            ChangePriorityButton.Size = new Size(104, 27);
            ChangePriorityButton.TabIndex = 9;
            ChangePriorityButton.Text = "ChangePriority";
            ChangePriorityButton.UseVisualStyleBackColor = false;
            ChangePriorityButton.Click += ChangePriorityButton_Click;
            // 
            // RenameButton
            // 
            RenameButton.BackColor = Color.FromArgb(173, 102, 213);
            RenameButton.FlatStyle = FlatStyle.Flat;
            RenameButton.Location = new Point(322, 371);
            RenameButton.Name = "RenameButton";
            RenameButton.Size = new Size(70, 27);
            RenameButton.TabIndex = 8;
            RenameButton.Text = "Rename";
            RenameButton.UseVisualStyleBackColor = false;
            RenameButton.Click += RenameButton_Click;
            // 
            // SelectProfileButton
            // 
            SelectProfileButton.BackColor = Color.FromArgb(173, 102, 213);
            SelectProfileButton.FlatStyle = FlatStyle.Flat;
            SelectProfileButton.Location = new Point(190, 371);
            SelectProfileButton.Name = "SelectProfileButton";
            SelectProfileButton.Size = new Size(60, 27);
            SelectProfileButton.TabIndex = 7;
            SelectProfileButton.Text = "Select";
            SelectProfileButton.UseVisualStyleBackColor = false;
            SelectProfileButton.Click += SelectProfileButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.BackColor = Color.FromArgb(173, 102, 213);
            DeleteButton.FlatStyle = FlatStyle.Flat;
            DeleteButton.Location = new Point(398, 371);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(60, 27);
            DeleteButton.TabIndex = 6;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = false;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.FlatStyle = FlatStyle.Flat;
            BackButton.Location = new Point(528, 371);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(60, 27);
            BackButton.TabIndex = 5;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // Profiles
            // 
            Profiles.Font = new Font("Segoe UI", 12F);
            Profiles.FormattingEnabled = true;
            Profiles.HorizontalScrollbar = true;
            Profiles.ItemHeight = 21;
            Profiles.Location = new Point(12, 114);
            Profiles.Name = "Profiles";
            Profiles.Size = new Size(576, 214);
            Profiles.TabIndex = 4;
            // 
            // ClearButton
            // 
            ClearButton.BackColor = Color.FromArgb(173, 102, 213);
            ClearButton.FlatStyle = FlatStyle.Flat;
            ClearButton.Location = new Point(464, 371);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(60, 27);
            ClearButton.TabIndex = 3;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = false;
            ClearButton.Click += ClearButton_Click;
            // 
            // AddButton
            // 
            AddButton.BackColor = Color.FromArgb(173, 102, 213);
            AddButton.FlatStyle = FlatStyle.Flat;
            AddButton.Location = new Point(256, 371);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(60, 27);
            AddButton.TabIndex = 2;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = false;
            AddButton.Click += AddButton_Click;
            // 
            // ProfileInput
            // 
            ProfileInput.Font = new Font("Unispace", 15F);
            ProfileInput.Location = new Point(12, 334);
            ProfileInput.Name = "ProfileInput";
            ProfileInput.PlaceholderText = "Enter the name of a new profile";
            ProfileInput.Size = new Size(576, 31);
            ProfileInput.TabIndex = 1;
            // 
            // ChangeProfileForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 405);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "ChangeProfileForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChangeProfileForm";
            KeyDown += ChangeProfileForm_KeyDown;
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