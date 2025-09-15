using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class SettingsManager : UserControl, IChildable, IKeyPreviewable
    {
        private bool _isConfirmed = true;

        private Settings _settings;

        public IParentable ParentElement { get; private set; }
        public KeyEventHandler KeyDownHandlers { get; private set; }
        public KeyPressEventHandler KeyPressHandlers { get; private set; }
        public KeyEventHandler KeyUpHandlers { get; private set; }

        public SettingsManager(Settings settings, IParentable parentElement)
        {
            _settings = settings;
            ParentElement = parentElement;
            KeyDownHandlers += Settings_KeyDown!;
            InitializeComponent();
            LoadSettings();
        }
        private void LoadSettings()
        {
            LocationLab.Text = _settings.GetProfileX() + ", " + _settings.GetProfileY();
            ListTextColorLab.ForeColor = _settings.GetTextColor();
            ListBgTextColorLab.ForeColor = _settings.GetBgColor();
            if (_settings.DoesAutostart())
            {
                autostartStateLab.Text = "On";
            }
            else
            {
                autostartStateLab.Text = "OFF";
            }
            if (_settings.DoesAskToDelete())
            {
                AskToDeleteState.Text = "On";
            }
            else
            {
                AskToDeleteState.Text = "OFF";
            }
            if (_settings.DoesNotificate())
            {
                NotificationState.Text = "On";
            }
            else
            {
                NotificationState.Text = "OFF";
            }
            DistanceToNotificate.Text = _settings.GetNotificationDayDistance().ToString();
        }
        private void CloseButton_Click(object sender, EventArgs e) => ParentElement.Exit();

        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _settings.GetCloseKey())
            {
                ParentElement.Exit();
            }
            if (e.KeyCode == _settings.GetReturnKey())
            {
                ParentElement.Return();
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
            => ParentElement.SetLastPoint(e);

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);

        private void SettingsLab_MouseDown(object sender, MouseEventArgs e)
            => ParentElement.SetLastPoint(e);

        private void SettingsLab_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (!_isConfirmed)
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
            ParentElement.Return();
        }

        private async void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Do you really want to reset the settings?",
                "Confirm window",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (!_settings.SettingsFileExists()) await _settings.CreateSettingsFileAsync();

                await _settings.WriteBaseSettingsAsync();

                LoadSettings();
            }
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            _isConfirmed = true;
            await _settings.SaveSettingsAsync();
        }

        private void autostartStateLab_MouseDown(object sender, MouseEventArgs e)
        {
            // Решить, как реализовать логику моментального применения состояния автостарта
            if (autostartStateLab.Text == "On")
            {
                autostartStateLab.Text = "OFF";
                _settings.SetAutostart(false);
            }
            else
            {
                autostartStateLab.Text = "On";
                _settings.SetAutostart(true);
            }
            _isConfirmed = false;
        }

        private void autostartStateLab_MouseLeave(object sender, EventArgs e)
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
            _settings.SetTextColor(newColor);
        }

        private void PickBgColorButton_Click(object sender, EventArgs e)
        {
            Color newColor = ListColorChanger();
            if (newColor == Color.Empty) return;
            ListBgTextColorLab.ForeColor = newColor;
            _settings.SetBgColor(newColor);
        }
        private Color ListColorChanger()
        {
            var dialogResult = ColorPicker.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _isConfirmed = false;
            }
            else
            {
                return Color.Empty;
            }
            return ColorPicker.Color;
        }

        private void LocationLab_DoubleClick(object sender, EventArgs e)
        {
            int prevX = _settings.GetProfileX(), prevY = _settings.GetProfileY();
            try
            {
                string newX = Interaction.InputBox("Enter x coordinate," +
                    $" max width is {_settings.screenWidth}",
                    "InputWindow", "");
                if (string.IsNullOrEmpty(newX)) return;
                _settings.SetProfileX(int.Parse(newX));
                if (_settings.GetProfileX() > _settings.screenWidth
                    || _settings.GetProfileX() < 0) throw new ArgumentException();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error, x is out of the screen width");
                _settings.SetProfileX(prevX);
                return;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
                _settings.SetProfileX(prevX);
                return;
            }
            try
            {
                string newY = Interaction.InputBox("Enter y coordinate" +
                    $" max height is {_settings.screenHeight}",
                    "InputWindow", "");
                if (string.IsNullOrEmpty(newY)) return;
                _settings.SetProfileY(int.Parse(newY));
                if (_settings.GetProfileY() > _settings.screenHeight
                    || _settings.GetProfileY() < 0) throw new ArgumentException();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error, y is out of the screen height");
                _settings.SetProfileY(prevY);
                return;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
                _settings.SetProfileY(prevY);
                return;
            }
            _isConfirmed = false;
            LocationLab.Text = _settings.GetProfileX() + ", " + _settings.GetProfileY();
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
            if (AskToDeleteState.Text == "On")
            {
                AskToDeleteState.Text = "OFF";
                _settings.SetAskToDelete(false);
            }
            else
            {
                AskToDeleteState.Text = "On";
                _settings.SetAskToDelete(true);
            }
            _isConfirmed = false;
        }

        private void AskToDeleteState_MouseLeave(object sender, EventArgs e)
        {
            AskToDeleteState.ForeColor = Color.White;
        }

        private void AskToDeleteState_MouseEnter(object sender, EventArgs e)
        {
            AskToDeleteState.ForeColor = Color.Gray;
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
            => ParentElement.MinimizeForm();

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }

        private void NotificationState_MouseDown(object sender, MouseEventArgs e)
        {
            _settings.SetDoesNotificate(!_settings.DoesNotificate());
            NotificationState.Text = _settings.DoesNotificate() ? "On" : "OFF";
            _isConfirmed = false;
        }

        private void NotificationState_MouseEnter(object sender, EventArgs e)
        {
            NotificationState.ForeColor = Color.Gray;
        }

        private void NotificationState_MouseLeave(object sender, EventArgs e)
        {
            NotificationState.ForeColor = Color.White;
        }
        private void DistanceToNotificate_MouseEnter(object sender, EventArgs e)
        {
            DistanceToNotificate.ForeColor = Color.Gray;
        }

        private void DistanceToNotificate_MouseLeave(object sender, EventArgs e)
        {
            DistanceToNotificate.ForeColor = Color.White;
        }

        private void DistanceToNotificate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string distance = Interaction.InputBox(
                    "Enter how many days far from the deadline you want to be notified",
                    "Day distance to notificate input box",
                    _settings.GetNotificationDayDistance().ToString());

                if (string.IsNullOrEmpty(distance)) return;

                uint distanceToNotificate = uint.Parse(distance);
                _settings.SetNotificationDayDistance(distanceToNotificate);
                DistanceToNotificate.Text = distanceToNotificate.ToString();
                _isConfirmed = false;
            }
            catch
            {
                MessageBox.Show("Error, wrong input type");
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ExportSettingsFileDialog.ShowDialog();
            try
            {
                File.Copy("settings.json", ExportSettingsFileDialog.SelectedPath + "\\settings.json");
                MessageBox.Show("Settings config was exported succesfully");
            }
            catch
            {
                MessageBox.Show("Error during file copying operation");
            }
        }

        private void ThemeBoxCB_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}
