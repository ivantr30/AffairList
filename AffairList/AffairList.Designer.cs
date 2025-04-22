namespace AffairList
{
    partial class AffairList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            AffairListLab = new Label();
            ClearListButton = new Button();
            ChangeListButton = new Button();
            button2 = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(72, 3, 111);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(ChangeListButton);
            panel1.Controls.Add(ClearListButton);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 480);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(159, 62, 213);
            panel2.Controls.Add(AffairListLab);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(450, 142);
            panel2.TabIndex = 0;
            // 
            // AffairListLab
            // 
            AffairListLab.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            AffairListLab.AutoSize = true;
            AffairListLab.Font = new Font("Unispace", 30F);
            AffairListLab.Location = new Point(94, 51);
            AffairListLab.Name = "AffairListLab";
            AffairListLab.Size = new Size(260, 48);
            AffairListLab.TabIndex = 1;
            AffairListLab.Text = "AffairList";
            AffairListLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ClearListButton
            // 
            ClearListButton.BackColor = Color.FromArgb(173, 102, 213);
            ClearListButton.FlatAppearance.BorderSize = 0;
            ClearListButton.FlatStyle = FlatStyle.Flat;
            ClearListButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            ClearListButton.ForeColor = SystemColors.ButtonHighlight;
            ClearListButton.Location = new Point(35, 290);
            ClearListButton.Name = "ClearListButton";
            ClearListButton.Size = new Size(100, 100);
            ClearListButton.TabIndex = 1;
            ClearListButton.Text = "ClearList";
            ClearListButton.UseVisualStyleBackColor = false;
            // 
            // ChangeListButton
            // 
            ChangeListButton.BackColor = Color.FromArgb(173, 102, 213);
            ChangeListButton.FlatAppearance.BorderSize = 0;
            ChangeListButton.FlatStyle = FlatStyle.Flat;
            ChangeListButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            ChangeListButton.ForeColor = SystemColors.ButtonHighlight;
            ChangeListButton.Location = new Point(175, 290);
            ChangeListButton.Name = "ChangeListButton";
            ChangeListButton.Size = new Size(100, 100);
            ChangeListButton.TabIndex = 2;
            ChangeListButton.Text = "ChangeList";
            ChangeListButton.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(173, 102, 213);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(315, 290);
            button2.Name = "button2";
            button2.Size = new Size(100, 100);
            button2.TabIndex = 3;
            button2.Text = "ClearList";
            button2.UseVisualStyleBackColor = false;
            // 
            // AffairList
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 480);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AffairList";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label AffairListLab;
        private Button button2;
        private Button ChangeListButton;
        private Button ClearListButton;
    }
}
