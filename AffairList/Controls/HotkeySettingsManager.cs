namespace AffairList
{
    public partial class HotKeySettingsManager : UserControl, IChildable
    {
        private bool _isConfirmed = true;
        private Keys _newCloseKey;
        private Keys _newReturnKey;

        private Settings _settings;

        public IParentable ParentElement { get; private set; }

        private Action _hotkeysUpdater;

        public HotKeySettingsManager(Settings settings, IParentable parent)
        {
            InitializeComponent();
            _settings = settings;
            ParentElement = parent;
        }
        private void LoadSettings()
        {
            CloseKeyType.Text = _settings.GetCloseKey().ToString();
            BackKeyType.Text = _settings.GetReturnKey().ToString();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (!_isConfirmed)
            {
                DialogResult saveOrNot = MessageBox.Show("Are you sure to leave with unsaved settings?",
                "Confirm form",
                MessageBoxButtons.YesNo);
                if (saveOrNot == DialogResult.No) return;
            }
            ParentElement.Return();
        }

        private void CloseButton_Click(object sender, EventArgs e) => ParentElement.Exit();

        private void MinimizeButton_Click(object sender, EventArgs e) => ParentElement.MinimizeForm();

        private async void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult resetOrNot = MessageBox.Show("Are you sure to reset hotkey settings?",
                "Confirm form",
                MessageBoxButtons.YesNo);
            if (resetOrNot == DialogResult.No) return;

            _settings.SetCloseKey(Keys.F7);
            _settings.SetReturnKey(Keys.F6);
            await _settings.SaveSettingsAsync();

            CloseKeyType.Text = "F7";
            BackKeyType.Text = "F6";
            _isConfirmed = true;
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            _hotkeysUpdater?.GetInvocationList();
            _hotkeysUpdater?.Invoke();
            await _settings.SaveSettingsAsync();
            _isConfirmed = true;
        }
        private void CloseKeyType_DoubleClick(object sender, EventArgs e)
        {
            _newCloseKey = SetKey();
            if (_newCloseKey == Keys.Escape) return;

            _hotkeysUpdater -= SetCloseKey;
            _hotkeysUpdater += SetCloseKey;
            CloseKeyType.Text = _newCloseKey.ToString();
            _isConfirmed = false;
        }

        private void SetCloseKey()
        {
            _settings.SetCloseKey(_newCloseKey);
        }

        private void SetReturnKey()
        {
            _settings.SetReturnKey(_newReturnKey);
        }

        private void BackKeyType_DoubleClick(object sender, EventArgs e)
        {
            _newReturnKey = SetKey();
            if (_newReturnKey == Keys.Escape) return;

            _hotkeysUpdater -= SetReturnKey;
            _hotkeysUpdater += SetReturnKey;
            BackKeyType.Text = _newReturnKey.ToString();
            _isConfirmed = false;
        }
        private Keys SetKey()
        {
            Keys key = Keys.None;
            InputKeyForm keyForm = new InputKeyForm();
            keyForm.OnKeyPressed += delegate
            {
                key = keyForm.Key;
            };
            keyForm.ShowDialog();
            return key;
        }

        private void CloseKeyType_MouseEnter(object sender, EventArgs e)
        {
            CloseKeyType.ForeColor = Color.Gray;
        }

        private void BackKeyType_MouseEnter(object sender, EventArgs e)
        {
            BackKeyType.ForeColor = Color.Gray;
        }

        private void BackKeyType_MouseLeave(object sender, EventArgs e)
        {
            BackKeyType.ForeColor = Color.White;
        }

        private void CloseKeyType_MouseLeave(object sender, EventArgs e)
        {
            CloseKeyType.ForeColor = Color.White;
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);
        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
            => ParentElement.SetLastPoint(e);
        private void HotKeySettingsLab_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);
        private void HotKeySettingsLab_MouseDown(object sender, MouseEventArgs e)
            => ParentElement.SetLastPoint(e);

        public void OnAddition()
        {
            LoadSettings();
        }
    }
}
