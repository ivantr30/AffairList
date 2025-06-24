namespace AffairList
{
    public partial class AffairList : BaseForm
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

            settings = new SettingsModel();
        }
        private void OnOpen(object sender, EventArgs e)
        {
            Restart();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Exit();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Exit();
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
            lastPoint = new Point(e.X, e.Y);
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }

        private void AffairListLab_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }

        private void AffairListLab_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void OpenListButton_Click(object sender, EventArgs e)
        {
            if (!settings.CurrentListNotNull())
            {
                MessageBox.Show("Error, there is no list available");
                return;
            }
            Hide();
            List list = new List(settings);
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
                if (!settings.CurrentListNotNull())
                {
                    MessageBox.Show("Error, list file does not exist");
                    return;
                }
                File.WriteAllText(settings.currentListFileFullPath, "");
                MessageBox.Show("The list is cleared");
            }
        }

        private void ChangeListButton_Click(object sender, EventArgs e)
        {
            if (!settings.ListFilesAvailable())
            {
                DialogResult createDefault = MessageBox.Show("There are no profiles available," +
                    " do you want to add default?",
                    "Confirm Form",
                    MessageBoxButtons.YesNo);
                if (createDefault == DialogResult.Yes)
                {
                    settings.CreateDefaultList();
                }
                else
                {
                    return;
                }
            }
            Hide();
            ChangeListForm listForm = new ChangeListForm(settings);
            listForm.Show();
        }

        private void ReplaceAffairListButton_Click(object sender, EventArgs e)
        {
            if (!settings.CurrentListNotNull())
            {
                MessageBox.Show("Error, there is no list available");
                return;
            }
            Hide();
            List list = new List(settings);
            list.BackColor = Color.White;
            list.canReplace = true;
            list.Show();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Hide();
            Settings settingsForm = new Settings(settings);
            settingsForm.Show();
        }
        private void ChangeProfileButton_Click(object sender, EventArgs e)
        {
            Hide();
            ChangeProfileForm profileForm = new ChangeProfileForm(settings);
            profileForm.Show();
        }
        private void HotKeyButton_Click(object sender, EventArgs e)
        {
            Hide();
            HotKeySettings hotKeySettings = new HotKeySettings(settings);
            hotKeySettings.Show();
        }
        private void AffairList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
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

        private void AffairList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == settings.closeKey)
            {
                Exit();
            }
        }

        private void MusicPlayerButton_Click(object sender, EventArgs e)
        {

        }

        private void AffairList_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        private void AffairList_Shown(object sender, EventArgs e)
        {
            TopMost = false;
        }
    }
}
