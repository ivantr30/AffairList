namespace AffairList
{
    public partial class AffairList : Form, IParentable
    {
        private readonly NotifyIcon _trayIcon = new NotifyIcon();
        private ContextMenuStrip _trayMenu = new ContextMenuStrip();

        private LoadTimeManager _loadTimeManager;
        private Settings _settings;

        private Form _childForm;
        private MainMenu _mainMenu;

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

            _mainMenu = new MainMenu(_settings, _loadTimeManager, this);
            SetControl(_mainMenu);
        }
        private void AffairList_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }
        private void AffairList_Shown(object sender, EventArgs e)
        {
            TopMost = false;
        }
        private void OnOpen(object sender, EventArgs e)
        {
            if (_childForm != null)
            {
                _childForm.Show();
                return;
            } 
            TopMost = true;
            ShowInTaskbar = true;
            Show();
        }

        private void OnExit(object sender, EventArgs e) => Exit();

        public void Exit()
        {
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            _trayMenu?.Dispose();
            _childForm?.Dispose();
            Application.Exit();
        }
        public void Return()
        {
            if (Controls[0] != _mainMenu)
                SetControl(_mainMenu);
        }
        public void MinimizeForm() => WindowState = FormWindowState.Minimized;
        public void SetControl(Control control)
        {
            Width = control.Width;
            Height = control.Height;
            Controls.Clear();
            Controls.Add(control);
            if(control is IChildable childControl)
            {
                childControl.OnAddition();
            }
            Focus();
        }
        public void OpenForm(Form form)
        {
            Hide();
            _childForm = form;
            _childForm?.ShowDialog();
            _childForm?.Dispose();
            _childForm = null;
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
        public void MoveChildForm(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _childForm.Left += e.X - LastPoint.X;
                _childForm.Top += e.Y - LastPoint.Y;
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
