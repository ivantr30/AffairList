using AffairList.Services.Managers;

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

        private AffairList()
        {
            InitializeComponent();
        }

        public static async Task<AffairList> CreateAsync()
        {
            var instance = new AffairList();
            await instance.InitializeAsync();
            return instance;
        }

        private async Task InitializeAsync()
        {
            _trayIconManager = new TrayIconManager();
            _trayIconManager.AddTrayMenuAction("Open", OnOpen!);
            _trayIconManager.AddTrayMenuAction("Close", OnExit!);

            _settings = new Settings();
            _loadTimeManager = new LoadTimeManager(_settings, _trayIconManager.TrayIcon);

            _mainMenu = new MainMenu(_settings, _loadTimeManager, this);
            await SetControlAsync(_mainMenu);
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
            Application.Exit();
        }
        private void DisposeObjects()
        {
            _trayIconManager.Dispose();
            _childForm?.Dispose();
        }
        public async Task ReturnAsync()
        {
            if (Controls[0] != _mainMenu)
            {
                await SetControlAsync(_mainMenu);
            }
        }
        public void MinimizeForm() => WindowState = FormWindowState.Minimized;
        public async Task SetControlAsync(Control control)
        {
            if (!await OnControlRemoveAsync(closing: false)) return;
            SuspendLayout();
            if (Controls.Count > 0)
            {
                if (Controls[0] is IKeyPreviewable ckp)
                {
                    KeyDown -= ckp.KeyDownHandlers;
                    KeyPress -= ckp.KeyPressHandlers;
                    KeyUp -= ckp.KeyUpHandlers;
                }
            }
            if (control is IKeyPreviewable kp)
            {
                KeyDown += kp.KeyDownHandlers;
                KeyPress += kp.KeyPressHandlers;
                KeyUp += kp.KeyUpHandlers;
            }
            Controls.Clear();
            Controls.Add(control);
            Width = control.Width;
            Height = control.Height;
            if(control is IChildable childControl)
            {
                await childControl.OnAdditionAsync();
            }
            ResumeLayout();
            Focus();
        }
        public async Task OpenFormAsync(Form form, bool asDialog)
        {
            if (!await OnControlRemoveAsync(false)) return;
            Hide();
            _childForm = form;
            if (asDialog)
            {
                if (!_childForm.IsDisposed)
                {
                    _childForm.ShowDialog();
                }
                await AfterFormClosedAsync();
            }
            else
            {
                if (!_childForm.IsDisposed)
                {
                    _childForm.Show();
                    _childForm.FormClosed += AfterFormClosed;
                }
                else await AfterFormClosedAsync();
            }
        }
        private async Task<bool> OnControlRemoveAsync(bool closing)
        {
            if (Controls.Count > 0)
            {
                if (Controls[0] is IChildable child)
                {
                    return await child.OnRemovingAsync(closing);
                }
            }
            return true;
        }
        private async void AfterFormClosed(object? sender, FormClosedEventArgs e)
        {
            await AfterFormClosedAsync();
        }
        private async Task AfterFormClosedAsync()
        {
            _childForm.FormClosed -= AfterFormClosed;
            _childForm?.Dispose();
            _childForm = null;
            TopMost = true;
            Show();
            if (Controls[0] == _mainMenu)
            {
                await _mainMenu.OnAdditionAsync();
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

        private async void AffairList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!await OnControlRemoveAsync(true))
            {
                e.Cancel = true;
                TopMost = true;
                TopMost = false;
                return;
            }
            DisposeObjects();
        }

        private async void AffairList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _settings.Data.CloseKey)
            {
                _trayIconManager.Dispose();
                Exit();
            }
            if (e.KeyCode == _settings.Data.ReturnKey)
            {
                await ReturnAsync();
            }
        }
    }
}
