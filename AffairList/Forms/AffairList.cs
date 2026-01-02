namespace AffairList
{
    public partial class AffairList : Form, IParentable
    {
        private LoadTimeManager _loadTimeManager = null!;
        private Settings _settings = null!;
        private TrayIconManager _trayIconManager = null!;

        private Form _childForm;
        private MainMenu _mainMenu = null!;

        public Point LastPoint { get; set; }

        public AffairList()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            _trayIconManager = new TrayIconManager();
            _trayIconManager.AddTrayMenuAction("Open", OnOpen!);
            _trayIconManager.AddTrayMenuAction("Close", OnExit!);

            _settings = new Settings();
            _loadTimeManager = new LoadTimeManager(_settings, _trayIconManager.TrayIcon);

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
            _trayIconManager.Dispose();
            _childForm?.Dispose();
            Application.Exit();
        }
        public void Return()
        {
            if (Controls[0] != _mainMenu)
            {
                SetControl(_mainMenu);
            }
        }
        public void MinimizeForm() => WindowState = FormWindowState.Minimized;
        public void SetControl(Control control)
        {
            if (Controls.Count > 0 && Controls[0] is IKeyPreviewable ckp)
            {
                KeyDown -= ckp.KeyDownHandlers;
                KeyPress -= ckp.KeyPressHandlers;
                KeyUp -= ckp.KeyUpHandlers;
            }
            if (control is IKeyPreviewable kp)
            {
                KeyDown += kp.KeyDownHandlers;
                KeyPress += kp.KeyPressHandlers;
                KeyUp += kp.KeyUpHandlers;
            }
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
        public void OpenForm(Form form, bool asDialog)
        {
            Hide();
            _childForm = form;
            if (asDialog)
            {
                try
                {
                    _childForm.ShowDialog();
                }
                catch (ObjectDisposedException) { }
                finally
                {
                    AfterFormClosed();
                }
            }
            else
            {
                try
                {
                    _childForm.Show();
                    _childForm.FormClosed += AfterFormClosed;
                }
                catch (ObjectDisposedException) { AfterFormClosed(); }
            }
        }
        private void AfterFormClosed(object? sender, FormClosedEventArgs e)
        {
            AfterFormClosed();
        }
        private void AfterFormClosed()
        {
            _childForm.FormClosed -= AfterFormClosed;
            _childForm?.Dispose();
            _childForm = null;
            TopMost = true;
            Show();
            if (Controls[0] == _mainMenu)
            {
                _mainMenu.OnAddition();
            }
            TopMost = false;
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

        private async void AffairList_KeyDown(object sender, KeyEventArgs e)
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
