
using Microsoft.Win32;
using System.Windows.Forms;

namespace AffairList
{
    public partial class AffairList : Form
    {
        public static NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        public AffairList()
        {
            InitializeComponent();

            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Открыть", null, OnOpen);
            trayMenu.Items.Add("Выход", null, OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "AffairList";
            trayIcon.Icon = SystemIcons.Application;

            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;

            Config.CreateFiles();
            Config.ConfigureSettings();
        }
        private void OnOpen(object sender, EventArgs e)
        {
            Config.Restart();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Config.Exit();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Config.Exit();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
        {
            Config.lastPoint = new Point(e.X, e.Y);
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - Config.lastPoint.X;
                Top += e.Y - Config.lastPoint.Y;
            }
        }

        private void AffairListLab_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - Config.lastPoint.X;
                Top += e.Y - Config.lastPoint.Y;
            }
        }

        private void AffairListLab_MouseDown(object sender, MouseEventArgs e)
        {
            Config.lastPoint = new Point(e.X, e.Y);
        }

        private void OpenListButton_Click(object sender, EventArgs e)
        {
            Hide();
            List list = new List();
            list.Show();
        }

        private void ClearListButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure to clear your affair list?",
            "Confirm window",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (!File.Exists(Config.listFileFullPath))
                {
                    MessageBox.Show("Error, list file does not exist");
                    return;
                }
                File.WriteAllText(Config.listFileFullPath, "");
                MessageBox.Show("The list is cleared");
            }
        }

        private void ChangeListButton_Click(object sender, EventArgs e)
        {
            Hide();
            ChangeListForm listForm = new ChangeListForm();
            listForm.Show();
        }

        private void ReplaceAffairListButton_Click(object sender, EventArgs e)
        {
            Hide();
            List list = new List();
            list.BackColor = Color.White;
            list.canReplace = true;
            list.Show();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Hide();
            Settings settings = new Settings();
            settings.Show();
        }

        private void AffairList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }
    }
}
