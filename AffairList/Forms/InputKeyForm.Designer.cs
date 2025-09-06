namespace AffairList
{
    partial class InputKeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputKeyForm));
            ClickKeyLabel = new Label();
            SuspendLayout();
            // 
            // ClickKeyLabel
            // 
            resources.ApplyResources(ClickKeyLabel, "ClickKeyLabel");
            ClickKeyLabel.Name = "ClickKeyLabel";
            // 
            // InputKeyForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ClickKeyLabel);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "InputKeyForm";
            ShowIcon = false;
            KeyDown += InputKeyForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ClickKeyLabel;
    }
}