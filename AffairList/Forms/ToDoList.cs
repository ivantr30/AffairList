using Gma.System.MouseKeyHook;
using System.Text;

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
            ParentElement = parent;
            _settings = settings;
            Task settingLocation = SetLocationAsync();
            Task loadingText = LoadTextAsync();

            SubscribeGlobalHook();
            Task.WhenAll(settingLocation, loadingText);
        }
        private async Task SetLocationAsync()
        {
            TopMost = true;

            await LoadSettingsAsync();

            Width = _settings.screenWidth;
            Height = _settings.screenHeight + _settings.screenHeight / 10;

            Affairs.AutoSize = false;
            Affairs.Size = new Size(500, Height);
            Affairs.Padding = new Padding(0, 0, 180, 0);

            Affairs.ForeColor = _settings.GetTextColor();

            BackColor = _settings.GetBgColor();
            TransparencyKey = _settings.GetBgColor();
        }
        private async Task LoadSettingsAsync()
        {
            if (!_settings.SettingsFileExists()) await _settings.CreateSettingsFileAsync();
            Affairs.Left = _settings.GetProfileX();
            Affairs.Top = _settings.GetProfileY();
        }
        private async Task LoadTextAsync()
        {
            if (_settings.CurrentListNotNull())
            {
                StringBuilder affairsShower = new StringBuilder();
                string currentAffair = "";
                await foreach (string affair in File.ReadLinesAsync(_settings.GetCurrentProfile()))
                {
                    currentAffair = affair;
                    if (currentAffair.EndsWith(_priorityTag))
                    {
                        currentAffair = currentAffair
                            .Substring(0, currentAffair.Length - _priorityTag.Length);
                    }
                    if (currentAffair.StartsWith(_deadlineTag))
                    {
                        currentAffair = currentAffair.Remove(0, _deadlineTag.Length);
                    }
                    affairsShower.AppendLine(affair.Trim());
                }
                Affairs.Text += affairsShower.ToString() + "Это весь список ваших дел";
            }
            else
            {
                MessageBox.Show("Ошибка, не найден текущий список дел");
                Close();
                return;
            }
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
        private void Affairs_MouseUp(object sender, MouseEventArgs e) => OnListMouseUpAsync(e);
        private void List_MouseDown(object sender, MouseEventArgs e) => OnListMouseDown(e);
        private void List_MouseMove(object sender, MouseEventArgs e) => OnListMouseMove(e);

        private void List_MouseUp(object sender, MouseEventArgs e) => OnListMouseUpAsync(e);
        private void OnListMouseDown(MouseEventArgs e)
        {
            if(canReplace) ParentElement.SetLastPoint(e);
        }
        private void OnListMouseMove(MouseEventArgs e)
        {
            if (canReplace) ParentElement.MoveForm(e);
        }
        private async void OnListMouseUpAsync(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                canReplace = false;
                _settings.SetProfileX(Left + Affairs.Left);
                _settings.SetProfileY(Top + Affairs.Top);
                await _settings.SaveSettingsAsync();
                Close();
            }
        }
    }
}
