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
            ClickKeyLabel = new Label();
            SuspendLayout();
            // 
            // ClickKeyLabel
            // 
            ClickKeyLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ClickKeyLabel.AutoSize = true;
            ClickKeyLabel.Font = new Font("Berlin Sans FB Demi", 16F, FontStyle.Bold);
            ClickKeyLabel.Location = new Point(57, 121);
            ClickKeyLabel.Name = "ClickKeyLabel";
            ClickKeyLabel.Size = new Size(297, 25);
            ClickKeyLabel.TabIndex = 0;
            ClickKeyLabel.Text = "Click key or escape to return";
            // 
            // InputKeyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(399, 277);
            Controls.Add(ClickKeyLabel);
            KeyPreview = true;
            Name = "InputKeyForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InputKeyForm";
            KeyDown += InputKeyForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ClickKeyLabel;
    }
}