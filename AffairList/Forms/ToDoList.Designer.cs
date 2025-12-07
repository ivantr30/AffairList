namespace AffairList
{
    partial class ToDoList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToDoList));
            Affairs = new Label();
            SuspendLayout();
            // 
            // Affairs
            // 
            Affairs.AutoSize = true;
            Affairs.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            Affairs.ForeColor = Color.MediumSpringGreen;
            Affairs.Location = new Point(981, 8);
            Affairs.Name = "Affairs";
            Affairs.Size = new Size(0, 26);
            Affairs.TabIndex = 0;
            Affairs.MouseDown += Affairs_MouseDown;
            Affairs.MouseMove += Affairs_MouseMove;
            Affairs.MouseUp += Affairs_MouseUp;
            // 
            // ToDoList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1202, 588);
            Controls.Add(Affairs);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ToDoList";
            Text = "List";
            TransparencyKey = Color.Black;
            FormClosing += ToDoList_FormClosing;
            MouseDown += List_MouseDown;
            MouseMove += List_MouseMove;
            MouseUp += List_MouseUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Affairs;
    }
}