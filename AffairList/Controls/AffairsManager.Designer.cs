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
            MinimizeButton = new Label();
            NameBackground = new Panel();
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
            CloseButton = new Label();
            NameBackground.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // MinimizeButton
            // 
            MinimizeButton.AutoSize = true;
            MinimizeButton.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MinimizeButton.Location = new Point(495, 0);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(31, 33);
            MinimizeButton.TabIndex = 4;
            MinimizeButton.Text = "-";
            // 
            // NameBackground
            // 
            NameBackground.BackColor = Color.FromArgb(159, 62, 213);
            NameBackground.Controls.Add(CloseButton);
            NameBackground.Controls.Add(MinimizeButton);
            NameBackground.Controls.Add(AffairsLab);
            NameBackground.Dock = DockStyle.Top;
            NameBackground.Location = new Point(0, 0);
            NameBackground.Name = "NameBackground";
            NameBackground.Size = new Size(563, 86);
            NameBackground.TabIndex = 0;
            // 
            // AffairsLab
            // 
            AffairsLab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AffairsLab.AutoSize = true;
            AffairsLab.Font = new Font("Unispace", 30F);
            AffairsLab.Location = new Point(200, 26);
            AffairsLab.Name = "AffairsLab";
            AffairsLab.Size = new Size(188, 48);
            AffairsLab.TabIndex = 1;
            AffairsLab.Text = "Affairs";
            AffairsLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ProfileBox
            // 
            ProfileBox.FormattingEnabled = true;
            ProfileBox.Location = new Point(372, 88);
            ProfileBox.Name = "ProfileBox";
            ProfileBox.Size = new Size(179, 25);
            ProfileBox.TabIndex = 11;
            // 
            // DeadlineLab
            // 
            DeadlineLab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            DeadlineLab.AutoSize = true;
            DeadlineLab.Font = new Font("Unispace", 15F);
            DeadlineLab.ForeColor = SystemColors.ButtonHighlight;
            DeadlineLab.Location = new Point(10, 89);
            DeadlineLab.Name = "DeadlineLab";
            DeadlineLab.Size = new Size(262, 24);
            DeadlineLab.TabIndex = 3;
            DeadlineLab.Text = "Deadline, Affair name";
            DeadlineLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Affairs
            // 
            Affairs.Font = new Font("Segoe UI", 12F);
            Affairs.FormattingEnabled = true;
            Affairs.HorizontalScrollbar = true;
            Affairs.ItemHeight = 21;
            Affairs.Location = new Point(12, 114);
            Affairs.Name = "Affairs";
            Affairs.Size = new Size(539, 214);
            Affairs.TabIndex = 4;
            // 
            // AffairInput
            // 
            AffairInput.Font = new Font("Unispace", 15F);
            AffairInput.Location = new Point(12, 334);
            AffairInput.Name = "AffairInput";
            AffairInput.PlaceholderText = "Enter the name of a new affair";
            AffairInput.Size = new Size(539, 31);
            AffairInput.TabIndex = 1;
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
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(563, 405);
            panel1.TabIndex = 2;
            // 
            // RenameAffairButton
            // 
            RenameAffairButton.BackColor = Color.FromArgb(173, 102, 213);
            RenameAffairButton.FlatStyle = FlatStyle.Flat;
            RenameAffairButton.Location = new Point(358, 371);
            RenameAffairButton.Name = "RenameAffairButton";
            RenameAffairButton.Size = new Size(65, 27);
            RenameAffairButton.TabIndex = 12;
            RenameAffairButton.Text = "Rename";
            RenameAffairButton.UseVisualStyleBackColor = false;
            // 
            // AddDeadlineButton
            // 
            AddDeadlineButton.BackColor = Color.FromArgb(173, 102, 213);
            AddDeadlineButton.FlatStyle = FlatStyle.Flat;
            AddDeadlineButton.Location = new Point(120, 371);
            AddDeadlineButton.Name = "AddDeadlineButton";
            AddDeadlineButton.Size = new Size(100, 27);
            AddDeadlineButton.TabIndex = 10;
            AddDeadlineButton.Text = "AddDeadline";
            AddDeadlineButton.UseVisualStyleBackColor = false;
            // 
            // PriorityButton
            // 
            PriorityButton.BackColor = Color.FromArgb(173, 102, 213);
            PriorityButton.FlatStyle = FlatStyle.Flat;
            PriorityButton.Font = new Font("Segoe UI", 9F);
            PriorityButton.Location = new Point(12, 371);
            PriorityButton.Name = "PriorityButton";
            PriorityButton.Size = new Size(102, 27);
            PriorityButton.TabIndex = 8;
            PriorityButton.Text = "ChangePriority";
            PriorityButton.UseVisualStyleBackColor = false;
            // 
            // DeleteButton
            // 
            DeleteButton.BackColor = Color.FromArgb(173, 102, 213);
            DeleteButton.FlatStyle = FlatStyle.Flat;
            DeleteButton.Location = new Point(292, 371);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(60, 27);
            DeleteButton.TabIndex = 6;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = false;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.FlatStyle = FlatStyle.Flat;
            BackButton.Location = new Point(495, 371);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(56, 27);
            BackButton.TabIndex = 5;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            // 
            // ClearButton
            // 
            ClearButton.BackColor = Color.FromArgb(173, 102, 213);
            ClearButton.FlatStyle = FlatStyle.Flat;
            ClearButton.Location = new Point(429, 371);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(60, 27);
            ClearButton.TabIndex = 3;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = false;
            // 
            // AddAffairButton
            // 
            AddAffairButton.BackColor = Color.FromArgb(173, 102, 213);
            AddAffairButton.FlatStyle = FlatStyle.Flat;
            AddAffairButton.Location = new Point(226, 371);
            AddAffairButton.Name = "AddAffairButton";
            AddAffairButton.Size = new Size(60, 27);
            AddAffairButton.TabIndex = 2;
            AddAffairButton.Text = "Add";
            AddAffairButton.UseVisualStyleBackColor = false;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.AutoSize = true;
            CloseButton.Font = new Font("Unispace", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.Location = new Point(532, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(31, 33);
            CloseButton.TabIndex = 3;
            CloseButton.Text = "X";
            // 
            // AffairsManager
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "AffairsManager";
            Size = new Size(563, 405);
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
