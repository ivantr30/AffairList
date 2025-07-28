using Gma.System.MouseKeyHook;

namespace AffairList
{
    public partial class ToDoList : BaseForm
    {
        private IKeyboardMouseEvents _globalHook;

        public bool canReplace { get; set; } = false;

        private string _priorityTag = "<priority>";
        private string _deadlineTag = "<deadline>";

        public ToDoList(Settings settings)
            : base(settings)
        {
            InitializeComponent();
            SubscribeGlobalHook();

            LoadText();
            SetLocation();
        }
        private void SetLocation()
        {
            TopMost = true;

            LoadSettings();

            Width = settings.width;
            Height = settings.height + settings.height / 10;

            Affairs.AutoSize = false;
            Affairs.Size = new Size(500, Height);
            Affairs.Padding = new Padding(0, 0, 180, 0);

            Affairs.ForeColor = settings.GetTextColor();

            BackColor = settings.GetBgColor();
            TransparencyKey = settings.GetBgColor();
        }
        private void LoadSettings()
        {
            if (settings.SettingsFileExists())
            {
                Affairs.Left = settings.GetProfileX();
                Affairs.Top = settings.GetProfileY();
            }
            else
            {
                MessageBox.Show("Нет настроек");
                Restart();
            }
        }
        private void LoadText()
        {
            if (settings.CurrentListNotNull())
            {
                string[] result = File.ReadAllLines(settings.GetCurrentProfile());
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i].EndsWith(_priorityTag))
                    {
                        result[i] = result[i].Substring(0, result[i].Length - _priorityTag.Length)
                            .Trim();
                    }
                    if (result[i].StartsWith(_deadlineTag))
                    {
                        result[i] = result[i].Substring(_deadlineTag.Length);
                    }
                }
                Affairs.Text = string.Join("\n", result);
            }
            else
            {
                MessageBox.Show("Ошибка, не найден текущий список дел");
                Restart();
            }
            Affairs.Text += "\nЭто весь список ваших дел";
        }
        private void SubscribeGlobalHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHook_KeyDown;
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == settings.GetCloseKey())
            {
                Exit();
            }
            if (e.KeyCode == settings.GetReturnKey())
            {
                Restart();
            }
        }

        private void Affairs_MouseDown(object sender, MouseEventArgs e)
        {
            OnListMouseDown(e);
        }

        private void Affairs_MouseMove(object sender, MouseEventArgs e)
        {
            OnListMouseMove(e);
        }

        private void Affairs_MouseUp(object sender, MouseEventArgs e)
        {
            OnListMouseUp(e);
        }

        private void List_MouseDown(object sender, MouseEventArgs e)
        {
            OnListMouseDown(e);
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            OnListMouseMove(e);
        }

        private void List_MouseUp(object sender, MouseEventArgs e)
        {
            OnListMouseUp(e);
        }
        private void OnListMouseDown(MouseEventArgs e)
        {
            if(canReplace) SetLastPoint(e);
        }
        private void OnListMouseMove(MouseEventArgs e)
        {
            if (canReplace) MoveForm(e);
        }
        private void OnListMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                canReplace = false;
                settings.SetProfileX(Left + Affairs.Left);
                settings.SetProfileY(Top + Affairs.Top);
                settings.SaveSettings();
                Restart();
            }
        }
        public Label GetAffairs()
        {
            return Affairs;
        }

        private void List_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }
    }
}
