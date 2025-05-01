using Gma.System.MouseKeyHook;

namespace AffairList
{
    public partial class List : Form
    {
        private IKeyboardMouseEvents globalHook;
        public bool canReplace = false;

        Point lastPoint;
        public List()
        {
            InitializeComponent();
            SubscribeGlobalHook();
            LoadText();
            SetLocation();
        }
        private void Exit()
        {
            AffairList.trayIcon.Visible = false;
            Application.Exit();
        }
        private void Return()
        {
            AffairList.trayIcon.Visible = false;
            this.Close();
            Application.Restart();
            Application.Run();
        }
        private void SetLocation()
        {
            TopMost = true;

            LoadSettings();
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height + Screen.PrimaryScreen.WorkingArea.Height / 10;
            Affairs.AutoSize = false;
            Affairs.Padding = new Padding(0, 0, 180, 0);
            Affairs.Size = new Size(500, Height);
            Affairs.ForeColor = Config.textColor;
            BackColor = Config.bgtextColor;
            TransparencyKey = Config.bgtextColor;
        }
        private void LoadSettings()
        {
            if (File.Exists(Application.StartupPath + "\\settings.txt"))
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
            if (File.Exists(Application.StartupPath + "\\list.txt"))
            {
                string[] result = File.ReadAllLines(Application.StartupPath + "\\list.txt");
                for(int i = 0; i < result.Length; i++)
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
                Exit();
            }
            if (e.KeyCode == Keys.F6)
            {
                Return();
            }
        }

        private void List_MouseDown(object sender, MouseEventArgs e)
        {
            if (canReplace)
                lastPoint = new Point(e.X, e.Y);
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                this.Left += e.X - Affairs.Left;
                this.Top += e.Y - Affairs.Top;
            }
        }

        private void List_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                canReplace = false;
                if (File.Exists(Application.StartupPath + "\\settings.txt"))
                {
                    var settingLines = File.ReadAllLines(Application.StartupPath + "\\settings.txt");
                    for(int i = 0; i < settingLines.Length; i++)
                    {
                        if (settingLines[i].StartsWith("x,y:"))
                        {
                            settingLines[i] = "x,y: " + (this.Left + e.X) + " " + (this.Top + e.Y);
                        }
                    }
                    File.WriteAllLines(Application.StartupPath + "\\settings.txt", settingLines);
                }
                Return();
            }
        }
    }
}
