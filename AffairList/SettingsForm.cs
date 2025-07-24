using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class SettingsForm : BaseForm
    {
        private bool isConfirmed = true;
        public SettingsForm(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadSettings();
        }
        private void LoadSettings()
        {
            LocationLab.Text = settings.GetProfileX() + ", " + settings.GetProfileY();
            ListTextColorLab.ForeColor = settings.GetTextColor();
            ListBgTextColorLab.ForeColor = settings.GetBgColor();
            if (settings.DoesAutostart())
            {
                autostartStateLab.Text = "On";
            }
            else
            {
                autostartStateLab.Text = "OFF";
            }
            if (settings.DoesAskToDelete())
            {
                AskToDeleteState.Text = "On";
            }
            else
            {
                AskToDeleteState.Text = "OFF";
            }
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == settings.GetCloseKey())
            {
                Exit();
            }
            if (e.KeyCode == settings.GetReturnKey())
            {
                Restart();
            }
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
        {
            SetLastPoint(e);
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e);
        }

        private void SettingsLab_MouseDown(object sender, MouseEventArgs e)
        {
            SetLastPoint(e);
        }

        private void SettingsLab_MouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (!isConfirmed)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure to leave with unsaved settings?",
                    "Confirm window",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            Restart();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Do you really want to reset the settings?",
                "Confirm window",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (!settings.SettingsFileExists()) settings.CreateSettingsFile();

                settings.WriteBaseSettings();

                MessageBox.Show("The settings were reseted succesfully");
            }
            Restart();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            isConfirmed = true;
            settings.SaveSettings();
        }

        private void autostartStateLab_MouseDown(object sender, MouseEventArgs e)
        {
            autostartStateLab.ForeColor = Color.DarkGray;
            if (autostartStateLab.Text == "On")
            {
                autostartStateLab.Text = "OFF";
                settings.SetAutostart(false);
            }
            else
            {
                autostartStateLab.Text = "On";
                settings.SetAutostart(true);
            }
            isConfirmed = false;
        }

        private void autostartStateLab_MouseLeave(object sender, EventArgs e)
        {
            autostartStateLab.ForeColor = Color.White;
        }

        private void autostartStateLab_MouseUp(object sender, MouseEventArgs e)
        {
            autostartStateLab.ForeColor = Color.White;
        }

        private void autostartStateLab_MouseEnter(object sender, EventArgs e)
        {
            autostartStateLab.ForeColor = Color.Gray;
        }

        private void PickTextColorButton_Click(object sender, EventArgs e)
        {
            Color newColor = ListColorChanger();
            if (newColor == Color.Empty) return;
            ListTextColorLab.ForeColor = newColor;
            settings.SetTextColor(newColor);
        }

        private void PickBgColorButton_Click(object sender, EventArgs e)
        {
            Color newColor = ListColorChanger();
            if (newColor == Color.Empty) return;
            ListBgTextColorLab.ForeColor = newColor;
            settings.SetBgColor(newColor);
        }
        private Color ListColorChanger()
        {
            var dialogResult = ColorPicker.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                isConfirmed = false;
            }
            else
            {
                return Color.Empty;
            }
            return ColorPicker.Color;
        }

        private void LocationLab_DoubleClick(object sender, EventArgs e)
        {
            int prevX = settings.GetProfileX(), prevY = settings.GetProfileY();
            try
            {
                settings.SetProfileX(int.Parse(Interaction.InputBox("Enter x coordinate," +
                    $" max width is {settings.width}",
                    "InputWindow", "")));
                if(settings.GetProfileX() > settings.width) throw new Exception();
            }
            catch
            {
                MessageBox.Show("Error");
                settings.SetProfileX(prevX);
                return;
            }
            try
            {
                settings.SetProfileY(int.Parse(Interaction.InputBox("Enter y coordinate" +
                    $" max height is {settings.height}",
                    "InputWindow", "")));
                if (settings.GetProfileY() > settings.height) throw new Exception();
            }
            catch
            {
                MessageBox.Show("Error");
                settings.SetProfileY(prevY);
                return;
            }
            isConfirmed = false;
            LocationLab.Text = settings.GetProfileX() + ", " + settings.GetProfileY();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }

        private void LocationLab_MouseEnter(object sender, EventArgs e)
        {
            LocationLab.ForeColor = Color.Gray;
        }

        private void LocationLab_MouseLeave(object sender, EventArgs e)
        {
            LocationLab.ForeColor = Color.White;
        }

        private void AskToDeleteState_MouseDown(object sender, MouseEventArgs e)
        {
            AskToDeleteState.ForeColor = Color.DarkGray;
            if (AskToDeleteState.Text == "On")
            {
                AskToDeleteState.Text = "OFF";
                settings.SetAskToDelete(false);
            }
            else
            {
                AskToDeleteState.Text = "On";
                settings.SetAskToDelete(true);
            }
            isConfirmed = false;
        }

        private void AskToDeleteState_MouseLeave(object sender, EventArgs e)
        {
            AskToDeleteState.ForeColor = Color.White;
        }

        private void AskToDeleteState_MouseEnter(object sender, EventArgs e)
        {
            AskToDeleteState.ForeColor = Color.Gray;
        }

        private void AskToDeleteState_MouseUp(object sender, MouseEventArgs e)
        {
            AskToDeleteState.ForeColor = Color.White;
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            MinimizeForm();
        }

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }
    }
}
