
namespace AffairList
{
    public partial class BaseForm : Form
    {
        protected Point lastPoint;
        protected SettingsModel settings;
        public BaseForm(SettingsModel settings)
        {
            InitializeComponent();
        }
        public BaseForm()
        {
            InitializeComponent();
        }
        protected virtual void Exit()
        {
            AffairList.trayIcon.Visible = false;
            Application.Exit();
        }
        protected virtual void Restart()
        {
            AffairList.trayIcon.Visible = false;
            Application.Restart();
        }
        protected void SetLastPoint(MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        protected void MoveForm(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }
        protected void CreateForm(BaseForm form)
        {
            Hide();
            form.Show();
        }
        protected void MinimizeForm()
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
