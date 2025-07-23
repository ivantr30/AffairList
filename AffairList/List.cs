using Gma.System.MouseKeyHook;

namespace AffairList
{
    public partial class List : BaseForm
    {
        private IKeyboardMouseEvents globalHook;

        public bool canReplace = false;

        private string priorityTag = "<priority>";
        private string deadlineTag = "<deadline>";

        public List(SettingsModel settings)
        {
            InitializeComponent();
            SubscribeGlobalHook();
            this.settings = settings;

            LoadText();
            SetLocation();
        }
        protected override void Restart()
        {
            Close();
            base.Restart();
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
            Affairs.ForeColor = settings.textColor;
            BackColor = settings.bgtextColor;
            TransparencyKey = settings.bgtextColor;
        }
        private void LoadSettings()
        {
            if (settings.SettingsFileExists())
            {
                Affairs.Left = settings.x;
                Affairs.Top = settings.y;
            }
            else
            {
                MessageBox.Show("Нет настроек");
            }
        }
        private void LoadText()
        {
            if (settings.CurrentListNotNull())
            {
                string[] result = File.ReadAllLines(settings.currentListFileFullPath);
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i].EndsWith(priorityTag))
                    {
                        result[i] = result[i].Substring(0, result[i].Length - priorityTag.Length)
                            .Trim();
                    }
                    if (result[i].StartsWith(deadlineTag))
                    {
                        result[i] = result[i].Substring(deadlineTag.Length);
                    }
                }
                Affairs.Text = string.Join("\n", result);
            }
            else
            {
                Affairs.Text = "Нет дел";
            }
            Affairs.Text += "\nЭто весь список ваших дел";
        }
        private void SubscribeGlobalHook()
        {
            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += GlobalHook_KeyDown;
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == settings.closeKey)
            {
                Exit();
            }
            if (e.KeyCode == settings.returnKey)
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
            if (canReplace) lastPoint = new Point(e.X, e.Y);
        }
        private void OnListMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }
        private void OnListMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                canReplace = false;
                settings.SaveParametr("x,y", Left + Affairs.Left, Top + Affairs.Top);
                Restart();
            }
        }

        private void List_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }
    }
}
