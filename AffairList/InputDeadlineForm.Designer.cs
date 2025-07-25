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
            DeadlinePicker = new DateTimePicker();
            ConfirmButton = new Button();
            BackButton = new Button();
            SuspendLayout();
            // 
            // DeadlinePicker
            // 
            DeadlinePicker.Format = DateTimePickerFormat.Short;
            DeadlinePicker.Location = new Point(125, 112);
            DeadlinePicker.Name = "DeadlinePicker";
            DeadlinePicker.Size = new Size(200, 25);
            DeadlinePicker.TabIndex = 0;
            DeadlinePicker.Value = new DateTime(2025, 7, 25, 0, 0, 0, 0);
            // 
            // ConfirmButton
            // 
            ConfirmButton.BackColor = Color.FromArgb(173, 102, 213);
            ConfirmButton.FlatStyle = FlatStyle.Flat;
            ConfirmButton.ForeColor = SystemColors.MenuText;
            ConfirmButton.Location = new Point(125, 143);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.Size = new Size(70, 27);
            ConfirmButton.TabIndex = 3;
            ConfirmButton.Text = "Confirm";
            ConfirmButton.UseVisualStyleBackColor = false;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // BackButton
            // 
            BackButton.BackColor = Color.FromArgb(173, 102, 213);
            BackButton.FlatStyle = FlatStyle.Flat;
            BackButton.ForeColor = SystemColors.MenuText;
            BackButton.Location = new Point(255, 143);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(70, 27);
            BackButton.TabIndex = 4;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // InputDeadlineForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(450, 270);
            Controls.Add(BackButton);
            Controls.Add(ConfirmButton);
            Controls.Add(DeadlinePicker);
            ForeColor = SystemColors.WindowFrame;
            FormBorderStyle = FormBorderStyle.None;
            Name = "InputDeadlineForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InputDeadlineForm";
            TransparencyKey = SystemColors.WindowFrame;
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker DeadlinePicker;
        private Button ConfirmButton;
        private Button BackButton;
    }
}