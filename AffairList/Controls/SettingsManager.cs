using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class SettingsManager : UserControl, IChildable
    {
        private Settings _settings;

        public IParentable ParentElement { get; private set; }

        private Settings _newSettings;
        public SettingsManager(Settings settings, IParentable parentElement)
        {
            InitializeComponent();
            _settings = settings;
            _newSettings = _settings.GetSettingsCopy();
            ParentElement = parentElement;
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
                autostartStateLab.Location.ChangeXPos(-14);
            }
            if (_settings.DoesAskToDelete())
            {
                AskToDeleteState.Text = "On";
            }
            else
            {
                AskToDeleteState.Text = "OFF";
                AskToDeleteState.Location.ChangeXPos(-14);
            }
            if (_settings.DoesNotificate())
            {
                NotificationState.Text = "On";
            }
            else
            {
                NotificationState.Text = "OFF";
                NotificationState.Location.ChangeXPos(-14);
            }
            HourDistanceToNotificate.Text = _settings.GetNotificationHourDistance().ToString();
            DistanceToNotificate.Text = _settings.GetNotificationDayDistance().ToString();
        }
        private bool CanLeave()
        {
            if (!_settings.SettingsEqual(_newSettings))
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure to leave with unsaved settings?",
                    "Confirm window",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            ParentElement.Exit();
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

                _settings.SelectFirstProfile();

                _newSettings = _settings.GetSettingsCopy();

                LoadSettings();
            }
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            _settings.SetNewSettings(_newSettings);
            await _settings.SaveSettingsAsync();
        }

        private void autostartStateLab_MouseDown(object sender, MouseEventArgs e)
        {
            _newSettings.SetAutostart(_newSettings.DoesAutostart() ? false : true, false);
            autostartStateLab.Text = _newSettings.DoesAutostart() ? "On" : "OFF";
            autostartStateLab.Location.ChangeXPos(_newSettings.DoesAutostart() ? 14 : -14);
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
            _newSettings.SetTextColor(newColor);
        }
        private void PickBgColorButton_Click(object sender, EventArgs e)
        {
            Color newColor = ListColorChanger();
            if (newColor == Color.Empty) return;
            ListBgTextColorLab.ForeColor = newColor;
            _newSettings.SetBgColor(newColor);
        }
        private Color ListColorChanger()
        {
            var dialogResult = ColorPicker.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return Color.Empty;
            }
            return ColorPicker.Color;
        }

        private void LocationLab_DoubleClick(object sender, EventArgs e)
        {
            int newX, newY;
            try
            {
                newX = int.Parse(Interaction.InputBox(
                    $"Enter x coordinate, max width is {_settings.screenWidth}",
                    "InputWindow", ""));

                if (newX > _settings.screenWidth || newX < 0) throw new ArgumentException();
                _newSettings.SetProfileX(newX);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error, x is out of the screen width");
                return;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
                return;
            }
            try
            {
                newY = int.Parse(Interaction.InputBox(
                    $"Enter y coordinate max height is {_settings.screenHeight}",
                    "InputWindow", ""));

                if (newY > _settings.screenHeight || newY < 0) throw new ArgumentException();

                _newSettings.SetProfileY(newY);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error, y is out of the screen height");
                return;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
                return;
            }
            LocationLab.Text = newX + ", " + newY;
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
            _newSettings.SetAskToDelete(_newSettings.DoesAskToDelete() ? false : true);
            AskToDeleteState.Text = _newSettings.DoesAskToDelete() ? "On" : "OFF";
            AskToDeleteState.Location.ChangeXPos(_newSettings.DoesAskToDelete() ? 14 : -14);
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
            _newSettings.SetDoesNotificate(_newSettings.DoesNotificate() ? false : true);
            NotificationState.Text = _newSettings.DoesNotificate() ? "On" : "OFF";
            NotificationState.Location.ChangeXPos(_newSettings.DoesNotificate() ? 14 : -14);
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
        private void DayDistanceToNotificate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string distanceNotFormated = Interaction.InputBox(
                    "Enter how many days far from the deadline you want to be notified",
                    "Day distance to notificate input box",
                    _settings.GetNotificationDayDistance().ToString());
                if (string.IsNullOrEmpty(distanceNotFormated)) return;
                uint distanceToNotificate = uint.Parse(distanceNotFormated);

                _newSettings.SetNotificationDayDistance(distanceToNotificate);
                DistanceToNotificate.Text = distanceToNotificate.ToString();
            }
            catch
            {
                MessageBox.Show("Error, wrong input type");
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select folder to export config into");
            ExportSettingsFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(ExportSettingsFileDialog.SelectedPath)) return;
            string destSettingsFilePath = ExportSettingsFileDialog.SelectedPath + "\\settings.json";
            if (File.Exists(destSettingsFilePath))
            {
                DialogResult rewriteOrCancel = MessageBox
                    .Show("File with this name already exists there, rewrite it?"
                    , "Rewrite or cancel"
                    , MessageBoxButtons.YesNo);
                if (rewriteOrCancel == DialogResult.No) return;
                try
                {
                    File.Delete(destSettingsFilePath);
                }
                catch (IOException)
                {
                    MessageBox.Show("Error, the file is in use");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Error, the acces denied");
                }
            }
            try
            {
                File.Copy(Settings.settingsFileFullPath, destSettingsFilePath);
            }
            catch (IOException)
            {
                MessageBox.Show("Error, the file is in use");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Error, the access denied");
            }

            MessageBox.Show("Settings config was exported succesfully");
        }

        private void ThemeBoxCB_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void CurrentFontComboBox_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        public void OnAddition()
        {
            LoadSettings();
        }

        private void HourDistanceToNotificate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string distanceNotFormated = Interaction.InputBox(
                    "Enter how many days far from the deadline you want to be notified",
                    "Day distance to notificate input box",
                    _settings.GetNotificationHourDistance().ToString());
                if (string.IsNullOrEmpty(distanceNotFormated)) return;
                uint distanceToNotificate = uint.Parse(distanceNotFormated);

                _newSettings.SetNotificationHourDistance(distanceToNotificate);
                HourDistanceToNotificate.Text = distanceToNotificate.ToString();
            }
            catch
            {
                MessageBox.Show("Error, wrong input type");
            }
        }

        private void HourDistanceToNotificate_MouseEnter(object sender, EventArgs e)
        {
            HourDistanceToNotificate.ForeColor = Color.Gray;
        }

        private void HourDistanceToNotificate_MouseLeave(object sender, EventArgs e)
        {
            HourDistanceToNotificate.ForeColor = Color.White;
        }

        private void CanBeAlwaysReplacable_MouseDown(object sender, MouseEventArgs e)
        {
            _newSettings.SetCanBeAlwaysReplaced(_newSettings.CanBeAlwaysReplaced() ? false : true);
            CanBeAlwaysReplacable.Text = _newSettings.CanBeAlwaysReplaced() ? "On" : "OFF";
            CanBeAlwaysReplacable.Location.ChangeXPos(_newSettings.CanBeAlwaysReplaced() ? 14 : -14);
        }
        private void CanBeAlwaysReplacable_MouseEnter(object sender, EventArgs e)
        {
            CanBeAlwaysReplacable.ForeColor = Color.Gray;
        }

        private void CanBeAlwaysReplacable_MouseLeave(object sender, EventArgs e)
        {
            CanBeAlwaysReplacable.ForeColor = Color.White;
        }

        public bool OnRemoving(bool closing = false)
        {
            return CanLeave();
        }
    }
}
