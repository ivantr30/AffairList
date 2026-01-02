using Gma.System.MouseKeyHook;
using Microsoft.Win32;
using System.Text;

namespace AffairList
{
    public partial class ToDoList : Form
    {
        private IKeyboardMouseEvents _globalHook;

        public bool canReplace { get; set; }

        private string _priorityTag = "<priority>";
        private string _priorityWord = " Priority";
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
            if (!_settings.SettingsFileExists()) await _settings.CreateSettingsFileAsync();
            Affairs.Left = _settings.GetProfileX();
            Affairs.Top = _settings.GetProfileY();
        }
        private async Task LoadTextAsync()
        {

            Affairs.Text = "";
            if (_settings.CurrentListNotNull())
            {
                StringBuilder affairsShower = new StringBuilder();
                string currentAffair = "";
                await foreach (string affair in File.ReadLinesAsync(_settings.GetCurrentProfile()))
                {
                    currentAffair = affair;
                    if (currentAffair.EndsWith(_priorityTag))
                    {
                        currentAffair = currentAffair.Replace($". {_priorityTag}", $"{_priorityWord}.");
                    }
                    if (currentAffair.StartsWith(_deadlineTag))
                    {
                        currentAffair = currentAffair.Remove(0, _deadlineTag.Length);
                    }
                    affairsShower.AppendLine(currentAffair.Trim());
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
            if (canReplace || _settings.CanBeAlwaysReplaced()) ParentElement.SetLastPoint(e);
        }
        private void OnListMouseMove(MouseEventArgs e)
        {
            if ((canReplace || _settings.CanBeAlwaysReplaced()) && e.Button == MouseButtons.Left)
            {
                ParentElement.MoveChildForm(e);

                SpecifyListLocation();
                TopMost = true;
            }
        }
        private void SpecifyAffairsLocation()
        {
            if(Affairs.Left + Affairs.Width - 310 >= _settings.screenWidth)
            {
                Affairs.Left = _settings.screenWidth - Affairs.Width - Affairs.Width/4 + 315;
            }
            if (Affairs.Top + Affairs.Height >= _settings.screenHeight)
            {
                Affairs.Top = _settings.screenHeight - Affairs.Height*5 - Affairs.Height/2;
            }
            _settings.SetProfileX(Left + Affairs.Left);
            _settings.SetProfileY(Top + Affairs.Top);
            _settings.SaveSettings();
        }
        private void SpecifyListLocation()
        {
            if (Left + Affairs.Left + 310 >= _settings.screenWidth)
            {
                Left = _settings.screenWidth - 310 - Affairs.Left;
            }
            else if (Affairs.Left + Left <= 0)
            {
                Left = -Affairs.Left;
            }
            if (Top + Affairs.Top + Affairs.Height - 40 >= _settings.screenHeight)
            {
                Top = _settings.screenHeight - Affairs.Top - Affairs.Height + 40;
            }
            else if (Affairs.Top + Top <= 0)
            {
                Top = -Affairs.Top;
            }
        }
        private async Task OnListMouseUpAsync(MouseEventArgs e)
        {
            _settings.SetProfileX(Left + Affairs.Left);
            _settings.SetProfileY(Top + Affairs.Top);
            await _settings.SaveSettingsAsync();
            TopMost = true;
            if (canReplace)
            {
                Close();
                canReplace = false;
                return;
            }
        }
        public Label GetAffairs()
        {
            return Affairs;
        }

        private void ToDoList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Affairs.BackColor = _settings.GetBgColor();
            canReplace = false;
            SystemEvents.PowerModeChanged -= OnPowerModeChanged;
        }
        private void UpdateLocation()
        {
            Affairs.Left = Affairs.Left;
            Affairs.Top = Affairs.Top;
        }
    }
}
