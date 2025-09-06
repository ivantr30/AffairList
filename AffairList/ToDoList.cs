using Gma.System.MouseKeyHook;

namespace AffairList
{
    public partial class ToDoList : BaseForm
    {
        private IKeyboardMouseEvents _globalHook;

        public bool canReplace { get; set; } = false;

        private string _priorityTag = "<priority>";
        private string _deadlineTag = "<deadline>";
        public IParentable ParentElement { get; set; }

        public ToDoList(Settings settings, IParentable parent)
            : base(settings)
        {
            InitializeComponent();
            Task.WhenAll(SetLocation(), LoadText());

            SubscribeGlobalHook();
            ParentElement = parent;
        }
        private async Task SetLocation()
        {
            TopMost = true;

            await LoadSettings();

            Width = settings.width;
            Height = settings.height + settings.height / 10;

            Affairs.AutoSize = false;
            Affairs.Size = new Size(500, Height);
            Affairs.Padding = new Padding(0, 0, 180, 0);

            Affairs.ForeColor = settings.GetTextColor();

            BackColor = settings.GetBgColor();
            TransparencyKey = settings.GetBgColor();
        }
        private async Task LoadSettings()
        {
            if (!settings.SettingsFileExists()) await settings.CreateSettingsFile();
            Affairs.Left = settings.GetProfileX();
            Affairs.Top = settings.GetProfileY();
        }
        private async Task LoadText()
        {
            if (settings.CurrentListNotNull())
            {
                string[] text = await File.ReadAllLinesAsync(settings.GetCurrentProfile());
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
            if (e.KeyCode == settings.GetCloseKey())
            {
                Exit();
            }
            if (e.KeyCode == settings.GetReturnKey())
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
            if(canReplace) SetLastPoint(e);
        }
        private void OnListMouseMove(MouseEventArgs e)
        {
            if (canReplace) MoveForm(e);
        }
        private async void OnListMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                canReplace = false;
                settings.SetProfileX(Left + Affairs.Left);
                settings.SetProfileY(Top + Affairs.Top);
                await settings.SaveSettings();
                Close();
            }
        }
        public Label GetAffairs()
        {
            return Affairs;
        }
    }
}
