using System.ComponentModel;
using System.Text;

using AffairList.Infrastructure.Settings;

using Gma.System.MouseKeyHook;

using Microsoft.Win32;

namespace AffairList
{
    public partial class ToDoList : Form
    {
        private IKeyboardMouseEvents _globalHook = null!;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanReplace { get; set; }

        private const string _priorityTag = "<priority>";
        private const string _priorityWord = " Priority";
        private const string _deadlineTag = "<deadline>";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParentable ParentElement { get; set; }
        private readonly Settings _settings;

        public ToDoList(Settings settings, IParentable parent)
        {
            InitializeComponent();
            ParentElement = parent;
            _settings = settings;
            Task settingLocation = SetLocationAsync();
            Task loadingText = LoadTextAsync();

            SubscribeGlobalHook();
            Task.WhenAll(settingLocation, loadingText);
            SpecifyAffairsLocation();
        }

        private async Task SetLocationAsync()
        {
            TopMost = true;

            await LoadSettingsAsync();

            Width = _settings.screenWidth;
            Height = _settings.screenHeight + _settings.screenHeight / 10;

            Affairs.AutoSize = false;
            Affairs.Size = new Size(500, Height);
            Affairs.MaximumSize = new Size(500, 0);
            Affairs.MinimumSize = new Size(500, 0);
            Affairs.Padding = new Padding(0, 0, 180, 0);
            Affairs.AutoSize = true;

            Affairs.ForeColor = _settings.GetTextColor();

            BackColor = _settings.GetBgColor();
            TransparencyKey = _settings.GetBgColor();
            SystemEvents.PowerModeChanged += OnPowerModeChanged;
        }

        private async Task LoadSettingsAsync()
        {
            if (!Settings.SettingsFileExists()) await _settings.CreateSettingsFileAsync();
            Affairs.Left = _settings.GetProfileX();
            Affairs.Top = _settings.GetProfileY();
        }

        private async Task LoadTextAsync()
        {

            Affairs.Text = "";
            if (_settings.CurrentListNotNull())
            {
                StringBuilder affairsShower = new StringBuilder();
                affairsShower.Append("* ");
                affairsShower.AppendJoin("\n* ", await File.ReadAllLinesAsync(_settings.GetCurrentProfile()));
                affairsShower.Replace(_priorityTag, _priorityWord);
                affairsShower.Replace(_deadlineTag, "");
                affairsShower.Replace("\n", ".\n");
                Affairs.Text += affairsShower.ToString() + "\nЭто весь список ваших дел";
                affairsShower.Clear();
            }
            else
            {
                MessageBox.Show("Ошибка, не найден текущий список дел");
                Close();
                return;
            }
        }

        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume)
                UpdateLocation();
        }

        private void SubscribeGlobalHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHook_KeyDown!;
        }

        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _settings.GetCloseKey())
                ParentElement.Exit();
            if (e.KeyCode == _settings.GetReturnKey())
                Close();
        }

        private void Affairs_MouseDown(object sender, MouseEventArgs e) => OnListMouseDown(e);
        private void Affairs_MouseMove(object sender, MouseEventArgs e) => OnListMouseMove(e);
        private async void Affairs_MouseUp(object sender, MouseEventArgs e) => await OnListMouseUpAsync(e);
        private void List_MouseDown(object sender, MouseEventArgs e) => OnListMouseDown(e);
        private void List_MouseMove(object sender, MouseEventArgs e) => OnListMouseMove(e);

        private async void List_MouseUp(object sender, MouseEventArgs e) => await OnListMouseUpAsync(e);

        private void OnListMouseDown(MouseEventArgs e)
        {
            if (CanReplace || _settings.GetCanBeAlwaysReplaced()) ParentElement.SetLastPoint(e);
        }

        private void OnListMouseMove(MouseEventArgs e)
        {
            if ((CanReplace || _settings.GetCanBeAlwaysReplaced()) && e.Button == MouseButtons.Left)
            {
                ParentElement.MoveChildForm(e);
                SpecifyListLocation();
                TopMost = true;
            }
        }

        private void SpecifyAffairsLocation()
        {
            if (Affairs.Left + Affairs.Width - 310 >= _settings.screenWidth)
                Affairs.Left = _settings.screenWidth - Affairs.Width - Affairs.Width / 4 + 315;
            if (Affairs.Top + Affairs.Height >= _settings.screenHeight)
                Affairs.Top = _settings.screenHeight - Affairs.Height * 5 - Affairs.Height / 2;

            _settings.SetProfileX(Left + Affairs.Left);
            _settings.SetProfileY(Top + Affairs.Top);
            _settings.SaveSettingsAsync().Wait();
        }

        private void SpecifyListLocation()
        {
            if (Left + Affairs.Left + 310 >= _settings.screenWidth)
                Left = _settings.screenWidth - 310 - Affairs.Left;
            else if (Affairs.Left + Left <= 0)
                Left = -Affairs.Left;

            if (Top + Affairs.Top + Affairs.Height - 40 >= _settings.screenHeight)
                Top = _settings.screenHeight - Affairs.Top - Affairs.Height + 40;
            else if (Affairs.Top + Top <= 0)
                Top = -Affairs.Top;
        }

        private async Task OnListMouseUpAsync(MouseEventArgs e)
        {
            _settings.SetProfileX(Left + Affairs.Left);
            _settings.SetProfileY(Top + Affairs.Top);
            await _settings.SaveSettingsAsync();
            TopMost = true;
            if (CanReplace)
            {
                Close();
                CanReplace = false;
                return;
            }
        }

        public Label GetAffairs() => Affairs;

        private void ToDoList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Affairs.BackColor = _settings.GetBgColor();
            CanReplace = false;
            SystemEvents.PowerModeChanged -= OnPowerModeChanged;
        }

        private void UpdateLocation()
        {
            Affairs.Left = Affairs.Left;
            Affairs.Top = Affairs.Top;
        }

        private void ToDoList_Resize(object sender, EventArgs e)
            => WindowState = FormWindowState.Normal;
    }
}
