namespace AffairList
{
    public partial class MainMenu : UserControl, IChildable
    {
        private Settings _settings;
        private LoadTimeManager _loadTimeManager;
        public IParentable ParentElement { get; private set; }

        public MainMenu(Settings settings, LoadTimeManager loadTimeManager, IParentable parent)
        {
            InitializeComponent();

            _settings = settings;
            _loadTimeManager = loadTimeManager;
            ParentElement = parent;
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
            => MinimizeButton.ForeColor = Color.Gray;

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
            => MinimizeButton.ForeColor = Color.Black;

        private void CloseButton_MouseEnter(object sender, EventArgs e)
            => CloseButton.ForeColor = Color.Gray;

        private void CloseButton_MouseLeave(object sender, EventArgs e)
            => CloseButton.ForeColor = Color.Black;

        private void OpenListButton_Click(object sender, EventArgs e)
        {
            if (!_settings.CurrentListNotNull())
            {
                MessageBox.Show("Error, there is no list available");
                return;
            }
            ParentElement.OpenForm(new ToDoList(_settings, ParentElement));
        }
        private void ReplaceAffairListButton_Click(object sender, EventArgs e)
        {
            if (!_settings.CurrentListNotNull())
            {
                MessageBox.Show("Error, there is no list available");
                return;
            }
            ToDoList list = new ToDoList(_settings, ParentElement)
            { BackColor = Color.White, canReplace = true };
            ParentElement.OpenForm(list);
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

        private void ChangeListButton_Click(object sender, EventArgs e)
        {
            if (!_settings.ListFilesAvailable())
            {
                DialogResult createDefault = MessageBox.Show("There are no profiles available," +
                    " do you want to add default?",
                    "Confirm Form",
                    MessageBoxButtons.YesNo);
                if (createDefault == DialogResult.No) return;

                _settings.CreateDefaultListAsync();
            }
            ParentElement.SetControl(new AffairsManager(_settings, ParentElement));
        }
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ParentElement.SetControl(new SettingsManager(_settings, ParentElement));
        }
        private void ChangeProfileButton_Click(object sender, EventArgs e)
        {
            ParentElement.SetControl(new AffairsManager(_settings, ParentElement));
        }
        private void HotKeyButton_Click(object sender, EventArgs e)
        {
            ParentElement.SetControl(new HotKeySettings1(_settings));
        }

        private async void MainMenu_Load(object sender, EventArgs e)
        {
            Task savingLoadTime = _loadTimeManager.SaveTimeAsync();
            _loadTimeManager.Notificate();
            await Task.WhenAll(savingLoadTime);
        }
    }
}
