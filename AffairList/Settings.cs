using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class Settings : BaseForm
    {
        private bool isConfirmed = true;
        public Settings(SettingsModel settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadSettings();
        }
        private void LoadSettings()
        {
            LocationLab.Text = settings.x + ", " + settings.y;
            ListTextColorLab.ForeColor = settings.textColor;
            ListBgTextColorLab.ForeColor = settings.bgtextColor;
            if (settings.autostartState)
            {
                autostartStateLab.Text = "On";
            }
            else
            {
                autostartStateLab.Text = "OFF";
            }
            if (settings.askToDelete)
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
            if (e.KeyCode == settings.closeKey)
            {
                Exit();
            }
            if (e.KeyCode == settings.returnKey)
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
            lastPoint = new Point(e.X, e.Y);
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }

        private void SettingsLab_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void SettingsLab_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
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
                settings.autostartState = false;
                return;
            }
            autostartStateLab.Text = "On";
            settings.autostartState = true;
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
            var res = ColorPicker.ShowDialog();
            if (res == DialogResult.OK)
            {
                ListTextColorLab.ForeColor = ColorPicker.Color;
                settings.textColor = ListTextColorLab.ForeColor;
                isConfirmed = false;
            }
        }

        private void PickBgColorButton_Click(object sender, EventArgs e)
        {
            var res = ColorPicker.ShowDialog();
            if (res == DialogResult.OK)
            {
                ListBgTextColorLab.ForeColor = ColorPicker.Color;
                settings.bgtextColor = ListBgTextColorLab.ForeColor;
                isConfirmed = false;
            }
        }

        private void LocationLab_DoubleClick(object sender, EventArgs e)
        {
            int prevX = settings.x, prevY = settings.y;
            try
            {
                settings.x = int.Parse(Interaction.InputBox("Enter x coordinate," +
                    $" max width is {settings.width}",
                    "InputWindow", ""));
                if(settings.x > settings.width) throw new Exception();
            }
            catch
            {
                MessageBox.Show("Error");
                settings.x = prevX;
                return;
            }
            try
            {
                settings.y = int.Parse(Interaction.InputBox("Enter y coordinate" +
                    $" max height is {settings.height}", "InputWindow", ""));
                if (settings.y > settings.height) throw new Exception();
            }
            catch
            {
                MessageBox.Show("Error");
                settings.y = prevY;
                return;
            }
            isConfirmed = false;
            LocationLab.Text = settings.x + ", " + settings.y;
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
                settings.askToDelete = false;
                return;
            }
            AskToDeleteState.Text = "On";
            settings.askToDelete = true;
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
            WindowState = FormWindowState.Minimized;
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
