using Microsoft.VisualBasic;
using AffairList.Services.Managers;

namespace AffairList
{
    public partial class SettingsManager : UserControl, IChildable
    {
        private Settings _settings;

        public IParentable ParentElement { get; private set; }

        private Settings _newSettings;

        /// <summary>
        /// an x position for all togglable settings
        /// </summary>
        private readonly int _globalTogglerX;
        public SettingsManager(Settings settings, IParentable parentElement)
        {
            InitializeComponent();
            _settings = settings;
            ParentElement = parentElement;
            _globalTogglerX = autostartStateLab.Left;
            _newSettings = new Settings(false);
        }
        private void LoadSettings()
        {
            LocationLab.Text = _settings.Data.TodoListX + ", " + _settings.Data.ToDoListY;
            ListTextColorLab.ForeColor = _settings.Data.TextColor;
            ListBgTextColorLab.ForeColor = _settings.Data.BgColor;
            if (_settings.Data.AutostartState)
            {
                autostartStateLab.Text = "On";
                autostartStateLab.Left = _globalTogglerX;
            }
            else
            {
                autostartStateLab.Text = "OFF";
                autostartStateLab.Left = _globalTogglerX - 8;
            }
            if (_settings.Data.AskToDelete)
            {
                AskToDeleteState.Text = "On";
                AskToDeleteState.Left = _globalTogglerX;
            }
            else
            {
                AskToDeleteState.Text = "OFF";
                AskToDeleteState.Left = _globalTogglerX - 8;
            }
            if (_settings.Data.DoesNotificate)
            {
                NotificationState.Text = "On";
                NotificationState.Left = _globalTogglerX;
            }
            else
            {
                NotificationState.Text = "OFF";
            }
            if (_settings.Data.CanBeAlwaysReplaced)
            {
                CanBeAlwaysReplacable.Text = "On";
                CanBeAlwaysReplacable.Left = _globalTogglerX;
            }
            else
            {
                CanBeAlwaysReplacable.Text = "OFF";
                CanBeAlwaysReplacable.Left = _globalTogglerX - 8;
            }
            HourDistanceToNotificate.Text = _settings.Data.NotificationHourDistance.ToString();
            DistanceToNotificate.Text = _settings.Data.NotificationDayDistance.ToString();
        }
        private bool CanLeave()
        {
            if (!_settings.SettingsEqual(_newSettings.Data))
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
            ParentElement.ReturnAsync();
        }

        private async void ResetButton_Click(object sender, EventArgs e)
        {
            SettingsModel defaultSettings = new SettingsModel();

            string currentProfile = _settings.Data.CurrentProfileFullPath;
            defaultSettings.CurrentProfileFullPath = currentProfile;

            if (!_newSettings.SettingsEqual(_settings.Data))
            {
                DialogResult restorationResult = MessageBox.Show(
                    "Do you really want to restore last-confirmed settings?",
                    "Confirm window",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (restorationResult == DialogResult.Yes)
                {
                    _newSettings.Data = _settings.GetSettingsCopy();
                    LoadSettings();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "Do you really want to set settings to default?",
                    "Confirm window",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    await _settings.WriteBaseSettingsAsync();

                    _settings.Data.CurrentProfileFullPath = currentProfile;

                    _newSettings.Data = _settings.GetSettingsCopy();

                    LoadSettings();
                }
            }
            defaultSettings = null;
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            _settings.SetNewSettings(_newSettings.Data);
            await _settings.SaveSettingsAsync();
        }

        private void autostartStateLab_MouseDown(object sender, MouseEventArgs e)
        {
            _newSettings.Data.AutostartState = _newSettings.Data.AutostartState ? false : true;
            autostartStateLab.Text = _newSettings.Data.AutostartState ? "On" : "OFF";
            autostartStateLab.Left = _newSettings.Data.AutostartState ? _globalTogglerX : _globalTogglerX - 8;
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
            _newSettings.Data.TextColor = newColor;
        }
        private void PickBgColorButton_Click(object sender, EventArgs e)
        {
            Color newColor = ListColorChanger();
            if (newColor == Color.Empty) return;
            ListBgTextColorLab.ForeColor = newColor;
            _newSettings.Data.BgColor = newColor;
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
                _newSettings.Data.TodoListX = newX;
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

                _newSettings.Data.ToDoListY = newY;
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
            _newSettings.Data.AskToDelete = _newSettings.Data.AskToDelete ? false : true;
            AskToDeleteState.Text = _newSettings.Data.AskToDelete ? "On" : "OFF";
            AskToDeleteState.Left = _newSettings.Data.AskToDelete ? _globalTogglerX : _globalTogglerX - 8;
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
            _newSettings.Data.DoesNotificate = _newSettings.Data.DoesNotificate ? false : true;
            NotificationState.Text = _newSettings.Data.DoesNotificate ? "On" : "OFF";
            NotificationState.Left = _newSettings.Data.DoesNotificate ? _globalTogglerX : _globalTogglerX - 8;
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
                    _settings.Data.NotificationDayDistance.ToString());
                if (string.IsNullOrEmpty(distanceNotFormated)) return;
                uint distanceToNotificate = uint.Parse(distanceNotFormated);

                _newSettings.Data.NotificationDayDistance = distanceToNotificate;
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

            bool _operationSuceed = false;

            var res = ExportSettingsFileDialog.ShowDialog();

            if (string.IsNullOrEmpty(ExportSettingsFileDialog.SelectedPath) || res == DialogResult.Cancel) return;

            string destSettingsFilePath = Path.Combine(ExportSettingsFileDialog.SelectedPath, "settings.json");
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
                _operationSuceed = true;
            }
            catch (IOException)
            {
                MessageBox.Show($"Error, couldn't find the file or it is in use");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Error, the access denied");
            }

            if(_operationSuceed) MessageBox.Show("Settings config was exported succesfully");
        }

        private void ThemeBoxCB_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void CurrentFontComboBox_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        public async Task OnAdditionAsync()
        {
            SuspendLayout();
            LoadSettings();
            _newSettings.Data = _settings.GetSettingsCopy();
            ResumeLayout();
        }

        private void HourDistanceToNotificate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string distanceNotFormated = Interaction.InputBox(
                    "Enter how many days far from the deadline you want to be notified",
                    "Day distance to notificate input box",
                    _settings.Data.NotificationHourDistance.ToString());
                if (string.IsNullOrEmpty(distanceNotFormated)) return;
                uint distanceToNotificate = uint.Parse(distanceNotFormated);

                _newSettings.Data.NotificationHourDistance = distanceToNotificate;
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
            _newSettings.Data.CanBeAlwaysReplaced = _newSettings.Data.CanBeAlwaysReplaced ? false : true;
            CanBeAlwaysReplacable.Text = _newSettings.Data.CanBeAlwaysReplaced ? "On" : "OFF";
            CanBeAlwaysReplacable.Left = _newSettings.Data.CanBeAlwaysReplaced ? _globalTogglerX : _globalTogglerX - 8;
        }
        private void CanBeAlwaysReplacable_MouseEnter(object sender, EventArgs e)
        {
            CanBeAlwaysReplacable.ForeColor = Color.Gray;
        }

        private void CanBeAlwaysReplacable_MouseLeave(object sender, EventArgs e)
        {
            CanBeAlwaysReplacable.ForeColor = Color.White;
        }

        public async Task<bool> OnRemovingAsync(bool closing = false)
        {
            if (CanLeave())
            {
                return true;
            }
            else return false;
        }
    }
}
