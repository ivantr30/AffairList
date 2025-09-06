using Gma.System.MouseKeyHook;

namespace AffairList
{
    public partial class ToDoList : Form
    {
        private IKeyboardMouseEvents _globalHook;

        public bool canReplace { get; set; } = false;

        private string _priorityTag = "<priority>";
        private string _deadlineTag = "<deadline>";
        public IParentable ParentElement { get; set; }
        private Settings _settings;

        public ToDoList(Settings settings, IParentable parent)
        {
            InitializeComponent();
            Task.WhenAll(SetLocation(), LoadText());

            SubscribeGlobalHook();
            ParentElement = parent;
            _settings = settings;
        }
        private async Task SetLocation()
        {
            TopMost = true;

            await LoadSettings();

            Width = _settings.width;
            Height = _settings.height + _settings.height / 10;

            Affairs.AutoSize = false;
            Affairs.Size = new Size(500, Height);
            Affairs.Padding = new Padding(0, 0, 180, 0);

            Affairs.ForeColor = _settings.GetTextColor();

            BackColor = _settings.GetBgColor();
            TransparencyKey = _settings.GetBgColor();
        }
        private async Task LoadSettings()
        {
            if (!_settings.SettingsFileExists()) await _settings.CreateSettingsFile();
            Affairs.Left = _settings.GetProfileX();
            Affairs.Top = _settings.GetProfileY();
        }
        private async Task LoadText()
        {
            if (_settings.CurrentListNotNull())
            {
                string[] text = await File.ReadAllLinesAsync(_settings.GetCurrentProfile());
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i].EndsWith(_priorityTag))
                    {
                        text[i] = text[i].Substring(0, text[i].Length - _priorityTag.Length)
                            .Trim();
                    }
                    if (text[i].StartsWith(_deadlineTag))
                    {
                        text[i] = text[i].Substring(_deadlineTag.Length);
                    }
                }
                Affairs.Text = string.Join("\n", text);
            }
            else
            {
                MessageBox.Show("Ошибка, не найден текущий список дел");
                Close();
                return;
            }
            Affairs.Text += "\nЭто весь список ваших дел";
        }
        private void SubscribeGlobalHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHook_KeyDown!;
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _settings.GetCloseKey())
            {
                ParentElement.Exit();
            }
            if (e.KeyCode == _settings.GetReturnKey())
            {
                Close();
            }
        }

        private void Affairs_MouseDown(object sender, MouseEventArgs e) => OnListMouseDown(e);
        private void Affairs_MouseMove(object sender, MouseEventArgs e) => OnListMouseMove(e);
        private void Affairs_MouseUp(object sender, MouseEventArgs e) => OnListMouseUp(e);
        private void List_MouseDown(object sender, MouseEventArgs e) => OnListMouseDown(e);
        private void List_MouseMove(object sender, MouseEventArgs e) => OnListMouseMove(e);

        private void List_MouseUp(object sender, MouseEventArgs e) => OnListMouseUp(e);
        private void OnListMouseDown(MouseEventArgs e)
        {
            if(canReplace) ParentElement.SetLastPoint(e);
        }
        private void OnListMouseMove(MouseEventArgs e)
        {
            if (canReplace) ParentElement.MoveForm(e);
        }
        private async void OnListMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                canReplace = false;
                _settings.SetProfileX(Left + Affairs.Left);
                _settings.SetProfileY(Top + Affairs.Top);
                await _settings.SaveSettings();
                Close();
            }
        }
        public Label GetAffairs()
        {
            return Affairs;
        }
    }
}
