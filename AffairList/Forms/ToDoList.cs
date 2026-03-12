using Gma.System.MouseKeyHook;
using Microsoft.Win32;
using System.Text;

namespace AffairList
{
    public partial class ToDoList : Form
    {
        private IKeyboardMouseEvents _globalHook;

        public bool CanReplace { get; set; }

        private string _priorityTag = "<priority>";
        private string _priorityWord = " Priority";
        private string _deadlineTag = "<deadline>";
        public IParentable ParentElement { get; set; }
        private Settings _settings;

        Point _dragOffset;

        public ToDoList(Settings settings, IParentable parent, Color backColor = default)
        {
            InitializeComponent();
            ParentElement = parent;
            _settings = settings;
            
            if (backColor != default)
                Affairs.BackColor = backColor;
            this.Load += ToDoList_Load;
        }

        private async void ToDoList_Load(object? sender, EventArgs e)
        {
            await LoadSettingsAsync();
            await LoadTextAsync();
            SetLocation();

            SpecifyAffairsLocation();
            SaveAffairsLocation();
            SubscribeGlobalHook();
        }

        private void SetLocation()
        {
            TopMost = true;

            Width = _settings.screenWidth;
            Height = _settings.screenHeight;

            Affairs.AutoSize = false;
            Affairs.Size = new Size(500, 0);
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
            if (!_settings.SettingsFileExists()) await _settings.CreateSettingsFileAsync();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
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
        /// <summary>
        /// Если экран был выключен и включен, а список был в углу, этот метод восстанавливает 
        /// исходное положение списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume)
            {
                UpdateLocation();
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
        private async void Affairs_MouseUp(object sender, MouseEventArgs e)
            => await OnListMouseUpAsync(e);
        private void List_MouseDown(object sender, MouseEventArgs e) => OnListMouseDown(e);
        private void List_MouseMove(object sender, MouseEventArgs e) => OnListMouseMove(e);

        private async void List_MouseUp(object sender, MouseEventArgs e)
            => await OnListMouseUpAsync(e);
        private void OnListMouseDown(MouseEventArgs e)
        {
            if (CanReplace || _settings.CanBeAlwaysReplaced())
                _dragOffset = new Point(Cursor.Position.X - Affairs.Left, Cursor.Position.Y - Affairs.Top);
        }
        private void OnListMouseMove(MouseEventArgs e)
        {
            if ((CanReplace || _settings.CanBeAlwaysReplaced()) && e.Button == MouseButtons.Left)
            {
                int newLeft = Cursor.Position.X - _dragOffset.X;
                int newTop = Cursor.Position.Y - _dragOffset.Y;

                newLeft = Math.Clamp(newLeft, 0, _settings.screenWidth - Affairs.Width + Affairs.Padding.Right);
                newTop = Math.Clamp(newTop, 0, _settings.screenHeight - Affairs.Height);

                Affairs.Left = newLeft;
                Affairs.Top = newTop;

                SaveAffairsLocation();
                TopMost = true;
            }
        }
        // УБРАТЬ SETTINGSMODEL, раскидать логику на разные классы.
        // Убрать логику SystemEvents_SessionSwitch в program.cs, поменять на булевую переменную с событием
        private void SpecifyAffairsLocation()
        {
            if (Affairs.Left > this.Width - Affairs.Width + Affairs.Padding.Right)
            {
                Affairs.Left = this.Width - Affairs.Width + Affairs.Padding.Right;
            }
            if (Affairs.Top > this.Height - Affairs.Height)
            {
                Affairs.Top = this.Height - Affairs.Height;
            }
            if (Affairs.Left < 0)
            {
                Affairs.Left = 0;
            }
            if (Affairs.Top < 0)
            {
                Affairs.Top = 0;
            }
        }
        private void SaveAffairsLocation()
        {
            _settings.SetProfileX(Affairs.Left);
            _settings.SetProfileY(Affairs.Top);
            _settings.SaveSettings();
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
        {
            WindowState = FormWindowState.Normal;
        }
    }
}
