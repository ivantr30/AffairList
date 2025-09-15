namespace AffairList
{
    public partial class AffairList : Form, IParentable
    {
        private readonly NotifyIcon _trayIcon = new NotifyIcon();
        private ContextMenuStrip _trayMenu = new ContextMenuStrip();

        private LoadTimeManager _loadTimeManager;
        private Settings _settings;

        private Form childForm;
        private Control mainMenu;

        public Point LastPoint { get; set; }

        public AffairList()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            _settings = new Settings();
            _loadTimeManager = new LoadTimeManager(_settings);

            _trayMenu.Items.Add("Открыть", null, OnOpen!);
            _trayMenu.Items.Add("Выход", null, OnExit!);

            _trayIcon.Text = "AffairList";
            _trayIcon.Icon = Icon.ExtractAssociatedIcon("AffairListLogo.ico");

            _trayIcon.ContextMenuStrip = _trayMenu;
            _trayIcon.Visible = true;

            mainMenu = new MainMenu(_settings, _loadTimeManager, this);
            SetControl(mainMenu);
        }
        private void AffairList_Shown(object sender, EventArgs e)
        {
            TopMost = false;
        }
        private void OnOpen(object sender, EventArgs e)
        {
            TopMost = true;
            ShowInTaskbar = true;
            Show();
            childForm?.Close();
        }

        private void OnExit(object sender, EventArgs e) => Exit();

        public void Exit()
        {
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            Application.Exit();
        }
        public void Return()
        {
            if (Controls[0] != mainMenu)
                SetControl(mainMenu);
        }
        public void MinimizeForm() => WindowState = FormWindowState.Minimized;
        public void SetControl(Control control)
        {
            if (control is IKeyPreviewable kp)
            {
                KeyDown += kp.KeyDownHandlers;
                KeyPress += kp.KeyPressHandlers;
                KeyDown += kp.KeyUpHandlers;
            }
            if (Controls.Count > 0)
                if (Controls[0] is IKeyPreviewable ckp)
                {
                    KeyDown -= ckp.KeyDownHandlers;
                    KeyPress -= ckp.KeyPressHandlers;
                    KeyDown -= ckp.KeyUpHandlers;
                }
            Width = control.Width;
            Height = control.Height;
            Controls.Clear();
            Controls.Add(control);
            Focus();
        }
        public void OpenForm(Form form)
        {
            Hide();
            childForm = form;
            childForm?.ShowDialog();
            Show();
        }
        public void SetLastPoint(MouseEventArgs e) => LastPoint = new Point(e.X, e.Y);
        public void MoveForm(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - LastPoint.X;
                Top += e.Y - LastPoint.Y;
            }
        }

        private void AffairList_FormClosing(object sender, FormClosingEventArgs e)
            => Exit();

        private void AffairList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _settings.GetCloseKey())
            {
                Exit();
            }
            if (e.KeyCode == _settings.GetReturnKey())
            {
                Return();
            }
        }
    }
}
