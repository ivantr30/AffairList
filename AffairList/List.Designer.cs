namespace AffairList
{
    partial class List
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
            Affairs = new Label();
            SuspendLayout();
            // 
            // Affairs
            // 
            Affairs.AutoSize = true;
            Affairs.Font = new Font("Gill Sans Ultra Bold", 12F, FontStyle.Bold);
            Affairs.ForeColor = Color.White;
            Affairs.Location = new Point(981, 9);
            Affairs.Name = "Affairs";
            Affairs.Size = new Size(39, 23);
            Affairs.TabIndex = 0;
            Affairs.Text = "хуй";
            // 
            // List
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1061, 640);
            Controls.Add(Affairs);
            FormBorderStyle = FormBorderStyle.None;
            Name = "List";
            Text = "List";
            TransparencyKey = Color.Black;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Affairs;
    }
}