using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class SettingsManager : UserControl, IChildable
    {
        private bool _isConfirmed = true;

        private Settings _settings;

        private Action _settingsUpdater;

        public IParentable ParentElement { get; private set; }

        private int _newX;
        private int _newY;
        private uint _newDayDistanceToNotificate;
        private uint _newHourDistanceToNotificate;

        private Color _newTextColor;
        private Color _newBgColor;

        private bool _newAutostartState;

        public SettingsManager(Settings settings, IParentable parentElement)
        {
            InitializeComponent();
            _settings = settings;
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
            HourDistanceToNotificate.Text = _settings.GetNotificationHourDistance().ToString();
            DistanceToNotificate.Text = _settings.GetNotificationDayDistance().ToString();
        }
        private bool CanLeave()
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
                    return false;
                }
            }
            return true;
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (CanLeave()) ParentElement.Exit();
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
            if (CanLeave()) ParentElement.Return();
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

                _isConfirmed = true;

                LoadSettings();
            }
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            _isConfirmed = true;
            _settingsUpdater?.Invoke();
            await _settings.SaveSettingsAsync();
        }

        private void autostartStateLab_MouseDown(object sender, MouseEventArgs e)
        {
            if (_settingsUpdater != null &&
                _settingsUpdater.GetInvocationList().Contains(ToggleAutostartState))
            {
                _settingsUpdater -= ToggleAutostartState;
            }
            else
            {
                _settingsUpdater += ToggleAutostartState;
            }
            autostartStateLab.Text = autostartStateLab.Text == "OFF" ? "On" : "OFF";
            _newAutostartState = _newAutostartState ? false : true;
            _isConfirmed = false;
        }

        private void ToggleAutostartState()
        {
            _settings.SetAutostart(!_settings.DoesAutostart());
            if (_newAutostartState) _settings.EnableAutoStart();
            else _settings.DisableAutoStart();
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
            _newTextColor = newColor;
            _settingsUpdater -= SetTextColor;
            _settingsUpdater += SetTextColor;
        }
        private void SetTextColor()
        {
            _settings.SetTextColor(_newTextColor);
        }

        private void PickBgColorButton_Click(object sender, EventArgs e)
        {
            Color newColor = ListColorChanger();
            if (newColor == Color.Empty) return;
            ListBgTextColorLab.ForeColor = newColor;
            _newBgColor = newColor;
            _settingsUpdater -= SetBgColor;
            _settingsUpdater += SetBgColor;
        }
        private void SetBgColor()
        {
            _settings.SetBgColor(_newBgColor);
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
            int newX, newY;
            try
            {
                newX = int.Parse(Interaction.InputBox(
                    $"Enter x coordinate, max width is {_settings.screenWidth}",
                    "InputWindow", ""));

                if (newX > _settings.screenWidth || newX < 0) throw new ArgumentException();

                _newX = newX;

                _settingsUpdater -= SetXPosition;
                _settingsUpdater += SetXPosition;
            }
            catch (ArgumentException ex)
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

                _newY = newY;

                _settingsUpdater -= SetYPosition;
                _settingsUpdater += SetYPosition;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error, y is out of the screen height");
                return;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
                return;
            }
            _isConfirmed = false;
            LocationLab.Text = newX + ", " + newY;
        }

        private void SetXPosition()
        {
            _settings.SetProfileX(_newX);
        }

        private void SetYPosition()
        {
            _settings.SetProfileY(_newY);
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
            if (_settingsUpdater != null &&
                _settingsUpdater.GetInvocationList().Contains(ToggleAskToDeleteState))
            {
                _settingsUpdater -= ToggleAskToDeleteState;
            }
            else
            {
                _settingsUpdater += ToggleAskToDeleteState;
            }
            AskToDeleteState.Text = AskToDeleteState.Text == "OFF" ? "On" : "OFF";
            _isConfirmed = false;
        }

        private void ToggleAskToDeleteState()
        {
            _settings.SetAskToDelete(!_settings.DoesAskToDelete());
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
            if (_settingsUpdater != null &&
                _settingsUpdater.GetInvocationList().Contains(ToggleNotificating))
            {
                _settingsUpdater -= ToggleNotificating;
            }
            else
            {
                _settingsUpdater += ToggleNotificating;
            }
            NotificationState.Text = NotificationState.Text == "OFF" ? "On" : "OFF";
            _isConfirmed = false;
        }

        private void ToggleNotificating()
        {
            _settings.SetDoesNotificate(!_settings.DoesNotificate());
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
                string distanceNotFormated = Interaction.InputBox(
                    "Enter how many days far from the deadline you want to be notified",
                    "Day distance to notificate input box",
                    _settings.GetNotificationDayDistance().ToString());
                if (string.IsNullOrEmpty(distanceNotFormated)) return;
                uint distanceToNotificate = uint.Parse(distanceNotFormated);

                _newDayDistanceToNotificate = distanceToNotificate;
                _settingsUpdater -= SetDistanceToNotificate;
                _settingsUpdater += SetDistanceToNotificate;
                DistanceToNotificate.Text = distanceToNotificate.ToString();
                _isConfirmed = false;
            }
            catch
            {
                MessageBox.Show("Error, wrong input type");
            }
        }

        private void SetDistanceToNotificate()
        {
            _settings.SetNotificationDayDistance(_newDayDistanceToNotificate);
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
                MessageBox.Show("Error, the acces denied");
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

                _newHourDistanceToNotificate = distanceToNotificate;
                _settingsUpdater -= SetHourDistanceToNotificate;
                _settingsUpdater += SetHourDistanceToNotificate;
                HourDistanceToNotificate.Text = distanceToNotificate.ToString();
                _isConfirmed = false;
            }
            catch
            {
                MessageBox.Show("Error, wrong input type");
            }
        }

        private void SetHourDistanceToNotificate()
        {
            _settings.SetNotificationHourDistance(_newHourDistanceToNotificate);
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
            if (_settingsUpdater != null &&
                _settingsUpdater.GetInvocationList().Contains(ToggleCanBeAlwaysReplacableState))
            {
                _settingsUpdater -= ToggleCanBeAlwaysReplacableState;
            }
            else
            {
                _settingsUpdater += ToggleCanBeAlwaysReplacableState;
            }
            CanBeAlwaysReplacable.Text = CanBeAlwaysReplacable.Text == "OFF" ? "On" : "OFF";
            _isConfirmed = false;
        }

        private void ToggleCanBeAlwaysReplacableState()
        {
            _settings.SetAlwaysReplacing(!_settings.CanBeAlwaysReplaced());
        }

        private void CanBeAlwaysReplacable_MouseEnter(object sender, EventArgs e)
        {
            CanBeAlwaysReplacable.ForeColor = Color.Gray;
        }

        private void CanBeAlwaysReplacable_MouseLeave(object sender, EventArgs e)
        {
            CanBeAlwaysReplacable.ForeColor = Color.White;
        }
    }
}
