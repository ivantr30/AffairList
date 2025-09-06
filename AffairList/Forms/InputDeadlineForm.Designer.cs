namespace AffairList
{
    partial class InputDeadlineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDeadlineForm));
            DeadlinePicker = new DateTimePicker();
            ConfirmButton = new Button();
            BackButton = new Button();
            SuspendLayout();
            // 
            // DeadlinePicker
            // 
            DeadlinePicker.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(DeadlinePicker, "DeadlinePicker");
            DeadlinePicker.Name = "DeadlinePicker";
            DeadlinePicker.Value = new DateTime(2025, 7, 25, 0, 0, 0, 0);
            DeadlinePicker.KeyDown += DeadlinePicker_KeyDown;
            // 
            // ConfirmButton
            // 
            ConfirmButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(ConfirmButton, "ConfirmButton");
            ConfirmButton.ForeColor = SystemColors.MenuText;
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.UseVisualStyleBackColor = false;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            resources.ApplyResources(BackButton, "BackButton");
            BackButton.ForeColor = SystemColors.MenuText;
            BackButton.Name = "BackButton";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // InputDeadlineForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            Controls.Add(BackButton);
            Controls.Add(ConfirmButton);
            Controls.Add(DeadlinePicker);
            ForeColor = SystemColors.WindowFrame;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "InputDeadlineForm";
            ShowInTaskbar = false;
            TransparencyKey = SystemColors.WindowFrame;
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker DeadlinePicker;
        private Button ConfirmButton;
        private Button BackButton;
    }
}