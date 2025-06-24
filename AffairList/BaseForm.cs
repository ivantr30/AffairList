
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
    }
}
