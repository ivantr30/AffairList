using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class HotKeySettings : BaseForm
    {
        private bool isConfirmed = true;
        public HotKeySettings(SettingsModel settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadSettings();
        }
        private void LoadSettings()
        {
            CloseKeyType.Text = settings.closeKey.ToString();
            BackKeyType.Text = settings.returnKey.ToString();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (!isConfirmed)
            {
                DialogResult resetOrNot = MessageBox.Show("Are you sure to leave with unsaved settings?",
                "Confirm form",
                MessageBoxButtons.YesNo);
                if (resetOrNot == DialogResult.No) return;
            }
            Restart();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult resetOrNot = MessageBox.Show("Are you sure to reset hotkey settings?",
                "Confirm form",
                MessageBoxButtons.YesNo);
            if (resetOrNot == DialogResult.No) return;

            settings.SaveParametr("closeKey", Keys.F7.ToString(), "");
            settings.SaveParametr("returnKey", Keys.F6.ToString(), "");

            CloseKeyType.Text = "F7";
            BackKeyType.Text = "F6";

            isConfirmed = false;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            settings.SaveParametr("closeKey", settings.closeKey.ToString(), "");
            settings.SaveParametr("returnKey", settings.returnKey.ToString(), "");
            isConfirmed = true;
        }

        private void CloseKeyType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                settings.closeKey = (Keys)Enum.Parse(typeof(Keys),
                    Interaction.InputBox("Enter close key", "KeyInputBox"), true);

                CloseKeyType.Text = settings.closeKey.ToString();
                isConfirmed = false;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
            }
        }

        private void BackKeyType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                settings.returnKey = (Keys)Enum.Parse(typeof(Keys),
                    Interaction.InputBox("Enter return key", "KeyInputBox"), true);

                BackKeyType.Text = settings.returnKey.ToString();
                isConfirmed = false;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
            }
        }

        private void CloseKeyType_MouseEnter(object sender, EventArgs e)
        {
            CloseKeyType.ForeColor = Color.Gray;
        }

        private void CloseKeyType_MouseUp(object sender, MouseEventArgs e)
        {
            CloseKeyType.ForeColor = Color.White;
        }

        private void BackKeyType_MouseEnter(object sender, EventArgs e)
        {
            BackKeyType.ForeColor = Color.Gray;
        }

        private void BackKeyType_MouseUp(object sender, MouseEventArgs e)
        {
            BackKeyType.ForeColor = Color.White;
        }

        private void BackKeyType_MouseLeave(object sender, EventArgs e)
        {
            BackKeyType.ForeColor = Color.White;
        }

        private void CloseKeyType_MouseLeave(object sender, EventArgs e)
        {
            CloseKeyType.ForeColor = Color.White;
        }
    }
}
