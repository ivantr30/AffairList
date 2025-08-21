namespace AffairList
{
    partial class ProfilesExportPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfilesExportPicker));
            ProfilePicker = new CheckedListBox();
            BackButton = new Button();
            OkButton = new Button();
            ProfilesExportDialog = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // ProfilePicker
            // 
            ProfilePicker.BackColor = Color.FromArgb(159, 62, 213);
            ProfilePicker.BorderStyle = BorderStyle.None;
            ProfilePicker.CheckOnClick = true;
            ProfilePicker.ForeColor = SystemColors.Info;
            ProfilePicker.FormattingEnabled = true;
            ProfilePicker.Location = new Point(12, 12);
            ProfilePicker.Name = "ProfilePicker";
            ProfilePicker.Size = new Size(496, 300);
            ProfilePicker.TabIndex = 0;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.FlatStyle = FlatStyle.Flat;
            BackButton.Location = new Point(448, 324);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(60, 27);
            BackButton.TabIndex = 6;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // OkButton
            // 
            OkButton.BackColor = Color.FromArgb(173, 102, 213);
            OkButton.FlatStyle = FlatStyle.Flat;
            OkButton.Location = new Point(382, 324);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(60, 27);
            OkButton.TabIndex = 7;
            OkButton.Text = "Ok";
            OkButton.UseVisualStyleBackColor = false;
            OkButton.Click += OkButton_Click;
            // 
            // ProfilesExportPicker
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(72, 3, 111);
            ClientSize = new Size(520, 358);
            Controls.Add(OkButton);
            Controls.Add(BackButton);
            Controls.Add(ProfilePicker);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ProfilesExportPicker";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ProfilesExportPicker";
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox ProfilePicker;
        private Button BackButton;
        private Button OkButton;
        private FolderBrowserDialog ProfilesExportDialog;
    }
}