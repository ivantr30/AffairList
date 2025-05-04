using Gma.System.MouseKeyHook;

namespace AffairList
{
    public partial class List : Form
    {
        private IKeyboardMouseEvents globalHook;
        public bool canReplace = false;

        public List()
        {
            InitializeComponent();
            SubscribeGlobalHook();
            LoadText();
            SetLocation();
        }
        private void Return()
        {
            AffairList.trayIcon.Visible = false;
            Close();
            Application.Restart();
            Application.Run();
        }
        private void SetLocation()
        {
            TopMost = true;

            LoadSettings();
            Width = Config.width;
            Height = Config.height + Config.height / 10;
            Affairs.AutoSize = false;
            Affairs.Padding = new Padding(0, 0, 180, 0);
            Affairs.Size = new Size(500, Height);
            Affairs.ForeColor = Config.textColor;
            BackColor = Config.bgtextColor;
            TransparencyKey = Config.bgtextColor;
        }
        private void LoadSettings()
        {
            if (File.Exists(Config.settingsFileFullPath))
            {
                Affairs.Left = Config.x;
                Affairs.Top = Config.y;
            }
            else
            {
                MessageBox.Show("Нет настроек");
            }
        }
        private void LoadText()
        {
            if (File.Exists(Config.currentListFileFullPath))
            {
                string[] result = File.ReadAllLines(Config.currentListFileFullPath);
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i].EndsWith("<priority>"))
                    {
                        result[i] = result[i].Substring(0, result[i].Length - "<priority>".Length);
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
            if (e.KeyCode == Keys.F7)
            {
                Config.Exit();
            }
            if (e.KeyCode == Keys.F6)
            {
                Return();
            }
        }

        private void List_MouseDown(object sender, MouseEventArgs e)
        {
            if (canReplace)
                Config.lastPoint = new Point(e.X, e.Y);
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                Left += e.X - Affairs.Left;
                Top += e.Y - Affairs.Top;
            }
        }

        private void List_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                canReplace = false;
                Config.SaveParametr("x,y", Left + e.X, Top + e.Y);
                Return();
            }
        }

        private void List_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Exit();
        }
    }
}
