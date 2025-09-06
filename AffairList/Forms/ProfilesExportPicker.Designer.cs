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
            resources.ApplyResources(ProfilePicker, "ProfilePicker");
            ProfilePicker.BackColor = Color.FromArgb(159, 62, 213);
            ProfilePicker.BorderStyle = BorderStyle.None;
            ProfilePicker.CheckOnClick = true;
            ProfilePicker.ForeColor = SystemColors.Info;
            ProfilePicker.FormattingEnabled = true;
            ProfilePicker.Name = "ProfilePicker";
            // 
            // BackButton
            // 
            resources.ApplyResources(BackButton, "BackButton");
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.Name = "BackButton";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // OkButton
            // 
            resources.ApplyResources(OkButton, "OkButton");
            OkButton.BackColor = Color.FromArgb(173, 102, 213);
            OkButton.Name = "OkButton";
            OkButton.UseVisualStyleBackColor = false;
            OkButton.Click += OkButton_Click;
            // 
            // ProfilesExportDialog
            // 
            resources.ApplyResources(ProfilesExportDialog, "ProfilesExportDialog");
            // 
            // ProfilesExportPicker
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(72, 3, 111);
            Controls.Add(OkButton);
            Controls.Add(BackButton);
            Controls.Add(ProfilePicker);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProfilesExportPicker";
            ShowIcon = false;
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox ProfilePicker;
        private Button BackButton;
        private Button OkButton;
        private FolderBrowserDialog ProfilesExportDialog;
    }
}