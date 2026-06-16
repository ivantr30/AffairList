using Gma.System.MouseKeyHook;
using Microsoft.Win32;
using System.Text;
using AffairList.Services.Managers;
using AffairList.Services.Providers;
using AffairList.Services.Models;

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
            Affairs.Left = _settings.Data.TodoListX;
            Affairs.Top = _settings.Data.ToDoListY;
            await LoadTextAsync();
            SetLocation();

            SpecifyAffairsLocation();
            await SaveAffairsLocation();
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

            Affairs.ForeColor = _settings.Data.TextColor;

            BackColor = _settings.Data.BgColor;
            TransparencyKey = _settings.Data.BgColor;
            SystemEvents.PowerModeChanged += OnPowerModeChanged;
        }
        private async Task LoadTextAsync()
        {
            Affairs.Text = "";
            if (_settings.CurrentListExists())
            {
                StringBuilder affairsShower = new StringBuilder();
                AffairsCollection affairs = await AffairsProvider.GetAffairsAsync(_settings.Data.CurrentProfileFullPath);
                if (affairs.Affairs.Any())
                {
                    affairsShower.Append("* ");
                    affairsShower.AppendJoin("\n* ", affairs.Affairs);
                    affairsShower.Replace(_priorityTag, _priorityWord);
                    affairsShower.Replace(_deadlineTag, "");
                    Affairs.Text += affairsShower.ToString() + "\nЭто весь список ваших дел";
                    affairsShower.Clear();
                }
                else Affairs.Text = "Это весь список ваших дел";
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
                RefreshLocation();
            }
        }
        private void SubscribeGlobalHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHook_KeyDown!;
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _settings.Data.CloseKey)
            {
                ParentElement.Exit();
            }
            if (e.KeyCode == _settings.Data.ReturnKey)
            {
                Close();
            }
        }

        private void Affairs_MouseDown(object sender, MouseEventArgs e) => OnListMouseDown(e);
        private async void Affairs_MouseMove(object sender, MouseEventArgs e) => await OnListMouseMove(e);
        private async void Affairs_MouseUp(object sender, MouseEventArgs e) => await OnListMouseUpAsync(e);

        private void List_MouseDown(object sender, MouseEventArgs e) => OnListMouseDown(e);
        private async void List_MouseMove(object sender, MouseEventArgs e) => await OnListMouseMove(e);
        private async void List_MouseUp(object sender, MouseEventArgs e) => await OnListMouseUpAsync(e);

        private void OnListMouseDown(MouseEventArgs e)
        {
            if (CanReplace || _settings.Data.CanBeAlwaysReplaced)
                _dragOffset = new Point(Cursor.Position.X - Affairs.Left, Cursor.Position.Y - Affairs.Top);
        }
        private async Task OnListMouseMove(MouseEventArgs e)
        {
            if ((CanReplace || _settings.Data.CanBeAlwaysReplaced) && e.Button == MouseButtons.Left)
            {
                int newLeft = Cursor.Position.X - _dragOffset.X;
                int newTop = Cursor.Position.Y - _dragOffset.Y;

                newLeft = Math.Clamp(newLeft, 0, _settings.screenWidth - Affairs.Width + Affairs.Padding.Right);
                newTop = Math.Clamp(newTop, 0, _settings.screenHeight - Affairs.Height);

                Affairs.Left = newLeft;
                Affairs.Top = newTop;

                TopMost = true;
            }
        }
        // ПРЕОБРАЗОВАТЬ профили В ОБЪЕКТЫ
        // Доделать loadtimemanager( + notify сделать асинхронным)
        // придумать механизм защиты данных от потери во время выхода из программы
        // локализация
        // Подгрузка текста при заходе в прогу и дальнейшее использование
        // Индикатор несохранённых данных
        // Сделать проверку на занятые клавиши в настройках
        // Добавить автопроверку для notificate в фоне
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

        private async Task SaveAffairsLocation()
        {
            _settings.Data.TodoListX = Affairs.Left;
            _settings.Data.ToDoListY = Affairs.Top;
            await _settings.SaveSettingsAsync();
        }

        private async Task OnListMouseUpAsync(MouseEventArgs e)
        {
            await SaveAffairsLocation();
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
            Affairs.BackColor = _settings.Data.BgColor;
            CanReplace = false;
            SystemEvents.PowerModeChanged -= OnPowerModeChanged; 
            _globalHook?.Dispose();
        }
        private void RefreshLocation()
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
