namespace AffairList
{
    public partial class MainMenu : UserControl, IChildable
    {
        private Settings _settings;
        private LoadTimeManager _loadTimeManager;
        public IParentable ParentElement { get; private set; }

        private Control _affairsManager;
        private Control _profileManager;
        private Control _settingsManager;
        private Control _hotkeySettingsManager;

        public MainMenu(Settings settings, LoadTimeManager loadTimeManager, IParentable parent)
        {
            InitializeComponent();

            _settings = settings;
            _loadTimeManager = loadTimeManager;
            ParentElement = parent;
            if (AffairListDebug.DEBUG)
            {
                ErrorHelpLab.Text = "DEBUG MOD ON";
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            ParentElement.Exit();
        }

        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
            => ParentElement.SetLastPoint(e);

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);

        private void AffairListLab_MouseDown(object sender, MouseEventArgs e)
            => ParentElement.SetLastPoint(e);

        private void AffairListLab_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);

        private void MinimizeButton_Click(object sender, EventArgs e)
            => ParentElement.MinimizeForm();

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
            => MinimizeButton.ForeColor = (System.Drawing.Color)Color.Gray;

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
            => MinimizeButton.ForeColor = (System.Drawing.Color)Color.Black;

        private void CloseButton_MouseEnter(object sender, EventArgs e)
            => CloseButton.ForeColor = (System.Drawing.Color)Color.Gray;

        private void CloseButton_MouseLeave(object sender, EventArgs e)
            => CloseButton.ForeColor = (System.Drawing.Color)Color.Black;

        private void OpenListButton_Click(object sender, EventArgs e)
        {
            if (!_settings.CurrentListNotNull())
            {
                MessageBox.Show("Error, there is no list available");
                return;
            }
            ParentElement.OpenForm(new ToDoList(_settings, ParentElement), asDialog: false);
        }
        private void ReplaceAffairListButton_Click(object sender, EventArgs e)
        {
            if (!_settings.CurrentListNotNull())
            {
                MessageBox.Show("Error, there is no list available");
                return;
            }
            using ToDoList toDoList = new ToDoList(_settings, ParentElement) { canReplace = true };
            toDoList.GetAffairs().BackColor = (System.Drawing.Color)Color.White;
            ParentElement.OpenForm(toDoList, asDialog: true);
        }

        private async void ClearListButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure to clear your affair list?",
            "Confirm window",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (!_settings.CurrentListNotNull())
                {
                    MessageBox.Show("Error, list file does not exist");
                    return;
                }
                await File.WriteAllTextAsync(_settings.GetCurrentProfile(), "");
                MessageBox.Show("The list is cleared");
            }
        }

        private async void ChangeListButton_Click(object sender, EventArgs e)
        {
            if (!_settings.ListFilesAvailable())
            {
                DialogResult createDefault = MessageBox.Show("There are no profiles available," +
                    " do you want to add default?",
                    "Confirm Form",
                    MessageBoxButtons.YesNo);
                if (createDefault == DialogResult.No) return;

                await _settings.CreateDefaultListAsync();
            }
            if (_affairsManager == null)
            {
                _affairsManager = new AffairsManager(_settings, ParentElement);
            }
            ParentElement.SetControl(_affairsManager);
        }
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (_settingsManager == null)
            {
                _settingsManager = new SettingsManager(_settings, ParentElement);
            }
            ParentElement.SetControl(_settingsManager);
        }
        private void ChangeProfileButton_Click(object sender, EventArgs e)
        {
            if (_profileManager == null)
            {
                _profileManager = new ProfileManager(_settings, ParentElement);
            }
            ParentElement.SetControl(_profileManager);
        }
        private void HotKeyButton_Click(object sender, EventArgs e)
        {
            if (_hotkeySettingsManager == null)
            {
                _hotkeySettingsManager = new HotKeySettingsManager(_settings, ParentElement);
            }
            ParentElement.SetControl(_hotkeySettingsManager);
        }
        public void OnAddition()
        {
            _loadTimeManager.Notificate();
            _loadTimeManager.SaveTime();
        }

        private async void MainMenu_Load(object sender, EventArgs e)
        {
        }
    }
}
