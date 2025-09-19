namespace AffairList
{
    public partial class HotkeySettingsManager : UserControl, IChildable, IKeyPreviewable
    {
        private bool _isConfirmed = true;
        private Keys _inputKey;

        private Settings _settings;

        public IParentable ParentElement { get; private set; }
        public KeyEventHandler KeyDownHandlers { get; private set; }
        public KeyPressEventHandler KeyPressHandlers { get; private set; }
        public KeyEventHandler KeyUpHandlers { get; private set; }

        private Action _hotkeysUpdater;

        public HotkeySettingsManager(Settings settings)
        {
            InitializeComponent();
            LoadSettings();
            KeyDownHandlers += HotkeySettingsManager_KeyDown;
            _settings = settings;
        }
        private void LoadSettings()
        {
            CloseKeyType.Text = _settings.GetCloseKey().ToString();
            BackKeyType.Text = _settings.GetReturnKey().ToString();
        }
        private void HotkeySettingsManager_KeyDown(object sender, KeyEventArgs e)
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
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            _hotkeysUpdater?.Invoke();
            await _settings.SaveSettingsAsync();
            _isConfirmed = true;
        }
        private void CloseKeyType_DoubleClick(object sender, EventArgs e)
        {
            _inputKey = SetKey();
            if (_inputKey == Keys.Escape) return;

            _hotkeysUpdater -= SetCloseKey;
            _hotkeysUpdater += SetCloseKey;
            CloseKeyType.Text = _inputKey.ToString();
            _isConfirmed = false;
        }

        private void SetCloseKey()
        {
            _settings.SetCloseKey(_inputKey);
        }

        private void SetReturnKey()
        {
            _settings.SetCloseKey(_inputKey);
        }

        private void BackKeyType_DoubleClick(object sender, EventArgs e)
        {
            _inputKey = SetKey();
            if (_inputKey == Keys.Escape) return;

            _hotkeysUpdater -= SetReturnKey;
            _hotkeysUpdater += SetReturnKey;
            BackKeyType.Text = _inputKey.ToString();
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
        private void MinimizeButton_MouseUp(object sender, MouseEventArgs e)
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
    }
}
