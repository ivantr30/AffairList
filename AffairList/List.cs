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
        private void SetLocation()
        {
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height + Screen.PrimaryScreen.WorkingArea.Height / 10;
            TopMost = true;

            Affairs.Left = Width - Width / 6;
            Affairs.AutoSize = false;
            Affairs.Padding = new Padding(0, 0, 180, 0);
            Affairs.Size = new Size(500, Height);
        }
        private void LoadText()
        {
            if (File.Exists(Application.StartupPath + "\\list.txt"))
            {
                Affairs.Text = File.ReadAllText(Application.StartupPath + "\\list.txt");
            }
            else
            {
                Affairs.Text = "Нет дел";
            }
            Affairs.Text += "Это весь список ваших дел";
        }
        private void SubscribeGlobalHook()
        {
            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += GlobalHook_KeyDown;
        }
        private void CloseList()
        {

            this.Close();
            Application.Restart();
            Application.Run(new AffairList());
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.F6)
            {
                CloseList();
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
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void List_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canReplace)
            {
                canReplace = false;
                CloseList();
            }
        }
    }
}
